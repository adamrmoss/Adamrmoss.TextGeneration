using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Adamrmoss.TextGeneration.Collections
{
    public class Bag<T> : ICollection<T>, IIndexible<T, int>
    {
        public Bag()
        {
            this.counter = new Dictionary<T, int>();
        }

        public Bag(IEnumerable<T> elements)
        {
            this.counter = elements.ToDictionary(e => e, e => 1);
        }

        public Bag(IDictionary<T, int> elements)
        {
            this.counter = new Dictionary<T, int>(elements);
        }

        private readonly Dictionary<T, int> counter;

        public void Add(T item)
        {
            var currentCount = this.counter.FailproofLookup(item);
            this.counter[item] = currentCount + 1;
        }

        public bool Contains(T item)
            => item != null && this.counter.ContainsKey(item);

        public bool Remove(T item)
        {
            if (item != null && this.counter.ContainsKey(item))
            {
                var currentCount = this.counter.FailproofLookup(item);
                if (currentCount >= 1)
                {
                    this.counter[item] = currentCount - 1;
                    return true;
                }
            }

            return false;
        }

        public void Clear()
            => this.counter.Clear();

        public int Count
            => this.counter.Sum(kvp => kvp.Value);

        public bool IsReadOnly => false;

        public int this[T key]
            => this.counter.FailproofLookup(key);

        public void CopyTo(T[] array, int arrayIndex)
            => this.ToChoiceArray().CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator()
            => this.ToChoiceArray().AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private T[] ToChoiceArray()
            => this.counter.SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value)).ToArray();
    }
}
