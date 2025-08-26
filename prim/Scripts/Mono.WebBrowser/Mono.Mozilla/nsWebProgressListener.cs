using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebProgressListener
{
	public static nsIWebProgressListener GetProxy(IWebBrowser control, nsIWebProgressListener obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebProgressListener).GUID, obj) as nsIWebProgressListener;
	}
}
