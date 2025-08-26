using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Represents information about the route and virtual path that are the result of generating a URL with the ASP.NET routing framework.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class VirtualPathData
{
	private string _virtualPath;

	private RouteValueDictionary _dataTokens = new RouteValueDictionary();

	/// <summary>Gets the collection of custom values for the route definition.</summary>
	/// <returns>A collection of custom values for a route.</returns>
	public RouteValueDictionary DataTokens => _dataTokens;

	/// <summary>Gets or sets the route that is used to create the URL.</summary>
	/// <returns>An object that represents the route that matched the parameters that were used to generate a URL.</returns>
	public RouteBase Route { get; set; }

	/// <summary>Gets or sets the URL that was created from the route definition.</summary>
	/// <returns>The URL that was generated from a route.</returns>
	public string VirtualPath
	{
		get
		{
			return _virtualPath ?? string.Empty;
		}
		set
		{
			_virtualPath = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.VirtualPathData" /> class. </summary>
	/// <param name="route">The object that is used to generate the URL.</param>
	/// <param name="virtualPath">The generated URL.</param>
	public VirtualPathData(RouteBase route, string virtualPath)
	{
		Route = route;
		VirtualPath = virtualPath;
	}
}
