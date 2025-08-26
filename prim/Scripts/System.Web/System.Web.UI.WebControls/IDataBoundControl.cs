namespace System.Web.UI.WebControls;

/// <summary>Defines properties that are shared by data-bound controls.</summary>
public interface IDataBoundControl
{
	/// <summary>Gets or sets the ID of the data source control from which the data-bound control retrieves a list of data items.</summary>
	/// <returns>The ID of the data source control that contains the list of data items that the data-bound control retrieves.</returns>
	string DataSourceID { get; set; }

	/// <summary>Gets the data source object from which the data-bound control retrieves a list of data items.</summary>
	/// <returns>The data source object that contains the list of data items that the data-bound control retrieves. </returns>
	IDataSource DataSourceObject { get; }

	/// <summary>Gets or sets the object from which the data-bound control retrieves a list of data items.</summary>
	/// <returns>The object that contains the list of data that the data-bound control retrieves.</returns>
	object DataSource { get; set; }

	/// <summary>Gets or sets an array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</summary>
	/// <returns>An array that contains the names of the primary-key fields of the items that are displayed in a data-bound control.</returns>
	string[] DataKeyNames { get; set; }

	/// <summary>Gets or sets the name of the list of data that the data-bound control binds to when the data source contains more than one list of data items.</summary>
	/// <returns>The name of the list of data that the data-bound control binds to.</returns>
	string DataMember { get; set; }
}
