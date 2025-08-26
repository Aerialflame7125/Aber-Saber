using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDocCharset
{
	public static nsIDocCharset GetProxy(IWebBrowser control, nsIDocCharset obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDocCharset).GUID, obj) as nsIDocCharset;
	}
}
