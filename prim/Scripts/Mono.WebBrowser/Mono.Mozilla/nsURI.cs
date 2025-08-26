using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsURI
{
	public static nsIURI GetProxy(IWebBrowser control, nsIURI obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIURI).GUID, obj) as nsIURI;
	}
}
