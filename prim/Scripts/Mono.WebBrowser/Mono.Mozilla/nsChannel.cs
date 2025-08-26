using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsChannel
{
	public static nsIChannel GetProxy(IWebBrowser control, nsIChannel obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIChannel).GUID, obj) as nsIChannel;
	}
}
