using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebProgress
{
	public static nsIWebProgress GetProxy(IWebBrowser control, nsIWebProgress obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebProgress).GUID, obj) as nsIWebProgress;
	}
}
