using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsSHistoryListener
{
	public static nsISHistoryListener GetProxy(IWebBrowser control, nsISHistoryListener obj)
	{
		return Base.GetProxyForObject(control, typeof(nsISHistoryListener).GUID, obj) as nsISHistoryListener;
	}
}
