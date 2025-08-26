using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDocumentEncoder
{
	public static nsIDocumentEncoder GetProxy(IWebBrowser control, nsIDocumentEncoder obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDocumentEncoder).GUID, obj) as nsIDocumentEncoder;
	}
}
