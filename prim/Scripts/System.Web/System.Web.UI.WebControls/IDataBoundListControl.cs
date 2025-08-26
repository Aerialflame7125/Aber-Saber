namespace System.Web.UI.WebControls;

/// <summary>Exposes the common properties of data-bound-controls that display multiple rows. </summary>
public interface IDataBoundListControl : IDataBoundControl
{
	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that represent the data key value of each row in a data-bound control.</summary>
	/// <returns>A collection of data-key objects that contains the data-key value of each row in a data-bound control.</returns>
	DataKeyArray DataKeys { get; }

	/// <summary>Gets the object that contains the data-key value for the selected row in a data-bound control.</summary>
	/// <returns>The object that contains the data-key value for the selected row in a data-bound control.</returns>
	DataKey SelectedDataKey { get; }

	/// <summary>Gets or sets the index of the selected row in a data-bound control.</summary>
	/// <returns>The index of the selected row in a data-bound control.</returns>
	int SelectedIndex { get; set; }

	/// <summary>Gets or sets the names of the data fields whose values are appended to the <see cref="P:System.Web.UI.Control.ClientID" /> property value to uniquely identify each instance of a data-bound control.</summary>
	/// <returns>An array of data field names.</returns>
	string[] ClientIDRowSuffix { get; set; }

	/// <summary>Gets or sets a value that indicates whether the selection of a row is based on index or on data-key values.</summary>
	/// <returns>
	///     <see langword="true" /> if the row selection is based on data-key values; otherwise, <see langword="false" />.</returns>
	bool EnablePersistedSelection { get; set; }
}
