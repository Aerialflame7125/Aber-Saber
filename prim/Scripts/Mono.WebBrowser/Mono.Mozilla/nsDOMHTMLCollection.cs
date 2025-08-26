using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMHTMLCollection
{
	public static nsIDOMHTMLCollection GetProxy(IWebBrowser control, nsIDOMHTMLCollection obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMHTMLCollection).GUID, obj) as nsIDOMHTMLCollection;
	}
}
