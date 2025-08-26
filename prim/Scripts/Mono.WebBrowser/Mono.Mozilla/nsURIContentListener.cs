using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsURIContentListener
{
	public static nsIURIContentListener GetProxy(IWebBrowser control, nsIURIContentListener obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIURIContentListener).GUID, obj) as nsIURIContentListener;
	}
}
