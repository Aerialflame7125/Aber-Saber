using System.Runtime.CompilerServices;

namespace System.Web.Routing;

/// <summary>Indicates whether ASP.NET routing is processing a URL from a client or generating a URL.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public enum RouteDirection
{
	/// <summary>A URL from a client is being processed.</summary>
	IncomingRequest,
	/// <summary>A URL is being created based on the route definition.</summary>
	UrlGeneration
}
