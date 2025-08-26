using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformationCollection" /> class contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects.</summary>
public class TrustRelationshipInformationCollection : ReadOnlyCollectionBase
{
	/// <summary>Gets an <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to get.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	public TrustRelationshipInformation this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object is contained in this collection.</summary>
	/// <param name="information">The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the specified object is in this collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="information" /> is <see langword="null" />.</exception>
	public bool Contains(TrustRelationshipInformation information)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object in this collection.</summary>
	/// <param name="information">The <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="information" /> is <see langword="null" />.</exception>
	public int IndexOf(TrustRelationshipInformation information)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="array">The array of <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(TrustRelationshipInformation[] array, int index)
	{
		throw new NotImplementedException();
	}
}
