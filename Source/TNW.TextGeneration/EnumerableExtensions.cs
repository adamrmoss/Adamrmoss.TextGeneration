using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.TextGeneration
{
  public static class EnumerableExtensions
  {
    public static T GetRandomElement<T>(this IEnumerable<T> sequence, Random random)
    {
      var sequenceAsArray = sequence as T[] ?? sequence.ToArray();
      var index = random.Next(sequenceAsArray.Length);

      return sequenceAsArray[index];
    }
  }
}
