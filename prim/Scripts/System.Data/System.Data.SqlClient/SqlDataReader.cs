using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient;

/// <summary>Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.</summary>
public class SqlDataReader : DbDataReader, IDataReader, IDisposable, IDataRecord, IDbColumnSchemaGenerator
{
	private enum ALTROWSTATUS
	{
		Null,
		AltRow,
		Done
	}

	internal class SharedState
	{
		internal int _nextColumnHeaderToRead;

		internal int _nextColumnDataToRead;

		internal long _columnDataBytesRemaining;

		internal bool _dataReady;
	}

	private class Snapshot
	{
		public bool _dataReady;

		public bool _haltRead;

		public bool _metaDataConsumed;

		public bool _browseModeInfoConsumed;

		public bool _hasRows;

		public ALTROWSTATUS _altRowStatus;

		public int _nextColumnDataToRead;

		public int _nextColumnHeaderToRead;

		public long _columnDataBytesRead;

		public long _columnDataBytesRemaining;

		public _SqlMetaDataSet _metadata;

		public _SqlMetaDataSetCollection _altMetaDataSetCollection;

		public MultiPartTableName[] _tableNames;

		public SqlSequentialStream _currentStream;

		public SqlSequentialTextReader _currentTextReader;
	}

	internal SharedState _sharedState = new SharedState();

	private TdsParser _parser;

	private TdsParserStateObject _stateObj;

	private SqlCommand _command;

	private SqlConnection _connection;

	private int _defaultLCID;

	private bool _haltRead;

	private bool _metaDataConsumed;

	private bool _browseModeInfoConsumed;

	private bool _isClosed;

	private bool _isInitialized;

	private bool _hasRows;

	private ALTROWSTATUS _altRowStatus;

	private int _recordsAffected = -1;

	private long _defaultTimeoutMilliseconds;

	private SqlConnectionString.TypeSystem _typeSystem;

	private SqlStatistics _statistics;

	private SqlBuffer[] _data;

	private SqlStreamingXml _streamingXml;

	private _SqlMetaDataSet _metaData;

	private _SqlMetaDataSetCollection _altMetaDataSetCollection;

	private FieldNameLookup _fieldNameLookup;

	private CommandBehavior _commandBehavior;

	private static int s_objectTypeCount;

	private static readonly ReadOnlyCollection<DbColumn> s_emptySchema = new ReadOnlyCollection<DbColumn>(Array.Empty<DbColumn>());

	internal readonly int ObjectID = Interlocked.Increment(ref s_objectTypeCount);

	private MultiPartTableName[] _tableNames;

	private string _resetOptionsString;

	private int _lastColumnWithDataChunkRead;

	private long _columnDataBytesRead;

	private long _columnDataCharsRead;

	private char[] _columnDataChars;

	private int _columnDataCharsIndex;

	private Task _currentTask;

	private Snapshot _snapshot;

	private CancellationTokenSource _cancelAsyncOnCloseTokenSource;

	private CancellationToken _cancelAsyncOnCloseToken;

	internal static readonly Type _typeofINullable = typeof(INullable);

	private static readonly Type s_typeofSqlString = typeof(SqlString);

	private SqlSequentialStream _currentStream;

	private SqlSequentialTextReader _currentTextReader;

	internal bool BrowseModeInfoConsumed
	{
		set
		{
			_browseModeInfoConsumed = value;
		}
	}

	internal SqlCommand Command => _command;

	/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
	/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
	protected SqlConnection Connection => _connection;

	/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
	/// <returns>The depth of nesting for the current row.</returns>
	public override int Depth
	{
		get
		{
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("Depth");
			}
			return 0;
		}
	}

	/// <summary>Gets the number of columns in the current row.</summary>
	/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current row. The default is -1.</returns>
	/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server.</exception>
	public override int FieldCount
	{
		get
		{
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("FieldCount");
			}
			if (_currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			if (MetaData == null)
			{
				return 0;
			}
			return _metaData.Length;
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows; otherwise <see langword="false" />.</returns>
	public override bool HasRows
	{
		get
		{
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("HasRows");
			}
			if (_currentTask != null)
			{
				throw ADP.AsyncOperationPending();
			}
			return _hasRows;
		}
	}

	/// <summary>Retrieves a Boolean value that indicates whether the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance has been closed.</summary>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance is closed; otherwise <see langword="false" />.</returns>
	public override bool IsClosed => _isClosed;

	internal bool IsInitialized
	{
		get
		{
			return _isInitialized;
		}
		set
		{
			_isInitialized = value;
		}
	}

	internal _SqlMetaDataSet MetaData
	{
		get
		{
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("MetaData");
			}
			if (_metaData == null && !_metaDataConsumed)
			{
				if (_currentTask != null)
				{
					throw SQL.PendingBeginXXXExists();
				}
				if (!TryConsumeMetaData())
				{
					throw SQL.SynchronousCallMayNotPend();
				}
			}
			return _metaData;
		}
	}

	/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.</summary>
	/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
	public override int RecordsAffected
	{
		get
		{
			if (_command != null)
			{
				return _command.InternalRecordsAffected;
			}
			return _recordsAffected;
		}
	}

	internal string ResetOptionsString
	{
		set
		{
			_resetOptionsString = value;
		}
	}

	private SqlStatistics Statistics => _statistics;

	internal MultiPartTableName[] TableNames
	{
		get
		{
			return _tableNames;
		}
		set
		{
			_tableNames = value;
		}
	}

	/// <summary>Gets the number of fields in the <see cref="T:System.Data.SqlClient.SqlDataReader" /> that are not hidden.</summary>
	/// <returns>The number of fields that are not hidden.</returns>
	public override int VisibleFieldCount
	{
		get
		{
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("VisibleFieldCount");
			}
			return MetaData?.visibleColumns ?? 0;
		}
	}

	/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column in its native format.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />.</exception>
	public override object this[int i] => GetValue(i);

	/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
	/// <param name="name">The column name.</param>
	/// <returns>The value of the specified column in its native format.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found.</exception>
	public override object this[string name] => GetValue(GetOrdinal(name));

	internal SqlDataReader(SqlCommand command, CommandBehavior behavior)
	{
		_command = command;
		_commandBehavior = behavior;
		if (_command != null)
		{
			_defaultTimeoutMilliseconds = (long)command.CommandTimeout * 1000L;
			_connection = command.Connection;
			if (_connection != null)
			{
				_statistics = _connection.Statistics;
				_typeSystem = _connection.TypeSystem;
			}
		}
		_sharedState._dataReady = false;
		_metaDataConsumed = false;
		_hasRows = false;
		_browseModeInfoConsumed = false;
		_currentStream = null;
		_currentTextReader = null;
		_cancelAsyncOnCloseTokenSource = new CancellationTokenSource();
		_cancelAsyncOnCloseToken = _cancelAsyncOnCloseTokenSource.Token;
		_columnDataCharsIndex = -1;
	}

	internal long ColumnDataBytesRemaining()
	{
		if (-1 == _sharedState._columnDataBytesRemaining)
		{
			_sharedState._columnDataBytesRemaining = (long)_parser.PlpBytesLeft(_stateObj);
		}
		return _sharedState._columnDataBytesRemaining;
	}

	internal virtual SmiExtendedMetaData[] GetInternalSmiMetaData()
	{
		SmiExtendedMetaData[] array = null;
		_SqlMetaDataSet metaData = MetaData;
		if (metaData != null && 0 < metaData.Length)
		{
			array = new SmiExtendedMetaData[metaData.visibleColumns];
			for (int i = 0; i < metaData.Length; i++)
			{
				_SqlMetaData sqlMetaData = metaData[i];
				if (!sqlMetaData.isHidden)
				{
					SqlCollation collation = sqlMetaData.collation;
					string typeSpecificNamePart = null;
					string typeSpecificNamePart2 = null;
					string typeSpecificNamePart3 = null;
					if (SqlDbType.Xml == sqlMetaData.type)
					{
						typeSpecificNamePart = sqlMetaData.xmlSchemaCollectionDatabase;
						typeSpecificNamePart2 = sqlMetaData.xmlSchemaCollectionOwningSchema;
						typeSpecificNamePart3 = sqlMetaData.xmlSchemaCollectionName;
					}
					else if (SqlDbType.Udt == sqlMetaData.type)
					{
						throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
					}
					int num = sqlMetaData.length;
					if (num > 8000)
					{
						num = -1;
					}
					else if (SqlDbType.NChar == sqlMetaData.type || SqlDbType.NVarChar == sqlMetaData.type)
					{
						num /= 2;
					}
					array[i] = new SmiQueryMetaData(sqlMetaData.type, num, sqlMetaData.precision, sqlMetaData.scale, collation?.LCID ?? _defaultLCID, collation?.SqlCompareOptions ?? SqlCompareOptions.None, isMultiValued: false, null, null, sqlMetaData.column, typeSpecificNamePart, typeSpecificNamePart2, typeSpecificNamePart3, sqlMetaData.isNullable, sqlMetaData.serverName, sqlMetaData.catalogName, sqlMetaData.schemaName, sqlMetaData.tableName, sqlMetaData.baseColumn, sqlMetaData.isKey, sqlMetaData.isIdentity, sqlMetaData.updatability == 0, sqlMetaData.isExpression, sqlMetaData.isDifferentName, sqlMetaData.isHidden);
				}
			}
		}
		return array;
	}

	internal void Bind(TdsParserStateObject stateObj)
	{
		stateObj.Owner = this;
		_stateObj = stateObj;
		_parser = stateObj.Parser;
		_defaultLCID = _parser.DefaultLCID;
	}

	internal DataTable BuildSchemaTable()
	{
		_SqlMetaDataSet metaData = MetaData;
		DataTable dataTable = new DataTable("SchemaTable");
		dataTable.Locale = CultureInfo.InvariantCulture;
		dataTable.MinimumCapacity = metaData.Length;
		DataColumn column = new DataColumn(SchemaTableColumn.ColumnName, typeof(string));
		DataColumn dataColumn = new DataColumn(SchemaTableColumn.ColumnOrdinal, typeof(int));
		DataColumn column2 = new DataColumn(SchemaTableColumn.ColumnSize, typeof(int));
		DataColumn column3 = new DataColumn(SchemaTableColumn.NumericPrecision, typeof(short));
		DataColumn column4 = new DataColumn(SchemaTableColumn.NumericScale, typeof(short));
		DataColumn column5 = new DataColumn(SchemaTableColumn.DataType, typeof(Type));
		DataColumn column6 = new DataColumn(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
		DataColumn column7 = new DataColumn(SchemaTableColumn.NonVersionedProviderType, typeof(int));
		DataColumn column8 = new DataColumn(SchemaTableColumn.ProviderType, typeof(int));
		DataColumn dataColumn2 = new DataColumn(SchemaTableColumn.IsLong, typeof(bool));
		DataColumn column9 = new DataColumn(SchemaTableColumn.AllowDBNull, typeof(bool));
		DataColumn column10 = new DataColumn(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
		DataColumn column11 = new DataColumn(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
		DataColumn column12 = new DataColumn(SchemaTableColumn.IsUnique, typeof(bool));
		DataColumn column13 = new DataColumn(SchemaTableColumn.IsKey, typeof(bool));
		DataColumn column14 = new DataColumn(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
		DataColumn column15 = new DataColumn(SchemaTableOptionalColumn.IsHidden, typeof(bool));
		DataColumn column16 = new DataColumn(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
		DataColumn column17 = new DataColumn(SchemaTableColumn.BaseSchemaName, typeof(string));
		DataColumn column18 = new DataColumn(SchemaTableColumn.BaseTableName, typeof(string));
		DataColumn column19 = new DataColumn(SchemaTableColumn.BaseColumnName, typeof(string));
		DataColumn column20 = new DataColumn(SchemaTableOptionalColumn.BaseServerName, typeof(string));
		DataColumn column21 = new DataColumn(SchemaTableColumn.IsAliased, typeof(bool));
		DataColumn column22 = new DataColumn(SchemaTableColumn.IsExpression, typeof(bool));
		DataColumn column23 = new DataColumn("IsIdentity", typeof(bool));
		DataColumn column24 = new DataColumn("DataTypeName", typeof(string));
		DataColumn column25 = new DataColumn("UdtAssemblyQualifiedName", typeof(string));
		DataColumn column26 = new DataColumn("XmlSchemaCollectionDatabase", typeof(string));
		DataColumn column27 = new DataColumn("XmlSchemaCollectionOwningSchema", typeof(string));
		DataColumn column28 = new DataColumn("XmlSchemaCollectionName", typeof(string));
		DataColumn column29 = new DataColumn("IsColumnSet", typeof(bool));
		dataColumn.DefaultValue = 0;
		dataColumn2.DefaultValue = false;
		DataColumnCollection columns = dataTable.Columns;
		columns.Add(column);
		columns.Add(dataColumn);
		columns.Add(column2);
		columns.Add(column3);
		columns.Add(column4);
		columns.Add(column12);
		columns.Add(column13);
		columns.Add(column20);
		columns.Add(column16);
		columns.Add(column19);
		columns.Add(column17);
		columns.Add(column18);
		columns.Add(column5);
		columns.Add(column9);
		columns.Add(column8);
		columns.Add(column21);
		columns.Add(column22);
		columns.Add(column23);
		columns.Add(column14);
		columns.Add(column11);
		columns.Add(column15);
		columns.Add(dataColumn2);
		columns.Add(column10);
		columns.Add(column6);
		columns.Add(column24);
		columns.Add(column26);
		columns.Add(column27);
		columns.Add(column28);
		columns.Add(column25);
		columns.Add(column7);
		columns.Add(column29);
		for (int i = 0; i < metaData.Length; i++)
		{
			_SqlMetaData sqlMetaData = metaData[i];
			DataRow dataRow = dataTable.NewRow();
			dataRow[column] = sqlMetaData.column;
			dataRow[dataColumn] = sqlMetaData.ordinal;
			dataRow[column2] = ((sqlMetaData.metaType.IsSizeInCharacters && sqlMetaData.length != int.MaxValue) ? (sqlMetaData.length / 2) : sqlMetaData.length);
			dataRow[column5] = GetFieldTypeInternal(sqlMetaData);
			dataRow[column6] = GetProviderSpecificFieldTypeInternal(sqlMetaData);
			dataRow[column7] = (int)sqlMetaData.type;
			dataRow[column24] = GetDataTypeNameInternal(sqlMetaData);
			if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
			{
				dataRow[column8] = SqlDbType.NVarChar;
				switch (sqlMetaData.type)
				{
				case SqlDbType.Date:
					dataRow[column2] = 10;
					break;
				case SqlDbType.Time:
					dataRow[column2] = TdsEnums.WHIDBEY_TIME_LENGTH[(byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale];
					break;
				case SqlDbType.DateTime2:
					dataRow[column2] = TdsEnums.WHIDBEY_DATETIME2_LENGTH[(byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale];
					break;
				case SqlDbType.DateTimeOffset:
					dataRow[column2] = TdsEnums.WHIDBEY_DATETIMEOFFSET_LENGTH[(byte.MaxValue != sqlMetaData.scale) ? sqlMetaData.scale : sqlMetaData.metaType.Scale];
					break;
				}
			}
			else if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsLargeUdt)
			{
				if (_typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
				{
					dataRow[column8] = SqlDbType.VarBinary;
				}
				else
				{
					dataRow[column8] = SqlDbType.Image;
				}
			}
			else if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
			{
				dataRow[column8] = (int)sqlMetaData.type;
				if (sqlMetaData.type == SqlDbType.Udt)
				{
					dataRow[column25] = sqlMetaData.udtAssemblyQualifiedName;
				}
				else if (sqlMetaData.type == SqlDbType.Xml)
				{
					dataRow[column26] = sqlMetaData.xmlSchemaCollectionDatabase;
					dataRow[column27] = sqlMetaData.xmlSchemaCollectionOwningSchema;
					dataRow[column28] = sqlMetaData.xmlSchemaCollectionName;
				}
			}
			else
			{
				dataRow[column8] = GetVersionedMetaType(sqlMetaData.metaType).SqlDbType;
			}
			if (byte.MaxValue != sqlMetaData.precision)
			{
				dataRow[column3] = sqlMetaData.precision;
			}
			else
			{
				dataRow[column3] = sqlMetaData.metaType.Precision;
			}
			if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
			{
				dataRow[column4] = MetaType.MetaNVarChar.Scale;
			}
			else if (byte.MaxValue != sqlMetaData.scale)
			{
				dataRow[column4] = sqlMetaData.scale;
			}
			else
			{
				dataRow[column4] = sqlMetaData.metaType.Scale;
			}
			dataRow[column9] = sqlMetaData.isNullable;
			if (_browseModeInfoConsumed)
			{
				dataRow[column21] = sqlMetaData.isDifferentName;
				dataRow[column13] = sqlMetaData.isKey;
				dataRow[column15] = sqlMetaData.isHidden;
				dataRow[column22] = sqlMetaData.isExpression;
			}
			dataRow[column23] = sqlMetaData.isIdentity;
			dataRow[column14] = sqlMetaData.isIdentity;
			dataRow[dataColumn2] = sqlMetaData.metaType.IsLong;
			if (SqlDbType.Timestamp == sqlMetaData.type)
			{
				dataRow[column12] = true;
				dataRow[column11] = true;
			}
			else
			{
				dataRow[column12] = false;
				dataRow[column11] = false;
			}
			dataRow[column10] = sqlMetaData.updatability == 0;
			dataRow[column29] = sqlMetaData.isColumnSet;
			if (!string.IsNullOrEmpty(sqlMetaData.serverName))
			{
				dataRow[column20] = sqlMetaData.serverName;
			}
			if (!string.IsNullOrEmpty(sqlMetaData.catalogName))
			{
				dataRow[column16] = sqlMetaData.catalogName;
			}
			if (!string.IsNullOrEmpty(sqlMetaData.schemaName))
			{
				dataRow[column17] = sqlMetaData.schemaName;
			}
			if (!string.IsNullOrEmpty(sqlMetaData.tableName))
			{
				dataRow[column18] = sqlMetaData.tableName;
			}
			if (!string.IsNullOrEmpty(sqlMetaData.baseColumn))
			{
				dataRow[column19] = sqlMetaData.baseColumn;
			}
			else if (!string.IsNullOrEmpty(sqlMetaData.column))
			{
				dataRow[column19] = sqlMetaData.column;
			}
			dataTable.Rows.Add(dataRow);
			dataRow.AcceptChanges();
		}
		foreach (DataColumn item in columns)
		{
			item.ReadOnly = true;
		}
		return dataTable;
	}

	internal void Cancel(SqlCommand command)
	{
		_stateObj?.Cancel(command);
	}

	private bool TryCleanPartialRead()
	{
		if (_stateObj._partialHeaderBytesRead > 0 && !_stateObj.TryProcessHeader())
		{
			return false;
		}
		if (-1 != _lastColumnWithDataChunkRead)
		{
			CloseActiveSequentialStreamAndTextReader();
		}
		if (_sharedState._nextColumnHeaderToRead == 0)
		{
			if (!_stateObj.Parser.TrySkipRow(_metaData, _stateObj))
			{
				return false;
			}
		}
		else
		{
			if (!TryResetBlobState())
			{
				return false;
			}
			if (!_stateObj.Parser.TrySkipRow(_metaData, _sharedState._nextColumnHeaderToRead, _stateObj))
			{
				return false;
			}
		}
		_sharedState._dataReady = false;
		return true;
	}

	private void CleanPartialReadReliable()
	{
		TryCleanPartialRead();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
		base.Dispose(disposing);
	}

	/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</summary>
	public override void Close()
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			TdsParserStateObject stateObj = _stateObj;
			_cancelAsyncOnCloseTokenSource.Cancel();
			Task currentTask = _currentTask;
			if (currentTask != null && !currentTask.IsCompleted)
			{
				try
				{
					((IAsyncResult)currentTask).AsyncWaitHandle.WaitOne();
					((IAsyncResult)stateObj._networkPacketTaskSource?.Task).AsyncWaitHandle.WaitOne();
				}
				catch (Exception)
				{
					_connection.InnerConnection.DoomThisConnection();
					_isClosed = true;
					if (stateObj != null)
					{
						lock (stateObj)
						{
							_stateObj = null;
							_command = null;
							_connection = null;
						}
					}
					throw;
				}
			}
			CloseActiveSequentialStreamAndTextReader();
			if (stateObj == null)
			{
				return;
			}
			lock (stateObj)
			{
				if (_stateObj != null)
				{
					if (_snapshot != null)
					{
						PrepareForAsyncContinuation();
					}
					SetTimeout(_defaultTimeoutMilliseconds);
					stateObj._syncOverAsync = true;
					if (!TryCloseInternal(closeReader: true))
					{
						throw SQL.SynchronousCallMayNotPend();
					}
				}
			}
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private bool TryCloseInternal(bool closeReader)
	{
		TdsParser parser = _parser;
		TdsParserStateObject stateObj = _stateObj;
		bool flag = IsCommandBehavior(CommandBehavior.CloseConnection);
		bool flag2 = false;
		bool flag3 = false;
		try
		{
			if (!_isClosed && parser != null && stateObj != null && stateObj._pendingData && parser.State == TdsParserState.OpenLoggedIn)
			{
				if (_altRowStatus == ALTROWSTATUS.AltRow)
				{
					_sharedState._dataReady = true;
				}
				_stateObj._internalTimeout = false;
				if (_sharedState._dataReady)
				{
					flag3 = true;
					if (!TryCleanPartialRead())
					{
						return false;
					}
					flag3 = false;
				}
				if (!parser.TryRun(RunBehavior.Clean, _command, this, null, stateObj, out var _))
				{
					return false;
				}
			}
			RestoreServerSettings(parser, stateObj);
			return true;
		}
		finally
		{
			if (flag2)
			{
				_isClosed = true;
				_command = null;
				_connection = null;
				_statistics = null;
				_stateObj = null;
				_parser = null;
			}
			else if (closeReader)
			{
				bool isClosed = _isClosed;
				_isClosed = true;
				_parser = null;
				_stateObj = null;
				_data = null;
				if (_snapshot != null)
				{
					CleanupAfterAsyncInvocationInternal(stateObj);
				}
				if (Connection != null)
				{
					Connection.RemoveWeakReference(this);
				}
				if (!isClosed && stateObj != null)
				{
					if (!flag3)
					{
						stateObj.CloseSession();
					}
					else if (parser != null)
					{
						parser.State = TdsParserState.Broken;
						parser.PutSession(stateObj);
						parser.Connection.BreakConnection();
					}
				}
				TrySetMetaData(null, moreInfo: false);
				_fieldNameLookup = null;
				if (flag && Connection != null)
				{
					Connection.Close();
				}
				if (_command != null)
				{
					_recordsAffected = _command.InternalRecordsAffected;
				}
				_command = null;
				_connection = null;
				_statistics = null;
			}
		}
	}

	internal virtual void CloseReaderFromConnection()
	{
		TdsParser parser = _parser;
		if (parser != null && parser.State == TdsParserState.OpenLoggedIn)
		{
			Close();
			return;
		}
		TdsParserStateObject stateObj = _stateObj;
		_isClosed = true;
		_cancelAsyncOnCloseTokenSource.Cancel();
		if (stateObj != null)
		{
			stateObj._networkPacketTaskSource?.TrySetException(ADP.ClosedConnectionError());
			if (_snapshot != null)
			{
				CleanupAfterAsyncInvocationInternal(stateObj, resetNetworkPacketTaskSource: false);
			}
			stateObj._syncOverAsync = true;
			stateObj.RemoveOwner();
		}
	}

	private bool TryConsumeMetaData()
	{
		while (_parser != null && _stateObj != null && _stateObj._pendingData && !_metaDataConsumed)
		{
			if (_parser.State == TdsParserState.Broken || _parser.State == TdsParserState.Closed)
			{
				if (_parser.Connection != null)
				{
					_parser.Connection.DoomThisConnection();
				}
				throw SQL.ConnectionDoomed();
			}
			if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out var _))
			{
				return false;
			}
		}
		if (_metaData != null)
		{
			if (_snapshot != null && _snapshot._metadata == _metaData)
			{
				_metaData = (_SqlMetaDataSet)_metaData.Clone();
			}
			_metaData.visibleColumns = 0;
			int[] array = new int[_metaData.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = _metaData.visibleColumns;
				if (!_metaData[i].isHidden)
				{
					_metaData.visibleColumns++;
				}
			}
			_metaData.indexMap = array;
		}
		return true;
	}

	/// <summary>Gets a string representing the data type of the specified column.</summary>
	/// <param name="i">The zero-based ordinal position of the column to find.</param>
	/// <returns>The string representing the data type of the specified column.</returns>
	public override string GetDataTypeName(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			CheckMetaDataIsReady(i);
			return GetDataTypeNameInternal(_metaData[i]);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private string GetDataTypeNameInternal(_SqlMetaData metaData)
	{
		string text = null;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
		{
			return MetaType.MetaNVarChar.TypeName;
		}
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
		{
			if (_typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
			{
				return MetaType.MetaMaxVarBinary.TypeName;
			}
			return MetaType.MetaImage.TypeName;
		}
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			if (metaData.type == SqlDbType.Udt)
			{
				return metaData.udtDatabaseName + "." + metaData.udtSchemaName + "." + metaData.udtTypeName;
			}
			return metaData.metaType.TypeName;
		}
		return GetVersionedMetaType(metaData.metaType).TypeName;
	}

	internal virtual SqlBuffer.StorageType GetVariantInternalStorageType(int i)
	{
		return _data[i].VariantInternalStorageType;
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
	public override IEnumerator GetEnumerator()
	{
		return new DbEnumerator(this, IsCommandBehavior(CommandBehavior.CloseConnection));
	}

	/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The <see cref="T:System.Type" /> that is the data type of the object. If the type does not exist on the client, in the case of a User-Defined Type (UDT) returned from the database, GetFieldType returns null.</returns>
	public override Type GetFieldType(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			CheckMetaDataIsReady(i);
			return GetFieldTypeInternal(_metaData[i]);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private Type GetFieldTypeInternal(_SqlMetaData metaData)
	{
		Type type = null;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
		{
			return MetaType.MetaNVarChar.ClassType;
		}
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
		{
			if (_typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
			{
				return MetaType.MetaMaxVarBinary.ClassType;
			}
			return MetaType.MetaImage.ClassType;
		}
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			if (metaData.type == SqlDbType.Udt)
			{
				return MetaType.MetaMaxVarBinary.ClassType;
			}
			return metaData.metaType.ClassType;
		}
		return GetVersionedMetaType(metaData.metaType).ClassType;
	}

	internal virtual int GetLocaleId(int i)
	{
		_SqlMetaData sqlMetaData = MetaData[i];
		if (sqlMetaData.collation != null)
		{
			return sqlMetaData.collation.LCID;
		}
		return 0;
	}

	/// <summary>Gets the name of the specified column.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The name of the specified column.</returns>
	public override string GetName(int i)
	{
		CheckMetaDataIsReady(i);
		return _metaData[i].column;
	}

	/// <summary>Gets an <see langword="Object" /> that is a representation of the underlying provider-specific field type.</summary>
	/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal.</param>
	/// <returns>Gets an <see cref="T:System.Object" /> that is a representation of the underlying provider-specific field type.</returns>
	public override Type GetProviderSpecificFieldType(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			CheckMetaDataIsReady(i);
			return GetProviderSpecificFieldTypeInternal(_metaData[i]);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private Type GetProviderSpecificFieldTypeInternal(_SqlMetaData metaData)
	{
		Type type = null;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
		{
			return MetaType.MetaNVarChar.SqlType;
		}
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
		{
			if (_typeSystem == SqlConnectionString.TypeSystem.SQLServer2005)
			{
				return MetaType.MetaMaxVarBinary.SqlType;
			}
			return MetaType.MetaImage.SqlType;
		}
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			if (metaData.type == SqlDbType.Udt)
			{
				return MetaType.MetaMaxVarBinary.SqlType;
			}
			return metaData.metaType.SqlType;
		}
		return GetVersionedMetaType(metaData.metaType).SqlType;
	}

	/// <summary>Gets the column ordinal, given the name of the column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <returns>The zero-based column ordinal.</returns>
	/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
	public override int GetOrdinal(string name)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			if (_fieldNameLookup == null)
			{
				CheckMetaDataIsReady();
				_fieldNameLookup = new FieldNameLookup(this, _defaultLCID);
			}
			return _fieldNameLookup.GetOrdinal(name);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Gets an <see langword="Object" /> that is a representation of the underlying provider specific value.</summary>
	/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal.</param>
	/// <returns>An <see cref="T:System.Object" /> that is a representation of the underlying provider specific value.</returns>
	public override object GetProviderSpecificValue(int i)
	{
		return GetSqlValue(i);
	}

	/// <summary>Gets an array of objects that are a representation of the underlying provider specific values.</summary>
	/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values.</param>
	/// <returns>The array of objects that are a representation of the underlying provider specific values.</returns>
	public override int GetProviderSpecificValues(object[] values)
	{
		return GetSqlValues(values);
	}

	/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
	/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed.</exception>
	public override DataTable GetSchemaTable()
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			if ((_metaData == null || _metaData.schemaTable == null) && MetaData != null)
			{
				_metaData.schemaTable = BuildSchemaTable();
			}
			return _metaData?.schemaTable;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Gets the value of the specified column as a Boolean.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override bool GetBoolean(int i)
	{
		ReadColumn(i);
		return _data[i].Boolean;
	}

	/// <summary>Retrieves data of type XML as an <see cref="T:System.Xml.XmlReader" />.</summary>
	/// <param name="i">The value of the specified column.</param>
	/// <returns>The returned object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Trying to read a previously read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	/// <exception cref="T:System.InvalidCastException">The returned type was not xml.</exception>
	public virtual XmlReader GetXmlReader(int i)
	{
		CheckDataIsReady(i, allowPartiallyReadColumn: false, permitAsync: false, "GetXmlReader");
		if (_metaData[i].metaType.SqlDbType != SqlDbType.Xml)
		{
			throw SQL.XmlReaderNotSupportOnColumnType(_metaData[i].column);
		}
		if (IsCommandBehavior(CommandBehavior.SequentialAccess))
		{
			_currentStream = new SqlSequentialStream(this, i);
			_lastColumnWithDataChunkRead = i;
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(_currentStream, closeInput: true);
		}
		ReadColumn(i);
		if (_data[i].IsNull)
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(new MemoryStream(Array.Empty<byte>(), writable: false), closeInput: true);
		}
		return _data[i].SqlXml.CreateReader();
	}

	/// <summary>Retrieves binary, image, varbinary, UDT, and variant data types as a <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>A stream object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Tried to read a previously-read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:  
	///
	/// binary  
	///
	/// image  
	///
	/// varbinary  
	///
	/// udt</exception>
	public override Stream GetStream(int i)
	{
		CheckDataIsReady(i, allowPartiallyReadColumn: false, permitAsync: false, "GetStream");
		MetaType metaType = _metaData[i].metaType;
		if ((!metaType.IsBinType || metaType.SqlDbType == SqlDbType.Timestamp) && metaType.SqlDbType != SqlDbType.Variant)
		{
			throw SQL.StreamNotSupportOnColumnType(_metaData[i].column);
		}
		if (metaType.SqlDbType != SqlDbType.Variant && IsCommandBehavior(CommandBehavior.SequentialAccess))
		{
			_currentStream = new SqlSequentialStream(this, i);
			_lastColumnWithDataChunkRead = i;
			return _currentStream;
		}
		ReadColumn(i);
		byte[] buffer = ((!_data[i].IsNull) ? _data[i].SqlBinary.Value : Array.Empty<byte>());
		return new MemoryStream(buffer, writable: false);
	}

	/// <summary>Gets the value of the specified column as a byte.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column as a byte.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override byte GetByte(int i)
	{
		ReadColumn(i);
		return _data[i].Byte;
	}

	/// <summary>Reads a stream of bytes from the specified column offset into the buffer an array starting at the given buffer offset.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
	/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
	/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start.</param>
	/// <param name="length">The maximum length to copy into the buffer.</param>
	/// <returns>The actual number of bytes read.</returns>
	public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
	{
		SqlStatistics statistics = null;
		long num = 0L;
		CheckDataIsReady(i, allowPartiallyReadColumn: true, permitAsync: false, "GetBytes");
		MetaType metaType = _metaData[i].metaType;
		if ((!metaType.IsLong && !metaType.IsBinType) || SqlDbType.Xml == metaType.SqlDbType)
		{
			throw SQL.NonBlobColumn(_metaData[i].column);
		}
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			num = GetBytesInternal(i, dataIndex, buffer, bufferIndex, length);
			_lastColumnWithDataChunkRead = i;
			return num;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	internal virtual long GetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!TryGetBytesInternal(i, dataIndex, buffer, bufferIndex, length, out var remaining))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return remaining;
	}

	private bool TryGetBytesInternal(int i, long dataIndex, byte[] buffer, int bufferIndex, int length, out long remaining)
	{
		remaining = 0L;
		int maxLen = 0;
		if (IsCommandBehavior(CommandBehavior.SequentialAccess))
		{
			if (_sharedState._nextColumnHeaderToRead <= i && !TryReadColumnHeader(i))
			{
				return false;
			}
			if (_data[i] != null && _data[i].IsNull)
			{
				throw new SqlNullValueException();
			}
			if (-1 == _sharedState._columnDataBytesRemaining && _metaData[i].metaType.IsPlp)
			{
				if (!_parser.TryPlpBytesLeft(_stateObj, out var left))
				{
					return false;
				}
				_sharedState._columnDataBytesRemaining = (long)left;
			}
			if (_sharedState._columnDataBytesRemaining == 0L)
			{
				return true;
			}
			if (buffer == null)
			{
				if (_metaData[i].metaType.IsPlp)
				{
					remaining = (long)_parser.PlpBytesTotalLength(_stateObj);
					return true;
				}
				remaining = _sharedState._columnDataBytesRemaining;
				return true;
			}
			if (dataIndex < 0)
			{
				throw ADP.NegativeParameter("dataIndex");
			}
			if (dataIndex < _columnDataBytesRead)
			{
				throw ADP.NonSeqByteAccess(dataIndex, _columnDataBytesRead, "GetBytes");
			}
			long num = dataIndex - _columnDataBytesRead;
			if (num > _sharedState._columnDataBytesRemaining && !_metaData[i].metaType.IsPlp)
			{
				return true;
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length)
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (length + bufferIndex > buffer.Length)
			{
				throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
			}
			if (length < 0)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (num > 0)
			{
				if (_metaData[i].metaType.IsPlp)
				{
					if (!_parser.TrySkipPlpValue((ulong)num, _stateObj, out var totalBytesSkipped))
					{
						return false;
					}
					_columnDataBytesRead += (long)totalBytesSkipped;
				}
				else
				{
					if (!_stateObj.TrySkipLongBytes(num))
					{
						return false;
					}
					_columnDataBytesRead += num;
					_sharedState._columnDataBytesRemaining -= num;
				}
			}
			int bytesRead;
			bool result = TryGetBytesInternalSequential(i, buffer, bufferIndex, length, out bytesRead);
			remaining = bytesRead;
			return result;
		}
		if (dataIndex < 0)
		{
			throw ADP.NegativeParameter("dataIndex");
		}
		if (dataIndex > int.MaxValue)
		{
			throw ADP.InvalidSourceBufferIndex(maxLen, dataIndex, "dataIndex");
		}
		int num2 = (int)dataIndex;
		byte[] array;
		if (_metaData[i].metaType.IsBinType)
		{
			array = GetSqlBinary(i).Value;
		}
		else
		{
			SqlString sqlString = GetSqlString(i);
			array = ((!_metaData[i].metaType.IsNCharType) ? sqlString.GetNonUnicodeBytes() : sqlString.GetUnicodeBytes());
		}
		maxLen = array.Length;
		if (buffer == null)
		{
			remaining = maxLen;
			return true;
		}
		if (num2 < 0 || num2 >= maxLen)
		{
			return true;
		}
		try
		{
			if (num2 < maxLen)
			{
				maxLen = ((num2 + length <= maxLen) ? length : (maxLen - num2));
			}
			Buffer.BlockCopy(array, num2, buffer, bufferIndex, maxLen);
		}
		catch (Exception e)
		{
			if (!ADP.IsCatchableExceptionType(e))
			{
				throw;
			}
			maxLen = array.Length;
			if (length < 0)
			{
				throw ADP.InvalidDataLength(length);
			}
			if (bufferIndex < 0 || bufferIndex >= buffer.Length)
			{
				throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
			}
			if (maxLen + bufferIndex > buffer.Length)
			{
				throw ADP.InvalidBufferSizeOrIndex(maxLen, bufferIndex);
			}
			throw;
		}
		remaining = maxLen;
		return true;
	}

	internal int GetBytesInternalSequential(int i, byte[] buffer, int index, int length, long? timeoutMilliseconds = null)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(timeoutMilliseconds ?? _defaultTimeoutMilliseconds);
			if (!TryReadColumnHeader(i))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			if (!TryGetBytesInternalSequential(i, buffer, index, length, out var bytesRead))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			return bytesRead;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	internal bool TryGetBytesInternalSequential(int i, byte[] buffer, int index, int length, out int bytesRead)
	{
		bytesRead = 0;
		if (_sharedState._columnDataBytesRemaining == 0L || length == 0)
		{
			bytesRead = 0;
			return true;
		}
		if (_metaData[i].metaType.IsPlp)
		{
			bool num = _stateObj.TryReadPlpBytes(ref buffer, index, length, out bytesRead);
			_columnDataBytesRead += bytesRead;
			if (!num)
			{
				return false;
			}
			if (!_parser.TryPlpBytesLeft(_stateObj, out var left))
			{
				_sharedState._columnDataBytesRemaining = -1L;
				return false;
			}
			_sharedState._columnDataBytesRemaining = (long)left;
			return true;
		}
		int len = (int)Math.Min(length, _sharedState._columnDataBytesRemaining);
		bool result = _stateObj.TryReadByteArray(buffer, index, len, out bytesRead);
		_columnDataBytesRead += bytesRead;
		_sharedState._columnDataBytesRemaining -= bytesRead;
		return result;
	}

	/// <summary>Retrieves Char, NChar, NText, NVarChar, text, varChar, and Variant data types as a <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="i">The column to be retrieved.</param>
	/// <returns>The returned object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Tried to read a previously-read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	/// <exception cref="T:System.InvalidCastException">The returned type was not one of the types below:  
	///
	/// char  
	///
	/// nchar  
	///
	/// ntext  
	///
	/// nvarchar  
	///
	/// text  
	///
	/// varchar</exception>
	public override TextReader GetTextReader(int i)
	{
		CheckDataIsReady(i, allowPartiallyReadColumn: false, permitAsync: false, "GetTextReader");
		MetaType metaType = _metaData[i].metaType;
		if ((!metaType.IsCharType && metaType.SqlDbType != SqlDbType.Variant) || metaType.SqlDbType == SqlDbType.Xml)
		{
			throw SQL.TextReaderNotSupportOnColumnType(_metaData[i].column);
		}
		if (metaType.SqlDbType != SqlDbType.Variant && IsCommandBehavior(CommandBehavior.SequentialAccess))
		{
			Encoding encoding = ((!metaType.IsNCharType) ? _metaData[i].encoding : SqlUnicodeEncoding.SqlUnicodeEncodingInstance);
			_currentTextReader = new SqlSequentialTextReader(this, i, encoding);
			_lastColumnWithDataChunkRead = i;
			return _currentTextReader;
		}
		ReadColumn(i);
		string s = ((!_data[i].IsNull) ? _data[i].SqlString.Value : string.Empty);
		return new StringReader(s);
	}

	/// <summary>Gets the value of the specified column as a single character.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override char GetChar(int i)
	{
		throw ADP.NotSupported();
	}

	/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
	/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
	/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start.</param>
	/// <param name="length">The maximum length to copy into the buffer.</param>
	/// <returns>The actual number of characters read.</returns>
	public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
	{
		SqlStatistics statistics = null;
		CheckMetaDataIsReady(i);
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			if (_metaData[i].metaType.IsPlp && IsCommandBehavior(CommandBehavior.SequentialAccess))
			{
				if (length < 0)
				{
					throw ADP.InvalidDataLength(length);
				}
				if (bufferIndex < 0 || (buffer != null && bufferIndex >= buffer.Length))
				{
					throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
				}
				if (buffer != null && length + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(length, bufferIndex);
				}
				long num = 0L;
				if (_metaData[i].type == SqlDbType.Xml)
				{
					try
					{
						CheckDataIsReady(i, allowPartiallyReadColumn: true, permitAsync: false, "GetChars");
					}
					catch (Exception ex)
					{
						if (ADP.IsCatchableExceptionType(ex))
						{
							throw new TargetInvocationException(ex);
						}
						throw;
					}
					num = GetStreamingXmlChars(i, dataIndex, buffer, bufferIndex, length);
				}
				else
				{
					CheckDataIsReady(i, allowPartiallyReadColumn: true, permitAsync: false, "GetChars");
					num = GetCharsFromPlpData(i, dataIndex, buffer, bufferIndex, length);
				}
				_lastColumnWithDataChunkRead = i;
				return num;
			}
			if (_sharedState._nextColumnDataToRead == i + 1 && _sharedState._nextColumnHeaderToRead == i + 1 && _columnDataChars != null && IsCommandBehavior(CommandBehavior.SequentialAccess) && dataIndex < _columnDataCharsRead)
			{
				throw ADP.NonSeqByteAccess(dataIndex, _columnDataCharsRead, "GetChars");
			}
			if (_columnDataCharsIndex != i)
			{
				string value = GetSqlString(i).Value;
				_columnDataChars = value.ToCharArray();
				_columnDataCharsRead = 0L;
				_columnDataCharsIndex = i;
			}
			int num2 = _columnDataChars.Length;
			if (dataIndex > int.MaxValue)
			{
				throw ADP.InvalidSourceBufferIndex(num2, dataIndex, "dataIndex");
			}
			int num3 = (int)dataIndex;
			if (buffer == null)
			{
				return num2;
			}
			if (num3 < 0 || num3 >= num2)
			{
				return 0L;
			}
			try
			{
				if (num3 < num2)
				{
					num2 = ((num3 + length <= num2) ? length : (num2 - num3));
				}
				Array.Copy(_columnDataChars, num3, buffer, bufferIndex, num2);
				_columnDataCharsRead += num2;
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				num2 = _columnDataChars.Length;
				if (length < 0)
				{
					throw ADP.InvalidDataLength(length);
				}
				if (bufferIndex < 0 || bufferIndex >= buffer.Length)
				{
					throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
				}
				if (num2 + bufferIndex > buffer.Length)
				{
					throw ADP.InvalidBufferSizeOrIndex(num2, bufferIndex);
				}
				throw;
			}
			return num2;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private long GetCharsFromPlpData(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
	{
		if (!_metaData[i].metaType.IsCharType)
		{
			throw SQL.NonCharColumn(_metaData[i].column);
		}
		if (_sharedState._nextColumnHeaderToRead <= i)
		{
			ReadColumnHeader(i);
		}
		if (_data[i] != null && _data[i].IsNull)
		{
			throw new SqlNullValueException();
		}
		if (dataIndex < _columnDataCharsRead)
		{
			throw ADP.NonSeqByteAccess(dataIndex, _columnDataCharsRead, "GetChars");
		}
		if (dataIndex == 0L)
		{
			_stateObj._plpdecoder = null;
		}
		bool isNCharType = _metaData[i].metaType.IsNCharType;
		if (-1 == _sharedState._columnDataBytesRemaining)
		{
			_sharedState._columnDataBytesRemaining = (long)_parser.PlpBytesLeft(_stateObj);
		}
		if (_sharedState._columnDataBytesRemaining == 0L)
		{
			_stateObj._plpdecoder = null;
			return 0L;
		}
		long num;
		if (buffer == null)
		{
			num = (long)_parser.PlpBytesTotalLength(_stateObj);
			if (!isNCharType || num <= 0)
			{
				return num;
			}
			return num >> 1;
		}
		if (dataIndex > _columnDataCharsRead)
		{
			_stateObj._plpdecoder = null;
			num = dataIndex - _columnDataCharsRead;
			num = (isNCharType ? (num << 1) : num);
			num = (long)_parser.SkipPlpValue((ulong)num, _stateObj);
			_columnDataBytesRead += num;
			_columnDataCharsRead += ((isNCharType && num > 0) ? (num >> 1) : num);
		}
		num = length;
		if (isNCharType)
		{
			num = _parser.ReadPlpUnicodeChars(ref buffer, bufferIndex, length, _stateObj);
			_columnDataBytesRead += num << 1;
		}
		else
		{
			num = _parser.ReadPlpAnsiChars(ref buffer, bufferIndex, length, _metaData[i], _stateObj);
			_columnDataBytesRead += num << 1;
		}
		_columnDataCharsRead += num;
		_sharedState._columnDataBytesRemaining = (long)_parser.PlpBytesLeft(_stateObj);
		return num;
	}

	internal long GetStreamingXmlChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
	{
		SqlStreamingXml sqlStreamingXml = null;
		if (_streamingXml != null && _streamingXml.ColumnOrdinal != i)
		{
			_streamingXml.Close();
			_streamingXml = null;
		}
		sqlStreamingXml = ((_streamingXml != null) ? _streamingXml : new SqlStreamingXml(i, this));
		long chars = sqlStreamingXml.GetChars(dataIndex, buffer, bufferIndex, length);
		if (_streamingXml == null)
		{
			_streamingXml = sqlStreamingXml;
		}
		return chars;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override DateTime GetDateTime(int i)
	{
		ReadColumn(i);
		DateTime result = _data[i].DateTime;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && _metaData[i].IsNewKatmaiDateTimeType)
		{
			result = (DateTime)(object)_data[i].String;
		}
		return result;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override decimal GetDecimal(int i)
	{
		ReadColumn(i);
		return _data[i].Decimal;
	}

	/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override double GetDouble(int i)
	{
		ReadColumn(i);
		return _data[i].Double;
	}

	/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override float GetFloat(int i)
	{
		ReadColumn(i);
		return _data[i].Single;
	}

	/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override Guid GetGuid(int i)
	{
		ReadColumn(i);
		return _data[i].SqlGuid.Value;
	}

	/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override short GetInt16(int i)
	{
		ReadColumn(i);
		return _data[i].Int16;
	}

	/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override int GetInt32(int i)
	{
		ReadColumn(i);
		return _data[i].Int32;
	}

	/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override long GetInt64(int i)
	{
		ReadColumn(i);
		return _data[i].Int64;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column.</returns>
	public virtual SqlBoolean GetSqlBoolean(int i)
	{
		ReadColumn(i);
		return _data[i].SqlBoolean;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
	public virtual SqlBinary GetSqlBinary(int i)
	{
		ReadColumn(i, setTimeout: true, allowPartiallyReadColumn: true);
		return _data[i].SqlBinary;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
	public virtual SqlByte GetSqlByte(int i)
	{
		ReadColumn(i);
		return _data[i].SqlByte;
	}

	/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
	public virtual SqlBytes GetSqlBytes(int i)
	{
		ReadColumn(i);
		return new SqlBytes(_data[i].SqlBinary);
	}

	/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlChars" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
	public virtual SqlChars GetSqlChars(int i)
	{
		ReadColumn(i);
		SqlString value = ((_typeSystem > SqlConnectionString.TypeSystem.SQLServer2005 || !_metaData[i].IsNewKatmaiDateTimeType) ? _data[i].SqlString : _data[i].KatmaiDateTimeSqlString);
		return new SqlChars(value);
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
	public virtual SqlDateTime GetSqlDateTime(int i)
	{
		ReadColumn(i);
		return _data[i].SqlDateTime;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
	public virtual SqlDecimal GetSqlDecimal(int i)
	{
		ReadColumn(i);
		return _data[i].SqlDecimal;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
	public virtual SqlGuid GetSqlGuid(int i)
	{
		ReadColumn(i);
		return _data[i].SqlGuid;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a  <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
	public virtual SqlDouble GetSqlDouble(int i)
	{
		ReadColumn(i);
		return _data[i].SqlDouble;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
	public virtual SqlInt16 GetSqlInt16(int i)
	{
		ReadColumn(i);
		return _data[i].SqlInt16;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
	public virtual SqlInt32 GetSqlInt32(int i)
	{
		ReadColumn(i);
		return _data[i].SqlInt32;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
	public virtual SqlInt64 GetSqlInt64(int i)
	{
		ReadColumn(i);
		return _data[i].SqlInt64;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
	public virtual SqlMoney GetSqlMoney(int i)
	{
		ReadColumn(i);
		return _data[i].SqlMoney;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
	public virtual SqlSingle GetSqlSingle(int i)
	{
		ReadColumn(i);
		return _data[i].SqlSingle;
	}

	/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
	public virtual SqlString GetSqlString(int i)
	{
		ReadColumn(i);
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && _metaData[i].IsNewKatmaiDateTimeType)
		{
			return _data[i].KatmaiDateTimeSqlString;
		}
		return _data[i].SqlString;
	}

	/// <summary>Gets the value of the specified column as an XML value.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>A <see cref="T:System.Data.SqlTypes.SqlXml" /> value that contains the XML stored within the corresponding field.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1</exception>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.SqlClient.SqlDataReader" />.</exception>
	/// <exception cref="T:System.InvalidCastException">The retrieved data is not compatible with the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</exception>
	public virtual SqlXml GetSqlXml(int i)
	{
		ReadColumn(i);
		SqlXml sqlXml = null;
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			return _data[i].IsNull ? SqlXml.Null : _data[i].SqlCachedBuffer.ToSqlXml();
		}
		sqlXml = (_data[i].IsNull ? SqlXml.Null : _data[i].SqlCachedBuffer.ToSqlXml());
		return (SqlXml)(object)_data[i].String;
	}

	/// <summary>Returns the data value in the specified column as a SQL Server type.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlDbType" />.</returns>
	public virtual object GetSqlValue(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			return GetSqlValueInternal(i);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private object GetSqlValueInternal(int i)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!TryReadColumn(i, setTimeout: false))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return GetSqlValueFromSqlBufferInternal(_data[i], _metaData[i]);
	}

	private object GetSqlValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
	{
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
		{
			return data.KatmaiDateTimeSqlString;
		}
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
		{
			return data.SqlValue;
		}
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			if (metaData.type == SqlDbType.Udt)
			{
				if (_connection != null)
				{
					throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
				}
				throw ADP.DataReaderClosed("GetSqlValueFromSqlBufferInternal");
			}
			return data.SqlValue;
		}
		if (metaData.type == SqlDbType.Xml)
		{
			return data.SqlString;
		}
		return data.SqlValue;
	}

	/// <summary>Fills an array of <see cref="T:System.Object" /> that contains the values for all the columns in the record, expressed as SQL Server types.</summary>
	/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the values. The column values are expressed as SQL Server types.</param>
	/// <returns>An integer indicating the number of columns copied.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is null.</exception>
	public virtual int GetSqlValues(object[] values)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			CheckDataIsReady();
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			SetTimeout(_defaultTimeoutMilliseconds);
			int num = ((values.Length < _metaData.visibleColumns) ? values.Length : _metaData.visibleColumns);
			for (int i = 0; i < num; i++)
			{
				values[_metaData.indexMap[i]] = GetSqlValueInternal(i);
			}
			return num;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Gets the value of the specified column as a string.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public override string GetString(int i)
	{
		ReadColumn(i);
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && _metaData[i].IsNewKatmaiDateTimeType)
		{
			return _data[i].KatmaiDateTimeString;
		}
		return _data[i].String;
	}

	/// <summary>Synchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValueAsync``1(System.Int32,System.Threading.CancellationToken)" /> is the asynchronous version of this method.</summary>
	/// <param name="i">The column to be retrieved.</param>
	/// <typeparam name="T">The type of the value to be returned.</typeparam>
	/// <returns>The returned type object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Tried to read a previously-read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == <see langword="true" />), retrieving a non-SQL type.</exception>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="T" /> doesn't match the type returned by SQL Server or cannot be cast.</exception>
	public override T GetFieldValue<T>(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			return GetFieldValueInternal<T>(i);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Gets the value of the specified column in its native format.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>This method returns <see cref="T:System.DBNull" /> for null database columns.</returns>
	public override object GetValue(int i)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			return GetValueInternal(i);
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Retrieves the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public virtual TimeSpan GetTimeSpan(int i)
	{
		ReadColumn(i);
		TimeSpan result = _data[i].Time;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
		{
			result = (TimeSpan)(object)_data[i].String;
		}
		return result;
	}

	/// <summary>Retrieves the value of the specified column as a <see cref="T:System.DateTimeOffset" /> object.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>The value of the specified column.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified cast is not valid.</exception>
	public virtual DateTimeOffset GetDateTimeOffset(int i)
	{
		ReadColumn(i);
		DateTimeOffset result = _data[i].DateTimeOffset;
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005)
		{
			result = (DateTimeOffset)(object)_data[i].String;
		}
		return result;
	}

	private object GetValueInternal(int i)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!TryReadColumn(i, setTimeout: false))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return GetValueFromSqlBufferInternal(_data[i], _metaData[i]);
	}

	private object GetValueFromSqlBufferInternal(SqlBuffer data, _SqlMetaData metaData)
	{
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsNewKatmaiDateTimeType)
		{
			if (data.IsNull)
			{
				return DBNull.Value;
			}
			return data.KatmaiDateTimeString;
		}
		if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && metaData.IsLargeUdt)
		{
			return data.Value;
		}
		if (_typeSystem != SqlConnectionString.TypeSystem.SQLServer2000)
		{
			if (metaData.type != SqlDbType.Udt)
			{
				return data.Value;
			}
			if (_connection != null)
			{
				throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
			}
			throw ADP.DataReaderClosed("GetValueFromSqlBufferInternal");
		}
		return data.Value;
	}

	private T GetFieldValueInternal<T>(int i)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!TryReadColumn(i, setTimeout: false))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return GetFieldValueFromSqlBufferInternal<T>(_data[i], _metaData[i]);
	}

	private T GetFieldValueFromSqlBufferInternal<T>(SqlBuffer data, _SqlMetaData metaData)
	{
		Type typeFromHandle = typeof(T);
		if (_typeofINullable.IsAssignableFrom(typeFromHandle))
		{
			object obj = GetSqlValueFromSqlBufferInternal(data, metaData);
			if (typeFromHandle == s_typeofSqlString && obj is SqlXml sqlXml)
			{
				obj = ((!sqlXml.IsNull) ? ((object)new SqlString(sqlXml.Value)) : ((object)SqlString.Null));
			}
			return (T)obj;
		}
		try
		{
			return (T)GetValueFromSqlBufferInternal(data, metaData);
		}
		catch (InvalidCastException)
		{
			if (data.IsNull)
			{
				throw SQL.SqlNullValue();
			}
			throw;
		}
	}

	/// <summary>Populates an array of objects with the column values of the current row.</summary>
	/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
	/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
	public override int GetValues(object[] values)
	{
		SqlStatistics statistics = null;
		bool flag = IsCommandBehavior(CommandBehavior.SequentialAccess);
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			CheckMetaDataIsReady();
			int num = ((values.Length < _metaData.visibleColumns) ? values.Length : _metaData.visibleColumns);
			int num2 = num - 1;
			SetTimeout(_defaultTimeoutMilliseconds);
			_commandBehavior &= ~CommandBehavior.SequentialAccess;
			if (!TryReadColumn(num2, setTimeout: false))
			{
				throw SQL.SynchronousCallMayNotPend();
			}
			for (int i = 0; i < num; i++)
			{
				values[_metaData.indexMap[i]] = GetValueFromSqlBufferInternal(_data[i], _metaData[i]);
				if (flag && i < num2)
				{
					_data[i].Clear();
				}
			}
			return num;
		}
		finally
		{
			if (flag)
			{
				_commandBehavior |= CommandBehavior.SequentialAccess;
			}
			SqlStatistics.StopTimer(statistics);
		}
	}

	private MetaType GetVersionedMetaType(MetaType actualMetaType)
	{
		MetaType metaType = null;
		if (actualMetaType == MetaType.MetaUdt)
		{
			return MetaType.MetaVarBinary;
		}
		if (actualMetaType == MetaType.MetaXml)
		{
			return MetaType.MetaNText;
		}
		if (actualMetaType == MetaType.MetaMaxVarBinary)
		{
			return MetaType.MetaImage;
		}
		if (actualMetaType == MetaType.MetaMaxVarChar)
		{
			return MetaType.MetaText;
		}
		if (actualMetaType == MetaType.MetaMaxNVarChar)
		{
			return MetaType.MetaNText;
		}
		return actualMetaType;
	}

	private bool TryHasMoreResults(out bool moreResults)
	{
		if (_parser != null)
		{
			if (!TryHasMoreRows(out var moreRows))
			{
				moreResults = false;
				return false;
			}
			if (moreRows)
			{
				moreResults = false;
				return true;
			}
			while (_stateObj._pendingData)
			{
				if (!_stateObj.TryPeekByte(out var value))
				{
					moreResults = false;
					return false;
				}
				switch (value)
				{
				case 211:
					if (_altRowStatus == ALTROWSTATUS.Null)
					{
						_altMetaDataSetCollection.metaDataSet = _metaData;
						_metaData = null;
					}
					_altRowStatus = ALTROWSTATUS.AltRow;
					_hasRows = true;
					moreResults = true;
					return true;
				case 209:
				case 210:
					moreResults = true;
					return true;
				case 253:
					_altRowStatus = ALTROWSTATUS.Null;
					_metaData = null;
					_altMetaDataSetCollection = null;
					moreResults = true;
					return true;
				case 129:
					moreResults = true;
					return true;
				default:
				{
					if (_parser.State == TdsParserState.Broken || _parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out var _))
					{
						moreResults = false;
						return false;
					}
					break;
				}
				}
			}
		}
		moreResults = false;
		return true;
	}

	private bool TryHasMoreRows(out bool moreRows)
	{
		if (_parser != null)
		{
			if (_sharedState._dataReady)
			{
				moreRows = true;
				return true;
			}
			switch (_altRowStatus)
			{
			case ALTROWSTATUS.AltRow:
				moreRows = true;
				return true;
			case ALTROWSTATUS.Done:
				moreRows = false;
				return true;
			}
			if (_stateObj._pendingData)
			{
				if (!_stateObj.TryPeekByte(out var value))
				{
					moreRows = false;
					return false;
				}
				bool flag = false;
				while (value == 253 || value == 254 || value == byte.MaxValue || (!flag && (value == 228 || value == 227 || value == 169 || value == 170 || value == 171)))
				{
					if (value == 253 || value == 254 || value == byte.MaxValue)
					{
						flag = true;
					}
					if (_parser.State == TdsParserState.Broken || _parser.State == TdsParserState.Closed)
					{
						throw ADP.ClosedConnectionError();
					}
					if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out var _))
					{
						moreRows = false;
						return false;
					}
					if (!_stateObj._pendingData)
					{
						break;
					}
					if (!_stateObj.TryPeekByte(out value))
					{
						moreRows = false;
						return false;
					}
				}
				if (IsRowToken(value))
				{
					moreRows = true;
					return true;
				}
			}
		}
		moreRows = false;
		return true;
	}

	private bool IsRowToken(byte token)
	{
		if (209 != token)
		{
			return 210 == token;
		}
		return true;
	}

	/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
	/// <param name="i">The zero-based column ordinal.</param>
	/// <returns>
	///   <see langword="true" /> if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise <see langword="false" />.</returns>
	public override bool IsDBNull(int i)
	{
		CheckHeaderIsReady(i, permitAsync: false, "IsDBNull");
		SetTimeout(_defaultTimeoutMilliseconds);
		ReadColumnHeader(i);
		return _data[i].IsNull;
	}

	/// <summary>Determines whether the specified <see cref="T:System.Data.CommandBehavior" /> matches that of the <see cref="T:System.Data.SqlClient.SqlDataReader" /> .</summary>
	/// <param name="condition">A <see cref="T:System.Data.CommandBehavior" /> enumeration.</param>
	/// <returns>
	///   <see langword="true" /> if the specified <see cref="T:System.Data.CommandBehavior" /> is true, <see langword="false" /> otherwise.</returns>
	protected internal bool IsCommandBehavior(CommandBehavior condition)
	{
		return condition == (condition & _commandBehavior);
	}

	/// <summary>Advances the data reader to the next result, when reading the results of batch Transact-SQL statements.</summary>
	/// <returns>
	///   <see langword="true" /> if there are more result sets; otherwise <see langword="false" />.</returns>
	public override bool NextResult()
	{
		if (_currentTask != null)
		{
			throw SQL.PendingBeginXXXExists();
		}
		if (!TryNextResult(out var more))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return more;
	}

	private bool TryNextResult(out bool more)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			SetTimeout(_defaultTimeoutMilliseconds);
			if (IsClosed)
			{
				throw ADP.DataReaderClosed("NextResult");
			}
			_fieldNameLookup = null;
			bool flag = false;
			_hasRows = false;
			if (IsCommandBehavior(CommandBehavior.SingleResult))
			{
				if (!TryCloseInternal(closeReader: false))
				{
					more = false;
					return false;
				}
				ClearMetaData();
				more = flag;
				return true;
			}
			if (_parser != null)
			{
				bool more2 = true;
				while (more2)
				{
					if (!TryReadInternal(setTimeout: false, out more2))
					{
						more = false;
						return false;
					}
				}
			}
			if (_parser != null)
			{
				if (!TryHasMoreResults(out var moreResults))
				{
					more = false;
					return false;
				}
				if (moreResults)
				{
					_metaDataConsumed = false;
					_browseModeInfoConsumed = false;
					switch (_altRowStatus)
					{
					case ALTROWSTATUS.AltRow:
					{
						if (!_parser.TryGetAltRowId(_stateObj, out var id))
						{
							more = false;
							return false;
						}
						_SqlMetaDataSet altMetaData = _altMetaDataSetCollection.GetAltMetaData(id);
						if (altMetaData != null)
						{
							_metaData = altMetaData;
						}
						break;
					}
					case ALTROWSTATUS.Done:
						_metaData = _altMetaDataSetCollection.metaDataSet;
						_altRowStatus = ALTROWSTATUS.Null;
						break;
					default:
						if (!TryConsumeMetaData())
						{
							more = false;
							return false;
						}
						if (_metaData == null)
						{
							more = false;
							return true;
						}
						break;
					}
					flag = true;
				}
				else
				{
					if (!TryCloseInternal(closeReader: false))
					{
						more = false;
						return false;
					}
					if (!TrySetMetaData(null, moreInfo: false))
					{
						more = false;
						return false;
					}
				}
			}
			else
			{
				ClearMetaData();
			}
			more = flag;
			return true;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	/// <summary>Advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.</summary>
	/// <returns>
	///   <see langword="true" /> if there are more rows; otherwise <see langword="false" />.</returns>
	/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
	public override bool Read()
	{
		if (_currentTask != null)
		{
			throw SQL.PendingBeginXXXExists();
		}
		if (!TryReadInternal(setTimeout: true, out var more))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
		return more;
	}

	private bool TryReadInternal(bool setTimeout, out bool more)
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			if (_parser != null)
			{
				if (setTimeout)
				{
					SetTimeout(_defaultTimeoutMilliseconds);
				}
				if (_sharedState._dataReady && !TryCleanPartialRead())
				{
					more = false;
					return false;
				}
				SqlBuffer.Clear(_data);
				_sharedState._nextColumnHeaderToRead = 0;
				_sharedState._nextColumnDataToRead = 0;
				_sharedState._columnDataBytesRemaining = -1L;
				_lastColumnWithDataChunkRead = -1;
				if (!_haltRead)
				{
					if (!TryHasMoreRows(out var moreRows))
					{
						more = false;
						return false;
					}
					if (moreRows)
					{
						while (_stateObj._pendingData)
						{
							if (_altRowStatus != ALTROWSTATUS.AltRow)
							{
								if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out _sharedState._dataReady))
								{
									more = false;
									return false;
								}
								if (_sharedState._dataReady)
								{
									break;
								}
								continue;
							}
							_altRowStatus = ALTROWSTATUS.Done;
							_sharedState._dataReady = true;
							break;
						}
						if (_sharedState._dataReady)
						{
							_haltRead = IsCommandBehavior(CommandBehavior.SingleRow);
							more = true;
							return true;
						}
					}
					if (!_stateObj._pendingData && !TryCloseInternal(closeReader: false))
					{
						more = false;
						return false;
					}
				}
				else
				{
					if (!TryHasMoreRows(out var moreRows2))
					{
						more = false;
						return false;
					}
					while (moreRows2)
					{
						while (_stateObj._pendingData && !_sharedState._dataReady)
						{
							if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out _sharedState._dataReady))
							{
								more = false;
								return false;
							}
						}
						if (_sharedState._dataReady && !TryCleanPartialRead())
						{
							more = false;
							return false;
						}
						SqlBuffer.Clear(_data);
						_sharedState._nextColumnHeaderToRead = 0;
						if (!TryHasMoreRows(out moreRows2))
						{
							more = false;
							return false;
						}
					}
					_haltRead = false;
				}
			}
			else if (IsClosed)
			{
				throw ADP.DataReaderClosed("Read");
			}
			more = false;
			return true;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private void ReadColumn(int i, bool setTimeout = true, bool allowPartiallyReadColumn = false)
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!TryReadColumn(i, setTimeout, allowPartiallyReadColumn))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
	}

	private bool TryReadColumn(int i, bool setTimeout, bool allowPartiallyReadColumn = false)
	{
		CheckDataIsReady(i, allowPartiallyReadColumn, permitAsync: true, null);
		if (setTimeout)
		{
			SetTimeout(_defaultTimeoutMilliseconds);
		}
		if (!TryReadColumnInternal(i))
		{
			return false;
		}
		return true;
	}

	private bool TryReadColumnData()
	{
		if (!_data[_sharedState._nextColumnDataToRead].IsNull)
		{
			_SqlMetaData md = _metaData[_sharedState._nextColumnDataToRead];
			if (!_parser.TryReadSqlValue(_data[_sharedState._nextColumnDataToRead], md, (int)_sharedState._columnDataBytesRemaining, _stateObj))
			{
				return false;
			}
			_sharedState._columnDataBytesRemaining = 0L;
		}
		_sharedState._nextColumnDataToRead++;
		return true;
	}

	private void ReadColumnHeader(int i)
	{
		if (!TryReadColumnHeader(i))
		{
			throw SQL.SynchronousCallMayNotPend();
		}
	}

	private bool TryReadColumnHeader(int i)
	{
		if (!_sharedState._dataReady)
		{
			throw SQL.InvalidRead();
		}
		return TryReadColumnInternal(i, readHeaderOnly: true);
	}

	private bool TryReadColumnInternal(int i, bool readHeaderOnly = false)
	{
		if (i < _sharedState._nextColumnHeaderToRead)
		{
			if (i == _sharedState._nextColumnDataToRead && !readHeaderOnly)
			{
				return TryReadColumnData();
			}
			return true;
		}
		bool flag = IsCommandBehavior(CommandBehavior.SequentialAccess);
		if (flag)
		{
			if (0 < _sharedState._nextColumnDataToRead)
			{
				_data[_sharedState._nextColumnDataToRead - 1].Clear();
			}
			if (_lastColumnWithDataChunkRead > -1 && i > _lastColumnWithDataChunkRead)
			{
				CloseActiveSequentialStreamAndTextReader();
			}
		}
		else if (_sharedState._nextColumnDataToRead < _sharedState._nextColumnHeaderToRead && !TryReadColumnData())
		{
			return false;
		}
		if (!TryResetBlobState())
		{
			return false;
		}
		do
		{
			_SqlMetaData sqlMetaData = _metaData[_sharedState._nextColumnHeaderToRead];
			if (flag && _sharedState._nextColumnHeaderToRead < i)
			{
				if (!_parser.TrySkipValue(sqlMetaData, _sharedState._nextColumnHeaderToRead, _stateObj))
				{
					return false;
				}
				_sharedState._nextColumnDataToRead = _sharedState._nextColumnHeaderToRead;
				_sharedState._nextColumnHeaderToRead++;
			}
			else
			{
				if (!_parser.TryProcessColumnHeader(sqlMetaData, _stateObj, _sharedState._nextColumnHeaderToRead, out var isNull, out var length))
				{
					return false;
				}
				_sharedState._nextColumnDataToRead = _sharedState._nextColumnHeaderToRead;
				_sharedState._nextColumnHeaderToRead++;
				if (isNull && sqlMetaData.type != SqlDbType.Timestamp)
				{
					_parser.GetNullSqlValue(_data[_sharedState._nextColumnDataToRead], sqlMetaData);
					if (!readHeaderOnly)
					{
						_sharedState._nextColumnDataToRead++;
					}
				}
				else if (i > _sharedState._nextColumnDataToRead || !readHeaderOnly)
				{
					if (!_parser.TryReadSqlValue(_data[_sharedState._nextColumnDataToRead], sqlMetaData, (int)length, _stateObj))
					{
						return false;
					}
					_sharedState._nextColumnDataToRead++;
				}
				else
				{
					_sharedState._columnDataBytesRemaining = (long)length;
				}
			}
			if (_snapshot != null)
			{
				_snapshot = null;
				PrepareAsyncInvocation(useSnapshot: true);
			}
		}
		while (_sharedState._nextColumnHeaderToRead <= i);
		return true;
	}

	private bool WillHaveEnoughData(int targetColumn, bool headerOnly = false)
	{
		if (_lastColumnWithDataChunkRead == _sharedState._nextColumnDataToRead && _metaData[_lastColumnWithDataChunkRead].metaType.IsPlp)
		{
			return false;
		}
		int num = Math.Min(checked(_stateObj._inBytesRead - _stateObj._inBytesUsed), _stateObj._inBytesPacket);
		num--;
		if (targetColumn >= _sharedState._nextColumnDataToRead && _sharedState._nextColumnDataToRead < _sharedState._nextColumnHeaderToRead)
		{
			if (_sharedState._columnDataBytesRemaining > num)
			{
				return false;
			}
			num = checked(num - (int)_sharedState._columnDataBytesRemaining);
		}
		int num2 = _sharedState._nextColumnHeaderToRead;
		while (num >= 0 && num2 <= targetColumn)
		{
			if (!_stateObj.IsNullCompressionBitSet(num2))
			{
				MetaType metaType = _metaData[num2].metaType;
				if (metaType.IsLong || metaType.IsPlp || metaType.SqlDbType == SqlDbType.Udt || metaType.SqlDbType == SqlDbType.Structured)
				{
					return false;
				}
				byte b = (byte)(_metaData[num2].tdsType & 0x30);
				int num3 = ((b == 32 || b == 0) ? (((_metaData[num2].tdsType & 0x80) != 0) ? 2 : (((_metaData[num2].tdsType & 0xC) != 0) ? 1 : 4)) : 0);
				checked
				{
					num -= num3;
					if (num2 < targetColumn || !headerOnly)
					{
						num -= _metaData[num2].length;
					}
				}
			}
			num2++;
		}
		return num >= 0;
	}

	private bool TryResetBlobState()
	{
		if (_sharedState._nextColumnDataToRead < _sharedState._nextColumnHeaderToRead)
		{
			if (_sharedState._nextColumnHeaderToRead > 0 && _metaData[_sharedState._nextColumnHeaderToRead - 1].metaType.IsPlp)
			{
				if (_stateObj._longlen != 0L && !_stateObj.Parser.TrySkipPlpValue(ulong.MaxValue, _stateObj, out var _))
				{
					return false;
				}
				if (_streamingXml != null)
				{
					SqlStreamingXml streamingXml = _streamingXml;
					_streamingXml = null;
					streamingXml.Close();
				}
			}
			else if (0 < _sharedState._columnDataBytesRemaining && !_stateObj.TrySkipLongBytes(_sharedState._columnDataBytesRemaining))
			{
				return false;
			}
		}
		_sharedState._columnDataBytesRemaining = 0L;
		_columnDataBytesRead = 0L;
		_columnDataCharsRead = 0L;
		_columnDataChars = null;
		_columnDataCharsIndex = -1;
		_stateObj._plpdecoder = null;
		return true;
	}

	private void CloseActiveSequentialStreamAndTextReader()
	{
		if (_currentStream != null)
		{
			_currentStream.SetClosed();
			_currentStream = null;
		}
		if (_currentTextReader != null)
		{
			_currentTextReader.SetClosed();
			_currentStream = null;
		}
	}

	private void RestoreServerSettings(TdsParser parser, TdsParserStateObject stateObj)
	{
		if (parser != null && _resetOptionsString != null)
		{
			if (parser.State == TdsParserState.OpenLoggedIn)
			{
				parser.TdsExecuteSQLBatch(_resetOptionsString, (_command != null) ? _command.CommandTimeout : 0, null, stateObj, sync: true);
				parser.Run(RunBehavior.UntilDone, _command, this, null, stateObj);
			}
			_resetOptionsString = null;
		}
	}

	internal bool TrySetAltMetaDataSet(_SqlMetaDataSet metaDataSet, bool metaDataConsumed)
	{
		if (_altMetaDataSetCollection == null)
		{
			_altMetaDataSetCollection = new _SqlMetaDataSetCollection();
		}
		else if (_snapshot != null && _snapshot._altMetaDataSetCollection == _altMetaDataSetCollection)
		{
			_altMetaDataSetCollection = (_SqlMetaDataSetCollection)_altMetaDataSetCollection.Clone();
		}
		_altMetaDataSetCollection.SetAltMetaData(metaDataSet);
		_metaDataConsumed = metaDataConsumed;
		if (_metaDataConsumed && _parser != null)
		{
			if (!_stateObj.TryPeekByte(out var value))
			{
				return false;
			}
			if (169 == value)
			{
				if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, this, null, _stateObj, out var _))
				{
					return false;
				}
				if (!_stateObj.TryPeekByte(out value))
				{
					return false;
				}
			}
			if (value == 171)
			{
				try
				{
					_stateObj._accumulateInfoEvents = true;
					if (!_parser.TryRun(RunBehavior.ReturnImmediately, _command, null, null, _stateObj, out var _))
					{
						return false;
					}
				}
				finally
				{
					_stateObj._accumulateInfoEvents = false;
				}
				if (!_stateObj.TryPeekByte(out value))
				{
					return false;
				}
			}
			_hasRows = IsRowToken(value);
		}
		if (metaDataSet != null && (_data == null || _data.Length < metaDataSet.Length))
		{
			_data = SqlBuffer.CreateBufferArray(metaDataSet.Length);
		}
		return true;
	}

	private void ClearMetaData()
	{
		_metaData = null;
		_tableNames = null;
		_fieldNameLookup = null;
		_metaDataConsumed = false;
		_browseModeInfoConsumed = false;
	}

	internal bool TrySetMetaData(_SqlMetaDataSet metaData, bool moreInfo)
	{
		_metaData = metaData;
		_tableNames = null;
		if (_metaData != null)
		{
			_data = SqlBuffer.CreateBufferArray(metaData.Length);
		}
		_fieldNameLookup = null;
		if (metaData != null)
		{
			if (!moreInfo)
			{
				_metaDataConsumed = true;
				if (_parser != null)
				{
					if (!_stateObj.TryPeekByte(out var value))
					{
						return false;
					}
					if (value == 169)
					{
						if (!_parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, _stateObj, out var _))
						{
							return false;
						}
						if (!_stateObj.TryPeekByte(out value))
						{
							return false;
						}
					}
					if (value == 171)
					{
						try
						{
							_stateObj._accumulateInfoEvents = true;
							if (!_parser.TryRun(RunBehavior.ReturnImmediately, null, null, null, _stateObj, out var _))
							{
								return false;
							}
						}
						finally
						{
							_stateObj._accumulateInfoEvents = false;
						}
						if (!_stateObj.TryPeekByte(out value))
						{
							return false;
						}
					}
					_hasRows = IsRowToken(value);
					if (136 == value)
					{
						_metaDataConsumed = false;
					}
				}
			}
		}
		else
		{
			_metaDataConsumed = false;
		}
		_browseModeInfoConsumed = false;
		return true;
	}

	private void SetTimeout(long timeoutMilliseconds)
	{
		_stateObj?.SetTimeoutMilliseconds(timeoutMilliseconds);
	}

	private bool HasActiveStreamOrTextReaderOnColumn(int columnIndex)
	{
		return (byte)(0u | ((_currentStream != null && _currentStream.ColumnIndex == columnIndex) ? 1u : 0u) | ((_currentTextReader != null && _currentTextReader.ColumnIndex == columnIndex) ? 1u : 0u)) != 0;
	}

	private void CheckMetaDataIsReady()
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (MetaData == null)
		{
			throw SQL.InvalidRead();
		}
	}

	private void CheckMetaDataIsReady(int columnIndex, bool permitAsync = false)
	{
		if (!permitAsync && _currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (MetaData == null)
		{
			throw SQL.InvalidRead();
		}
		if (columnIndex < 0 || columnIndex >= _metaData.Length)
		{
			throw ADP.IndexOutOfRange();
		}
	}

	private void CheckDataIsReady()
	{
		if (_currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!_sharedState._dataReady || _metaData == null)
		{
			throw SQL.InvalidRead();
		}
	}

	private void CheckHeaderIsReady(int columnIndex, bool permitAsync = false, [CallerMemberName] string methodName = null)
	{
		if (_isClosed)
		{
			throw ADP.DataReaderClosed(methodName ?? "CheckHeaderIsReady");
		}
		if (!permitAsync && _currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!_sharedState._dataReady || _metaData == null)
		{
			throw SQL.InvalidRead();
		}
		if (columnIndex < 0 || columnIndex >= _metaData.Length)
		{
			throw ADP.IndexOutOfRange();
		}
		if (IsCommandBehavior(CommandBehavior.SequentialAccess) && (_sharedState._nextColumnHeaderToRead > columnIndex + 1 || _lastColumnWithDataChunkRead > columnIndex))
		{
			throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(_sharedState._nextColumnHeaderToRead - 1, _lastColumnWithDataChunkRead));
		}
	}

	private void CheckDataIsReady(int columnIndex, bool allowPartiallyReadColumn = false, bool permitAsync = false, [CallerMemberName] string methodName = null)
	{
		if (_isClosed)
		{
			throw ADP.DataReaderClosed(methodName ?? "CheckDataIsReady");
		}
		if (!permitAsync && _currentTask != null)
		{
			throw ADP.AsyncOperationPending();
		}
		if (!_sharedState._dataReady || _metaData == null)
		{
			throw SQL.InvalidRead();
		}
		if (columnIndex < 0 || columnIndex >= _metaData.Length)
		{
			throw ADP.IndexOutOfRange();
		}
		if (IsCommandBehavior(CommandBehavior.SequentialAccess) && (_sharedState._nextColumnDataToRead > columnIndex || _lastColumnWithDataChunkRead > columnIndex || (!allowPartiallyReadColumn && _lastColumnWithDataChunkRead == columnIndex) || (allowPartiallyReadColumn && HasActiveStreamOrTextReaderOnColumn(columnIndex))))
		{
			throw ADP.NonSequentialColumnAccess(columnIndex, Math.Max(_sharedState._nextColumnDataToRead, _lastColumnWithDataChunkRead + 1));
		}
	}

	[Conditional("DEBUG")]
	private void AssertReaderState(bool requireData, bool permitAsync, int? columnIndex = null, bool enforceSequentialAccess = false)
	{
		_ = columnIndex.HasValue;
	}

	/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.NextResult" />, which advances the data reader to the next result, when reading the results of batch Transact-SQL statements.  
	///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses.  Exceptions will be reported via the returned Task object.</summary>
	/// <param name="cancellationToken">The cancellation instruction.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.NextResultAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.  
	///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
	/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
	public override Task<bool> NextResultAsync(CancellationToken cancellationToken)
	{
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		if (IsClosed)
		{
			taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("NextResultAsync")));
			return taskCompletionSource.Task;
		}
		IDisposable objectToDispose = null;
		if (cancellationToken.CanBeCanceled)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
				return taskCompletionSource.Task;
			}
			objectToDispose = cancellationToken.Register(delegate(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}, _command);
		}
		if (Interlocked.CompareExchange(ref _currentTask, taskCompletionSource.Task, null) != null)
		{
			taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
			return taskCompletionSource.Task;
		}
		if (_cancelAsyncOnCloseToken.IsCancellationRequested)
		{
			taskCompletionSource.SetCanceled();
			_currentTask = null;
			return taskCompletionSource.Task;
		}
		PrepareAsyncInvocation(useSnapshot: true);
		Func<Task, Task<bool>> moreFunc = null;
		moreFunc = delegate(Task t)
		{
			if (t != null)
			{
				PrepareForAsyncContinuation();
			}
			bool more;
			return TryNextResult(out more) ? ((!more) ? ADP.FalseTask : ADP.TrueTask) : ContinueRetryable(moreFunc);
		};
		return InvokeRetryable(moreFunc, taskCompletionSource, objectToDispose);
	}

	internal Task<int> GetBytesAsync(int i, byte[] buffer, int index, int length, int timeout, CancellationToken cancellationToken, out int bytesRead)
	{
		bytesRead = 0;
		if (IsClosed)
		{
			return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetBytesAsync")));
		}
		if (_currentTask != null)
		{
			return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
		}
		if (cancellationToken.CanBeCanceled && cancellationToken.IsCancellationRequested)
		{
			return null;
		}
		if (_sharedState._nextColumnHeaderToRead <= _lastColumnWithDataChunkRead || _sharedState._nextColumnDataToRead < _lastColumnWithDataChunkRead)
		{
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			if (Interlocked.CompareExchange(ref _currentTask, taskCompletionSource.Task, null) != null)
			{
				taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
				return taskCompletionSource.Task;
			}
			PrepareAsyncInvocation(useSnapshot: true);
			Func<Task, Task<int>> moreFunc = null;
			CancellationToken timeoutToken = CancellationToken.None;
			CancellationTokenSource cancellationTokenSource = null;
			if (timeout > 0)
			{
				cancellationTokenSource = new CancellationTokenSource();
				cancellationTokenSource.CancelAfter(timeout);
				timeoutToken = cancellationTokenSource.Token;
			}
			moreFunc = delegate(Task t)
			{
				if (t != null)
				{
					PrepareForAsyncContinuation();
				}
				SetTimeout(_defaultTimeoutMilliseconds);
				if (TryReadColumnHeader(i))
				{
					if (cancellationToken.IsCancellationRequested)
					{
						return Task.FromCanceled<int>(cancellationToken);
					}
					if (timeoutToken.IsCancellationRequested)
					{
						return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
					}
					SwitchToAsyncWithoutSnapshot();
					int bytesRead2;
					Task<int> bytesAsyncReadDataStage = GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, isContinuation: true, cancellationToken, timeoutToken, out bytesRead2);
					if (bytesAsyncReadDataStage == null)
					{
						return Task.FromResult(bytesRead2);
					}
					return bytesAsyncReadDataStage;
				}
				return ContinueRetryable(moreFunc);
			};
			return InvokeRetryable(moreFunc, taskCompletionSource, cancellationTokenSource);
		}
		PrepareAsyncInvocation(useSnapshot: false);
		try
		{
			return GetBytesAsyncReadDataStage(i, buffer, index, length, timeout, isContinuation: false, cancellationToken, CancellationToken.None, out bytesRead);
		}
		catch
		{
			CleanupAfterAsyncInvocation();
			throw;
		}
	}

	private Task<int> GetBytesAsyncReadDataStage(int i, byte[] buffer, int index, int length, int timeout, bool isContinuation, CancellationToken cancellationToken, CancellationToken timeoutToken, out int bytesRead)
	{
		_lastColumnWithDataChunkRead = i;
		TaskCompletionSource<int> source = null;
		CancellationTokenSource timeoutCancellationSource = null;
		SetTimeout(_defaultTimeoutMilliseconds);
		if (!TryGetBytesInternalSequential(i, buffer, index, length, out bytesRead))
		{
			int totalBytesRead = bytesRead;
			if (!isContinuation)
			{
				source = new TaskCompletionSource<int>();
				if (Interlocked.CompareExchange(ref _currentTask, source.Task, null) != null)
				{
					source.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
					return source.Task;
				}
				if (_cancelAsyncOnCloseToken.IsCancellationRequested)
				{
					source.SetCanceled();
					_currentTask = null;
					return source.Task;
				}
				if (timeout > 0)
				{
					timeoutCancellationSource = new CancellationTokenSource();
					timeoutCancellationSource.CancelAfter(timeout);
					timeoutToken = timeoutCancellationSource.Token;
				}
			}
			Func<Task, Task<int>> moreFunc = null;
			moreFunc = delegate
			{
				PrepareForAsyncContinuation();
				if (cancellationToken.IsCancellationRequested)
				{
					return Task.FromCanceled<int>(cancellationToken);
				}
				if (timeoutToken.IsCancellationRequested)
				{
					return Task.FromException<int>(ADP.ExceptionWithStackTrace(ADP.IO(SQLMessage.Timeout())));
				}
				SetTimeout(_defaultTimeoutMilliseconds);
				int bytesRead2;
				bool num = TryGetBytesInternalSequential(i, buffer, index + totalBytesRead, length - totalBytesRead, out bytesRead2);
				totalBytesRead += bytesRead2;
				return num ? Task.FromResult(totalBytesRead) : ContinueRetryable(moreFunc);
			};
			Task<int> task = ContinueRetryable(moreFunc);
			if (isContinuation)
			{
				return task;
			}
			task.ContinueWith(delegate(Task<int> t)
			{
				CompleteRetryable(t, source, timeoutCancellationSource);
			}, TaskScheduler.Default);
			return source.Task;
		}
		if (!isContinuation)
		{
			CleanupAfterAsyncInvocation();
		}
		return null;
	}

	/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.Read" />, which advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.  
	///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
	/// <param name="cancellationToken">The cancellation instruction.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="T:System.InvalidOperationException">Calling <see cref="M:System.Data.SqlClient.SqlDataReader.ReadAsync(System.Threading.CancellationToken)" /> more than once for the same instance before task completion.  
	///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
	/// <exception cref="T:System.Data.SqlClient.SqlException">SQL Server returned an error while executing the command text.</exception>
	public override Task<bool> ReadAsync(CancellationToken cancellationToken)
	{
		if (IsClosed)
		{
			return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("ReadAsync")));
		}
		if (cancellationToken.IsCancellationRequested)
		{
			return Task.FromCanceled<bool>(cancellationToken);
		}
		if (_currentTask != null)
		{
			return Task.FromException<bool>(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
		}
		bool rowTokenRead = false;
		bool more = false;
		try
		{
			if (!_haltRead && (!_sharedState._dataReady || WillHaveEnoughData(_metaData.Length - 1)))
			{
				if (_sharedState._dataReady)
				{
					CleanPartialReadReliable();
				}
				if (_stateObj.IsRowTokenReady())
				{
					TryReadInternal(setTimeout: true, out more);
					rowTokenRead = true;
					if (!more)
					{
						return ADP.FalseTask;
					}
					if (IsCommandBehavior(CommandBehavior.SequentialAccess))
					{
						return ADP.TrueTask;
					}
					if (WillHaveEnoughData(_metaData.Length - 1))
					{
						TryReadColumn(_metaData.Length - 1, setTimeout: true);
						return ADP.TrueTask;
					}
				}
			}
		}
		catch (Exception ex)
		{
			if (!ADP.IsCatchableExceptionType(ex))
			{
				throw;
			}
			return Task.FromException<bool>(ex);
		}
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		if (Interlocked.CompareExchange(ref _currentTask, taskCompletionSource.Task, null) != null)
		{
			taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(SQL.PendingBeginXXXExists()));
			return taskCompletionSource.Task;
		}
		if (_cancelAsyncOnCloseToken.IsCancellationRequested)
		{
			taskCompletionSource.SetCanceled();
			_currentTask = null;
			return taskCompletionSource.Task;
		}
		IDisposable objectToDispose = null;
		if (cancellationToken.CanBeCanceled)
		{
			objectToDispose = cancellationToken.Register(delegate(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}, _command);
		}
		PrepareAsyncInvocation(useSnapshot: true);
		Func<Task, Task<bool>> moreFunc = null;
		moreFunc = delegate(Task t)
		{
			if (t != null)
			{
				PrepareForAsyncContinuation();
			}
			if (rowTokenRead || TryReadInternal(setTimeout: true, out more))
			{
				if (!more || (_commandBehavior & CommandBehavior.SequentialAccess) == CommandBehavior.SequentialAccess)
				{
					if (!more)
					{
						return ADP.FalseTask;
					}
					return ADP.TrueTask;
				}
				if (!rowTokenRead)
				{
					rowTokenRead = true;
					_snapshot = null;
					PrepareAsyncInvocation(useSnapshot: true);
				}
				if (TryReadColumn(_metaData.Length - 1, setTimeout: true))
				{
					return ADP.TrueTask;
				}
			}
			return ContinueRetryable(moreFunc);
		};
		return InvokeRetryable(moreFunc, taskCompletionSource, objectToDispose);
	}

	/// <summary>An asynchronous version of <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />, which gets a value that indicates whether the column contains non-existent or missing values.  
	///  The cancellation token can be used to request that the operation be abandoned before the command timeout elapses. Exceptions will be reported via the returned Task object.</summary>
	/// <param name="i">The zero-based column to be retrieved.</param>
	/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
	/// <returns>
	///   <see langword="true" /> if the specified column value is equivalent to <see langword="DBNull" /> otherwise <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Trying to read a previously read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.  
	///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	public override Task<bool> IsDBNullAsync(int i, CancellationToken cancellationToken)
	{
		try
		{
			CheckHeaderIsReady(i, permitAsync: false, "IsDBNullAsync");
		}
		catch (Exception ex)
		{
			if (!ADP.IsCatchableExceptionType(ex))
			{
				throw;
			}
			return Task.FromException<bool>(ex);
		}
		if (_sharedState._nextColumnHeaderToRead > i && !cancellationToken.IsCancellationRequested && _currentTask == null)
		{
			SqlBuffer[] data = _data;
			if (data != null)
			{
				if (!data[i].IsNull)
				{
					return ADP.FalseTask;
				}
				return ADP.TrueTask;
			}
			return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("IsDBNullAsync")));
		}
		if (_currentTask != null)
		{
			return Task.FromException<bool>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
		}
		if (cancellationToken.IsCancellationRequested)
		{
			return Task.FromCanceled<bool>(cancellationToken);
		}
		try
		{
			if (WillHaveEnoughData(i, headerOnly: true))
			{
				ReadColumnHeader(i);
				return _data[i].IsNull ? ADP.TrueTask : ADP.FalseTask;
			}
		}
		catch (Exception ex2)
		{
			if (!ADP.IsCatchableExceptionType(ex2))
			{
				throw;
			}
			return Task.FromException<bool>(ex2);
		}
		TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
		if (Interlocked.CompareExchange(ref _currentTask, taskCompletionSource.Task, null) != null)
		{
			taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			return taskCompletionSource.Task;
		}
		if (_cancelAsyncOnCloseToken.IsCancellationRequested)
		{
			taskCompletionSource.SetCanceled();
			_currentTask = null;
			return taskCompletionSource.Task;
		}
		IDisposable objectToDispose = null;
		if (cancellationToken.CanBeCanceled)
		{
			objectToDispose = cancellationToken.Register(delegate(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}, _command);
		}
		PrepareAsyncInvocation(useSnapshot: true);
		Func<Task, Task<bool>> moreFunc = null;
		moreFunc = delegate(Task t)
		{
			if (t != null)
			{
				PrepareForAsyncContinuation();
			}
			return TryReadColumnHeader(i) ? ((!_data[i].IsNull) ? ADP.FalseTask : ADP.TrueTask) : ContinueRetryable(moreFunc);
		};
		return InvokeRetryable(moreFunc, taskCompletionSource, objectToDispose);
	}

	/// <summary>Asynchronously gets the value of the specified column as a type. <see cref="M:System.Data.SqlClient.SqlDataReader.GetFieldValue``1(System.Int32)" /> is the synchronous version of this method.</summary>
	/// <param name="i">The column to be retrieved.</param>
	/// <param name="cancellationToken">The cancellation instruction, which propagates a notification that operations should be canceled. This does not guarantee the cancellation. A setting of <see langword="CancellationToken.None" /> makes this method equivalent to <see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" />. The returned task must be marked as cancelled.</param>
	/// <typeparam name="T">The type of the value to be returned.</typeparam>
	/// <returns>The returned type object.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection drops or is closed during the data retrieval.  
	///  The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed during the data retrieval.  
	///  There is no data ready to be read (for example, the first <see cref="M:System.Data.SqlClient.SqlDataReader.Read" /> hasn't been called, or returned false).  
	///  Tried to read a previously-read column in sequential mode.  
	///  There was an asynchronous operation in progress. This applies to all Get* methods when running in sequential mode, as they could be called while reading a stream.  
	///  <see langword="Context Connection=true" /> is specified in the connection string.</exception>
	/// <exception cref="T:System.IndexOutOfRangeException">Trying to read a column that does not exist.</exception>
	/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the column was null (<see cref="M:System.Data.SqlClient.SqlDataReader.IsDBNull(System.Int32)" /> == <see langword="true" />), retrieving a non-SQL type.</exception>
	/// <exception cref="T:System.InvalidCastException">
	///   <paramref name="T" /> doesn't match the type returned by SQL Server or cannot be cast.</exception>
	public override Task<T> GetFieldValueAsync<T>(int i, CancellationToken cancellationToken)
	{
		try
		{
			CheckDataIsReady(i, allowPartiallyReadColumn: false, permitAsync: false, "GetFieldValueAsync");
			if (!IsCommandBehavior(CommandBehavior.SequentialAccess) && _sharedState._nextColumnDataToRead > i && !cancellationToken.IsCancellationRequested && _currentTask == null)
			{
				SqlBuffer[] data = _data;
				_SqlMetaDataSet metaData = _metaData;
				if (data != null && metaData != null)
				{
					return Task.FromResult(GetFieldValueFromSqlBufferInternal<T>(data[i], metaData[i]));
				}
				return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.DataReaderClosed("GetFieldValueAsync")));
			}
		}
		catch (Exception ex)
		{
			if (!ADP.IsCatchableExceptionType(ex))
			{
				throw;
			}
			return Task.FromException<T>(ex);
		}
		if (_currentTask != null)
		{
			return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
		}
		if (cancellationToken.IsCancellationRequested)
		{
			return Task.FromCanceled<T>(cancellationToken);
		}
		try
		{
			if (WillHaveEnoughData(i))
			{
				return Task.FromResult(GetFieldValueInternal<T>(i));
			}
		}
		catch (Exception ex2)
		{
			if (!ADP.IsCatchableExceptionType(ex2))
			{
				throw;
			}
			return Task.FromException<T>(ex2);
		}
		TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
		if (Interlocked.CompareExchange(ref _currentTask, taskCompletionSource.Task, null) != null)
		{
			taskCompletionSource.SetException(ADP.ExceptionWithStackTrace(ADP.AsyncOperationPending()));
			return taskCompletionSource.Task;
		}
		if (_cancelAsyncOnCloseToken.IsCancellationRequested)
		{
			taskCompletionSource.SetCanceled();
			_currentTask = null;
			return taskCompletionSource.Task;
		}
		IDisposable objectToDispose = null;
		if (cancellationToken.CanBeCanceled)
		{
			objectToDispose = cancellationToken.Register(delegate(object s)
			{
				((SqlCommand)s).CancelIgnoreFailure();
			}, _command);
		}
		PrepareAsyncInvocation(useSnapshot: true);
		Func<Task, Task<T>> moreFunc = null;
		moreFunc = delegate(Task t)
		{
			if (t != null)
			{
				PrepareForAsyncContinuation();
			}
			return TryReadColumn(i, setTimeout: false) ? Task.FromResult(GetFieldValueFromSqlBufferInternal<T>(_data[i], _metaData[i])) : ContinueRetryable(moreFunc);
		};
		return InvokeRetryable(moreFunc, taskCompletionSource, objectToDispose);
	}

	private Task<T> ContinueRetryable<T>(Func<Task, Task<T>> moreFunc)
	{
		TaskCompletionSource<object> networkPacketTaskSource = _stateObj._networkPacketTaskSource;
		if (_cancelAsyncOnCloseToken.IsCancellationRequested || networkPacketTaskSource == null)
		{
			return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
		}
		return networkPacketTaskSource.Task.ContinueWith(delegate(Task<object> retryTask)
		{
			if (retryTask.IsFaulted)
			{
				return Task.FromException<T>(retryTask.Exception.InnerException);
			}
			if (!_cancelAsyncOnCloseToken.IsCancellationRequested)
			{
				TdsParserStateObject stateObj = _stateObj;
				if (stateObj != null)
				{
					lock (stateObj)
					{
						if (_stateObj != null)
						{
							if (retryTask.IsCanceled)
							{
								if (_parser != null)
								{
									_parser.State = TdsParserState.Broken;
									_parser.Connection.BreakConnection();
									_parser.ThrowExceptionAndWarning(_stateObj);
								}
							}
							else if (!IsClosed)
							{
								try
								{
									return moreFunc(retryTask);
								}
								catch (Exception)
								{
									CleanupAfterAsyncInvocation();
									throw;
								}
							}
						}
					}
				}
			}
			return Task.FromException<T>(ADP.ExceptionWithStackTrace(ADP.ClosedConnectionError()));
		}, TaskScheduler.Default).Unwrap();
	}

	private Task<T> InvokeRetryable<T>(Func<Task, Task<T>> moreFunc, TaskCompletionSource<T> source, IDisposable objectToDispose = null)
	{
		try
		{
			Task<T> task;
			try
			{
				task = moreFunc(null);
			}
			catch (Exception exception)
			{
				task = Task.FromException<T>(exception);
			}
			if (task.IsCompleted)
			{
				CompleteRetryable(task, source, objectToDispose);
			}
			else
			{
				task.ContinueWith(delegate(Task<T> t)
				{
					CompleteRetryable(t, source, objectToDispose);
				}, TaskScheduler.Default);
			}
		}
		catch (AggregateException ex)
		{
			source.TrySetException(ex.InnerException);
		}
		catch (Exception exception2)
		{
			source.TrySetException(exception2);
		}
		return source.Task;
	}

	private void CompleteRetryable<T>(Task<T> task, TaskCompletionSource<T> source, IDisposable objectToDispose)
	{
		objectToDispose?.Dispose();
		bool ignoreCloseToken = _stateObj?._syncOverAsync ?? false;
		CleanupAfterAsyncInvocation(ignoreCloseToken);
		Interlocked.CompareExchange(ref _currentTask, null, source.Task);
		if (task.IsFaulted)
		{
			Exception innerException = task.Exception.InnerException;
			source.TrySetException(innerException);
		}
		else if (task.IsCanceled)
		{
			source.TrySetCanceled();
		}
		else
		{
			source.TrySetResult(task.Result);
		}
	}

	private void PrepareAsyncInvocation(bool useSnapshot)
	{
		if (useSnapshot)
		{
			if (_snapshot == null)
			{
				_snapshot = new Snapshot
				{
					_dataReady = _sharedState._dataReady,
					_haltRead = _haltRead,
					_metaDataConsumed = _metaDataConsumed,
					_browseModeInfoConsumed = _browseModeInfoConsumed,
					_hasRows = _hasRows,
					_altRowStatus = _altRowStatus,
					_nextColumnDataToRead = _sharedState._nextColumnDataToRead,
					_nextColumnHeaderToRead = _sharedState._nextColumnHeaderToRead,
					_columnDataBytesRead = _columnDataBytesRead,
					_columnDataBytesRemaining = _sharedState._columnDataBytesRemaining,
					_metadata = _metaData,
					_altMetaDataSetCollection = _altMetaDataSetCollection,
					_tableNames = _tableNames,
					_currentStream = _currentStream,
					_currentTextReader = _currentTextReader
				};
				_stateObj.SetSnapshot();
			}
		}
		else
		{
			_stateObj._asyncReadWithoutSnapshot = true;
		}
		_stateObj._syncOverAsync = false;
		_stateObj._executionContext = ExecutionContext.Capture();
	}

	private void CleanupAfterAsyncInvocation(bool ignoreCloseToken = false)
	{
		TdsParserStateObject stateObj = _stateObj;
		if (stateObj == null || (!ignoreCloseToken && _cancelAsyncOnCloseToken.IsCancellationRequested && !stateObj._asyncReadWithoutSnapshot))
		{
			return;
		}
		lock (stateObj)
		{
			if (_stateObj != null)
			{
				CleanupAfterAsyncInvocationInternal(_stateObj);
			}
		}
	}

	private void CleanupAfterAsyncInvocationInternal(TdsParserStateObject stateObj, bool resetNetworkPacketTaskSource = true)
	{
		if (resetNetworkPacketTaskSource)
		{
			stateObj._networkPacketTaskSource = null;
		}
		stateObj.ResetSnapshot();
		stateObj._syncOverAsync = true;
		stateObj._executionContext = null;
		stateObj._asyncReadWithoutSnapshot = false;
		_snapshot = null;
	}

	private void PrepareForAsyncContinuation()
	{
		if (_snapshot != null)
		{
			_sharedState._dataReady = _snapshot._dataReady;
			_haltRead = _snapshot._haltRead;
			_metaDataConsumed = _snapshot._metaDataConsumed;
			_browseModeInfoConsumed = _snapshot._browseModeInfoConsumed;
			_hasRows = _snapshot._hasRows;
			_altRowStatus = _snapshot._altRowStatus;
			_sharedState._nextColumnDataToRead = _snapshot._nextColumnDataToRead;
			_sharedState._nextColumnHeaderToRead = _snapshot._nextColumnHeaderToRead;
			_columnDataBytesRead = _snapshot._columnDataBytesRead;
			_sharedState._columnDataBytesRemaining = _snapshot._columnDataBytesRemaining;
			_metaData = _snapshot._metadata;
			_altMetaDataSetCollection = _snapshot._altMetaDataSetCollection;
			_tableNames = _snapshot._tableNames;
			_currentStream = _snapshot._currentStream;
			_currentTextReader = _snapshot._currentTextReader;
			_stateObj.PrepareReplaySnapshot();
		}
		_stateObj._executionContext = ExecutionContext.Capture();
	}

	private void SwitchToAsyncWithoutSnapshot()
	{
		_snapshot = null;
		_stateObj.ResetSnapshot();
		_stateObj._asyncReadWithoutSnapshot = true;
	}

	private Exception UdtNotSupportedException()
	{
		return SQL.UnsupportedFeatureAndToken(_parser.Connection, SqlDbType.Udt.ToString());
	}

	public ReadOnlyCollection<DbColumn> GetColumnSchema()
	{
		SqlStatistics statistics = null;
		try
		{
			statistics = SqlStatistics.StartTimer(Statistics);
			if ((_metaData == null || _metaData.dbColumnSchema == null) && MetaData != null)
			{
				_metaData.dbColumnSchema = BuildColumnSchema();
			}
			if (_metaData != null)
			{
				return _metaData.dbColumnSchema;
			}
			return s_emptySchema;
		}
		finally
		{
			SqlStatistics.StopTimer(statistics);
		}
	}

	private ReadOnlyCollection<DbColumn> BuildColumnSchema()
	{
		_SqlMetaDataSet metaData = MetaData;
		DbColumn[] array = new DbColumn[metaData.Length];
		for (int i = 0; i < metaData.Length; i++)
		{
			_SqlMetaData sqlMetaData = metaData[i];
			SqlDbColumn sqlDbColumn = new SqlDbColumn(metaData[i]);
			if (_typeSystem <= SqlConnectionString.TypeSystem.SQLServer2005 && sqlMetaData.IsNewKatmaiDateTimeType)
			{
				sqlDbColumn.SqlNumericScale = MetaType.MetaNVarChar.Scale;
			}
			else if (byte.MaxValue != sqlMetaData.scale)
			{
				sqlDbColumn.SqlNumericScale = sqlMetaData.scale;
			}
			else
			{
				sqlDbColumn.SqlNumericScale = sqlMetaData.metaType.Scale;
			}
			if (_browseModeInfoConsumed)
			{
				sqlDbColumn.SqlIsAliased = sqlMetaData.isDifferentName;
				sqlDbColumn.SqlIsKey = sqlMetaData.isKey;
				sqlDbColumn.SqlIsHidden = sqlMetaData.isHidden;
				sqlDbColumn.SqlIsExpression = sqlMetaData.isExpression;
			}
			sqlDbColumn.SqlDataType = GetFieldTypeInternal(sqlMetaData);
			sqlDbColumn.SqlDataTypeName = GetDataTypeNameInternal(sqlMetaData);
			array[i] = sqlDbColumn;
		}
		return new ReadOnlyCollection<DbColumn>(array);
	}
}
