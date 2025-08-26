namespace System.Web.UI.WebControls;

/// <summary>Represents a data view on a site map node or collection of nodes for a <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control.</summary>
public class SiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
{
	private SiteMapNodeCollection collection;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, adding the specified node to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
	/// <param name="node">A <see cref="T:System.Web.SiteMapNode" /> that the data source view represents.</param>
	public SiteMapHierarchicalDataSourceView(SiteMapNode node)
		: this(new SiteMapNodeCollection(node))
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> class, setting the specified collection to the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection that the data source view maintains.</summary>
	/// <param name="collection">A <see cref="T:System.Web.SiteMapNodeCollection" /> that the data source view represents.</param>
	public SiteMapHierarchicalDataSourceView(SiteMapNodeCollection collection)
	{
		this.collection = collection;
	}

	/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of site map nodes.</returns>
	public override IHierarchicalEnumerable Select()
	{
		return collection;
	}
}
