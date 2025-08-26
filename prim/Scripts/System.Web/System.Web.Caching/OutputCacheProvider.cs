using System.Configuration.Provider;

namespace System.Web.Caching;

/// <summary>Serves as a base class that contains abstract methods for implementing an output-cache provider. </summary>
public abstract class OutputCacheProvider : ProviderBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.OutputCacheProvider" /> class. </summary>
	protected OutputCacheProvider()
	{
	}

	/// <summary>Inserts the specified entry into the output cache. </summary>
	/// <param name="key">A unique identifier for <paramref name="entry" />.</param>
	/// <param name="entry">The content to add to the output cache.</param>
	/// <param name="utcExpiry">The time and date on which the cached entry expires.</param>
	/// <returns>A reference to the specified provider. </returns>
	public abstract object Add(string key, object entry, DateTime utcExpiry);

	/// <summary>Returns a reference to the specified entry in the output cache.</summary>
	/// <param name="key">A unique identifier for a cached entry in the output cache. </param>
	/// <returns>The <paramref name="key" /> value that identifies the specified entry in the cache, or <see langword="null" /> if the specified entry is not in the cache.</returns>
	public abstract object Get(string key);

	/// <summary>Removes the specified entry from the output cache.</summary>
	/// <param name="key">The unique identifier for the entry to remove from the output cache. </param>
	public abstract void Remove(string key);

	/// <summary>Inserts the specified entry into the output cache, overwriting the entry if it is already cached.</summary>
	/// <param name="key">A unique identifier for <paramref name="entry" />.</param>
	/// <param name="entry">The content to add to the output cache.</param>
	/// <param name="utcExpiry">The time and date on which the cached <paramref name="entry" /> expires.</param>
	public abstract void Set(string key, object entry, DateTime utcExpiry);
}
