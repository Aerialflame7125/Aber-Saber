using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of cells in a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class DataGridViewCellCollection : BaseCollection, ICollection, IEnumerable, IList
{
	private DataGridViewRow dataGridViewRow;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => base.List.IsFixedSize;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified index.</returns>
	/// <param name="index">The index of the item to get or set.</param>
	/// <exception cref="T:System.InvalidCastException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
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
			this[index] = value as DataGridViewCell;
		}
	}

	/// <summary>Gets or sets the cell at the provided index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored at the given index.</returns>
	/// <param name="index">The zero-based index of the cell to get or set.</param>
	/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.-or-<paramref name="index" /> is equal to or greater than the number of cells in the collection.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCell this[int index]
	{
		get
		{
			return (DataGridViewCell)base.List[index];
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Insert(index, value);
		}
	}

	/// <summary>Gets or sets the cell in the column with the provided name. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> stored in the column with the given name.</returns>
	/// <param name="columnName">The name of the column in which to get or set the cell.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="columnName" /> does not match the name of any columns in the control.</exception>
	/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCell this[string columnName]
	{
		get
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			foreach (DataGridViewCell item in base.List)
			{
				if (string.Compare(item.OwningColumn.Name, columnName, ignoreCase: true) == 0)
				{
					return item;
				}
			}
			throw new ArgumentException($"Column name {columnName} cannot be found.", "columnName");
		}
		set
		{
			if (columnName == null)
			{
				throw new ArgumentNullException("columnName");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < base.List.Count; i++)
			{
				DataGridViewCell dataGridViewCell = (DataGridViewCell)base.List[i];
				if (string.Compare(dataGridViewCell.OwningColumn.Name, columnName, ignoreCase: true) == 0)
				{
					Insert(i, value);
					return;
				}
			}
			Add(value);
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> containing <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> objects.</summary>
	/// <returns>
	///   <see cref="T:System.Collections.ArrayList" />.</returns>
	protected override ArrayList List => base.List;

	/// <summary>Occurs when the collection is changed. </summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> class.</summary>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the collection.</param>
	public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
	{
		this.dataGridViewRow = dataGridViewRow;
	}

	/// <summary>Adds an item to the collection.</summary>
	/// <returns>The position into which the new element was inserted.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="value" /> represents a cell that already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	int IList.Add(object value)
	{
		return Add(value as DataGridViewCell);
	}

	/// <summary>Determines whether the collection contains the specified value.</summary>
	/// <returns>true if the <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />; otherwise, false.</returns>
	/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
	bool IList.Contains(object value)
	{
		return Contains(value as DataGridViewCell);
	}

	/// <summary>Determines the index of a specific item in a collection.</summary>
	/// <returns>The index of value if found in the list; otherwise, -1.</returns>
	/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf(value as DataGridViewCell);
	}

	/// <summary>Inserts an item into the collection at the specified position.</summary>
	/// <param name="index">The zero-based index at which value should be inserted. </param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert into the <see cref="M:System.Windows.Forms.DataGridViewCellCollection.System#Collections#IList#Insert(System.Int32,System.Object)" />.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	void IList.Insert(int index, object value)
	{
		Insert(index, value as DataGridViewCell);
	}

	/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="cell" /> could not be found in the collection.</exception>
	void IList.Remove(object value)
	{
		Remove(value as DataGridViewCell);
	}

	internal DataGridViewCell GetCellInternal(int colIndex)
	{
		return (DataGridViewCell)base.List[colIndex];
	}

	internal DataGridViewCell GetBoundCell(string dataPropertyName)
	{
		foreach (DataGridViewCell item in base.List)
		{
			if (string.Compare(item.OwningColumn.DataPropertyName, dataPropertyName, ignoreCase: true) == 0)
			{
				return item;
			}
		}
		return null;
	}

	/// <summary>Adds a cell to the collection.</summary>
	/// <returns>The position in which to insert the new element.</returns>
	/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to add to the collection.</param>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual int Add(DataGridViewCell dataGridViewCell)
	{
		int num = base.List.Add(dataGridViewCell);
		dataGridViewCell.SetOwningRow(dataGridViewRow);
		dataGridViewCell.SetColumnIndex(num);
		dataGridViewCell.SetDataGridView(dataGridViewRow.DataGridView);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
		return num;
	}

	/// <summary>Adds an array of cells to the collection.</summary>
	/// <param name="dataGridViewCells">The array of <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewCells" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-At least one value in <paramref name="dataGridViewCells" /> is null.-or-At least one cell in <paramref name="dataGridViewCells" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.-or-At least two values in <paramref name="dataGridViewCells" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewCell" />.</exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
	{
		foreach (DataGridViewCell dataGridViewCell in dataGridViewCells)
		{
			Add(dataGridViewCell);
		}
	}

	/// <summary>Clears all cells from the collection.</summary>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void Clear()
	{
		base.List.Clear();
	}

	/// <summary>Determines whether the specified cell is contained in the collection.</summary>
	/// <returns>true if <paramref name="dataGridViewCell" /> is in the collection; otherwise, false.</returns>
	/// <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell" /> to locate in the collection.</param>
	/// <filterpriority>1</filterpriority>
	public virtual bool Contains(DataGridViewCell dataGridViewCell)
	{
		return base.List.Contains(dataGridViewCell);
	}

	/// <summary>Copies the entire collection of cells into an array at a specified location within the array.</summary>
	/// <param name="array">The destination array to which the contents will be copied.</param>
	/// <param name="index">The index of the element in <paramref name="array" /> at which to start copying.</param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(DataGridViewCell[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Returns the index of the specified cell.</summary>
	/// <returns>The zero-based index of the value of <paramref name="dataGridViewCell" /> parameter, if it is found in the collection; otherwise, -1.</returns>
	/// <param name="dataGridViewCell">The cell to locate in the collection.</param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(DataGridViewCell dataGridViewCell)
	{
		return base.List.IndexOf(dataGridViewCell);
	}

	/// <summary>Inserts a cell into the collection at the specified index. </summary>
	/// <param name="index">The zero-based index at which to place <paramref name="dataGridViewCell" />.</param>
	/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to insert.</param>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<paramref name="dataGridViewCell" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
	{
		base.List.Insert(index, dataGridViewCell);
		dataGridViewCell.SetOwningRow(dataGridViewRow);
		dataGridViewCell.SetColumnIndex(index);
		dataGridViewCell.SetDataGridView(dataGridViewRow.DataGridView);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewCell));
	}

	/// <summary>Removes the specified cell from the collection.</summary>
	/// <param name="cell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> to remove from the collection.</param>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="cell" /> could not be found in the collection.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Remove(DataGridViewCell cell)
	{
		base.List.Remove(cell);
		ReIndex();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, cell));
	}

	/// <summary>Removes the cell at the specified index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> to be removed.</param>
	/// <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveAt(int index)
	{
		DataGridViewCell element = this[index];
		base.List.RemoveAt(index);
		ReIndex();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, element));
	}

	private void ReIndex()
	{
		for (int i = 0; i < base.List.Count; i++)
		{
			this[i].SetColumnIndex(i);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewCellCollection.CollectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
	protected void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	virtual bool IList.get_IsReadOnly()
	{
		return base.IsReadOnly;
	}
}
