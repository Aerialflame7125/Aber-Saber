using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyDirectoryServerCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects.</summary>
public class ReadOnlyDirectoryServerCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public DirectoryServer this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object is in this collection.</summary>
	/// <param name="directoryServer">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="directoryServer" /> is <see langword="null" />.</exception>
	public bool Contains(DirectoryServer directoryServer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object in this collection.</summary>
	/// <param name="directoryServer">The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="directoryServer" /> is <see langword="null" />.</exception>
	public int IndexOf(DirectoryServer directoryServer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="directoryServers">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="directoryServers" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="directoryServers" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(DirectoryServer[] directoryServers, int index)
	{
		throw new NotImplementedException();
	}
}
