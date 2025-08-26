using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMWindow
{
	public static nsIDOMWindow GetProxy(IWebBrowser control, nsIDOMWindow obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMWindow).GUID, obj) as nsIDOMWindow;
	}
}
