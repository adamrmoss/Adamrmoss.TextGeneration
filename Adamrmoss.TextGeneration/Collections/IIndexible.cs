using System;

namespace Adamrmoss.TextGeneration
{
    public interface IIndexible<in TKey, out TValue>
    {
        TValue this[TKey key] { get; }
    }
}
