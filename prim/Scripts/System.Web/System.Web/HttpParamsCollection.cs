using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web;

internal class HttpParamsCollection : WebROCollection
{
	private NameValueCollection _queryString;

	private NameValueCollection _form;

	private NameValueCollection _serverVariables;

	private HttpCookieCollection _cookies;

	private bool _merged;

	public override string[] AllKeys
	{
		get
		{
			MergeCollections();
			return base.AllKeys;
		}
	}

	public override int Count
	{
		get
		{
			MergeCollections();
			return base.Count;
		}
	}

	public HttpParamsCollection(NameValueCollection queryString, NameValueCollection form, NameValueCollection serverVariables, HttpCookieCollection cookies)
	{
		_queryString = queryString;
		_form = form;
		_serverVariables = serverVariables;
		_cookies = cookies;
		_merged = false;
		Protect();
	}

	public override string Get(string name)
	{
		MergeCollections();
		return base.Get(name);
	}

	private void MergeCollections()
	{
		if (!_merged)
		{
			Unprotect();
			Add(_queryString);
			Add(_form);
			Add(_serverVariables);
			for (int i = 0; i < _cookies.Count; i++)
			{
				HttpCookie httpCookie = _cookies[i];
				Add(httpCookie.Name, httpCookie.Value);
			}
			_merged = true;
			Protect();
		}
	}

	public override string Get(int index)
	{
		MergeCollections();
		return base.Get(index);
	}

	public override string GetKey(int index)
	{
		MergeCollections();
		return base.GetKey(index);
	}

	public override string[] GetValues(int index)
	{
		MergeCollections();
		return base.GetValues(index);
	}

	public override string[] GetValues(string name)
	{
		MergeCollections();
		return base.GetValues(name);
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		throw new SerializationException();
	}
}
