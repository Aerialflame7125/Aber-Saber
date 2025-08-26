using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMRange
{
	public static nsIDOMRange GetProxy(IWebBrowser control, nsIDOMRange obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMRange).GUID, obj) as nsIDOMRange;
	}
}
