using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCounter
{
	public static nsIDOMCounter GetProxy(IWebBrowser control, nsIDOMCounter obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCounter).GUID, obj) as nsIDOMCounter;
	}
}
