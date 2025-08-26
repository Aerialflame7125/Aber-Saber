using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects within a control designer.</summary>
public class DesignerRegionCollection : IList, ICollection, IEnumerable
{
	/// <summary>Gets the number of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.Design.DesignerRegion" /> objects in the collection.</returns>
	[System.MonoNotSupported("")]
	public int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object has a fixed size.</summary>
	/// <returns>
	///   <see langword="true" />, if the size of the collection cannot be changed by adding or removing items; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool IsFixedSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object is read-only.</summary>
	/// <returns>
	///   <see langword="true" />, if the collection cannot be changed; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object is synchronized (thread safe).</summary>
	/// <returns>
	///   <see langword="true" />, if access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> is synchronized; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Web.UI.Design.DesignerRegion" /> object at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> to get or set in the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerRegion" /> at the specified index in the collection.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than zero.  
	/// -or-
	///  <paramref name="value" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public DesignerRegion this[int index]
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

	/// <summary>Gets the control designer that owns the designer region collection.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.ControlDesigner" /> that represents the control designer that owns the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" />.</returns>
	[System.MonoNotSupported("")]
	public ControlDesigner Owner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> object.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" />.</returns>
	[System.MonoNotSupported("")]
	public object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
	/// <returns>The number of elements in the collection.</returns>
	[System.MonoNotSupported("")]
	int ICollection.Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
	/// <returns>
	///   <see langword="true" />, if access to the collection is synchronized; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	bool ICollection.IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	[System.MonoNotSupported("")]
	object ICollection.SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>
	///   <see langword="false" />, if the collection dynamically increases in size as new objects are added; otherwise, <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	bool IList.IsFixedSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
	/// <returns>The value of the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.IsReadOnly" /> property.</returns>
	[System.MonoNotSupported("")]
	bool IList.IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
	/// <param name="index">The zero-based index of the object to get in the collection.</param>
	/// <returns>The object at the specified index in the collection.</returns>
	[System.MonoNotSupported("")]
	object IList.this[int index]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> class.</summary>
	[System.MonoNotSupported("")]
	public DesignerRegionCollection()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerRegionCollection" /> class for the specified control designer.</summary>
	/// <param name="owner">The control designer that owns this collection of designer regions.</param>
	[System.MonoNotSupported("")]
	public DesignerRegionCollection(ControlDesigner owner)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object to the end of the collection.</summary>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to add to the collection.</param>
	/// <returns>The index at which the region was added to the collection.</returns>
	[System.MonoNotSupported("")]
	public int Add(DesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes all regions from the collection.</summary>
	[System.MonoNotSupported("")]
	public void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the specified region is contained within the collection.</summary>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
	/// <returns>
	///   <see langword="true" />, if the region is in the collection; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool Contains(DesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" /> object, starting at a particular <see cref="T:System.Array" /> index.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied regions. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	[System.MonoNotSupported("")]
	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an enumerator that iterates through the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	[System.MonoNotSupported("")]
	public IEnumerator GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object within the collection.</summary>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="region" /> within the collection; otherwise, -1, if <paramref name="region" /> is not in the collection.</returns>
	[System.MonoNotSupported("")]
	public int IndexOf(DesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.Design.DesignerRegion" /> object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert the region.</param>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to insert into the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public void Insert(int index, DesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.Design.DesignerRegion" /> object from the collection.</summary>
	/// <param name="region">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to remove from the collection.</param>
	[System.MonoNotSupported("")]
	public void Remove(DesignerRegion region)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.Design.DesignerRegion" /> object at the specified index within the collection.</summary>
	/// <param name="index">The zero-based index within the collection of the <see cref="T:System.Web.UI.Design.DesignerRegion" /> to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerRegionCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied regions. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	[System.MonoNotSupported("")]
	void ICollection.CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	[System.MonoNotSupported("")]
	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
	/// <param name="o">The item to add to the collection.</param>
	/// <returns>The index at which the item was added to the collection.</returns>
	[System.MonoNotSupported("")]
	int IList.Add(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
	[System.MonoNotSupported("")]
	void IList.Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
	/// <returns>
	///   <see langword="true" />, if the region is in the collection; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	bool IList.Contains(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.Design.DesignerRegion" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of the object within the collection; otherwise, -1, if the object is not in the collection.</returns>
	[System.MonoNotSupported("")]
	int IList.IndexOf(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert the object.</param>
	/// <param name="o">The object to insert into the collection.</param>
	[System.MonoNotSupported("")]
	void IList.Insert(int index, object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
	/// <param name="o">The object to remove from the collection.</param>
	[System.MonoNotSupported("")]
	void IList.Remove(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
	/// <param name="index">The zero-based index within the collection of the object to remove.</param>
	[System.MonoNotSupported("")]
	void IList.RemoveAt(int index)
	{
		throw new NotImplementedException();
	}
}
