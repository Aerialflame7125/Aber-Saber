using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsIOService
{
	public static nsIIOService GetProxy(IWebBrowser control, nsIIOService obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIIOService).GUID, obj) as nsIIOService;
	}
}
