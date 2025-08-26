using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDocumentEncoderNodeFixup
{
	public static nsIDocumentEncoderNodeFixup GetProxy(IWebBrowser control, nsIDocumentEncoderNodeFixup obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDocumentEncoderNodeFixup).GUID, obj) as nsIDocumentEncoderNodeFixup;
	}
}
