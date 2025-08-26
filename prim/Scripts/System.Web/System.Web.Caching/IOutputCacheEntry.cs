using System.Collections.Generic;

namespace System.Web.Caching;

/// <summary>Defines collections of HTTP header and response elements that together make up one kind of output-cached data that ASP.NET can pass to a provider. </summary>
public interface IOutputCacheEntry
{
	/// <summary>Gets the collection of HTTP header elements in an output-cache entry.</summary>
	/// <returns>A list of HTTP header elements.</returns>
	List<HeaderElement> HeaderElements { get; set; }

	/// <summary>Gets the collection of HTTP response elements in an output-cache entry.</summary>
	/// <returns>A list of HTTP response elements.</returns>
	List<ResponseElement> ResponseElements { get; set; }
}
