namespace System.Web.UI;

/// <summary>Represents a data view on a node or collection of nodes in a hierarchical data structure for a <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
public abstract class HierarchicalDataSourceView
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> class. </summary>
	protected HierarchicalDataSourceView()
	{
	}

	/// <summary>Gets a list of all the data items in the view.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of data items.</returns>
	public abstract IHierarchicalEnumerable Select();
}
