namespace System.Web;

/// <summary>Defines the contract that class factories must implement to create new <see cref="T:System.Web.IHttpHandler" /> objects.</summary>
public interface IHttpHandlerFactory
{
	/// <summary>Returns an instance of a class that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
	/// <param name="context">An instance of the <see cref="T:System.Web.HttpContext" /> class that provides references to intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests. </param>
	/// <param name="requestType">The HTTP data transfer method (<see langword="GET" /> or <see langword="POST" />) that the client uses. </param>
	/// <param name="url">The <see cref="P:System.Web.HttpRequest.RawUrl" /> of the requested resource. </param>
	/// <param name="pathTranslated">The <see cref="P:System.Web.HttpRequest.PhysicalApplicationPath" /> to the requested resource. </param>
	/// <returns>A new <see cref="T:System.Web.IHttpHandler" /> object that processes the request.</returns>
	IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated);

	/// <summary>Enables a factory to reuse an existing handler instance.</summary>
	/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> object to reuse. </param>
	void ReleaseHandler(IHttpHandler handler);
}
