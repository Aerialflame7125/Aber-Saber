using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMElement
{
	public static nsIDOMElement GetProxy(IWebBrowser control, nsIDOMElement obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMElement).GUID, obj) as nsIDOMElement;
	}
}
