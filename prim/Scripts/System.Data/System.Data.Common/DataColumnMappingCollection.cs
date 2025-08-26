using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace System.Data.Common;

/// <summary>Contains a collection of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
public sealed class DataColumnMappingCollection : MarshalByRefObject, IColumnMappingCollection, IList, ICollection, IEnumerable
{
	private List<DataColumnMapping> _items;

	/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
	/// <returns>
	///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
	object ICollection.SyncRoot => this;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> is read-only.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> is read-only; otherwise, <see langword="false" />.</returns>
	bool IList.IsReadOnly => false;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IList" /> has a fixed size.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, <see langword="false" />.</returns>
	bool IList.IsFixedSize => false;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element to get or set.</param>
	/// <returns>The element at the specified index.</returns>
	object IList.this[int index]
	{
		get
		{
			return this[index];
		}
		set
		{
			ValidateType(value);
			this[index] = (DataColumnMapping)value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Data.IColumnMapping" /> object with the specified <see langword="SourceColumn" /> name.</summary>
	/// <param name="index">Index of the element.</param>
	/// <returns>The <see langword="IColumnMapping" /> object with the specified <see langword="SourceColumn" /> name.</returns>
	object IColumnMappingCollection.this[string index]
	{
		get
		{
			return this[index];
		}
		set
		{
			ValidateType(value);
			this[index] = (DataColumnMapping)value;
		}
	}

	/// <summary>Gets the number of <see cref="T:System.Data.Common.DataColumnMapping" /> objects in the collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Count
	{
		get
		{
			if (_items == null)
			{
				return 0;
			}
			return _items.Count;
		}
	}

	private Type ItemType => typeof(DataColumnMapping);

	/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to find.</param>
	/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataColumnMapping this[int index]
	{
		get
		{
			RangeCheck(index);
			return _items[index];
		}
		set
		{
			RangeCheck(index);
			Replace(index, value);
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</summary>
	/// <param name="sourceColumn">The case-sensitive name of the source column.</param>
	/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataColumnMapping this[string sourceColumn]
	{
		get
		{
			int index = RangeCheck(sourceColumn);
			return _items[index];
		}
		set
		{
			int index = RangeCheck(sourceColumn);
			Replace(index, value);
		}
	}

	/// <summary>Creates an empty <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</summary>
	public DataColumnMappingCollection()
	{
	}

	/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> by using the source column and <see cref="T:System.Data.DataSet" /> column names.</summary>
	/// <param name="sourceColumnName">The case-sensitive name of the source column.</param>
	/// <param name="dataSetColumnName">The name of the <see cref="T:System.Data.DataSet" /> column.</param>
	/// <returns>The ColumnMapping object that was added to the collection.</returns>
	IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
	{
		return Add(sourceColumnName, dataSetColumnName);
	}

	/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object that has the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
	/// <param name="dataSetColumnName">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find.</param>
	/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object that  has the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
	IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
	{
		return GetByDataSetColumn(dataSetColumnName);
	}

	/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection.</summary>
	/// <param name="value">A <see langword="DataColumnMapping" /> object to add to the collection.</param>
	/// <returns>The index of the <see langword="DataColumnMapping" /> object that was added to the collection.</returns>
	/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
	public int Add(object value)
	{
		ValidateType(value);
		Add((DataColumnMapping)value);
		return Count - 1;
	}

	private DataColumnMapping Add(DataColumnMapping value)
	{
		AddWithoutEvents(value);
		return value;
	}

	/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection when given a source column name and a <see cref="T:System.Data.DataSet" /> column name.</summary>
	/// <param name="sourceColumn">The case-sensitive name of the source column to map to.</param>
	/// <param name="dataSetColumn">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to map to.</param>
	/// <returns>The <see langword="DataColumnMapping" /> object that was added to the collection.</returns>
	public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
	{
		return Add(new DataColumnMapping(sourceColumn, dataSetColumn));
	}

	/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array to the end of the collection.</summary>
	/// <param name="values">The array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects to add to the collection.</param>
	public void AddRange(DataColumnMapping[] values)
	{
		AddEnumerableRange(values, doClone: false);
	}

	/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
	/// <param name="values">The <see cref="T:System.Array" /> to add to the collection.</param>
	public void AddRange(Array values)
	{
		AddEnumerableRange(values, doClone: false);
	}

	private void AddEnumerableRange(IEnumerable values, bool doClone)
	{
		if (values == null)
		{
			throw ADP.ArgumentNull("values");
		}
		foreach (object value2 in values)
		{
			ValidateType(value2);
		}
		if (doClone)
		{
			foreach (ICloneable value3 in values)
			{
				AddWithoutEvents(value3.Clone() as DataColumnMapping);
			}
			return;
		}
		foreach (DataColumnMapping value4 in values)
		{
			AddWithoutEvents(value4);
		}
	}

	private void AddWithoutEvents(DataColumnMapping value)
	{
		Validate(-1, value);
		value.Parent = this;
		ArrayList().Add(value);
	}

	private List<DataColumnMapping> ArrayList()
	{
		if (_items == null)
		{
			_items = new List<DataColumnMapping>();
		}
		return _items;
	}

	/// <summary>Removes all <see cref="T:System.Data.Common.DataColumnMapping" /> objects from the collection.</summary>
	public void Clear()
	{
		if (0 < Count)
		{
			ClearWithoutEvents();
		}
	}

	private void ClearWithoutEvents()
	{
		if (_items == null)
		{
			return;
		}
		foreach (DataColumnMapping item in _items)
		{
			item.Parent = null;
		}
		_items.Clear();
	}

	/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given source column name exists in the collection.</summary>
	/// <param name="value">The case-sensitive source column name of the <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
	/// <returns>
	///   <see langword="true" /> if collection contains a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name; otherwise, <see langword="false" />.</returns>
	public bool Contains(string value)
	{
		return -1 != IndexOf(value);
	}

	/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given <see cref="T:System.Object" /> exists in the collection.</summary>
	/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" />.</param>
	/// <returns>
	///   <see langword="true" /> if the collection contains the specified <see cref="T:System.Data.Common.DataColumnMapping" /> object; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
	public bool Contains(object value)
	{
		return -1 != IndexOf(value);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified array.</summary>
	/// <param name="array">An <see cref="T:System.Array" /> to which to copy <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
	/// <param name="index">The starting index of the array.</param>
	public void CopyTo(Array array, int index)
	{
		((ICollection)ArrayList()).CopyTo(array, index);
	}

	/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array.</summary>
	/// <param name="array">A <see cref="T:System.Data.Common.DataColumnMapping" /> array to which to copy the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
	/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(DataColumnMapping[] array, int index)
	{
		ArrayList().CopyTo(array, index);
	}

	/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
	/// <param name="value">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find.</param>
	/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
	public DataColumnMapping GetByDataSetColumn(string value)
	{
		int num = IndexOfDataSetColumn(value);
		if (0 > num)
		{
			throw ADP.ColumnsDataSetColumn(value);
		}
		return _items[num];
	}

	/// <summary>Gets an enumerator that can iterate through the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return ArrayList().GetEnumerator();
	}

	/// <summary>Gets the location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</summary>
	/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to find.</param>
	/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</returns>
	public int IndexOf(object value)
	{
		if (value != null)
		{
			ValidateType(value);
			for (int i = 0; i < Count; i++)
			{
				if (_items[i] == value)
				{
					return i;
				}
			}
		}
		return -1;
	}

	/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified source column name.</summary>
	/// <param name="sourceColumn">The case-sensitive name of the source column.</param>
	/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified case-sensitive source column name.</returns>
	public int IndexOf(string sourceColumn)
	{
		if (!string.IsNullOrEmpty(sourceColumn))
		{
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				if (ADP.SrcCompare(sourceColumn, _items[i].SourceColumn) == 0)
				{
					return i;
				}
			}
		}
		return -1;
	}

	/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see cref="T:System.Data.DataSet" /> column name.</summary>
	/// <param name="dataSetColumn">The name, which is not case-sensitive, of the data set column to find.</param>
	/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see langword="DataSet" /> column name, or -1 if the <see langword="DataColumnMapping" /> object does not exist in the collection.</returns>
	public int IndexOfDataSetColumn(string dataSetColumn)
	{
		if (!string.IsNullOrEmpty(dataSetColumn))
		{
			int count = Count;
			for (int i = 0; i < count; i++)
			{
				if (ADP.DstCompare(dataSetColumn, _items[i].DataSetColumn) == 0)
				{
					return i;
				}
			}
		}
		return -1;
	}

	/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
	/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
	public void Insert(int index, object value)
	{
		ValidateType(value);
		Insert(index, (DataColumnMapping)value);
	}

	/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
	/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
	public void Insert(int index, DataColumnMapping value)
	{
		if (value == null)
		{
			throw ADP.ColumnsAddNullAttempt("value");
		}
		Validate(-1, value);
		value.Parent = this;
		ArrayList().Insert(index, value);
	}

	private void RangeCheck(int index)
	{
		if (index < 0 || Count <= index)
		{
			throw ADP.ColumnsIndexInt32(index, this);
		}
	}

	private int RangeCheck(string sourceColumn)
	{
		int num = IndexOf(sourceColumn);
		if (num < 0)
		{
			throw ADP.ColumnsIndexSource(sourceColumn);
		}
		return num;
	}

	/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index from the collection.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to remove.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index.</exception>
	public void RemoveAt(int index)
	{
		RangeCheck(index);
		RemoveIndex(index);
	}

	/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name from the collection.</summary>
	/// <param name="sourceColumn">The case-sensitive source column name.</param>
	/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</exception>
	public void RemoveAt(string sourceColumn)
	{
		int index = RangeCheck(sourceColumn);
		RemoveIndex(index);
	}

	private void RemoveIndex(int index)
	{
		_items[index].Parent = null;
		_items.RemoveAt(index);
	}

	/// <summary>Removes the <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
	/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</exception>
	/// <exception cref="T:System.ArgumentException">The object specified is not in the collection.</exception>
	public void Remove(object value)
	{
		ValidateType(value);
		Remove((DataColumnMapping)value);
	}

	/// <summary>Removes the specified <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
	/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
	public void Remove(DataColumnMapping value)
	{
		if (value == null)
		{
			throw ADP.ColumnsAddNullAttempt("value");
		}
		int num = IndexOf(value);
		if (-1 != num)
		{
			RemoveIndex(num);
			return;
		}
		throw ADP.CollectionRemoveInvalidObject(ItemType, this);
	}

	private void Replace(int index, DataColumnMapping newValue)
	{
		Validate(index, newValue);
		_items[index].Parent = null;
		newValue.Parent = this;
		_items[index] = newValue;
	}

	private void ValidateType(object value)
	{
		if (value == null)
		{
			throw ADP.ColumnsAddNullAttempt("value");
		}
		if (!ItemType.IsInstanceOfType(value))
		{
			throw ADP.NotADataColumnMapping(value);
		}
	}

	private void Validate(int index, DataColumnMapping value)
	{
		if (value == null)
		{
			throw ADP.ColumnsAddNullAttempt("value");
		}
		if (value.Parent != null)
		{
			if (this != value.Parent)
			{
				throw ADP.ColumnsIsNotParent(this);
			}
			if (index != IndexOf(value))
			{
				throw ADP.ColumnsIsParent(this);
			}
		}
		string sourceColumn = value.SourceColumn;
		if (string.IsNullOrEmpty(sourceColumn))
		{
			index = 1;
			do
			{
				sourceColumn = "SourceColumn" + index.ToString(CultureInfo.InvariantCulture);
				index++;
			}
			while (-1 != IndexOf(sourceColumn));
			value.SourceColumn = sourceColumn;
		}
		else
		{
			ValidateSourceColumn(index, sourceColumn);
		}
	}

	internal void ValidateSourceColumn(int index, string value)
	{
		int num = IndexOf(value);
		if (-1 != num && index != num)
		{
			throw ADP.ColumnsUniqueSourceColumn(value);
		}
	}

	/// <summary>A static method that returns a <see cref="T:System.Data.DataColumn" /> object without instantiating a <see cref="T:System.Data.Common.DataColumnMappingCollection" /> object.</summary>
	/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
	/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
	/// <param name="dataType">The data type for the column being mapped.</param>
	/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
	/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
	/// <param name="schemaAction">Determines the action to take when the existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
	/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
	{
		if (columnMappings != null)
		{
			int num = columnMappings.IndexOf(sourceColumn);
			if (-1 != num)
			{
				return columnMappings._items[num].GetDataColumnBySchemaAction(dataTable, dataType, schemaAction);
			}
		}
		if (string.IsNullOrEmpty(sourceColumn))
		{
			throw ADP.InvalidSourceColumn("sourceColumn");
		}
		return mappingAction switch
		{
			MissingMappingAction.Passthrough => DataColumnMapping.GetDataColumnBySchemaAction(sourceColumn, sourceColumn, dataTable, dataType, schemaAction), 
			MissingMappingAction.Ignore => null, 
			MissingMappingAction.Error => throw ADP.MissingColumnMapping(sourceColumn), 
			_ => throw ADP.InvalidMissingMappingAction(mappingAction), 
		};
	}

	/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> for the specified <see cref="T:System.Data.Common.DataColumnMappingCollection" />, source column name, and <see cref="T:System.Data.MissingMappingAction" />.</summary>
	/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
	/// <param name="sourceColumn">The case-sensitive source column name to find.</param>
	/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
	/// <returns>A <see cref="T:System.Data.Common.DataColumnMapping" /> object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to <see langword="Error" />, and no mapping was specified.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction)
	{
		if (columnMappings != null)
		{
			int num = columnMappings.IndexOf(sourceColumn);
			if (-1 != num)
			{
				return columnMappings._items[num];
			}
		}
		if (string.IsNullOrEmpty(sourceColumn))
		{
			throw ADP.InvalidSourceColumn("sourceColumn");
		}
		return mappingAction switch
		{
			MissingMappingAction.Passthrough => new DataColumnMapping(sourceColumn, sourceColumn), 
			MissingMappingAction.Ignore => null, 
			MissingMappingAction.Error => throw ADP.MissingColumnMapping(sourceColumn), 
			_ => throw ADP.InvalidMissingMappingAction(mappingAction), 
		};
	}
}
