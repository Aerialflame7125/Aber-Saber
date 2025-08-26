using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessNode
{
	public static nsIAccessNode GetProxy(IWebBrowser control, nsIAccessNode obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessNode).GUID, obj) as nsIAccessNode;
	}
}
