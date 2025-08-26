using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Serves as the base class for all classes that represent an ASP.NET route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class RouteBase
{
	private bool _routeExistingFiles = true;

	/// <summary>Gets or sets a value that indicates whether ASP.NET routing should handle URLs that match an existing file.</summary>
	/// <returns>
	///     <see langword="true" /> if ASP.NET routing handles all requests, even those that match an existing file; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool RouteExistingFiles
	{
		get
		{
			return _routeExistingFiles;
		}
		set
		{
			_routeExistingFiles = value;
		}
	}

	/// <summary>When overridden in a derived class, returns route information about the request.</summary>
	/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
	/// <returns>An object that contains the values from the route definition if the route matches the current request, or <see langword="null" /> if the route does not match the request.</returns>
	public abstract RouteData GetRouteData(HttpContextBase httpContext);

	/// <summary>When overridden in a derived class, checks whether the route matches the specified values, and if so, generates a URL and retrieves information about the route.</summary>
	/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
	/// <param name="values">An object that contains the parameters for a route.</param>
	/// <returns>An object that contains the generated URL and information about the route, or <see langword="null" /> if the route does not match <paramref name="values" />.</returns>
	public abstract VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values);

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class. </summary>
	protected RouteBase()
	{
	}
}
