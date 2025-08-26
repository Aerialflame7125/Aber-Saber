using System.Data;

namespace System.ComponentModel.Design.Data;

/// <summary>Represents a column of a table or view in the data store accessed through a data connection. This class cannot be inherited.</summary>
public sealed class DesignerDataColumn
{
	private string name;

	private DbType data_type;

	private object default_value;

	private bool identity;

	private bool nullable;

	private bool primary_key;

	private int precision;

	private int scale;

	private int length;

	/// <summary>Gets the name of the column in the data store.</summary>
	/// <returns>The name of the column in the data store.</returns>
	public string Name => name;

	/// <summary>Gets the data type of the data column.</summary>
	/// <returns>One of the <see cref="T:System.Data.DbType" /> values.</returns>
	public DbType DataType => data_type;

	/// <summary>Gets the default value of the data column.</summary>
	/// <returns>The default value of the data column. The default is <see langword="null" />.</returns>
	public object DefaultValue => default_value;

	/// <summary>Gets a value indicating whether the data column is an identity column for the data row.</summary>
	/// <returns>
	///   <see langword="true" /> of the column is an identity column; otherwise, <see langword="false" />.</returns>
	public bool Identity => identity;

	/// <summary>Gets a value indicating whether the column can be null in the data store.</summary>
	/// <returns>
	///   <see langword="true" /> if the column can be null in the data store; otherwise, <see langword="false" />.</returns>
	public bool Nullable => nullable;

	/// <summary>Gets a value indicating whether the column is part of the table's primary key.</summary>
	/// <returns>
	///   <see langword="true" /> if the column is part of the table's primary key; otherwise, <see langword="false" />.</returns>
	public bool PrimaryKey => primary_key;

	/// <summary>Gets the number of digits in a numeric data column.</summary>
	/// <returns>The number of digits in a numeric data column.</returns>
	public int Precision => precision;

	/// <summary>Gets the number of digits to the right of the decimal point in a numeric column.</summary>
	/// <returns>The number of digits to the right of the decimal point in a numeric column.</returns>
	public int Scale => scale;

	/// <summary>Gets the length in bytes of the data column.</summary>
	/// <returns>The length of the data column, in bytes.</returns>
	public int Length => length;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified name and data type.</summary>
	/// <param name="name">The name identifying the column in the data store.</param>
	/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
	[System.MonoTODO]
	public DesignerDataColumn(string name, DbType dataType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified name, data type, and default value.</summary>
	/// <param name="name">The name identifying the column in the data store.</param>
	/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
	/// <param name="defaultValue">The default value of the column.</param>
	[System.MonoTODO]
	public DesignerDataColumn(string name, DbType dataType, object defaultValue)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> class with the specified values.</summary>
	/// <param name="name">The name identifying the column in the data store.</param>
	/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
	/// <param name="defaultValue">The default value of the column</param>
	/// <param name="identity">
	///   <see langword="true" /> if the field is the identity field of the data row; otherwise, <see langword="false" />.</param>
	/// <param name="nullable">
	///   <see langword="true" /> if the field can be null in the data store; otherwise, <see langword="false" />.</param>
	/// <param name="primaryKey">
	///   <see langword="true" /> if the field is the primary key of the data row; otherwise, <see langword="false" />.</param>
	/// <param name="precision">The maximum number of digits used by a numeric data field.</param>
	/// <param name="scale">The maximum number of digits to the right of the decimal point in a numeric data field.</param>
	/// <param name="length">The length of the data field, in bytes.</param>
	[System.MonoTODO]
	public DesignerDataColumn(string name, DbType dataType, object defaultValue, bool identity, bool nullable, bool primaryKey, int precision, int scale, int length)
	{
		this.name = name;
		data_type = dataType;
		default_value = defaultValue;
		this.identity = identity;
		this.nullable = nullable;
		primary_key = primaryKey;
		this.precision = precision;
		this.scale = scale;
		this.length = length;
	}
}
