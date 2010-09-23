using System;
using OAwesomeAuth.Extensions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
namespace OAwesomeAuth
{
  [TestFixture()]
  public class TestStringExtensions
  {
    [Test()]
    public void SeparationWithUnderscore()
    {
      String s = "TestThisByUnderscore";
      Assert.That(s.ToUnderscore(), Is.EqualTo("test_this_by_underscore"));
    }

    [Test]
    public void JoiningWithCamelCase()
    {
      String s = "test_this_by_underscore";
      Assert.That(s.ToCamelCase(), Is.EqualTo("TestThisByUnderscore"));
    }

    [Test]
    public void EncodingWithRfc3986()
    {
      String s = "<>~.\"{}|\\-`_^% ";
      Assert.That(s.EncodeRfc(), Is.EqualTo("%3C%3E%7E%2E%22%7B%7D%7C%5C%2D%60%5F%5E%25%20"));
    }
  }
}

