using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMKeyEvent
{
	public static nsIDOMKeyEvent GetProxy(IWebBrowser control, nsIDOMKeyEvent obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMKeyEvent).GUID, obj) as nsIDOMKeyEvent;
	}
}
