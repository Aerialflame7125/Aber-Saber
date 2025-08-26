using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMEventListener
{
	public static nsIDOMEventListener GetProxy(IWebBrowser control, nsIDOMEventListener obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMEventListener).GUID, obj) as nsIDOMEventListener;
	}
}
