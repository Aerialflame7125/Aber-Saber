using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsSimpleEnumerator
{
	public static nsISimpleEnumerator GetProxy(IWebBrowser control, nsISimpleEnumerator obj)
	{
		return Base.GetProxyForObject(control, typeof(nsISimpleEnumerator).GUID, obj) as nsISimpleEnumerator;
	}
}
