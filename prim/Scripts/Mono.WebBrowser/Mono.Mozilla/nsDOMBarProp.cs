using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMBarProp
{
	public static nsIDOMBarProp GetProxy(IWebBrowser control, nsIDOMBarProp obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMBarProp).GUID, obj) as nsIDOMBarProp;
	}
}
