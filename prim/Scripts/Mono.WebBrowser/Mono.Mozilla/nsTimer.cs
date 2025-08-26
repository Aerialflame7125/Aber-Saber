using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsTimer
{
	public static nsITimer GetProxy(IWebBrowser control, nsITimer obj)
	{
		return Base.GetProxyForObject(control, typeof(nsITimer).GUID, obj) as nsITimer;
	}
}
