namespace System.Web.Routing;

internal sealed class UrlAuthFailureHandler : IHttpHandler
{
	public bool IsReusable => true;

	public void ProcessRequest(HttpContext context)
	{
		throw new NotImplementedException();
	}
}
