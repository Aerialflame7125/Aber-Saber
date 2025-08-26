using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDOMImplementation
{
	public static nsIDOMDOMImplementation GetProxy(IWebBrowser control, nsIDOMDOMImplementation obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDOMImplementation).GUID, obj) as nsIDOMDOMImplementation;
	}
}
