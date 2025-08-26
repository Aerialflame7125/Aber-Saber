using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMEntityReference
{
	public static nsIDOMEntityReference GetProxy(IWebBrowser control, nsIDOMEntityReference obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMEntityReference).GUID, obj) as nsIDOMEntityReference;
	}
}
