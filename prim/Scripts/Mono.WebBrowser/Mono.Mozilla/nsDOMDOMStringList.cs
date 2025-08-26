using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDOMStringList
{
	public static nsIDOMDOMStringList GetProxy(IWebBrowser control, nsIDOMDOMStringList obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDOMStringList).GUID, obj) as nsIDOMDOMStringList;
	}
}
