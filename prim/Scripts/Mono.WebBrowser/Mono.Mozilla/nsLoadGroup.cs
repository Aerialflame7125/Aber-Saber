using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsLoadGroup
{
	public static nsILoadGroup GetProxy(IWebBrowser control, nsILoadGroup obj)
	{
		return Base.GetProxyForObject(control, typeof(nsILoadGroup).GUID, obj) as nsILoadGroup;
	}
}
