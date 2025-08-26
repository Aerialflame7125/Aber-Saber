using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSStyleSheet
{
	public static nsIDOMCSSStyleSheet GetProxy(IWebBrowser control, nsIDOMCSSStyleSheet obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSStyleSheet).GUID, obj) as nsIDOMCSSStyleSheet;
	}
}
