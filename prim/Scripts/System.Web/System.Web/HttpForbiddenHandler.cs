namespace System.Web;

internal class HttpForbiddenHandler : IHttpHandler
{
	public bool IsReusable => true;

	public void ProcessRequest(HttpContext context)
	{
		HttpRequest httpRequest = context?.Request;
		string text = httpRequest?.Path;
		string description = "The type of page you have requested is not served because it has been explicitly forbidden. The extension '" + ((text == null) ? string.Empty : VirtualPathUtility.GetExtension(text)) + "' may be incorrect. Please review the URL below and make sure that it is spelled correctly.";
		throw new HttpException(403, "This type of page is not served.", (httpRequest != null) ? HttpUtility.HtmlEncode(httpRequest.Path) : null, description);
	}
}
