using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsStreamListener
{
	public static nsIStreamListener GetProxy(IWebBrowser control, nsIStreamListener obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIStreamListener).GUID, obj) as nsIStreamListener;
	}
}
