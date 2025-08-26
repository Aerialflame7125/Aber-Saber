using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsPrefService
{
	public static nsIPrefService GetProxy(IWebBrowser control, nsIPrefService obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIPrefService).GUID, obj) as nsIPrefService;
	}
}
