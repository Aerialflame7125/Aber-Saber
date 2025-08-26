using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMEventTarget
{
	public static nsIDOMEventTarget GetProxy(IWebBrowser control, nsIDOMEventTarget obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMEventTarget).GUID, obj) as nsIDOMEventTarget;
	}
}
