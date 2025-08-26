using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsProperties
{
	public static nsIProperties GetProxy(IWebBrowser control, nsIProperties obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIProperties).GUID, obj) as nsIProperties;
	}
}
