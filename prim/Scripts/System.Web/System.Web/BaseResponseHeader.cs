using System.Web.Util;

namespace System.Web;

internal abstract class BaseResponseHeader
{
	private string headerValue;

	public string Value
	{
		get
		{
			return headerValue;
		}
		set
		{
			HttpEncoder.Current.HeaderNameValueEncode(null, value, out var _, out var encodedHeaderValue);
			headerValue = encodedHeaderValue;
		}
	}

	internal BaseResponseHeader(string val)
	{
		Value = val;
	}

	internal abstract void SendContent(HttpWorkerRequest wr);
}
