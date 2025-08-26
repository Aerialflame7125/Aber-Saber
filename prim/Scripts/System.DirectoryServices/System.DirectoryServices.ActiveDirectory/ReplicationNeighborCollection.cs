using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> class is a read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> objects.</summary>
public class ReplicationNeighborCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public ReplicationNeighbor this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object is in this collection.</summary>
	/// <param name="neighbor">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object is in this collection, otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="neighbor" /> is <see langword="null" />.</exception>
	public bool Contains(ReplicationNeighbor neighbor)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object in this collection.</summary>
	/// <param name="neighbor">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="neighbor" /> is <see langword="null" />.</exception>
	public int IndexOf(ReplicationNeighbor neighbor)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="neighbors">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="neighbors" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="neighbors" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ReplicationNeighbor[] neighbors, int index)
	{
		throw new NotImplementedException();
	}
}
