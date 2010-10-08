using System;
using System.Net;
using System.Drawing;
namespace OAwesomeAuth
{
  public interface IDispatcher
  {
    String GetText(WebClient c);
    Image GetImage(WebClient c);
    Byte[] GetData(WebClient c);
  }
}

