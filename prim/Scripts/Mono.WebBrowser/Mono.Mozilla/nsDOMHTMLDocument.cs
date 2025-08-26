using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMHTMLDocument
{
	public static nsIDOMHTMLDocument GetProxy(IWebBrowser control, nsIDOMHTMLDocument obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMHTMLDocument).GUID, obj) as nsIDOMHTMLDocument;
	}
}
