namespace System.Web.UI.Design;

/// <summary>Provides basic functionality for describing the structure of a data field at design time.</summary>
public interface IDataSourceFieldSchema
{
	/// <summary>Gets the type of data stored in the field.</summary>
	/// <returns>A <see cref="T:System.Type" /> object.</returns>
	Type DataType { get; }

	/// <summary>Gets a value indicating whether the value of the field automatically increments for each new row.</summary>
	/// <returns>
	///   <see langword="true" /> if the field's <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> is numeric and the underlying field increments automatically as new rows are added; otherwise, <see langword="false" />.</returns>
	bool Identity { get; }

	/// <summary>Gets a value indicating whether the field is editable.</summary>
	/// <returns>
	///   <see langword="true" /> if the field is read-only; otherwise, <see langword="false" />.</returns>
	bool IsReadOnly { get; }

	/// <summary>Gets a value indicating whether values in the field are required to be unique.</summary>
	/// <returns>
	///   <see langword="true" /> if data in the field must be unique; otherwise, <see langword="false" />.</returns>
	bool IsUnique { get; }

	/// <summary>Gets a value indicting the size of data that can be stored in the field.</summary>
	/// <returns>The number of bytes the field can store.</returns>
	int Length { get; }

	/// <summary>Gets the name of the field.</summary>
	/// <returns>The name of the field.</returns>
	string Name { get; }

	/// <summary>Gets a value indicating whether the field can accept <see langword="null" /> values.</summary>
	/// <returns>
	///   <see langword="true" /> if the field can accept <see langword="null" /> values; otherwise, <see langword="false" />.</returns>
	bool Nullable { get; }

	/// <summary>Gets the maximum number of digits used to represent a numerical value in the field.</summary>
	/// <returns>The maximum number of digits used to represent the values of the field if the <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> property of the field represents a numeric type. If this property is not implemented, it should return -1.</returns>
	int Precision { get; }

	/// <summary>Gets a value indicating whether the field is in the primary key.</summary>
	/// <returns>
	///   <see langword="true" /> if the field is in the primary key; otherwise, <see langword="false" />.</returns>
	bool PrimaryKey { get; }

	/// <summary>Gets the number of decimal places to which numerical values in the field are resolved.</summary>
	/// <returns>If the <see cref="P:System.Web.UI.Design.IDataSourceFieldSchema.DataType" /> property of the field represents a numeric type, returns the number of decimal places to which values are resolved, otherwise <see langword="-1" />.</returns>
	int Scale { get; }
}
