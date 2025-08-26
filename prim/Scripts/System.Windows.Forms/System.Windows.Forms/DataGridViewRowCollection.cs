using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms;

/// <summary>A collection of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[DesignerSerializer("System.Windows.Forms.Design.DataGridViewRowCollectionCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ListBindable(false)]
public class DataGridViewRowCollection : ICollection, IEnumerable, IList
{
	private class RowIndexComparator : IComparer
	{
		public int Compare(object o1, object o2)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)o1;
			DataGridViewRow dataGridViewRow2 = (DataGridViewRow)o2;
			if (dataGridViewRow.Index < dataGridViewRow2.Index)
			{
				return -1;
			}
			if (dataGridViewRow.Index > dataGridViewRow2.Index)
			{
				return 1;
			}
			return 0;
		}
	}

	private ArrayList list;

	private DataGridView dataGridView;

	private bool raiseEvent = true;

	/// <summary>Gets the number of elements contained in the collection.</summary>
	/// <returns>The number of elements contained in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
	int ICollection.Count => Count;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => list.IsFixedSize;

	/// <summary>Gets a value indicating whether the collection is read-only.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsReadOnly => list.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
	/// <returns>false in all cases.</returns>
	bool ICollection.IsSynchronized => list.IsSynchronized;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
	/// <param name="index">The zero-based index of the element to get or set.</param>
	/// <exception cref="T:System.NotSupportedException">The user tried to set this property.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			list[index] = value as DataGridViewRow;
		}
	}

	/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
	object ICollection.SyncRoot => list.SyncRoot;

	/// <summary>Gets the number of rows in the collection.</summary>
	/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int Count => list.Count;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index. Accessing a <see cref="T:System.Windows.Forms.DataGridViewRow" /> with this indexer causes the row to become unshared. To keep the row shared, use the <see cref="M:System.Windows.Forms.DataGridViewRowCollection.SharedRow(System.Int32)" /> method. For more information, see Best Practices for Scaling the Windows Forms DataGridView Control.</returns>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than 0.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DataGridViewRow this[int index]
	{
		get
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[index];
			if (dataGridViewRow.Index == -1)
			{
				dataGridViewRow = (DataGridViewRow)dataGridViewRow.Clone();
				dataGridViewRow.SetIndex(index);
				list[index] = dataGridViewRow;
			}
			return dataGridViewRow;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> that owns the collection.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</returns>
	protected DataGridView DataGridView => dataGridView;

	/// <summary>Gets an array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</summary>
	/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects.</returns>
	protected ArrayList List => list;

	internal ArrayList RowIndexSortedArrayList
	{
		get
		{
			ArrayList arrayList = (ArrayList)list.Clone();
			arrayList.Sort(new RowIndexComparator());
			return arrayList;
		}
	}

	/// <summary>Occurs when the contents of the collection change.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> class. </summary>
	/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	public DataGridViewRowCollection(DataGridView dataGridView)
	{
		this.dataGridView = dataGridView;
		list = new ArrayList();
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
	/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not null.-or-<paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-This operation would add a frozen row after unfrozen rows. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
	int IList.Add(object value)
	{
		return Add(value as DataGridViewRow);
	}

	/// <summary>Determines whether the collection contains the specified item.</summary>
	/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, false.</returns>
	/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	bool IList.Contains(object value)
	{
		return Contains(value as DataGridViewRow);
	}

	/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at the specified index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
	/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> cannot be cast automatically to the type of <paramref name="array" />. </exception>
	void ICollection.CopyTo(Array array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that iterates through the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return list.GetEnumerator();
	}

	/// <summary>Returns the index of a specified item in the collection.</summary>
	/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the list; otherwise, -1.</returns>
	/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf(value as DataGridViewRow);
	}

	/// <summary>Inserts a <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than the number of rows in the collection. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-<paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="value" /> is not null.-or-<paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> has more cells than there are columns in the control.</exception>
	void IList.Insert(int index, object value)
	{
		Insert(index, value as DataGridViewRow);
	}

	/// <summary>Removes the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not contained in this collection.-or-<paramref name="value" /> is a shared row.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="value" /> is the row for new records.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true. </exception>
	void IList.Remove(object value)
	{
		Remove(value as DataGridViewRow);
	}

	/// <summary>Adds a new row to the collection.</summary>
	/// <returns>The index of the new row.</returns>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-This operation would add a frozen row after unfrozen rows.</exception>
	/// <exception cref="T:System.ArgumentException">The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual int Add()
	{
		return Add(dataGridView.RowTemplateFull);
	}

	private int AddCore(DataGridViewRow dataGridViewRow, bool sharable)
	{
		if (dataGridView.Columns.Count == 0)
		{
			throw new InvalidOperationException("DataGridView has no columns.");
		}
		dataGridViewRow.SetDataGridView(dataGridView);
		int num = -1;
		if (DataGridView != null && DataGridView.EditingRow != null && DataGridView.EditingRow != dataGridViewRow)
		{
			num = list.Count - 1;
			DataGridView.EditingRow.SetIndex(list.Count);
		}
		int num2;
		if (num >= 0)
		{
			list.Insert(num, dataGridViewRow);
			num2 = num;
		}
		else
		{
			num2 = list.Add(dataGridViewRow);
		}
		if (sharable && CanBeShared(dataGridViewRow))
		{
			dataGridViewRow.SetIndex(-1);
		}
		else
		{
			dataGridViewRow.SetIndex(num2);
		}
		CompleteRowCells(dataGridViewRow);
		for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
		{
			dataGridViewRow.Cells[i].SetOwningColumn(dataGridView.Columns[i]);
		}
		if (raiseEvent)
		{
			OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow));
			DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num2, 1));
		}
		return num2;
	}

	private void CompleteRowCells(DataGridViewRow row)
	{
		if (row != null && DataGridView != null && row.Cells.Count < DataGridView.ColumnCount)
		{
			for (int i = row.Cells.Count; i < DataGridView.ColumnCount; i++)
			{
				row.Cells.Add((DataGridViewCell)DataGridView.Columns[i].CellTemplate.Clone());
			}
		}
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> to the collection.</summary>
	/// <returns>The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the <paramref name="dataGridViewRow" /> is not null.-or-<paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true. -or-This operation would add a frozen row after unfrozen rows.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRow" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int Add(DataGridViewRow dataGridViewRow)
	{
		if (dataGridView.DataSource != null)
		{
			throw new InvalidOperationException("DataSource of DataGridView is not null.");
		}
		return AddCore(dataGridViewRow, sharable: true);
	}

	private bool CanBeShared(DataGridViewRow row)
	{
		return false;
	}

	/// <summary>Adds the specified number of new rows to the collection.</summary>
	/// <returns>The index of the last row that was added.</returns>
	/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="count" /> is less than 1.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control. -or-This operation would add frozen rows after unfrozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual int Add(int count)
	{
		if (count <= 0)
		{
			throw new ArgumentOutOfRangeException("Count is less than or equeal to 0.");
		}
		if (dataGridView.DataSource != null)
		{
			throw new InvalidOperationException("DataSource of DataGridView is not null.");
		}
		if (dataGridView.Columns.Count == 0)
		{
			throw new InvalidOperationException("DataGridView has no columns.");
		}
		raiseEvent = false;
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			num = Add(dataGridView.RowTemplateFull);
		}
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num - count + 1, count));
		raiseEvent = true;
		return num;
	}

	/// <summary>Adds a new row to the collection, and populates the cells with the specified objects.</summary>
	/// <returns>The index of the new row.</returns>
	/// <param name="values">A variable number of objects that populate the cells of the new <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.- or -The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns. -or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.-or-This operation would add a frozen row after unfrozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual int Add(params object[] values)
	{
		if (values == null)
		{
			throw new ArgumentNullException("values is null.");
		}
		if (dataGridView.VirtualMode)
		{
			throw new InvalidOperationException("DataGridView is in virtual mode.");
		}
		DataGridViewRow rowTemplateFull = dataGridView.RowTemplateFull;
		int result = AddCore(rowTemplateFull, sharable: false);
		rowTemplateFull.SetValues(values);
		return result;
	}

	/// <summary>Adds the specified number of rows to the collection based on the row at the specified index.</summary>
	/// <returns>The index of the last row that was added.</returns>
	/// <param name="indexSource">The index of the row on which to base the new rows.</param>
	/// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the control.-or-<paramref name="count" /> is less than zero.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int AddCopies(int indexSource, int count)
	{
		raiseEvent = false;
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			num = AddCopy(indexSource);
		}
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num - count + 1, count));
		raiseEvent = true;
		return num;
	}

	/// <summary>Adds a new row based on the row at the specified index.</summary>
	/// <returns>The index of the new row.</returns>
	/// <param name="indexSource">The index of the row on which to base the new row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexSource" /> is less than zero or greater than or equal to the number of rows in the collection.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int AddCopy(int indexSource)
	{
		return Add((list[indexSource] as DataGridViewRow).Clone() as DataGridViewRow);
	}

	/// <summary>Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to the collection.</summary>
	/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to be added to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRows" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-At least one entry in the <paramref name="dataGridViewRows" /> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control.-or-This operation would add frozen rows after unfrozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
	{
		if (dataGridView.DataSource != null)
		{
			throw new InvalidOperationException("DataSource of DataGridView is not null.");
		}
		int num = 0;
		int num2 = -1;
		raiseEvent = false;
		foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
		{
			num2 = Add(dataGridViewRow);
			num++;
		}
		raiseEvent = true;
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(num2 - num + 1, num));
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRows));
	}

	/// <summary>Clears the collection. </summary>
	/// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents the row collection from being modified:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Clear()
	{
		int count = list.Count;
		DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(0, count));
		for (int i = 0; i < count; i++)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[0];
			if (dataGridViewRow.IsNewRow)
			{
				break;
			}
			list.Remove(dataGridViewRow);
			ReIndex();
		}
		DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(0, count));
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	internal void ClearInternal()
	{
		list.Clear();
	}

	/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the collection.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.DataGridViewRow" /> is in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, false.</returns>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <filterpriority>1</filterpriority>
	public virtual bool Contains(DataGridViewRow dataGridViewRow)
	{
		return list.Contains(dataGridViewRow);
	}

	/// <summary>Copies the items from the collection into the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> array, starting at the specified index.</summary>
	/// <param name="array">A <see cref="T:System.Windows.Forms.DataGridViewRow" /> array that is the destination of the items copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="array" /> is null. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero. </exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />. </exception>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(DataGridViewRow[] array, int index)
	{
		list.CopyTo(array, index);
	}

	/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
	/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetFirstRow(DataGridViewElementStates includeFilter)
	{
		for (int i = 0; i < list.Count; i++)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[i];
			if ((dataGridViewRow.State & includeFilter) != 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
	/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		for (int i = 0; i < list.Count; i++)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[i];
			if ((dataGridViewRow.State & includeFilter) != 0 && (dataGridViewRow.State & excludeFilter) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
	/// <returns>The index of the last <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetLastRow(DataGridViewElementStates includeFilter)
	{
		for (int num = list.Count - 1; num >= 0; num--)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[num];
			if ((dataGridViewRow.State & includeFilter) != 0)
			{
				return num;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
	/// <returns>The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow" /> after <paramref name="indexStart" /> that has the attributes specified by <paramref name="includeFilter" />, or -1 if no row is found.</returns>
	/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexStart" /> is less than -1.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
	{
		for (int i = indexStart + 1; i < list.Count; i++)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[i];
			if ((dataGridViewRow.State & includeFilter) != 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
	/// <returns>The index of the next <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
	/// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexStart" /> is less than -1.</exception>
	/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		for (int i = indexStart + 1; i < list.Count; i++)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[i];
			if ((dataGridViewRow.State & includeFilter) != 0 && (dataGridViewRow.State & excludeFilter) == 0)
			{
				return i;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified criteria.</summary>
	/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />; -1 if no row is found.</returns>
	/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
	{
		for (int num = indexStart - 1; num >= 0; num--)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[num];
			if ((dataGridViewRow.State & includeFilter) != 0)
			{
				return num;
			}
		}
		return -1;
	}

	/// <summary>Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that meets the specified inclusion and exclusion criteria.</summary>
	/// <returns>The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has the attributes specified by <paramref name="includeFilter" />, and does not have the attributes specified by <paramref name="excludeFilter" />; -1 if no row is found.</returns>
	/// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexStart" /> is greater than the number of rows in the collection.</exception>
	/// <exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		for (int num = indexStart - 1; num >= 0; num--)
		{
			DataGridViewRow dataGridViewRow = (DataGridViewRow)list[num];
			if ((dataGridViewRow.State & includeFilter) != 0 && (dataGridViewRow.State & excludeFilter) == 0)
			{
				return num;
			}
		}
		return -1;
	}

	/// <summary>Returns the number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the collection that meet the specified criteria.</summary>
	/// <returns>The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetRowCount(DataGridViewElementStates includeFilter)
	{
		int num = 0;
		foreach (DataGridViewRow item in list)
		{
			if ((item.State & includeFilter) != 0)
			{
				num++;
			}
		}
		return num;
	}

	/// <summary>Returns the cumulative height of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects that meet the specified criteria.</summary>
	/// <returns>The cumulative height of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> that have the attributes specified by <paramref name="includeFilter" />.</returns>
	/// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetRowsHeight(DataGridViewElementStates includeFilter)
	{
		int num = 0;
		foreach (DataGridViewRow item in list)
		{
			if ((item.State & includeFilter) != 0)
			{
				num += item.Height;
			}
		}
		return num;
	}

	/// <summary>Gets the state of the row with the specified index.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state of the specified row.</returns>
	/// <param name="rowIndex">The index of the row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero and greater than the number of rows in the collection minus one.</exception>
	public virtual DataGridViewElementStates GetRowState(int rowIndex)
	{
		return (list[rowIndex] as DataGridViewRow).State;
	}

	/// <summary>Returns the index of a specified item in the collection.</summary>
	/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow" /> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />; otherwise, -1.</returns>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(DataGridViewRow dataGridViewRow)
	{
		return list.IndexOf(dataGridViewRow);
	}

	/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.DataGridViewRow" /> into the collection.</summary>
	/// <param name="rowIndex">The position at which to insert the row.</param>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRow" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of <paramref name="dataGridViewRow" /> is not null.-or-<paramref name="dataGridViewRow" /> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewRow" /> has more cells than there are columns in the control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
	{
		dataGridViewRow.SetIndex(rowIndex);
		dataGridViewRow.SetDataGridView(dataGridView);
		CompleteRowCells(dataGridViewRow);
		list.Insert(rowIndex, dataGridViewRow);
		ReIndex();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewRow));
		if (raiseEvent)
		{
			DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, 1));
		}
	}

	/// <summary>Inserts the specified number of rows into the collection at the specified location.</summary>
	/// <param name="rowIndex">The position at which to insert the rows.</param>
	/// <param name="count">The number of rows to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. -or-<paramref name="count" /> is less than 1.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Insert(int rowIndex, int count)
	{
		int num = rowIndex;
		raiseEvent = false;
		for (int i = 0; i < count; i++)
		{
			Insert(num++, dataGridView.RowTemplateFull);
		}
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, count));
		raiseEvent = true;
	}

	/// <summary>Inserts a row into the collection at the specified position, and populates the cells with the specified objects.</summary>
	/// <param name="rowIndex">The position at which to insert the row.</param>
	/// <param name="values">A variable number of objects that populate the cells of the new row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property is not null. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
	/// <exception cref="T:System.ArgumentException">The row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate" /> property has more cells than there are columns in the control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Insert(int rowIndex, params object[] values)
	{
		if (values == null)
		{
			throw new ArgumentNullException("Values is null.");
		}
		if (dataGridView.VirtualMode || dataGridView.DataSource != null)
		{
			throw new InvalidOperationException();
		}
		DataGridViewRow dataGridViewRow = new DataGridViewRow();
		dataGridViewRow.SetValues(values);
		Insert(rowIndex, dataGridViewRow);
	}

	/// <summary>Inserts rows into the collection at the specified position.</summary>
	/// <param name="indexSource">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> on which to base the new rows.</param>
	/// <param name="indexDestination">The position at which to insert the rows.</param>
	/// <param name="count">The number of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.-or-<paramref name="count" /> is less than 1.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true.-or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void InsertCopies(int indexSource, int indexDestination, int count)
	{
		raiseEvent = false;
		int num = indexDestination;
		for (int i = 0; i < count; i++)
		{
			InsertCopy(indexSource, num++);
		}
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(indexDestination, count));
		raiseEvent = true;
	}

	/// <summary>Inserts a row into the collection at the specified position, based on the row at specified position.</summary>
	/// <param name="indexSource">The index of the row on which to base the new row.</param>
	/// <param name="indexDestination">The position at which to insert the row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="indexSource" /> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination" /> is less than zero or greater than the number of rows in the collection.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="indexDestination" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void InsertCopy(int indexSource, int indexDestination)
	{
		Insert(indexDestination, (list[indexSource] as DataGridViewRow).Clone());
	}

	/// <summary>Inserts the <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects into the collection at the specified position.</summary>
	/// <param name="rowIndex">The position at which to insert the rows.</param>
	/// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow" /> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRows" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the collection.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewRows" /> contains only one row, and the row it contains has more cells than there are columns in the control.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="rowIndex" /> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> is true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is not null.-or-At least one entry in the <paramref name="dataGridViewRows" /> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> has no columns.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows" /> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected" /> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows" /> array are identical.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows" /> array contains more cells than there are columns in the control. -or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
	{
		raiseEvent = false;
		int num = rowIndex;
		int num2 = 0;
		foreach (DataGridViewRow dataGridViewRow in dataGridViewRows)
		{
			Insert(num++, dataGridViewRow);
			num2++;
		}
		DataGridView.OnRowsAddedInternal(new DataGridViewRowsAddedEventArgs(rowIndex, num2));
		raiseEvent = true;
	}

	/// <summary>Removes the row from the collection.</summary>
	/// <param name="dataGridViewRow">The row to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewRow" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewRow" /> is not contained in this collection.-or-<paramref name="dataGridViewRow" /> is a shared row.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewRow" /> is the row for new records.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Remove(DataGridViewRow dataGridViewRow)
	{
		if (dataGridViewRow.IsNewRow)
		{
			throw new InvalidOperationException("Cannot delete the new row");
		}
		DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
		list.Remove(dataGridViewRow);
		ReIndex();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewRow));
		DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
	}

	internal virtual void RemoveInternal(DataGridViewRow dataGridViewRow)
	{
		DataGridView.OnRowsPreRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
		list.Remove(dataGridViewRow);
		ReIndex();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewRow));
		DataGridView.OnRowsPostRemovedInternal(new DataGridViewRowsRemovedEventArgs(dataGridViewRow.Index, 1));
	}

	/// <summary>Removes the row at the specified position from the collection.</summary>
	/// <param name="index">The position of the row to remove.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero and greater than the number of rows in the collection minus one. </exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="index" /> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows" /> property of the <see cref="T:System.Windows.Forms.DataGridView" /> is set to true.-or-The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is bound to an <see cref="T:System.ComponentModel.IBindingList" /> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove" /> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" /> property values that are not both true.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveAt(int index)
	{
		DataGridViewRow dataGridViewRow = this[index];
		Remove(dataGridViewRow);
	}

	internal void RemoveAtInternal(int index)
	{
		DataGridViewRow dataGridViewRow = this[index];
		RemoveInternal(dataGridViewRow);
	}

	/// <summary>Returns the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> positioned at the specified index.</returns>
	/// <param name="rowIndex">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to get.</param>
	/// <filterpriority>1</filterpriority>
	public DataGridViewRow SharedRow(int rowIndex)
	{
		return (DataGridViewRow)list[rowIndex];
	}

	internal int SharedRowIndexOf(DataGridViewRow row)
	{
		return list.IndexOf(row);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewRowCollection.CollectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
	protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	internal void AddInternal(DataGridViewRow dataGridViewRow, bool sharable)
	{
		raiseEvent = false;
		AddCore(dataGridViewRow, sharable);
		raiseEvent = true;
	}

	internal void ReIndex()
	{
		for (int i = 0; i < Count; i++)
		{
			(list[i] as DataGridViewRow).SetIndex(i);
		}
	}

	internal void Sort(IComparer comparer)
	{
		if (DataGridView != null && DataGridView.EditingRow != null)
		{
			list.Sort(0, Count - 1, comparer);
		}
		else
		{
			list.Sort(comparer);
		}
		for (int i = 0; i < list.Count; i++)
		{
			(list[i] as DataGridViewRow).SetIndex(i);
		}
	}
}
