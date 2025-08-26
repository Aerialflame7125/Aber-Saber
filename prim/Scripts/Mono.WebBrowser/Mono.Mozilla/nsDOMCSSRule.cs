using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSRule
{
	public static nsIDOMCSSRule GetProxy(IWebBrowser control, nsIDOMCSSRule obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSRule).GUID, obj) as nsIDOMCSSRule;
	}
}
