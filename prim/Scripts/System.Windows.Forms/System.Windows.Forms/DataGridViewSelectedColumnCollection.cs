using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class DataGridViewSelectedColumnCollection : BaseCollection, ICollection, IEnumerable, IList
{
	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => base.List.IsFixedSize;

	/// <summary>Gets the element at the specified index.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The index of the element to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			throw new NotSupportedException("Can't insert or modify this collection.");
		}
	}

	/// <summary>Gets the column at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of columns in the collection.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewColumn this[int index] => (DataGridViewColumn)base.List[index];

	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
	protected override ArrayList List => base.List;

	internal DataGridViewSelectedColumnCollection()
	{
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <returns>Not applicable. Always throws an exception.</returns>
	/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	int IList.Add(object value)
	{
		throw new NotSupportedException("Can't add elements to this collection.");
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines whether the specified value is contained in the collection.</summary>
	/// <returns>true if the <paramref name="value" /> parameter is in the collection; otherwise, false.</returns>
	/// <param name="value">An object to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	bool IList.Contains(object value)
	{
		return Contains(value as DataGridViewColumn);
	}

	/// <summary>Returns the index of the specified element.</summary>
	/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
	/// <param name="value">The element to locate in the collection.</param>
	int IList.IndexOf(object value)
	{
		return base.List.IndexOf(value);
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Insert(int index, object value)
	{
		Insert(index, value as DataGridViewColumn);
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Remove(object value)
	{
		throw new NotSupportedException("Can't remove elements of this collection.");
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException("Can't remove elements of this collection.");
	}

	/// <summary>Clears the collection.</summary>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Clear()
	{
		throw new NotSupportedException("This collection cannot be cleared.");
	}

	/// <summary>Determines whether the specified column is contained in the collection.</summary>
	/// <returns>true if the <paramref name="dataGridViewColumn" /> parameter is in the collection; otherwise, false.</returns>
	/// <param name="dataGridViewColumn">A <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(DataGridViewColumn dataGridViewColumn)
	{
		return base.List.Contains(dataGridViewColumn);
	}

	/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
	/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in the array at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(DataGridViewColumn[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Inserts a column into the collection at the specified position.</summary>
	/// <param name="index">The zero-based index at which the column should be inserted. </param>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Insert(int index, DataGridViewColumn dataGridViewColumn)
	{
		throw new NotSupportedException("Insert is not allowed.");
	}

	internal void InternalAdd(DataGridViewColumn dataGridViewColumn)
	{
		base.List.Add(dataGridViewColumn);
	}

	internal void InternalAddRange(DataGridViewSelectedColumnCollection columns)
	{
		if (columns != null)
		{
			for (int num = columns.Count - 1; num >= 0; num--)
			{
				base.List.Add(columns[num]);
			}
		}
	}

	internal void InternalClear()
	{
		List.Clear();
	}

	internal void InternalRemove(DataGridViewColumn dataGridViewColumn)
	{
		base.List.Remove(dataGridViewColumn);
	}

	virtual bool IList.get_IsReadOnly()
	{
		return base.IsReadOnly;
	}
}
