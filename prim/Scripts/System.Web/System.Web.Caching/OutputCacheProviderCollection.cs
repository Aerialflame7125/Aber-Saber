using System.Configuration.Provider;

namespace System.Web.Caching;

/// <summary>Represents the collection of output-cache providers that are configured for a Web site.</summary>
public sealed class OutputCacheProviderCollection : ProviderCollection
{
	/// <summary>Returns a reference to the named output-cache provider in the collection.</summary>
	/// <param name="name">The name of a provider in the collection.</param>
	/// <returns>A provider from the collection. </returns>
	public new OutputCacheProvider this[string name] => (OutputCacheProvider)base[name];

	/// <summary>Inserts a provider into the collection of output-cache providers.</summary>
	/// <param name="provider">An output cache provider.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="provider" /> is not of type <see cref="T:System.Web.Caching.OutputCacheProvider" />.</exception>
	public override void Add(ProviderBase provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		if (!(provider is OutputCacheProvider))
		{
			throw new ArgumentException(global::SR.GetString("Provider must implement the class '{0}'.", typeof(OutputCacheProvider).Name), "provider");
		}
		base.Add(provider);
	}

	/// <summary>Copies the output-cache providers to a compatible one-dimensional array at the specified index.</summary>
	/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection The array must have zero-based indexing.</param>
	/// <param name="index">The point in the array where the copying starts.</param>
	public void CopyTo(OutputCacheProvider[] array, int index)
	{
		CopyTo((ProviderBase[])array, index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.OutputCacheProviderCollection" /> class.</summary>
	public OutputCacheProviderCollection()
	{
	}
}
