using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsPersistentProperties
{
	public static nsIPersistentProperties GetProxy(IWebBrowser control, nsIPersistentProperties obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIPersistentProperties).GUID, obj) as nsIPersistentProperties;
	}
}
