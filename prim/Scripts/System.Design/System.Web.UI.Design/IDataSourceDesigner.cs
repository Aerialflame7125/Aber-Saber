namespace System.Web.UI.Design;

/// <summary>Defines the basic functionality for a data source designer.</summary>
public interface IDataSourceDesigner
{
	/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if the underlying data source has a configuration wizard that can be launched with the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.Configure" /> method; otherwise, <see langword="false" />.</returns>
	bool CanConfigure { get; }

	/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if the underlying data source can refresh its schema; otherwise, <see langword="false" />.</returns>
	bool CanRefreshSchema { get; }

	/// <summary>Occurs when a data source has changed in a way that affects data-bound controls.</summary>
	event EventHandler DataSourceChanged;

	/// <summary>Occurs when the fields or data of the underlying data source have changed.</summary>
	event EventHandler SchemaRefreshed;

	/// <summary>Launches the underlying data source's configuration wizard.</summary>
	void Configure();

	/// <summary>Gets the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> for the specified view.</summary>
	/// <param name="viewName">The name of a view in the underlying data source.</param>
	/// <returns>A <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> containing information about the identified view, or <see langword="null" /> if a view with the specified name is not found.</returns>
	DesignerDataSourceView GetView(string viewName);

	/// <summary>Gets the names of the views in the underlying data source.</summary>
	/// <returns>An array of type <see cref="T:System.String" />.</returns>
	string[] GetViewNames();

	/// <summary>Refreshes the schema of the underlying data source.</summary>
	/// <param name="preferSilent">Indicates whether to suppress any events raised while refreshing the schema.</param>
	void RefreshSchema(bool preferSilent);

	/// <summary>Resumes raising data source events after calling the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.SuppressDataSourceEvents" /> method.</summary>
	void ResumeDataSourceEvents();

	/// <summary>Suppresses all events raised by a data source until the <see cref="M:System.Web.UI.Design.IDataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
	void SuppressDataSourceEvents();
}
