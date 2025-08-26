using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentEvent
{
	public static nsIDOMDocumentEvent GetProxy(IWebBrowser control, nsIDOMDocumentEvent obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentEvent).GUID, obj) as nsIDOMDocumentEvent;
	}
}
