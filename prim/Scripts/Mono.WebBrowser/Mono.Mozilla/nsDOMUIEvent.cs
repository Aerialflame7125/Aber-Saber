using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMUIEvent
{
	public static nsIDOMUIEvent GetProxy(IWebBrowser control, nsIDOMUIEvent obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMUIEvent).GUID, obj) as nsIDOMUIEvent;
	}
}
