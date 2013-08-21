using NUnit.Framework;

namespace TNW.TextGeneration.Specs
{
  [TestFixture]
  public class WordAnalyzerSpecs : AssertionHelper
  {
    private WordAnalyzer WordAnalyzer;

    [SetUp]
    public void SetUp()
    {
      WordAnalyzer = new WordAnalyzer {
        MinSubwordLength = 2,
        MaxSubwordLength = 4,
      };
    }

    [Test]
    public void It_can_analyze_banana()
    {
      WordAnalyzer.Analyze("banana");

      Expect(WordAnalyzer.AnalyzedWords, EquivalentTo(new[] { "banana" }));

      Expect(WordAnalyzer.WordLengthFrequency.Count, EqualTo(1));
      Expect(WordAnalyzer.WordLengthFrequency[6], EqualTo(1));

      Expect(WordAnalyzer.SubwordFrequency.Count, EqualTo(9));
      Expect(WordAnalyzer.SubwordFrequency["ba"], EqualTo(1));
      Expect(WordAnalyzer.SubwordFrequency["an"], EqualTo(2));
      Expect(WordAnalyzer.SubwordFrequency["na"], EqualTo(2));
      Expect(WordAnalyzer.SubwordFrequency["ban"], EqualTo(1));
      Expect(WordAnalyzer.SubwordFrequency["ana"], EqualTo(2));
      Expect(WordAnalyzer.SubwordFrequency["nan"], EqualTo(1));
      Expect(WordAnalyzer.SubwordFrequency["bana"], EqualTo(1));
      Expect(WordAnalyzer.SubwordFrequency["anan"], EqualTo(1));
      Expect(WordAnalyzer.SubwordFrequency["nana"], EqualTo(1));

      Expect(WordAnalyzer.CharacterFollowingFrequency.Count, EqualTo(3));

      var followingBFrequency = WordAnalyzer.CharacterFollowingFrequency['b'];
      Expect(followingBFrequency.Count, EqualTo(1));
      Expect(followingBFrequency['a'], EqualTo(1));

      var followingAFrequency = WordAnalyzer.CharacterFollowingFrequency['a'];
      Expect(followingAFrequency.Count, EqualTo(1));
      Expect(followingAFrequency['n'], EqualTo(2));

      var followingNFrequency = WordAnalyzer.CharacterFollowingFrequency['n'];
      Expect(followingNFrequency.Count, EqualTo(1));
      Expect(followingNFrequency['a'], EqualTo(2));
    }

    [Test]
    public void It_can_analyze_the_continents_and_oceans()
    {
      WordAnalyzer.Analyze("Africa", "America", "Asia", "Arctic", "Atlantic", "Australia", "Europe", "Indian", "Pacific");
      Expect(WordAnalyzer.AnalyzedWords, EquivalentTo(new[] { "africa", "america", "asia", "arctic", "atlantic", "australia", "europe", "indian", "pacific" }));
    }
  }
}
