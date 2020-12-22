using System;
using System.Collections.Generic;
using System.Linq;

namespace Adamrmoss.TextGeneration
{
    public static class Enumerables
    {
        public static T GetRandomElement<T>(this IEnumerable<T> sequence, Random random)
        {
            var sequenceAsArray = sequence as T[] ?? sequence.ToArray();
            var index = random.Next(sequenceAsArray.Length);

            return sequenceAsArray[index];
        }

        public static IEnumerable<int> Infinite()
        {
            for (var i = 0; ; i++)
            {
                yield return i;
            }
        }
    }
}
