using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowserFocus
{
	public static nsIWebBrowserFocus GetProxy(IWebBrowser control, nsIWebBrowserFocus obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowserFocus).GUID, obj) as nsIWebBrowserFocus;
	}
}
