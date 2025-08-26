using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class GridTableStylesCollection : BaseCollection, ICollection, IEnumerable, IList
{
	private ArrayList items;

	private DataGrid owner;

	/// <summary>Gets the number of items in the collection.</summary>
	/// <returns>The number of items contained in the collection.</returns>
	int ICollection.Count => items.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>The <see cref="T:System.Object" /> used to synchronize access to the collection.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The zero-based index of the element.</param>
	/// <exception cref="T:System.NotSupportedException">The item property cannot be set.</exception>
	object IList.this[int index]
	{
		get
		{
			return items[index];
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified name.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> with the specified <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" />.</returns>
	/// <param name="tableName">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to retrieve. </param>
	/// <filterpriority>1</filterpriority>
	public DataGridTableStyle this[string tableName]
	{
		get
		{
			int num = FromTableNameToIndex(tableName);
			return (num != -1) ? this[num] : null;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</returns>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to get. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">No item exists at the specified index. </exception>
	/// <filterpriority>1</filterpriority>
	public DataGridTableStyle this[int index] => (DataGridTableStyle)items[index];

	/// <summary>Gets the underlying list.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the table data.</returns>
	protected override ArrayList List => items;

	/// <summary>Occurs when the collection has changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	internal GridTableStylesCollection(DataGrid grid)
	{
		items = new ArrayList();
		owner = grid;
	}

	/// <summary>Copies the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
	/// <param name="index">The zero-based index in the array at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> is greater than the available space from index to the end of the destination array.</exception>
	/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of the destination array.</exception>
	void ICollection.CopyTo(Array array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator for the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
	/// <returns>The index of the newly added object.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> has already been assigned to a <see cref="T:System.Windows.Forms.GridTableStylesCollection" />.-or-A <see cref="T:System.Windows.Forms.DataGridTableStyle" /> in <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> has the same <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> property value as <paramref name="value" />.</exception>
	int IList.Add(object value)
	{
		return Add((DataGridTableStyle)value);
	}

	/// <summary>Clears the collection.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines whether an element is in the collection.</summary>
	/// <returns>true if value is found in the collection; otherwise, false.</returns>
	/// <param name="value">The object to locate in the collection. The value can be null.</param>
	bool IList.Contains(object value)
	{
		return Contains((DataGridTableStyle)value);
	}

	/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
	/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
	/// <param name="value">The object to locate in the collection. The value can be null.</param>
	int IList.IndexOf(object value)
	{
		return items.IndexOf(value);
	}

	/// <summary>Implements the <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" /> method. Always throws <see cref="T:System.NotSupportedException" />.</summary>
	/// <param name="index">The zero-based index at which value should be inserted.</param>
	/// <param name="value">The object to insert into the collection.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove from the collection.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</exception>
	void IList.Remove(object value)
	{
		Remove((DataGridTableStyle)value);
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove.</param>
	void IList.RemoveAt(int index)
	{
		RemoveAt(index);
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to this collection.</summary>
	/// <returns>The index of the newly added object.</returns>
	/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to add to the collection. </param>
	/// <filterpriority>1</filterpriority>
	public virtual int Add(DataGridTableStyle table)
	{
		int result = AddInternal(table);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
		return result;
	}

	/// <summary>Adds an array of table styles to the collection.</summary>
	/// <param name="tables">An array of <see cref="T:System.Windows.Forms.DataGridTableStyle" /> objects. </param>
	/// <filterpriority>1</filterpriority>
	public virtual void AddRange(DataGridTableStyle[] tables)
	{
		foreach (DataGridTableStyle table in tables)
		{
			AddInternal(table);
		}
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Clears the collection.</summary>
	/// <filterpriority>1</filterpriority>
	public void Clear()
	{
		items.Clear();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
	/// <returns>true if the specified table style exists in the collection; otherwise, false.</returns>
	/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(DataGridTableStyle table)
	{
		return FromTableNameToIndex(table.MappingName) != -1;
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridTableStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> specified by name.</summary>
	/// <returns>true if the specified table style exists in the collection; otherwise, false.</returns>
	/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to look for. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(string name)
	{
		return FromTableNameToIndex(name) != -1;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.GridTableStylesCollection.CollectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> containing the event data. </param>
	protected void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
	/// <param name="table">The <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	public void Remove(DataGridTableStyle table)
	{
		items.Remove(table);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
	}

	private void MappingNameChanged(object sender, EventArgs args)
	{
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Removes a <see cref="T:System.Windows.Forms.DataGridTableStyle" /> at the specified index.</summary>
	/// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	public void RemoveAt(int index)
	{
		DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)items[index];
		items.RemoveAt(index);
		dataGridTableStyle.MappingNameChanged -= MappingNameChanged;
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridTableStyle));
	}

	private int AddInternal(DataGridTableStyle table)
	{
		if (FromTableNameToIndex(table.MappingName) != -1)
		{
			throw new ArgumentException("The TableStyles collection already has a TableStyle with this mapping name");
		}
		table.MappingNameChanged += MappingNameChanged;
		table.DataGrid = owner;
		return items.Add(table);
	}

	private int FromTableNameToIndex(string tableName)
	{
		for (int i = 0; i < items.Count; i++)
		{
			DataGridTableStyle dataGridTableStyle = (DataGridTableStyle)items[i];
			if (string.Compare(dataGridTableStyle.MappingName, tableName, ignoreCase: true) == 0)
			{
				return i;
			}
		}
		return -1;
	}
}
