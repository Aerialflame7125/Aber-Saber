using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects.</summary>
public class ReadOnlySiteLinkCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public ActiveDirectorySiteLink this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object is in this collection.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to search for.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="link" /> parameter is <see langword="null" />.</exception>
	public bool Contains(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the zero-based index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object in this collection.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to locate in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member in this collection is identical to the specified <paramref name="link" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="link" /> parameter is <see langword="null" />.</exception>
	public int IndexOf(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="links">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> array that receives the elements of this collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="links" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="links" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ActiveDirectorySiteLink[] links, int index)
	{
		base.InnerList.CopyTo(links, index);
	}
}
