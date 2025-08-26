using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMHTMLBodyElement
{
	public static nsIDOMHTMLBodyElement GetProxy(IWebBrowser control, nsIDOMHTMLBodyElement obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMHTMLBodyElement).GUID, obj) as nsIDOMHTMLBodyElement;
	}
}
