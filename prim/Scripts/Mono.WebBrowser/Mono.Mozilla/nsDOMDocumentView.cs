using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentView
{
	public static nsIDOMDocumentView GetProxy(IWebBrowser control, nsIDOMDocumentView obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentView).GUID, obj) as nsIDOMDocumentView;
	}
}
