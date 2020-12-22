using System;
using System.Collections.Generic;
using Adamrmoss.TextGeneration.Collections;

namespace Adamrmoss.TextGeneration
{
    public interface IProbabilityDistrubution<TKey> : IIndexible<TKey, double>
    {
        TKey Choose(Random random);
    }

    public class ProbabilityDistrubution<TKey> : IProbabilityDistrubution<TKey>
    {
        private readonly Bag<TKey> Dataset;

        public ProbabilityDistrubution(Bag<TKey> dataset)
        {
            this.Dataset = dataset;
        }

        public double this[TKey key] 
            => throw new NotImplementedException();

        public TKey Choose(Random random)
        {
            throw new NotImplementedException();
        }
    }
}
