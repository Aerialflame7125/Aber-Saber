using System.Collections;
using System.ComponentModel;

namespace System.Data;

/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
public class DataViewSettingCollection : ICollection, IEnumerable
{
	private sealed class DataViewSettingsEnumerator : IEnumerator
	{
		private DataViewSettingCollection _dataViewSettings;

		private IEnumerator _tableEnumerator;

		public object Current => _dataViewSettings[(DataTable)_tableEnumerator.Current];

		public DataViewSettingsEnumerator(DataViewManager dvm)
		{
			if (dvm.DataSet != null)
			{
				_dataViewSettings = dvm.DataViewSettings;
				_tableEnumerator = dvm.DataSet.Tables.GetEnumerator();
			}
			else
			{
				_dataViewSettings = null;
				_tableEnumerator = Array.Empty<DataTable>().GetEnumerator();
			}
		}

		public bool MoveNext()
		{
			return _tableEnumerator.MoveNext();
		}

		public void Reset()
		{
			_tableEnumerator.Reset();
		}
	}

	private readonly DataViewManager _dataViewManager;

	private readonly Hashtable _list = new Hashtable();

	/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection.</summary>
	/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find.</param>
	/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
	public virtual DataViewSetting this[DataTable table]
	{
		get
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			DataViewSetting dataViewSetting = (DataViewSetting)_list[table];
			if (dataViewSetting == null)
			{
				dataViewSetting = (this[table] = new DataViewSetting());
			}
			return dataViewSetting;
		}
		set
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			value.SetDataViewManager(_dataViewManager);
			value.SetDataTable(table);
			_list[table] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> of the <see cref="T:System.Data.DataTable" /> specified by its name.</summary>
	/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
	/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
	public virtual DataViewSetting this[string tableName]
	{
		get
		{
			DataTable table = GetTable(tableName);
			if (table != null)
			{
				return this[table];
			}
			return null;
		}
	}

	/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the <see cref="T:System.Data.DataTable" /> specified by its index.</summary>
	/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find.</param>
	/// <returns>A collection of <see cref="T:System.Data.DataViewSetting" /> objects.</returns>
	public virtual DataViewSetting this[int index]
	{
		get
		{
			DataTable table = GetTable(index);
			if (table != null)
			{
				return this[table];
			}
			return null;
		}
		set
		{
			DataTable table = GetTable(index);
			if (table != null)
			{
				this[table] = value;
			}
		}
	}

	/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
	/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
	[Browsable(false)]
	public virtual int Count => _dataViewManager.DataSet?.Tables.Count ?? 0;

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataViewSettingCollection" /> is read-only.</summary>
	/// <returns>Always returns <see langword="true" /> to indicate the collection is read-only.</returns>
	[Browsable(false)]
	public bool IsReadOnly => true;

	/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
	/// <returns>This property is always <see langword="false" />, unless overridden by a derived class.</returns>
	[Browsable(false)]
	public bool IsSynchronized => false;

	/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
	[Browsable(false)]
	public object SyncRoot => this;

	internal DataViewSettingCollection(DataViewManager dataViewManager)
	{
		if (dataViewManager == null)
		{
			throw ExceptionBuilder.ArgumentNull("dataViewManager");
		}
		_dataViewManager = dataViewManager;
	}

	private DataTable GetTable(string tableName)
	{
		DataTable result = null;
		DataSet dataSet = _dataViewManager.DataSet;
		if (dataSet != null)
		{
			result = dataSet.Tables[tableName];
		}
		return result;
	}

	private DataTable GetTable(int index)
	{
		DataTable result = null;
		DataSet dataSet = _dataViewManager.DataSet;
		if (dataSet != null)
		{
			result = dataSet.Tables[index];
		}
		return result;
	}

	/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
	/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
	/// <param name="index">The index of the array at which to start inserting.</param>
	public void CopyTo(Array ar, int index)
	{
		IEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			ar.SetValue(enumerator.Current, index++);
		}
	}

	/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
	/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
	/// <param name="index">The index of the array at which to start inserting.</param>
	public void CopyTo(DataViewSetting[] ar, int index)
	{
		IEnumerator enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			ar.SetValue(enumerator.Current, index++);
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
	public IEnumerator GetEnumerator()
	{
		return new DataViewSettingsEnumerator(_dataViewManager);
	}

	internal void Remove(DataTable table)
	{
		_list.Remove(table);
	}
}
