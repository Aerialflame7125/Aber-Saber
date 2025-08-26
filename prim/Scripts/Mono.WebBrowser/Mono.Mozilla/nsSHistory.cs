using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsSHistory
{
	public static nsISHistory GetProxy(IWebBrowser control, nsISHistory obj)
	{
		return Base.GetProxyForObject(control, typeof(nsISHistory).GUID, obj) as nsISHistory;
	}
}
