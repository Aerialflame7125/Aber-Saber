using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a business object that provides data to data-bound controls in multitier Web application architectures.</summary>
[DefaultEvent("Selecting")]
[DefaultProperty("TypeName")]
[Designer("System.Web.UI.Design.WebControls.ObjectDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true)]
[PersistChildren(false)]
[ToolboxBitmap("bitmap file goes here")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ObjectDataSource : DataSourceControl
{
	private static readonly string[] emptyNames = new string[1] { "DefaultView" };

	private ObjectDataSourceView defaultView;

	private int cacheDuration;

	private bool enableCaching;

	private string cacheKeyDependency;

	private string sqlCacheDependency;

	private DataSourceCacheManager cache;

	private DataSourceCacheExpiry cacheExpirationPolicy;

	private ObjectDataSourceView DefaultView
	{
		get
		{
			if (defaultView == null)
			{
				defaultView = new ObjectDataSourceView(this, emptyNames[0], Context);
				if (base.IsTrackingViewState)
				{
					((IStateManager)defaultView).TrackViewState();
				}
			}
			return defaultView;
		}
	}

	/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
	/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> caches the results of a <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property invocation. The default is 0. The value cannot be negative.</returns>
	[DefaultValue(0)]
	[TypeConverter(typeof(DataSourceCacheDurationConverter))]
	public virtual int CacheDuration
	{
		get
		{
			return cacheDuration;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", "The duration must be non-negative");
			}
			cacheDuration = value;
		}
	}

	/// <summary>Gets or sets the cache expiration behavior that, when combined with the duration, describes the behavior of the cache that the data source control uses.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" />.</returns>
	[DefaultValue(DataSourceCacheExpiry.Absolute)]
	public virtual DataSourceCacheExpiry CacheExpirationPolicy
	{
		get
		{
			return cacheExpirationPolicy;
		}
		set
		{
			cacheExpirationPolicy = value;
		}
	}

	/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control.</summary>
	/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
	[DefaultValue("")]
	public virtual string CacheKeyDependency
	{
		get
		{
			if (cacheKeyDependency == null)
			{
				return string.Empty;
			}
			return cacheKeyDependency;
		}
		set
		{
			cacheKeyDependency = value;
		}
	}

	/// <summary>Gets or sets a value that determines whether or not just the new values are passed to the <see langword="Update" /> method or both the old and new values are passed to the <see langword="Update" /> method.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" />.</returns>
	[WebCategory("Data")]
	[DefaultValue(ConflictOptions.OverwriteChanges)]
	public ConflictOptions ConflictDetection
	{
		get
		{
			return DefaultView.ConflictDetection;
		}
		set
		{
			DefaultView.ConflictDetection = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from <see langword="null" /> to the <see cref="F:System.DBNull.Value" /> value by the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	/// <returns>
	///     <see langword="true" />, if any <see langword="null" /> values in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control are automatically converted to <see cref="F:System.DBNull.Value" /> values; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public bool ConvertNullToDBNull
	{
		get
		{
			return DefaultView.ConvertNullToDBNull;
		}
		set
		{
			DefaultView.ConvertNullToDBNull = value;
		}
	}

	/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in an update, insert, or delete data operation, instead of passing individual values from the data-bound control.</summary>
	/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
	[WebCategory("Data")]
	[DefaultValue("")]
	public string DataObjectTypeName
	{
		get
		{
			return DefaultView.DataObjectTypeName;
		}
		set
		{
			DefaultView.DataObjectTypeName = value;
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to delete data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to delete data. The default is an empty string ("").</returns>
	[WebCategory("Data")]
	[DefaultValue("")]
	public string DeleteMethod
	{
		get
		{
			return DefaultView.DeleteMethod;
		}
		set
		{
			DefaultView.DeleteMethod = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> method.</returns>
	[WebCategory("Data")]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection DeleteParameters => DefaultView.DeleteParameters;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control has data caching enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if data caching is enabled for the data source control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnableCaching" /> property is set to <see langword="true" /> when the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property returns a <see cref="T:System.Data.Common.DbDataReader" />.</exception>
	[DefaultValue(false)]
	public virtual bool EnableCaching
	{
		get
		{
			return enableCaching;
		}
		set
		{
			enableCaching = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the data source control supports paging through the set of data that it retrieves.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source control supports paging through the data it retrieves; otherwise, <see langword="false" />.</returns>
	[WebCategory("Paging")]
	[DefaultValue(false)]
	public bool EnablePaging
	{
		get
		{
			return DefaultView.EnablePaging;
		}
		set
		{
			DefaultView.EnablePaging = value;
		}
	}

	/// <summary>Gets or sets a filtering expression that is applied when the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property is called.</summary>
	/// <returns>A string that represents a filtering expression that is applied when data is retrieved by using the method or function identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
	[WebCategory("Data")]
	[DefaultValue("")]
	public string FilterExpression
	{
		get
		{
			return DefaultView.FilterExpression;
		}
		set
		{
			DefaultView.FilterExpression = value;
		}
	}

	/// <summary>Gets a collection of parameters that are associated with any parameter placeholders in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> string.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property.</returns>
	/// <exception cref="T:System.NotSupportedException">You set the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.FilterExpression" /> property and the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> method does not return a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />. </exception>
	[WebCategory("Data")]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection FilterParameters => DefaultView.FilterParameters;

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to insert data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to insert data. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public string InsertMethod
	{
		get
		{
			return DefaultView.InsertMethod;
		}
		set
		{
			DefaultView.InsertMethod = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property.</returns>
	[WebCategory("Data")]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection InsertParameters => DefaultView.InsertParameters;

	/// <summary>Gets or sets the name of the business object data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
	/// <returns>The name of the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter that is used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
	[WebCategory("Paging")]
	[DefaultValue("maximumRows")]
	public string MaximumRowsParameterName
	{
		get
		{
			return DefaultView.MaximumRowsParameterName;
		}
		set
		{
			DefaultView.MaximumRowsParameterName = value;
		}
	}

	/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the <see langword="Delete" /> or <see langword="Update" /> methods.</summary>
	/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> or key parameters passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> methods. The default is "{0}", which means the parameter name is the field name.</returns>
	[WebCategory("Data")]
	[DefaultValue("{0}")]
	public string OldValuesParameterFormatString
	{
		get
		{
			return DefaultView.OldValuesParameterFormatString;
		}
		set
		{
			DefaultView.OldValuesParameterFormatString = value;
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve a row count.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve a row count. The method must return an integer (<see cref="T:System.Int32" />). The default is an empty string ("").</returns>
	[WebCategory("Paging")]
	[DefaultValue("")]
	public string SelectCountMethod
	{
		get
		{
			return DefaultView.SelectCountMethod;
		}
		set
		{
			DefaultView.SelectCountMethod = value;
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to retrieve data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to retrieve data. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public string SelectMethod
	{
		get
		{
			return DefaultView.SelectMethod;
		}
		set
		{
			DefaultView.SelectMethod = value;
		}
	}

	/// <summary>Gets a collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</summary>
	/// <returns>A collection of parameters that are used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
	[WebCategory("Data")]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection SelectParameters => DefaultView.SelectParameters;

	/// <summary>Gets or sets the name of the business object that the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> parameter used to specify a sort expression for data source sorting support.</summary>
	/// <returns>The name of the method parameter used to indicate the parameter which is used to sort the data. The default is an empty string.</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public string SortParameterName
	{
		get
		{
			return DefaultView.SortParameterName;
		}
		set
		{
			DefaultView.SortParameterName = value;
		}
	}

	/// <summary>Gets or sets a semicolon-delimited string that indicates which databases and tables to use for the Microsoft SQL Server cache dependency.</summary>
	/// <returns>A string that indicates which databases and tables to use for the SQL Server cache dependency.</returns>
	[MonoTODO("SQLServer specific")]
	[DefaultValue("")]
	public virtual string SqlCacheDependency
	{
		get
		{
			if (sqlCacheDependency == null)
			{
				return string.Empty;
			}
			return sqlCacheDependency;
		}
		set
		{
			sqlCacheDependency = value;
		}
	}

	/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the value of the identifier of the first record to retrieve for data source paging support.</summary>
	/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The parameter must return an integer value. The default is "startRowIndex".</returns>
	[WebCategory("Paging")]
	[DefaultValue("startRowIndex")]
	public string StartRowIndexParameterName
	{
		get
		{
			return DefaultView.StartRowIndexParameterName;
		}
		set
		{
			DefaultView.StartRowIndexParameterName = value;
		}
	}

	/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object represents.</summary>
	/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public string TypeName
	{
		get
		{
			return DefaultView.TypeName;
		}
		set
		{
			DefaultView.TypeName = value;
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control invokes to update data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> uses to update data. The default is an empty string.</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public string UpdateMethod
	{
		get
		{
			return DefaultView.UpdateMethod;
		}
		set
		{
			DefaultView.UpdateMethod = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method that is specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property.</returns>
	[WebCategory("Data")]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection UpdateParameters => DefaultView.UpdateParameters;

	internal DataSourceCacheManager Cache
	{
		get
		{
			if (cache == null)
			{
				cache = new DataSourceCacheManager(CacheDuration, CacheKeyDependency, CacheExpirationPolicy, this, Context);
			}
			return cache;
		}
	}

	/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Deleted
	{
		add
		{
			DefaultView.Deleted += value;
		}
		remove
		{
			DefaultView.Deleted -= value;
		}
	}

	/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Deleting
	{
		add
		{
			DefaultView.Deleting += value;
		}
		remove
		{
			DefaultView.Deleting -= value;
		}
	}

	/// <summary>Occurs before a filter operation.</summary>
	public event ObjectDataSourceFilteringEventHandler Filtering
	{
		add
		{
			DefaultView.Filtering += value;
		}
		remove
		{
			DefaultView.Filtering -= value;
		}
	}

	/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Inserted
	{
		add
		{
			DefaultView.Inserted += value;
		}
		remove
		{
			DefaultView.Inserted -= value;
		}
	}

	/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Inserting
	{
		add
		{
			DefaultView.Inserting += value;
		}
		remove
		{
			DefaultView.Inserting -= value;
		}
	}

	/// <summary>Occurs after the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
	public event ObjectDataSourceObjectEventHandler ObjectCreated
	{
		add
		{
			DefaultView.ObjectCreated += value;
		}
		remove
		{
			DefaultView.ObjectCreated -= value;
		}
	}

	/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is created.</summary>
	public event ObjectDataSourceObjectEventHandler ObjectCreating
	{
		add
		{
			DefaultView.ObjectCreating += value;
		}
		remove
		{
			DefaultView.ObjectCreating -= value;
		}
	}

	/// <summary>Occurs before the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.TypeName" /> property is discarded.</summary>
	public event ObjectDataSourceDisposingEventHandler ObjectDisposing
	{
		add
		{
			DefaultView.ObjectDisposing += value;
		}
		remove
		{
			DefaultView.ObjectDisposing -= value;
		}
	}

	/// <summary>Occurs when a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Selected
	{
		add
		{
			DefaultView.Selected += value;
		}
		remove
		{
			DefaultView.Selected -= value;
		}
	}

	/// <summary>Occurs before a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Select" /> operation.</summary>
	public event ObjectDataSourceSelectingEventHandler Selecting
	{
		add
		{
			DefaultView.Selecting += value;
		}
		remove
		{
			DefaultView.Selecting -= value;
		}
	}

	/// <summary>Occurs when an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Updated
	{
		add
		{
			DefaultView.Updated += value;
		}
		remove
		{
			DefaultView.Updated -= value;
		}
	}

	/// <summary>Occurs before an <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Updating
	{
		add
		{
			DefaultView.Updating += value;
		}
		remove
		{
			DefaultView.Updating -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class.</summary>
	public ObjectDataSource()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> class with the specified type name and data retrieval method name.</summary>
	/// <param name="typeName">The name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> works with. </param>
	/// <param name="selectMethod">The name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> invokes to retrieve data. </param>
	public ObjectDataSource(string typeName, string selectMethod)
	{
		SelectMethod = selectMethod;
		TypeName = typeName;
	}

	/// <summary>Retrieves the named data source view that is associated with the data source control.</summary>
	/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> named <see langword="DefaultView" /> that is associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
	/// <exception cref="T:System.ArgumentException">The specified <paramref name="viewName" /> is <see langword="null" /> or something other than <see langword="DefaultView" />. </exception>
	protected override DataSourceView GetView(string viewName)
	{
		if (viewName == null)
		{
			throw new ArgumentException("viewName");
		}
		return DefaultView;
	}

	/// <summary>Retrieves a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> object.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" />.</returns>
	protected override ICollection GetViewNames()
	{
		return emptyNames;
	}

	/// <summary>Retrieves data from the underlying data storage by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property with the parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectParameters" /> collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
	public IEnumerable Select()
	{
		return DefaultView.Select(DataSourceSelectArguments.Empty);
	}

	/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateMethod" /> property and any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.UpdateParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows updated in the underlying data storage.</returns>
	public int Update()
	{
		Hashtable hashtable = new Hashtable();
		return DefaultView.Update(hashtable, hashtable, null);
	}

	/// <summary>Performs a delete operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteMethod" /> property with any parameters that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DeleteParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows deleted from the underlying data storage, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs.AffectedRows" /> property of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> is set in the <see cref="E:System.Web.UI.WebControls.ObjectDataSource.Deleted" /> event; otherwise, -1.</returns>
	public int Delete()
	{
		Hashtable keys = new Hashtable();
		return DefaultView.Delete(keys, null);
	}

	/// <summary>Performs an insert operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertMethod" /> property and any parameters in the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.InsertParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows inserted into the underlying data storage.</returns>
	public int Insert()
	{
		Hashtable values = new Hashtable();
		return DefaultView.Insert(values);
	}

	/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the page that contains the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.LoadComplete += OnPageLoadComplete;
	}

	private void OnPageLoadComplete(object sender, EventArgs e)
	{
		FilterParameters.UpdateValues(Context, this);
		SelectParameters.UpdateValues(Context, this);
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control. </summary>
	/// <param name="savedState">An object that contains the saved view state values for the control. </param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState(null);
			((IStateManager)DefaultView).LoadViewState((object)null);
		}
		else
		{
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			((IStateManager)DefaultView).LoadViewState(pair.Second);
		}
	}

	/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control.</summary>
	/// <returns>Returns the server control's current view state; otherwise, returns <see langword="null" />, if there is no view state associated with the control.</returns>
	protected override object SaveViewState()
	{
		object obj = base.SaveViewState();
		object obj2 = ((IStateManager)DefaultView).SaveViewState();
		if (obj != null || obj2 != null)
		{
			return new Pair(obj, obj2);
		}
		return null;
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	protected override void TrackViewState()
	{
		((IStateManager)DefaultView).TrackViewState();
	}
}
