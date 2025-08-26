using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;

namespace System.Web.UI.WebControls;

/// <summary>Represents an SQL database to data-bound controls.</summary>
[ParseChildren(true)]
[PersistChildren(false)]
[DefaultProperty("SelectQuery")]
[Designer("System.Web.UI.Design.WebControls.SqlDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultEvent("Selecting")]
[ToolboxBitmap("")]
public class SqlDataSource : DataSourceControl
{
	private static readonly string[] emptyNames = new string[1] { "DefaultView" };

	private string providerName = string.Empty;

	private string connectionString = string.Empty;

	private SqlDataSourceMode dataSourceMode = SqlDataSourceMode.DataSet;

	private int cacheDuration;

	private bool enableCaching;

	private string cacheKeyDependency;

	private string sqlCacheDependency;

	private DataSourceCacheManager cache;

	private DataSourceCacheExpiry cacheExpirationPolicy;

	private SqlDataSourceView view;

	/// <summary>Gets or sets a value indicating whether a data retrieval operation is canceled when any parameter that is contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluates to <see langword="null" />.</summary>
	/// <returns>
	///     <see langword="true" /> if a data retrieval operation is canceled when a parameter contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection evaluated to <see langword="null" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool CancelSelectOnNullParameter
	{
		get
		{
			return View.CancelSelectOnNullParameter;
		}
		set
		{
			View.CancelSelectOnNullParameter = value;
		}
	}

	/// <summary>Gets or sets the value indicating how the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control performs updates and deletes when data in a row in the underlying database changes during the time of the operation.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
	[DefaultValue(ConflictOptions.OverwriteChanges)]
	public ConflictOptions ConflictDetection
	{
		get
		{
			return View.ConflictDetection;
		}
		set
		{
			View.ConflictDetection = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	[DefaultValue(SqlDataSourceCommandType.Text)]
	public SqlDataSourceCommandType DeleteCommandType
	{
		get
		{
			return View.DeleteCommandType;
		}
		set
		{
			View.DeleteCommandType = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property is an SQL statement or the name of a stored procedure. </summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	[DefaultValue(SqlDataSourceCommandType.Text)]
	public SqlDataSourceCommandType InsertCommandType
	{
		get
		{
			return View.InsertCommandType;
		}
		set
		{
			View.InsertCommandType = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property is an SQL query or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	[DefaultValue(SqlDataSourceCommandType.Text)]
	public SqlDataSourceCommandType SelectCommandType
	{
		get
		{
			return View.SelectCommandType;
		}
		set
		{
			View.SelectCommandType = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property is an SQL statement or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	[DefaultValue(SqlDataSourceCommandType.Text)]
	public SqlDataSourceCommandType UpdateCommandType
	{
		get
		{
			return View.UpdateCommandType;
		}
		set
		{
			View.UpdateCommandType = value;
		}
	}

	/// <summary>Gets or sets a format string to apply to the names of any parameters that are passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> method.</summary>
	/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> parameters passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Delete" /> or <see cref="M:System.Web.UI.WebControls.SqlDataSource.Update" /> methods. The default is "{0}".</returns>
	[DefaultValue("{0}")]
	public string OldValuesParameterFormatString
	{
		get
		{
			return View.OldValuesParameterFormatString;
		}
		set
		{
			View.OldValuesParameterFormatString = value;
		}
	}

	/// <summary>Gets or sets the name of a stored procedure parameter that is used to sort retrieved data when data retrieval is performed using a stored procedure.</summary>
	/// <returns>The name of a stored procedure parameter used to sort retrieved data when data retrieval is performed using a stored procedure.</returns>
	[DefaultValue("")]
	public string SortParameterName
	{
		get
		{
			return View.SortParameterName;
		}
		set
		{
			View.SortParameterName = value;
		}
	}

	/// <summary>Gets or sets a filtering expression that is applied when the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method is called.</summary>
	/// <returns>A string that represents a filtering expression applied when data is retrieved using the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property was set and the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> is in <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataReader" /> mode. </exception>
	[DefaultValue("")]
	public string FilterExpression
	{
		get
		{
			return View.FilterExpression;
		}
		set
		{
			View.FilterExpression = value;
		}
	}

	/// <summary>Gets or sets the name of the .NET Framework data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying data source.</summary>
	/// <returns>The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses; otherwise, the ADO.NET provider for Microsoft SQL Server, if no provider is set. The default is the ADO.NET provider for Microsoft SQL Server.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.WebControls.DataProviderNameConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string ProviderName
	{
		get
		{
			return providerName;
		}
		set
		{
			if (providerName != value)
			{
				providerName = value;
				RaiseDataSourceChangedEvent(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the ADO.NET provider–specific connection string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to connect to an underlying database.</summary>
	/// <returns>A .NET Framework data provider–specific string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to connect to the SQL database that it represents. The default is an empty string ("").</returns>
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.SqlDataSourceConnectionStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	public virtual string ConnectionString
	{
		get
		{
			return connectionString;
		}
		set
		{
			if (connectionString != value)
			{
				connectionString = value;
				RaiseDataSourceChangedEvent(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the data retrieval mode that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to fetch data.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" /> values. The default is the  <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataSet" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.DataSourceMode" /> property is not one of the values defined in the <see cref="T:System.Web.UI.WebControls.SqlDataSourceMode" />. </exception>
	[DefaultValue(SqlDataSourceMode.DataSet)]
	public SqlDataSourceMode DataSourceMode
	{
		get
		{
			return dataSourceMode;
		}
		set
		{
			if (dataSourceMode != value)
			{
				dataSourceMode = value;
				RaiseDataSourceChangedEvent(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to delete data from the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to delete data.</returns>
	[DefaultValue("")]
	public string DeleteCommand
	{
		get
		{
			return View.DeleteCommand;
		}
		set
		{
			View.DeleteCommand = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> property.</returns>
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	public ParameterCollection DeleteParameters => View.DeleteParameters;

	/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> string.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.FilterExpression" /> property.</returns>
	[DefaultValue(null)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public ParameterCollection FilterParameters => View.FilterParameters;

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to insert data into the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to insert data.</returns>
	[DefaultValue("")]
	public string InsertCommand
	{
		get
		{
			return View.InsertCommand;
		}
		set
		{
			View.InsertCommand = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> property.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public ParameterCollection InsertParameters => View.InsertParameters;

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to retrieve data from the underlying database.</summary>
	/// <returns>An SQL string or the name of a stored procedure that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to retrieve data.</returns>
	[DefaultValue("")]
	public string SelectCommand
	{
		get
		{
			return View.SelectCommand;
		}
		set
		{
			View.SelectCommand = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> property.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[MergableProperty(false)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public ParameterCollection SelectParameters => View.SelectParameters;

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control uses to update data in the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses to update data.</returns>
	[DefaultValue("")]
	public string UpdateCommand
	{
		get
		{
			return View.UpdateCommand;
		}
		set
		{
			View.UpdateCommand = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property from the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> control that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> property.</returns>
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[MergableProperty(false)]
	[DefaultValue(null)]
	public ParameterCollection UpdateParameters => View.UpdateParameters;

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

	/// <summary>Gets or sets a user-defined key dependency that is linked to all data cache objects that are created by the data source control. All cache objects are explicitly expired when the key is expired.</summary>
	/// <returns>A key that identifies all cache objects created by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
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

	/// <summary>Gets or sets the length of time, in seconds, that the data source control caches data that is retrieved by the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
	/// <returns>The number of seconds that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> caches the results of a <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> operation. The default is 0. The value cannot be negative.</returns>
	[TypeConverter("System.Web.UI.DataSourceCacheDurationConverter")]
	[DefaultValue(0)]
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
	/// <returns>One of the <see cref="T:System.Web.UI.DataSourceCacheExpiry" /> values. The default is the <see cref="F:System.Web.UI.DataSourceCacheExpiry.Absolute" /> value.</returns>
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has data caching enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if data caching is enabled for the data source control; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSource.EnableCaching" /> property is set to <see langword="true" /> when caching is not supported by the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />. </exception>
	[DefaultValue(false)]
	public virtual bool EnableCaching
	{
		get
		{
			return enableCaching;
		}
		set
		{
			if (DataSourceMode == SqlDataSourceMode.DataReader && value)
			{
				throw new NotSupportedException();
			}
			enableCaching = value;
		}
	}

	private SqlDataSourceView View
	{
		get
		{
			if (view == null)
			{
				view = CreateDataSourceView("DefaultView");
				if (base.IsTrackingViewState)
				{
					((IStateManager)view).TrackViewState();
				}
			}
			return view;
		}
	}

	/// <summary>Occurs when a delete operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Deleted
	{
		add
		{
			View.Deleted += value;
		}
		remove
		{
			View.Deleted -= value;
		}
	}

	/// <summary>Occurs before a delete operation.</summary>
	public event SqlDataSourceCommandEventHandler Deleting
	{
		add
		{
			View.Deleting += value;
		}
		remove
		{
			View.Deleting -= value;
		}
	}

	/// <summary>Occurs when an insert operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Inserted
	{
		add
		{
			View.Inserted += value;
		}
		remove
		{
			View.Inserted -= value;
		}
	}

	/// <summary>Occurs before a filter operation.</summary>
	public event SqlDataSourceFilteringEventHandler Filtering
	{
		add
		{
			View.Filtering += value;
		}
		remove
		{
			View.Filtering -= value;
		}
	}

	/// <summary>Occurs before an insert operation.</summary>
	public event SqlDataSourceCommandEventHandler Inserting
	{
		add
		{
			View.Inserting += value;
		}
		remove
		{
			View.Inserting -= value;
		}
	}

	/// <summary>Occurs when a data retrieval operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Selected
	{
		add
		{
			View.Selected += value;
		}
		remove
		{
			View.Selected -= value;
		}
	}

	/// <summary>Occurs before a data retrieval operation.</summary>
	public event SqlDataSourceSelectingEventHandler Selecting
	{
		add
		{
			View.Selecting += value;
		}
		remove
		{
			View.Selecting -= value;
		}
	}

	/// <summary>Occurs when an update operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Updated
	{
		add
		{
			View.Updated += value;
		}
		remove
		{
			View.Updated -= value;
		}
	}

	/// <summary>Occurs before an update operation.</summary>
	public event SqlDataSourceCommandEventHandler Updating
	{
		add
		{
			View.Updating += value;
		}
		remove
		{
			View.Updating -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
	public SqlDataSource()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
	/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
	/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
	public SqlDataSource(string connectionString, string selectCommand)
	{
		ConnectionString = connectionString;
		SelectCommand = selectCommand;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class with the specified connection string and Select command.</summary>
	/// <param name="providerName">The name of the data provider that the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses. If no provider is set, the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> uses the ADO.NET provider for Microsoft SQL Server, by default. </param>
	/// <param name="connectionString">The connection string used to connect to the underlying database. </param>
	/// <param name="selectCommand">The SQL query used to retrieve data from the underlying database. If the SQL query is a parameterized SQL string, you might need to add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
	public SqlDataSource(string providerName, string connectionString, string selectCommand)
	{
		ProviderName = providerName;
		ConnectionString = connectionString;
		SelectCommand = selectCommand;
	}

	/// <summary>Gets the named data source view that is associated with the data source control.</summary>
	/// <param name="viewName">The name of the view to retrieve. Because the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> supports only one view, <paramref name="viewName" /> is ignored. </param>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> named "Table" that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="viewName" /> is <see langword="null" /> or something other than Table. </exception>
	protected override DataSourceView GetView(string viewName)
	{
		if (string.IsNullOrEmpty(viewName) || string.Compare(viewName, emptyNames[0], StringComparison.InvariantCultureIgnoreCase) == 0)
		{
			return View;
		}
		throw new ArgumentException("viewName");
	}

	/// <summary>Creates a data source view object that is associated with the data source control.</summary>
	/// <param name="viewName">The name of the data source view. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> that is associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
	protected virtual SqlDataSourceView CreateDataSourceView(string viewName)
	{
		SqlDataSourceView sqlDataSourceView = new SqlDataSourceView(this, viewName, Context);
		if (base.IsTrackingViewState)
		{
			((IStateManager)sqlDataSourceView).TrackViewState();
		}
		return sqlDataSourceView;
	}

	/// <summary>Returns the <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the ADO.NET provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
	/// <returns>A <see cref="T:System.Data.Common.DbProviderFactory" /> that represents the identified ADO.NET provider; otherwise, and instance of the <see cref="N:System.Data.SqlClient" />, if no provider is set.</returns>
	protected virtual DbProviderFactory GetDbProviderFactory()
	{
		if (!string.IsNullOrEmpty(ProviderName))
		{
			return DbProviderFactories.GetFactory(ProviderName);
		}
		return SqlClientFactory.Instance;
	}

	internal DbProviderFactory GetDbProviderFactoryInternal()
	{
		return GetDbProviderFactory();
	}

	/// <summary>Gets a collection of names representing the list of view objects that are associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
	protected override ICollection GetViewNames()
	{
		return emptyNames;
	}

	/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.InsertParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	public int Insert()
	{
		return View.Insert(null);
	}

	/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DeleteParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	public int Delete()
	{
		return View.Delete(null, null);
	}

	/// <summary>Retrieves data from the underlying database by using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> object cannot establish a connection with the underlying data source. </exception>
	public IEnumerable Select(DataSourceSelectArguments arguments)
	{
		return View.Select(arguments);
	}

	/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSource.UpdateParameters" /> collection.</summary>
	/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	public int Update()
	{
		return View.Update(null, null, null);
	}

	/// <summary>Adds a <see cref="E:System.Web.UI.Page.LoadComplete" /> event handler to the <see cref="T:System.Web.UI.Page" /> control that contains the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Page.LoadComplete += OnPageLoadComplete;
	}

	private void OnPageLoadComplete(object sender, EventArgs e)
	{
		FilterParameters.UpdateValues(Context, this);
		SelectParameters.UpdateValues(Context, this);
	}

	/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control that need to be persisted.</summary>
	/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState is Pair pair)
		{
			base.LoadViewState(pair.First);
			((IStateManager)View).LoadViewState(pair.Second);
		}
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
	/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" />.</returns>
	protected override object SaveViewState()
	{
		object obj = base.SaveViewState();
		object obj2 = ((IStateManager)View).SaveViewState();
		if (obj != null || obj2 != null)
		{
			return new Pair(obj, obj2);
		}
		return null;
	}

	/// <summary>Tracks view state changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control so that the changes can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (view != null)
		{
			((IStateManager)view).TrackViewState();
		}
	}
}
