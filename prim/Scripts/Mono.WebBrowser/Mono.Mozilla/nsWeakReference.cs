using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWeakReference
{
	public static nsIWeakReference GetProxy(IWebBrowser control, nsIWeakReference obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWeakReference).GUID, obj) as nsIWeakReference;
	}
}
