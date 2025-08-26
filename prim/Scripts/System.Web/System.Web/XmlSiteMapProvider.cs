using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml;

namespace System.Web;

/// <summary>
///     The <see cref="T:System.Web.XmlSiteMapProvider" /> class is derived from the <see cref="T:System.Web.SiteMapProvider" /> class and is the default site map provider for ASP.NET. The <see cref="T:System.Web.XmlSiteMapProvider" /> class generates site map trees from XML files with the file name extension .sitemap.</summary>
public class XmlSiteMapProvider : StaticSiteMapProvider, IDisposable
{
	private static readonly char[] seperators = new char[2] { ';', ',' };

	private bool initialized;

	private string fileVirtualPath;

	private SiteMapNode root;

	private List<FileSystemWatcher> watchers;

	private Dictionary<string, bool> _childProvidersPresent;

	private List<SiteMapProvider> _childProviders;

	private Dictionary<string, bool> ChildProvidersPresent
	{
		get
		{
			if (_childProvidersPresent == null)
			{
				_childProvidersPresent = new Dictionary<string, bool>();
			}
			return _childProvidersPresent;
		}
	}

	private List<SiteMapProvider> ChildProviders
	{
		get
		{
			if (_childProviders == null)
			{
				_childProviders = new List<SiteMapProvider>();
			}
			return _childProviders;
		}
	}

	/// <summary>Gets the root node of the site map.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the site map; otherwise, <see langword="null" />, if security trimming is enabled and the root node is not accessible to the current user.</returns>
	public override SiteMapNode RootNode
	{
		get
		{
			BuildSiteMap();
			return root;
		}
	}

	/// <summary>Adds a <see cref="T:System.Web.SiteMapNode" /> object to the collections that are maintained by the current provider.</summary>
	/// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> to add to the provider.</param>
	/// <param name="parentNode">The <see cref="T:System.Web.SiteMapNode" /> under which to add <paramref name="node" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> or <paramref name="parentNode" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The current <see cref="T:System.Web.XmlSiteMapProvider" /> is not the provider associated with <paramref name="node" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">A node with the same URL or key is already registered with the <see cref="T:System.Web.XmlSiteMapProvider" />. - or -A duplicate site map node has been encountered programmatically, such as when linking two site map providers.- or -
	///         <paramref name="node" /> is the root node of the <see cref="T:System.Web.XmlSiteMapProvider" />.</exception>
	protected internal override void AddNode(SiteMapNode node, SiteMapNode parentNode)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		if (parentNode == null)
		{
			throw new ArgumentNullException("parentNode");
		}
		SiteMapProvider provider = node.Provider;
		if (provider != this)
		{
			throw new ArgumentException(string.Concat("SiteMapNode '", node, "' cannot be found in current provider, only nodes in the same provider can be added."), "node");
		}
		SiteMapProvider provider2 = parentNode.Provider;
		if (provider != provider2)
		{
			throw new ArgumentException(string.Concat("SiteMapNode '", parentNode, "' cannot be found in current provider, only nodes in the same provider can be added."), "parentNode");
		}
		AddNodeNoCheck(node, parentNode);
	}

	private void AddNodeNoCheck(SiteMapNode node, SiteMapNode parentNode)
	{
		base.AddNode(node, parentNode);
		SiteMapProvider provider = node.Provider;
		if (provider != this)
		{
			RegisterChildProvider(provider.Name, provider);
		}
	}

	/// <summary>Links a child site map provider to the current provider. </summary>
	/// <param name="providerName">The name of one of the <see cref="T:System.Web.SiteMapProvider" /> objects currently registered in the <see cref="P:System.Web.SiteMap.Providers" />.</param>
	/// <param name="parentNode">A site map node of the current site map provider under which the root node and all nodes of the child provider is added.</param>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.SiteMapNode.Provider" /> property of the <paramref name="parentNode" /> does not reference the current provider. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="parentNode" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="providerName" /> cannot be resolved.</exception>
	/// <exception cref="T:System.InvalidOperationException">The site map file used by <paramref name="providerName" /> is already in use within the provider hierarchy. -or-The root node returned by <paramref name="providerName" /> is <see langword="null" />.-or-The root node returned by <paramref name="providerName" /> has a URL or key that is already registered with the parent <see cref="T:System.Web.XmlSiteMapProvider" />.   </exception>
	protected virtual void AddProvider(string providerName, SiteMapNode parentNode)
	{
		if (parentNode == null)
		{
			throw new ArgumentNullException("parentNode");
		}
		if (parentNode.Provider != this)
		{
			throw new ArgumentException("The Provider property of the parentNode does not reference the current provider.", "parentNode");
		}
		SiteMapProvider siteMapProvider = SiteMap.Providers[providerName];
		if (siteMapProvider == null)
		{
			throw new ProviderException("Provider with name [" + providerName + "] was not found.");
		}
		AddNode(siteMapProvider.GetRootNodeCore());
		RegisterChildProvider(providerName, siteMapProvider);
	}

	private void RegisterChildProvider(string name, SiteMapProvider smp)
	{
		Dictionary<string, bool> childProvidersPresent = ChildProvidersPresent;
		if (!childProvidersPresent.ContainsKey(name))
		{
			childProvidersPresent.Add(name, value: true);
			ChildProviders.Add(smp);
		}
	}

	private XmlNode FindStartingNode(string virtualPath, out bool enableLocalization)
	{
		XmlElement documentElement = GetConfigDocument(virtualPath).DocumentElement;
		if (string.Compare("siteMap", documentElement.Name, StringComparison.Ordinal) != 0)
		{
			throw new ConfigurationErrorsException("Top element must be 'siteMap'");
		}
		XmlNode xmlNode = documentElement.Attributes["enableLocalization"];
		if (xmlNode != null && !string.IsNullOrEmpty(xmlNode.Value))
		{
			enableLocalization = (bool)Convert.ChangeType(xmlNode.Value, typeof(bool));
		}
		else
		{
			enableLocalization = false;
		}
		XmlNodeList childNodes = documentElement.ChildNodes;
		XmlNode xmlNode2 = null;
		foreach (XmlNode item in childNodes)
		{
			if (string.Compare("siteMapNode", item.Name, StringComparison.Ordinal) != 0)
			{
				throw new ConfigurationErrorsException("Only <siteMapNode> elements are allowed at the document top level.");
			}
			if (xmlNode2 != null)
			{
				throw new ConfigurationErrorsException("Only one <siteMapNode> element is allowed at the document top level.");
			}
			xmlNode2 = item;
		}
		if (xmlNode2 == null)
		{
			throw new ConfigurationErrorsException("Missing <siteMapNode> element at the document top level.");
		}
		return xmlNode2;
	}

	private XmlDocument GetConfigDocument(string virtualPath)
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentException("The siteMapFile attribute must be specified on the XmlSiteMapProvider");
		}
		string text = HostingEnvironment.MapPath(virtualPath);
		if (text == null)
		{
			throw new HttpException("Virtual path '" + virtualPath + "' cannot be mapped to physical path.");
		}
		if (string.Compare(Path.GetExtension(text), ".sitemap", RuntimeHelpers.StringComparison) != 0)
		{
			throw new InvalidOperationException($"The file {(string.IsNullOrEmpty(virtualPath) ? Path.GetFileName(text) : virtualPath)} has an invalid extension, only .sitemap files are allowed in XmlSiteMapProvider.");
		}
		if (!File.Exists(text))
		{
			throw new InvalidOperationException($"The file '{(string.IsNullOrEmpty(virtualPath) ? Path.GetFileName(text) : virtualPath)}' required by XmlSiteMapProvider does not exist.");
		}
		base.ResourceKey = Path.GetFileName(text);
		CreateWatcher(text);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.Load(text);
		return xmlDocument;
	}

	/// <summary>Loads the site map information from an XML file and builds it in memory.</summary>
	/// <returns>Returns the root <see cref="T:System.Web.SiteMapNode" /> of the site map navigation structure.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.XmlSiteMapProvider" /> was not initialized properly.- or -A <see langword="siteMapFile" /> is parsed for a <see langword="&lt;siteMapNode&gt;" /> that is not unique.- or -The file specified by the <see langword="siteMapFile" /> does not have the file name extension .sitemap.- or -The file specified by the <see langword="siteMapFile" /> does not exist.- or -A provider configured in the <see langword="provider" /> of a <see langword="&lt;siteMapNode&gt;" /> returns a null root node. </exception>
	/// <exception cref="T:System.ArgumentException">The <see langword="siteMapFile" /> is specified but the path lies outside the current directory structure for the application.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">There is an error loading the configuration file.- or -The top element of the configuration file is not <see langword="named &lt;siteMap&gt;" />.- or - More than one top node exists in the configuration file.- or -A child of the <see langword="&lt;siteMap&gt;" /> has a name other than <see langword="&lt;siteMapNode&gt;" />. - or -An unexpected attribute is parsed for the <see langword="&lt;siteMapNode&gt;" />.- or -Sub-elements are nested beneath a <see langword="&lt;siteMapNode&gt;" /> where the <see langword="provider" /> is set.- or -The <see langword="roles" /> of the <see langword="&lt;siteMapNode&gt;" /> contain characters that are not valid.- or - A <see langword="url" /> is parsed for a <see langword="&lt;siteMapNode&gt;" /> that is not unique.- or - A <see cref="T:System.Web.SiteMapNode" /> was encountered with a duplicate value for <see cref="P:System.Web.SiteMapNode.Key" />. - or -The <see cref="P:System.Web.SiteMapNode.ResourceKey" /> or <see cref="P:System.Web.SiteMapNode.Title" /> was specified on a <see cref="T:System.Web.SiteMapNode" /> or a <see langword="custom" /> attribute defined for the node contained an explicit resource expression.- or -An explicit resource expression was applied either to the <see cref="P:System.Web.SiteMapNode.Title" /> or <see cref="P:System.Web.SiteMapNode.Description" /> or to a <see langword="custom" /> attribute of a <see cref="T:System.Web.SiteMapNode" /> but the explicit information was not valid.- or -An error occurred while parsing the <see cref="P:System.Web.SiteMapNode.Url" /> of a <see cref="T:System.Web.SiteMapNode" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">A named provider cannot be found in the current site map providers collection. </exception>
	/// <exception cref="T:System.ArgumentNullException">A <see langword="&lt;siteMapNode&gt;" /> referencing a site map file contains an empty string for the <see langword="siteMapFile" />.</exception>
	/// <exception cref="T:System.Web.HttpException">A <see langword="siteMapFile" /> of a <see langword="&lt;siteMapNode&gt;" /> uses a physical path.- or -An error occurred while attempting to parse the virtual path to the file specified in the <see langword="siteMapFile" />.</exception>
	public override SiteMapNode BuildSiteMap()
	{
		if (root != null)
		{
			return root;
		}
		lock (this_lock)
		{
			if (root != null)
			{
				return root;
			}
			Clear();
			bool flag;
			XmlNode xmlNode = FindStartingNode(fileVirtualPath, out flag);
			base.EnableLocalization = flag;
			BuildSiteMapRecursive(xmlNode, null);
			return root;
		}
	}

	private SiteMapNode ConvertToSiteMapNode(XmlNode xmlNode)
	{
		bool flag = base.EnableLocalization;
		string optionalAttribute = GetOptionalAttribute(xmlNode, "url");
		string title = GetOptionalAttribute(xmlNode, "title");
		string description = GetOptionalAttribute(xmlNode, "description");
		string optionalAttribute2 = GetOptionalAttribute(xmlNode, "roles");
		string optionalAttribute3 = GetOptionalAttribute(xmlNode, "resourceKey");
		List<string> list = new List<string>();
		if (optionalAttribute2 != null && optionalAttribute2.Length > 0)
		{
			string[] array = optionalAttribute2.Split(seperators);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (text.Length > 0)
				{
					list.Add(text);
				}
			}
		}
		optionalAttribute = MapUrl(optionalAttribute);
		NameValueCollection attributes = null;
		NameValueCollection explicitResourceKeys = null;
		if (flag)
		{
			CollectLocalizationInfo(xmlNode, ref title, ref description, ref attributes, ref explicitResourceKeys);
		}
		else
		{
			foreach (XmlNode attribute in xmlNode.Attributes)
			{
				PutInCollection(attribute.Name, attribute.Value, ref attributes);
			}
		}
		string key = Guid.NewGuid().ToString();
		return new SiteMapNode(this, key, optionalAttribute, title, description, list.AsReadOnly(), attributes, explicitResourceKeys, optionalAttribute3);
	}

	private void BuildSiteMapRecursive(XmlNode xmlNode, SiteMapNode parent)
	{
		if (xmlNode.Name != "siteMapNode")
		{
			throw new ConfigurationException("incorrect element name", xmlNode);
		}
		string nonEmptyOptionalAttribute = GetNonEmptyOptionalAttribute(xmlNode, "provider");
		if (nonEmptyOptionalAttribute != null)
		{
			SiteMapProvider obj = SiteMap.Providers[nonEmptyOptionalAttribute] ?? throw new ProviderException("Provider with name [" + nonEmptyOptionalAttribute + "] was not found.");
			obj.ParentProvider = this;
			SiteMapNode rootNodeCore = obj.GetRootNodeCore();
			if (parent == null)
			{
				root = rootNodeCore;
			}
			else
			{
				AddNodeNoCheck(rootNodeCore, parent);
			}
			return;
		}
		nonEmptyOptionalAttribute = GetNonEmptyOptionalAttribute(xmlNode, "siteMapFile");
		if (nonEmptyOptionalAttribute != null)
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("siteMapFile", nonEmptyOptionalAttribute);
			string optionalAttribute = GetOptionalAttribute(xmlNode, "description");
			if (!string.IsNullOrEmpty(optionalAttribute))
			{
				nameValueCollection.Add("description", optionalAttribute);
			}
			string name = MapUrl(nonEmptyOptionalAttribute);
			XmlSiteMapProvider xmlSiteMapProvider = new XmlSiteMapProvider();
			xmlSiteMapProvider.Initialize(name, nameValueCollection);
			SiteMapNode rootNodeCore2 = xmlSiteMapProvider.GetRootNodeCore();
			if (parent == null)
			{
				root = rootNodeCore2;
			}
			else
			{
				AddNodeNoCheck(rootNodeCore2, parent);
			}
			return;
		}
		SiteMapNode siteMapNode = ConvertToSiteMapNode(xmlNode);
		if (parent == null)
		{
			root = siteMapNode;
		}
		else
		{
			AddNodeNoCheck(siteMapNode, parent);
		}
		XmlNodeList childNodes = xmlNode.ChildNodes;
		if (childNodes == null || childNodes.Count < 1)
		{
			return;
		}
		foreach (XmlNode item in childNodes)
		{
			if (item.NodeType == XmlNodeType.Element)
			{
				BuildSiteMapRecursive(item, siteMapNode);
			}
		}
	}

	private string GetNonEmptyOptionalAttribute(XmlNode n, string name)
	{
		return HandlersUtil.ExtractAttributeValue(name, n, optional: true);
	}

	private string GetOptionalAttribute(XmlNode n, string name)
	{
		return HandlersUtil.ExtractAttributeValue(name, n, optional: true, allowEmpty: true);
	}

	private void PutInCollection(string name, string value, ref NameValueCollection coll)
	{
		PutInCollection(name, null, value, ref coll);
	}

	private void PutInCollection(string name, string classKey, string value, ref NameValueCollection coll)
	{
		if (coll == null)
		{
			coll = new NameValueCollection();
		}
		if (!string.IsNullOrEmpty(classKey))
		{
			coll.Add(name, classKey);
		}
		coll.Add(name, value);
	}

	private bool GetAttributeLocalization(string value, out string resClass, out string resKey, out string resDefault)
	{
		resClass = null;
		resKey = null;
		resDefault = null;
		if (string.IsNullOrEmpty(value))
		{
			return false;
		}
		string text = value.TrimStart(' ', '\t');
		if (text.Length < 11 || string.Compare(text, 0, "$resources:", 0, 11, StringComparison.InvariantCultureIgnoreCase) != 0)
		{
			return false;
		}
		text = text.Substring(11);
		if (text.Length == 0)
		{
			return false;
		}
		string[] array = text.Split(',');
		if (array.Length < 2)
		{
			return false;
		}
		resClass = array[0].Trim();
		resKey = array[1].Trim();
		if (array.Length == 3)
		{
			resDefault = array[2];
		}
		else if (array.Length > 3)
		{
			resDefault = string.Join(",", array, 2, array.Length - 2);
		}
		return true;
	}

	private void CollectLocalizationInfo(XmlNode xmlNode, ref string title, ref string description, ref NameValueCollection attributes, ref NameValueCollection explicitResourceKeys)
	{
		if (GetAttributeLocalization(title, out var resClass, out var resKey, out var resDefault))
		{
			PutInCollection("title", resClass, resKey, ref explicitResourceKeys);
			title = resDefault;
		}
		if (GetAttributeLocalization(description, out resClass, out resKey, out resDefault))
		{
			PutInCollection("description", resClass, resKey, ref explicitResourceKeys);
			description = resDefault;
		}
		foreach (XmlNode attribute in xmlNode.Attributes)
		{
			string value;
			if (GetAttributeLocalization(attribute.Value, out resClass, out resKey, out resDefault))
			{
				PutInCollection(attribute.Name, resClass, resKey, ref explicitResourceKeys);
				value = resDefault;
			}
			else
			{
				value = attribute.Value;
			}
			PutInCollection(attribute.Name, value, ref attributes);
		}
	}

	/// <summary>Removes all elements in the collections of child and parent site map nodes and site map providers that the <see cref="T:System.Web.XmlSiteMapProvider" /> object internally tracks as part of its state.</summary>
	protected override void Clear()
	{
		base.Clear();
		root = null;
		ChildProviders.Clear();
		ChildProvidersPresent.Clear();
	}

	/// <summary>Notifies the file monitor of the Web.sitemap file that the <see cref="T:System.Web.XmlSiteMapProvider" /> object no longer requires the file to be monitored. The <see cref="M:System.Web.XmlSiteMapProvider.Dispose(System.Boolean)" /> method takes a Boolean parameter indicating whether the method is called by user code.</summary>
	/// <param name="disposing">
	///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}
		foreach (FileSystemWatcher watcher in watchers)
		{
			watcher.Dispose();
		}
		watchers = null;
	}

	/// <summary>Notifies the file monitor of the Web.sitemap file that the <see cref="T:System.Web.XmlSiteMapProvider" /> object no longer requires the file to be monitored.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
	}

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.</summary>
	/// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />. </param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">A child provider linked to the current site map provider returned a node that is not valid.</exception>
	public override SiteMapNode FindSiteMapNode(string rawUrl)
	{
		SiteMapNode siteMapNode = base.FindSiteMapNode(rawUrl);
		if (siteMapNode != null)
		{
			return siteMapNode;
		}
		siteMapNode = RootNode;
		string text = MapUrl(rawUrl);
		if (siteMapNode != null && string.Compare(text, siteMapNode.Url, RuntimeHelpers.StringComparison) == 0)
		{
			return siteMapNode;
		}
		foreach (SiteMapProvider childProvider in ChildProviders)
		{
			siteMapNode = childProvider.FindSiteMapNode(text);
			if (siteMapNode != null)
			{
				return siteMapNode;
			}
		}
		return null;
	}

	/// <summary>Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.</summary>
	/// <param name="key">A lookup key with which to search for a <see cref="T:System.Web.SiteMapNode" />.</param>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, <see langword="null" />, if security trimming is enabled and the node cannot be shown to the current user or the node is not found by <paramref name="key" /> in the node collection.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">A child provider linked to the current site map provider returned a node that is not valid.</exception>
	public override SiteMapNode FindSiteMapNodeFromKey(string key)
	{
		SiteMapNode siteMapNode = base.FindSiteMapNodeFromKey(key);
		if (siteMapNode != null)
		{
			return siteMapNode;
		}
		foreach (SiteMapProvider childProvider in ChildProviders)
		{
			siteMapNode = childProvider.FindSiteMapNodeFromKey(key);
			if (siteMapNode != null)
			{
				return siteMapNode;
			}
		}
		return null;
	}

	/// <summary>Initializes the <see cref="T:System.Web.XmlSiteMapProvider" /> object. The <see cref="M:System.Web.XmlSiteMapProvider.Initialize(System.String,System.Collections.Specialized.NameValueCollection)" /> method does not actually build a site map, it only prepares the state of the <see cref="T:System.Web.XmlSiteMapProvider" /> to do so.</summary>
	/// <param name="name">The <see cref="T:System.Web.XmlSiteMapProvider" /> to initialize. </param>
	/// <param name="attributes">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that can contain additional attributes to help initialize <paramref name="name" />. These attributes are read from the <see cref="T:System.Web.XmlSiteMapProvider" /> configuration in the Web.config file. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.XmlSiteMapProvider" /> is initialized more than once.</exception>
	/// <exception cref="T:System.Web.HttpException">A <see cref="T:System.Web.SiteMapNode" /> used a physical path to reference a site map file.- or -An error occurred while attempting to parse the virtual path supplied for the <see langword="siteMapFile" /> attribute.</exception>
	public override void Initialize(string name, NameValueCollection attributes)
	{
		if (initialized)
		{
			throw new InvalidOperationException("XmlSiteMapProvider cannot be initialized twice.");
		}
		initialized = true;
		if (attributes != null)
		{
			string[] allKeys = attributes.AllKeys;
			foreach (string text in allKeys)
			{
				switch (text)
				{
				case "siteMapFile":
					fileVirtualPath = MapUrl(attributes["siteMapFile"]);
					continue;
				case "description":
				case "securityTrimmingEnabled":
					continue;
				}
				throw new ConfigurationErrorsException("The attribute '" + text + "' is unexpected in the configuration of the '" + name + "' provider.");
			}
		}
		base.Initialize(name, (attributes != null) ? attributes : new NameValueCollection());
	}

	private void CreateWatcher(string file)
	{
		FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();
		fileSystemWatcher.NotifyFilter |= NotifyFilters.Size;
		fileSystemWatcher.Path = Path.GetFullPath(Path.GetDirectoryName(file));
		fileSystemWatcher.Filter = Path.GetFileName(file);
		fileSystemWatcher.Changed += OnFileChanged;
		fileSystemWatcher.EnableRaisingEvents = true;
		if (watchers == null)
		{
			watchers = new List<FileSystemWatcher>();
		}
		watchers.Add(fileSystemWatcher);
	}

	/// <summary>Removes the specified <see cref="T:System.Web.SiteMapNode" /> object from all node collections that are tracked by the provider.</summary>
	/// <param name="node">The node to remove from the node collections.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="node" /> is the root node of the site map provider that owns it.- or -
	///         <paramref name="node" /> is not managed by the provider or by a provider in the chain of parent providers for this provider.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="node" /> is <see langword="null" />. </exception>
	protected override void RemoveNode(SiteMapNode node)
	{
		base.RemoveNode(node);
	}

	/// <summary>Removes a linked child site map provider from the hierarchy for the current provider.</summary>
	/// <param name="providerName">The name of one of the <see cref="T:System.Web.SiteMapProvider" /> objects currently registered in the <see cref="P:System.Web.SiteMap.Providers" />.</param>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="providerName" /> cannot be resolved.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="providerName" /> is not a registered child provider of the current site map provider.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerName" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	protected virtual void RemoveProvider(string providerName)
	{
		throw new NotImplementedException();
	}

	private void OnFileChanged(object sender, FileSystemEventArgs args)
	{
		Clear();
	}

	/// <summary>Retrieves the top-level node of the current site map data structure.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the top-level node in the current site map data structure.</returns>
	protected internal override SiteMapNode GetRootNodeCore()
	{
		return BuildSiteMap();
	}

	/// <summary>Creates a new, unnamed, instance of the <see cref="T:System.Web.XmlSiteMapProvider" /> class.</summary>
	public XmlSiteMapProvider()
	{
	}
}
