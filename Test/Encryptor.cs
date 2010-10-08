using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
namespace OAwesomeAuth
{
  [TestFixture]
  public class Encryptor
  {
    [Test]
    public void HmacSha1()
    {
      //using the test data from the OAuth spec
      OAuthProperties authProperties = new OAuthProperties()
      {
        ConsumerKey = "dpf43f3p2l4k3l03",
        ConsumerSecret = "kd94hf93k423kf44",
        Token = "nnch734d00sl2jdk",
        TokenSecret = "pfkkdhi9sl3r4s00",
        Timestamp = "1191242096",
        Nonce = "kllo9940pd9333jh"
      };

      String hashLine = "GET&http%3A%2F%2Fphotos.example.net%2Fphotos&file%3Dvacation.jpg%26oauth_consumer_key%3Ddpf43f3p2l4k3l03%26oauth_nonce%3Dkllo9940pd9333jh%26oauth_signature_method%3DHMAC-SHA1%26oauth_timestamp%3D1191242096%26oauth_token%3Dnnch734d00sl2jdk%26oauth_version%3D1.0%26size%3Doriginal";
      Assert.That(Encryption.SignRequest(hashLine, authProperties),
                  Is.EqualTo("tR3%2BTy81lMeYAr%2FFid0kMTYa%2FWM%3D"));
    }
  }
}

