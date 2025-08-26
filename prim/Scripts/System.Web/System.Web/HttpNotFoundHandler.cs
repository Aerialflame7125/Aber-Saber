namespace System.Web;

internal sealed class HttpNotFoundHandler : IHttpHandler
{
	public bool IsReusable => true;

	public void ProcessRequest(HttpContext context)
	{
		string path = context.Request.Path;
		throw new HttpException(404, "Path '" + path + "' was not found.", path);
	}
}
