using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsWebNavigation
{
	public static nsIWebNavigation GetProxy(IWebBrowser control, nsIWebNavigation obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIWebNavigation).GUID, obj) as nsIWebNavigation;
	}
}
