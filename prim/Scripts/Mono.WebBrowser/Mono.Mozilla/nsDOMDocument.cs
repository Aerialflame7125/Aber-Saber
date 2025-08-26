using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocument
{
	public static nsIDOMDocument GetProxy(IWebBrowser control, nsIDOMDocument obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocument).GUID, obj) as nsIDOMDocument;
	}
}
