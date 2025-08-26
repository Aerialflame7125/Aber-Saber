namespace System.Web.UI;

/// <summary>Describes the way data cached using ASP.NET caching mechanisms expires when a time-out is set. </summary>
public enum DataSourceCacheExpiry
{
	/// <summary>Cached data expires when the amount of time specified by the <see langword="CacheDuration" /> property has passed since the data was first cached.</summary>
	Absolute,
	/// <summary>Cached data expires only when the cache entry has not been used for the amount of time specified by the <see langword="CacheDuration" /> property.</summary>
	Sliding
}
