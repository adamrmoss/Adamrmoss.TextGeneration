using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuardClaws;

namespace TNW.TextGeneration
{
  public class NamesBuilder
  {
    private const int StandardMaxNumberOfAttempts = 10;

    public int MaxNumberOfAttempts { get; set; }
    public WordAnalyzer WordAnalyzer { get; set; }
    public int? Seed { get; set; }

    private HashSet<string> DisallowedNames;
    private Random Random;

    private int[] WordLengthChoiceArray;
    private string[] InitialSubwordChoiceArray;
    private Dictionary<char, string[]> LeadingCharacterSubwordChoiceArray;
    private Dictionary<char, char[]> CharacterFollowingChoiceArrays;
    
    public NamesBuilder() {
      this.MaxNumberOfAttempts = StandardMaxNumberOfAttempts;
      this.DisallowedNames = new HashSet<string>();
    }

    public IEnumerable<string> Build() {
      Claws.NotNull(() => this.WordAnalyzer);

      this.DisallowAnalyzedWords();
      this.BuildChoiceArrays();
      this.BuildRandomNumberGenerator();

      var failedAttempts = 0;
      while (failedAttempts < this.MaxNumberOfAttempts) {
        var nextName = this.NextName();

        if (this.DisallowedNames.Contains(nextName)) {
          failedAttempts++;
        } else {
          failedAttempts = 0;
          this.DisallowedNames.Add(nextName);
          yield return nextName;
        }
      }
    }

    private void DisallowAnalyzedWords() {
      foreach (var analyzedWord in this.WordAnalyzer.AnalyzedWords) {
        this.DisallowedNames.Add(analyzedWord);
      }
    }

    private void BuildChoiceArrays() {
      this.WordLengthChoiceArray = this.WordAnalyzer.WordLengthFrequency.ToChoiceArray();
      this.InitialSubwordChoiceArray = this.WordAnalyzer.InitialSubwordFrequency.ToChoiceArray();
      this.LeadingCharacterSubwordChoiceArray = this.WordAnalyzer.SubwordFrequency.GroupBy(kvp => kvp.Key[0])
                                                                        .ToDictionary(grouping => grouping.Key, grouping => grouping.ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
                                                                                                                                    .ToChoiceArray()); 
      this.CharacterFollowingChoiceArrays = this.WordAnalyzer.CharacterFollowingFrequency.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToChoiceArray());
    }

    private void BuildRandomNumberGenerator() {
      this.Random = this.Seed == null ? new Random() : new Random(this.Seed.Value);
    }

    private string NextName() {
      var stopLength = this.WordLengthChoiceArray.GetRandomElement(this.Random);

      var stringBuilder = new StringBuilder();
      var terminatingCharacter = (char?) null;
      while (stringBuilder.Length < stopLength && (terminatingCharacter == null || this.CharacterFollowingChoiceArrays.ContainsKey(terminatingCharacter.Value))) {
        var newSubword = this.GetNewSubword(terminatingCharacter);

        terminatingCharacter = newSubword.Last();
        stringBuilder.Append(newSubword);
      }

      return stringBuilder.ToString();
    }

    private string GetNewSubword(char? terminatingCharacter) {
      if (terminatingCharacter == null) {
        return this.InitialSubwordChoiceArray.GetRandomElement(this.Random);
      } else {
        var leadingCharacter = this.CharacterFollowingChoiceArrays[terminatingCharacter.Value].GetRandomElement(this.Random);
        if (this.LeadingCharacterSubwordChoiceArray.ContainsKey(leadingCharacter)) {
          return this.LeadingCharacterSubwordChoiceArray[leadingCharacter].GetRandomElement(this.Random);
        } else {
          return this.InitialSubwordChoiceArray.GetRandomElement(this.Random);
        }
      }
    }
  }
}
