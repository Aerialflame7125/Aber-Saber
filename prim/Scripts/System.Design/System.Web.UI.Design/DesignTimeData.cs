using System.Collections;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.Design;

/// <summary>Provides helper methods that can be used by control designers to generate sample data for data-bound properties at design time. This class cannot be inherited.</summary>
public sealed class DesignTimeData
{
	/// <summary>Gets an event handler for data binding.</summary>
	public static readonly EventHandler DataBindingHandler = OnDataBind;

	private DesignTimeData()
	{
	}

	/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object that contains three columns with names indicating that the columns are connected to a data source.</summary>
	/// <returns>A new <see cref="T:System.Data.DataTable" /> object with three columns and no data.</returns>
	[System.MonoTODO]
	public static DataTable CreateDummyDataBoundDataTable()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object that contains three columns with names that indicate that the columns contain sample data.</summary>
	/// <returns>A new <see cref="T:System.Data.DataTable" /> with three columns. These columns can contain data of type string.</returns>
	[System.MonoTODO]
	public static DataTable CreateDummyDataTable()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a sample <see cref="T:System.Data.DataTable" /> object with the same schema as the provided data.</summary>
	/// <param name="referenceData">A data source with the desired schema to use as the format for the sample <see cref="T:System.Data.DataTable" /> object.</param>
	/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains columns with the same names and data types as the provided <paramref name="referenceData" />.</returns>
	[System.MonoTODO]
	public static DataTable CreateSampleDataTable(IEnumerable referenceData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object with the same schema as the provided data and optionally containing column names indicating that the data is bound data.</summary>
	/// <param name="referenceData">An <see cref="T:System.Collections.IEnumerable" /> object containing data.</param>
	/// <param name="useDataBoundData">If <see langword="true" />, the column names indicate that they contain bound data.</param>
	/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
	[System.MonoTODO]
	public static DataTable CreateSampleDataTable(IEnumerable referenceData, bool useDataBoundData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a collection of property descriptors for the data fields of the specified data source.</summary>
	/// <param name="dataSource">The data source from which to retrieve the data fields.</param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> object that describes the data fields of the specified data source.</returns>
	[System.MonoTODO]
	public static PropertyDescriptorCollection GetDataFields(IEnumerable dataSource)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the specified data member from the specified data source.</summary>
	/// <param name="dataSource">An <see cref="T:System.ComponentModel.IListSource" /> that contains the data in which to find the member.</param>
	/// <param name="dataMember">The name of the data member to retrieve.</param>
	/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing the specified data member from the specified data source, if it exists.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataSource" /> is <see langword="null" />  
	/// -or-  
	/// <paramref name="dataMember" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public static IEnumerable GetDataMember(IListSource dataSource, string dataMember)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the names of the data members in the specified data source.</summary>
	/// <param name="dataSource">The data source from which to retrieve the names of the members.</param>
	/// <returns>An array of type <see langword="String" /> that contains the names of the data members in the specified data source.</returns>
	[System.MonoTODO]
	public static string[] GetDataMembers(object dataSource)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the specified number of sample rows to the specified data table.</summary>
	/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> object to which the sample rows are added.</param>
	/// <param name="minimumRows">The minimum number of rows to add.</param>
	/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing sample data for use at design time.</returns>
	[System.MonoTODO]
	public static IEnumerable GetDesignTimeDataSource(DataTable dataTable, int minimumRows)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a data source selected by name in the design host, represented by the specified component's site property and identified by the specified data source name.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> object that contains the data source.</param>
	/// <param name="dataSource">The name of the data source to retrieve.</param>
	/// <returns>An object implementing either <see cref="T:System.ComponentModel.IListSource" /> or <see cref="T:System.Collections.IEnumerable" /> representing the selected data source, or <see langword="null" /> if the data source or the designer host could not be accessed.</returns>
	[System.MonoTODO]
	public static object GetSelectedDataSource(IComponent component, string dataSource)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a data source selected by name in the design host, represented by the specified component's site property and identified by the specified data-source name and member name.</summary>
	/// <param name="component">The object implementing <see cref="T:System.ComponentModel.IComponent" /> that contains the data sourced property.</param>
	/// <param name="dataSource">The data source to retrieve.</param>
	/// <param name="dataMember">The data member to retrieve.</param>
	/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> containing the data member, or <see langword="null" /> if the data source, member, or component's site could not be accessed.</returns>
	[System.MonoTODO]
	public static IEnumerable GetSelectedDataSource(IComponent component, string dataSource, string dataMember)
	{
		throw new NotImplementedException();
	}

	[System.MonoTODO]
	private static void OnDataBind(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
