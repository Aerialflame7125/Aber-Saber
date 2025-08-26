using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> class is a read-only collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> objects.</summary>
public class ReplicationConnectionCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets a <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public ReplicationConnection this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object is in this collection.</summary>
	/// <param name="connection">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="connection" /> parameter is <see langword="null" />.</exception>
	public bool Contains(ReplicationConnection connection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object in this collection.</summary>
	/// <param name="connection">The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of the collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="connection" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The specified <paramref name="connection" /> does not exist.</exception>
	public int IndexOf(ReplicationConnection connection)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="connections">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnection" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="connections" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="connections" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ReplicationConnection[] connections, int index)
	{
		throw new NotImplementedException();
	}
}
