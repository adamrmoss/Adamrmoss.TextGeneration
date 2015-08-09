using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GuardClaws;

namespace TNW.TextGeneration
{
  public class WordBuilder
  {
    private readonly WordAnalyzer wordAnalyzer;
    private readonly Random random;
    private bool capitalize;

    private int[] wordLengthChoiceArray;
    private string[] initialSubwordChoiceArray;
    private Dictionary<string, string[]> subwordFollowingChoiceArrays;

    public WordBuilder(WordAnalyzer wordAnalyzer, int? seed = null)
    {
      Claws.NotNull(() => wordAnalyzer);
      this.wordAnalyzer = wordAnalyzer;

      this.random = seed == null ? new Random() : new Random(seed.Value);
      this.BuildChoiceArrays();
    }

    public WordBuilder Capitalize(bool capitalize = true)
    {
      this.capitalize = capitalize;
      return this;
    }

    public int ChoiceArrayMemorySize
    {
      get {
        const int sizeofReference = 4;
        var wordLengthChoiceArraySize = this.wordLengthChoiceArray.Length * sizeof(int);
        var initialSubwordChoiceArraySize = this.initialSubwordChoiceArray.Length * sizeofReference;
        var subwordFollowingChoiceArraySize = this.subwordFollowingChoiceArrays.SelectMany(x => x.Value).Count() * sizeofReference;
        return wordLengthChoiceArraySize + initialSubwordChoiceArraySize + subwordFollowingChoiceArraySize;
      }
    }

    private void BuildChoiceArrays()
    {
      this.wordLengthChoiceArray = this.wordAnalyzer.WordLengthFrequency.ToChoiceArray();
      this.initialSubwordChoiceArray = this.wordAnalyzer.InitialSubwordFrequency.ToChoiceArray();
      this.subwordFollowingChoiceArrays = this.wordAnalyzer.SubwordFollowingFrequency.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToChoiceArray());
    }

    public string BuildNextWord()
    {
      var stopLength = this.wordLengthChoiceArray.GetRandomElement(this.random) - ((this.wordAnalyzer.MinSubwordLength + this.wordAnalyzer.MaxSubwordLength) / 2) + 1;

      var stringBuilder = new StringBuilder();
      var lastSubword = (string) null;
      var wordIsEndedProperly = false;
      while (!wordIsEndedProperly && (lastSubword == null || this.subwordFollowingChoiceArrays.ContainsKey(lastSubword))) {
        var newSubword = this.GetNewSubword(lastSubword);

        lastSubword = newSubword;
        stringBuilder.Append(newSubword);

        if (stringBuilder.Length > stopLength) {
          if (!this.wordAnalyzer.FinalSubwords.Contains(lastSubword) && this.wordAnalyzer.SubwordFollowingFrequency.ContainsKey(lastSubword)) {
            var validFinals = this.wordAnalyzer.SubwordFollowingFrequency[lastSubword].Select(kvp => kvp.Key).Intersect(this.wordAnalyzer.FinalSubwords).ToArray();
            if (validFinals.Any()) {
              var final = validFinals.GetRandomElement(this.random);
              stringBuilder.Append(final);
            }
          }
          wordIsEndedProperly = true;
        }
      }

      var nextWord = stringBuilder.ToString();
      return this.capitalize ? nextWord.Capitalize() : nextWord;
    }

    private string GetNewSubword(string terminatingSubword)
    {
      return terminatingSubword == null ?
        this.initialSubwordChoiceArray.GetRandomElement(this.random) :
        this.subwordFollowingChoiceArrays[terminatingSubword].GetRandomElement(this.random);
    }
  }
}
