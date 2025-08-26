using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
/// <filterpriority>2</filterpriority>
[ListBindable(false)]
public class DataGridViewColumnCollection : BaseCollection, ICollection, IEnumerable, IList
{
	private class ColumnDisplayIndexComparator : IComparer<DataGridViewColumn>
	{
		public int Compare(DataGridViewColumn o1, DataGridViewColumn o2)
		{
			return o1.DisplayIndex - o2.DisplayIndex;
		}
	}

	private DataGridView dataGridView;

	private List<DataGridViewColumn> display_index_sorted;

	/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
	/// <returns>false in all cases.</returns>
	bool IList.IsFixedSize => base.List.IsFixedSize;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the specified index.</returns>
	/// <param name="index">The zero-based index of the column to get.</param>
	/// <exception cref="T:System.NotSupportedException">This property is being set.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">When getting this property, <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
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

	/// <summary>Gets or sets the column at the given index in the collection. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> at the given index.</returns>
	/// <param name="index">The zero-based index of the column to get or set.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than the number of columns in the collection minus one.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewColumn this[int index] => (DataGridViewColumn)base.List[index];

	/// <summary>Gets or sets the column of the given name in the collection. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> identified by the <paramref name="columnName" /> parameter.</returns>
	/// <param name="columnName">The name of the column to get or set.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="columnName" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public DataGridViewColumn this[string columnName]
	{
		get
		{
			foreach (DataGridViewColumn item in base.List)
			{
				if (item.Name == columnName)
				{
					return item;
				}
			}
			return null;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> upon which the collection performs column-related operations.</summary>
	/// <returns>
	///   <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	protected DataGridView DataGridView => dataGridView;

	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the elements of the collection. This property returns null unless overridden in a derived class.</returns>
	protected override ArrayList List => base.List;

	internal List<DataGridViewColumn> ColumnDisplayIndexSortedArrayList => display_index_sorted;

	/// <summary>Occurs when the collection changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event CollectionChangeEventHandler CollectionChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> class for the given <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that created this collection.</param>
	public DataGridViewColumnCollection(DataGridView dataGridView)
	{
		this.dataGridView = dataGridView;
		RegenerateSortedList();
	}

	/// <summary>Adds an object to the end of the collection.</summary>
	/// <returns>The index at which <paramref name="value" /> has been added.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to add to the end of the collection. The value can be null.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
	int IList.Add(object value)
	{
		return Add(value as DataGridViewColumn);
	}

	/// <summary>Determines whether an object is in the collection.</summary>
	/// <returns>true if <paramref name="value" /> is found in the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" />; otherwise, false.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be null.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	bool IList.Contains(object value)
	{
		return Contains(value as DataGridViewColumn);
	}

	/// <summary>Determines the index of a specific item in the collection.</summary>
	/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the collection, if found; otherwise, -1.</returns>
	/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection. The value can be null.</param>
	int IList.IndexOf(object value)
	{
		return IndexOf(value as DataGridViewColumn);
	}

	/// <summary>Inserts an element into the collection at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to insert. The value can be null.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The column indicated by <paramref name="value" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of the column indicated by <paramref name="value" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-The column indicated by <paramref name="value" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-The column indicated by <paramref name="value" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and the column indicated by <paramref name="value" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
	void IList.Insert(int index, object value)
	{
		Insert(index, value as DataGridViewColumn);
	}

	/// <summary>Removes the first occurrence of the specified object from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection. The value can be null.</param>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="value" /> is not in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="value" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	void IList.Remove(object value)
	{
		Remove(value as DataGridViewColumn);
	}

	/// <summary>Adds the given column to the collection.</summary>
	/// <returns>The index of the column.</returns>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumn" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-<paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-<paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-<paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual int Add(DataGridViewColumn dataGridViewColumn)
	{
		int num = base.List.Add(dataGridViewColumn);
		dataGridViewColumn.SetIndex(num);
		dataGridViewColumn.SetDataGridView(dataGridView);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn));
		return num;
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> with the given column name and column header text to the collection.</summary>
	/// <returns>The index of the column.</returns>
	/// <param name="columnName">The name by which the column will be referred.</param>
	/// <param name="headerText">The text for the column's header.</param>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-The <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />, which conflicts with the default column <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" />.-or-The default column <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property value of 100 would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual int Add(string columnName, string headerText)
	{
		DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
		dataGridViewColumn.Name = columnName;
		dataGridViewColumn.HeaderText = headerText;
		return Add(dataGridViewColumn);
	}

	/// <summary>Adds a range of columns to the collection. </summary>
	/// <param name="dataGridViewColumns">An array of <see cref="T:System.Windows.Forms.DataGridViewColumn" /> objects to add.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumns" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-At least one of the values in <paramref name="dataGridViewColumns" /> is null.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null and the <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-The columns in <paramref name="dataGridViewColumns" /> have <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> property values that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight" /> values of all columns in the control to exceed 65535.-or-At least two of the values in <paramref name="dataGridViewColumns" /> are references to the same <see cref="T:System.Windows.Forms.DataGridViewColumn" />.-or-At least one of the columns in <paramref name="dataGridViewColumns" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
	{
		foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumns)
		{
			Add(dataGridViewColumn);
		}
	}

	/// <summary>Clears the collection. </summary>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Clear()
	{
		base.List.Clear();
		dataGridView.Rows.Clear();
		dataGridView.RemoveEditingRow();
		RegenerateSortedList();
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
	}

	/// <summary>Determines whether the collection contains the given column.</summary>
	/// <returns>true if the given column is in the collection; otherwise, false.</returns>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to look for.</param>
	/// <filterpriority>1</filterpriority>
	public virtual bool Contains(DataGridViewColumn dataGridViewColumn)
	{
		return base.List.Contains(dataGridViewColumn);
	}

	/// <summary>Determines whether the collection contains the column referred to by the given name. </summary>
	/// <returns>true if the column is contained in the collection; otherwise, false.</returns>
	/// <param name="columnName">The name of the column to look for.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="columnName" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	public virtual bool Contains(string columnName)
	{
		foreach (DataGridViewColumn item in base.List)
		{
			if (item.Name == columnName)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Copies the items from the collection to the given array.</summary>
	/// <param name="array">The destination <see cref="T:System.Windows.Forms.DataGridViewColumn" /> array.</param>
	/// <param name="index">The index of the destination array at which to start copying.</param>
	/// <filterpriority>1</filterpriority>
	public void CopyTo(DataGridViewColumn[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	/// <summary>Returns the number of columns that meet the given filter requirements.</summary>
	/// <returns>The number of columns that meet the filter requirements.</returns>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetColumnCount(DataGridViewElementStates includeFilter)
	{
		return 0;
	}

	/// <summary>Returns the width, in pixels, required to display all of the columns that meet the given filter requirements. </summary>
	/// <returns>The width, in pixels, that is necessary to display all of the columns that meet the filter requirements.</returns>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter for inclusion.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public int GetColumnsWidth(DataGridViewElementStates includeFilter)
	{
		return 0;
	}

	/// <summary>Returns the first column in display order that meets the given inclusion-filter requirements.</summary>
	/// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represents the filter for inclusion.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="includeFilter" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter)
	{
		return null;
	}

	/// <summary>Returns the first column in display order that meets the given inclusion-filter and exclusion-filter requirements. </summary>
	/// <returns>The first column in display order that meets the given filter requirements, or null if no column is found.</returns>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
	/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
	/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		return null;
	}

	/// <summary>Returns the last column in display order that meets the given filter requirements. </summary>
	/// <returns>The last displayed column in display order that meets the given filter requirements, or null if no column is found.</returns>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
	/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
	/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		return null;
	}

	/// <summary>Gets the first column after the given column in display order that meets the given filter requirements. </summary>
	/// <returns>The next column that meets the given filter requirements, or null if no column is found.</returns>
	/// <param name="dataGridViewColumnStart">The column from which to start searching for the next column.</param>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
	/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumnStart" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		return null;
	}

	/// <summary>Gets the last column prior to the given column in display order that meets the given filter requirements. </summary>
	/// <returns>The previous column that meets the given filter requirements, or null if no column is found.</returns>
	/// <param name="dataGridViewColumnStart">The column from which to start searching for the previous column.</param>
	/// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for inclusion.</param>
	/// <param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that represent the filter to apply for exclusion.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumnStart" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</exception>
	public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
	{
		return null;
	}

	/// <summary>Gets the index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" /> in the collection.</summary>
	/// <returns>The index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to return the index of.</param>
	/// <filterpriority>1</filterpriority>
	public int IndexOf(DataGridViewColumn dataGridViewColumn)
	{
		return base.List.IndexOf(dataGridViewColumn);
	}

	/// <summary>Inserts a column at the given index in the collection.</summary>
	/// <param name="columnIndex">The zero-based index at which to insert the given column.</param>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to insert.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumn" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" />-or-<paramref name="dataGridViewColumn" /> already belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic" /> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect" /> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect" />. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit" /> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit" /> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn" /><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible" /> property value is false.-or-<paramref name="dataGridViewColumn" /> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value of true.-or-<paramref name="dataGridViewColumn" /> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen" /> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView" /> control contains at least one row and <paramref name="dataGridViewColumn" /> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType" /> property value of null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
	{
		base.List.Insert(columnIndex, dataGridViewColumn);
		dataGridViewColumn.SetIndex(columnIndex);
		dataGridViewColumn.SetDataGridView(dataGridView);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataGridViewColumn));
	}

	/// <summary>Removes the specified column from the collection.</summary>
	/// <param name="dataGridViewColumn">The column to delete.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="dataGridViewColumn" /> is not in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewColumn" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Remove(DataGridViewColumn dataGridViewColumn)
	{
		DataGridView.OnColumnPreRemovedInternal(new DataGridViewColumnEventArgs(dataGridViewColumn));
		base.List.Remove(dataGridViewColumn);
		OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataGridViewColumn));
	}

	/// <summary>Removes the column with the specified name from the collection.</summary>
	/// <param name="columnName">The name of the column to delete.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="columnName" /> does not match the name of any column in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="columnName" /> is null.</exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Remove(string columnName)
	{
		foreach (DataGridViewColumn item in base.List)
		{
			if (item.Name == columnName)
			{
				Remove(item);
				break;
			}
		}
	}

	/// <summary>Removes the column at the given index in the collection.</summary>
	/// <param name="index">The index of the column to delete.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> is less than zero or greater than the number of columns in the control minus one. </exception>
	/// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView" /> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex" /> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView" /> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter" /><see cref="E:System.Windows.Forms.DataGridView.CellLeave" /><see cref="E:System.Windows.Forms.DataGridView.CellValidating" /><see cref="E:System.Windows.Forms.DataGridView.CellValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowEnter" /><see cref="E:System.Windows.Forms.DataGridView.RowLeave" /><see cref="E:System.Windows.Forms.DataGridView.RowValidated" /><see cref="E:System.Windows.Forms.DataGridView.RowValidating" /></exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void RemoveAt(int index)
	{
		DataGridViewColumn dataGridViewColumn = this[index];
		Remove(dataGridViewColumn);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridViewColumnCollection.CollectionChanged" /> event.</summary>
	/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
	protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
	{
		RegenerateIndexes();
		RegenerateSortedList();
		if (this.CollectionChanged != null)
		{
			this.CollectionChanged(this, e);
		}
	}

	private void RegenerateIndexes()
	{
		for (int i = 0; i < Count; i++)
		{
			this[i].SetIndex(i);
		}
	}

	internal void RegenerateSortedList()
	{
		DataGridViewColumn[] collection = (DataGridViewColumn[])base.List.ToArray(typeof(DataGridViewColumn));
		List<DataGridViewColumn> list = new List<DataGridViewColumn>(collection);
		list.Sort(new ColumnDisplayIndexComparator());
		for (int i = 0; i < list.Count; i++)
		{
			list[i].DisplayIndex = i;
		}
		display_index_sorted = list;
	}

	internal void ClearAutoGeneratedColumns()
	{
		for (int num = list.Count - 1; num >= 0; num--)
		{
			if ((list[num] as DataGridViewColumn).AutoGenerated)
			{
				RemoveAt(num);
			}
		}
	}

	virtual bool IList.get_IsReadOnly()
	{
		return base.IsReadOnly;
	}
}
