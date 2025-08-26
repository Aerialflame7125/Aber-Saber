using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsAccessibleDocument
{
	public static nsIAccessibleDocument GetProxy(IWebBrowser control, nsIAccessibleDocument obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIAccessibleDocument).GUID, obj) as nsIAccessibleDocument;
	}
}
