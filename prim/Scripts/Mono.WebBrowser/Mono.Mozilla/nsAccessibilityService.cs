using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessibilityService
{
	public static nsIAccessibilityService GetProxy(IWebBrowser control, nsIAccessibilityService obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessibilityService).GUID, obj) as nsIAccessibilityService;
	}
}
