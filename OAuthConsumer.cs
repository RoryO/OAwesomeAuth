using System;
using System.Net;
using System.Text;
using OAwesomeAuth.Extensions;
using System.Collections.Specialized;

namespace OAwesomeAuth
{
  public class OAuthConsumer
  {
    public OAuthProperties AuthProperties { get; set; }
    public RequestMethod AuthRequestMethod { get; set; }
    public HttpAction HttpMethod;

    public String RequestTokenUrl { get; set; }
    public String AccessTokenUrl { get; set; }
    public String ResourceUrl { get; set; }

    private String _hashLine;
    public String HashLine { get { return _hashLine; } }

    private IDispatcher _dispatcher;

    public OAuthConsumer ()
    {
      AuthProperties = new OAuthProperties();
      AuthRequestMethod = RequestMethod.Header;
      HttpMethod = HttpAction.GET;
      _dispatcher = new Dispatcher();
    }

    public OAuthConsumer(IDispatcher d) : this()
    {
      _dispatcher = d;
    }

    public void GetRequestToken()
    {
      GenerateHashLine(RequestTokenUrl, AuthProperties.ToNameValueCollection(), null, null);
      WebClient c = BuildRequest(RequestTokenUrl);
      String r = _dispatcher.GetText(c);
    }

    private WebClient BuildRequest(String url, NameValueCollection post)
    {
      WebClient c = new WebClient();
      Uri uri = new Uri(url);
      StampTime();
      AuthProperties.Nonce = Encryption.GenerateNonce();
      GenerateHashLine(url, AuthProperties.ToNameValueCollection(), null , post);
      AuthProperties.Signature = Encryption.SignRequest(_hashLine, AuthProperties);
      switch (AuthRequestMethod) {
      case(RequestMethod.Header):
        c.Headers["Authorization"] = String.Format("OAuth {0}, {1}={2}", AuthProperties.ToHeaders(), "oauth_signature", "fart");
        break;
      case(RequestMethod.Query):
        break;
      case(RequestMethod.Form):
        break;
      }

      return c;
    }

    private WebClient BuildRequest(String url)
    {
      return BuildRequest(url, null);
    }

    private void StampTime()
    {
      AuthProperties.Timestamp = ((int)(DateTime.UtcNow - new DateTime (1970, 1, 1)).TotalSeconds).ToString ();
    }

    private void GenerateHashLine(String url, NameValueCollection authParams, NameValueCollection queryParams, NameValueCollection postParams)
    {
      StringBuilder sb = new StringBuilder ();
      sb.Append (HttpMethod);
      sb.Append ("&");
      sb.Append (url.EncodeRfc());
      sb.Append ("&");
      
      foreach(String key in authParams.SortKeys())
      {
        sb.Append(key.EncodeRfc());
        sb.Append("%3D");
        sb.Append(authParams[key].EncodeRfc());
        sb.Append("%26");
      }
      
      if( queryParams != null )
      {
        //process
      }
      
      if( postParams != null )
      {
        //process
      }
      
      sb.Remove ((sb.Length - 3), 3);
      _hashLine = sb.ToString();
    }
  }
}