using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMText
{
	public static nsIDOMText GetProxy(IWebBrowser control, nsIDOMText obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMText).GUID, obj) as nsIDOMText;
	}
}
