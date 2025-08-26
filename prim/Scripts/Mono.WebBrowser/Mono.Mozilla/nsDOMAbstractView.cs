using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMAbstractView
{
	public static nsIDOMAbstractView GetProxy(IWebBrowser control, nsIDOMAbstractView obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMAbstractView).GUID, obj) as nsIDOMAbstractView;
	}
}
