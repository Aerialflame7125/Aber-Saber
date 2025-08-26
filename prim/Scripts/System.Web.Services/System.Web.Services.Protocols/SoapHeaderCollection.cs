using System.Collections;
using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>Contains a collection of instances of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> class.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class SoapHeaderCollection : CollectionBase
{
	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> at the specified index of the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to get or set. </param>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameteris not a valid index in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </exception>
	public SoapHeader this[int index]
	{
		get
		{
			return (SoapHeader)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
	/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to add to the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	/// <returns>The position at which the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> was inserted.</returns>
	public int Add(SoapHeader header)
	{
		return base.List.Add(header);
	}

	/// <summary>Inserts a <see cref="T:System.Web.Services.Protocols.SoapHeader" /> into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index at which to insert the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to insert into the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameteris not a valid index in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </exception>
	public void Insert(int index, SoapHeader header)
	{
		base.List.Insert(index, header);
	}

	/// <summary>Determines the index of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
	/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to locate in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	/// <returns>The index of the <paramref name="header" /> parameter, if found in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />; otherwise, -1.</returns>
	public int IndexOf(SoapHeader header)
	{
		return base.List.IndexOf(header);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> contains a specific <see cref="T:System.Web.Services.Protocols.SoapHeader" />.</summary>
	/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to locate in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the value of the <paramref name="header" /> parameter is found in the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />; otherwise, <see langword="false" />.</returns>
	public bool Contains(SoapHeader header)
	{
		return base.List.Contains(header);
	}

	/// <summary>Removes the first occurrence of a specific <see cref="T:System.Web.Services.Protocols.SoapHeader" /> from the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />.</summary>
	/// <param name="header">The <see cref="T:System.Web.Services.Protocols.SoapHeader" /> to remove from the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. </param>
	public void Remove(SoapHeader header)
	{
		base.List.Remove(header);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> to an <see cref="T:System.Array" />, starting at a particular index of the <see cref="T:System.Array" />.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" />. The array must have zero-based indexing. </param>
	/// <param name="index">The zero-based index in the <paramref name="array" /> parameter at which copying begins. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.-or- The number of elements in the source SoapHeaderCollection is greater than the available space from the <paramref name="index" /> parameter to the end of the destination array. </exception>
	public void CopyTo(SoapHeader[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> class. </summary>
	public SoapHeaderCollection()
	{
	}
}
