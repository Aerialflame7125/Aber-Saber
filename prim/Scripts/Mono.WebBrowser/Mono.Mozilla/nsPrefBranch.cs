using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsPrefBranch
{
	public static nsIPrefBranch GetProxy(IWebBrowser control, nsIPrefBranch obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIPrefBranch).GUID, obj) as nsIPrefBranch;
	}
}
