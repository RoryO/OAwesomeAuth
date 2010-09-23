using System;
using System.Text;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OAwesomeAuth.Extensions
{

  public static class StringExtensions
  {
    //Totally from ActiveSupport::String
    public static String ToUnderscore (this String s)
    {
      Regex first = new Regex ("([A-Z])+([A-Z][a-z])");
      Regex second = new Regex ("([a-z\\d])([A-Z])");
      String one = first.Replace (s, "$1_$2");
      String two = second.Replace (one, "$1_$2");
      return two.ToLower();
    }

    public static String ToCamelCase (this String s)
    {
      StringBuilder sb = new StringBuilder();
      foreach(String word in s.Split('_'))
      {
        sb.Append(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word));
      }
      return sb.ToString();
    }
    
    public static String EncodeRfc (this String str)
    {
      str = HttpUtility.UrlEncode (str, System.Text.Encoding.UTF8);
      str = str.Replace ("'", "%27")
        .Replace ("(", "%28")
          .Replace (")", "%29")
          .Replace ("*", "%2A")
          .Replace ("!", "%21")
          .Replace ("~", "%7e")
          .Replace ("+", "%20")
          .Replace (".", "%2E")
          .Replace ("-", "%2D")
          .Replace ("_", "%5F");
      
      StringBuilder sbuilder = new StringBuilder (str);
      for (int i = 0; i < sbuilder.Length; i++) {
        if (sbuilder[i] == '%') { 
          if (Char.IsLetter (sbuilder[i + 1]) || Char.IsLetter (sbuilder[i + 2])) {
            sbuilder[i + 1] = Char.ToUpper (sbuilder[i + 1]);
            sbuilder[i + 2] = Char.ToUpper (sbuilder[i + 2]);
          }
        }
      }
      return sbuilder.ToString ();
    }

    public static String DecodeRfc (this String str)
    {
      return "";
    }
  }
}
