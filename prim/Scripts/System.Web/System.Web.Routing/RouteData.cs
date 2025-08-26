using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Encapsulates information about a route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class RouteData
{
	private IRouteHandler _routeHandler;

	private RouteValueDictionary _values = new RouteValueDictionary();

	private RouteValueDictionary _dataTokens = new RouteValueDictionary();

	/// <summary>Gets a collection of custom values that are passed to the route handler but are not used when ASP.NET routing determines whether the route matches a request.</summary>
	/// <returns>An object that contains custom values.</returns>
	public RouteValueDictionary DataTokens => _dataTokens;

	/// <summary>Gets or sets the object that represents a route.</summary>
	/// <returns>An object that represents the route definition.</returns>
	public RouteBase Route { get; set; }

	/// <summary>Gets or sets the object that processes a requested route.</summary>
	/// <returns>An object that processes the route request.</returns>
	public IRouteHandler RouteHandler
	{
		get
		{
			return _routeHandler;
		}
		set
		{
			_routeHandler = value;
		}
	}

	/// <summary>Gets a collection of URL parameter values and default values for the route.</summary>
	/// <returns>An object that contains values that are parsed from the URL and from default values.</returns>
	public RouteValueDictionary Values => _values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class. </summary>
	public RouteData()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteData" /> class by using the specified route and route handler. </summary>
	/// <param name="route">An object that defines the route.</param>
	/// <param name="routeHandler">An object that processes the request.</param>
	public RouteData(RouteBase route, IRouteHandler routeHandler)
	{
		Route = route;
		RouteHandler = routeHandler;
	}

	/// <summary>Retrieves the value with the specified identifier.</summary>
	/// <param name="valueName">The key of the value to retrieve.</param>
	/// <returns>The element in the <see cref="P:System.Web.Routing.RouteData.Values" /> property whose key matches <paramref name="valueName" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">A value does not exist for <paramref name="valueName" />.</exception>
	public string GetRequiredString(string valueName)
	{
		if (Values.TryGetValue(valueName, out var value))
		{
			string text = value as string;
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
		}
		throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The RouteData must contain an item named '{0}' with a non-empty string value."), valueName));
	}
}
