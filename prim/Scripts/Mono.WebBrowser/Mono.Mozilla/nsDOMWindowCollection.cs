using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMWindowCollection
{
	public static nsIDOMWindowCollection GetProxy(IWebBrowser control, nsIDOMWindowCollection obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMWindowCollection).GUID, obj) as nsIDOMWindowCollection;
	}
}
