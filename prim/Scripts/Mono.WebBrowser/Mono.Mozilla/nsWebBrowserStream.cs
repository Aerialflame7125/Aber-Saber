using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebBrowserStream
{
	public static nsIWebBrowserStream GetProxy(IWebBrowser control, nsIWebBrowserStream obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebBrowserStream).GUID, obj) as nsIWebBrowserStream;
	}
}
