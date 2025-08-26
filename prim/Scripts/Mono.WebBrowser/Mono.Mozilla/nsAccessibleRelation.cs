using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessibleRelation
{
	public static nsIAccessibleRelation GetProxy(IWebBrowser control, nsIAccessibleRelation obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessibleRelation).GUID, obj) as nsIAccessibleRelation;
	}
}
