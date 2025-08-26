using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsTimerCallback
{
	public static nsITimerCallback GetProxy(IWebBrowser control, nsITimerCallback obj)
	{
		return Base.GetProxyForObject(control, typeof(nsITimerCallback).GUID, obj) as nsITimerCallback;
	}
}
