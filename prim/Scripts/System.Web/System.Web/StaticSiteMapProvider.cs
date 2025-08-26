using System.Collections.Generic;
using System.Web.Util;

namespace System.Web;

/// <summary>Serves as a partial implementation of the abstract <see cref="T:System.Web.SiteMapProvider" /> class and serves as a base class for the <see cref="T:System.Web.XmlSiteMapProvider" /> class, which is the default site map provider in ASP.NET. </summary>
public abstract class StaticSiteMapProvider : SiteMapProvider
{
	private Dictionary<string, SiteMapNode> keyToNode;

	private Dictionary<SiteMapNode, SiteMapNode> nodeToParent;

	private Dictionary<SiteMapNode, SiteMapNodeCollection> nodeToChildren;

	private Dictionary<string, SiteMapNode> urlToNode;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.StaticSiteMapProvider" /> class. </summary>
	protected StaticSiteMapProvider()
	{
		keyToNode = new Dictionary<string, SiteMapNode>();
		nodeToParent = new Dictionary<SiteMapNode, SiteMapNode>();
		nodeToChildren = new Dictionary<SiteMapNode, SiteMapNodeCollection>();
		urlToNode = new Dictionary<string, SiteMapNode>(StringComparer.InvariantCultureIgnoreCase);
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> to the collections that are maintained by the site map provider and establishes a parent/child relationship between the <see cref="T:System.Web.SiteMapNode" /> objects.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the site map provider. </param>
	/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> under which to add <paramref name="node" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.SiteMapNode.Url" /> or <see cref="P:System.Web.SiteMapNode.Key" /> is already registered with the <see cref="T:System.Web.StaticSiteMapProvider" />. A site map node must be made up of pages with unique URLs or keys. </exception>
	protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		lock (this_lock)
		{
			string key = node.Key;
			if (FindSiteMapNodeFromKey(key) != null && node.Provider == this)
			{
				throw new InvalidOperationException($"A node with key '{key}' already exists.");
			}
			string url = node.Url;
			if (!string.IsNullOrEmpty(url))
			{
				string text = MapUrl(url);
				SiteMapNode siteMapNode = FindSiteMapNode(text);
				if (siteMapNode != null && string.Compare(siteMapNode.Url, text, RuntimeHelpers.StringComparison) == 0)
				{
					throw new InvalidOperationException($"Multiple nodes with the same URL '{node.Url}' were found. StaticSiteMapProvider requires that sitemap nodes have unique URLs.");
				}
				urlToNode.Add(text, node);
			}
			keyToNode.Add(key, node);
			if (node != RootNode)
			{
				if (parentNode == null)
				{
					parentNode = RootNode;
				}
				nodeToParent.Add(node, parentNode);
				if (!nodeToChildren.TryGetValue(parentNode, out var value))
				{
					nodeToChildren.Add(parentNode, value = new SiteMapNodeCollection());
				}
				value.Add(node);
			}
		}
	}

	/// <summary>Removes all elements in the collections of child and parent site map nodes that the <see cref="T:System.Web.StaticSiteMapProvider" /> tracks as part of its state.</summary>
	protected virtual void Clear()
	{
		lock (this_lock)
		{
			urlToNode.Clear();
			nodeToChildren.Clear();
			nodeToParent.Clear();
			keyToNode.Clear();
		}
	}

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
	/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, <see langword="null" />, if no corresponding site map node is found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="rawURL" /> is <see langword="null" />. </exception>
	public override SiteMapNode FindSiteMapNode(string rawUrl)
	{
		if (rawUrl == null)
		{
			throw new ArgumentNullException("rawUrl");
		}
		if (rawUrl == string.Empty)
		{
			return null;
		}
		BuildSiteMap();
		if (VirtualPathUtility.IsAppRelative(rawUrl))
		{
			rawUrl = VirtualPathUtility.ToAbsolute(rawUrl, HttpRuntime.AppDomainAppVirtualPath, normalize: false);
		}
		if (!urlToNode.TryGetValue(rawUrl, out var value))
		{
			return null;
		}
		return CheckAccessibility(value);
	}

	/// <summary>Retrieves the child site map nodes of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child site map nodes. </param>
	/// <returns>A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the child site map nodes of <paramref name="node" />. If security trimming is enabled, the collection contains only site map nodes that the user is permitted to see.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />. </exception>
	public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		BuildSiteMap();
		if (!nodeToChildren.TryGetValue(node, out var value))
		{
			return SiteMapNodeCollection.EmptyCollection;
		}
		SiteMapNodeCollection siteMapNodeCollection = null;
		for (int i = 0; i < value.Count; i++)
		{
			if (!IsAccessibleToUser(HttpContext.Current, value[i]))
			{
				if (siteMapNodeCollection == null)
				{
					siteMapNodeCollection = new SiteMapNodeCollection();
					for (int j = 0; j < i; j++)
					{
						siteMapNodeCollection.Add(value[j]);
					}
				}
			}
			else
			{
				siteMapNodeCollection?.Add(value[i]);
			}
		}
		if (siteMapNodeCollection == null)
		{
			return SiteMapNodeCollection.ReadOnly(value);
		}
		if (siteMapNodeCollection.Count > 0)
		{
			return SiteMapNodeCollection.ReadOnly(siteMapNodeCollection);
		}
		return SiteMapNodeCollection.EmptyCollection;
	}

	/// <summary>Retrieves the parent site map node of a specific <see cref="T:System.Web.SiteMapNode" /> object.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent site map node. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, <see langword="null" />, if no parent site map node exists or the user is not permitted to see the parent site map node.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />. </exception>
	public override SiteMapNode GetParentNode(SiteMapNode node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		BuildSiteMap();
		nodeToParent.TryGetValue(node, out var value);
		return CheckAccessibility(value);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from all site map node collections that are tracked by the site map provider.</summary>
	/// <param name="node">The site map node to remove from the site map node collections. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />. </exception>
	protected override void RemoveNode(SiteMapNode node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		string key = node.Key;
		lock (this_lock)
		{
			if (keyToNode.ContainsKey(key))
			{
				keyToNode.Remove(key);
			}
			string url = node.Url;
			if (!string.IsNullOrEmpty(url))
			{
				url = MapUrl(url);
				if (urlToNode.ContainsKey(url))
				{
					urlToNode.Remove(url);
				}
			}
			if (node != RootNode && nodeToParent.TryGetValue(node, out var value))
			{
				nodeToParent.Remove(node);
				if (nodeToChildren.ContainsKey(value))
				{
					nodeToChildren[value].Remove(node);
				}
			}
		}
	}

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
	/// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, <see langword="null" />, if security trimming is enabled and the site map node cannot be shown to the current user or the site map node is not found in the site map node collection by <paramref name="key" />. </returns>
	public override SiteMapNode FindSiteMapNodeFromKey(string key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		keyToNode.TryGetValue(key, out var value);
		return CheckAccessibility(value);
	}

	/// <summary>When overridden in a derived class, loads the site map information from persistent storage and builds it in memory.</summary>
	/// <returns>The root <see cref="T:System.Web.SiteMapNode" /> of the site map navigation structure.</returns>
	public abstract SiteMapNode BuildSiteMap();

	private SiteMapNode CheckAccessibility(SiteMapNode node)
	{
		if (node == null || !IsAccessibleToUser(HttpContext.Current, node))
		{
			return null;
		}
		return node;
	}

	internal string MapUrl(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return url;
		}
		string text = HttpRuntime.AppDomainAppVirtualPath;
		if (string.IsNullOrEmpty(text))
		{
			text = "/";
		}
		if (VirtualPathUtility.IsAppRelative(url))
		{
			return VirtualPathUtility.ToAbsolute(url, text, normalize: true);
		}
		return VirtualPathUtility.ToAbsolute(UrlUtils.Combine(text, url), text, normalize: true);
	}
}
