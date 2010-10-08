using System;
using System.Net;
using Moq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
namespace OAwesomeAuth
{
  [TestFixture()]
  public class AuthConsumer
  {

    [Test]
    public void VerifyConstructor()
    {
      OAuthConsumer c = new OAuthConsumer();
      Assert.That(c.AuthProperties.Version, Is.EqualTo("1.0"));
      Assert.That(c.AuthProperties.SignatureMethod, Is.EqualTo(SignatureMethod.HmacSha1));
      Assert.That(c.AuthRequestMethod, Is.EqualTo(RequestMethod.Header));
    }

    [Test]
    public void GetRequestToken()
    {
      var mock = new Mock<IDispatcher>();
      mock.Setup(foo =>foo.GetText( new WebClient() ) )
        .Returns("oauth_token=\"token\"");
      OAuthConsumer c = new OAuthConsumer(mock.Object);
      c.GetRequestToken();
      Assert.That(c.AuthProperties.Token, Is.EqualTo("token"));
    }
  }
}

