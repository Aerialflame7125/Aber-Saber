using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMNode
{
	public static nsIDOMNode GetProxy(IWebBrowser control, nsIDOMNode obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMNode).GUID, obj) as nsIDOMNode;
	}
}
