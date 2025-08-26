using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Stores the URL routes for an application.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class RouteTable
{
	private static RouteCollection _instance = new RouteCollection();

	/// <summary>Gets a collection of objects that derive from the <see cref="T:System.Web.Routing.RouteBase" /> class.</summary>
	/// <returns>An object that contains all the routes in the collection.</returns>
	public static RouteCollection Routes => _instance;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteTable" /> class. </summary>
	public RouteTable()
	{
	}
}
