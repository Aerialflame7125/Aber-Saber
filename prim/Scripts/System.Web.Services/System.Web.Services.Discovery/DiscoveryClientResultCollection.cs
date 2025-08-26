using System.Collections;

namespace System.Web.Services.Discovery;

/// <summary>Contains a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> objects. This class cannot be inherited.</summary>
public sealed class DiscoveryClientResultCollection : CollectionBase
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> at position <paramref name="i" /> of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
	/// <param name="i">The zero-based index of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to get or set. </param>
	/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="i" /> is not a valid index in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </exception>
	public DiscoveryClientResult this[int i]
	{
		get
		{
			return (DiscoveryClientResult)base.List[i];
		}
		set
		{
			base.List[i] = value;
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
	/// <returns>The position into which the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> was inserted.</returns>
	public int Add(DiscoveryClientResult value)
	{
		return base.List.Add(value);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> contains a specific <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to locate in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> is found in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(DiscoveryClientResult value)
	{
		return base.List.Contains(value);
	}

	/// <summary>Removes the first occurrence of a specific <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" />. </param>
	public void Remove(DiscoveryClientResult value)
	{
		base.List.Remove(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> class. </summary>
	public DiscoveryClientResultCollection()
	{
	}
}
