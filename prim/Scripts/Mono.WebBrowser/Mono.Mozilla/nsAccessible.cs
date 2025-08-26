using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessible
{
	public static nsIAccessible GetProxy(IWebBrowser control, nsIAccessible obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessible).GUID, obj) as nsIAccessible;
	}
}
