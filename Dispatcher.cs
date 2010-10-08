using System;
using System.IO;
using System.Net;
using System.Drawing;

namespace OAwesomeAuth
{
  public class Dispatcher : IDispatcher
  {
    public String GetText(WebClient c)
    {
      return c.DownloadString(c.BaseAddress);
    }

    public Image GetImage(WebClient c)
    {
      Stream s = c.OpenRead("http://clownpenis.fart");
      Image i = Image.FromStream(s);
      return i;
    }

    public Byte[] GetData(WebClient c)
    {
      Byte[] b = new Byte[1];
      return b;
    }
  }
}

