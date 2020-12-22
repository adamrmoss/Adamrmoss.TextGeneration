using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamrmoss.TextGeneration
{
    public static class DictionaryExtensions
    {
        public static TValue FailproofLookup<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            //return dictionary.ContainsKey(key) ? dictionary[key] : new TValue();
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                var value = new TValue();
                dictionary[key] = value;
                return value;
            }
        }

        public static void Tally<TKey>(this IDictionary<TKey, int> dictionary, TKey key)
        {
            var currentCount = dictionary.FailproofLookup(key);
            dictionary[key] = currentCount + 1;
        }

        public static TKey[] ToChoiceArray<TKey>(this IDictionary<TKey, int> dictionary)
            => dictionary.SelectMany(kvp => Enumerable.Repeat(kvp.Key, kvp.Value)).ToArray();
    }
}
