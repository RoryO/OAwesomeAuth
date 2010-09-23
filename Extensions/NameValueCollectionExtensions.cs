using System;
using System.Text;
using System.Collections.Specialized;

namespace OAwesomeAuth.Extensions
{
  public static class NameValueCollectionExtensions
  {
    public static NameValueCollection Sort (this NameValueCollection n)
    {
      String[] a = n.AllKeys;
      Array.Sort(a);
      NameValueCollection retval = new NameValueCollection();
      foreach(String key in a)
      {
        retval.Add(key, n[key]);
      }
      return retval;
    }

    public static String[] SortKeys (this NameValueCollection n)
    {
      String[] a = n.AllKeys;
      Array.Sort(a);
      return a;
    }
    
  }
}

