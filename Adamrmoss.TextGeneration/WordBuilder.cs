using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adamrmoss.TextGeneration
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
            this.wordAnalyzer = wordAnalyzer
                ?? throw new ArgumentException("Word Analyzer must not be null", nameof(wordAnalyzer));

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
            get
            {
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
            var stopLength = this.wordLengthChoiceArray.GetRandomElement(this.random) + 2;

            var possibleFirstSubwords = this.initialSubwordChoiceArray.Intersect(this.subwordFollowingChoiceArrays.Keys);
            var newSubword = possibleFirstSubwords.GetRandomElement(this.random);
            var stringBuilder = new StringBuilder(newSubword);
            var longestValidWord = this.wordAnalyzer.FinalSubwords.Contains(newSubword) ? newSubword : null;

            while (stringBuilder.Length < stopLength && this.subwordFollowingChoiceArrays.ContainsKey(newSubword))
            {
                newSubword = this.subwordFollowingChoiceArrays[newSubword].GetRandomElement(this.random);
                stringBuilder.Append(newSubword);

                if (this.wordAnalyzer.FinalSubwords.Contains(newSubword))
                {
                    longestValidWord = stringBuilder.ToString();
                }
            }

            var nextWord = longestValidWord ?? stringBuilder.ToString();
            return this.capitalize ? nextWord.Capitalize() : nextWord;
        }
    }
}
