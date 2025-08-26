using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsServiceManager
{
	public static nsIServiceManager GetProxy(IWebBrowser control, nsIServiceManager obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIServiceManager).GUID, obj) as nsIServiceManager;
	}
}
