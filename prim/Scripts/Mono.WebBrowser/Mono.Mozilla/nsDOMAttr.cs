using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMAttr
{
	public static nsIDOMAttr GetProxy(IWebBrowser control, nsIDOMAttr obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMAttr).GUID, obj) as nsIDOMAttr;
	}
}
