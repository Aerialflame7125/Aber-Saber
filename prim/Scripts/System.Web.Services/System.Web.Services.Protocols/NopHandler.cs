namespace System.Web.Services.Protocols;

internal class NopHandler : IHttpHandler
{
	public bool IsReusable => false;

	public void ProcessRequest(HttpContext context)
	{
	}
}
