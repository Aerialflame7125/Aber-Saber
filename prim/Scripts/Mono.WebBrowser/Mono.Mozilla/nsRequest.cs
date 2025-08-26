using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsRequest
{
	public static nsIRequest GetProxy(IWebBrowser control, nsIRequest obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIRequest).GUID, obj) as nsIRequest;
	}
}
