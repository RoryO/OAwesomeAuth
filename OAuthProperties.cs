using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using OAwesomeAuth.Extensions;

namespace OAwesomeAuth
{

	//class that is a container for all of the oauth properties
	//Indexer time!
	public class OAuthProperties
	{
		//some stuff is named a bit differently to avoid namespace collisions
		//but mostly because I'm terrible at everything
		
		private enum OAuthProperty
		{
			ConsumerKey,
			ConsumerSecret,
			TokenSecret,
			Version,
			Nonce,
			Timestamp,
			Token,
      AccessSecret
		}
		
		private String[] _dataMembers;
		
		public String ConsumerKey 
		{
			get	{	return (String)_dataMembers[(int)OAuthProperty.ConsumerKey]; }
			set	{	_dataMembers[(int)OAuthProperty.ConsumerKey] = value;	}
		}

		public String ConsumerSecret 
		{
			get {	return (String)_dataMembers[(int)OAuthProperty.ConsumerSecret];	}
			set {	_dataMembers[(int)OAuthProperty.ConsumerSecret] = value; }
		}
		
		public String Version
		{
			get	{	return (String)_dataMembers[(int)OAuthProperty.Version]; }
			set	{ _dataMembers[(int)OAuthProperty.Version] = value;	}
		}
		
		public String TokenSecret 
		{
			get { return _dataMembers[(int)OAuthProperty.TokenSecret]; }
			set { _dataMembers[(int)OAuthProperty.TokenSecret] = value; }
		}
		
		public String Nonce
		{
			get { return _dataMembers[(int)OAuthProperty.Nonce]; }
			set { _dataMembers[(int)OAuthProperty.Nonce] = value; }
		}
		
		public String Timestamp
		{
			get { return _dataMembers[(int)OAuthProperty.Timestamp]; }
			set { _dataMembers[(int)OAuthProperty.Timestamp] = value; }
		}
		
		public String Token 
		{
			get { return _dataMembers[(int)OAuthProperty.Token]; }
			set { _dataMembers[(int)OAuthProperty.Token] = value; }
		}
		public SignatureMethod SignatureMethod { get; set; }
		
		public String Signature { get; set; }
		
		public OAuthProperties ()
		{
			_dataMembers = new String[(int)Enum.GetValues (typeof(OAuthProperty)).Length];
			this.SignatureMethod = SignatureMethod.HmacSha1;
      this.Version = "1.0";
		}
		
		public String ToQueryString ()
    {
      NameValueCollection nvc = this.ToNameValueCollection();
      nvc.Remove("oauth_consumer_secret");
      nvc.Remove("oauth_access_secret");
      nvc.Remove("oauth_token_secret");
      StringBuilder sb = new StringBuilder();
      foreach(String key in nvc.SortKeys())
      {
        sb.Append(String.Format("{0}={1}&", key.EncodeRfc(), nvc[key].EncodeRfc()));
      }
      sb.Remove((sb.Length - 1), 1);
      return sb.ToString();
    }

		public NameValueCollection ToNameValueCollection() 
		{
      NameValueCollection nvc = new NameValueCollection();
      Array properties = Enum.GetValues(typeof(OAuthProperty));
      foreach (int n in properties)
      {
        if(_dataMembers[n] != null)
        {
          String propertyName = String.Format("oauth_{0}", properties.GetValue(n).ToString().ToUnderscore());
           nvc.Add(propertyName, _dataMembers[n]);
        }
      }
      // Signature method is a bit oddly named and doesn't fit into enum naming convetions well,
      // we have to deal with it separately
      nvc.Add("oauth_signature_method", SignatureMethodString(this.SignatureMethod));
      return nvc;
		}
		
		public String ToHeaders ()
		{
			NameValueCollection nvc = this.ToNameValueCollection ();
      //Never send secrets
      nvc.Remove("oauth_access_secret");
      nvc.Remove("oauth_token_secret");

      StringBuilder sb = new StringBuilder ();
      sb.Append ("OAuth ");
      foreach (String key in nvc.SortKeys()) {
        sb.Append (String.Format ("{0}=\"{1}\", ", key, nvc[key]));
      }
      sb.Remove((sb.Length - 2), 2);
      return sb.ToString ();
		}
		
		public String SignatureMethodString (SignatureMethod s)
		{
			String x = String.Empty;
			switch (s) {
			case SignatureMethod.HmacSha1:
				x = "HMAC-SHA1";
				break;
			case SignatureMethod.RsaSha1:
				x = "RSA-SHA1";
				break;
			case SignatureMethod.Plaintext:
				x = "PLAINTEXT";
				break;
			}
			return x;
		}
	}
}
