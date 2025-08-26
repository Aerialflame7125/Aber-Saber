using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects within a control designer. This class cannot be inherited.</summary>
public sealed class TemplateGroupCollection : IList, ICollection, IEnumerable
{
	/// <summary>Gets the number of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.Design.TemplateGroup" /> objects in the collection.</returns>
	[System.MonoNotSupported("")]
	public int Count
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Web.UI.Design.TemplateGroup" /> object at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> to get or set in the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.Design.TemplateGroup" /> at <paramref name="index" /> in the collection.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than zero.  
	/// -or-
	///  <paramref name="value" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public TemplateGroup this[int index]
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
	/// <returns>The number of elements in the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" />.</returns>
	[System.MonoNotSupported("")]
	int ICollection.Count
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
	/// <returns>
	///   <see langword="false" />, if access to the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> is not synchronized (thread safe); otherwise, <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	bool ICollection.IsSynchronized
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
	/// <returns>An object to use to synchronize access to the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" />.</returns>
	[System.MonoNotSupported("")]
	object ICollection.SyncRoot
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
	/// <returns>
	///   <see langword="false" />, if the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> dynamically increases as new objects are added; otherwise, <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	bool IList.IsFixedSize
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
	/// <returns>
	///   <see langword="false" />, if the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> can be added, modified, and removed; otherwise, <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	bool IList.IsReadOnly
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see the <see cref="T:System.Collections.IList" /> class.</summary>
	/// <param name="index">The zero-based index of the object to get in the collection.</param>
	/// <returns>The object at <paramref name="index" /> in the collection.</returns>
	[System.MonoNotSupported("")]
	object IList.this[int index]
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> class.</summary>
	[System.MonoNotSupported("")]
	public TemplateGroupCollection()
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object to the end of the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to add to the collection.</param>
	/// <returns>The index at which the <see cref="T:System.Web.UI.Design.TemplateGroup" /> was added to the collection.</returns>
	[System.MonoNotSupported("")]
	public int Add(TemplateGroup group)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the template groups in an existing <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> object to the current <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> object.</summary>
	/// <param name="groups">A <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> that contains the groups to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="groups" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public void AddRange(TemplateGroupCollection groups)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes all groups from the collection.</summary>
	[System.MonoNotSupported("")]
	public void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the specified group is contained within the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="group" /> is in the collection; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool Contains(TemplateGroup group)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies the groups in the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied groups. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.  
	/// -or-  
	/// <paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.  
	/// -or-  
	/// The number of elements in the source <see cref="T:System.Web.UI.Design.TemplateGroupCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public void CopyTo(TemplateGroup[] array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object within the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="group" /> within the collection; otherwise, -1, if <paramref name="group" /> is not in the collection.</returns>
	[System.MonoNotSupported("")]
	public int IndexOf(TemplateGroup group)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.Design.TemplateGroup" /> object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert <paramref name="group" />.</param>
	/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to insert into the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public void Insert(int index, TemplateGroup group)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.Design.TemplateGroup" /> object from the collection.</summary>
	/// <param name="group">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to remove from the collection.</param>
	[System.MonoNotSupported("")]
	public void Remove(TemplateGroup group)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.Design.TemplateGroup" /> object at the specified index within the collection.</summary>
	/// <param name="index">The zero-based index within the collection of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.TemplateGroupCollection.Count" /> property.</exception>
	[System.MonoNotSupported("")]
	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that is the destination of the copied groups. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	[System.MonoNotSupported("")]
	void ICollection.CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> to use to iterate through the collection.</returns>
	[System.MonoNotSupported("")]
	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to add to the collection.</param>
	/// <returns>The index at which <paramref name="o" /> was added to the collection.</returns>
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
	/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
	/// <returns>
	///   <see langword="true" />, if <paramref name="o" /> is in the collection; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	bool IList.Contains(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
	/// <param name="o">The <see cref="T:System.Web.UI.Design.TemplateGroup" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="o" /> within the collection; otherwise, -1, if <paramref name="o" /> is not in the collection.</returns>
	[System.MonoNotSupported("")]
	int IList.IndexOf(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert <paramref name="o" />.</param>
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
