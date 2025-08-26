using System.Web.Util;

namespace System.Web;

internal sealed class UnknownResponseHeader : BaseResponseHeader
{
	private string headerName;

	public string Name
	{
		get
		{
			return headerName;
		}
		set
		{
			HttpEncoder.Current.HeaderNameValueEncode(value, null, out var encodedHeaderName, out var _);
			headerName = encodedHeaderName;
		}
	}

	public UnknownResponseHeader(string name, string val)
		: base(val)
	{
		Name = name;
	}

	internal override void SendContent(HttpWorkerRequest wr)
	{
		wr.SendUnknownResponseHeader(Name, base.Value);
	}
}
