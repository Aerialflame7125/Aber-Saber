using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMMouseEvent
{
	public static nsIDOMMouseEvent GetProxy(IWebBrowser control, nsIDOMMouseEvent obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMMouseEvent).GUID, obj) as nsIDOMMouseEvent;
	}
}
