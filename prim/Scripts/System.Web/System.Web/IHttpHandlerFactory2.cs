namespace System.Web;

internal interface IHttpHandlerFactory2 : IHttpHandlerFactory
{
	IHttpHandler GetHandler(HttpContext context, string requestType, VirtualPath virtualPath, string physicalPath);
}
