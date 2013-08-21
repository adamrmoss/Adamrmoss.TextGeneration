using System.Collections.Generic;

namespace TNW.TextGeneration
{
  public class WordAnalyzer
  {
    public int MinSubwordLength { get; set; }
    public int MaxSubwordLength { get; set; }

    public ISet<string> AnalyzedWords { get; private set; }

    public Dictionary<int, int> WordLengthFrequency { get; private set; }
    public Dictionary<string, int> InitialSubwordFrequency { get; private set; }
    public Dictionary<string, int> SubwordFrequency { get; private set; }
    public Dictionary<char, Dictionary<char, int>> CharacterFollowingFrequency { get; private set; }

    public WordAnalyzer() {
      this.AnalyzedWords = new HashSet<string>();
      this.WordLengthFrequency = new Dictionary<int, int>();
      this.InitialSubwordFrequency = new Dictionary<string, int>();
      this.SubwordFrequency = new Dictionary<string, int>();
      this.CharacterFollowingFrequency = new Dictionary<char, Dictionary<char, int>>();
    }

    public void Analyze(params string[] words) {
      this.Analyze((IEnumerable<string>) words);
    }

    public void Analyze(IEnumerable<string> words) {
      foreach (var word in words) {
        this.Analyze(word);
      }
    }

    public void Analyze(string word) {
      word = word.ToLower();

      this.WordLengthFrequency.Tally(word.Length);
      this.NoteSubwordFrequency(word);
      this.NoteCharacterFollowingFrequency(word);

      this.AnalyzedWords.Add(word);
    }

    private void NoteSubwordFrequency(string word) {
      for (var subwordLength = this.MinSubwordLength; subwordLength <= this.MaxSubwordLength; subwordLength++) {
        for (var subwordStartIndex = 0; subwordStartIndex <= word.Length - subwordLength; subwordStartIndex++) {
          var subword = word.Substring(subwordStartIndex, subwordLength);
          this.SubwordFrequency.Tally(subword);
          if (subwordStartIndex == 0) {
            this.InitialSubwordFrequency.Tally(subword);
          }
        }
      }
    }

    private void NoteCharacterFollowingFrequency(string word) {
      for (var firstLetterIndex = 0; firstLetterIndex < word.Length - 1; firstLetterIndex++) {
        var frequenciesFollowingThisCharacter = this.CharacterFollowingFrequency.FailproofLookup(word[firstLetterIndex]);
        frequenciesFollowingThisCharacter.Tally(word[firstLetterIndex + 1]);
      }
    }
  }
}
