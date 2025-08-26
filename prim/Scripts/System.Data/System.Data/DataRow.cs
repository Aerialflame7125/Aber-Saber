using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace System.Data;

/// <summary>Represents a row of data in a <see cref="T:System.Data.DataTable" />.</summary>
public class DataRow
{
	private readonly DataTable _table;

	private readonly DataColumnCollection _columns;

	internal int _oldRecord = -1;

	internal int _newRecord = -1;

	internal int _tempRecord;

	internal long _rowID = -1L;

	internal DataRowAction _action;

	internal bool _inChangingEvent;

	internal bool _inDeletingEvent;

	internal bool _inCascade;

	private DataColumn _lastChangedColumn;

	private int _countColumnChange;

	private DataError _error;

	private object _element;

	private int _rbTreeNodeId;

	private static int s_objectTypeCount;

	internal readonly int _objectID = Interlocked.Increment(ref s_objectTypeCount);

	internal XmlBoundElement Element
	{
		get
		{
			return (XmlBoundElement)_element;
		}
		set
		{
			_element = value;
		}
	}

	internal DataColumn LastChangedColumn
	{
		get
		{
			if (_countColumnChange == 1)
			{
				return _lastChangedColumn;
			}
			return null;
		}
		set
		{
			_countColumnChange++;
			_lastChangedColumn = value;
		}
	}

	internal bool HasPropertyChanged => 0 < _countColumnChange;

	internal int RBTreeNodeId
	{
		get
		{
			return _rbTreeNodeId;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataRow.set_RBTreeNodeId|INFO> {0}, value={1}", _objectID, value);
			_rbTreeNodeId = value;
		}
	}

	/// <summary>Gets or sets the custom error description for a row.</summary>
	/// <returns>The text describing an error.</returns>
	public string RowError
	{
		get
		{
			if (_error != null)
			{
				return _error.Text;
			}
			return string.Empty;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataRow.set_RowError|API> {0}, value='{1}'", _objectID, value);
			if (_error == null)
			{
				if (!string.IsNullOrEmpty(value))
				{
					_error = new DataError(value);
				}
				RowErrorChanged();
			}
			else if (_error.Text != value)
			{
				_error.Text = value;
				RowErrorChanged();
			}
		}
	}

	internal long rowID
	{
		get
		{
			return _rowID;
		}
		set
		{
			ResetLastChangedColumn();
			_rowID = value;
		}
	}

	/// <summary>Gets the current state of the row with regard to its relationship to the <see cref="T:System.Data.DataRowCollection" />.</summary>
	/// <returns>One of the <see cref="T:System.Data.DataRowState" /> values.</returns>
	public DataRowState RowState
	{
		get
		{
			if (_oldRecord == _newRecord)
			{
				if (_oldRecord == -1)
				{
					return DataRowState.Detached;
				}
				if (0 < _columns.ColumnsImplementingIChangeTrackingCount)
				{
					DataColumn[] columnsImplementingIChangeTracking = _columns.ColumnsImplementingIChangeTracking;
					foreach (DataColumn column in columnsImplementingIChangeTracking)
					{
						object obj = this[column];
						if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
						{
							return DataRowState.Modified;
						}
					}
				}
				return DataRowState.Unchanged;
			}
			if (_oldRecord == -1)
			{
				return DataRowState.Added;
			}
			if (_newRecord == -1)
			{
				return DataRowState.Deleted;
			}
			return DataRowState.Modified;
		}
	}

	/// <summary>Gets the <see cref="T:System.Data.DataTable" /> for which this row has a schema.</summary>
	/// <returns>The <see cref="T:System.Data.DataTable" /> to which this row belongs.</returns>
	public DataTable Table => _table;

	/// <summary>Gets or sets the data stored in the column specified by index.</summary>
	/// <param name="columnIndex">The zero-based index of the column.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range.</exception>
	/// <exception cref="T:System.InvalidCastException">Occurs when you set the value and the new value's <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />.</exception>
	public object this[int columnIndex]
	{
		get
		{
			DataColumn dataColumn = _columns[columnIndex];
			int defaultRecord = GetDefaultRecord();
			return dataColumn[defaultRecord];
		}
		set
		{
			DataColumn column = _columns[columnIndex];
			this[column] = value;
		}
	}

	/// <summary>Gets or sets the data stored in the column specified by name.</summary>
	/// <param name="columnName">The name of the column.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row.</exception>
	/// <exception cref="T:System.InvalidCastException">Occurs when you set a value and its <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />.</exception>
	/// <exception cref="T:System.Data.NoNullAllowedException">Occurs when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to <see langword="false" />.</exception>
	public object this[string columnName]
	{
		get
		{
			DataColumn dataColumn = GetDataColumn(columnName);
			int defaultRecord = GetDefaultRecord();
			return dataColumn[defaultRecord];
		}
		set
		{
			DataColumn dataColumn = GetDataColumn(columnName);
			this[dataColumn] = value;
		}
	}

	/// <summary>Gets or sets the data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains the data.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.ArgumentException">The column does not belong to this table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row.</exception>
	/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match.</exception>
	public object this[DataColumn column]
	{
		get
		{
			CheckColumn(column);
			int defaultRecord = GetDefaultRecord();
			return column[defaultRecord];
		}
		set
		{
			CheckColumn(column);
			if (_inChangingEvent)
			{
				throw ExceptionBuilder.EditInRowChanging();
			}
			if (-1 != rowID && column.ReadOnly)
			{
				throw ExceptionBuilder.ReadOnly(column.ColumnName);
			}
			DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
			if (_table.NeedColumnChangeEvents)
			{
				dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this, column, value);
				_table.OnColumnChanging(dataColumnChangeEventArgs);
			}
			if (column.Table != _table)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, _table.TableName);
			}
			if (-1 != rowID && column.ReadOnly)
			{
				throw ExceptionBuilder.ReadOnly(column.ColumnName);
			}
			object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value);
			if (obj == null)
			{
				if (column.IsValueType)
				{
					throw ExceptionBuilder.CannotSetToNull(column);
				}
				obj = DBNull.Value;
			}
			bool flag = BeginEditInternal();
			try
			{
				int proposedRecordNo = GetProposedRecordNo();
				column[proposedRecordNo] = obj;
			}
			catch (Exception e) when (ADP.IsCatchableOrSecurityExceptionType(e))
			{
				if (flag)
				{
					CancelEdit();
				}
				throw;
			}
			LastChangedColumn = column;
			if (dataColumnChangeEventArgs != null)
			{
				_table.OnColumnChanged(dataColumnChangeEventArgs);
			}
			if (flag)
			{
				EndEdit();
			}
		}
	}

	/// <summary>Gets the data stored in the column, specified by index and version of the data to retrieve.</summary>
	/// <param name="columnIndex">The zero-based index of the column.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range.</exception>
	/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row.</exception>
	public object this[int columnIndex, DataRowVersion version]
	{
		get
		{
			DataColumn dataColumn = _columns[columnIndex];
			int recordFromVersion = GetRecordFromVersion(version);
			return dataColumn[recordFromVersion];
		}
	}

	/// <summary>Gets the specified version of data stored in the named column.</summary>
	/// <param name="columnName">The name of the column.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found.</exception>
	/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row was deleted.</exception>
	public object this[string columnName, DataRowVersion version]
	{
		get
		{
			DataColumn dataColumn = GetDataColumn(columnName);
			int recordFromVersion = GetRecordFromVersion(version);
			return dataColumn[recordFromVersion];
		}
	}

	/// <summary>Gets the specified version of data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains information about the column.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
	/// <exception cref="T:System.ArgumentException">The column does not belong to the table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> argument contains null.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data.</exception>
	public object this[DataColumn column, DataRowVersion version]
	{
		get
		{
			CheckColumn(column);
			int recordFromVersion = GetRecordFromVersion(version);
			return column[recordFromVersion];
		}
	}

	/// <summary>Gets or sets all the values for this row through an array.</summary>
	/// <returns>An array of type <see cref="T:System.Object" />.</returns>
	/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table.</exception>
	/// <exception cref="T:System.InvalidCastException">A value in the array does not match its <see cref="P:System.Data.DataColumn.DataType" /> in its respective <see cref="T:System.Data.DataColumn" />.</exception>
	/// <exception cref="T:System.Data.ConstraintException">An edit broke a constraint.</exception>
	/// <exception cref="T:System.Data.ReadOnlyException">An edit tried to change the value of a read-only column.</exception>
	/// <exception cref="T:System.Data.NoNullAllowedException">An edit tried to put a null value in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> of the <see cref="T:System.Data.DataColumn" /> object is <see langword="false" />.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row has been deleted.</exception>
	public object[] ItemArray
	{
		get
		{
			int defaultRecord = GetDefaultRecord();
			object[] array = new object[_columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DataColumn dataColumn = _columns[i];
				array[i] = dataColumn[defaultRecord];
			}
			return array;
		}
		set
		{
			if (value == null)
			{
				throw ExceptionBuilder.ArgumentNull("ItemArray");
			}
			if (_columns.Count < value.Length)
			{
				throw ExceptionBuilder.ValueArrayLength();
			}
			DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
			if (_table.NeedColumnChangeEvents)
			{
				dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this);
			}
			bool flag = BeginEditInternal();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == null)
				{
					continue;
				}
				DataColumn dataColumn = _columns[i];
				if (-1 != rowID && dataColumn.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
				}
				if (dataColumnChangeEventArgs != null)
				{
					dataColumnChangeEventArgs.InitializeColumnChangeEvent(dataColumn, value[i]);
					_table.OnColumnChanging(dataColumnChangeEventArgs);
				}
				if (dataColumn.Table != _table)
				{
					throw ExceptionBuilder.ColumnNotInTheTable(dataColumn.ColumnName, _table.TableName);
				}
				if (-1 != rowID && dataColumn.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
				}
				if (_tempRecord == -1)
				{
					BeginEditInternal();
				}
				object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value[i]);
				if (obj == null)
				{
					if (dataColumn.IsValueType)
					{
						throw ExceptionBuilder.CannotSetToNull(dataColumn);
					}
					obj = DBNull.Value;
				}
				try
				{
					int proposedRecordNo = GetProposedRecordNo();
					dataColumn[proposedRecordNo] = obj;
				}
				catch (Exception e) when (ADP.IsCatchableOrSecurityExceptionType(e))
				{
					if (flag)
					{
						CancelEdit();
					}
					throw;
				}
				LastChangedColumn = dataColumn;
				if (dataColumnChangeEventArgs != null)
				{
					_table.OnColumnChanged(dataColumnChangeEventArgs);
				}
			}
			EndEdit();
		}
	}

	/// <summary>Gets a value that indicates whether there are errors in a row.</summary>
	/// <returns>
	///   <see langword="true" /> if the row contains an error; otherwise, <see langword="false" />.</returns>
	public bool HasErrors
	{
		get
		{
			if (_error != null)
			{
				return _error.HasErrors;
			}
			return false;
		}
	}

	/// <summary>Initializes a new instance of the DataRow. Constructs a row from the builder. Only for internal usage.</summary>
	/// <param name="builder">builder</param>
	protected internal DataRow(DataRowBuilder builder)
	{
		_tempRecord = builder._record;
		_table = builder._table;
		_columns = _table.Columns;
	}

	private void RowErrorChanged()
	{
		if (_oldRecord != -1)
		{
			_table.RecordChanged(_oldRecord);
		}
		if (_newRecord != -1)
		{
			_table.RecordChanged(_newRecord);
		}
	}

	internal void CheckForLoops(DataRelation rel)
	{
		if (_table._fInLoadDiffgram || (_table.DataSet != null && _table.DataSet._fInLoadDiffgram))
		{
			return;
		}
		int count = _table.Rows.Count;
		int num = 0;
		for (DataRow parentRow = GetParentRow(rel); parentRow != null; parentRow = parentRow.GetParentRow(rel))
		{
			if (parentRow == this || num > count)
			{
				throw ExceptionBuilder.NestedCircular(_table.TableName);
			}
			num++;
		}
	}

	internal int GetNestedParentCount()
	{
		int num = 0;
		DataRelation[] nestedParentRelations = _table.NestedParentRelations;
		foreach (DataRelation dataRelation in nestedParentRelations)
		{
			if (dataRelation != null)
			{
				if (dataRelation.ParentTable == _table)
				{
					CheckForLoops(dataRelation);
				}
				if (GetParentRow(dataRelation) != null)
				{
					num++;
				}
			}
		}
		return num;
	}

	/// <summary>Commits all the changes made to this row since the last time <see cref="M:System.Data.DataRow.AcceptChanges" /> was called.</summary>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	public void AcceptChanges()
	{
		long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataRow.AcceptChanges|API> {0}", _objectID);
		try
		{
			EndEdit();
			if (RowState != DataRowState.Detached && RowState != DataRowState.Deleted && _columns.ColumnsImplementingIChangeTrackingCount > 0)
			{
				DataColumn[] columnsImplementingIChangeTracking = _columns.ColumnsImplementingIChangeTracking;
				foreach (DataColumn column in columnsImplementingIChangeTracking)
				{
					object obj = this[column];
					if (DBNull.Value != obj)
					{
						IChangeTracking changeTracking = (IChangeTracking)obj;
						if (changeTracking.IsChanged)
						{
							changeTracking.AcceptChanges();
						}
					}
				}
			}
			_table.CommitRow(this);
		}
		finally
		{
			DataCommonEventSource.Log.ExitScope(scopeId);
		}
	}

	/// <summary>Starts an edit operation on a <see cref="T:System.Data.DataRow" /> object.</summary>
	/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event.</exception>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The method was called upon a deleted row.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void BeginEdit()
	{
		BeginEditInternal();
	}

	private bool BeginEditInternal()
	{
		if (_inChangingEvent)
		{
			throw ExceptionBuilder.BeginEditInRowChanging();
		}
		if (_tempRecord != -1)
		{
			if (_tempRecord < _table._recordManager.LastFreeRecord)
			{
				return false;
			}
			_tempRecord = -1;
		}
		if (_oldRecord != -1 && _newRecord == -1)
		{
			throw ExceptionBuilder.DeletedRowInaccessible();
		}
		ResetLastChangedColumn();
		_tempRecord = _table.NewRecord(_newRecord);
		return true;
	}

	/// <summary>Cancels the current edit on the row.</summary>
	/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void CancelEdit()
	{
		if (_inChangingEvent)
		{
			throw ExceptionBuilder.CancelEditInRowChanging();
		}
		_table.FreeRecord(ref _tempRecord);
		ResetLastChangedColumn();
	}

	private void CheckColumn(DataColumn column)
	{
		if (column == null)
		{
			throw ExceptionBuilder.ArgumentNull("column");
		}
		if (column.Table != _table)
		{
			throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, _table.TableName);
		}
	}

	internal void CheckInTable()
	{
		if (rowID == -1)
		{
			throw ExceptionBuilder.RowNotInTheTable();
		}
	}

	/// <summary>Deletes the <see cref="T:System.Data.DataRow" />.</summary>
	/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The <see cref="T:System.Data.DataRow" /> has already been deleted.</exception>
	public void Delete()
	{
		if (_inDeletingEvent)
		{
			throw ExceptionBuilder.DeleteInRowDeleting();
		}
		if (_newRecord != -1)
		{
			_table.DeleteRow(this);
		}
	}

	/// <summary>Ends the edit occurring on the row.</summary>
	/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event.</exception>
	/// <exception cref="T:System.Data.ConstraintException">The edit broke a constraint.</exception>
	/// <exception cref="T:System.Data.ReadOnlyException">The row belongs to the table and the edit tried to change the value of a read-only column.</exception>
	/// <exception cref="T:System.Data.NoNullAllowedException">The edit tried to put a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void EndEdit()
	{
		if (_inChangingEvent)
		{
			throw ExceptionBuilder.EndEditInRowChanging();
		}
		if (_newRecord == -1 || _tempRecord == -1)
		{
			return;
		}
		try
		{
			_table.SetNewRecord(this, _tempRecord, DataRowAction.Change, isInMerge: false, fireEvent: true, suppressEnsurePropertyChanged: true);
		}
		finally
		{
			ResetLastChangedColumn();
		}
	}

	/// <summary>Sets the error description for a column specified by index.</summary>
	/// <param name="columnIndex">The zero-based index of the column.</param>
	/// <param name="error">The error description.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range</exception>
	public void SetColumnError(int columnIndex, string error)
	{
		DataColumn dataColumn = _columns[columnIndex];
		if (dataColumn == null)
		{
			throw ExceptionBuilder.ColumnOutOfRange(columnIndex);
		}
		SetColumnError(dataColumn, error);
	}

	/// <summary>Sets the error description for a column specified by name.</summary>
	/// <param name="columnName">The name of the column.</param>
	/// <param name="error">The error description.</param>
	public void SetColumnError(string columnName, string error)
	{
		DataColumn dataColumn = GetDataColumn(columnName);
		SetColumnError(dataColumn, error);
	}

	/// <summary>Sets the error description for a column specified as a <see cref="T:System.Data.DataColumn" />.</summary>
	/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to set the error description for.</param>
	/// <param name="error">The error description.</param>
	public void SetColumnError(DataColumn column, string error)
	{
		CheckColumn(column);
		long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataRow.SetColumnError|API> {0}, column={1}, error='{2}'", _objectID, column.ObjectID, error);
		try
		{
			if (_error == null)
			{
				_error = new DataError();
			}
			if (GetColumnError(column) != error)
			{
				_error.SetColumnError(column, error);
				RowErrorChanged();
			}
		}
		finally
		{
			DataCommonEventSource.Log.ExitScope(scopeId);
		}
	}

	/// <summary>Gets the error description for the column specified by index.</summary>
	/// <param name="columnIndex">The zero-based index of the column.</param>
	/// <returns>The text of the error description.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range.</exception>
	public string GetColumnError(int columnIndex)
	{
		return GetColumnError(_columns[columnIndex]);
	}

	/// <summary>Gets the error description for a column, specified by name.</summary>
	/// <param name="columnName">The name of the column.</param>
	/// <returns>The text of the error description.</returns>
	public string GetColumnError(string columnName)
	{
		return GetColumnError(GetDataColumn(columnName));
	}

	/// <summary>Gets the error description of the specified <see cref="T:System.Data.DataColumn" />.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" />.</param>
	/// <returns>The text of the error description.</returns>
	public string GetColumnError(DataColumn column)
	{
		CheckColumn(column);
		if (_error == null)
		{
			_error = new DataError();
		}
		return _error.GetColumnError(column);
	}

	/// <summary>Clears the errors for the row. This includes the <see cref="P:System.Data.DataRow.RowError" /> and errors set with <see cref="M:System.Data.DataRow.SetColumnError(System.Int32,System.String)" />.</summary>
	public void ClearErrors()
	{
		if (_error != null)
		{
			_error.Clear();
			RowErrorChanged();
		}
	}

	internal void ClearError(DataColumn column)
	{
		if (_error != null)
		{
			_error.Clear(column);
			RowErrorChanged();
		}
	}

	/// <summary>Gets an array of columns that have errors.</summary>
	/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that contain errors.</returns>
	public DataColumn[] GetColumnsInError()
	{
		if (_error != null)
		{
			return _error.GetColumnsInError();
		}
		return Array.Empty<DataColumn>();
	}

	/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	public DataRow[] GetChildRows(string relationName)
	{
		return GetChildRows(_table.ChildRelations[relationName], DataRowVersion.Default);
	}

	/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />.</exception>
	public DataRow[] GetChildRows(string relationName, DataRowVersion version)
	{
		return GetChildRows(_table.ChildRelations[relationName], version);
	}

	/// <summary>Gets the child rows of this <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The relation is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data.</exception>
	public DataRow[] GetChildRows(DataRelation relation)
	{
		return GetChildRows(relation, DataRowVersion.Default);
	}

	/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />.</exception>
	public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
	{
		if (relation == null)
		{
			return _table.NewRowArray(0);
		}
		if (relation.DataSet != _table.DataSet)
		{
			throw ExceptionBuilder.RowNotInTheDataSet();
		}
		if (relation.ParentKey.Table != _table)
		{
			throw ExceptionBuilder.RelationForeignTable(relation.ParentTable.TableName, _table.TableName);
		}
		return DataRelation.GetChildRows(relation.ParentKey, relation.ChildKey, this, version);
	}

	internal DataColumn GetDataColumn(string columnName)
	{
		DataColumn dataColumn = _columns[columnName];
		if (dataColumn != null)
		{
			return dataColumn;
		}
		throw ExceptionBuilder.ColumnNotInTheTable(columnName, _table.TableName);
	}

	/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</param>
	/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	public DataRow GetParentRow(string relationName)
	{
		return GetParentRow(_table.ParentRelations[relationName], DataRowVersion.Default);
	}

	/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
	/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />.</exception>
	public DataRow GetParentRow(string relationName, DataRowVersion version)
	{
		return GetParentRow(_table.ParentRelations[relationName], version);
	}

	/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> does not belong to the <see cref="T:System.Data.DataTable" />.  
	/// -or-
	///  The row is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
	/// <exception cref="T:System.Data.InvalidConstraintException">This row does not belong to the child table of the <see cref="T:System.Data.DataRelation" /> object.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table.</exception>
	public DataRow GetParentRow(DataRelation relation)
	{
		return GetParentRow(relation, DataRowVersion.Default);
	}

	/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get.</param>
	/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
	/// <exception cref="T:System.ArgumentNullException">The row is <see langword="null" />.  
	/// -or-
	///  The <paramref name="relation" /> does not belong to this table's parent relations.</exception>
	/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
	/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data.</exception>
	public DataRow GetParentRow(DataRelation relation, DataRowVersion version)
	{
		if (relation == null)
		{
			return null;
		}
		if (relation.DataSet != _table.DataSet)
		{
			throw ExceptionBuilder.RelationForeignRow();
		}
		if (relation.ChildKey.Table != _table)
		{
			throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, _table.TableName);
		}
		return DataRelation.GetParentRow(relation.ParentKey, relation.ChildKey, this, version);
	}

	internal DataRow GetNestedParentRow(DataRowVersion version)
	{
		DataRelation[] nestedParentRelations = _table.NestedParentRelations;
		foreach (DataRelation dataRelation in nestedParentRelations)
		{
			if (dataRelation != null)
			{
				if (dataRelation.ParentTable == _table)
				{
					CheckForLoops(dataRelation);
				}
				DataRow parentRow = GetParentRow(dataRelation, version);
				if (parentRow != null)
				{
					return parentRow;
				}
			}
		}
		return null;
	}

	/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	public DataRow[] GetParentRows(string relationName)
	{
		return GetParentRows(_table.ParentRelations[relationName], DataRowVersion.Default);
	}

	/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />.</exception>
	public DataRow[] GetParentRows(string relationName, DataRowVersion version)
	{
		return GetParentRows(_table.ParentRelations[relationName], version);
	}

	/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The row is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />.</exception>
	public DataRow[] GetParentRows(DataRelation relation)
	{
		return GetParentRows(relation, DataRowVersion.Default);
	}

	/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
	/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get.</param>
	/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The row is <see langword="null" />.</exception>
	/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to.</exception>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />.</exception>
	/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />.</exception>
	public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version)
	{
		if (relation == null)
		{
			return _table.NewRowArray(0);
		}
		if (relation.DataSet != _table.DataSet)
		{
			throw ExceptionBuilder.RowNotInTheDataSet();
		}
		if (relation.ChildKey.Table != _table)
		{
			throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, _table.TableName);
		}
		return DataRelation.GetParentRows(relation.ParentKey, relation.ChildKey, this, version);
	}

	internal object[] GetColumnValues(DataColumn[] columns)
	{
		return GetColumnValues(columns, DataRowVersion.Default);
	}

	internal object[] GetColumnValues(DataColumn[] columns, DataRowVersion version)
	{
		DataKey key = new DataKey(columns, copyColumns: false);
		return GetKeyValues(key, version);
	}

	internal object[] GetKeyValues(DataKey key)
	{
		int defaultRecord = GetDefaultRecord();
		return key.GetKeyValues(defaultRecord);
	}

	internal object[] GetKeyValues(DataKey key, DataRowVersion version)
	{
		int recordFromVersion = GetRecordFromVersion(version);
		return key.GetKeyValues(recordFromVersion);
	}

	internal int GetCurrentRecordNo()
	{
		if (_newRecord == -1)
		{
			throw ExceptionBuilder.NoCurrentData();
		}
		return _newRecord;
	}

	internal int GetDefaultRecord()
	{
		if (_tempRecord != -1)
		{
			return _tempRecord;
		}
		if (_newRecord != -1)
		{
			return _newRecord;
		}
		throw (_oldRecord == -1) ? ExceptionBuilder.RowRemovedFromTheTable() : ExceptionBuilder.DeletedRowInaccessible();
	}

	internal int GetOriginalRecordNo()
	{
		if (_oldRecord == -1)
		{
			throw ExceptionBuilder.NoOriginalData();
		}
		return _oldRecord;
	}

	private int GetProposedRecordNo()
	{
		if (_tempRecord == -1)
		{
			throw ExceptionBuilder.NoProposedData();
		}
		return _tempRecord;
	}

	internal int GetRecordFromVersion(DataRowVersion version)
	{
		return version switch
		{
			DataRowVersion.Original => GetOriginalRecordNo(), 
			DataRowVersion.Current => GetCurrentRecordNo(), 
			DataRowVersion.Proposed => GetProposedRecordNo(), 
			DataRowVersion.Default => GetDefaultRecord(), 
			_ => throw ExceptionBuilder.InvalidRowVersion(), 
		};
	}

	internal DataRowVersion GetDefaultRowVersion(DataViewRowState viewState)
	{
		if (_oldRecord == _newRecord)
		{
			_ = _oldRecord;
			_ = -1;
			return DataRowVersion.Default;
		}
		if (_oldRecord == -1)
		{
			return DataRowVersion.Default;
		}
		if (_newRecord == -1)
		{
			return DataRowVersion.Original;
		}
		if ((DataViewRowState.ModifiedCurrent & viewState) != 0)
		{
			return DataRowVersion.Default;
		}
		return DataRowVersion.Original;
	}

	internal DataViewRowState GetRecordState(int record)
	{
		if (record == -1)
		{
			return DataViewRowState.None;
		}
		if (record == _oldRecord && record == _newRecord)
		{
			return DataViewRowState.Unchanged;
		}
		if (record == _oldRecord)
		{
			if (_newRecord == -1)
			{
				return DataViewRowState.Deleted;
			}
			return DataViewRowState.ModifiedOriginal;
		}
		if (record == _newRecord)
		{
			if (_oldRecord == -1)
			{
				return DataViewRowState.Added;
			}
			return DataViewRowState.ModifiedCurrent;
		}
		return DataViewRowState.None;
	}

	internal bool HasKeyChanged(DataKey key)
	{
		return HasKeyChanged(key, DataRowVersion.Current, DataRowVersion.Proposed);
	}

	internal bool HasKeyChanged(DataKey key, DataRowVersion version1, DataRowVersion version2)
	{
		if (!HasVersion(version1) || !HasVersion(version2))
		{
			return true;
		}
		return !key.RecordsEqual(GetRecordFromVersion(version1), GetRecordFromVersion(version2));
	}

	/// <summary>Gets a value that indicates whether a specified version exists.</summary>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version.</param>
	/// <returns>
	///   <see langword="true" /> if the version exists; otherwise, <see langword="false" />.</returns>
	public bool HasVersion(DataRowVersion version)
	{
		switch (version)
		{
		case DataRowVersion.Original:
			return _oldRecord != -1;
		case DataRowVersion.Current:
			return _newRecord != -1;
		case DataRowVersion.Proposed:
			return _tempRecord != -1;
		case DataRowVersion.Default:
			if (_tempRecord == -1)
			{
				return _newRecord != -1;
			}
			return true;
		default:
			throw ExceptionBuilder.InvalidRowVersion();
		}
	}

	internal bool HasChanges()
	{
		if (!HasVersion(DataRowVersion.Original) || !HasVersion(DataRowVersion.Current))
		{
			return true;
		}
		foreach (DataColumn column in Table.Columns)
		{
			if (column.Compare(_oldRecord, _newRecord) != 0)
			{
				return true;
			}
		}
		return false;
	}

	internal bool HaveValuesChanged(DataColumn[] columns)
	{
		return HaveValuesChanged(columns, DataRowVersion.Current, DataRowVersion.Proposed);
	}

	internal bool HaveValuesChanged(DataColumn[] columns, DataRowVersion version1, DataRowVersion version2)
	{
		for (int i = 0; i < columns.Length; i++)
		{
			CheckColumn(columns[i]);
		}
		DataKey key = new DataKey(columns, copyColumns: false);
		return HasKeyChanged(key, version1, version2);
	}

	/// <summary>Gets a value that indicates whether the column at the specified index contains a null value.</summary>
	/// <param name="columnIndex">The zero-based index of the column.</param>
	/// <returns>
	///   <see langword="true" /> if the column contains a null value; otherwise, <see langword="false" />.</returns>
	public bool IsNull(int columnIndex)
	{
		DataColumn dataColumn = _columns[columnIndex];
		int defaultRecord = GetDefaultRecord();
		return dataColumn.IsNull(defaultRecord);
	}

	/// <summary>Gets a value that indicates whether the named column contains a null value.</summary>
	/// <param name="columnName">The name of the column.</param>
	/// <returns>
	///   <see langword="true" /> if the column contains a null value; otherwise, <see langword="false" />.</returns>
	public bool IsNull(string columnName)
	{
		DataColumn dataColumn = GetDataColumn(columnName);
		int defaultRecord = GetDefaultRecord();
		return dataColumn.IsNull(defaultRecord);
	}

	/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> contains a null value.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" />.</param>
	/// <returns>
	///   <see langword="true" /> if the column contains a null value; otherwise, <see langword="false" />.</returns>
	public bool IsNull(DataColumn column)
	{
		CheckColumn(column);
		int defaultRecord = GetDefaultRecord();
		return column.IsNull(defaultRecord);
	}

	/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> and <see cref="T:System.Data.DataRowVersion" /> contains a null value.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" />.</param>
	/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. Possible values are <see langword="Default" />, <see langword="Original" />, <see langword="Current" />, and <see langword="Proposed" />.</param>
	/// <returns>
	///   <see langword="true" /> if the column contains a null value; otherwise, <see langword="false" />.</returns>
	public bool IsNull(DataColumn column, DataRowVersion version)
	{
		CheckColumn(column);
		int recordFromVersion = GetRecordFromVersion(version);
		return column.IsNull(recordFromVersion);
	}

	/// <summary>Rejects all changes made to the row since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
	/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table.</exception>
	public void RejectChanges()
	{
		long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataRow.RejectChanges|API> {0}", _objectID);
		try
		{
			if (RowState != DataRowState.Detached)
			{
				DataColumn[] columnsImplementingIChangeTracking;
				if (_columns.ColumnsImplementingIChangeTrackingCount != _columns.ColumnsImplementingIRevertibleChangeTrackingCount)
				{
					columnsImplementingIChangeTracking = _columns.ColumnsImplementingIChangeTracking;
					foreach (DataColumn dataColumn in columnsImplementingIChangeTracking)
					{
						if (!dataColumn.ImplementsIRevertibleChangeTracking)
						{
							object obj = null;
							obj = ((RowState == DataRowState.Deleted) ? this[dataColumn, DataRowVersion.Original] : this[dataColumn]);
							if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
							{
								throw ExceptionBuilder.UDTImplementsIChangeTrackingButnotIRevertible(dataColumn.DataType.AssemblyQualifiedName);
							}
						}
					}
				}
				columnsImplementingIChangeTracking = _columns.ColumnsImplementingIChangeTracking;
				foreach (DataColumn column in columnsImplementingIChangeTracking)
				{
					object obj2 = null;
					obj2 = ((RowState == DataRowState.Deleted) ? this[column, DataRowVersion.Original] : this[column]);
					if (DBNull.Value != obj2 && ((IChangeTracking)obj2).IsChanged)
					{
						((IRevertibleChangeTracking)obj2).RejectChanges();
					}
				}
			}
			_table.RollbackRow(this);
		}
		finally
		{
			DataCommonEventSource.Log.ExitScope(scopeId);
		}
	}

	internal void ResetLastChangedColumn()
	{
		_lastChangedColumn = null;
		_countColumnChange = 0;
	}

	internal void SetKeyValues(DataKey key, object[] keyValues)
	{
		bool flag = true;
		bool flag2 = _tempRecord == -1;
		for (int i = 0; i < keyValues.Length; i++)
		{
			if (!this[key.ColumnsReference[i]].Equals(keyValues[i]))
			{
				if (flag2 && flag)
				{
					flag = false;
					BeginEditInternal();
				}
				this[key.ColumnsReference[i]] = keyValues[i];
			}
		}
		if (!flag)
		{
			EndEdit();
		}
	}

	/// <summary>Sets the value of the specified <see cref="T:System.Data.DataColumn" /> to a null value.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" />.</param>
	protected void SetNull(DataColumn column)
	{
		this[column] = DBNull.Value;
	}

	internal void SetNestedParentRow(DataRow parentRow, bool setNonNested)
	{
		if (parentRow == null)
		{
			SetParentRowToDBNull();
			return;
		}
		foreach (DataRelation parentRelation in _table.ParentRelations)
		{
			if (!(parentRelation.Nested || setNonNested) || parentRelation.ParentKey.Table != parentRow._table)
			{
				continue;
			}
			object[] keyValues = parentRow.GetKeyValues(parentRelation.ParentKey);
			SetKeyValues(parentRelation.ChildKey, keyValues);
			if (parentRelation.Nested)
			{
				if (parentRow._table == _table)
				{
					CheckForLoops(parentRelation);
				}
				else
				{
					GetParentRow(parentRelation);
				}
			}
		}
	}

	/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" />.</summary>
	/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />.</param>
	public void SetParentRow(DataRow parentRow)
	{
		SetNestedParentRow(parentRow, setNonNested: true);
	}

	/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" /> and <see cref="T:System.Data.DataRelation" />.</summary>
	/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />.</param>
	/// <param name="relation">The relation <see cref="T:System.Data.DataRelation" /> to use.</param>
	/// <exception cref="T:System.Data.RowNotInTableException">One of the rows does not belong to a table</exception>
	/// <exception cref="T:System.ArgumentNullException">One of the rows is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The relation does not belong to the <see cref="T:System.Data.DataRelationCollection" /> of the <see cref="T:System.Data.DataSet" /> object.</exception>
	/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child <see cref="T:System.Data.DataTable" /> is not the table this row belongs to.</exception>
	public void SetParentRow(DataRow parentRow, DataRelation relation)
	{
		if (relation == null)
		{
			SetParentRow(parentRow);
			return;
		}
		if (parentRow == null)
		{
			SetParentRowToDBNull(relation);
			return;
		}
		if (_table.DataSet != parentRow._table.DataSet)
		{
			throw ExceptionBuilder.ParentRowNotInTheDataSet();
		}
		if (relation.ChildKey.Table != _table)
		{
			throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, _table.TableName);
		}
		if (relation.ParentKey.Table != parentRow._table)
		{
			throw ExceptionBuilder.SetParentRowTableMismatch(relation.ParentKey.Table.TableName, parentRow._table.TableName);
		}
		object[] keyValues = parentRow.GetKeyValues(relation.ParentKey);
		SetKeyValues(relation.ChildKey, keyValues);
	}

	internal void SetParentRowToDBNull()
	{
		foreach (DataRelation parentRelation in _table.ParentRelations)
		{
			SetParentRowToDBNull(parentRelation);
		}
	}

	internal void SetParentRowToDBNull(DataRelation relation)
	{
		if (relation.ChildKey.Table != _table)
		{
			throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, _table.TableName);
		}
		SetKeyValues(keyValues: new object[1] { DBNull.Value }, key: relation.ChildKey);
	}

	/// <summary>Changes the <see cref="P:System.Data.DataRow.RowState" /> of a <see cref="T:System.Data.DataRow" /> to <see langword="Added" />.</summary>
	public void SetAdded()
	{
		if (RowState == DataRowState.Unchanged)
		{
			_table.SetOldRecord(this, -1);
			return;
		}
		throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
	}

	/// <summary>Changes the <see cref="P:System.Data.DataRow.RowState" /> of a <see cref="T:System.Data.DataRow" /> to <see langword="Modified" />.</summary>
	public void SetModified()
	{
		if (RowState == DataRowState.Unchanged)
		{
			_tempRecord = _table.NewRecord(_newRecord);
			if (_tempRecord != -1)
			{
				_table.SetNewRecord(this, _tempRecord, DataRowAction.Change, isInMerge: false, fireEvent: true, suppressEnsurePropertyChanged: true);
			}
			return;
		}
		throw ExceptionBuilder.SetAddedAndModifiedCalledOnnonUnchanged();
	}

	internal int CopyValuesIntoStore(ArrayList storeList, ArrayList nullbitList, int storeIndex)
	{
		int num = 0;
		if (_oldRecord != -1)
		{
			for (int i = 0; i < _columns.Count; i++)
			{
				_columns[i].CopyValueIntoStore(_oldRecord, storeList[i], (BitArray)nullbitList[i], storeIndex);
			}
			num++;
			storeIndex++;
		}
		DataRowState rowState = RowState;
		if (DataRowState.Added == rowState || DataRowState.Modified == rowState)
		{
			for (int j = 0; j < _columns.Count; j++)
			{
				_columns[j].CopyValueIntoStore(_newRecord, storeList[j], (BitArray)nullbitList[j], storeIndex);
			}
			num++;
			storeIndex++;
		}
		if (-1 != _tempRecord)
		{
			for (int k = 0; k < _columns.Count; k++)
			{
				_columns[k].CopyValueIntoStore(_tempRecord, storeList[k], (BitArray)nullbitList[k], storeIndex);
			}
			num++;
			storeIndex++;
		}
		return num;
	}

	[Conditional("DEBUG")]
	private void VerifyValueFromStorage(DataColumn column, DataRowVersion version, object valueFromStorage)
	{
		if (column.DataExpression != null && !_inChangingEvent && _tempRecord == -1 && _newRecord != -1 && version == DataRowVersion.Original && _oldRecord == _newRecord)
		{
			version = DataRowVersion.Current;
		}
	}
}
