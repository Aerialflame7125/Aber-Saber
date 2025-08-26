using System.Collections;

namespace System.Web.Services.Discovery;

/// <summary>Represents a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> objects. This class cannot be inherited.</summary>
public sealed class DiscoveryClientReferenceCollection : DictionaryBase
{
	/// <summary>Gets or sets a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> object from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> with the specified URL.</summary>
	/// <param name="url">The URL for the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to get or set from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	/// <returns>An <see langword="DiscoveryReference" /> representing a reference to a discovery document.</returns>
	public DiscoveryReference this[string url]
	{
		get
		{
			return (DiscoveryReference)base.Dictionary[url];
		}
		set
		{
			base.Dictionary[url] = value;
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</returns>
	public ICollection Keys => base.Dictionary.Keys;

	/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</returns>
	public ICollection Values => base.Dictionary.Values;

	/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	public void Add(DiscoveryReference value)
	{
		Add(value.Url, value);
	}

	/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL and value to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
	/// <param name="url">The URL for the reference to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	public void Add(string url, DiscoveryReference value)
	{
		base.Dictionary.Add(url, value);
	}

	/// <summary>Determines if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> contains a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL.</summary>
	/// <param name="url">The URL for the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> contains a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL; otherwise, <see langword="false" />.</returns>
	public bool Contains(string url)
	{
		return base.Dictionary.Contains(url);
	}

	/// <summary>Removes a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />.</summary>
	/// <param name="url">A string that represents the URL for the object to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" />. </param>
	public void Remove(string url)
	{
		base.Dictionary.Remove(url);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> class. </summary>
	public DiscoveryClientReferenceCollection()
	{
	}
}
