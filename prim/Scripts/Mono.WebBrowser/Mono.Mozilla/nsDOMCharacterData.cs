using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMCharacterData
{
	public static nsIDOMCharacterData GetProxy(IWebBrowser control, nsIDOMCharacterData obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMCharacterData).GUID, obj) as nsIDOMCharacterData;
	}
}
