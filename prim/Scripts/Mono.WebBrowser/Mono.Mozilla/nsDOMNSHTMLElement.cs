using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMNSHTMLElement
{
	public static nsIDOMNSHTMLElement GetProxy(IWebBrowser control, nsIDOMNSHTMLElement obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMNSHTMLElement).GUID, obj) as nsIDOMNSHTMLElement;
	}
}
