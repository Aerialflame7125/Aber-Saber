using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowserPersist
{
	public static nsIWebBrowserPersist GetProxy(IWebBrowser control, nsIWebBrowserPersist obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowserPersist).GUID, obj) as nsIWebBrowserPersist;
	}
}
