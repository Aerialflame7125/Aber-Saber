using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowserChrome
{
	public static nsIWebBrowserChrome GetProxy(IWebBrowser control, nsIWebBrowserChrome obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowserChrome).GUID, obj) as nsIWebBrowserChrome;
	}
}
