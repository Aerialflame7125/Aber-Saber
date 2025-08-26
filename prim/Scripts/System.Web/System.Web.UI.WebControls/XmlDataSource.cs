using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Caching;
using System.Xml;
using System.Xml.Xsl;

namespace System.Web.UI.WebControls;

/// <summary>Represents an XML data source to data-bound controls.</summary>
[Designer("System.Web.UI.Design.WebControls.XmlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("DataFile")]
[DefaultEvent("Transforming")]
[ParseChildren(true)]
[PersistChildren(false)]
[WebSysDescription("Connect to an XML file.")]
[ToolboxBitmap("")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class XmlDataSource : HierarchicalDataSourceControl, IDataSource, IListSource
{
	private string _data = string.Empty;

	private string _transform = string.Empty;

	private string _xpath = string.Empty;

	private string _dataFile = string.Empty;

	private string _transformFile = string.Empty;

	private string _cacheKeyDependency = string.Empty;

	private bool _enableCaching = true;

	private int _cacheDuration;

	private bool _documentNeedsUpdate;

	private DataSourceCacheExpiry _cacheExpirationPolicy;

	private static readonly string[] emptyNames = new string[1] { "DefaultView" };

	private static object EventTransforming = new object();

	private XmlDocument xmlDocument;

	private XsltArgumentList transformArgumentList;

	private Cache DataCache
	{
		get
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.InternalCache;
			}
			return null;
		}
	}

	private bool CanBeSaved
	{
		get
		{
			if (Transform == string.Empty && TransformFile == string.Empty)
			{
				return DataFile != string.Empty;
			}
			return false;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the collection is a collection of <see cref="T:System.Collections.IList" /> objects; otherwise, <see langword="false" />.</returns>
	bool IListSource.ContainsListCollection => ListSourceHelper.ContainsListCollection(this);

	/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data it has retrieved.</summary>
	/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control caches the results of a data retrieval operation. The default value is 0.</returns>
	[DefaultValue(0)]
	[TypeConverter(typeof(DataSourceCacheDurationConverter))]
	public virtual int CacheDuration
	{
		get
		{
			return _cacheDuration;
		}
		set
		{
			_cacheDuration = value;
		}
	}

	/// <summary>Gets or sets the cache expiration policy that is combined with the cache duration to describe the caching behavior of the cache that the data source control uses.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default cache expiration policy setting is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
	[DefaultValue(DataSourceCacheExpiry.Absolute)]
	public virtual DataSourceCacheExpiry CacheExpirationPolicy
	{
		get
		{
			return _cacheExpirationPolicy;
		}
		set
		{
			_cacheExpirationPolicy = value;
		}
	}

	/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects created by the data source control. All cache objects explicitly expire when the key expires.</summary>
	/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
	[DefaultValue("")]
	public virtual string CacheKeyDependency
	{
		get
		{
			return _cacheKeyDependency;
		}
		set
		{
			_cacheKeyDependency = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control has data caching enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if data caching is enabled for the data source control; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool EnableCaching
	{
		get
		{
			return _enableCaching;
		}
		set
		{
			_enableCaching = value;
		}
	}

	/// <summary>Gets or sets a block of XML data that the data source control binds to.</summary>
	/// <returns>A string of inline XML data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control binds to. The default value is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("Inline XML data.")]
	[WebCategory("Data")]
	[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(MultilineStringConverter))]
	public virtual string Data
	{
		get
		{
			return _data;
		}
		set
		{
			if (_data != value)
			{
				_data = value;
				_documentNeedsUpdate = true;
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Specifies the file name of an XML file that the data source binds to.</summary>
	/// <returns>The absolute physical path or relative path of the XML file that contains data that the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control represents. The default value is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.XmlDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
	public virtual string DataFile
	{
		get
		{
			return _dataFile;
		}
		set
		{
			if (_dataFile != value)
			{
				_dataFile = value;
				_documentNeedsUpdate = true;
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Provides a list of XSLT arguments that are used with the style sheet defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> properties to perform a transformation on the XML data.</summary>
	/// <returns>An <see cref="T:System.Xml.Xsl.XsltArgumentList" /> object that contains XSLT parameters and objects to be applied to XML data when it is loaded by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	public virtual XsltArgumentList TransformArgumentList
	{
		get
		{
			return transformArgumentList;
		}
		set
		{
			transformArgumentList = value;
		}
	}

	/// <summary>Gets or sets a block of Extensible Stylesheet Language (XSL) data that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	/// <returns>A string of inline XSL that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[TypeConverter(typeof(MultilineStringConverter))]
	public virtual string Transform
	{
		get
		{
			return _transform;
		}
		set
		{
			if (_transform != value)
			{
				_transform = value;
				_documentNeedsUpdate = true;
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Specifies the file name of an Extensible Stylesheet Language (XSL) file (.xsl) that defines an XSLT transformation to be performed on the XML data managed by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
	/// <returns>The absolute physical path or relative path of the XSL style sheet file that defines an XML transformation to be performed on the data contained in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> or <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> properties. The default value is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
	[Editor("System.Web.UI.Design.XslTransformFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[MonoLimitation("Absolute path to the file system is not supported; use a relative URI instead.")]
	public virtual string TransformFile
	{
		get
		{
			return _transformFile;
		}
		set
		{
			if (_transformFile != value)
			{
				_transformFile = value;
				_documentNeedsUpdate = true;
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Specifies an XPath expression to be applied to the XML data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</summary>
	/// <returns>A string that represents an XPath expression that can be used to filter the data contained by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or by the XML file indicated by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The document is loading.</exception>
	[DefaultValue("")]
	public virtual string XPath
	{
		get
		{
			return _xpath;
		}
		set
		{
			if (_xpath != value)
			{
				_xpath = value;
				OnDataSourceChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the value of the cache key for the data source control from view state, or adds the cache key to view state.</summary>
	/// <returns>The value of the cache key, or an empty string if the cache key is not in view state.</returns>
	[DefaultValue("")]
	public virtual string CacheKeyContext
	{
		get
		{
			return ViewState.GetString("CacheKeyContext", string.Empty);
		}
		set
		{
			ViewState["CacheKeyContext"] = value;
		}
	}

	/// <summary>For a description of this member, see <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" />.</summary>
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

	/// <summary>Occurs before the style sheet that is defined by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Transform" /> property or identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.TransformFile" /> property is applied to XML data.</summary>
	public event EventHandler Transforming
	{
		add
		{
			base.Events.AddHandler(EventTransforming, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventTransforming, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.XmlDataSource.Transforming" /> event before the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control performs an XSLT transformation on its XML data.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnTransforming(EventArgs e)
	{
		if (base.Events[EventTransforming] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Loads the XML data into memory, either directly from the underlying data storage or from the cache, and returns it in the form of an <see cref="T:System.Xml.XmlDataDocument" /> object.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlDataDocument" /> object that represents the XML specified in the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property or in the file identified by the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property, with any transformations and <see cref="P:System.Web.UI.WebControls.XmlDataSource.XPath" /> queries applied.</returns>
	/// <exception cref="T:System.InvalidOperationException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
	/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not support caching when client impersonation is enabled.</exception>
	/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
	public XmlDocument GetXmlDocument()
	{
		if (_documentNeedsUpdate)
		{
			UpdateXml();
		}
		if (xmlDocument == null && EnableCaching)
		{
			xmlDocument = GetXmlDocumentFromCache();
		}
		if (xmlDocument == null)
		{
			xmlDocument = LoadXmlDocument();
			UpdateCache();
		}
		return xmlDocument;
	}

	[MonoTODO("schema")]
	private XmlDocument LoadXmlDocument()
	{
		XmlDocument xmlDocument = LoadFileOrData(DataFile, Data);
		if (string.IsNullOrEmpty(TransformFile) && string.IsNullOrEmpty(Transform))
		{
			return xmlDocument;
		}
		XslTransform xslTransform = new XslTransform();
		XmlDocument stylesheet = LoadFileOrData(TransformFile, Transform);
		xslTransform.Load(stylesheet);
		OnTransforming(EventArgs.Empty);
		XmlDocument obj = new XmlDocument();
		obj.Load(xslTransform.Transform(xmlDocument, TransformArgumentList));
		return obj;
	}

	private XmlDocument LoadFileOrData(string filename, string data)
	{
		XmlDocument xmlDocument = new XmlDocument();
		if (!string.IsNullOrEmpty(filename))
		{
			if (Uri.TryCreate(filename, UriKind.Absolute, out var _))
			{
				xmlDocument.Load(filename);
			}
			else
			{
				xmlDocument.Load(MapPathSecure(filename));
			}
		}
		else if (!string.IsNullOrEmpty(data))
		{
			xmlDocument.LoadXml(data);
		}
		return xmlDocument;
	}

	private XmlDocument GetXmlDocumentFromCache()
	{
		if (DataCache != null)
		{
			return (XmlDocument)DataCache[GetDataKey()];
		}
		return null;
	}

	private string GetDataKey()
	{
		if (string.IsNullOrEmpty(DataFile) && !string.IsNullOrEmpty(Data))
		{
			string cacheKeyContext = CacheKeyContext;
			if (!string.IsNullOrEmpty(cacheKeyContext))
			{
				return cacheKeyContext;
			}
		}
		Page page = Page;
		string text = ((page != null) ? page.ToString() : "NullPage");
		return TemplateSourceDirectory + "_" + text + "_" + ID;
	}

	private void UpdateCache()
	{
		if (!EnableCaching || DataCache == null)
		{
			return;
		}
		string dataKey = GetDataKey();
		if (DataCache[dataKey] != null)
		{
			DataCache.Remove(dataKey);
		}
		DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;
		TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
		if (CacheDuration > 0)
		{
			if (CacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
			{
				absoluteExpiration = DateTime.Now.AddSeconds(CacheDuration);
			}
			else
			{
				slidingExpiration = new TimeSpan((long)CacheDuration * 10000L);
			}
		}
		CacheDependency cacheDependency = null;
		cacheDependency = ((CacheKeyDependency.Length <= 0) ? new CacheDependency(new string[0], new string[0]) : new CacheDependency(new string[0], new string[1] { CacheKeyDependency }));
		DataCache.Add(dataKey, xmlDocument, cacheDependency, absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null);
	}

	private void UpdateXml()
	{
		xmlDocument = LoadXmlDocument();
		UpdateCache();
		_documentNeedsUpdate = false;
	}

	/// <summary>Saves the XML data currently held in memory by the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control to disk if the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property is set.</summary>
	/// <exception cref="T:System.InvalidOperationException">XML data was loaded using the <see cref="P:System.Web.UI.WebControls.XmlDataSource.Data" /> property instead of the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property. - or -A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control does not have the correct permissions for the Web resource.</exception>
	/// <exception cref="T:System.NotSupportedException">A URL is specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property; however, it is not an HTTP-based URL. - or -A design-time relative path was not mapped correctly by the designer before using the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</exception>
	/// <exception cref="T:System.Web.HttpException">Access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.XmlDataSource.DataFile" /> property.</exception>
	public void Save()
	{
		if (!CanBeSaved)
		{
			throw new InvalidOperationException();
		}
		if (xmlDocument != null)
		{
			xmlDocument.Save(MapPathSecure(DataFile));
		}
	}

	/// <summary>Gets the data source view object for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control. The <paramref name="viewPath" /> parameter can be an XPath expression.</summary>
	/// <param name="viewPath">An XPath expression that identifies a node from which the current hierarchical view is built. </param>
	/// <returns>Returns an <see cref="T:System.Web.UI.WebControls.XmlHierarchicalDataSourceView" /> object that represents a single view of the data starting with the data node identified by <paramref name="viewPath" />.</returns>
	protected override HierarchicalDataSourceView GetHierarchicalView(string viewPath)
	{
		XmlNode xmlNode = GetXmlDocument();
		XmlNodeList nodeList = null;
		if (string.IsNullOrEmpty(viewPath))
		{
			nodeList = (string.IsNullOrEmpty(XPath) ? xmlNode.ChildNodes : xmlNode.SelectNodes(XPath));
		}
		else
		{
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(viewPath);
			if (xmlNode2 != null)
			{
				nodeList = xmlNode2.ChildNodes;
			}
		}
		return new XmlHierarchicalDataSourceView(nodeList);
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
	/// <returns>An object implementing <see cref="T:System.Collections.IList" /> that can be bound to a data source.</returns>
	IList IListSource.GetList()
	{
		return ListSourceHelper.GetList(this);
	}

	/// <summary>Gets the named data source view associated with the data source control.</summary>
	/// <param name="viewName">The name of the view to retrieve. If <see cref="F:System.String.Empty" /> is specified, the default view for the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control is retrieved. </param>
	/// <returns>Returns the named <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</returns>
	DataSourceView IDataSource.GetView(string viewName)
	{
		if (string.IsNullOrEmpty(viewName))
		{
			viewName = "DefaultView";
		}
		return new XmlDataSourceView(this, viewName);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IDataSource.GetViewNames" />.</summary>
	/// <returns>An object implementing <see cref="T:System.Collections.ICollection" /> containing names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> object.</returns>
	ICollection IDataSource.GetViewNames()
	{
		return emptyNames;
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> class.</summary>
	public XmlDataSource()
	{
	}
}
