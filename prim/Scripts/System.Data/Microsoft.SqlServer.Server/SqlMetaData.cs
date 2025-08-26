using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;

namespace Microsoft.SqlServer.Server;

/// <summary>Specifies and retrieves metadata information from parameters and columns of <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> objects. This class cannot be inherited.</summary>
public sealed class SqlMetaData
{
	private string _strName;

	private long _lMaxLength;

	private SqlDbType _sqlDbType;

	private byte _bPrecision;

	private byte _bScale;

	private long _lLocale;

	private SqlCompareOptions _eCompareOptions;

	private string _xmlSchemaCollectionDatabase;

	private string _xmlSchemaCollectionOwningSchema;

	private string _xmlSchemaCollectionName;

	private bool _bPartialLength;

	private bool _useServerDefault;

	private bool _isUniqueKey;

	private SortOrder _columnSortOrder;

	private int _sortOrdinal;

	private const long x_lMax = -1L;

	private const long x_lServerMaxUnicode = 4000L;

	private const long x_lServerMaxANSI = 8000L;

	private const long x_lServerMaxBinary = 8000L;

	private const bool x_defaultUseServerDefault = false;

	private const bool x_defaultIsUniqueKey = false;

	private const SortOrder x_defaultColumnSortOrder = SortOrder.Unspecified;

	private const int x_defaultSortOrdinal = -1;

	private const SqlCompareOptions x_eDefaultStringCompareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

	private static byte[] s_maxLenFromPrecision = new byte[38]
	{
		5, 5, 5, 5, 5, 5, 5, 5, 5, 9,
		9, 9, 9, 9, 9, 9, 9, 9, 9, 13,
		13, 13, 13, 13, 13, 13, 13, 13, 17, 17,
		17, 17, 17, 17, 17, 17, 17, 17
	};

	private const byte MaxTimeScale = 7;

	private static byte[] s_maxVarTimeLenOffsetFromScale = new byte[8] { 2, 2, 2, 1, 1, 0, 0, 0 };

	private static readonly DateTime s_dtSmallMax = new DateTime(2079, 6, 6, 23, 59, 29, 998);

	private static readonly DateTime s_dtSmallMin = new DateTime(1899, 12, 31, 23, 59, 29, 999);

	private static readonly SqlMoney s_smSmallMax = new SqlMoney(214748.3647m);

	private static readonly SqlMoney s_smSmallMin = new SqlMoney(-214748.3648m);

	private static readonly TimeSpan s_timeMin = TimeSpan.Zero;

	private static readonly TimeSpan s_timeMax = new TimeSpan(863999999999L);

	private static readonly long[] s_unitTicksFromScale = new long[8] { 10000000L, 1000000L, 100000L, 10000L, 1000L, 100L, 10L, 1L };

	internal static SqlMetaData[] sxm_rgDefaults = new SqlMetaData[35]
	{
		new SqlMetaData("bigint", SqlDbType.BigInt, 8L, 19, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("binary", SqlDbType.Binary, 1L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("bit", SqlDbType.Bit, 1L, 1, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("char", SqlDbType.Char, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("datetime", SqlDbType.DateTime, 8L, 23, 3, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("decimal", SqlDbType.Decimal, 9L, 18, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("float", SqlDbType.Float, 8L, 53, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("image", SqlDbType.Image, -1L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("int", SqlDbType.Int, 4L, 10, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("money", SqlDbType.Money, 8L, 19, 4, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("nchar", SqlDbType.NChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("ntext", SqlDbType.NText, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("real", SqlDbType.Real, 4L, 24, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("uniqueidentifier", SqlDbType.UniqueIdentifier, 16L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("smalldatetime", SqlDbType.SmallDateTime, 4L, 16, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("smallint", SqlDbType.SmallInt, 2L, 5, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("smallmoney", SqlDbType.SmallMoney, 4L, 10, 4, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("text", SqlDbType.Text, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("timestamp", SqlDbType.Timestamp, 8L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("tinyint", SqlDbType.TinyInt, 1L, 3, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("varbinary", SqlDbType.VarBinary, 8000L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("varchar", SqlDbType.VarChar, 8000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("sql_variant", SqlDbType.Variant, 8016L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("xml", SqlDbType.Xml, -1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: true),
		new SqlMetaData("nvarchar", SqlDbType.NVarChar, 1L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("nvarchar", SqlDbType.NVarChar, 4000L, 0, 0, 0L, SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth, partialLength: false),
		new SqlMetaData("udt", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("table", SqlDbType.Structured, 0L, 0, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("date", SqlDbType.Date, 3L, 10, 0, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("time", SqlDbType.Time, 5L, 0, 7, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("datetime2", SqlDbType.DateTime2, 8L, 0, 7, 0L, SqlCompareOptions.None, partialLength: false),
		new SqlMetaData("datetimeoffset", SqlDbType.DateTimeOffset, 10L, 0, 7, 0L, SqlCompareOptions.None, partialLength: false)
	};

	/// <summary>Gets the comparison rules used for the column or parameter.</summary>
	/// <returns>The comparison rules used for the column or parameter as a <see cref="T:System.Data.SqlTypes.SqlCompareOptions" />.</returns>
	public SqlCompareOptions CompareOptions => _eCompareOptions;

	/// <summary>Indicates if the column in the table-valued parameter is unique.</summary>
	/// <returns>A <see langword="Boolean" /> value.</returns>
	public bool IsUniqueKey => _isUniqueKey;

	/// <summary>Gets the locale ID of the column or parameter.</summary>
	/// <returns>The locale ID of the column or parameter as a <see cref="T:System.Int64" />.</returns>
	public long LocaleId => _lLocale;

	/// <summary>Gets the length of <see langword="text" />, <see langword="ntext" />, and <see langword="image" /> data types.</summary>
	/// <returns>The length of <see langword="text" />, <see langword="ntext" />, and <see langword="image" /> data types.</returns>
	public static long Max => -1L;

	/// <summary>Gets the maximum length of the column or parameter.</summary>
	/// <returns>The maximum length of the column or parameter as a <see cref="T:System.Int64" />.</returns>
	public long MaxLength => _lMaxLength;

	/// <summary>Gets the name of the column or parameter.</summary>
	/// <returns>The name of the column or parameter as a <see cref="T:System.String" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="Name" /> specified in the constructor is longer than 128 characters.</exception>
	public string Name => _strName;

	/// <summary>Gets the precision of the column or parameter.</summary>
	/// <returns>The precision of the column or parameter as a <see cref="T:System.Byte" />.</returns>
	public byte Precision => _bPrecision;

	/// <summary>Gets the scale of the column or parameter.</summary>
	/// <returns>The scale of the column or parameter.</returns>
	public byte Scale => _bScale;

	/// <summary>Returns the sort order for a column.</summary>
	/// <returns>A <see cref="T:System.Data.SqlClient.SortOrder" /> object.</returns>
	public SortOrder SortOrder => _columnSortOrder;

	/// <summary>Returns the ordinal of the sort column.</summary>
	/// <returns>The ordinal of the sort column.</returns>
	public int SortOrdinal => _sortOrdinal;

	/// <summary>Gets the data type of the column or parameter.</summary>
	/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
	public SqlDbType SqlDbType => _sqlDbType;

	/// <summary>Gets the three-part name of the user-defined type (UDT) or the SQL Server type represented by the instance.</summary>
	/// <returns>The name of the UDT or SQL Server type as a <see cref="T:System.String" />.</returns>
	public string TypeName => sxm_rgDefaults[(int)SqlDbType].Name;

	/// <summary>Reports whether this column should use the default server value.</summary>
	/// <returns>A <see langword="Boolean" /> value.</returns>
	public bool UseServerDefault => _useServerDefault;

	/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
	/// <returns>The name of the database where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
	public string XmlSchemaCollectionDatabase => _xmlSchemaCollectionDatabase;

	/// <summary>Gets the name of the schema collection for this XML instance.</summary>
	/// <returns>The name of the schema collection for this XML instance as a <see cref="T:System.String" />.</returns>
	public string XmlSchemaCollectionName => _xmlSchemaCollectionName;

	/// <summary>Gets the owning relational schema where the schema collection for this XML instance is located.</summary>
	/// <returns>The owning relational schema where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
	public string XmlSchemaCollectionOwningSchema => _xmlSchemaCollectionOwningSchema;

	internal bool IsPartialLength => _bPartialLength;

	/// <summary>Gets the data type of the column or parameter.</summary>
	/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
	[System.MonoTODO]
	public DbType DbType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name and type.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
	public SqlMetaData(string name, SqlDbType dbType)
	{
		Construct(name, dbType, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and maximum length.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength)
	{
		Construct(name, dbType, maxLength, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, and scale.</summary>
	/// <param name="name">The name of the parameter or column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="precision">The precision of the parameter or column.</param>
	/// <param name="scale">The scale of the parameter or column.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="scale" /> was greater than <paramref name="precision" />.</exception>
	public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale)
	{
		Construct(name, dbType, precision, scale, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, scale, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="precision">The precision of the parameter or column.</param>
	/// <param name="scale">The scale of the parameter or column.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, and compare options.</summary>
	/// <param name="name">The name of the parameter or column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <param name="locale">The locale ID of the parameter or column.</param>
	/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions)
	{
		Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, compare options, and server default. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <param name="locale">The locale ID of the parameter or column.</param>
	/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		Construct(name, dbType, maxLength, locale, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, database name, owning schema, object name, and default server. This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
	/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
	/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		Construct(name, dbType, database, owningSchema, objectName, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT).</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <param name="precision">The precision of the parameter or column.</param>
	/// <param name="scale">The scale of the parameter or column.</param>
	/// <param name="locale">The locale ID of the parameter or column.</param>
	/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
	/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A <see langword="SqlDbType" /> that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long locale, SqlCompareOptions compareOptions, Type userDefinedType)
		: this(name, dbType, maxLength, precision, scale, locale, compareOptions, userDefinedType, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT). This form of the constructor supports table-valued parameters by allowing you to specify if the column is unique in the table-valued parameter, the sort order for the column, and the ordinal of the sort column.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="maxLength">The maximum length of the specified type.</param>
	/// <param name="precision">The precision of the parameter or column.</param>
	/// <param name="scale">The scale of the parameter or column.</param>
	/// <param name="localeId">The locale ID of the parameter or column.</param>
	/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
	/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
	/// <param name="useServerDefault">Specifes whether this column should use the default server value.</param>
	/// <param name="isUniqueKey">Specifies if the column in the table-valued parameter is unique.</param>
	/// <param name="columnSortOrder">Specifies the sort order for a column.</param>
	/// <param name="sortOrdinal">Specifies the ordinal of the sort column.</param>
	public SqlMetaData(string name, SqlDbType dbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		switch (dbType)
		{
		case SqlDbType.BigInt:
		case SqlDbType.Bit:
		case SqlDbType.DateTime:
		case SqlDbType.Float:
		case SqlDbType.Image:
		case SqlDbType.Int:
		case SqlDbType.Money:
		case SqlDbType.Real:
		case SqlDbType.UniqueIdentifier:
		case SqlDbType.SmallDateTime:
		case SqlDbType.SmallInt:
		case SqlDbType.SmallMoney:
		case SqlDbType.Timestamp:
		case SqlDbType.TinyInt:
		case SqlDbType.Xml:
		case SqlDbType.Date:
			Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.Binary:
		case SqlDbType.VarBinary:
			Construct(name, dbType, maxLength, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.Char:
		case SqlDbType.NChar:
		case SqlDbType.NVarChar:
		case SqlDbType.VarChar:
			Construct(name, dbType, maxLength, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.NText:
		case SqlDbType.Text:
			Construct(name, dbType, Max, localeId, compareOptions, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.Decimal:
		case SqlDbType.Time:
		case SqlDbType.DateTime2:
		case SqlDbType.DateTimeOffset:
			Construct(name, dbType, precision, scale, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.Variant:
			Construct(name, dbType, useServerDefault, isUniqueKey, columnSortOrder, sortOrdinal);
			break;
		case SqlDbType.Udt:
			throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
		default:
			SQL.InvalidSqlDbTypeForConstructor(dbType);
			break;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, database name, owning schema, and object name.</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
	/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
	/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />, or <paramref name="objectName" /> is <see langword="null" /> when <paramref name="database" /> and <paramref name="owningSchema" /> are non-<see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
	public SqlMetaData(string name, SqlDbType dbType, string database, string owningSchema, string objectName)
	{
		Construct(name, dbType, database, owningSchema, objectName, useServerDefault: false, isUniqueKey: false, SortOrder.Unspecified, -1);
	}

	internal SqlMetaData(string name, SqlDbType sqlDBType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName, bool partialLength)
	{
		AssertNameIsValid(name);
		_strName = name;
		_sqlDbType = sqlDBType;
		_lMaxLength = maxLength;
		_bPrecision = precision;
		_bScale = scale;
		_lLocale = localeId;
		_eCompareOptions = compareOptions;
		_xmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
		_xmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
		_xmlSchemaCollectionName = xmlSchemaCollectionName;
		_bPartialLength = partialLength;
		ThrowIfUdt(sqlDBType);
	}

	private SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, bool partialLength)
	{
		AssertNameIsValid(name);
		_strName = name;
		_sqlDbType = sqlDbType;
		_lMaxLength = maxLength;
		_bPrecision = precision;
		_bScale = scale;
		_lLocale = localeId;
		_eCompareOptions = compareOptions;
		_bPartialLength = partialLength;
		ThrowIfUdt(sqlDbType);
	}

	private void Construct(string name, SqlDbType dbType, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		AssertNameIsValid(name);
		ValidateSortOrder(columnSortOrder, sortOrdinal);
		if (dbType != 0 && SqlDbType.Bit != dbType && SqlDbType.DateTime != dbType && SqlDbType.Date != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType && SqlDbType.Decimal != dbType && SqlDbType.Float != dbType && SqlDbType.Image != dbType && SqlDbType.Int != dbType && SqlDbType.Money != dbType && SqlDbType.NText != dbType && SqlDbType.Real != dbType && SqlDbType.SmallDateTime != dbType && SqlDbType.SmallInt != dbType && SqlDbType.SmallMoney != dbType && SqlDbType.Text != dbType && SqlDbType.Time != dbType && SqlDbType.Timestamp != dbType && SqlDbType.TinyInt != dbType && SqlDbType.UniqueIdentifier != dbType && SqlDbType.Variant != dbType && SqlDbType.Xml != dbType)
		{
			throw SQL.InvalidSqlDbTypeForConstructor(dbType);
		}
		ThrowIfUdt(dbType);
		SetDefaultsForType(dbType);
		if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
		{
			_lLocale = CultureInfo.CurrentCulture.LCID;
		}
		_strName = name;
		_useServerDefault = useServerDefault;
		_isUniqueKey = isUniqueKey;
		_columnSortOrder = columnSortOrder;
		_sortOrdinal = sortOrdinal;
	}

	private void Construct(string name, SqlDbType dbType, long maxLength, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		AssertNameIsValid(name);
		ValidateSortOrder(columnSortOrder, sortOrdinal);
		long lLocale = 0L;
		if (SqlDbType.Char == dbType)
		{
			if (maxLength > 8000 || maxLength < 0)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
			lLocale = CultureInfo.CurrentCulture.LCID;
		}
		else if (SqlDbType.VarChar == dbType)
		{
			if ((maxLength > 8000 || maxLength < 0) && maxLength != Max)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
			lLocale = CultureInfo.CurrentCulture.LCID;
		}
		else if (SqlDbType.NChar == dbType)
		{
			if (maxLength > 4000 || maxLength < 0)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
			lLocale = CultureInfo.CurrentCulture.LCID;
		}
		else if (SqlDbType.NVarChar == dbType)
		{
			if ((maxLength > 4000 || maxLength < 0) && maxLength != Max)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
			lLocale = CultureInfo.CurrentCulture.LCID;
		}
		else if (SqlDbType.NText == dbType || SqlDbType.Text == dbType)
		{
			if (Max != maxLength)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
			lLocale = CultureInfo.CurrentCulture.LCID;
		}
		else if (SqlDbType.Binary == dbType)
		{
			if (maxLength > 8000 || maxLength < 0)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else if (SqlDbType.VarBinary == dbType)
		{
			if ((maxLength > 8000 || maxLength < 0) && maxLength != Max)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else
		{
			if (SqlDbType.Image != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (Max != maxLength)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		SetDefaultsForType(dbType);
		_strName = name;
		_lMaxLength = maxLength;
		_lLocale = lLocale;
		_useServerDefault = useServerDefault;
		_isUniqueKey = isUniqueKey;
		_columnSortOrder = columnSortOrder;
		_sortOrdinal = sortOrdinal;
	}

	private void Construct(string name, SqlDbType dbType, long maxLength, long locale, SqlCompareOptions compareOptions, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		AssertNameIsValid(name);
		ValidateSortOrder(columnSortOrder, sortOrdinal);
		if (SqlDbType.Char == dbType)
		{
			if (maxLength > 8000 || maxLength < 0)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else if (SqlDbType.VarChar == dbType)
		{
			if ((maxLength > 8000 || maxLength < 0) && maxLength != Max)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else if (SqlDbType.NChar == dbType)
		{
			if (maxLength > 4000 || maxLength < 0)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else if (SqlDbType.NVarChar == dbType)
		{
			if ((maxLength > 4000 || maxLength < 0) && maxLength != Max)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		else
		{
			if (SqlDbType.NText != dbType && SqlDbType.Text != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (Max != maxLength)
			{
				throw ADP.Argument(global::SR.GetString("Specified length '{0}' is out of range.", maxLength.ToString(CultureInfo.InvariantCulture)), "maxLength");
			}
		}
		if (SqlCompareOptions.BinarySort != compareOptions && (~(SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth) & compareOptions) != 0)
		{
			throw ADP.InvalidEnumerationValue(typeof(SqlCompareOptions), (int)compareOptions);
		}
		SetDefaultsForType(dbType);
		_strName = name;
		_lMaxLength = maxLength;
		_lLocale = locale;
		_eCompareOptions = compareOptions;
		_useServerDefault = useServerDefault;
		_isUniqueKey = isUniqueKey;
		_columnSortOrder = columnSortOrder;
		_sortOrdinal = sortOrdinal;
	}

	private void Construct(string name, SqlDbType dbType, byte precision, byte scale, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		AssertNameIsValid(name);
		ValidateSortOrder(columnSortOrder, sortOrdinal);
		if (SqlDbType.Decimal == dbType)
		{
			if (precision > SqlDecimal.MaxPrecision || scale > precision)
			{
				throw SQL.PrecisionValueOutOfRange(precision);
			}
			if (scale > SqlDecimal.MaxScale)
			{
				throw SQL.ScaleValueOutOfRange(scale);
			}
		}
		else
		{
			if (SqlDbType.Time != dbType && SqlDbType.DateTime2 != dbType && SqlDbType.DateTimeOffset != dbType)
			{
				throw SQL.InvalidSqlDbTypeForConstructor(dbType);
			}
			if (scale > 7)
			{
				throw SQL.TimeScaleValueOutOfRange(scale);
			}
		}
		SetDefaultsForType(dbType);
		_strName = name;
		_bPrecision = precision;
		_bScale = scale;
		if (SqlDbType.Decimal == dbType)
		{
			_lMaxLength = s_maxLenFromPrecision[precision - 1];
		}
		else
		{
			_lMaxLength -= s_maxVarTimeLenOffsetFromScale[scale];
		}
		_useServerDefault = useServerDefault;
		_isUniqueKey = isUniqueKey;
		_columnSortOrder = columnSortOrder;
		_sortOrdinal = sortOrdinal;
	}

	private void Construct(string name, SqlDbType dbType, string database, string owningSchema, string objectName, bool useServerDefault, bool isUniqueKey, SortOrder columnSortOrder, int sortOrdinal)
	{
		AssertNameIsValid(name);
		ValidateSortOrder(columnSortOrder, sortOrdinal);
		if (SqlDbType.Xml != dbType)
		{
			throw SQL.InvalidSqlDbTypeForConstructor(dbType);
		}
		if ((database != null || owningSchema != null) && objectName == null)
		{
			throw ADP.ArgumentNull("objectName");
		}
		SetDefaultsForType(SqlDbType.Xml);
		_strName = name;
		_xmlSchemaCollectionDatabase = database;
		_xmlSchemaCollectionOwningSchema = owningSchema;
		_xmlSchemaCollectionName = objectName;
		_useServerDefault = useServerDefault;
		_isUniqueKey = isUniqueKey;
		_columnSortOrder = columnSortOrder;
		_sortOrdinal = sortOrdinal;
	}

	private void AssertNameIsValid(string name)
	{
		if (name == null)
		{
			throw ADP.ArgumentNull("name");
		}
		if (128L < (long)name.Length)
		{
			throw SQL.NameTooLong("name");
		}
	}

	private void ValidateSortOrder(SortOrder columnSortOrder, int sortOrdinal)
	{
		if (SortOrder.Unspecified != columnSortOrder && columnSortOrder != 0 && SortOrder.Descending != columnSortOrder)
		{
			throw SQL.InvalidSortOrder(columnSortOrder);
		}
		if (SortOrder.Unspecified == columnSortOrder != (-1 == sortOrdinal))
		{
			throw SQL.MustSpecifyBothSortOrderAndOrdinal(columnSortOrder, sortOrdinal);
		}
	}

	/// <summary>Validates the specified <see cref="T:System.Int16" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Int16" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public short Adjust(short value)
	{
		if (SqlDbType.SmallInt != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Int32" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Int32" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public int Adjust(int value)
	{
		if (SqlDbType.Int != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Int64" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Int64" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public long Adjust(long value)
	{
		if (SqlDbType != 0)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Single" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Single" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public float Adjust(float value)
	{
		if (SqlDbType.Real != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Double" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Double" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public double Adjust(double value)
	{
		if (SqlDbType.Float != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.String" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.String" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public string Adjust(string value)
	{
		if (SqlDbType.Char == SqlDbType || SqlDbType.NChar == SqlDbType)
		{
			if (value != null && value.Length < MaxLength)
			{
				value = value.PadRight((int)MaxLength);
			}
		}
		else if (SqlDbType.VarChar != SqlDbType && SqlDbType.NVarChar != SqlDbType && SqlDbType.Text != SqlDbType && SqlDbType.NText != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value == null)
		{
			return null;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			value = value.Remove((int)MaxLength, (int)(value.Length - MaxLength));
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Decimal" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Decimal" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public decimal Adjust(decimal value)
	{
		if (SqlDbType.Decimal != SqlDbType && SqlDbType.Money != SqlDbType && SqlDbType.SmallMoney != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (SqlDbType.Decimal != SqlDbType)
		{
			VerifyMoneyRange(new SqlMoney(value));
			return value;
		}
		return InternalAdjustSqlDecimal(new SqlDecimal(value)).Value;
	}

	/// <summary>Validates the specified <see cref="T:System.DateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.DateTime" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public DateTime Adjust(DateTime value)
	{
		if (SqlDbType.DateTime == SqlDbType || SqlDbType.SmallDateTime == SqlDbType)
		{
			VerifyDateTimeRange(value);
		}
		else
		{
			if (SqlDbType.DateTime2 == SqlDbType)
			{
				return new DateTime(InternalAdjustTimeTicks(value.Ticks));
			}
			if (SqlDbType.Date == SqlDbType)
			{
				return value.Date;
			}
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Guid" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Guid" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public Guid Adjust(Guid value)
	{
		if (SqlDbType.UniqueIdentifier != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlBoolean Adjust(SqlBoolean value)
	{
		if (SqlDbType.Bit != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlByte Adjust(SqlByte value)
	{
		if (SqlDbType.TinyInt != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlInt16 Adjust(SqlInt16 value)
	{
		if (SqlDbType.SmallInt != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlInt32 Adjust(SqlInt32 value)
	{
		if (SqlDbType.Int != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlInt64 Adjust(SqlInt64 value)
	{
		if (SqlDbType != 0)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlSingle Adjust(SqlSingle value)
	{
		if (SqlDbType.Real != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlDouble Adjust(SqlDouble value)
	{
		if (SqlDbType.Float != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlMoney Adjust(SqlMoney value)
	{
		if (SqlDbType.Money != SqlDbType && SqlDbType.SmallMoney != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (!value.IsNull)
		{
			VerifyMoneyRange(value);
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlDateTime Adjust(SqlDateTime value)
	{
		if (SqlDbType.DateTime != SqlDbType && SqlDbType.SmallDateTime != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (!value.IsNull)
		{
			VerifyDateTimeRange(value.Value);
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlDecimal Adjust(SqlDecimal value)
	{
		if (SqlDbType.Decimal != SqlDbType)
		{
			ThrowInvalidType();
		}
		return InternalAdjustSqlDecimal(value);
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlString Adjust(SqlString value)
	{
		if (SqlDbType.Char == SqlDbType || SqlDbType.NChar == SqlDbType)
		{
			if (!value.IsNull && value.Value.Length < MaxLength)
			{
				return new SqlString(value.Value.PadRight((int)MaxLength));
			}
		}
		else if (SqlDbType.VarChar != SqlDbType && SqlDbType.NVarChar != SqlDbType && SqlDbType.Text != SqlDbType && SqlDbType.NText != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value.IsNull)
		{
			return value;
		}
		if (value.Value.Length > MaxLength && Max != MaxLength)
		{
			value = new SqlString(value.Value.Remove((int)MaxLength, (int)(value.Value.Length - MaxLength)));
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlBinary Adjust(SqlBinary value)
	{
		if (SqlDbType.Binary == SqlDbType || SqlDbType.Timestamp == SqlDbType)
		{
			if (!value.IsNull && value.Length < MaxLength)
			{
				byte[] value2 = value.Value;
				byte[] array = new byte[MaxLength];
				Buffer.BlockCopy(value2, 0, array, 0, value2.Length);
				Array.Clear(array, value2.Length, array.Length - value2.Length);
				return new SqlBinary(array);
			}
		}
		else if (SqlDbType.VarBinary != SqlDbType && SqlDbType.Image != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value.IsNull)
		{
			return value;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			byte[] value3 = value.Value;
			byte[] array2 = new byte[MaxLength];
			Buffer.BlockCopy(value3, 0, array2, 0, (int)MaxLength);
			value = new SqlBinary(array2);
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlGuid Adjust(SqlGuid value)
	{
		if (SqlDbType.UniqueIdentifier != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlChars Adjust(SqlChars value)
	{
		if (SqlDbType.Char == SqlDbType || SqlDbType.NChar == SqlDbType)
		{
			if (value != null && !value.IsNull)
			{
				long length = value.Length;
				if (length < MaxLength)
				{
					if (value.MaxLength < MaxLength)
					{
						char[] array = new char[(int)MaxLength];
						Array.Copy(value.Buffer, 0, array, 0, (int)length);
						value = new SqlChars(array);
					}
					char[] buffer = value.Buffer;
					for (long num = length; num < MaxLength; num++)
					{
						buffer[num] = ' ';
					}
					value.SetLength(MaxLength);
					return value;
				}
			}
		}
		else if (SqlDbType.VarChar != SqlDbType && SqlDbType.NVarChar != SqlDbType && SqlDbType.Text != SqlDbType && SqlDbType.NText != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value == null || value.IsNull)
		{
			return value;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			value.SetLength(MaxLength);
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlBytes Adjust(SqlBytes value)
	{
		if (SqlDbType.Binary == SqlDbType || SqlDbType.Timestamp == SqlDbType)
		{
			if (value != null && !value.IsNull)
			{
				int num = (int)value.Length;
				if (num < MaxLength)
				{
					if (value.MaxLength < MaxLength)
					{
						byte[] array = new byte[MaxLength];
						Buffer.BlockCopy(value.Buffer, 0, array, 0, num);
						value = new SqlBytes(array);
					}
					byte[] buffer = value.Buffer;
					Array.Clear(buffer, num, buffer.Length - num);
					value.SetLength(MaxLength);
					return value;
				}
			}
		}
		else if (SqlDbType.VarBinary != SqlDbType && SqlDbType.Image != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value == null || value.IsNull)
		{
			return value;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			value.SetLength(MaxLength);
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlXml" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlXml" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public SqlXml Adjust(SqlXml value)
	{
		if (SqlDbType.Xml != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.TimeSpan" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as an array of <see cref="T:System.TimeSpan" /> values.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public TimeSpan Adjust(TimeSpan value)
	{
		if (SqlDbType.Time != SqlDbType)
		{
			ThrowInvalidType();
		}
		VerifyTimeRange(value);
		return new TimeSpan(InternalAdjustTimeTicks(value.Ticks));
	}

	/// <summary>Validates the specified <see cref="T:System.DateTimeOffset" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as an array of <see cref="T:System.DateTimeOffset" /> values.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public DateTimeOffset Adjust(DateTimeOffset value)
	{
		if (SqlDbType.DateTimeOffset != SqlDbType)
		{
			ThrowInvalidType();
		}
		return new DateTimeOffset(InternalAdjustTimeTicks(value.Ticks), value.Offset);
	}

	/// <summary>Validates the specified <see cref="T:System.Object" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Object" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public object Adjust(object value)
	{
		if (value == null)
		{
			return null;
		}
		if (value is bool)
		{
			value = Adjust((bool)value);
		}
		else if (value is byte)
		{
			value = Adjust((byte)value);
		}
		else if (value is char)
		{
			value = Adjust((char)value);
		}
		else if (value is DateTime)
		{
			value = Adjust((DateTime)value);
		}
		else if (!(value is DBNull))
		{
			if (value is decimal)
			{
				value = Adjust((decimal)value);
			}
			else if (value is double)
			{
				value = Adjust((double)value);
			}
			else if (value is short)
			{
				value = Adjust((short)value);
			}
			else if (value is int)
			{
				value = Adjust((int)value);
			}
			else if (value is long)
			{
				value = Adjust((long)value);
			}
			else
			{
				if (value is sbyte)
				{
					throw ADP.InvalidDataType("SByte");
				}
				if (value is float)
				{
					value = Adjust((float)value);
				}
				else if (value is string)
				{
					value = Adjust((string)value);
				}
				else
				{
					if (value is ushort)
					{
						throw ADP.InvalidDataType("UInt16");
					}
					if (value is uint)
					{
						throw ADP.InvalidDataType("UInt32");
					}
					if (value is ulong)
					{
						throw ADP.InvalidDataType("UInt64");
					}
					if (value is byte[])
					{
						value = Adjust((byte[])value);
					}
					else if (value is char[])
					{
						value = Adjust((char[])value);
					}
					else if (value is Guid)
					{
						value = Adjust((Guid)value);
					}
					else if (value is SqlBinary)
					{
						value = Adjust((SqlBinary)value);
					}
					else if (value is SqlBoolean)
					{
						value = Adjust((SqlBoolean)value);
					}
					else if (value is SqlByte)
					{
						value = Adjust((SqlByte)value);
					}
					else if (value is SqlDateTime)
					{
						value = Adjust((SqlDateTime)value);
					}
					else if (value is SqlDouble)
					{
						value = Adjust((SqlDouble)value);
					}
					else if (value is SqlGuid)
					{
						value = Adjust((SqlGuid)value);
					}
					else if (value is SqlInt16)
					{
						value = Adjust((SqlInt16)value);
					}
					else if (value is SqlInt32)
					{
						value = Adjust((SqlInt32)value);
					}
					else if (value is SqlInt64)
					{
						value = Adjust((SqlInt64)value);
					}
					else if (value is SqlMoney)
					{
						value = Adjust((SqlMoney)value);
					}
					else if (value is SqlDecimal)
					{
						value = Adjust((SqlDecimal)value);
					}
					else if (value is SqlSingle)
					{
						value = Adjust((SqlSingle)value);
					}
					else if (value is SqlString)
					{
						value = Adjust((SqlString)value);
					}
					else if (value is SqlChars)
					{
						value = Adjust((SqlChars)value);
					}
					else if (value is SqlBytes)
					{
						value = Adjust((SqlBytes)value);
					}
					else if (value is SqlXml)
					{
						value = Adjust((SqlXml)value);
					}
					else if (value is TimeSpan)
					{
						value = Adjust((TimeSpan)value);
					}
					else
					{
						if (!(value is DateTimeOffset))
						{
							throw ADP.UnknownDataType(value.GetType());
						}
						value = Adjust((DateTimeOffset)value);
					}
				}
			}
		}
		return value;
	}

	/// <summary>Infers the metadata from the specified object and returns it as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</summary>
	/// <param name="value">The object used from which the metadata is inferred.</param>
	/// <param name="name">The name assigned to the returned <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The inferred metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is <see langword="null" />.</exception>
	public static SqlMetaData InferFromValue(object value, string name)
	{
		if (value == null)
		{
			throw ADP.ArgumentNull("value");
		}
		if (value is bool)
		{
			return new SqlMetaData(name, SqlDbType.Bit);
		}
		if (value is byte)
		{
			return new SqlMetaData(name, SqlDbType.TinyInt);
		}
		if (value is char)
		{
			return new SqlMetaData(name, SqlDbType.NVarChar, 1L);
		}
		if (value is DateTime)
		{
			return new SqlMetaData(name, SqlDbType.DateTime);
		}
		if (value is DBNull)
		{
			throw ADP.InvalidDataType("DBNull");
		}
		if (value is decimal)
		{
			SqlDecimal sqlDecimal = new SqlDecimal((decimal)value);
			return new SqlMetaData(name, SqlDbType.Decimal, sqlDecimal.Precision, sqlDecimal.Scale);
		}
		if (value is double)
		{
			return new SqlMetaData(name, SqlDbType.Float);
		}
		if (value is short)
		{
			return new SqlMetaData(name, SqlDbType.SmallInt);
		}
		if (value is int)
		{
			return new SqlMetaData(name, SqlDbType.Int);
		}
		if (value is long)
		{
			return new SqlMetaData(name, SqlDbType.BigInt);
		}
		if (value is sbyte)
		{
			throw ADP.InvalidDataType("SByte");
		}
		if (value is float)
		{
			return new SqlMetaData(name, SqlDbType.Real);
		}
		if (value is string)
		{
			long num = ((string)value).Length;
			if (num < 1)
			{
				num = 1L;
			}
			if (4000 < num)
			{
				num = Max;
			}
			return new SqlMetaData(name, SqlDbType.NVarChar, num);
		}
		if (value is ushort)
		{
			throw ADP.InvalidDataType("UInt16");
		}
		if (value is uint)
		{
			throw ADP.InvalidDataType("UInt32");
		}
		if (value is ulong)
		{
			throw ADP.InvalidDataType("UInt64");
		}
		if (value is byte[])
		{
			long num2 = ((byte[])value).Length;
			if (num2 < 1)
			{
				num2 = 1L;
			}
			if (8000 < num2)
			{
				num2 = Max;
			}
			return new SqlMetaData(name, SqlDbType.VarBinary, num2);
		}
		if (value is char[])
		{
			long num3 = ((char[])value).Length;
			if (num3 < 1)
			{
				num3 = 1L;
			}
			if (4000 < num3)
			{
				num3 = Max;
			}
			return new SqlMetaData(name, SqlDbType.NVarChar, num3);
		}
		if (value is Guid)
		{
			return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
		}
		if (value != null)
		{
			return new SqlMetaData(name, SqlDbType.Variant);
		}
		if (value is SqlBinary sqlBinary)
		{
			long num4;
			if (!sqlBinary.IsNull)
			{
				num4 = sqlBinary.Length;
				if (num4 < 1)
				{
					num4 = 1L;
				}
				if (8000 < num4)
				{
					num4 = Max;
				}
			}
			else
			{
				num4 = sxm_rgDefaults[21].MaxLength;
			}
			return new SqlMetaData(name, SqlDbType.VarBinary, num4);
		}
		if (value is SqlBoolean)
		{
			return new SqlMetaData(name, SqlDbType.Bit);
		}
		if (value is SqlByte)
		{
			return new SqlMetaData(name, SqlDbType.TinyInt);
		}
		if (value is SqlDateTime)
		{
			return new SqlMetaData(name, SqlDbType.DateTime);
		}
		if (value is SqlDouble)
		{
			return new SqlMetaData(name, SqlDbType.Float);
		}
		if (value is SqlGuid)
		{
			return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
		}
		if (value is SqlInt16)
		{
			return new SqlMetaData(name, SqlDbType.SmallInt);
		}
		if (value is SqlInt32)
		{
			return new SqlMetaData(name, SqlDbType.Int);
		}
		if (value is SqlInt64)
		{
			return new SqlMetaData(name, SqlDbType.BigInt);
		}
		if (value is SqlMoney)
		{
			return new SqlMetaData(name, SqlDbType.Money);
		}
		if (value is SqlDecimal sqlDecimal2)
		{
			byte precision;
			byte scale;
			if (!sqlDecimal2.IsNull)
			{
				precision = sqlDecimal2.Precision;
				scale = sqlDecimal2.Scale;
			}
			else
			{
				precision = sxm_rgDefaults[5].Precision;
				scale = sxm_rgDefaults[5].Scale;
			}
			return new SqlMetaData(name, SqlDbType.Decimal, precision, scale);
		}
		if (value is SqlSingle)
		{
			return new SqlMetaData(name, SqlDbType.Real);
		}
		if (value is SqlString sqlString)
		{
			if (!sqlString.IsNull)
			{
				long num5 = sqlString.Value.Length;
				if (num5 < 1)
				{
					num5 = 1L;
				}
				if (num5 > 4000)
				{
					num5 = Max;
				}
				return new SqlMetaData(name, SqlDbType.NVarChar, num5, sqlString.LCID, sqlString.SqlCompareOptions);
			}
			return new SqlMetaData(name, SqlDbType.NVarChar, sxm_rgDefaults[12].MaxLength);
		}
		if (value is SqlChars)
		{
			SqlChars sqlChars = (SqlChars)value;
			long num6;
			if (!sqlChars.IsNull)
			{
				num6 = sqlChars.Length;
				if (num6 < 1)
				{
					num6 = 1L;
				}
				if (num6 > 4000)
				{
					num6 = Max;
				}
			}
			else
			{
				num6 = sxm_rgDefaults[12].MaxLength;
			}
			return new SqlMetaData(name, SqlDbType.NVarChar, num6);
		}
		if (value is SqlBytes)
		{
			SqlBytes sqlBytes = (SqlBytes)value;
			long num7;
			if (!sqlBytes.IsNull)
			{
				num7 = sqlBytes.Length;
				if (num7 < 1)
				{
					num7 = 1L;
				}
				else if (8000 < num7)
				{
					num7 = Max;
				}
			}
			else
			{
				num7 = sxm_rgDefaults[21].MaxLength;
			}
			return new SqlMetaData(name, SqlDbType.VarBinary, num7);
		}
		if (value is SqlXml)
		{
			return new SqlMetaData(name, SqlDbType.Xml);
		}
		if (value is TimeSpan)
		{
			return new SqlMetaData(name, SqlDbType.Time, 0, InferScaleFromTimeTicks(((TimeSpan)value).Ticks));
		}
		if (value is DateTimeOffset)
		{
			return new SqlMetaData(name, SqlDbType.DateTimeOffset, 0, InferScaleFromTimeTicks(((DateTimeOffset)value).Ticks));
		}
		throw ADP.UnknownDataType(value.GetType());
	}

	/// <summary>Validates the specified <see cref="T:System.Boolean" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Boolean" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public bool Adjust(bool value)
	{
		if (SqlDbType.Bit != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Byte" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Byte" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public byte Adjust(byte value)
	{
		if (SqlDbType.TinyInt != SqlDbType)
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified array of <see cref="T:System.Byte" /> values against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as an array of <see cref="T:System.Byte" /> values.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public byte[] Adjust(byte[] value)
	{
		if (SqlDbType.Binary == SqlDbType || SqlDbType.Timestamp == SqlDbType)
		{
			if (value != null && value.Length < MaxLength)
			{
				byte[] array = new byte[MaxLength];
				Buffer.BlockCopy(value, 0, array, 0, value.Length);
				Array.Clear(array, value.Length, array.Length - value.Length);
				return array;
			}
		}
		else if (SqlDbType.VarBinary != SqlDbType && SqlDbType.Image != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value == null)
		{
			return null;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			byte[] array2 = new byte[MaxLength];
			Buffer.BlockCopy(value, 0, array2, 0, (int)MaxLength);
			value = array2;
		}
		return value;
	}

	/// <summary>Validates the specified <see cref="T:System.Char" /> value against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as a <see cref="T:System.Char" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public char Adjust(char value)
	{
		if (SqlDbType.Char == SqlDbType || SqlDbType.NChar == SqlDbType)
		{
			if (1 != MaxLength)
			{
				ThrowInvalidType();
			}
		}
		else if (1 > MaxLength || (SqlDbType.VarChar != SqlDbType && SqlDbType.NVarChar != SqlDbType && SqlDbType.Text != SqlDbType && SqlDbType.NText != SqlDbType))
		{
			ThrowInvalidType();
		}
		return value;
	}

	/// <summary>Validates the specified array of <see cref="T:System.Char" /> values against the metadata, and adjusts the value if necessary.</summary>
	/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
	/// <returns>The adjusted value as an array <see cref="T:System.Char" /> values.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted.</exception>
	public char[] Adjust(char[] value)
	{
		if (SqlDbType.Char == SqlDbType || SqlDbType.NChar == SqlDbType)
		{
			if (value != null)
			{
				long num = value.Length;
				if (num < MaxLength)
				{
					char[] array = new char[(int)MaxLength];
					Array.Copy(value, 0, array, 0, (int)num);
					for (long num2 = num; num2 < array.Length; num2++)
					{
						array[num2] = ' ';
					}
					return array;
				}
			}
		}
		else if (SqlDbType.VarChar != SqlDbType && SqlDbType.NVarChar != SqlDbType && SqlDbType.Text != SqlDbType && SqlDbType.NText != SqlDbType)
		{
			ThrowInvalidType();
		}
		if (value == null)
		{
			return null;
		}
		if (value.Length > MaxLength && Max != MaxLength)
		{
			char[] array2 = new char[MaxLength];
			Array.Copy(value, 0, array2, 0, (int)MaxLength);
			value = array2;
		}
		return value;
	}

	internal static SqlMetaData GetPartialLengthMetaData(SqlMetaData md)
	{
		if (md.IsPartialLength)
		{
			return md;
		}
		if (md.SqlDbType == SqlDbType.Xml)
		{
			ThrowInvalidType();
		}
		if (md.SqlDbType == SqlDbType.NVarChar || md.SqlDbType == SqlDbType.VarChar || md.SqlDbType == SqlDbType.VarBinary)
		{
			return new SqlMetaData(md.Name, md.SqlDbType, Max, 0, 0, md.LocaleId, md.CompareOptions, null, null, null, partialLength: true);
		}
		return md;
	}

	private static void ThrowInvalidType()
	{
		throw ADP.InvalidMetaDataValue();
	}

	private void VerifyDateTimeRange(DateTime value)
	{
		if (SqlDbType.SmallDateTime == SqlDbType && (s_dtSmallMax < value || s_dtSmallMin > value))
		{
			ThrowInvalidType();
		}
	}

	private void VerifyMoneyRange(SqlMoney value)
	{
		if (SqlDbType.SmallMoney == SqlDbType && ((s_smSmallMax < value).Value || (s_smSmallMin > value).Value))
		{
			ThrowInvalidType();
		}
	}

	private SqlDecimal InternalAdjustSqlDecimal(SqlDecimal value)
	{
		if (!value.IsNull && (value.Precision != Precision || value.Scale != Scale))
		{
			if (value.Scale != Scale)
			{
				value = SqlDecimal.AdjustScale(value, Scale - value.Scale, fRound: false);
			}
			return SqlDecimal.ConvertToPrecScale(value, Precision, Scale);
		}
		return value;
	}

	private void VerifyTimeRange(TimeSpan value)
	{
		if (SqlDbType.Time == SqlDbType && (s_timeMin > value || value > s_timeMax))
		{
			ThrowInvalidType();
		}
	}

	private long InternalAdjustTimeTicks(long ticks)
	{
		return ticks / s_unitTicksFromScale[Scale] * s_unitTicksFromScale[Scale];
	}

	private static byte InferScaleFromTimeTicks(long ticks)
	{
		for (byte b = 0; b < 7; b++)
		{
			if (ticks / s_unitTicksFromScale[b] * s_unitTicksFromScale[b] == ticks)
			{
				return b;
			}
		}
		return 7;
	}

	private void SetDefaultsForType(SqlDbType dbType)
	{
		if (SqlDbType.BigInt <= dbType && SqlDbType.DateTimeOffset >= dbType)
		{
			SqlMetaData sqlMetaData = sxm_rgDefaults[(int)dbType];
			_sqlDbType = dbType;
			_lMaxLength = sqlMetaData.MaxLength;
			_bPrecision = sqlMetaData.Precision;
			_bScale = sqlMetaData.Scale;
			_lLocale = sqlMetaData.LocaleId;
			_eCompareOptions = sqlMetaData.CompareOptions;
		}
	}

	private void ThrowIfUdt(SqlDbType dbType)
	{
		if (dbType == SqlDbType.Udt)
		{
			throw ADP.DbTypeNotSupported(SqlDbType.Udt.ToString());
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and user-defined type (UDT).</summary>
	/// <param name="name">The name of the column.</param>
	/// <param name="dbType">The SQL Server type of the parameter or column.</param>
	/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
	public SqlMetaData(string name, SqlDbType dbType, Type userDefinedType)
		: this(name, dbType, -1L, 0, 0, 0L, SqlCompareOptions.None, userDefinedType)
	{
	}
}
