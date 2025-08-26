using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMMediaList
{
	public static nsIDOMMediaList GetProxy(IWebBrowser control, nsIDOMMediaList obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMMediaList).GUID, obj) as nsIDOMMediaList;
	}
}
