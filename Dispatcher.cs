using System;
using System.Net;
namespace OAwesomeAuth
{
  public class Dispatcher : IDispatcher
  {
    WebClient client;

    public Dispatcher()
    {
      client = new WebClient();
    }

    public String Exec (String url)
    {
      return String.Empty;
    }
  }
}

