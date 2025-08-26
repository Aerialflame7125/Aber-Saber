using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsProtocolHandler
{
	public static nsIProtocolHandler GetProxy(IWebBrowser control, nsIProtocolHandler obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIProtocolHandler).GUID, obj) as nsIProtocolHandler;
	}
}
