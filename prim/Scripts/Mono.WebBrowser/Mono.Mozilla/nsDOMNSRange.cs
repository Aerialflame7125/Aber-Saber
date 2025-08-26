using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMNSRange
{
	public static nsIDOMNSRange GetProxy(IWebBrowser control, nsIDOMNSRange obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMNSRange).GUID, obj) as nsIDOMNSRange;
	}
}
