using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsCancelable
{
	public static nsICancelable GetProxy(IWebBrowser control, nsICancelable obj)
	{
		return Base.GetProxyForObject(control, typeof(nsICancelable).GUID, obj) as nsICancelable;
	}
}
