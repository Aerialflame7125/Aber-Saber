using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCSSPrimitiveValue
{
	public static nsIDOMCSSPrimitiveValue GetProxy(IWebBrowser control, nsIDOMCSSPrimitiveValue obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCSSPrimitiveValue).GUID, obj) as nsIDOMCSSPrimitiveValue;
	}
}
