namespace System.Web.UI;

/// <summary>Creates instances of classes that inherit from the <see cref="T:System.Web.UI.Page" /> class and implement the <see cref="T:System.Web.IHttpHandler" /> interface. Instances are created dynamically to handle requests for ASP.NET files. The <see cref="T:System.Web.UI.PageHandlerFactory" /> class is the default handler factory implementation for ASP.NET pages.</summary>
public class PageHandlerFactory : IHttpHandlerFactory
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageHandlerFactory" /> class.</summary>
	protected internal PageHandlerFactory()
	{
	}

	/// <summary>Returns an instance of the <see cref="T:System.Web.IHttpHandler" /> interface to process the requested resource.</summary>
	/// <param name="context">An instance of the <see cref="T:System.Web.HttpContext" /> class that provides references to intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests. </param>
	/// <param name="requestType">The HTTP data transfer method (GET or POST) that the client uses.</param>
	/// <param name="virtualPath">The virtual path to the requested resource.</param>
	/// <param name="path">The <see cref="P:System.Web.HttpRequest.PhysicalApplicationPath" /> property to the requested resource.</param>
	/// <returns>A new <see cref="T:System.Web.IHttpHandler" /> that processes the request; otherwise, <see langword="null" />.</returns>
	public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
	{
		return PageParser.GetCompiledPageInstance(virtualPath, path, context);
	}

	/// <summary>Enables a factory to reuse an existing instance of a handler.</summary>
	/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> to reuse.</param>
	public virtual void ReleaseHandler(IHttpHandler handler)
	{
	}
}
