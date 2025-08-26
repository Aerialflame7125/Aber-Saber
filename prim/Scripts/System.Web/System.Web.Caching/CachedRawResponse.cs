using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace System.Web.Caching;

internal sealed class CachedRawResponse
{
	public sealed class DataItem
	{
		public readonly byte[] Buffer;

		public readonly long Length;

		public readonly HttpResponseSubstitutionCallback Callback;

		public DataItem(byte[] buffer, long length)
		{
			Buffer = buffer;
			Length = length;
		}

		public DataItem(HttpResponseSubstitutionCallback callback)
			: this(null, 0L)
		{
			Callback = callback;
		}
	}

	private HttpCachePolicy policy;

	private CachedVaryBy varyby;

	private int status_code;

	private string status_desc;

	private NameValueCollection headers;

	private List<DataItem> data;

	private IList Data
	{
		get
		{
			if (data == null)
			{
				data = new List<DataItem>();
			}
			return data;
		}
	}

	public HttpCachePolicy Policy
	{
		get
		{
			return policy;
		}
		set
		{
			policy = value;
		}
	}

	public CachedVaryBy VaryBy
	{
		get
		{
			return varyby;
		}
		set
		{
			varyby = value;
		}
	}

	public int StatusCode
	{
		get
		{
			return status_code;
		}
		set
		{
			status_code = value;
		}
	}

	public string StatusDescription
	{
		get
		{
			return status_desc;
		}
		set
		{
			status_desc = value;
		}
	}

	public NameValueCollection Headers => headers;

	public CachedRawResponse(HttpCachePolicy policy)
	{
		this.policy = policy;
	}

	public void SetHeaders(NameValueCollection headers)
	{
		this.headers = headers;
	}

	public void SetData(MemoryStream ms)
	{
		if (ms != null)
		{
			Data.Add(new DataItem(ms.GetBuffer(), ms.Length));
		}
	}

	public void SetData(HttpResponseSubstitutionCallback callback)
	{
		if (callback != null)
		{
			Data.Add(new DataItem(callback));
		}
	}

	public IList GetData()
	{
		if (data == null || data.Count == 0)
		{
			return null;
		}
		return data;
	}
}
