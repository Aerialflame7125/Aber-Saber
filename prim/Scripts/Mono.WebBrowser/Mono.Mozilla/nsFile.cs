using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsFile
{
	public static nsIFile GetProxy(IWebBrowser control, nsIFile obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIFile).GUID, obj) as nsIFile;
	}
}
