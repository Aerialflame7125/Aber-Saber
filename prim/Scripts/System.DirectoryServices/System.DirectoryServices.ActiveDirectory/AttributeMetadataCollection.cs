using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>A read-only collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ApplicationPartition" /> objects.</summary>
public class AttributeMetadataCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object that exists at the specified index.</returns>
	public AttributeMetadata this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object is in this collection.</summary>
	/// <param name="metadata">The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> is in this collection, otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="metadata" /> is <see langword="null" />.</exception>
	public bool Contains(AttributeMetadata metadata)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
	/// <param name="metadata">The <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching item. -1 if no member of this collection is identical to <paramref name="metadata" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="metadata" /> is <see langword="null" />.</exception>
	public int IndexOf(AttributeMetadata metadata)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="metadata">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="metadata" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="metadata" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(AttributeMetadata[] metadata, int index)
	{
		throw new NotImplementedException();
	}
}
