using System;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
namespace OAwesomeAuth
{
  [TestFixture]
  public class TestAuthProperties
  {
    [Test]
    public void DefaultAuthProperties()
    {
      OAuthProperties p =  new OAuthProperties();
      Assert.That(p.Version, Is.EqualTo("1.0"));
    }

    [Test]
    public void HttpAuthorizationHeader()
    {
      OAuthProperties p = new OAuthProperties();
      p.ConsumerKey = "key";
      p.Token = "token";
      String expected = "OAuth oauth_consumer_key=\"key\", oauth_signature_method=\"HMAC-SHA1\", oauth_token=\"token\", oauth_version=\"1.0\"";
      Assert.That(p.ToHeaders(), Is.EqualTo(expected));
    }

    [Test]
    public void SignatureMethodString ()
    {
      OAuthProperties p = new OAuthProperties ();
      Assert.That (p.SignatureMethodString (SignatureMethod.HmacSha1), Is.EqualTo ("HMAC-SHA1"));
      Assert.That (p.SignatureMethodString (SignatureMethod.Plaintext), Is.EqualTo ("PLAINTEXT"));
      Assert.That (p.SignatureMethodString (SignatureMethod.RsaSha1), Is.EqualTo ("RSA-SHA1"));
    }
  }
}

