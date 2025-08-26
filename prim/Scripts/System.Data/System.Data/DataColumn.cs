using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data;

/// <summary>Represents the schema of a column in a <see cref="T:System.Data.DataTable" />.</summary>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DefaultProperty("ColumnName")]
public class DataColumn : MarshalByValueComponent
{
	private bool _allowNull = true;

	private string _caption;

	private string _columnName;

	private Type _dataType;

	private StorageType _storageType;

	internal object _defaultValue = DBNull.Value;

	private DataSetDateTime _dateTimeMode = DataSetDateTime.UnspecifiedLocal;

	private DataExpression _expression;

	private int _maxLength = -1;

	private int _ordinal = -1;

	private bool _readOnly;

	internal Index _sortIndex;

	internal DataTable _table;

	private bool _unique;

	internal MappingType _columnMapping = MappingType.Element;

	internal int _hashCode;

	internal int _errors;

	private bool _isSqlType;

	private bool _implementsINullable;

	private bool _implementsIChangeTracking;

	private bool _implementsIRevertibleChangeTracking;

	private bool _implementsIXMLSerializable;

	private bool _defaultValueIsNull = true;

	internal List<DataColumn> _dependentColumns;

	internal PropertyCollection _extendedProperties;

	private DataStorage _storage;

	private AutoIncrementValue _autoInc;

	internal string _columnUri;

	private string _columnPrefix = string.Empty;

	internal string _encodedColumnName;

	internal SimpleType _simpleType;

	private static int s_objectTypeCount;

	private readonly int _objectID = Interlocked.Increment(ref s_objectTypeCount);

	/// <summary>Gets or sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</summary>
	/// <returns>
	///   <see langword="true" /> if null values values are allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public bool AllowDBNull
	{
		get
		{
			return _allowNull;
		}
		set
		{
			long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataColumn.set_AllowDBNull|API> {0}, {1}", ObjectID, value);
			try
			{
				if (_allowNull != value)
				{
					if (_table != null && !value && _table.EnforceConstraints)
					{
						CheckNotAllowNull();
					}
					_allowNull = value;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the column automatically increments the value of the column for new rows added to the table.</summary>
	/// <returns>
	///   <see langword="true" /> if the value of the column increments automatically; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">The column is a computed column.</exception>
	[DefaultValue(false)]
	[RefreshProperties(RefreshProperties.All)]
	public bool AutoIncrement
	{
		get
		{
			if (_autoInc != null)
			{
				return _autoInc.Auto;
			}
			return false;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_AutoIncrement|API> {0}, {1}", ObjectID, value);
			if (AutoIncrement == value)
			{
				return;
			}
			if (value)
			{
				if (_expression != null)
				{
					throw ExceptionBuilder.AutoIncrementAndExpression();
				}
				if (!DefaultValueIsNull)
				{
					throw ExceptionBuilder.AutoIncrementAndDefaultValue();
				}
				if (!IsAutoIncrementType(DataType))
				{
					if (HasData)
					{
						throw ExceptionBuilder.AutoIncrementCannotSetIfHasData(DataType.Name);
					}
					DataType = typeof(int);
				}
			}
			AutoInc.Auto = value;
		}
	}

	internal object AutoIncrementCurrent
	{
		get
		{
			if (_autoInc == null)
			{
				return AutoIncrementSeed;
			}
			return _autoInc.Current;
		}
		set
		{
			if ((BigInteger)AutoIncrementSeed != BigIntegerStorage.ConvertToBigInteger(value, FormatProvider))
			{
				AutoInc.SetCurrent(value, FormatProvider);
			}
		}
	}

	internal AutoIncrementValue AutoInc => _autoInc ?? (_autoInc = ((DataType == typeof(BigInteger)) ? ((AutoIncrementValue)new AutoIncrementBigInteger()) : ((AutoIncrementValue)new AutoIncrementInt64())));

	/// <summary>Gets or sets the starting value for a column that has its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to <see langword="true" />. The default is 0.</summary>
	/// <returns>The starting value for the <see cref="P:System.Data.DataColumn.AutoIncrement" /> feature.</returns>
	[DefaultValue(0L)]
	public long AutoIncrementSeed
	{
		get
		{
			if (_autoInc == null)
			{
				return 0L;
			}
			return _autoInc.Seed;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_AutoIncrementSeed|API> {0}, {1}", ObjectID, value);
			if (AutoIncrementSeed != value)
			{
				AutoInc.Seed = value;
			}
		}
	}

	/// <summary>Gets or sets the increment used by a column with its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to <see langword="true" />.</summary>
	/// <returns>The number by which the value of the column is automatically incremented. The default is 1.</returns>
	/// <exception cref="T:System.ArgumentException">The value set is zero.</exception>
	[DefaultValue(1L)]
	public long AutoIncrementStep
	{
		get
		{
			if (_autoInc == null)
			{
				return 1L;
			}
			return _autoInc.Step;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_AutoIncrementStep|API> {0}, {1}", ObjectID, value);
			if (AutoIncrementStep != value)
			{
				AutoInc.Step = value;
			}
		}
	}

	/// <summary>Gets or sets the caption for the column.</summary>
	/// <returns>The caption of the column. If not set, returns the <see cref="P:System.Data.DataColumn.ColumnName" /> value.</returns>
	public string Caption
	{
		get
		{
			if (_caption == null)
			{
				return _columnName;
			}
			return _caption;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (_caption == null || string.Compare(_caption, value, ignoreCase: true, Locale) != 0)
			{
				_caption = value;
			}
		}
	}

	/// <summary>Gets or sets the name of the column in the <see cref="T:System.Data.DataColumnCollection" />.</summary>
	/// <returns>The name of the column.</returns>
	/// <exception cref="T:System.ArgumentException">The property is set to <see langword="null" /> or an empty string and the column belongs to a collection.</exception>
	/// <exception cref="T:System.Data.DuplicateNameException">A column with the same name already exists in the collection. The name comparison is not case sensitive.</exception>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue("")]
	public string ColumnName
	{
		get
		{
			return _columnName;
		}
		set
		{
			long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataColumn.set_ColumnName|API> {0}, '{1}'", ObjectID, value);
			try
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (string.Compare(_columnName, value, ignoreCase: true, Locale) != 0)
				{
					if (_table != null)
					{
						if (value.Length == 0)
						{
							throw ExceptionBuilder.ColumnNameRequired();
						}
						_table.Columns.RegisterColumnName(value, this);
						if (_columnName.Length != 0)
						{
							_table.Columns.UnregisterName(_columnName);
						}
					}
					RaisePropertyChanging("ColumnName");
					_columnName = value;
					_encodedColumnName = null;
					if (_table != null)
					{
						_table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
				}
				else if (_columnName != value)
				{
					RaisePropertyChanging("ColumnName");
					_columnName = value;
					_encodedColumnName = null;
					if (_table != null)
					{
						_table.Columns.OnColumnPropertyChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}
	}

	internal string EncodedColumnName
	{
		get
		{
			if (_encodedColumnName == null)
			{
				_encodedColumnName = XmlConvert.EncodeLocalName(ColumnName);
			}
			return _encodedColumnName;
		}
	}

	internal IFormatProvider FormatProvider
	{
		get
		{
			if (_table == null)
			{
				return CultureInfo.CurrentCulture;
			}
			return _table.FormatProvider;
		}
	}

	internal CultureInfo Locale
	{
		get
		{
			if (_table == null)
			{
				return CultureInfo.CurrentCulture;
			}
			return _table.Locale;
		}
	}

	internal int ObjectID => _objectID;

	/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataTable" />.</summary>
	/// <returns>The XML prefix for the <see cref="T:System.Data.DataTable" /> namespace.</returns>
	[DefaultValue("")]
	public string Prefix
	{
		get
		{
			return _columnPrefix;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_Prefix|API> {0}, '{1}'", ObjectID, value);
			if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
			{
				throw ExceptionBuilder.InvalidPrefix(value);
			}
			_columnPrefix = value;
		}
	}

	internal bool Computed => _expression != null;

	internal DataExpression DataExpression => _expression;

	/// <summary>Gets or sets the type of data stored in the column.</summary>
	/// <returns>A <see cref="T:System.Type" /> object that represents the column data type.</returns>
	/// <exception cref="T:System.ArgumentException">The column already has data stored.</exception>
	[DefaultValue(typeof(string))]
	[RefreshProperties(RefreshProperties.All)]
	[TypeConverter(typeof(ColumnTypeConverter))]
	public Type DataType
	{
		get
		{
			return _dataType;
		}
		set
		{
			if (!(_dataType != value))
			{
				return;
			}
			if (HasData)
			{
				throw ExceptionBuilder.CantChangeDataType();
			}
			if (value == null)
			{
				throw ExceptionBuilder.NullDataType();
			}
			StorageType storageType = DataStorage.GetStorageType(value);
			if (DataStorage.ImplementsINullableValue(storageType, value))
			{
				throw ExceptionBuilder.ColumnTypeNotSupported();
			}
			if (_table != null && IsInRelation())
			{
				throw ExceptionBuilder.ColumnsTypeMismatch();
			}
			if (storageType == StorageType.BigInteger && _expression != null)
			{
				throw ExprException.UnsupportedDataType(value);
			}
			if (!DefaultValueIsNull)
			{
				try
				{
					if (_defaultValue is BigInteger)
					{
						_defaultValue = BigIntegerStorage.ConvertFromBigInteger((BigInteger)_defaultValue, value, FormatProvider);
					}
					else if (typeof(BigInteger) == value)
					{
						_defaultValue = BigIntegerStorage.ConvertToBigInteger(_defaultValue, FormatProvider);
					}
					else if (typeof(string) == value)
					{
						_defaultValue = DefaultValue.ToString();
					}
					else if (typeof(SqlString) == value)
					{
						_defaultValue = SqlConvert.ConvertToSqlString(DefaultValue);
					}
					else if (typeof(object) != value)
					{
						DefaultValue = SqlConvert.ChangeTypeForDefaultValue(DefaultValue, value, FormatProvider);
					}
				}
				catch (InvalidCastException inner)
				{
					throw ExceptionBuilder.DefaultValueDataType(ColumnName, DefaultValue.GetType(), value, inner);
				}
				catch (FormatException inner2)
				{
					throw ExceptionBuilder.DefaultValueDataType(ColumnName, DefaultValue.GetType(), value, inner2);
				}
			}
			if (ColumnMapping == MappingType.SimpleContent && value == typeof(char))
			{
				throw ExceptionBuilder.CannotSetSimpleContentType(ColumnName, value);
			}
			SimpleType = SimpleType.CreateSimpleType(storageType, value);
			if (StorageType.String == storageType)
			{
				_maxLength = -1;
			}
			UpdateColumnType(value, storageType);
			XmlDataType = null;
			if (!AutoIncrement)
			{
				return;
			}
			if (!IsAutoIncrementType(value))
			{
				AutoIncrement = false;
			}
			if (_autoInc != null)
			{
				AutoIncrementValue autoInc = _autoInc;
				_autoInc = null;
				AutoInc.Auto = autoInc.Auto;
				AutoInc.Seed = autoInc.Seed;
				AutoInc.Step = autoInc.Step;
				if (_autoInc.DataType == autoInc.DataType)
				{
					_autoInc.Current = autoInc.Current;
				}
				else if (autoInc.DataType == typeof(long))
				{
					AutoInc.Current = (BigInteger)(long)autoInc.Current;
				}
				else
				{
					AutoInc.Current = (long)(BigInteger)autoInc.Current;
				}
			}
		}
	}

	/// <summary>Gets or sets the <see langword="DateTimeMode" /> for the column.</summary>
	/// <returns>The <see cref="T:System.Data.DataSetDateTime" /> for the specified column.</returns>
	[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
	[RefreshProperties(RefreshProperties.All)]
	public DataSetDateTime DateTimeMode
	{
		get
		{
			return _dateTimeMode;
		}
		set
		{
			if (_dateTimeMode == value)
			{
				return;
			}
			if (DataType != typeof(DateTime) && value != DataSetDateTime.UnspecifiedLocal)
			{
				throw ExceptionBuilder.CannotSetDateTimeModeForNonDateTimeColumns();
			}
			switch (value)
			{
			case DataSetDateTime.Local:
			case DataSetDateTime.Utc:
				if (HasData)
				{
					throw ExceptionBuilder.CantChangeDateTimeMode(_dateTimeMode, value);
				}
				break;
			case DataSetDateTime.Unspecified:
			case DataSetDateTime.UnspecifiedLocal:
				if (_dateTimeMode != DataSetDateTime.Unspecified && _dateTimeMode != DataSetDateTime.UnspecifiedLocal && HasData)
				{
					throw ExceptionBuilder.CantChangeDateTimeMode(_dateTimeMode, value);
				}
				break;
			default:
				throw ExceptionBuilder.InvalidDateTimeMode(value);
			}
			_dateTimeMode = value;
		}
	}

	/// <summary>Gets or sets the default value for the column when you are creating new rows.</summary>
	/// <returns>A value appropriate to the column's <see cref="P:System.Data.DataColumn.DataType" />.</returns>
	/// <exception cref="T:System.InvalidCastException">When you are adding a row, the default value is not an instance of the column's data type.</exception>
	[TypeConverter(typeof(DefaultValueTypeConverter))]
	public object DefaultValue
	{
		get
		{
			if (_defaultValue == DBNull.Value && _implementsINullable)
			{
				if (_storage != null)
				{
					_defaultValue = _storage._nullValue;
				}
				else if (_isSqlType)
				{
					_defaultValue = SqlConvert.ChangeTypeForDefaultValue(_defaultValue, _dataType, FormatProvider);
				}
				else if (_implementsINullable)
				{
					PropertyInfo property = _dataType.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
					if (property != null)
					{
						_defaultValue = property.GetValue(null, null);
					}
				}
			}
			return _defaultValue;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_DefaultValue|API> {0}", ObjectID);
			if (_defaultValue != null && DefaultValue.Equals(value))
			{
				return;
			}
			if (AutoIncrement)
			{
				throw ExceptionBuilder.DefaultValueAndAutoIncrement();
			}
			object obj = ((value == null) ? DBNull.Value : value);
			if (obj != DBNull.Value && DataType != typeof(object))
			{
				try
				{
					obj = SqlConvert.ChangeTypeForDefaultValue(obj, DataType, FormatProvider);
				}
				catch (InvalidCastException inner)
				{
					throw ExceptionBuilder.DefaultValueColumnDataType(ColumnName, obj.GetType(), DataType, inner);
				}
			}
			_defaultValue = obj;
			_defaultValueIsNull = ((obj == DBNull.Value || (ImplementsINullable && DataStorage.IsObjectSqlNull(obj))) ? true : false);
		}
	}

	internal bool DefaultValueIsNull => _defaultValueIsNull;

	/// <summary>Gets or sets the expression used to filter rows, calculate the values in a column, or create an aggregate column.</summary>
	/// <returns>An expression to calculate the value of a column, or create an aggregate column. The return type of an expression is determined by the <see cref="P:System.Data.DataColumn.DataType" /> of the column.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Data.DataColumn.AutoIncrement" /> or <see cref="P:System.Data.DataColumn.Unique" /> property is set to <see langword="true" />.</exception>
	/// <exception cref="T:System.FormatException">When you are using the CONVERT function, the expression evaluates to a string, but the string does not contain a representation that can be converted to the type parameter.</exception>
	/// <exception cref="T:System.InvalidCastException">When you are using the CONVERT function, the requested cast is not possible. See the Conversion function in the following section for detailed information about possible casts.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">When you use the SUBSTRING function, the start argument is out of range.  
	///  -Or-  
	///  When you use the SUBSTRING function, the length argument is out of range.</exception>
	/// <exception cref="T:System.Exception">When you use the LEN function or the TRIM function, the expression does not evaluate to a string. This includes expressions that evaluate to <see cref="T:System.Char" />.</exception>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue("")]
	public string Expression
	{
		get
		{
			if (_expression != null)
			{
				return _expression.Expression;
			}
			return "";
		}
		set
		{
			long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataColumn.set_Expression|API> {0}, '{1}'", ObjectID, value);
			if (value == null)
			{
				value = string.Empty;
			}
			try
			{
				DataExpression dataExpression = null;
				if (value.Length > 0)
				{
					DataExpression dataExpression2 = new DataExpression(_table, value, _dataType);
					if (dataExpression2.HasValue)
					{
						dataExpression = dataExpression2;
					}
				}
				if (_expression == null && dataExpression != null)
				{
					if (AutoIncrement || Unique)
					{
						throw ExceptionBuilder.ExpressionAndUnique();
					}
					if (_table != null)
					{
						for (int i = 0; i < _table.Constraints.Count; i++)
						{
							if (_table.Constraints[i].ContainsColumn(this))
							{
								throw ExceptionBuilder.ExpressionAndConstraint(this, _table.Constraints[i]);
							}
						}
					}
					bool readOnly = ReadOnly;
					try
					{
						ReadOnly = true;
					}
					catch (ReadOnlyException e)
					{
						ExceptionBuilder.TraceExceptionForCapture(e);
						ReadOnly = readOnly;
						throw ExceptionBuilder.ExpressionAndReadOnly();
					}
				}
				if (_table != null)
				{
					if (dataExpression != null && dataExpression.DependsOn(this))
					{
						throw ExceptionBuilder.ExpressionCircular();
					}
					HandleDependentColumnList(_expression, dataExpression);
					DataExpression expression = _expression;
					_expression = dataExpression;
					try
					{
						if (dataExpression == null)
						{
							for (int j = 0; j < _table.RecordCapacity; j++)
							{
								InitializeRecord(j);
							}
						}
						else
						{
							_table.EvaluateExpressions(this);
						}
						_table.ResetInternalIndexes(this);
						_table.EvaluateDependentExpressions(this);
						return;
					}
					catch (Exception e2) when (ADP.IsCatchableExceptionType(e2))
					{
						ExceptionBuilder.TraceExceptionForCapture(e2);
						try
						{
							_expression = expression;
							HandleDependentColumnList(dataExpression, _expression);
							if (expression == null)
							{
								for (int k = 0; k < _table.RecordCapacity; k++)
								{
									InitializeRecord(k);
								}
							}
							else
							{
								_table.EvaluateExpressions(this);
							}
							_table.ResetInternalIndexes(this);
							_table.EvaluateDependentExpressions(this);
						}
						catch (Exception e3) when (ADP.IsCatchableExceptionType(e3))
						{
							ExceptionBuilder.TraceExceptionWithoutRethrow(e3);
						}
						throw;
					}
				}
				_expression = dataExpression;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}
	}

	/// <summary>Gets the collection of custom user information associated with a <see cref="T:System.Data.DataColumn" />.</summary>
	/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
	[Browsable(false)]
	public PropertyCollection ExtendedProperties => _extendedProperties ?? (_extendedProperties = new PropertyCollection());

	internal bool HasData => _storage != null;

	internal bool ImplementsINullable => _implementsINullable;

	internal bool ImplementsIChangeTracking => _implementsIChangeTracking;

	internal bool ImplementsIRevertibleChangeTracking => _implementsIRevertibleChangeTracking;

	internal bool IsCloneable => _storage._isCloneable;

	internal bool IsStringType => _storage._isStringType;

	internal bool IsValueType => _storage._isValueType;

	internal bool IsSqlType => _isSqlType;

	/// <summary>Gets or sets the maximum length of a text column.</summary>
	/// <returns>The maximum length of the column in characters. If the column has no maximum length, the value is -1 (default).</returns>
	[DefaultValue(-1)]
	public int MaxLength
	{
		get
		{
			return _maxLength;
		}
		set
		{
			long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataColumn.set_MaxLength|API> {0}, {1}", ObjectID, value);
			try
			{
				if (_maxLength != value)
				{
					if (ColumnMapping == MappingType.SimpleContent)
					{
						throw ExceptionBuilder.CannotSetMaxLength2(this);
					}
					if (DataType != typeof(string) && DataType != typeof(SqlString))
					{
						throw ExceptionBuilder.HasToBeStringType(this);
					}
					int maxLength = _maxLength;
					_maxLength = Math.Max(value, -1);
					if ((maxLength < 0 || value < maxLength) && _table != null && _table.EnforceConstraints && !CheckMaxLength())
					{
						_maxLength = maxLength;
						throw ExceptionBuilder.CannotSetMaxLength(this, value);
					}
					SetMaxLengthSimpleType();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}
	}

	/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataColumn" />.</summary>
	/// <returns>The namespace of the <see cref="T:System.Data.DataColumn" />.</returns>
	/// <exception cref="T:System.ArgumentException">The namespace already has data.</exception>
	public string Namespace
	{
		get
		{
			if (_columnUri == null)
			{
				if (Table != null && _columnMapping != MappingType.Attribute)
				{
					return Table.Namespace;
				}
				return string.Empty;
			}
			return _columnUri;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_Namespace|API> {0}, '{1}'", ObjectID, value);
			if (_columnUri != value)
			{
				if (_columnMapping != MappingType.SimpleContent)
				{
					RaisePropertyChanging("Namespace");
					_columnUri = value;
				}
				else if (value != Namespace)
				{
					throw ExceptionBuilder.CannotChangeNamespace(ColumnName);
				}
			}
		}
	}

	/// <summary>Gets the (zero-based) position of the column in the <see cref="T:System.Data.DataColumnCollection" /> collection.</summary>
	/// <returns>The position of the column. Gets -1 if the column is not a member of a collection.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Ordinal => _ordinal;

	/// <summary>Gets or sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</summary>
	/// <returns>
	///   <see langword="true" /> if the column is read only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property is set to <see langword="false" /> on a computed column.</exception>
	[DefaultValue(false)]
	public bool ReadOnly
	{
		get
		{
			return _readOnly;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_ReadOnly|API> {0}, {1}", ObjectID, value);
			if (_readOnly != value)
			{
				if (!value && _expression != null)
				{
					throw ExceptionBuilder.ReadOnlyAndExpression();
				}
				_readOnly = value;
			}
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Index SortIndex
	{
		get
		{
			if (_sortIndex == null)
			{
				IndexField[] indexDesc = new IndexField[1]
				{
					new IndexField(this, isDescending: false)
				};
				_sortIndex = _table.GetIndex(indexDesc, DataViewRowState.CurrentRows, null);
				_sortIndex.AddRef();
			}
			return _sortIndex;
		}
	}

	/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the column belongs to.</summary>
	/// <returns>The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataColumn" /> belongs to.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataTable Table => _table;

	internal object this[int record]
	{
		get
		{
			return _storage.Get(record);
		}
		set
		{
			try
			{
				_storage.Set(record, value);
			}
			catch (Exception ex)
			{
				ExceptionBuilder.TraceExceptionForCapture(ex);
				throw ExceptionBuilder.SetFailed(value, this, DataType, ex);
			}
			if (AutoIncrement && !_storage.IsNull(record))
			{
				AutoInc.SetCurrentAndIncrement(_storage.Get(record));
			}
			if (Computed)
			{
				DataRow dataRow = GetDataRow(record);
				if (dataRow != null)
				{
					dataRow.LastChangedColumn = this;
				}
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the values in each row of the column must be unique.</summary>
	/// <returns>
	///   <see langword="true" /> if the value must be unique; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">The column is a calculated column.</exception>
	[DefaultValue(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool Unique
	{
		get
		{
			return _unique;
		}
		set
		{
			long scopeId = DataCommonEventSource.Log.EnterScope("<ds.DataColumn.set_Unique|API> {0}, {1}", ObjectID, value);
			try
			{
				if (_unique == value)
				{
					return;
				}
				if (value && _expression != null)
				{
					throw ExceptionBuilder.UniqueAndExpression();
				}
				UniqueConstraint constraint = null;
				if (_table != null)
				{
					if (value)
					{
						CheckUnique();
					}
					else
					{
						IEnumerator enumerator = Table.Constraints.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (enumerator.Current is UniqueConstraint uniqueConstraint && uniqueConstraint.ColumnsReference.Length == 1 && uniqueConstraint.ColumnsReference[0] == this)
							{
								constraint = uniqueConstraint;
							}
						}
						_table.Constraints.CanRemove(constraint, fThrowException: true);
					}
				}
				_unique = value;
				if (_table != null)
				{
					if (value)
					{
						UniqueConstraint constraint2 = new UniqueConstraint(this);
						_table.Constraints.Add(constraint2);
					}
					else
					{
						_table.Constraints.Remove(constraint);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}
	}

	internal string XmlDataType { get; set; } = string.Empty;

	internal SimpleType SimpleType
	{
		get
		{
			return _simpleType;
		}
		set
		{
			_simpleType = value;
			if (value != null && value.CanHaveMaxLength())
			{
				_maxLength = _simpleType.MaxLength;
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Data.MappingType" /> of the column.</summary>
	/// <returns>One of the <see cref="T:System.Data.MappingType" /> values.</returns>
	[DefaultValue(MappingType.Element)]
	public virtual MappingType ColumnMapping
	{
		get
		{
			return _columnMapping;
		}
		set
		{
			DataCommonEventSource.Log.Trace("<ds.DataColumn.set_ColumnMapping|API> {0}, {1}", ObjectID, value);
			if (value == _columnMapping)
			{
				return;
			}
			if (value == MappingType.SimpleContent && _table != null)
			{
				int num = 0;
				if (_columnMapping == MappingType.Element)
				{
					num = 1;
				}
				if (_dataType == typeof(char))
				{
					throw ExceptionBuilder.CannotSetSimpleContent(ColumnName, _dataType);
				}
				if (_table.XmlText != null && _table.XmlText != this)
				{
					throw ExceptionBuilder.CannotAddColumn3();
				}
				if (_table.ElementColumnCount > num)
				{
					throw ExceptionBuilder.CannotAddColumn4(ColumnName);
				}
			}
			RaisePropertyChanging("ColumnMapping");
			if (_table != null)
			{
				if (_columnMapping == MappingType.SimpleContent)
				{
					_table._xmlText = null;
				}
				if (value == MappingType.Element)
				{
					_table.ElementColumnCount++;
				}
				else if (_columnMapping == MappingType.Element)
				{
					_table.ElementColumnCount--;
				}
			}
			_columnMapping = value;
			if (value == MappingType.SimpleContent)
			{
				_columnUri = null;
				if (_table != null)
				{
					_table.XmlText = this;
				}
				SimpleType = null;
			}
		}
	}

	internal bool IsCustomType
	{
		get
		{
			if (_storage == null)
			{
				return DataStorage.IsTypeCustomType(DataType);
			}
			return _storage._isCustomDefinedType;
		}
	}

	internal bool ImplementsIXMLSerializable => _implementsIXMLSerializable;

	internal event PropertyChangedEventHandler PropertyChanging;

	/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataColumn" /> class as type string.</summary>
	public DataColumn()
		: this(null, typeof(string), null, MappingType.Element)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class, as type string, using the specified column name.</summary>
	/// <param name="columnName">A string that represents the name of the column to be created. If set to <see langword="null" /> or an empty string (""), a default name will be specified when added to the columns collection.</param>
	public DataColumn(string columnName)
		: this(columnName, typeof(string), null, MappingType.Element)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified column name and data type.</summary>
	/// <param name="columnName">A string that represents the name of the column to be created. If set to <see langword="null" /> or an empty string (""), a default name will be specified when added to the columns collection.</param>
	/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />.</param>
	/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified.</exception>
	public DataColumn(string columnName, Type dataType)
		: this(columnName, dataType, null, MappingType.Element)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, and expression.</summary>
	/// <param name="columnName">A string that represents the name of the column to be created. If set to <see langword="null" /> or an empty string (""), a default name will be specified when added to the columns collection.</param>
	/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />.</param>
	/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property.</param>
	/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified.</exception>
	public DataColumn(string columnName, Type dataType, string expr)
		: this(columnName, dataType, expr, MappingType.Element)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, expression, and value that determines whether the column is an attribute.</summary>
	/// <param name="columnName">A string that represents the name of the column to be created. If set to <see langword="null" /> or an empty string (""), a default name will be specified when added to the columns collection.</param>
	/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />.</param>
	/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property.</param>
	/// <param name="type">One of the <see cref="T:System.Data.MappingType" /> values.</param>
	/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified.</exception>
	public DataColumn(string columnName, Type dataType, string expr, MappingType type)
	{
		GC.SuppressFinalize(this);
		DataCommonEventSource.Log.Trace("<ds.DataColumn.DataColumn|API> {0}, columnName='{1}', expr='{2}', type={3}", ObjectID, columnName, expr, type);
		if (dataType == null)
		{
			throw ExceptionBuilder.ArgumentNull("dataType");
		}
		StorageType storageType = DataStorage.GetStorageType(dataType);
		if (DataStorage.ImplementsINullableValue(storageType, dataType))
		{
			throw ExceptionBuilder.ColumnTypeNotSupported();
		}
		_columnName = columnName ?? string.Empty;
		SimpleType simpleType = SimpleType.CreateSimpleType(storageType, dataType);
		if (simpleType != null)
		{
			SimpleType = simpleType;
		}
		UpdateColumnType(dataType, storageType);
		if (!string.IsNullOrEmpty(expr))
		{
			Expression = expr;
		}
		_columnMapping = type;
	}

	private void UpdateColumnType(Type type, StorageType typeCode)
	{
		_dataType = type;
		_storageType = typeCode;
		if (StorageType.DateTime != typeCode)
		{
			_dateTimeMode = DataSetDateTime.UnspecifiedLocal;
		}
		DataStorage.ImplementsInterfaces(typeCode, type, out _isSqlType, out _implementsINullable, out _implementsIXMLSerializable, out _implementsIChangeTracking, out _implementsIRevertibleChangeTracking);
		if (!_isSqlType && _implementsINullable)
		{
			SqlUdtStorage.GetStaticNullForUdtType(type);
		}
	}

	private void ResetCaption()
	{
		if (_caption != null)
		{
			_caption = null;
		}
	}

	private bool ShouldSerializeCaption()
	{
		return _caption != null;
	}

	internal string GetColumnValueAsString(DataRow row, DataRowVersion version)
	{
		object value = this[row.GetRecordFromVersion(version)];
		if (DataStorage.IsObjectNull(value))
		{
			return null;
		}
		return ConvertObjectToXml(value);
	}

	internal void BindExpression()
	{
		DataExpression.Bind(_table);
	}

	private void SetMaxLengthSimpleType()
	{
		if (_simpleType != null)
		{
			_simpleType.MaxLength = _maxLength;
			if (_simpleType.IsPlainString())
			{
				_simpleType = null;
			}
			else if (_simpleType.Name != null && XmlDataType != null)
			{
				_simpleType.ConvertToAnnonymousSimpleType();
				XmlDataType = null;
			}
		}
		else if (-1 < _maxLength)
		{
			SimpleType = SimpleType.CreateLimitedStringType(_maxLength);
		}
	}

	private bool ShouldSerializeNamespace()
	{
		return _columnUri != null;
	}

	private void ResetNamespace()
	{
		Namespace = null;
	}

	/// <summary>Changes the ordinal or position of the <see cref="T:System.Data.DataColumn" /> to the specified ordinal or position.</summary>
	/// <param name="ordinal">The specified ordinal.</param>
	public void SetOrdinal(int ordinal)
	{
		if (_ordinal == -1)
		{
			throw ExceptionBuilder.ColumnNotInAnyTable();
		}
		if (_ordinal != ordinal)
		{
			_table.Columns.MoveTo(this, ordinal);
		}
	}

	internal void SetOrdinalInternal(int ordinal)
	{
		if (_ordinal == ordinal)
		{
			return;
		}
		if (Unique && _ordinal != -1 && ordinal == -1)
		{
			UniqueConstraint uniqueConstraint = _table.Constraints.FindKeyConstraint(this);
			if (uniqueConstraint != null)
			{
				_table.Constraints.Remove(uniqueConstraint);
			}
		}
		if (_sortIndex != null && -1 == ordinal)
		{
			_sortIndex.RemoveRef();
			_sortIndex.RemoveRef();
			_sortIndex = null;
		}
		int ordinal2 = _ordinal;
		_ordinal = ordinal;
		if (ordinal2 == -1 && _ordinal != -1 && Unique)
		{
			UniqueConstraint constraint = new UniqueConstraint(this);
			_table.Constraints.Add(constraint);
		}
	}

	internal void SetTable(DataTable table)
	{
		if (_table == table)
		{
			return;
		}
		if (Computed && (table == null || (!table.fInitInProgress && (table.DataSet == null || (!table.DataSet._fIsSchemaLoading && !table.DataSet._fInitInProgress)))))
		{
			DataExpression.Bind(table);
		}
		if (Unique && _table != null)
		{
			UniqueConstraint uniqueConstraint = table.Constraints.FindKeyConstraint(this);
			if (uniqueConstraint != null)
			{
				table.Constraints.CanRemove(uniqueConstraint, fThrowException: true);
			}
		}
		_table = table;
		_storage = null;
	}

	private DataRow GetDataRow(int index)
	{
		return _table._recordManager[index];
	}

	internal void InitializeRecord(int record)
	{
		_storage.Set(record, DefaultValue);
	}

	internal void SetValue(int record, object value)
	{
		try
		{
			_storage.Set(record, value);
		}
		catch (Exception ex)
		{
			ExceptionBuilder.TraceExceptionForCapture(ex);
			throw ExceptionBuilder.SetFailed(value, this, DataType, ex);
		}
		DataRow dataRow = GetDataRow(record);
		if (dataRow != null)
		{
			dataRow.LastChangedColumn = this;
		}
	}

	internal void FreeRecord(int record)
	{
		_storage.Set(record, _storage._nullValue);
	}

	internal void InternalUnique(bool value)
	{
		_unique = value;
	}

	internal void CheckColumnConstraint(DataRow row, DataRowAction action)
	{
		if (_table.UpdatingCurrent(row, action))
		{
			CheckNullable(row);
			CheckMaxLength(row);
		}
	}

	internal bool CheckMaxLength()
	{
		if (0 <= _maxLength && Table != null && 0 < Table.Rows.Count)
		{
			foreach (DataRow row in Table.Rows)
			{
				if (row.HasVersion(DataRowVersion.Current) && _maxLength < GetStringLength(row.GetCurrentRecordNo()))
				{
					return false;
				}
			}
		}
		return true;
	}

	internal void CheckMaxLength(DataRow dr)
	{
		if (0 <= _maxLength && _maxLength < GetStringLength(dr.GetDefaultRecord()))
		{
			throw ExceptionBuilder.LongerThanMaxLength(this);
		}
	}

	/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	protected internal void CheckNotAllowNull()
	{
		if (_storage == null)
		{
			return;
		}
		if (_sortIndex != null)
		{
			if (!_sortIndex.IsKeyInIndex(_storage._nullValue))
			{
				return;
			}
			throw ExceptionBuilder.NullKeyValues(ColumnName);
		}
		foreach (DataRow row in _table.Rows)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				continue;
			}
			if (!_implementsINullable)
			{
				if (row[this] == DBNull.Value)
				{
					throw ExceptionBuilder.NullKeyValues(ColumnName);
				}
			}
			else if (DataStorage.IsObjectNull(row[this]))
			{
				throw ExceptionBuilder.NullKeyValues(ColumnName);
			}
		}
	}

	internal void CheckNullable(DataRow row)
	{
		if (!AllowDBNull && _storage.IsNull(row.GetDefaultRecord()))
		{
			throw ExceptionBuilder.NullValues(ColumnName);
		}
	}

	/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	protected void CheckUnique()
	{
		if (!SortIndex.CheckUnique())
		{
			throw ExceptionBuilder.NonUniqueValues(ColumnName);
		}
	}

	internal int Compare(int record1, int record2)
	{
		return _storage.Compare(record1, record2);
	}

	internal bool CompareValueTo(int record1, object value, bool checkType)
	{
		if (CompareValueTo(record1, value) == 0)
		{
			Type type = value.GetType();
			Type type2 = _storage.Get(record1).GetType();
			if (type == typeof(string) && type2 == typeof(string))
			{
				if (string.CompareOrdinal((string)_storage.Get(record1), (string)value) != 0)
				{
					return false;
				}
				return true;
			}
			if (type == type2)
			{
				return true;
			}
		}
		return false;
	}

	internal int CompareValueTo(int record1, object value)
	{
		return _storage.CompareValueTo(record1, value);
	}

	internal object ConvertValue(object value)
	{
		return _storage.ConvertValue(value);
	}

	internal void Copy(int srcRecordNo, int dstRecordNo)
	{
		_storage.Copy(srcRecordNo, dstRecordNo);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal DataColumn Clone()
	{
		DataColumn dataColumn = (DataColumn)Activator.CreateInstance(GetType());
		dataColumn.SimpleType = SimpleType;
		dataColumn._allowNull = _allowNull;
		if (_autoInc != null)
		{
			dataColumn._autoInc = _autoInc.Clone();
		}
		dataColumn._caption = _caption;
		dataColumn.ColumnName = ColumnName;
		dataColumn._columnUri = _columnUri;
		dataColumn._columnPrefix = _columnPrefix;
		dataColumn.DataType = DataType;
		dataColumn._defaultValue = _defaultValue;
		dataColumn._defaultValueIsNull = ((_defaultValue == DBNull.Value || (dataColumn.ImplementsINullable && DataStorage.IsObjectSqlNull(_defaultValue))) ? true : false);
		dataColumn._columnMapping = _columnMapping;
		dataColumn._readOnly = _readOnly;
		dataColumn.MaxLength = MaxLength;
		dataColumn.XmlDataType = XmlDataType;
		dataColumn._dateTimeMode = _dateTimeMode;
		if (_extendedProperties != null)
		{
			foreach (object key in _extendedProperties.Keys)
			{
				dataColumn.ExtendedProperties[key] = _extendedProperties[key];
			}
		}
		return dataColumn;
	}

	internal DataRelation FindParentRelation()
	{
		DataRelation[] array = new DataRelation[Table.ParentRelations.Count];
		Table.ParentRelations.CopyTo(array, 0);
		foreach (DataRelation dataRelation in array)
		{
			DataKey childKey = dataRelation.ChildKey;
			if (childKey.ColumnsReference.Length == 1 && childKey.ColumnsReference[0] == this)
			{
				return dataRelation;
			}
		}
		return null;
	}

	internal object GetAggregateValue(int[] records, AggregateType kind)
	{
		if (_storage == null)
		{
			if (kind != AggregateType.Count)
			{
				return DBNull.Value;
			}
			return 0;
		}
		return _storage.Aggregate(records, kind);
	}

	private int GetStringLength(int record)
	{
		return _storage.GetStringLength(record);
	}

	internal void Init(int record)
	{
		if (AutoIncrement)
		{
			object current = _autoInc.Current;
			_autoInc.MoveAfter();
			_storage.Set(record, current);
		}
		else
		{
			this[record] = _defaultValue;
		}
	}

	internal static bool IsAutoIncrementType(Type dataType)
	{
		if (!(dataType == typeof(int)) && !(dataType == typeof(long)) && !(dataType == typeof(short)) && !(dataType == typeof(decimal)) && !(dataType == typeof(BigInteger)) && !(dataType == typeof(SqlInt32)) && !(dataType == typeof(SqlInt64)) && !(dataType == typeof(SqlInt16)))
		{
			return dataType == typeof(SqlDecimal);
		}
		return true;
	}

	private bool IsColumnMappingValid(StorageType typeCode, MappingType mapping)
	{
		if (mapping != MappingType.Element)
		{
			return !DataStorage.IsTypeCustomType(typeCode);
		}
		return true;
	}

	internal bool IsValueCustomTypeInstance(object value)
	{
		if (DataStorage.IsTypeCustomType(value.GetType()))
		{
			return !(value is Type);
		}
		return false;
	}

	internal bool IsNull(int record)
	{
		return _storage.IsNull(record);
	}

	internal bool IsInRelation()
	{
		DataRelationCollection parentRelations = _table.ParentRelations;
		for (int i = 0; i < parentRelations.Count; i++)
		{
			if (parentRelations[i].ChildKey.ContainsColumn(this))
			{
				return true;
			}
		}
		parentRelations = _table.ChildRelations;
		for (int j = 0; j < parentRelations.Count; j++)
		{
			if (parentRelations[j].ParentKey.ContainsColumn(this))
			{
				return true;
			}
		}
		return false;
	}

	internal bool IsMaxLengthViolated()
	{
		if (MaxLength < 0)
		{
			return true;
		}
		bool result = false;
		string text = null;
		foreach (DataRow row in Table.Rows)
		{
			if (!row.HasVersion(DataRowVersion.Current))
			{
				continue;
			}
			object obj = row[this];
			if (!_isSqlType)
			{
				if (obj != null && obj != DBNull.Value && ((string)obj).Length > MaxLength)
				{
					if (text == null)
					{
						text = ExceptionBuilder.MaxLengthViolationText(ColumnName);
					}
					row.RowError = text;
					row.SetColumnError(this, text);
					result = true;
				}
			}
			else if (!DataStorage.IsObjectNull(obj) && ((SqlString)obj).Value.Length > MaxLength)
			{
				if (text == null)
				{
					text = ExceptionBuilder.MaxLengthViolationText(ColumnName);
				}
				row.RowError = text;
				row.SetColumnError(this, text);
				result = true;
			}
		}
		return result;
	}

	internal bool IsNotAllowDBNullViolated()
	{
		Index sortIndex = SortIndex;
		DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(DBNull.Value));
		for (int i = 0; i < rows.Length; i++)
		{
			string text = ExceptionBuilder.NotAllowDBNullViolationText(ColumnName);
			rows[i].RowError = text;
			rows[i].SetColumnError(this, text);
		}
		return rows.Length != 0;
	}

	internal void FinishInitInProgress()
	{
		if (Computed)
		{
			BindExpression();
		}
	}

	/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <param name="pcevent">Parameter reference.</param>
	protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
	{
		this.PropertyChanging?.Invoke(this, pcevent);
	}

	/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	/// <param name="name">Parameter reference.</param>
	protected internal void RaisePropertyChanging(string name)
	{
		OnPropertyChanging(new PropertyChangedEventArgs(name));
	}

	private void InsureStorage()
	{
		if (_storage == null)
		{
			_storage = DataStorage.CreateStorage(this, _dataType, _storageType);
		}
	}

	internal void SetCapacity(int capacity)
	{
		InsureStorage();
		_storage.SetCapacity(capacity);
	}

	private bool ShouldSerializeDefaultValue()
	{
		return !DefaultValueIsNull;
	}

	internal void OnSetDataSet()
	{
	}

	/// <summary>Gets the <see cref="P:System.Data.DataColumn.Expression" /> of the column, if one exists.</summary>
	/// <returns>The <see cref="P:System.Data.DataColumn.Expression" /> value, if the property is set; otherwise, the <see cref="P:System.Data.DataColumn.ColumnName" /> property.</returns>
	public override string ToString()
	{
		if (_expression != null)
		{
			return ColumnName + " + " + Expression;
		}
		return ColumnName;
	}

	internal object ConvertXmlToObject(string s)
	{
		InsureStorage();
		return _storage.ConvertXmlToObject(s);
	}

	internal object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
	{
		InsureStorage();
		return _storage.ConvertXmlToObject(xmlReader, xmlAttrib);
	}

	internal string ConvertObjectToXml(object value)
	{
		InsureStorage();
		return _storage.ConvertObjectToXml(value);
	}

	internal void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
	{
		InsureStorage();
		_storage.ConvertObjectToXml(value, xmlWriter, xmlAttrib);
	}

	internal object GetEmptyColumnStore(int recordCount)
	{
		InsureStorage();
		return _storage.GetEmptyStorageInternal(recordCount);
	}

	internal void CopyValueIntoStore(int record, object store, BitArray nullbits, int storeIndex)
	{
		_storage.CopyValueInternal(record, store, nullbits, storeIndex);
	}

	internal void SetStorage(object store, BitArray nullbits)
	{
		InsureStorage();
		_storage.SetStorageInternal(store, nullbits);
	}

	internal void AddDependentColumn(DataColumn expressionColumn)
	{
		if (_dependentColumns == null)
		{
			_dependentColumns = new List<DataColumn>();
		}
		_dependentColumns.Add(expressionColumn);
		_table.AddDependentColumn(expressionColumn);
	}

	internal void RemoveDependentColumn(DataColumn expressionColumn)
	{
		if (_dependentColumns != null && _dependentColumns.Contains(expressionColumn))
		{
			_dependentColumns.Remove(expressionColumn);
		}
		_table.RemoveDependentColumn(expressionColumn);
	}

	internal void HandleDependentColumnList(DataExpression oldExpression, DataExpression newExpression)
	{
		DataColumn[] dependency;
		if (oldExpression != null)
		{
			dependency = oldExpression.GetDependency();
			foreach (DataColumn obj in dependency)
			{
				obj.RemoveDependentColumn(this);
				if (obj._table != _table)
				{
					_table.RemoveDependentColumn(this);
				}
			}
			_table.RemoveDependentColumn(this);
		}
		if (newExpression == null)
		{
			return;
		}
		dependency = newExpression.GetDependency();
		foreach (DataColumn obj2 in dependency)
		{
			obj2.AddDependentColumn(this);
			if (obj2._table != _table)
			{
				_table.AddDependentColumn(this);
			}
		}
		_table.AddDependentColumn(this);
	}
}
