using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowser
{
	public static nsIWebBrowser GetProxy(IWebBrowser control, nsIWebBrowser obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowser).GUID, obj) as nsIWebBrowser;
	}
}
