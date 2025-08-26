using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsInterfaceRequestor
{
	public static nsIInterfaceRequestor GetProxy(IWebBrowser control, nsIInterfaceRequestor obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIInterfaceRequestor).GUID, obj) as nsIInterfaceRequestor;
	}
}
