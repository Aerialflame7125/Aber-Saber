using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyStringCollection" /> class is a read-only collection that contains <see cref="T:System.String" /> objects.</summary>
public class ReadOnlyStringCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets a <see cref="T:System.String" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.String" /> object to get.</param>
	/// <returns>The <see cref="T:System.String" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public string this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.String" /> object is in this collection.</summary>
	/// <param name="value">The <see cref="T:System.String" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.String" /> object is in this collection, otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
	public bool Contains(string value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.String" /> object in this collection.</summary>
	/// <param name="value">The <see cref="T:System.String" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.String" /> object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="site" /> is <see langword="null" />.</exception>
	public int IndexOf(string value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.String" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="values">The array of <see cref="T:System.String" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="values" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(string[] values, int index)
	{
		throw new NotImplementedException();
	}
}
