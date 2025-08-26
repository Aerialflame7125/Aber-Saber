using System.Collections;
using System.Drawing;

namespace System.Web.UI.Design;

/// <summary>Represents a collection of <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> objects within a control designer. This class cannot be inherited.</summary>
public sealed class DesignerAutoFormatCollection : IList, ICollection, IEnumerable
{
	/// <summary>Gets the number of <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> objects in the collection.</returns>
	[System.MonoTODO]
	public int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object at the specified index in the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to get or set in the collection.</param>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> at the specified index in the collection.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerAutoFormatCollection.Count" /> property.</exception>
	[System.MonoTODO]
	public DesignerAutoFormat this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the maximum outer dimensions of the control as it will appear at run time.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> structure that contains the height and width of the control on the design surface.</returns>
	[System.MonoTODO]
	public Size PreviewSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" />.</returns>
	[System.MonoTODO]
	public object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of elements that are contained in the collection when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.ICollection" /> interface.</summary>
	/// <returns>The number of items in the collection.</returns>
	[System.MonoTODO]
	int ICollection.Count => Count;

	/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe) when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.ICollection" /> interface.</summary>
	/// <returns>
	///   <see langword="true" />, if the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> is synchronized; otherwise <see langword="false" />.</returns>
	[System.MonoTODO]
	bool ICollection.IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the collection has a fixed size when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <returns>Always <see langword="false" />.</returns>
	[System.MonoTODO]
	bool IList.IsFixedSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this method, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
	/// <returns>Always <see langword="true" />, which indicates that the collection cannot be replaced or deleted.</returns>
	[System.MonoTODO]
	bool IList.IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the element at the specified index when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="index">The index.</param>
	/// <returns>The element at the specified index.</returns>
	[System.MonoTODO]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> class.</summary>
	public DesignerAutoFormatCollection()
	{
	}

	/// <summary>Adds the specified <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object to the end of the collection.</summary>
	/// <param name="format">An instance of <see cref="T:System.Web.UI.Design.DesignerAutoFormat" />.</param>
	/// <returns>The index at which the format was added to the collection.</returns>
	[System.MonoTODO]
	public int Add(DesignerAutoFormat format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes all formats from the collection.</summary>
	[System.MonoTODO]
	public void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the specified format is contained within the collection.</summary>
	/// <param name="format">An instance of <see cref="T:System.Web.UI.Design.DesignerAutoFormat" />.</param>
	/// <returns>
	///   <see langword="true" />, if the specified format is in the collection; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool Contains(DesignerAutoFormat format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the index of the specified <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object within the collection.</summary>
	/// <param name="format">The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to locate within the collection.</param>
	/// <returns>The zero-based index of the first occurrence of <paramref name="format" /> within the collection; otherwise, -1, if the format is not in the collection.</returns>
	[System.MonoTODO]
	public int IndexOf(DesignerAutoFormat format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts a <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index within the collection at which to insert <paramref name="format" />.</param>
	/// <param name="format">The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to insert into the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerAutoFormatCollection.Count" /> property.</exception>
	[System.MonoTODO]
	public void Insert(int index, DesignerAutoFormat format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the specified <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object from the collection.</summary>
	/// <param name="format">The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to remove from the collection.</param>
	[System.MonoTODO]
	public void Remove(DesignerAutoFormat format)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> object at the specified index within the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to remove from the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.  
	/// -or-
	///  <paramref name="index" /> is greater than the <see cref="P:System.Web.UI.Design.DesignerAutoFormatCollection.Count" /> property.</exception>
	[System.MonoTODO]
	public void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" /> object, starting at a particular <see cref="T:System.Array" /> index when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.ICollection" /> interface.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> to receive the designated items.</param>
	/// <param name="index">The starting index for the items to copy.</param>
	[System.MonoTODO]
	void ICollection.CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> interface that iterates through the collection when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IEnumerable" /> interface.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" />.</returns>
	[System.MonoTODO]
	IEnumerator IEnumerable.GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an item to the collection when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="value">A <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to add to the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" />.</param>
	/// <returns>The index of the added item.</returns>
	[System.MonoTODO]
	int IList.Add(object item)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines whether the collection contains a specific value when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="value">A <see cref="T:System.Web.UI.Design.DesignerAutoFormat" />.</param>
	/// <returns>
	///   <see langword="true" />, if the object is in the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	bool IList.Contains(object item)
	{
		throw new NotImplementedException();
	}

	/// <summary>Determines the index of a specific item in the collection when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="value">The value.</param>
	/// <returns>The index of a item in the collection.</returns>
	[System.MonoTODO]
	int IList.IndexOf(object item)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts an item into the collection at the specified index when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="index">The index at which to insert <paramref name="value" />.</param>
	/// <param name="value">A <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to insert.</param>
	[System.MonoTODO]
	void IList.Insert(int index, object item)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the first occurrence of a specific object from the collection when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="value">The <see cref="T:System.Web.UI.Design.DesignerAutoFormat" /> to remove.</param>
	[System.MonoTODO]
	void IList.Remove(object item)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the item at the specified index when the <see cref="T:System.Web.UI.Design.DesignerAutoFormatCollection" /> object is cast to an <see cref="T:System.Collections.IList" /> interface.</summary>
	/// <param name="index">The index of the item to remove.</param>
	[System.MonoTODO]
	void IList.RemoveAt(int index)
	{
		throw new NotImplementedException();
	}
}
