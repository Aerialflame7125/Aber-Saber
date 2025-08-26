using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSValue
{
	public static nsIDOMCSSValue GetProxy(IWebBrowser control, nsIDOMCSSValue obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSValue).GUID, obj) as nsIDOMCSSValue;
	}
}
