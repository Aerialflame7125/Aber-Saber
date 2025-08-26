using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design;

/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
[ComVisible(true)]
public class DesignerActionListCollection : CollectionBase
{
	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element.</param>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> at the specified index.</returns>
	public DesignerActionList this[int index]
	{
		get
		{
			return (DesignerActionList)base.List[index];
		}
		set
		{
			base.List[index] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> class with default settings.</summary>
	public DesignerActionListCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> class with the specified panel items.</summary>
	/// <param name="value">The array of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects to populate the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	public DesignerActionListCollection(DesignerActionList[] value)
	{
		AddRange(value);
	}

	/// <summary>Adds the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to the current collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to add.</param>
	/// <returns>The position into which the new element is inserted into the collection's internal list.</returns>
	public int Add(DesignerActionList value)
	{
		return base.List.Add(value);
	}

	/// <summary>Adds the elements of the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> array to the end of the current collection.</summary>
	/// <param name="value">The array of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	public void AddRange(DesignerActionList[] value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		foreach (DesignerActionList value2 in value)
		{
			Add(value2);
		}
	}

	/// <summary>Adds the elements of the supplied <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to the end of the current collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is <see langword="null" />.</exception>
	public void AddRange(DesignerActionListCollection value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		foreach (DesignerActionList item in value)
		{
			Add(item);
		}
	}

	/// <summary>Indicates whether the collection contains a specific value.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to search for.</param>
	/// <returns>
	///   <see langword="true" /> if the collection contains the specified value; otherwise, <see langword="false" />.</returns>
	public bool Contains(DesignerActionList value)
	{
		return base.List.Contains(value);
	}

	/// <summary>Copies the elements of the current collection into the supplied array, starting at the specified array index.</summary>
	/// <param name="array">The one-dimensional array of type <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that is the destination of the elements copied from the current collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.  
	/// -or-  
	/// The number of elements in the current collection is greater than the available space from <paramref name="index" /> to the end of the destination array.</exception>
	/// <exception cref="T:System.InvalidCastException">A problem occurred casting the elements of the current collection to the type of the destination array, perhaps as the result of a failed downcast.</exception>
	public void CopyTo(DesignerActionList[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Determines the index of a specific item in the collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to locate in the collection.</param>
	/// <returns>The index of <paramref name="value" /> if found in the internal list; otherwise, -1.</returns>
	public int IndexOf(DesignerActionList value)
	{
		return base.List.IndexOf(value);
	}

	/// <summary>Inserts the supplied <see cref="T:System.ComponentModel.Design.DesignerActionList" /> into the collection at the specified position.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to insert into the collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0 or greater than the count of elements in the current collection.</exception>
	public void Insert(int index, DesignerActionList value)
	{
		base.List.Insert(index, value);
	}

	/// <summary>Removes the first occurrence of a specific <see cref="T:System.ComponentModel.Design.DesignerActionList" /> from the collection.</summary>
	/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to remove from the current collection.</param>
	public void Remove(DesignerActionList value)
	{
		base.List.Remove(value);
	}

	/// <summary>Performs additional custom processes when clearing the contents of the <see cref="T:System.Collections.CollectionBase" /> instance.</summary>
	protected override void OnClear()
	{
	}

	/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
	/// <param name="index">The zero-based index at which to insert <paramref name="value" />.</param>
	/// <param name="value">The new value of the element at <paramref name="index" />.</param>
	protected override void OnInsert(int index, object value)
	{
	}

	/// <summary>Performs additional custom processes when removing an element from the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> can be found.</param>
	/// <param name="value">The value of the element to remove from <paramref name="index" />.</param>
	protected override void OnRemove(int index, object value)
	{
	}

	/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> instance.</summary>
	/// <param name="index">The zero-based index at which <paramref name="oldValue" /> can be found.</param>
	/// <param name="oldValue">The value to replace with <paramref name="newValue" />.</param>
	/// <param name="newValue">The new value of the element at <paramref name="index" />.</param>
	protected override void OnSet(int index, object oldValue, object newValue)
	{
	}

	/// <summary>Performs additional custom processes when validating a value.</summary>
	/// <param name="value">The object to validate.</param>
	protected override void OnValidate(object value)
	{
	}
}
