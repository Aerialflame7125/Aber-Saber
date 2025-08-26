using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
[Editor("System.Windows.Forms.Design.DataGridColumnCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public class GridColumnStylesCollection : BaseCollection, ICollection, IEnumerable, IList
{
	private ArrayList items;

	private DataGridTableStyle owner;

	private bool fire_event;

	/// <summary>Gets the number of elements contained in the collection.</summary>
	/// <returns>The number of elements contained in the collection.</returns>
	int ICollection.Count => items.Count;

	/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <returns>The object used to synchronize access to the collection.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets the element at the specified index.</summary>
	/// <returns>The element at the specified index.</returns>
	/// <param name="index">The zero-based index of the element to get.</param>
	/// <exception cref="T:System.NotSupportedException">An operation attempts to set this property.</exception>
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

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified column header.</returns>
	/// <param name="columnName">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to retrieve. </param>
	/// <filterpriority>1</filterpriority>
	public DataGridColumnStyle this[string columnName]
	{
		get
		{
			int num = FromColumnNameToIndex(columnName);
			return (num != -1) ? this[num] : null;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at a specified index.</summary>
	/// <returns>The specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to return. </param>
	/// <filterpriority>1</filterpriority>
	public DataGridColumnStyle this[int index] => (DataGridColumnStyle)items[index];

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
	/// <param name="propertyDesciptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <filterpriority>1</filterpriority>
	public DataGridColumnStyle this[PropertyDescriptor propertyDesciptor]
	{
		get
		{
			for (int i = 0; i < items.Count; i++)
			{
				DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)items[i];
				if (dataGridColumnStyle.PropertyDescriptor.Equals(propertyDesciptor))
				{
					return dataGridColumnStyle;
				}
			}
			return null;
		}
	}

	/// <summary>Gets the list of items in the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the collection items.</returns>
	protected override ArrayList List => items;

	internal bool FireEvents
	{
		get
		{
			return fire_event;
		}
		set
		{
			fire_event = value;
		}
	}

	/// <summary>Occurs when a change is made to the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	internal GridColumnStylesCollection(DataGridTableStyle tablestyle)
	{
		items = new ArrayList();
		owner = tablestyle;
		fire_event = true;
	}

	/// <summary>Copies the collection to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The array must have zero-based indexing.  </param>
	/// <param name="index">The zero-based index in the array at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The type of the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> cannot be cast automatically to the type of <paramref name="array" />.</exception>
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

	/// <summary>Adds an object to the collection.</summary>
	/// <returns>The index at which the value has been added.</returns>
	/// <param name="value">The object to be added to the collection. The value can be null.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
	int IList.Add(object value)
	{
		return Add((DataGridColumnStyle)value);
	}

	/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
	void IList.Clear()
	{
		Clear();
	}

	/// <summary>Determines whether an element is in the collection.</summary>
	/// <returns>true if the element is in the collection; otherwise, false.</returns>
	/// <param name="value">The object to locate in the collection. The value can be null.</param>
	bool IList.Contains(object value)
	{
		return Contains((DataGridColumnStyle)value);
	}

	/// <summary>Returns the zero-based index of the first occurrence of the specified object in the collection.</summary>
	/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter within the collection, if found; otherwise, -1.</returns>
	/// <param name="value">The object to locate in the collection. The value can be null.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf((DataGridColumnStyle)value);
	}

	/// <summary>This method is not supported by this control.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The object to insert into the collection.</param>
	/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
	void IList.Insert(int index, object value)
	{
		throw new NotSupportedException();
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> cannot be cast to a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</exception>
	void IList.Remove(object value)
	{
		Remove((DataGridColumnStyle)value);
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> at the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove.</param>
	void IList.RemoveAt(int index)
	{
		RemoveAt(index);
	}

	/// <summary>Adds a column style to the collection.</summary>
	/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
	/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to add. </param>
	/// <filterpriority>1</filterpriority>
	public virtual int Add(DataGridColumnStyle column)
	{
		if (FromColumnNameToIndex(column.MappingName) != -1)
		{
			throw new ArgumentException("The ColumnStyles collection already has a column with this mapping name");
		}
		column.TableStyle = owner;
		column.SetDataGridInternal(owner.DataGrid);
		ConnectColumnEvents(column);
		int result = items.Add(column);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		return result;
	}

	/// <summary>Adds an array of column style objects to the collection.</summary>
	/// <param name="columns">An array of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects to add to the collection. </param>
	/// <filterpriority>1</filterpriority>
	public void AddRange(DataGridColumnStyle[] columns)
	{
		foreach (DataGridColumnStyle column in columns)
		{
			Add(column);
		}
	}

	/// <summary>Clears the collection of <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	public void Clear()
	{
		items.Clear();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
	/// <param name="column">The desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(DataGridColumnStyle column)
	{
		return FromColumnNameToIndex(column.MappingName) != -1;
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> associated with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
	/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
	/// <param name="propertyDescriptor">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(PropertyDescriptor propertyDescriptor)
	{
		return this[propertyDescriptor] != null;
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified name.</summary>
	/// <returns>true if the collection contains the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />; otherwise, false.</returns>
	/// <param name="name">The <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> of the desired <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <filterpriority>1</filterpriority>
	public bool Contains(string name)
	{
		return FromColumnNameToIndex(name) != -1;
	}

	/// <summary>Gets the index of a specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	/// <returns>The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> within the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> or -1 if no corresponding <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> exists.</returns>
	/// <param name="element">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to find. </param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(DataGridColumnStyle element)
	{
		return items.IndexOf(element);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.GridColumnStylesCollection.CollectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data event. </param>
	protected void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		if (fire_event && this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <param name="column">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove from the collection. </param>
	/// <filterpriority>1</filterpriority>
	public void Remove(DataGridColumnStyle column)
	{
		items.Remove(column);
		DisconnectColumnEvents(column);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, column));
	}

	/// <summary>Removes the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified index from the <see cref="T:System.Windows.Forms.GridColumnStylesCollection" />.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to remove. </param>
	/// <filterpriority>1</filterpriority>
	public void RemoveAt(int index)
	{
		DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)items[index];
		items.RemoveAt(index);
		DisconnectColumnEvents(dataGridColumnStyle);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridColumnStyle));
	}

	/// <summary>Sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for each column style in the collection to null.</summary>
	/// <filterpriority>1</filterpriority>
	public void ResetPropertyDescriptors()
	{
		for (int i = 0; i < items.Count; i++)
		{
			DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)items[i];
			if (dataGridColumnStyle.PropertyDescriptor != null)
			{
				dataGridColumnStyle.PropertyDescriptor = null;
			}
		}
	}

	private void ConnectColumnEvents(DataGridColumnStyle col)
	{
		col.AlignmentChanged += ColumnAlignmentChangedEvent;
		col.FontChanged += ColumnFontChangedEvent;
		col.HeaderTextChanged += ColumnHeaderTextChanged;
		col.MappingNameChanged += ColumnMappingNameChangedEvent;
		col.NullTextChanged += ColumnNullTextChangedEvent;
		col.PropertyDescriptorChanged += ColumnPropertyDescriptorChanged;
		col.ReadOnlyChanged += ColumnReadOnlyChangedEvent;
		col.WidthChanged += ColumnWidthChangedEvent;
	}

	private void DisconnectColumnEvents(DataGridColumnStyle col)
	{
		col.AlignmentChanged -= ColumnAlignmentChangedEvent;
		col.FontChanged -= ColumnFontChangedEvent;
		col.HeaderTextChanged -= ColumnHeaderTextChanged;
		col.MappingNameChanged -= ColumnMappingNameChangedEvent;
		col.NullTextChanged -= ColumnNullTextChangedEvent;
		col.PropertyDescriptorChanged -= ColumnPropertyDescriptorChanged;
		col.ReadOnlyChanged -= ColumnReadOnlyChangedEvent;
		col.WidthChanged -= ColumnWidthChangedEvent;
	}

	private void ColumnAlignmentChangedEvent(object sender, EventArgs e)
	{
	}

	private void ColumnFontChangedEvent(object sender, EventArgs e)
	{
	}

	private void ColumnHeaderTextChanged(object sender, EventArgs e)
	{
	}

	private void ColumnMappingNameChangedEvent(object sender, EventArgs e)
	{
	}

	private void ColumnNullTextChangedEvent(object sender, EventArgs e)
	{
	}

	private void ColumnPropertyDescriptorChanged(object sender, EventArgs e)
	{
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender));
	}

	private void ColumnReadOnlyChangedEvent(object sender, EventArgs e)
	{
	}

	private void ColumnWidthChangedEvent(object sender, EventArgs e)
	{
	}

	private int FromColumnNameToIndex(string columnName)
	{
		for (int i = 0; i < items.Count; i++)
		{
			DataGridColumnStyle dataGridColumnStyle = (DataGridColumnStyle)items[i];
			if (dataGridColumnStyle.MappingName != null && !(dataGridColumnStyle.MappingName == string.Empty) && string.Compare(dataGridColumnStyle.MappingName, columnName, ignoreCase: true) == 0)
			{
				return i;
			}
		}
		return -1;
	}
}
