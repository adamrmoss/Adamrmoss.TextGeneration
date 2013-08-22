using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuardClaws;

namespace TNW.TextGeneration
{
  public class WordBuilder
  {
    private const int StandardMaxNumberOfAttempts = 10;

    public int MaxNumberOfAttempts { get; set; }
    public WordAnalyzer WordAnalyzer { get; set; }
    public int? Seed { get; set; }

    private HashSet<string> DisallowedNames;
    private Random Random;

    private int[] WordLengthChoiceArray;
    private string[] InitialSubwordChoiceArray;
    private Dictionary<string, string[]> SubwordFollowingChoiceArrays;
    
    public WordBuilder() {
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
      this.SubwordFollowingChoiceArrays = this.WordAnalyzer.SubwordFollowingFrequency.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToChoiceArray());
    }

    private void BuildRandomNumberGenerator() {
      this.Random = this.Seed == null ? new Random() : new Random(this.Seed.Value);
    }

    private string NextName() {
      var stopLength = this.WordLengthChoiceArray.GetRandomElement(this.Random);

      var stringBuilder = new StringBuilder();
      var terminatingSubword = (string) null;
      while (stringBuilder.Length < stopLength && (terminatingSubword == null || this.SubwordFollowingChoiceArrays.ContainsKey(terminatingSubword))) {
        var newSubword = this.GetNewSubword(terminatingSubword);

        terminatingSubword = newSubword;
        stringBuilder.Append(newSubword);
      }

      return stringBuilder.ToString();
    }

    private string GetNewSubword(string terminatingSubword) {
      if (terminatingSubword == null) {
        return this.InitialSubwordChoiceArray.GetRandomElement(this.Random);
      } else {
        if (this.SubwordFollowingChoiceArrays.ContainsKey(terminatingSubword)) {
          return this.SubwordFollowingChoiceArrays[terminatingSubword].GetRandomElement(this.Random);
        } else {
          return "-" + this.InitialSubwordChoiceArray.GetRandomElement(this.Random);
        }
      }
    }
  }
}
