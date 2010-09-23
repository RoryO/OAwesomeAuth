using System;
using System.Text;
using System.Linq;
using OAwesomeAuth.Extensions;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OAwesomeAuth
{
  public class Encryption
  {
    public static String SignRequest(String hashLine, String consumerSecret, String tokenSecret, SignatureMethod method)
    {
      String feeder = String.Format ("{0}&{1}", consumerSecret, tokenSecret);
      String final = String.Empty;
      byte[] feederBytes = Encoding.UTF8.GetBytes (feeder);
      switch(method){
      case(SignatureMethod.HmacSha1):
        HMACSHA1 hashEncoder = new HMACSHA1 (feederBytes);
        byte[] lineHashBytes = Encoding.UTF8.GetBytes (hashLine);
        hashEncoder.ComputeHash (lineHashBytes);
        final = Convert.ToBase64String (hashEncoder.Hash);
        break;
      }
      return final.EncodeRfc();
    }

    public static String GenerateNonce()
    {
      List<byte> allowedChars = new List<byte> (62);
      foreach (byte n in Enumerable.Range (48, 10)) {
        allowedChars.Add (n);
      }
      
      foreach (byte n in Enumerable.Range (65, 26)) {
        allowedChars.Add (n);
      }
      
      foreach (byte n in Enumerable.Range (97, 26)) {
        allowedChars.Add (n);
      }
      
      RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider ();
      List<Byte> nonceList = new List<Byte> (32);
      int i = 1;
      do {
        byte[] b = new byte[1];
        rng.GetBytes (b);
        byte bt = b[0];
        if (allowedChars.Contains (bt)) {
          nonceList.Add (bt);
          i++;
        }
      } while (i <= 32);
      
      UTF8Encoding encoder = new UTF8Encoding ();
      return encoder.GetString (nonceList.ToArray ());
    }


  }
}

