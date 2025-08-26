using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> class contains replication information for a set of Active Directory Domain Services attributes.</summary>
public class ActiveDirectoryReplicationMetadata : DictionaryBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object in this collection.</summary>
	/// <param name="name">The LDAP display name of the attribute to get.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> object that represents the specified attribute.</returns>
	public AttributeMetadata this[string name]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the names that are contained in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the LDAP display names of the attributes in this collection.</returns>
	public ReadOnlyStringCollection AttributeNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the values that are contained in this collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> object that contains the <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection.</returns>
	public AttributeMetadataCollection Values
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified attribute is in this collection.</summary>
	/// <param name="attributeName">The LDAP display name of the attribute to search for.</param>
	/// <returns>
	///   <see langword="true" /> if the attribute is in this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(string attributeName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="array">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.AttributeMetadata" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough to hold the required number of elements.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is out of range of the destination array.</exception>
	public void CopyTo(AttributeMetadata[] array, int index)
	{
		throw new NotImplementedException();
	}
}
