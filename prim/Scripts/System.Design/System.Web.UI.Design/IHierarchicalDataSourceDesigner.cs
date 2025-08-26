namespace System.Web.UI.Design;

/// <summary>Provides design-time support in a visual designer for a class that is derived from the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> class.</summary>
public interface IHierarchicalDataSourceDesigner
{
	/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if the underlying data source has a configuration wizard that can be launched with <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" />, otherwise, <see langword="false" />.</returns>
	bool CanConfigure { get; }

	/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> can be called; otherwise, <see langword="false" />.</returns>
	bool CanRefreshSchema { get; }

	/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls.</summary>
	event EventHandler DataSourceChanged;

	/// <summary>Occurs when the fields or data of the underlying data source have changed.</summary>
	event EventHandler SchemaRefreshed;

	/// <summary>Launches the configuration wizard for the underlying data source.</summary>
	void Configure();

	/// <summary>Gets the named data source view that is associated with the data source control.</summary>
	/// <param name="viewPath">The XPath for the part of the data source to retrieve.</param>
	/// <returns>The named data source view that is associated with the data source control.</returns>
	DesignerHierarchicalDataSourceView GetView(string viewPath);

	/// <summary>Refreshes the schema of the underlying data source.</summary>
	/// <param name="preferSilent">
	///   <see langword="true" /> to suppress events raised while refreshing the schema; otherwise <see langword="false" />.</param>
	void RefreshSchema(bool preferSilent);

	/// <summary>Restores events after calling the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.SuppressDataSourceEvents" /> method.</summary>
	void ResumeDataSourceEvents();

	/// <summary>Turns off events in the data source control.</summary>
	void SuppressDataSourceEvents();
}
