using Mono.WebBrowser;

namespace Mono.Mozilla;

internal class nsDOMComment
{
	public static nsIDOMComment GetProxy(IWebBrowser control, nsIDOMComment obj)
	{
		return Base.GetProxyForObject(control, typeof(nsIDOMComment).GUID, obj) as nsIDOMComment;
	}
}
