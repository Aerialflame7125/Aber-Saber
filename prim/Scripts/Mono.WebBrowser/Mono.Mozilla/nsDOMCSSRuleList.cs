using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSRuleList
{
	public static nsIDOMCSSRuleList GetProxy(IWebBrowser control, nsIDOMCSSRuleList obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSRuleList).GUID, obj) as nsIDOMCSSRuleList;
	}
}
