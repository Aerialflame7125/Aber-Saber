using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMRect
{
	public static nsIDOMRect GetProxy(IWebBrowser control, nsIDOMRect obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMRect).GUID, obj) as nsIDOMRect;
	}
}
