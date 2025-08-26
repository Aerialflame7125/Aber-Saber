using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Encapsulates information about an HTTP request that matches a defined route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class RequestContext
{
	/// <summary>Gets information about the HTTP request.</summary>
	/// <returns>An object that contains information about the HTTP request.</returns>
	public virtual HttpContextBase HttpContext { get; set; }

	/// <summary>Gets information about the requested route.</summary>
	/// <returns>An object that contains information about the requested route.</returns>
	public virtual RouteData RouteData { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class.</summary>
	public RequestContext()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RequestContext" /> class. </summary>
	/// <param name="httpContext">An object that contains information about the HTTP request.</param>
	/// <param name="routeData">An object that contains information about the route that matched the current request.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpContext" /> or <paramref name="routeData" /> is <see langword="null" />.</exception>
	public RequestContext(HttpContextBase httpContext, RouteData routeData)
	{
		if (httpContext == null)
		{
			throw new ArgumentNullException("httpContext");
		}
		if (routeData == null)
		{
			throw new ArgumentNullException("routeData");
		}
		HttpContext = httpContext;
		RouteData = routeData;
	}
}
