using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace System.Web;

/// <summary>Provides a common base class for all site map data providers, and a way for developers to implement custom site map data providers that can be used with the ASP.NET site map infrastructure as persistent stores for <see cref="T:System.Web.SiteMap" /> objects. </summary>
public abstract class SiteMapProvider : ProviderBase
{
	private static readonly object siteMapResolveEvent = new object();

	internal object this_lock = new object();

	private bool enableLocalization;

	private SiteMapProvider parentProvider;

	private SiteMapProvider rootProviderCache;

	private bool securityTrimming;

	private object resolveLock = new object();

	private bool resolving;

	private EventHandlerList events = new EventHandlerList();

	private string resourceKey;

	/// <summary>Gets the <see cref="T:System.Web.SiteMapNode" /> object that represents the currently requested page.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, <see langword="null" />, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
	public virtual SiteMapNode CurrentNode
	{
		get
		{
			if (HttpContext.Current != null)
			{
				SiteMapNode siteMapNode = ResolveSiteMapNode(HttpContext.Current);
				if (siteMapNode != null)
				{
					return siteMapNode;
				}
				return FindSiteMapNode(HttpContext.Current);
			}
			return null;
		}
	}

	/// <summary>Gets or sets the parent <see cref="T:System.Web.SiteMapProvider" /> object of the current provider.</summary>
	/// <returns>The parent provider of the current <see cref="T:System.Web.SiteMapProvider" />.</returns>
	public virtual SiteMapProvider ParentProvider
	{
		get
		{
			return parentProvider;
		}
		set
		{
			parentProvider = value;
		}
	}

	/// <summary>Gets the root <see cref="T:System.Web.SiteMapProvider" /> object in the current provider hierarchy.</summary>
	/// <returns>An <see cref="T:System.Web.SiteMapProvider" /> that is the top-level site map provider in the provider hierarchy that the current provider belongs to.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">There is a circular reference to the current site map provider. </exception>
	public virtual SiteMapProvider RootProvider
	{
		get
		{
			lock (this_lock)
			{
				if (rootProviderCache == null)
				{
					SiteMapProvider siteMapProvider = this;
					while (siteMapProvider.ParentProvider != null)
					{
						siteMapProvider = siteMapProvider.ParentProvider;
					}
					rootProviderCache = siteMapProvider;
				}
			}
			return rootProviderCache;
		}
	}

	/// <summary>Gets or sets a Boolean value indicating whether localized values of <see cref="T:System.Web.SiteMapNode" /> attributes are returned.</summary>
	/// <returns>
	///     <see langword="true" /> if a localized value of the <see cref="T:System.Web.SiteMapNode" /> attributes are returned; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool EnableLocalization
	{
		get
		{
			return enableLocalization;
		}
		set
		{
			enableLocalization = value;
		}
	}

	/// <summary>Gets a Boolean value indicating whether a site map provider filters site map nodes based on a user's role.</summary>
	/// <returns>
	///     <see langword="true" /> if the provider is configured to filter nodes based on role; otherwise, <see langword="false" />.</returns>
	public bool SecurityTrimmingEnabled => securityTrimming;

	/// <summary>Get or sets the resource key that is used for localizing <see cref="T:System.Web.SiteMapNode" /> attributes. </summary>
	/// <returns>A string containing the resource key name.</returns>
	public string ResourceKey
	{
		get
		{
			return resourceKey;
		}
		set
		{
			resourceKey = value;
		}
	}

	/// <summary>Gets the root <see cref="T:System.Web.SiteMapNode" /> object of the site map data that the current provider represents.</summary>
	/// <returns>The root <see cref="T:System.Web.SiteMapNode" /> of the current site map data provider. The default implementation performs security trimming on the returned node.</returns>
	public virtual SiteMapNode RootNode => ReturnNodeIfAccessible(GetRootNodeCore());

	/// <summary>Occurs when the <see cref="P:System.Web.SiteMapProvider.CurrentNode" /> property is called. </summary>
	public event SiteMapResolveEventHandler SiteMapResolve
	{
		add
		{
			events.AddHandler(siteMapResolveEvent, value);
		}
		remove
		{
			events.RemoveHandler(siteMapResolveEvent, value);
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the node collection that is maintained by the site map provider.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the node collection maintained by the provider. </param>
	protected virtual void AddNode(SiteMapNode node)
	{
		AddNode(node, null);
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the node collection that is maintained by the site map provider and specifies the parent <see cref="T:System.Web.SiteMapNode" /> object. </summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the node collection maintained by the provider.</param>
	/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> that is the parent of <paramref name="node" />.</param>
	/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
	protected internal virtual void AddNode(SiteMapNode node, SiteMapNode parentNode)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the currently requested page using the specified <see cref="T:System.Web.HttpContext" /> object.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> used to match node information with the URL of the requested page.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, <see langword="null" />, if no corresponding <see cref="T:System.Web.SiteMapNode" /> can be found in the <see cref="T:System.Web.SiteMapNode" /> or if the page context is <see langword="null" />. </returns>
	public virtual SiteMapNode FindSiteMapNode(HttpContext context)
	{
		if (context == null)
		{
			return null;
		}
		HttpRequest request = context.Request;
		if (request == null)
		{
			return null;
		}
		SiteMapNode siteMapNode = FindSiteMapNode(request.RawUrl);
		if (siteMapNode == null)
		{
			siteMapNode = FindSiteMapNode(request.Path);
		}
		return siteMapNode;
	}

	/// <summary>When overridden in a derived class, retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
	/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, <see langword="null" />, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user.</returns>
	public abstract SiteMapNode FindSiteMapNode(string rawUrl);

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
	/// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, <see langword="null" />, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user. The default is <see langword="null" />.</returns>
	public virtual SiteMapNode FindSiteMapNodeFromKey(string key)
	{
		return FindSiteMapNode(key);
	}

	/// <summary>When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes. </param>
	/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, <see langword="null" /> or an empty collection, if no child nodes exist.</returns>
	public abstract SiteMapNodeCollection GetChildNodes(SiteMapNode node);

	/// <summary>Provides an optimized lookup method for site map providers when retrieving the node for the currently requested page and fetching the parent and ancestor site map nodes for the current page.</summary>
	/// <param name="upLevel">The number of ancestor site map node generations to get. A value of -1 indicates that all ancestors might be retrieved and cached by the provider.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, <see langword="null" />, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="upLevel" /> is less than -1.</exception>
	public virtual SiteMapNode GetCurrentNodeAndHintAncestorNodes(int upLevel)
	{
		if (upLevel < -1)
		{
			throw new ArgumentOutOfRangeException("upLevel");
		}
		return CurrentNode;
	}

	/// <summary>Provides an optimized lookup method for site map providers when retrieving the node for the currently requested page and fetching the site map nodes in the proximity of the current node.</summary>
	/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors might be retrieved and cached by the provider.</param>
	/// <param name="downLevel">The number of child <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no descendant nodes are retrieved and a -1 indicates that all descendant nodes might be retrieved and cached by the provider.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, <see langword="null" />, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="upLevel" /> or <paramref name="downLevel" /> is less than -1.</exception>
	public virtual SiteMapNode GetCurrentNodeAndHintNeighborhoodNodes(int upLevel, int downLevel)
	{
		if (upLevel < -1)
		{
			throw new ArgumentOutOfRangeException("upLevel");
		}
		if (downLevel < -1)
		{
			throw new ArgumentOutOfRangeException("downLevel");
		}
		return CurrentNode;
	}

	/// <summary>When overridden in a derived class, retrieves the parent node of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent node. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of <paramref name="node" />; otherwise, <see langword="null" />, if the <see cref="T:System.Web.SiteMapNode" /> has no parent or security trimming is enabled and the parent node is not accessible to the current user.
	///       <see cref="M:System.Web.SiteMapProvider.GetParentNode(System.Web.SiteMapNode)" /> might also return <see langword="null" /> if the parent node belongs to a different provider. In this case, use the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property of <paramref name="node" /> instead.</returns>
	public abstract SiteMapNode GetParentNode(SiteMapNode node);

	/// <summary>Provides an optimized lookup method for site map providers when retrieving an ancestor node for the currently requested page and fetching the descendant nodes for the ancestor.</summary>
	/// <param name="walkupLevels">The number of ancestor node levels to traverse when retrieving the requested ancestor node. </param>
	/// <param name="relativeDepthFromWalkup">The number of descendant node levels to retrieve from the target ancestor node. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an ancestor <see cref="T:System.Web.SiteMapNode" /> of the currently requested page; otherwise, <see langword="null" />, if the current or ancestor <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="walkupLevels" /> or <paramref name="relativeDepthFromWalkup" /> is less than 0.</exception>
	public virtual SiteMapNode GetParentNodeRelativeToCurrentNodeAndHintDownFromParent(int walkupLevels, int relativeDepthFromWalkup)
	{
		if (walkupLevels < 0)
		{
			throw new ArgumentOutOfRangeException("walkupLevels");
		}
		if (relativeDepthFromWalkup < 0)
		{
			throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
		}
		SiteMapNode siteMapNode = GetCurrentNodeAndHintAncestorNodes(walkupLevels);
		for (int i = 0; i < walkupLevels; i++)
		{
			if (siteMapNode == null)
			{
				break;
			}
			siteMapNode = GetParentNode(siteMapNode);
		}
		if (siteMapNode == null)
		{
			return null;
		}
		HintNeighborhoodNodes(siteMapNode, 0, relativeDepthFromWalkup);
		return siteMapNode;
	}

	/// <summary>Provides an optimized lookup method for site map providers when retrieving an ancestor node for the specified <see cref="T:System.Web.SiteMapNode" /> object and fetching its child nodes.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="walkupLevels" /> and <paramref name="relativeDepthFromWalkup" />. </param>
	/// <param name="walkupLevels">The number of ancestor node levels to traverse when retrieving the requested ancestor node.</param>
	/// <param name="relativeDepthFromWalkup">The number of descendant node levels to retrieve from the target ancestor node.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents an ancestor of <paramref name="node" />; otherwise, <see langword="null" />, if the current or ancestor <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="walkupLevels" /> or <paramref name="relativeDepthFromWalkup" /> is less than 0.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />.</exception>
	public virtual SiteMapNode GetParentNodeRelativeToNodeAndHintDownFromParent(SiteMapNode node, int walkupLevels, int relativeDepthFromWalkup)
	{
		if (walkupLevels < 0)
		{
			throw new ArgumentOutOfRangeException("walkupLevels");
		}
		if (relativeDepthFromWalkup < 0)
		{
			throw new ArgumentOutOfRangeException("relativeDepthFromWalkup");
		}
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		HintAncestorNodes(node, walkupLevels);
		for (int i = 0; i < walkupLevels; i++)
		{
			if (node == null)
			{
				break;
			}
			node = GetParentNode(node);
		}
		if (node == null)
		{
			return null;
		}
		HintNeighborhoodNodes(node, 0, relativeDepthFromWalkup);
		return node;
	}

	/// <summary>When overridden in a derived class, retrieves the root node of all the nodes that are currently managed by the current provider. </summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that the current provider manages. </returns>
	protected internal abstract SiteMapNode GetRootNodeCore();

	/// <summary>Retrieves the root node of all the nodes that are currently managed by the specified site map provider.</summary>
	/// <param name="provider">The provider that calls the <see cref="M:System.Web.SiteMapProvider.GetRootNodeCore" />.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that is managed by <paramref name="provider" />.</returns>
	protected static SiteMapNode GetRootNodeCoreFromProvider(SiteMapProvider provider)
	{
		return provider.GetRootNodeCore();
	}

	/// <summary>Provides a method that site map providers can override to perform an optimized retrieval of one or more levels of parent and ancestor nodes, relative to the specified <see cref="T:System.Web.SiteMapNode" /> object. </summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="upLevel" />.</param>
	/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors might be retrieved and cached.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="upLevel" /> is less than -1.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />.</exception>
	public virtual void HintAncestorNodes(SiteMapNode node, int upLevel)
	{
		if (upLevel < -1)
		{
			throw new ArgumentOutOfRangeException("upLevel");
		}
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
	}

	/// <summary>Provides a method that site map providers can override to perform an optimized retrieval of nodes found in the proximity of the specified node. </summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that acts as a reference point for <paramref name="upLevel" />.</param>
	/// <param name="upLevel">The number of ancestor <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no ancestor nodes are retrieved and -1 indicates that all ancestors (and their descendant nodes to the level of <paramref name="node" />) might be retrieved and cached.</param>
	/// <param name="downLevel">The number of descendant <see cref="T:System.Web.SiteMapNode" /> generations to fetch. 0 indicates no descendant nodes are retrieved and -1 indicates that all descendant nodes might be retrieved and cached.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="upLevel" /> or <paramref name="downLevel" /> is less than -1.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />.</exception>
	public virtual void HintNeighborhoodNodes(SiteMapNode node, int upLevel, int downLevel)
	{
		if (upLevel < -1)
		{
			throw new ArgumentOutOfRangeException("upLevel");
		}
		if (downLevel < -1)
		{
			throw new ArgumentOutOfRangeException("downLevel");
		}
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
	}

	/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from the node collection that is maintained by the site map provider.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to remove from the node collection maintained by the provider.</param>
	/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
	protected virtual void RemoveNode(SiteMapNode node)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the <see cref="T:System.Web.SiteMapProvider" /> implementation, including any resources that are needed to load site map data from persistent storage.</summary>
	/// <param name="name">The <see cref="P:System.Configuration.Provider.ProviderBase.Name" /> of the provider to initialize. </param>
	/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that can contain additional attributes to help initialize the provider. These attributes are read from the site map provider configuration in the Web.config file. </param>
	public override void Initialize(string name, NameValueCollection attributes)
	{
		base.Initialize(name, attributes);
		if (attributes["securityTrimmingEnabled"] != null)
		{
			securityTrimming = (bool)Convert.ChangeType(attributes["securityTrimmingEnabled"], typeof(bool));
		}
	}

	/// <summary>Retrieves a Boolean value indicating whether the specified <see cref="T:System.Web.SiteMapNode" /> object can be viewed by the user in the specified context.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that is requested by the user.</param>
	/// <returns>
	///     <see langword="true" /> if security trimming is enabled and <paramref name="node" /> can be viewed by the user or security trimming is not enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="context" /> is <see langword="null" />.- or -
	///         <paramref name="node" /> is <see langword="null" />.</exception>
	[MonoTODO("need to implement cases 2 and 3")]
	public virtual bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
	{
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		if (!SecurityTrimmingEnabled)
		{
			return true;
		}
		IList roles = node.Roles;
		if (roles != null && roles.Count > 0)
		{
			foreach (string item in roles)
			{
				if (item == "*" || context.User.IsInRole(item))
				{
					return true;
				}
			}
		}
		string text2 = node.Url;
		if (!string.IsNullOrEmpty(text2))
		{
			if (VirtualPathUtility.IsAppRelative(text2) || !VirtualPathUtility.IsAbsolute(text2))
			{
				text2 = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath), text2);
			}
			AuthorizationSection authorizationSection = (AuthorizationSection)WebConfigurationManager.GetSection("system.web/authorization", text2);
			if (authorizationSection != null)
			{
				return authorizationSection.IsValidUser(context.User, context.Request.HttpMethod);
			}
		}
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event. </summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for which the site map currently exists. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> provided by the event handler delegate that is registered to handle the event or, if more than one delegate is registered to handle the event, the return value of the last delegate in the delegate chain; otherwise, <see langword="null" />. </returns>
	protected SiteMapNode ResolveSiteMapNode(HttpContext context)
	{
		if (events[siteMapResolveEvent] is SiteMapResolveEventHandler siteMapResolveEventHandler)
		{
			lock (resolveLock)
			{
				if (resolving)
				{
					return null;
				}
				resolving = true;
				SiteMapResolveEventArgs e = new SiteMapResolveEventArgs(context, this);
				SiteMapNode result = siteMapResolveEventHandler(this, e);
				resolving = false;
				return result;
			}
		}
		return null;
	}

	internal static SiteMapNode ReturnNodeIfAccessible(SiteMapNode node)
	{
		if (node.IsAccessibleToUser(HttpContext.Current))
		{
			return node;
		}
		throw new InvalidOperationException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapProvider" /> class.</summary>
	protected SiteMapProvider()
	{
	}
}
