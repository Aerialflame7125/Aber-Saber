using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Defines an interface that a control designer can implement to provide access to a data source.</summary>
public interface IDataSourceProvider
{
	/// <summary>Gets the selected data member from the selected data source.</summary>
	/// <returns>The selected data member from the selected data source, if the control allows the user to select an <see cref="T:System.ComponentModel.IListSource" /> (such as a <see cref="T:System.Data.DataSet" />) for the data source, and provides a <see langword="DataMember" /> property to select a particular list (or <see cref="T:System.Data.DataTable" />) within the data source.</returns>
	IEnumerable GetResolvedSelectedDataSource();

	/// <summary>Gets a reference to the selected data source from the data source provider.</summary>
	/// <returns>The currently selected data source object of this data source provider.</returns>
	object GetSelectedDataSource();
}
