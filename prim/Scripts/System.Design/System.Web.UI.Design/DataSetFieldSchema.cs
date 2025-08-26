using System.Data;

namespace System.Web.UI.Design;

/// <summary>Represents the structure, or schema, of a data field.</summary>
public sealed class DataSetFieldSchema : IDataSourceFieldSchema
{
	/// <summary>Gets the type of data stored in the data field.</summary>
	/// <returns>A <see cref="T:System.Type" /> object that represents the type of data the data field contains.</returns>
	[System.MonoTODO]
	public Type DataType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the value of the data field automatically increments for each new row added to the table or view.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="P:System.Web.UI.Design.DataSetFieldSchema.DataType" /> is numeric and the value of the column increments automatically as new rows are added to the <see cref="T:System.Data.DataTable" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool Identity
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataColumn" /> is read-only.</summary>
	/// <returns>
	///   <see langword="true" /> if the data field is read-only; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether values in the data field are required to be unique.</summary>
	/// <returns>
	///   <see langword="true" /> if data in the data field is unique; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool IsUnique
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating the size of data that can be stored in the data field.</summary>
	/// <returns>The number of bytes the column can store.</returns>
	[System.MonoTODO]
	public int Length
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the data field.</summary>
	/// <returns>The name of the data field.</returns>
	[System.MonoTODO]
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the data field can accept <see langword="null" /> values.</summary>
	/// <returns>
	///   <see langword="true" /> if the data field can accept <see langword="null" /> values; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool Nullable
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the maximum number of digits used to represent a numerical value in the data field.</summary>
	/// <returns>This property always returns <see langword="-1" />.</returns>
	[System.MonoTODO]
	public int Precision
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the data field is in the primary key for the containing table or view.</summary>
	/// <returns>
	///   <see langword="true" /> if the data field is in the primary key; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool PrimaryKey
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of decimal places to which numerical values in the data field are resolved.</summary>
	/// <returns>This property always returns -1.</returns>
	[System.MonoTODO]
	public int Scale
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataSetFieldSchema" /> class using a specified <see cref="T:System.Data.DataColumn" />.</summary>
	/// <param name="column">A <see cref="T:System.Data.DataColumn" /> object that the <see cref="T:System.Web.UI.Design.DataSetFieldSchema" /> object  describes.</param>
	[System.MonoTODO]
	public DataSetFieldSchema(DataColumn column)
	{
		throw new NotImplementedException();
	}
}
