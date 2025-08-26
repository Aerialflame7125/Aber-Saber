using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMDocumentFragment
{
	public static nsIDOMDocumentFragment GetProxy(IWebBrowser control, nsIDOMDocumentFragment obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMDocumentFragment).GUID, obj) as nsIDOMDocumentFragment;
	}
}
