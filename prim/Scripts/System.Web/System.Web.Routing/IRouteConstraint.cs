using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Defines the contract that a class must implement in order to check whether a URL parameter value is valid for a constraint.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public interface IRouteConstraint
{
	/// <summary>Determines whether the URL parameter contains a valid value for this constraint.</summary>
	/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
	/// <param name="route">The object that this constraint belongs to.</param>
	/// <param name="parameterName">The name of the parameter that is being checked.</param>
	/// <param name="values">An object that contains the parameters for the URL.</param>
	/// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated.</param>
	/// <returns>
	///     <see langword="true" /> if the URL parameter contains a valid value; otherwise, <see langword="false" />.</returns>
	bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection);
}
