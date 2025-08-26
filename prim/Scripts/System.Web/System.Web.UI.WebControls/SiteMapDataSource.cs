using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Provides a data source control that Web server controls and other controls can use to bind to hierarchical site map data.</summary>
[PersistChildren(false)]
[Designer("System.Web.UI.Design.WebControls.SiteMapDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true)]
[ToolboxBitmap("")]
public class SiteMapDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
{
	private static string[] emptyNames = new string[1] { "DefaultView" };

	private SiteMapProvider provider;

	/// <summary>Gets a value that indicates whether the collection is a collection of <see cref="T:System.Collections.IList" /> objects.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is associated with one or more <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects; otherwise, <see langword="false" />.</returns>
	bool IListSource.ContainsListCollection => ContainsListCollection;

	/// <summary>Gets a value indicating whether the data source control contains a collection of data source view objects.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source control contains a collection of data source view objects; otherwise, <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool ContainsListCollection => ListSourceHelper.ContainsListCollection(this);

	/// <summary>Gets or sets a <see cref="T:System.Web.SiteMapProvider" /> object that is associated with the data source control.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapProvider" /> that is associated with the data source control; otherwise, if no provider is explicitly set, the default site map provider.</returns>
	/// <exception cref="T:System.Web.HttpException">The provider named by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.SiteMapProvider" /> is not available.- or -No default provider is configured for the site.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SiteMapProvider Provider
	{
		get
		{
			if (provider == null)
			{
				if (SiteMapProvider.Length == 0)
				{
					provider = SiteMap.Provider;
					if (provider == null)
					{
						throw new HttpException("There is no default provider configured for the site.");
					}
				}
				else
				{
					provider = SiteMap.Providers[SiteMapProvider];
					if (provider == null)
					{
						throw new HttpException("SiteMap provider '" + SiteMapProvider + "' not found.");
					}
				}
			}
			return provider;
		}
		set
		{
			provider = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets the name of the site map provider that the data source binds to.</summary>
	/// <returns>The name of the site map provider that the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> binds to. By default, the value is <see cref="F:System.String.Empty" />, and the default site map provider for the site is used.</returns>
	[DefaultValue("")]
	public virtual string SiteMapProvider
	{
		get
		{
			return ViewState.GetString("SiteMapProvider", "");
		}
		set
		{
			ViewState["SiteMapProvider"] = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a node in the site map that the data source then uses as a reference point to retrieve nodes from a hierarchical site map.</summary>
	/// <returns>The URL of a node in the site map. The <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> retrieves the identified <see cref="T:System.Web.SiteMapNode" /> and any child nodes from the site map. The default is an <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string StartingNodeUrl
	{
		get
		{
			return ViewState.GetString("StartingNodeUrl", "");
		}
		set
		{
			ViewState["StartingNodeUrl"] = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a positive or negative integer offset from the starting node that determines the root hierarchy that is exposed by the data source control.</summary>
	/// <returns>The default is 0, which indicates that the root hierarchy exposed by the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> is the same as the starting node.</returns>
	[DefaultValue(0)]
	public virtual int StartingNodeOffset
	{
		get
		{
			return ViewState.GetInt("StartingNodeOffset", 0);
		}
		set
		{
			ViewState["StartingNodeOffset"] = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a value indicating whether the site map node tree is retrieved using the node that represents the current page.</summary>
	/// <returns>
	///     <see langword="true" /> if the node tree is retrieved relative to the current page; otherwise, <see langword="false" />. The default is <see langword="false" />. </returns>
	[DefaultValue(false)]
	public virtual bool StartFromCurrentNode
	{
		get
		{
			return ViewState.GetBool("StartFromCurrentNode", def: false);
		}
		set
		{
			ViewState["StartFromCurrentNode"] = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a value indicating whether the starting node is retrieved and displayed. </summary>
	/// <returns>
	///     <see langword="true" /> if the starting node is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool ShowStartingNode
	{
		get
		{
			return ViewState.GetBool("ShowStartingNode", def: true);
		}
		set
		{
			ViewState["ShowStartingNode"] = value;
			OnDataSourceChanged(EventArgs.Empty);
		}
	}

	/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls.</summary>
	event EventHandler IDataSource.DataSourceChanged
	{
		add
		{
			((IHierarchicalDataSource)this).DataSourceChanged += value;
		}
		remove
		{
			((IHierarchicalDataSource)this).DataSourceChanged -= value;
		}
	}

	/// <summary>Retrieves a collection of named views for the data source control.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> method returns an <see cref="T:System.Collections.ICollection" /> with one <see cref="F:System.String.Empty" /> element.</returns>
	public virtual ICollection GetViewNames()
	{
		return emptyNames;
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
	IList IListSource.GetList()
	{
		return GetList();
	}

	/// <summary>Retrieves a list of data source controls that can be used as sources of lists of data.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> of data source controls that can be used as sources of lists of data.</returns>
	public virtual IList GetList()
	{
		return ListSourceHelper.GetList(this);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetView(System.String)" />.</summary>
	/// <param name="viewName">The URL of the root node of the view. </param>
	/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
	DataSourceView IDataSource.GetView(string viewName)
	{
		return GetView(viewName);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> of named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> objects associated with the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" />. Because the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> supports only one named view, the <see cref="M:System.Web.UI.WebControls.SiteMapDataSource.GetViewNames" /> returns a collection containing one element set to <see cref="F:System.String.Empty" />.</returns>
	ICollection IDataSource.GetViewNames()
	{
		return GetViewNames();
	}

	/// <summary>Retrieves a named view on the site map data of the site map provider according to the starting node and other properties of the data source.</summary>
	/// <param name="viewName">The name of the data source view to retrieve.</param>
	/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, according to the starting node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.Provider" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is <see langword="true" /> but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
	public virtual DataSourceView GetView(string viewName)
	{
		SiteMapNode startNode = GetStartNode(viewName);
		if (startNode == null)
		{
			return new SiteMapDataSourceView(this, viewName, SiteMapNodeCollection.EmptyList);
		}
		if (ShowStartingNode)
		{
			return new SiteMapDataSourceView(this, viewName, startNode);
		}
		return new SiteMapDataSourceView(this, viewName, startNode.ChildNodes);
	}

	/// <summary>Retrieves a single view on the site map data for the <see cref="T:System.Web.SiteMapProvider" /> object according to the starting node and other properties of the data source.</summary>
	/// <param name="viewPath">The URL of the starting node, specified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />. </param>
	/// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object on the site map data, starting with the node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" /> is <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" /> is configured or available for the site. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" /> is <see langword="true" /> but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot resolve a node for the specified URL.</exception>
	protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
	{
		SiteMapNode startNode = GetStartNode(viewPath);
		if (startNode == null)
		{
			return new SiteMapHierarchicalDataSourceView(SiteMapNodeCollection.EmptyList);
		}
		if (ShowStartingNode || startNode == null)
		{
			return new SiteMapHierarchicalDataSourceView(startNode);
		}
		return new SiteMapHierarchicalDataSourceView(startNode.ChildNodes);
	}

	[MonoTODO("handle StartNodeOffsets > 0")]
	private SiteMapNode GetStartNode(string viewPath)
	{
		if (viewPath != null && viewPath.Length != 0)
		{
			string rawUrl = MapUrl(StartingNodeUrl);
			return Provider.FindSiteMapNode(rawUrl);
		}
		SiteMapNode siteMapNode;
		if (StartFromCurrentNode)
		{
			if (StartingNodeUrl.Length != 0)
			{
				throw new InvalidOperationException("StartingNodeUrl can't be set if StartFromCurrentNode is set to true.");
			}
			siteMapNode = SiteMap.CurrentNode;
		}
		else if (StartingNodeUrl.Length != 0)
		{
			string rawUrl2 = MapUrl(StartingNodeUrl);
			siteMapNode = Provider.FindSiteMapNode(rawUrl2) ?? throw new ArgumentException("Can't find a site map node for the url: " + StartingNodeUrl);
		}
		else
		{
			siteMapNode = Provider.RootNode;
		}
		if (siteMapNode == null)
		{
			return null;
		}
		if (StartingNodeOffset < 0)
		{
			for (int i = StartingNodeOffset; i < 0; i++)
			{
				if (siteMapNode.ParentNode == null)
				{
					break;
				}
				siteMapNode = siteMapNode.ParentNode;
			}
		}
		else if (StartingNodeOffset > 0)
		{
			List<SiteMapNode> list = new List<SiteMapNode>();
			SiteMapNode siteMapNode2 = Provider.CurrentNode;
			while (siteMapNode2 != null && siteMapNode2 != siteMapNode)
			{
				list.Insert(0, siteMapNode2);
				siteMapNode2 = siteMapNode2.ParentNode;
			}
			if (siteMapNode2 == siteMapNode && StartingNodeOffset <= list.Count)
			{
				siteMapNode = list[StartingNodeOffset - 1];
			}
		}
		return siteMapNode;
	}

	private string MapUrl(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return string.Empty;
		}
		if (UrlUtils.IsRelativeUrl(url))
		{
			return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
		}
		return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> class. </summary>
	public SiteMapDataSource()
	{
	}
}
