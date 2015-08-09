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
    public Dictionary<string, Dictionary<string, int>> SubwordFollowingFrequency { get; private set; }
    public HashSet<string> FinalSubwords { get; private set; }

    public WordAnalyzer()
    {
      this.AnalyzedWords = new HashSet<string>();
      this.WordLengthFrequency = new Dictionary<int, int>();
      this.InitialSubwordFrequency = new Dictionary<string, int>();
      this.SubwordFollowingFrequency = new Dictionary<string, Dictionary<string, int>>();
      this.FinalSubwords = new HashSet<string>();
    }

    public void Analyze(params string[] words)
    {
      this.Analyze((IEnumerable<string>) words);
    }

    public void Analyze(IEnumerable<string> words)
    {
      foreach (var word in words) {
        this.Analyze(word);
      }
    }

    public void Analyze(string word)
    {
      word = word.ToLower();

      this.WordLengthFrequency.Tally(word.Length);
      this.Scan(word);
      this.AnalyzedWords.Add(word);
    }

    private void Scan(string word)
    {
      for (var firstSubwordLength = this.MinSubwordLength; firstSubwordLength <= this.MaxSubwordLength; firstSubwordLength++) {
        for (var secondSubwordLength = this.MinSubwordLength; secondSubwordLength <= this.MaxSubwordLength; secondSubwordLength++) {
          for (var startIndex = 0; startIndex <= word.Length - firstSubwordLength - secondSubwordLength; startIndex++) {
            var firstSubword = word.Substring(startIndex, firstSubwordLength);
            if (startIndex == 0) {
              this.InitialSubwordFrequency.Tally(firstSubword);
            }

            var secondSubword = word.Substring(startIndex + firstSubwordLength, secondSubwordLength);
            this.NoteSubwordFollowingFrequency(firstSubword, secondSubword);
            if (startIndex == word.Length - firstSubwordLength - secondSubwordLength) {
              this.FinalSubwords.Add(secondSubword);
            }
          }
        }
      }
    }

    private void NoteSubwordFollowingFrequency(string firstSubword, string secondSubword)
    {
      var currentFollowingFrequency = this.SubwordFollowingFrequency.FailproofLookup(firstSubword);
      currentFollowingFrequency.Tally(secondSubword);
    }
  }
}
