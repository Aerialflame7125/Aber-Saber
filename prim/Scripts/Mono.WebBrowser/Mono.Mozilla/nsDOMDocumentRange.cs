using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentRange
{
	public static nsIDOMDocumentRange GetProxy(IWebBrowser control, nsIDOMDocumentRange obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentRange).GUID, obj) as nsIDOMDocumentRange;
	}
}
