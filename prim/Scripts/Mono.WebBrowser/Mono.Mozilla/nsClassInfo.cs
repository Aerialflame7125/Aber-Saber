using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsClassInfo
{
	public static nsIClassInfo GetProxy(IWebBrowser control, nsIClassInfo obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIClassInfo).GUID, obj) as nsIClassInfo;
	}
}
