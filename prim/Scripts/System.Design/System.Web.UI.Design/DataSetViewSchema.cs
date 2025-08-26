using System.Data;

namespace System.Web.UI.Design;

/// <summary>Represents the structure, or schema, of a <see cref="T:System.Data.DataTable" />. This class cannot be inherited.</summary>
public sealed class DataSetViewSchema : IDataSourceViewSchema
{
	/// <summary>Gets the name of the view using its <see cref="P:System.Data.DataTable.TableName" /> property.</summary>
	/// <returns>The name of the view.</returns>
	[System.MonoTODO]
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.UI.Design.DataSetViewSchema" /> class using a specified <see cref="T:System.Data.DataTable" />.</summary>
	/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Web.UI.Design.DataSetViewSchema" /> instance will describe.</param>
	[System.MonoTODO]
	public DataSetViewSchema(DataTable dataTable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array representing the child views contained in the current view.</summary>
	/// <returns>
	///   <see langword="null" />.</returns>
	[System.MonoTODO]
	public IDataSourceViewSchema[] GetChildren()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets an array containing information about each data field in the view.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceFieldSchema" /> objects.</returns>
	[System.MonoTODO]
	public IDataSourceFieldSchema[] GetFields()
	{
		throw new NotImplementedException();
	}
}
