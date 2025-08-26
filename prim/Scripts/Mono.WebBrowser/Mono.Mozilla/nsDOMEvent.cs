using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMEvent
{
	public static nsIDOMEvent GetProxy(IWebBrowser control, nsIDOMEvent obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMEvent).GUID, obj) as nsIDOMEvent;
	}
}
