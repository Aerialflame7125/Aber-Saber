using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace System.Web.Routing;

/// <summary>Provides properties and methods for defining a route and for obtaining information about the route.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class Route : RouteBase
{
	private const string HttpMethodParameterName = "httpMethod";

	private string _url;

	private ParsedRoute _parsedRoute;

	/// <summary>Gets or sets a dictionary of expressions that specify valid values for a URL parameter.</summary>
	/// <returns>An object that contains the parameter names and expressions.</returns>
	public RouteValueDictionary Constraints { get; set; }

	/// <summary>Gets or sets custom values that are passed to the route handler, but which are not used to determine whether the route matches a URL pattern.</summary>
	/// <returns>An object that contains custom values.</returns>
	public RouteValueDictionary DataTokens { get; set; }

	/// <summary>Gets or sets the values to use if the URL does not contain all the parameters.</summary>
	/// <returns>An object that contains the parameter names and default values.</returns>
	public RouteValueDictionary Defaults { get; set; }

	/// <summary>Gets or sets the object that processes requests for the route.</summary>
	/// <returns>The object that processes the request.</returns>
	public IRouteHandler RouteHandler { get; set; }

	/// <summary>Gets or sets the URL pattern for the route.</summary>
	/// <returns>The pattern for matching the route to a URL.</returns>
	/// <exception cref="T:System.ArgumentException">Any of the following:The value starts with ~ or /.The value contains a ? character.The catch-all parameter is not last.</exception>
	/// <exception cref="T:System.Exception">URL segments are not separated by a delimiter or a literal constant.</exception>
	public string Url
	{
		get
		{
			return _url ?? string.Empty;
		}
		set
		{
			_parsedRoute = RouteParser.Parse(value);
			_url = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern and handler class. </summary>
	/// <param name="url">The URL pattern for the route.</param>
	/// <param name="routeHandler">The object that processes requests for the route.</param>
	public Route(string url, IRouteHandler routeHandler)
	{
		Url = url;
		RouteHandler = routeHandler;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, and handler class. </summary>
	/// <param name="url">The URL pattern for the route.</param>
	/// <param name="defaults">The values to use for any parameters that are missing in the URL.</param>
	/// <param name="routeHandler">The object that processes requests for the route.</param>
	public Route(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
	{
		Url = url;
		Defaults = defaults;
		RouteHandler = routeHandler;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, and handler class. </summary>
	/// <param name="url">The URL pattern for the route.</param>
	/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
	/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
	/// <param name="routeHandler">The object that processes requests for the route.</param>
	public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
	{
		Url = url;
		Defaults = defaults;
		Constraints = constraints;
		RouteHandler = routeHandler;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.Route" /> class, by using the specified URL pattern, default parameter values, constraints, custom values, and handler class. </summary>
	/// <param name="url">The URL pattern for the route.</param>
	/// <param name="defaults">The values to use if the URL does not contain all the parameters.</param>
	/// <param name="constraints">A regular expression that specifies valid values for a URL parameter.</param>
	/// <param name="dataTokens">Custom values that are passed to the route handler, but which are not used to determine whether the route matches a specific URL pattern. These values are passed to the route handler, where they can be used for processing the request.</param>
	/// <param name="routeHandler">The object that processes requests for the route.</param>
	public Route(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
	{
		Url = url;
		Defaults = defaults;
		Constraints = constraints;
		DataTokens = dataTokens;
		RouteHandler = routeHandler;
	}

	/// <summary>Returns information about the requested route.</summary>
	/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
	/// <returns>An object that contains the values from the route definition.</returns>
	public override RouteData GetRouteData(HttpContextBase httpContext)
	{
		string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
		RouteValueDictionary routeValueDictionary = _parsedRoute.Match(virtualPath, Defaults);
		if (routeValueDictionary == null)
		{
			return null;
		}
		RouteData routeData = new RouteData(this, RouteHandler);
		if (!ProcessConstraints(httpContext, routeValueDictionary, RouteDirection.IncomingRequest))
		{
			return null;
		}
		foreach (KeyValuePair<string, object> item in routeValueDictionary)
		{
			routeData.Values.Add(item.Key, item.Value);
		}
		if (DataTokens != null)
		{
			foreach (KeyValuePair<string, object> dataToken in DataTokens)
			{
				routeData.DataTokens[dataToken.Key] = dataToken.Value;
			}
		}
		return routeData;
	}

	/// <summary>Returns information about the URL that is associated with the route.</summary>
	/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
	/// <param name="values">An object that contains the parameters for a route.</param>
	/// <returns>An object that contains information about the URL that is associated with the route.</returns>
	public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
	{
		BoundUrl boundUrl = _parsedRoute.Bind(requestContext.RouteData.Values, values, Defaults, Constraints);
		if (boundUrl == null)
		{
			return null;
		}
		if (!ProcessConstraints(requestContext.HttpContext, boundUrl.Values, RouteDirection.UrlGeneration))
		{
			return null;
		}
		VirtualPathData virtualPathData = new VirtualPathData(this, boundUrl.Url);
		if (DataTokens != null)
		{
			foreach (KeyValuePair<string, object> dataToken in DataTokens)
			{
				virtualPathData.DataTokens[dataToken.Key] = dataToken.Value;
			}
		}
		return virtualPathData;
	}

	/// <summary>Determines whether a parameter value matches the constraint for that parameter.</summary>
	/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
	/// <param name="constraint">The regular expression or object to use to test <paramref name="parameterName" />.</param>
	/// <param name="parameterName">The name of the parameter to test.</param>
	/// <param name="values">The values to test.</param>
	/// <param name="routeDirection">A value that specifies whether URL routing is processing an incoming request or constructing a URL.</param>
	/// <returns>
	///     <see langword="true" /> if the parameter value matches the constraint; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="constraint" /> is not a string that contains a regular expression.</exception>
	protected virtual bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
	{
		if (constraint is IRouteConstraint routeConstraint)
		{
			return routeConstraint.Match(httpContext, this, parameterName, values, routeDirection);
		}
		if (!(constraint is string text))
		{
			throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("The constraint entry '{0}' on the route with URL '{1}' must have a string value or be of a type which implements IRouteConstraint."), parameterName, Url));
		}
		values.TryGetValue(parameterName, out var value);
		string input = Convert.ToString(value, CultureInfo.InvariantCulture);
		string pattern = "^(" + text + ")$";
		return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
	}

	private bool ProcessConstraints(HttpContextBase httpContext, RouteValueDictionary values, RouteDirection routeDirection)
	{
		if (Constraints != null)
		{
			foreach (KeyValuePair<string, object> constraint in Constraints)
			{
				if (!ProcessConstraint(httpContext, constraint.Value, constraint.Key, values, routeDirection))
				{
					return false;
				}
			}
		}
		return true;
	}
}
