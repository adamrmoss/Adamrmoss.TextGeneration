using System.Collections.Generic;
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

      Expect(WordAnalyzer.SubwordFollowingFrequency.Count, EqualTo(6));

      var baFollowers = new Dictionary<string, int> { { "na", 1 }, { "nan", 1 }, { "nana", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["ba"], EquivalentTo(baFollowers));

      var anFollowers = new Dictionary<string, int> { { "an", 1 }, { "ana", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["an"], EquivalentTo(anFollowers));

      var naFollowers = new Dictionary<string, int> { { "na", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["na"], EquivalentTo(naFollowers));

      var banFollowers = new Dictionary<string, int> { {"an", 1}, { "ana", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["ban"], EquivalentTo(banFollowers));

      var anaFollowers = new Dictionary<string, int> { { "na", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["ana"], EquivalentTo(anaFollowers));

      var banaFollowers = new Dictionary<string, int> { { "na", 1 } };
      Expect(WordAnalyzer.SubwordFollowingFrequency["bana"], EquivalentTo(banaFollowers));
    }

    [Test]
    public void It_can_analyze_the_continents_and_oceans()
    {
      WordAnalyzer.Analyze("Africa", "America", "Asia", "Arctic", "Atlantic", "Australia", "Europe", "Indian", "Pacific");
      Expect(WordAnalyzer.AnalyzedWords, EquivalentTo(new[] { "africa", "america", "asia", "arctic", "atlantic", "australia", "europe", "indian", "pacific" }));
    }
  }
}
