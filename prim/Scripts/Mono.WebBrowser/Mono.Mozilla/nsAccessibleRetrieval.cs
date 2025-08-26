using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessibleRetrieval
{
	public static nsIAccessibleRetrieval GetProxy(IWebBrowser control, nsIAccessibleRetrieval obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessibleRetrieval).GUID, obj) as nsIAccessibleRetrieval;
	}
}
