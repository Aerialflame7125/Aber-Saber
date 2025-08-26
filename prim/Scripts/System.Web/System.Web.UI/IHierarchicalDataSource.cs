namespace System.Web.UI;

/// <summary>Represents a hierarchical data source that hierarchical data-bound controls such as <see cref="T:System.Web.UI.WebControls.TreeView" /> can bind to.</summary>
public interface IHierarchicalDataSource
{
	/// <summary>Occurs when the data storage that the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface represents has changed.</summary>
	event EventHandler DataSourceChanged;

	/// <summary>Gets the view helper object for the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface for the specified path.</summary>
	/// <param name="viewPath">The hierarchical path of the view to retrieve. </param>
	/// <returns>Returns a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> that represents a single view of the data at the hierarchical level identified by the <paramref name="viewPath" /> parameter.</returns>
	HierarchicalDataSourceView GetHierarchicalView(string viewPath);
}
