using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMStyleSheetList
{
	public static nsIDOMStyleSheetList GetProxy(IWebBrowser control, nsIDOMStyleSheetList obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMStyleSheetList).GUID, obj) as nsIDOMStyleSheetList;
	}
}
