using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsHistoryEntry
{
	public static nsIHistoryEntry GetProxy(IWebBrowser control, nsIHistoryEntry obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIHistoryEntry).GUID, obj) as nsIHistoryEntry;
	}
}
