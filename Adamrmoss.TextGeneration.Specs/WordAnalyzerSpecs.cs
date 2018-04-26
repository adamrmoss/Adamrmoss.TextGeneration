using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Adamrmoss.TextGeneration.Specs
{
    [TestFixture]
    public class WordAnalyzerSpecs
    {
        private WordAnalyzer WordAnalyzer;

        [SetUp]
        public void SetUp()
        {
            this.WordAnalyzer = new WordAnalyzer
            {
                MinSubwordLength = 2,
                MaxSubwordLength = 4,
            };
        }

        [Test]
        public void It_can_analyze_banana()
        {
            this.WordAnalyzer.Analyze("banana");

            this.WordAnalyzer.AnalyzedWords.Should().BeEquivalentTo("banana");

            this.WordAnalyzer.WordLengthFrequency.Count.Should().Be(1);
            this.WordAnalyzer.WordLengthFrequency[6].Should().Be(1);

            this.WordAnalyzer.SubwordFollowingFrequency.Count.Should().Be(6);

            var baFollowers = new Dictionary<string, int> { { "na", 1 }, { "nan", 1 }, { "nana", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["ba"].Should().BeEquivalentTo(baFollowers);

            var anFollowers = new Dictionary<string, int> { { "an", 1 }, { "ana", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["an"].Should().BeEquivalentTo(anFollowers);

            var naFollowers = new Dictionary<string, int> { { "na", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["na"].Should().BeEquivalentTo(naFollowers);

            var banFollowers = new Dictionary<string, int> { { "an", 1 }, { "ana", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["ban"].Should().BeEquivalentTo(banFollowers);

            var anaFollowers = new Dictionary<string, int> { { "na", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["ana"].Should().BeEquivalentTo(anaFollowers);

            var banaFollowers = new Dictionary<string, int> { { "na", 1 } };
            this.WordAnalyzer.SubwordFollowingFrequency["bana"].Should().BeEquivalentTo(banaFollowers);
        }

        [Test]
        public void It_can_analyze_the_continents_and_oceans()
        {
            this.WordAnalyzer.Analyze("Africa", "America", "Asia", "Arctic", "Atlantic", "Australia", "Europe", "Indian", "Pacific");
            this.WordAnalyzer.AnalyzedWords.Should().BeEquivalentTo("africa", "america", "asia", "arctic", "atlantic", "australia", "europe", "indian", "pacific");
        }
    }
}
