using System;
using JetBrains.Annotations;

namespace TNW.TextGeneration
{
  public static class StringExtensions
  {
    public static string Capitalize(this string text)
    {
      var capitalizedInitialLetter = text.Substring(0, 1).ToUpper();
      return capitalizedInitialLetter + text.Substring(1);
    }

    [StringFormatMethod("me")]
    public static string FormatWith(this string me, params object[] args)
    {
      if (me == null)
        return null;
      var tmp = String.Format(me, args);
      return tmp;
    }
  }
}
