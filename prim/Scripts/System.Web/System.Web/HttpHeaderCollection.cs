using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web;

internal sealed class HttpHeaderCollection : NameValueCollection
{
	private bool? headerCheckingEnabled;

	private bool HeaderCheckingEnabled
	{
		get
		{
			if (!headerCheckingEnabled.HasValue)
			{
				headerCheckingEnabled = HttpRuntime.Section.EnableHeaderChecking;
			}
			return headerCheckingEnabled.Value;
		}
	}

	public HttpHeaderCollection()
		: base(StringComparer.OrdinalIgnoreCase)
	{
	}

	public override void Add(string name, string value)
	{
		EncodeAndSetHeader(name, value, replaceExisting: false);
	}

	public override void Set(string name, string value)
	{
		EncodeAndSetHeader(name, value, replaceExisting: true);
	}

	private void EncodeAndSetHeader(string name, string value, bool replaceExisting)
	{
		if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
		{
			string encodedHeaderName;
			string encodedHeaderValue;
			if (HeaderCheckingEnabled)
			{
				HttpEncoder.Current.HeaderNameValueEncode(name, value, out encodedHeaderName, out encodedHeaderValue);
			}
			else
			{
				encodedHeaderName = name;
				encodedHeaderValue = value;
			}
			if (replaceExisting)
			{
				base.Set(encodedHeaderName, encodedHeaderValue);
			}
			else
			{
				base.Add(encodedHeaderName, encodedHeaderValue);
			}
		}
	}
}
