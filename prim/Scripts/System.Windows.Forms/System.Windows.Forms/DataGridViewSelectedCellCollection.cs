using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of cells that are selected in a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class DataGridViewSelectedCellCollection : BaseCollection, ICollection, IEnumerable, IList
{
	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => base.List.IsFixedSize;

	/// <summary>Gets the element at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">The property is set.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets the cell at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCell this[int index] => (DataGridViewCell)base.List[index];

	/// <summary>Gets a list of elements in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection.</returns>
	protected override ArrayList List => base.List;

	internal DataGridViewSelectedCellCollection()
	{
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Add(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <returns>The position into which the new element was inserted.</returns>
	/// <param name="value">The item to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	int IList.Add(object value)
	{
		throw new NotSupportedException();
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Clear" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines whether the specified cell is contained in the collection.</summary>
	/// <returns>true if <paramref name="value" /> is in the collection; otherwise, false.</returns>
	/// <param name="value">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	bool IList.Contains(object value)
	{
		return Contains(value as DataGridViewCell);
	}

	/// <summary>Returns the index of the specified cell.</summary>
	/// <returns>The zero-based index of the <paramref name="value" /> parameter if it is found in the collection; otherwise, -1.</returns>
	/// <param name="value">The cell to locate in the collection.</param>
	int IList.IndexOf(object value)
	{
		return base.List.IndexOf(value as DataGridViewCell);
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="index">The index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Insert(int index, object value)
	{
		Insert(index, value as DataGridViewCell);
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Remove(System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="value">The object to be removed from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Remove(object value)
	{
		throw new NotSupportedException("Can't remove elements of selected cell base.List.");
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="index">The zero-based index of the item to remove.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.RemoveAt(int index)
	{
		throw new NotSupportedException("Can't remove elements of selected cell base.List.");
	}

	/// <summary>Clears the collection. </summary>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Clear()
	{
		throw new NotSupportedException("Cannot clear this base.List");
	}

	/// <summary>Determines whether the specified cell is contained in the collection.</summary>
	/// <returns>true if <paramref name="dataGridViewCell" /> is in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />; otherwise, false.</returns>
	/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(DataGridViewCell dataGridViewCell)
	{
		return base.List.Contains(dataGridViewCell);
	}

	/// <summary>Copies the elements of the collection to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" /> array, starting at the specified index.</summary>
	/// <param name="array">The one-dimensional array of type <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(DataGridViewCell[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Inserts a cell into the collection.</summary>
	/// <param name="index">The index at which <paramref name="dataGridViewCell" /> should be inserted.</param>
	/// <param name="dataGridViewCell">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection" />.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Insert(int index, DataGridViewCell dataGridViewCell)
	{
		throw new NotSupportedException("Can't insert to selected cell base.List");
	}

	internal void InternalAdd(DataGridViewCell dataGridViewCell)
	{
		base.List.Add(dataGridViewCell);
	}

	internal void InternalRemove(DataGridViewCell dataGridViewCell)
	{
		base.List.Remove(dataGridViewCell);
	}

	virtual bool IList.get_IsReadOnly()
	{
		return base.IsReadOnly;
	}
}
