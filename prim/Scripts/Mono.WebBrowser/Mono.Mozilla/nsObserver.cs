using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsObserver
{
	public static nsIObserver GetProxy(IWebBrowser control, nsIObserver obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIObserver).GUID, obj) as nsIObserver;
	}
}
