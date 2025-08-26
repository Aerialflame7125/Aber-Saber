using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentStyle
{
	public static nsIDOMDocumentStyle GetProxy(IWebBrowser control, nsIDOMDocumentStyle obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentStyle).GUID, obj) as nsIDOMDocumentStyle;
	}
}
