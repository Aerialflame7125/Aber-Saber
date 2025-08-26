using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowserChromeFocus
{
	public static nsIWebBrowserChromeFocus GetProxy(IWebBrowser control, nsIWebBrowserChromeFocus obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowserChromeFocus).GUID, obj) as nsIWebBrowserChromeFocus;
	}
}
