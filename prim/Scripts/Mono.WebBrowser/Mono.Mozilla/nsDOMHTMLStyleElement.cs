using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMHTMLStyleElement
{
	public static nsIDOMHTMLStyleElement GetProxy(IWebBrowser control, nsIDOMHTMLStyleElement obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMHTMLStyleElement).GUID, obj) as nsIDOMHTMLStyleElement;
	}
}
