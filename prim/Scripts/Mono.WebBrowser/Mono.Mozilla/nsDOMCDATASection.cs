using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCDATASection
{
	public static nsIDOMCDATASection GetProxy(IWebBrowser control, nsIDOMCDATASection obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCDATASection).GUID, obj) as nsIDOMCDATASection;
	}
}
