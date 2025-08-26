using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsInputStream
{
	public static nsIInputStream GetProxy(IWebBrowser control, nsIInputStream obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIInputStream).GUID, obj) as nsIInputStream;
	}
}
