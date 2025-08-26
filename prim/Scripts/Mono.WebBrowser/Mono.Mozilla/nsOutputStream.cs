using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsOutputStream
{
	public static nsIOutputStream GetProxy(IWebBrowser control, nsIOutputStream obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIOutputStream).GUID, obj) as nsIOutputStream;
	}
}
