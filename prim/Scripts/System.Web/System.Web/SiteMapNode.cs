using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Resources;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web;

/// <summary>Represents a node in the hierarchical site map structure such as that described by the <see cref="T:System.Web.SiteMap" /> class and classes that implement the abstract <see cref="T:System.Web.SiteMapProvider" /> class.</summary>
public class SiteMapNode : IHierarchyData, INavigateUIData, ICloneable
{
	private SiteMapProvider provider;

	private string key;

	private string url;

	private string title;

	private string description;

	private IList roles;

	private NameValueCollection attributes;

	private NameValueCollection resourceKeys;

	private bool readOnly;

	private string resourceKey;

	private SiteMapNode parent;

	private SiteMapNodeCollection childNodes;

	private IPrincipal user;

	/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> has any child nodes.</summary>
	/// <returns>
	///     <see langword="true" /> if the node has children; otherwise, <see langword="false" />.</returns>
	public virtual bool HasChildNodes
	{
		get
		{
			SiteMapNodeCollection siteMapNodeCollection = ChildNodes;
			if (siteMapNodeCollection != null)
			{
				return siteMapNodeCollection.Count > 0;
			}
			return false;
		}
	}

	/// <summary>Gets the next <see cref="T:System.Web.SiteMapNode" /> node on the same hierarchical level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property (if one exists).</summary>
	/// <returns>The next <see cref="T:System.Web.SiteMapNode" />, serially, after the current one, under the parent node; otherwise, <see langword="null" />, if no parent exists, there is no node that follows this one, or security trimming is enabled and the user cannot view the parent or next sibling nodes.</returns>
	public virtual SiteMapNode NextSibling
	{
		get
		{
			IList siblingNodes = SiblingNodes;
			if (siblingNodes == null)
			{
				return null;
			}
			int num = siblingNodes.IndexOf(this);
			if (num >= 0 && num < siblingNodes.Count - 1)
			{
				return (SiteMapNode)siblingNodes[num + 1];
			}
			return null;
		}
	}

	/// <summary>Gets the previous <see cref="T:System.Web.SiteMapNode" /> object on the same level as the current one, relative to the <see cref="P:System.Web.SiteMapNode.ParentNode" /> object (if one exists).</summary>
	/// <returns>The previous <see cref="T:System.Web.SiteMapNode" />, serially, before the current one, under the parent node; otherwise, <see langword="null" />, if no parent exists, there is no node before this one, or security trimming is enabled and the user cannot view the parent or previous sibling nodes.</returns>
	public virtual SiteMapNode PreviousSibling
	{
		get
		{
			IList siblingNodes = SiblingNodes;
			if (siblingNodes == null)
			{
				return null;
			}
			int num = siblingNodes.IndexOf(this);
			if (num > 0 && num < siblingNodes.Count)
			{
				return (SiteMapNode)siblingNodes[num - 1];
			}
			return null;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that is the parent of the current node.</summary>
	/// <returns>The parent <see cref="T:System.Web.SiteMapNode" />; otherwise, <see langword="null" />, if security trimming is enabled and the user cannot view the parent node.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public virtual SiteMapNode ParentNode
	{
		get
		{
			if (parent != null)
			{
				return parent;
			}
			SiteMapProvider parentProvider = provider;
			do
			{
				parent = parentProvider.GetParentNode(this);
				if (parent != null)
				{
					return parent;
				}
				parentProvider = parentProvider.ParentProvider;
			}
			while (parentProvider != null);
			return null;
		}
		set
		{
			CheckWritable();
			parent = value;
		}
	}

	/// <summary>Gets or sets all the child nodes of the current <see cref="T:System.Web.SiteMapNode" /> object from the associated <see cref="T:System.Web.SiteMapProvider" /> provider.</summary>
	/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> of child nodes, if any exist for the current node; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public virtual SiteMapNodeCollection ChildNodes
	{
		get
		{
			if (provider.SecurityTrimmingEnabled)
			{
				IPrincipal principal = HttpContext.Current.User;
				if ((user == null && user != principal) || (user != null && user != principal))
				{
					user = principal;
					childNodes = provider.GetChildNodes(this);
				}
			}
			else if (childNodes == null)
			{
				childNodes = provider.GetChildNodes(this);
			}
			return childNodes;
		}
		set
		{
			CheckWritable();
			user = null;
			childNodes = value;
		}
	}

	/// <summary>Gets the root node of the root provider in a site map provider hierarchy. If no provider hierarchy exists, the <see cref="P:System.Web.SiteMapNode.RootNode" /> property gets the root node of the current provider. </summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the site navigation structure.</returns>
	/// <exception cref="T:System.InvalidOperationException">The root node cannot be retrieved from the root provider.</exception>
	public virtual SiteMapNode RootNode => provider.RootProvider.RootNode;

	private SiteMapNodeCollection SiblingNodes
	{
		get
		{
			if (ParentNode != null)
			{
				return ParentNode.ChildNodes;
			}
			return null;
		}
	}

	/// <summary>Gets or sets a custom attribute from the <see cref="P:System.Web.SiteMapNode.Attributes" /> collection or a resource string based on the specified key.</summary>
	/// <param name="key">A string that identifies the attribute or resource string to retrieve.</param>
	/// <returns>A custom attribute or resource string identified by <paramref name="key" />; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public virtual string this[string key]
	{
		get
		{
			if (provider.EnableLocalization)
			{
				string text = GetImplicitResourceString(key);
				if (text == null)
				{
					text = GetExplicitResourceString(key, null, throwIfNotFound: true);
				}
				if (text != null)
				{
					return text;
				}
			}
			if (attributes != null)
			{
				return attributes[key];
			}
			return null;
		}
		set
		{
			CheckWritable();
			if (attributes == null)
			{
				attributes = new NameValueCollection();
			}
			attributes[key] = value;
		}
	}

	/// <summary>Gets or sets a collection of additional attributes beyond the strongly typed properties that are defined for the <see cref="T:System.Web.SiteMapNode" /> class.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes for the <see cref="T:System.Web.SiteMapNode" /> beyond <see cref="P:System.Web.SiteMapNode.Title" />, <see cref="P:System.Web.SiteMapNode.Description" />, <see cref="P:System.Web.SiteMapNode.Url" />, and <see cref="P:System.Web.SiteMapNode.Roles" />; otherwise, <see langword="null" />, if no attributes exist.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	protected NameValueCollection Attributes
	{
		get
		{
			return attributes;
		}
		set
		{
			CheckWritable();
			attributes = value;
		}
	}

	/// <summary>Gets or sets a description for the <see cref="T:System.Web.SiteMapNode" />. </summary>
	/// <returns>A string that represents a description of the node; otherwise, <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	[Localizable(true)]
	public virtual string Description
	{
		get
		{
			string text = null;
			if (provider.EnableLocalization)
			{
				text = GetImplicitResourceString("description");
				if (text == null)
				{
					text = GetExplicitResourceString("description", description, throwIfNotFound: true);
				}
			}
			else
			{
				text = description;
			}
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			CheckWritable();
			description = value;
		}
	}

	/// <summary>Gets or sets the title of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
	/// <returns>A string that represents the title of the node. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	[Localizable(true)]
	public virtual string Title
	{
		get
		{
			string text = null;
			if (provider.EnableLocalization)
			{
				text = GetImplicitResourceString("title");
				if (text == null)
				{
					text = GetExplicitResourceString("title", title, throwIfNotFound: true);
				}
			}
			else
			{
				text = title;
			}
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			CheckWritable();
			title = value;
		}
	}

	/// <summary>Gets or sets the URL of the page that the <see cref="T:System.Web.SiteMapNode" /> object represents.</summary>
	/// <returns>The URL of the page that the node represents. The default is <see cref="F:System.String.Empty" />. </returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public virtual string Url
	{
		get
		{
			if (url == null)
			{
				return "";
			}
			return url;
		}
		set
		{
			CheckWritable();
			url = value;
		}
	}

	/// <summary>Gets or sets a collection of roles that are associated with the <see cref="T:System.Web.SiteMapNode" /> object, used during security trimming. </summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> of roles.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public IList Roles
	{
		get
		{
			return roles;
		}
		set
		{
			CheckWritable();
			roles = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the site map node can be modified.</summary>
	/// <returns>
	///     <see langword="true" /> if the site map node can be modified; otherwise, <see langword="false" />.</returns>
	public bool ReadOnly
	{
		get
		{
			return readOnly;
		}
		set
		{
			readOnly = value;
		}
	}

	/// <summary>Gets or sets the resource key that is used to localize the <see cref="T:System.Web.SiteMapNode" />.</summary>
	/// <returns>A string containing the resource key name.</returns>
	/// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
	public string ResourceKey
	{
		get
		{
			return resourceKey;
		}
		set
		{
			if (ReadOnly)
			{
				throw new InvalidOperationException("The node is read-only.");
			}
			resourceKey = value;
		}
	}

	/// <summary>Gets a string representing a lookup key for a site map node.</summary>
	/// <returns>A string representing a lookup key.</returns>
	public string Key => key;

	/// <summary>Gets the <see cref="T:System.Web.SiteMapProvider" /> provider that the <see cref="T:System.Web.SiteMapNode" /> object is tracked by.</summary>
	/// <returns>The <see cref="T:System.Web.SiteMapProvider" /> that the <see cref="T:System.Web.SiteMapNode" /> is tracked by. </returns>
	public SiteMapProvider Provider => provider;

	/// <summary>Gets a value that indicates whether the current <see cref="T:System.Web.SiteMapNode" /> object has any child nodes. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.HasChildren" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the node has child nodes; otherwise, <see langword="false" />.</returns>
	bool IHierarchyData.HasChildren => HasChildNodes;

	/// <summary>Gets the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Item" />.</summary>
	/// <returns>An hierarchical data node object.</returns>
	object IHierarchyData.Item => this;

	/// <summary>Gets the path of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Path" />.</summary>
	/// <returns>The path of the data item.</returns>
	string IHierarchyData.Path => Url;

	/// <summary>Gets a string that represents the type name of the hierarchical data item. For a description of this member, see <see cref="P:System.Web.UI.IHierarchyData.Type" />.</summary>
	/// <returns>The string named "SiteMapNode".</returns>
	string IHierarchyData.Type => "SiteMapNode";

	/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Name" />.</summary>
	/// <returns>Text that is displayed for a node of a navigation control; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Title" /> is set for the node.</returns>
	string INavigateUIData.Name => Title;

	/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Url" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.NavigateUrl" />.</summary>
	/// <returns>The URL to navigate to when the node is clicked; otherwise, <see cref="F:System.String.Empty" /> if no <see cref="P:System.Web.SiteMapNode.Url" /> is set for the node.</returns>
	string INavigateUIData.NavigateUrl => Url;

	/// <summary>Gets the <see cref="P:System.Web.SiteMapNode.Title" /> property of the site map node. For a description of this member, see <see cref="P:System.Web.UI.INavigateUIData.Value" />.</summary>
	/// <returns>A value that is not displayed; otherwise, <see cref="F:System.String.Empty" />.</returns>
	string INavigateUIData.Value => Title;

	private SiteMapNode()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class, using the specified <paramref name="key" /> to identify the page that the node represents and the site map provider that manages the node.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
	/// <param name="key">A provider-specific lookup key.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. - or -
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public SiteMapNode(SiteMapProvider provider, string key)
		: this(provider, key, null, null, null, null, null, null, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, and the site map provider that manages the node.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
	/// <param name="key">A provider-specific lookup key.</param>
	/// <param name="url">The URL of the page that the node represents within the site. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. - or -
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public SiteMapNode(SiteMapProvider provider, string key, string url)
		: this(provider, key, url, null, null, null, null, null, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title, and the site map provider that manages the node.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
	/// <param name="key">A provider-specific lookup key.</param>
	/// <param name="url">The URL of the page that the node represents within the site. </param>
	/// <param name="title">A label for the node, often displayed by navigation controls. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. - or -
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public SiteMapNode(SiteMapProvider provider, string key, string url, string title)
		: this(provider, key, url, title, null, null, null, null, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified URL, a <paramref name="key" /> to identify the page that the node represents, a title and description, and the site map provider that manages the node.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
	/// <param name="key">A provider-specific lookup key.</param>
	/// <param name="url">The URL of the page that the node represents within the site. </param>
	/// <param name="title">A label for the node, often displayed by navigation controls. </param>
	/// <param name="description">A description of the page that the node represents. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. - or -
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description)
		: this(provider, key, url, title, description, null, null, null, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapNode" /> class using the specified site map provider that manages the node, URL, title, description, roles, additional attributes, and explicit and implicit resource keys for localization.</summary>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> with which the node is associated. </param>
	/// <param name="key">A provider-specific lookup key. </param>
	/// <param name="url">The URL of the page that the node represents within the site. </param>
	/// <param name="title">A label for the node, often displayed by navigation controls. </param>
	/// <param name="description">A description of the page that the node represents. </param>
	/// <param name="roles">An <see cref="T:System.Collections.IList" /> of roles that are allowed to view the page represented by the <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes used to initialize the <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <param name="explicitResourceKeys">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of explicit resource keys used for localization. </param>
	/// <param name="implicitResourceKey">An implicit resource key used for localization.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <see cref="T:System.Web.SiteMapProvider" /> is <see langword="null" />. - or -
	///         <paramref name="key" /> is <see langword="null" />.</exception>
	public SiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes, NameValueCollection explicitResourceKeys, string implicitResourceKey)
	{
		if (provider == null)
		{
			throw new ArgumentNullException("provider");
		}
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		this.provider = provider;
		this.key = key;
		this.url = url;
		this.title = title;
		this.description = description;
		this.roles = roles;
		this.attributes = attributes;
		resourceKeys = explicitResourceKeys;
		resourceKey = implicitResourceKey;
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> object that is associated with the current node.</summary>
	/// <param name="owner">A <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> control that the view is associated with.</param>
	/// <param name="viewName">The name of the view.</param>
	/// <returns>A named <see cref="T:System.Web.UI.WebControls.SiteMapDataSourceView" /> for the current node.</returns>
	public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
	{
		return new SiteMapDataSourceView(owner, viewName, this);
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> object that is associated with the current node.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView" /> for the current node.</returns>
	public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
	{
		return new SiteMapHierarchicalDataSourceView(this);
	}

	/// <summary>Gets a value indicating whether the specified site map node can be viewed by the user in the specified context.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
	/// <returns>
	///     <see langword="true" /> if any one of the following conditions is met: the security trimming is enabled and the current user is a member of at least one of the roles allowing access to view the site map node; the current user is authorized specifically for the requested node's URL in the authorization element for the current application and the URL is located within the directory structure for the application; the current thread has an associated <see cref="T:System.Security.Principal.WindowsIdentity" /> that has file access to the requested node's URL and the URL is located within the directory structure for the application; or security trimming is not enabled and therefore any user is allowed to view the site map node; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified context is <see langword="null" />.</exception>
	public virtual bool IsAccessibleToUser(HttpContext context)
	{
		return provider.IsAccessibleToUser(context, this);
	}

	/// <summary>Converts the value of this instance of the <see cref="T:System.Web.SiteMapNode" /> class to its equivalent string representation.</summary>
	/// <returns>The string representation of the value of this <see cref="T:System.Web.SiteMapNode" />.</returns>
	public override string ToString()
	{
		return Title;
	}

	/// <summary>Retrieves a read-only collection of all <see cref="T:System.Web.SiteMapNode" /> objects that are descendants of the calling node, regardless of the degree of separation.</summary>
	/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that represents all the descendants of a <see cref="T:System.Web.SiteMapNode" /> within the scope of the current provider.</returns>
	public SiteMapNodeCollection GetAllNodes()
	{
		SiteMapNodeCollection siteMapNodeCollection = new SiteMapNodeCollection();
		GetAllNodesRecursive(siteMapNodeCollection);
		return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
	}

	private void GetAllNodesRecursive(SiteMapNodeCollection c)
	{
		SiteMapNodeCollection siteMapNodeCollection = ChildNodes;
		if (siteMapNodeCollection == null || siteMapNodeCollection.Count <= 0)
		{
			return;
		}
		c.AddRange(siteMapNodeCollection);
		foreach (SiteMapNode item in siteMapNodeCollection)
		{
			item.GetAllNodesRecursive(c);
		}
	}

	/// <summary>Gets a value indicating whether the current site map node is a child or a direct descendant of the specified node.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to check if the current node is a child or descendant of.</param>
	/// <returns>
	///     <see langword="true" /> if the current node is a child or descendant of the specified node; otherwise, <see langword="false" />.</returns>
	public virtual bool IsDescendantOf(SiteMapNode node)
	{
		for (SiteMapNode parentNode = ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
		{
			if (parentNode == node)
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Retrieves a localized string based on a <see cref="T:System.Web.SiteMapNode" /> attribute to localize, a default string to return if no resource is found, and a Boolean value indicating whether to throw an exception if no resource is found. </summary>
	/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize. </param>
	/// <param name="defaultValue">The default value to return if a matching resource is not found.</param>
	/// <param name="throwIfNotFound">
	///       <see langword="true" /> to throw an <see cref="T:System.InvalidOperationException" />, if an explicit resource is defined for <paramref name="attributeName" />, <paramref name="defaultValue" /> is <see langword="null" />, and a localized value is not found; otherwise, <see langword="false" />. </param>
	/// <returns>A string representing the localized attribute.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="attributeName" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">A matching resource object was not found and <paramref name="throwIfNotFound" /> is <see langword="true" />. </exception>
	protected string GetExplicitResourceString(string attributeName, string defaultValue, bool throwIfNotFound)
	{
		if (attributeName == null)
		{
			throw new ArgumentNullException("attributeName");
		}
		if (resourceKeys != null)
		{
			string[] values = resourceKeys.GetValues(attributeName);
			if (values != null && values.Length == 2)
			{
				try
				{
					object globalResourceObject = HttpContext.GetGlobalResourceObject(values[0], values[1]);
					if (globalResourceObject is string)
					{
						return (string)globalResourceObject;
					}
				}
				catch (MissingManifestResourceException)
				{
				}
				if (throwIfNotFound && defaultValue == null)
				{
					throw new InvalidOperationException($"The resource object with classname '{values[0]}' and key '{values[1]}' was not found.");
				}
			}
		}
		return defaultValue;
	}

	/// <summary>Gets a localized string based on the attribute name and <see cref="P:System.Web.SiteMapProvider.ResourceKey" /> property that is specified by the <see cref="T:System.Web.SiteMapProvider" /> by which the <see cref="T:System.Web.SiteMapNode" /> is tracked.</summary>
	/// <param name="attributeName">The <see cref="T:System.Web.SiteMapNode" /> attribute to localize.</param>
	/// <returns>A string representing the localized attribute. The default is <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="attributeName" /> is <see langword="null" />. </exception>
	protected string GetImplicitResourceString(string attributeName)
	{
		if (attributeName == null)
		{
			throw new ArgumentNullException("attributeName");
		}
		string text = ResourceKey;
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		try
		{
			object globalResourceObject = HttpContext.GetGlobalResourceObject(provider.ResourceKey, text + "." + attributeName);
			if (globalResourceObject is string)
			{
				return (string)globalResourceObject;
			}
		}
		catch (MissingManifestResourceException)
		{
		}
		return null;
	}

	/// <summary>Creates a new node that is a copy of the current node. For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
	/// <returns>A new node that is a copy of the current node.</returns>
	object ICloneable.Clone()
	{
		return Clone(cloneParentNodes: false);
	}

	/// <summary>Creates a new node that is a copy of the current node.</summary>
	/// <returns>A new node that is a copy of the current node.</returns>
	public virtual SiteMapNode Clone()
	{
		return Clone(cloneParentNodes: false);
	}

	/// <summary>Creates a new copy that is a copy of the current node, optionally cloning all parent and ancestor nodes of the current node.</summary>
	/// <param name="cloneParentNodes">
	///       <see langword="true" /> to clone all parent and ancestor nodes of the current node; otherwise, <see langword="false" />.</param>
	/// <returns>A new node that is a copy of the current node.</returns>
	public virtual SiteMapNode Clone(bool cloneParentNodes)
	{
		SiteMapNode siteMapNode = new SiteMapNode();
		siteMapNode.provider = provider;
		siteMapNode.key = key;
		siteMapNode.url = url;
		siteMapNode.title = title;
		siteMapNode.description = description;
		if (roles != null)
		{
			siteMapNode.roles = new ArrayList(roles);
		}
		if (attributes != null)
		{
			siteMapNode.attributes = new NameValueCollection(attributes);
		}
		if (cloneParentNodes && ParentNode != null)
		{
			siteMapNode.parent = ParentNode.Clone(cloneParentNodes: true);
		}
		return siteMapNode;
	}

	/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.SiteMapNode" /> is identical to the specified object. </summary>
	/// <param name="obj">An object to compare to the current <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="obj" /> is both a <see cref="T:System.Web.SiteMapNode" /> and identical to the current <see cref="T:System.Web.SiteMapNode" />; otherwise, <see langword="false" />. </returns>
	public override bool Equals(object obj)
	{
		if (!(obj is SiteMapNode siteMapNode))
		{
			return false;
		}
		if (siteMapNode.key != key || siteMapNode.url != url || siteMapNode.title != title || siteMapNode.description != description)
		{
			return false;
		}
		if (roles == null || siteMapNode.roles == null)
		{
			if (roles != siteMapNode.roles)
			{
				return false;
			}
		}
		else
		{
			if (roles.Count != siteMapNode.roles.Count)
			{
				return false;
			}
			foreach (object role in roles)
			{
				if (!siteMapNode.roles.Contains(role))
				{
					return false;
				}
			}
		}
		if (attributes == null || siteMapNode.attributes == null)
		{
			if (attributes != siteMapNode.attributes)
			{
				return false;
			}
		}
		else
		{
			if (attributes.Count != siteMapNode.attributes.Count)
			{
				return false;
			}
			foreach (string attribute in attributes)
			{
				if (attributes[attribute] != siteMapNode.attributes[attribute])
				{
					return false;
				}
			}
		}
		return true;
	}

	/// <summary>Returns the hash code of the <see cref="T:System.Web.SiteMapNode" /> object. </summary>
	/// <returns>A 32-bit signed integer representing the hash code.</returns>
	public override int GetHashCode()
	{
		return (key + url + title + description).GetHashCode();
	}

	private void CheckWritable()
	{
		if (readOnly)
		{
			throw new InvalidOperationException("Can't modify read-only node");
		}
	}

	/// <summary>Retrieves the hierarchical children data items of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetChildren" />.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the immediate children of the current item in the hierarchy.</returns>
	IHierarchicalEnumerable IHierarchyData.GetChildren()
	{
		return ChildNodes;
	}

	/// <summary>Retrieves the hierarchical parent of the current item. For a description of this member, see <see cref="M:System.Web.UI.IHierarchyData.GetParent" />.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> that represents the parent of the current item in the hierarchy.</returns>
	IHierarchyData IHierarchyData.GetParent()
	{
		return ParentNode;
	}
}
