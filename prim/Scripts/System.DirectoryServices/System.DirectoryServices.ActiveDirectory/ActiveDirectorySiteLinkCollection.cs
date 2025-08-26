using System.Collections;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkCollection" /> class is a read/write collection that contains <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects.</summary>
public class ActiveDirectorySiteLinkCollection : CollectionBase
{
	/// <summary>Gets or sets an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to get or set.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object that exists at the specified index.</returns>
	/// <exception cref="T:System.ArgumentException">The member already exists in this collection (applies to set only).</exception>
	/// <exception cref="T:System.ArgumentNullException">The item specified is <see langword="null" /> (applies to set only).</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of this collection.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> specified has not yet been saved to the Active Directory Domain Services store (applies to set only).</exception>
	public ActiveDirectorySiteLink this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Appends the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to this collection.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to append to this collection.</param>
	/// <returns>The zero-based index of the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object that is appended to this collection.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The object already exists in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="link" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public int Add(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> array to this collection.</summary>
	/// <param name="links">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> array that contains the objects to append to this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The object already exists in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="links" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public void AddRange(ActiveDirectorySiteLink[] links)
	{
		throw new NotImplementedException();
	}

	/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkCollection" /> object to this collection.</summary>
	/// <param name="links">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLinkCollection" /> object that contains the objects to append to this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The object already exists in this collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="links" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public void AddRange(ActiveDirectorySiteLinkCollection links)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object is in this collection.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object is in this collection; otherwise <see langword="false" />.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="link" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public bool Contains(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> objects in this collection to the specified array, starting at the specified index of the target array.</summary>
	/// <param name="array">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> array that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> where this method starts copying this collection.</param>
	/// <exception cref="T:System.ArgumentException">The destination array is not large enough, based on the source collection size and the <paramref name="index" /> specified.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the destination array.</exception>
	public void CopyTo(ActiveDirectorySiteLink[] array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object in this collection.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object. Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="link" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public int IndexOf(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object into this collection at the specified index.</summary>
	/// <param name="index">The zero-based index in this collection where the object is inserted.</param>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to insert into this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The object already exists in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="link" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> specified is out of range of the collection.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public void Insert(int index, ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object.</summary>
	/// <param name="link">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object to remove from this collection.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentException">The object does not exist in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="link" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	public void Remove(ActiveDirectorySiteLink link)
	{
		throw new NotImplementedException();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
	protected override void OnClearComplete()
	{
		throw new NotImplementedException();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClearComplete" /> method.</summary>
	/// <param name="index">The zero-based index at which the element was inserted.</param>
	/// <param name="value">The new value of the element at <paramref name="index" />.</param>
	protected override void OnInsertComplete(int index, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemoveComplete(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which the element was removed.</param>
	/// <param name="value">The element that was removed from the collection.</param>
	protected override void OnRemoveComplete(int index, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSetComplete(System.Int32,System.Object,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which the set operation occurred.</param>
	/// <param name="oldValue">The element that was replaced by <paramref name="newValue" />.</param>
	/// <param name="newValue">The element that replaced <paramref name="oldValue" />.</param>
	protected override void OnSetComplete(int index, object oldValue, object newValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnValidate(System.Object)" /> method.</summary>
	/// <param name="value">The element in this collection to validate.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not an <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySiteLink" /> object has not yet been saved in the Active Directory Domain Services store.</exception>
	protected override void OnValidate(object value)
	{
		throw new NotImplementedException();
	}
}
