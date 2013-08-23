using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuardClaws;

namespace TNW.TextGeneration
{
  public class WordBuilder : IEnumerable<string>
  {
    private const int StandardMaxNumberOfAttempts = 20;
    private const int StandardSeed = 69;

    private readonly WordAnalyzer wordAnalyzer;
    private readonly HashSet<string> disallowedWords;

    private int maxNumberOfAttempts;
    private int? seed;
    private bool capitalize;

    private Random random;

    private int[] wordLengthChoiceArray;
    private string[] initialSubwordChoiceArray;
    private Dictionary<string, string[]> subwordFollowingChoiceArrays;

    public WordBuilder(WordAnalyzer wordAnalyzer) {
      Claws.NotNull(() => wordAnalyzer);
      this.wordAnalyzer = wordAnalyzer;

      this.maxNumberOfAttempts = StandardMaxNumberOfAttempts;
      this.seed = StandardSeed;
      this.disallowedWords = new HashSet<string>();
    }

    public WordBuilder MaxNumberOfAttempts(int maxNumberOfAttempts) {
      this.maxNumberOfAttempts = maxNumberOfAttempts;
      return this;
    }

    public WordBuilder Seed(int seed) {
      this.seed = seed;
      return this;
    }

    public WordBuilder Capitalize() {
      this.capitalize = true;
      return this;
    }

    public int ChoiceArrayMemorySize {
      get {
        const int sizeofReference = 4;
        var wordLengthChoiceArraySize = wordLengthChoiceArray.Length * sizeof(int);
        var initialSubwordChoiceArraySize = initialSubwordChoiceArray.Length * sizeofReference;
        var subwordFollowingChoiceArraySize = subwordFollowingChoiceArrays.SelectMany(x => x.Value).Count() * sizeofReference;
        return wordLengthChoiceArraySize + initialSubwordChoiceArraySize + subwordFollowingChoiceArraySize;
      }
    }

    public IEnumerable<string> Build() {
      this.DisallowAnalyzedWords();
      this.BuildChoiceArrays();
      this.BuildRandomNumberGenerator();

      return this.Iterate();
    }

    private IEnumerable<string> Iterate() {
      var failedAttempts = 0;
      while (failedAttempts < this.maxNumberOfAttempts) {
        var nextName = this.NextName();

        if (this.disallowedWords.Contains(nextName)) {
          failedAttempts++;
        } else {
          failedAttempts = 0;
          this.disallowedWords.Add(nextName);
          yield return nextName;
        }
      }
    }

    private void DisallowAnalyzedWords() {
      foreach (var analyzedWord in this.wordAnalyzer.AnalyzedWords) {
        this.disallowedWords.Add(analyzedWord);
      }
    }

    private void BuildChoiceArrays() {
      this.wordLengthChoiceArray = this.wordAnalyzer.WordLengthFrequency.ToChoiceArray();
      this.initialSubwordChoiceArray = this.wordAnalyzer.InitialSubwordFrequency.ToChoiceArray();
      this.subwordFollowingChoiceArrays = this.wordAnalyzer.SubwordFollowingFrequency.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToChoiceArray());
    }

    private void BuildRandomNumberGenerator() {
      this.random = this.seed == null ? new Random() : new Random(this.seed.Value);
    }

    private string NextName() {
      var stopLength = this.wordLengthChoiceArray.GetRandomElement(this.random);

      var stringBuilder = new StringBuilder();
      var terminatingSubword = (string) null;
      while (stringBuilder.Length < stopLength && (terminatingSubword == null || this.subwordFollowingChoiceArrays.ContainsKey(terminatingSubword))) {
        var newSubword = this.GetNewSubword(terminatingSubword);

        terminatingSubword = newSubword;
        stringBuilder.Append(newSubword);
      }

      return stringBuilder.ToString();
    }

    private string GetNewSubword(string terminatingSubword) {
      return terminatingSubword == null ?
        this.initialSubwordChoiceArray.GetRandomElement(this.random) :
        this.subwordFollowingChoiceArrays[terminatingSubword].GetRandomElement(this.random);
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    public IEnumerator<string> GetEnumerator() {
      throw new NotImplementedException();
    }
  }
}
