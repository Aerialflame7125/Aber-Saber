using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsErrorService
{
	public static nsIErrorService GetProxy(IWebBrowser control, nsIErrorService obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIErrorService).GUID, obj) as nsIErrorService;
	}
}
