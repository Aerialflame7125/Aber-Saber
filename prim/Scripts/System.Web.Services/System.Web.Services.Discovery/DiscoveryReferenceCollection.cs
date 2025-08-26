using System.Collections;

namespace System.Web.Services.Discovery;

/// <summary>A collection of discovery references. This class cannot be inherited.</summary>
public sealed class DiscoveryReferenceCollection : CollectionBase
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> at the specified index.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to get or set. </param>
	/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="i" /> is not a valid index in the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </exception>
	public DiscoveryReference this[int i]
	{
		get
		{
			return (DiscoveryReference)base.List[i];
		}
		set
		{
			base.List[i] = value;
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
	/// <returns>The position where the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> was inserted in the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</returns>
	public int Add(DiscoveryReference value)
	{
		return base.List.Add(value);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" /> contains a specific <see cref="T:System.Web.Services.Discovery.DiscoveryReference" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" /> contains the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(DiscoveryReference value)
	{
		return base.List.Contains(value);
	}

	/// <summary>Removes a <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> from the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" />. </param>
	public void Remove(DiscoveryReference value)
	{
		base.List.Remove(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryReferenceCollection" /> class. </summary>
	public DiscoveryReferenceCollection()
	{
	}
}
