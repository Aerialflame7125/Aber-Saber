using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlySiteLinkBridgeCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> objects.</summary>
public class ReadOnlySiteLinkBridgeCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public ActiveDirectorySiteLinkBridge this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object is in this collection.</summary>
	/// <param name="bridge">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="bridge" /> is <see langword="null" />.</exception>
	public bool Contains(ActiveDirectorySiteLinkBridge bridge)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object in this collection.</summary>
	/// <param name="bridge">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="bridge" /> is <see langword="null" />.</exception>
	public int IndexOf(ActiveDirectorySiteLinkBridge bridge)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="bridges">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkBridge" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="bridges" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="bridges" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ActiveDirectorySiteLinkBridge[] bridges, int index)
	{
		throw new NotImplementedException();
	}
}
