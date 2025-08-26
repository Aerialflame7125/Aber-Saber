using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMProcessingInstruction
{
	public static nsIDOMProcessingInstruction GetProxy(IWebBrowser control, nsIDOMProcessingInstruction obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMProcessingInstruction).GUID, obj) as nsIDOMProcessingInstruction;
	}
}
