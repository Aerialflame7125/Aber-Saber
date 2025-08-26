using System.Collections;
using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>Contains a strongly-typed collection of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects.</summary>
[Serializable]
[System.MonoTODO("Fix serialization compatibility with MS.NET")]
public class DirectoryServicesPermissionEntryCollection : CollectionBase
{
	private DirectoryServicesPermission owner;

	/// <summary>Gets or sets a <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object in this collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to get or set.</param>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object that exists at the specified index.</returns>
	public DirectoryServicesPermissionEntry this[int index]
	{
		get
		{
			return base.List[index] as DirectoryServicesPermissionEntry;
		}
		set
		{
			base.List[index] = value;
		}
	}

	internal DirectoryServicesPermissionEntryCollection(DirectoryServicesPermission owner)
	{
		this.owner = owner;
		ResourcePermissionBaseEntry[] entries = owner.GetEntries();
		if (entries.Length != 0)
		{
			ResourcePermissionBaseEntry[] array = entries;
			foreach (ResourcePermissionBaseEntry resourcePermissionBaseEntry in array)
			{
				DirectoryServicesPermissionEntry value = new DirectoryServicesPermissionEntry((DirectoryServicesPermissionAccess)resourcePermissionBaseEntry.PermissionAccess, resourcePermissionBaseEntry.PermissionAccessPath[0]);
				base.InnerList.Add(value);
			}
		}
	}

	/// <summary>Appends the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to add to this collection.</param>
	/// <returns>The zero-based index of the added <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object that is appended to this collection.</returns>
	public int Add(DirectoryServicesPermissionEntry value)
	{
		return base.List.Add(value);
	}

	/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> array to this collection.</summary>
	/// <param name="value">
	///   <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> array that contains the objects to append to this collection.</param>
	public void AddRange(DirectoryServicesPermissionEntry[] value)
	{
		foreach (DirectoryServicesPermissionEntry value2 in value)
		{
			Add(value2);
		}
	}

	/// <summary>Appends the contents of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object to this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object that contains the objects to append to this collection.</param>
	public void AddRange(DirectoryServicesPermissionEntryCollection value)
	{
		foreach (DirectoryServicesPermissionEntry item in value)
		{
			Add(item);
		}
	}

	/// <summary>Copies all <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects in this collection to the specified array, starting at the specified index in the target array.</summary>
	/// <param name="array">The array of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects that receives the elements of this collection.</param>
	/// <param name="index">The zero-based index in the array where this method starts copying this collection.</param>
	public void CopyTo(DirectoryServicesPermissionEntry[] array, int index)
	{
		foreach (DirectoryServicesPermissionEntry item in base.List)
		{
			array[index++] = item;
		}
	}

	/// <summary>Determines if the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object is in this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to search for in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object is in this collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(DirectoryServicesPermissionEntry value)
	{
		return base.List.Contains(value);
	}

	/// <summary>Returns the index of the first occurrence of the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object in this collection.</summary>
	/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to search for in this collection.</param>
	/// <returns>The zero-based index of the first matching object.  Returns -1 if no member of this collection is identical to the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object.</returns>
	public int IndexOf(DirectoryServicesPermissionEntry value)
	{
		return base.List.IndexOf(value);
	}

	/// <summary>Inserts the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> into this collection at the specified index.</summary>
	/// <param name="index">The zero-based index in this collection where the object is inserted.</param>
	/// <param name="value">The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to insert into this collection.</param>
	public void Insert(int index, DirectoryServicesPermissionEntry value)
	{
		base.List.Insert(index, value);
	}

	/// <summary>Removes the first occurrence of an object in this collection that is identical to the specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object.</summary>
	/// <param name="value">The specified <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> object to remove from this collection.</param>
	public void Remove(DirectoryServicesPermissionEntry value)
	{
		base.List.Remove(value);
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnClear" /> method.</summary>
	protected override void OnClear()
	{
		owner.ClearEntries();
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnInsert(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
	/// <param name="value">The new value of the element at <paramref name="index" />.</param>
	protected override void OnInsert(int index, object value)
	{
		owner.Add(value);
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnRemove(System.Int32,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
	/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
	protected override void OnRemove(int index, object value)
	{
		owner.Remove(value);
	}

	/// <summary>Overrides the <see cref="M:System.Collections.CollectionBase.OnSet(System.Int32,System.Object,System.Object)" /> method.</summary>
	/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
	/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
	/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
	protected override void OnSet(int index, object oldValue, object newValue)
	{
		owner.Remove(oldValue);
		owner.Add(newValue);
	}
}
