using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSStyleDeclaration
{
	public static nsIDOMCSSStyleDeclaration GetProxy(IWebBrowser control, nsIDOMCSSStyleDeclaration obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSStyleDeclaration).GUID, obj) as nsIDOMCSSStyleDeclaration;
	}
}
