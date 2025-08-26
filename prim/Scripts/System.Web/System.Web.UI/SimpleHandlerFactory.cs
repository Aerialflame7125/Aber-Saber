using System.Web.Compilation;

namespace System.Web.UI;

internal class SimpleHandlerFactory : IHttpHandlerFactory
{
	public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
	{
		return BuildManager.CreateInstanceFromVirtualPath(virtualPath, typeof(IHttpHandler)) as IHttpHandler;
	}

	public virtual void ReleaseHandler(IHttpHandler handler)
	{
	}
}
