namespace System.Data.Common;

/// <summary>Represents a column within a data source.</summary>
public abstract class DbColumn
{
	/// <summary>Gets a nullable boolean value that indicates whether <see langword="DBNull" /> values are allowed in this column, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether <see langword="DBNull" /> values are allowed in this column, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if <see langword="DBNull" /> values are allowed in this column; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? AllowDBNull { get; protected set; }

	/// <summary>Gets the catalog name associated with the data source; otherwise, <see langword="null" /> if no value is set. Can be set to either the catalog name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The catalog name associated with the data source; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string BaseCatalogName { get; protected set; }

	/// <summary>Gets the base column name; otherwise, <see langword="null" /> if no value is set. Can be set to either the column name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The base column name; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string BaseColumnName { get; protected set; }

	/// <summary>Gets the schema name associated with the data source; otherwise, <see langword="null" /> if no value is set. Can be set to either the schema name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The schema name associated with the data source; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string BaseSchemaName { get; protected set; }

	/// <summary>Gets the server name associated with the column; otherwise, <see langword="null" /> if no value is set. Can be set to either the server name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The server name associated with the column; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string BaseServerName { get; protected set; }

	/// <summary>Gets the table name in the schema; otherwise, <see langword="null" /> if no value is set. Can be set to either the table name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The table name in the schema; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string BaseTableName { get; protected set; }

	/// <summary>Gets the name of the column. Can be set to the column name when overridden in a derived class.</summary>
	/// <returns>The name of the column.</returns>
	public string ColumnName { get; protected set; }

	/// <summary>Gets the column position (ordinal) in the datasource row; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the column position or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>An <see langword="int32" /> value for column ordinal; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public int? ColumnOrdinal { get; protected set; }

	/// <summary>Gets the column size; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the column size or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>An <see langword="int32" /> value for column size; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public int? ColumnSize { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is aliased, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is aliased, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is aliased; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsAliased { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether values in this column are automatically incremented, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether values in this column are automatically incremented, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if values in this column are automatically incremented; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsAutoIncrement { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is an expression, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is an expression, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is an expression; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsExpression { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is hidden, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is hidden, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is hidden; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsHidden { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is an identity, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is an identity, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is an identity; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsIdentity { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is a key, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is a key, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is a key; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsKey { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column contains long data, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column contains long data, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column contains long data; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsLong { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether this column is read-only, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether this column is read-only, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if this column is read-only; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsReadOnly { get; protected set; }

	/// <summary>Gets a nullable boolean value that indicates whether a unique constraint applies to this column, or returns <see langword="null" /> if no value is set. Can be set to either <see langword="true" /> or <see langword="false" /> indicating whether a unique constraint applies to this column, or <see langword="null" /> (<see langword="Nothing" /> in Visual Basic) when overridden in a derived class.</summary>
	/// <returns>Returns <see langword="true" /> if a unique constraint applies to this column; otherwise, <see langword="false" />. If no value is set, returns a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	public bool? IsUnique { get; protected set; }

	/// <summary>Gets the numeric precision of the column data; otherwise, <see langword="null" /> if no value is set. Can be set to either an <see langword="int32" /> value to specify the numeric precision of the column data or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>An <see langword="int32" /> value that specifies the precision of the column data, if the data is numeric; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public int? NumericPrecision { get; protected set; }

	/// <summary>Gets a nullable <see langword="int32" /> value that either returns <see langword="null" /> or the numeric scale of the column data. Can be set to either <see langword="null" /> or an <see langword="int32" /> value for the numeric scale of the column data when overridden in a derived class.</summary>
	/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) if no value is set; otherwise, a <see langword="int32" /> value that specifies the scale of the column data, if the data is numeric.</returns>
	public int? NumericScale { get; protected set; }

	/// <summary>Gets the assembly-qualified name of the <see cref="T:System.Type" /> object that represents the type of data in the column; otherwise, <see langword="null" /> if no value is set. Can be set to either the assembly-qualified name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The assembly-qualified name of the <see cref="T:System.Type" /> object that represents the type of data in the column; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string UdtAssemblyQualifiedName { get; protected set; }

	/// <summary>Gets the type of data stored in the column. Can be set to a <see cref="T:System.Type" /> object that represents the type of data in the column when overridden in a derived class.</summary>
	/// <returns>A <see cref="T:System.Type" /> object that represents the type of data the column contains.</returns>
	public Type DataType { get; protected set; }

	/// <summary>Gets the name of the data type; otherwise, <see langword="null" /> if no value is set. Can be set to either the data type name or <see langword="null" /> when overridden in a derived class.</summary>
	/// <returns>The name of the data type; otherwise, a null reference (<see langword="Nothing" /> in Visual Basic) if no value is set.</returns>
	public string DataTypeName { get; protected set; }

	/// <summary>Gets the object based on the column property name.</summary>
	/// <param name="property">The column property name.</param>
	/// <returns>The object based on the column property name.</returns>
	public virtual object this[string property] => property switch
	{
		"AllowDBNull" => AllowDBNull, 
		"BaseCatalogName" => BaseCatalogName, 
		"BaseColumnName" => BaseColumnName, 
		"BaseSchemaName" => BaseSchemaName, 
		"BaseServerName" => BaseServerName, 
		"BaseTableName" => BaseTableName, 
		"ColumnName" => ColumnName, 
		"ColumnOrdinal" => ColumnOrdinal, 
		"ColumnSize" => ColumnSize, 
		"IsAliased" => IsAliased, 
		"IsAutoIncrement" => IsAutoIncrement, 
		"IsExpression" => IsExpression, 
		"IsHidden" => IsHidden, 
		"IsIdentity" => IsIdentity, 
		"IsKey" => IsKey, 
		"IsLong" => IsLong, 
		"IsReadOnly" => IsReadOnly, 
		"IsUnique" => IsUnique, 
		"NumericPrecision" => NumericPrecision, 
		"NumericScale" => NumericScale, 
		"UdtAssemblyQualifiedName" => UdtAssemblyQualifiedName, 
		"DataType" => DataType, 
		"DataTypeName" => DataTypeName, 
		_ => null, 
	};

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbColumn" /> class.</summary>
	protected DbColumn()
	{
	}
}
