namespace System.Web;

internal class HttpMethodNotAllowedHandler : IHttpHandler
{
	public bool IsReusable => true;

	public void ProcessRequest(HttpContext context)
	{
		throw new HttpException(405, "Method not allowed");
	}
}
