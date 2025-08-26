namespace System.Web;

internal sealed class HttpNotImplementedHandler : IHttpHandler
{
	public bool IsReusable => true;

	public void ProcessRequest(HttpContext context)
	{
		HttpRequest request = context.Request;
		throw new HttpException(501, request.HttpMethod + " " + request.Path + " is not implemented.");
	}
}
