using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace OAwesomeAuth.Extensions
{
  public static class SortedDictionaryExtensions
  {
    public static NameValueCollection ToNameValueCollection (this SortedDictionary<String, String> dict)
    {
      NameValueCollection nvc = new NameValueCollection ();
      foreach (KeyValuePair<String, String> kvp in dict)
      {
        nvc.Add (kvp.Key, kvp.Value);
      }
      return nvc;
    }
    
    public static String ToQueryString (this SortedDictionary<String, String> dict)
    {
      StringBuilder sb = new StringBuilder ();
      foreach (KeyValuePair<String, String> kvp in dict)
      {
        sb.Append (String.Format ("{0}={1}&", kvp.Key, kvp.Value));
      }
      sb.Remove ((sb.Length - 1), 1);
      return sb.ToString ();
    }
    
    public static String ToHeaders (this SortedDictionary<String, String> dict)
    {
      StringBuilder sb = new StringBuilder ();
      sb.Append ("OAuth ");
      foreach (KeyValuePair<String, String> kvp in dict) {
        sb.Append (String.Format ("{0}={1}, ", kvp.Key, kvp.Value));
      }
      sb.Remove((sb.Length - 2), 2);
      return sb.ToString ();
    }
    
    static SortedDictionary<K, V> MergeWith<K, V> (this SortedDictionary<K, V> origin, SortedDictionary<K, V> target)
    {
      SortedDictionary<K, V> final = new SortedDictionary<K, V>();
      foreach (KeyValuePair<K, V> kvp in origin)
      {
        final[kvp.Key] = kvp.Value;
      }
      
      foreach(KeyValuePair<K, V> kvp in target)
      {
        final[kvp.Key] = kvp.Value;
      }
      return final;
    }

  } 
}
