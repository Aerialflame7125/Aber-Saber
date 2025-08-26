using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsSelection
{
	public static nsISelection GetProxy(IWebBrowser control, nsISelection obj)
	{
		return Base.GetProxyForObject(control, typeof(nsISelection).GUID, obj) as nsISelection;
	}
}
