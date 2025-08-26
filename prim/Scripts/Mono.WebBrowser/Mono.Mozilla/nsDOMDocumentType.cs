using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentType
{
	public static nsIDOMDocumentType GetProxy(IWebBrowser control, nsIDOMDocumentType obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentType).GUID, obj) as nsIDOMDocumentType;
	}
}
