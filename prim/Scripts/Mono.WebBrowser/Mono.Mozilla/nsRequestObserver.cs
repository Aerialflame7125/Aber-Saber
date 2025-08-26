using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsRequestObserver
{
	public static nsIRequestObserver GetProxy(IWebBrowser control, nsIRequestObserver obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIRequestObserver).GUID, obj) as nsIRequestObserver;
	}
}
