using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMStyleSheet
{
	public static nsIDOMStyleSheet GetProxy(IWebBrowser control, nsIDOMStyleSheet obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMStyleSheet).GUID, obj) as nsIDOMStyleSheet;
	}
}
