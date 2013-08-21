using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.TextGeneration
{
  public static class StringExtensions
  {
    public static string Capitalize(this string text)
    {
      var capitalizedInitialLetter = text.Substring(0, 1).ToUpper();
      return capitalizedInitialLetter + text.Substring(1);
    }
  }
}
