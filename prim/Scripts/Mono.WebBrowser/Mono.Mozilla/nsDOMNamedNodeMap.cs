using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMNamedNodeMap
{
	public static nsIDOMNamedNodeMap GetProxy(IWebBrowser control, nsIDOMNamedNodeMap obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMNamedNodeMap).GUID, obj) as nsIDOMNamedNodeMap;
	}
}
