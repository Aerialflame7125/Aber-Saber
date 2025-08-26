using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMNodeList
{
	public static nsIDOMNodeList GetProxy(IWebBrowser control, nsIDOMNodeList obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMNodeList).GUID, obj) as nsIDOMNodeList;
	}
}
