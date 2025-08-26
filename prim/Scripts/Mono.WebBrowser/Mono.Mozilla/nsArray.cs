using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsArray
{
	public static nsIArray GetProxy(IWebBrowser control, nsIArray obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIArray).GUID, obj) as nsIArray;
	}
}
