using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteCollection" /> class is a read-only collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects.</summary>
public class ReadOnlySiteCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public ActiveDirectorySite this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection.</summary>
	/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="site" /> is <see langword="null" />.</exception>
	public bool Contains(ActiveDirectorySite site)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object in this collection.</summary>
	/// <param name="site">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="site" /> parameter is <see langword="null" />.</exception>
	public int IndexOf(ActiveDirectorySite site)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="sites">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="sites" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sites" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ActiveDirectorySite[] sites, int index)
	{
		throw new NotImplementedException();
	}
}
