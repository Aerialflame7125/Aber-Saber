using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Provides a way to specify that ASP.NET routing should not handle requests for a URL pattern.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class StopRoutingHandler : IRouteHandler
{
	/// <summary>Returns the object that processes the request.</summary>
	/// <param name="requestContext">An object that encapsulates information about the request.</param>
	/// <returns>An object that processes the request.</returns>
	protected virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
	{
		throw new NotSupportedException();
	}

	/// <summary>Returns the object that processes the request.</summary>
	/// <param name="requestContext">An object that encapsulates information about the request.</param>
	/// <returns>An object that processes the request.</returns>
	IHttpHandler IRouteHandler.GetHttpHandler(RequestContext requestContext)
	{
		return GetHttpHandler(requestContext);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.StopRoutingHandler" /> class. </summary>
	public StopRoutingHandler()
	{
	}
}
