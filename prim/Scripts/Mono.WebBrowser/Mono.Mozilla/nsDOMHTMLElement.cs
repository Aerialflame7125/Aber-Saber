using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMHTMLElement
{
	public static nsIDOMHTMLElement GetProxy(IWebBrowser control, nsIDOMHTMLElement obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMHTMLElement).GUID, obj) as nsIDOMHTMLElement;
	}
}
