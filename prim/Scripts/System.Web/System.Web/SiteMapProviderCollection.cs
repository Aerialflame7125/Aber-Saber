using System.Configuration.Provider;

namespace System.Web;

/// <summary>Used by the <see cref="T:System.Web.SiteMap" /> class to track the set of <see cref="T:System.Web.SiteMapProvider" /> objects that are available to the <see cref="T:System.Web.SiteMap" /> during site map initialization. This class cannot be inherited. </summary>
public sealed class SiteMapProviderCollection : ProviderCollection
{
	/// <summary>Gets a <see cref="T:System.Web.SiteMapProvider" /> object with a specific name from the provider collection.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.SiteMapProvider" /> to find. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> that represents a <see cref="T:System.Web.SiteMapProviderCollection" /> element.</returns>
	public new SiteMapProvider this[string name] => (SiteMapProvider)base[name];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapProviderCollection" /> class.</summary>
	public SiteMapProviderCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapProvider" /> to the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property as the key.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> to add. </param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of the <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="provider" /> is not an instance of the <see cref="T:System.Web.SiteMapProvider" /> class.- or -A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
	public override void Add(ProviderBase provider)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		if (!(provider is SiteMapProvider))
		{
			throw new InvalidOperationException($"{provider.GetType()} must implement {typeof(SiteMapProvider)} to act as a site map provider");
		}
		if (this[provider.Name] != null)
		{
			throw new ArgumentException("Duplicate site map providers");
		}
		base.Add(provider);
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapProvider" /> object to the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> property as the key.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
	/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />.</exception>
	public void Add(SiteMapProvider provider)
	{
		Add((ProviderBase)provider);
	}

	/// <summary>Adds an array of <see cref="T:System.Web.SiteMapProvider" /> objects into the provider collection using the <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> properties as keys.</summary>
	/// <param name="providerArray">The array of <see cref="T:System.Web.SiteMapProvider" /> objects to add.</param>
	/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.SiteMapProvider" /> with the same name already exists in the <see cref="T:System.Web.SiteMapProviderCollection" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerArray" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.SiteMapProviderCollection" /> is read-only.</exception>
	public void AddArray(SiteMapProvider[] providerArray)
	{
		foreach (SiteMapProvider provider in providerArray)
		{
			Add(provider);
		}
	}
}
