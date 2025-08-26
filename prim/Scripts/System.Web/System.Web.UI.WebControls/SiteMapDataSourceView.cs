using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Provides a strongly typed <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object for the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control.</summary>
public class SiteMapDataSourceView : DataSourceView
{
	private SiteMapNodeCollection collection;

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> class, adding the single specified node to the internal collection of nodes.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> is associated with.</param>
	/// <param name="name">The name of the view.</param>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the internal <see cref="T:System.Web.SiteMapNodeCollection" />. </param>
	public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNode node)
		: this(owner, name, new SiteMapNodeCollection(node))
	{
	}

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> class, setting the internal collection of nodes to the specified node collection.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> is associated with.</param>
	/// <param name="name">The name of the view.</param>
	/// <param name="collection">A <see cref="T:System.Web.SiteMapNodeCollection" /> of nodes that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> provides a view of. </param>
	public SiteMapDataSourceView(SiteMapDataSource owner, string name, SiteMapNodeCollection collection)
		: base(owner, name)
	{
		this.collection = collection;
	}

	/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNodeCollection" /> that represents the site navigation structure for the current user. </returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SiteMapDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
	public IEnumerable Select(DataSourceSelectArguments arguments)
	{
		return ExecuteSelect(arguments);
	}

	/// <summary>Gets the collection of the <see cref="T:System.Web.SiteMapNode" /> objects that represents the site navigation structure for the current user.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNodeCollection" /> that represents the site navigation structure for the current user.</returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SiteMapDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		arguments.RaiseUnsupportedCapabilitiesError(this);
		return collection;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected override void OnDataSourceViewChanged(EventArgs e)
	{
		base.OnDataSourceViewChanged(e);
	}
}
