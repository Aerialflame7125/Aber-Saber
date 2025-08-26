using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a Microsoft Access database for use with data-bound controls.</summary>
[Designer("System.Web.UI.Design.WebControls.AccessDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxBitmap("")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AccessDataSource : SqlDataSource
{
	private const string PROVIDER_NAME = "System.Data.OleDb";

	private const string PROVIDER_STRING = "Microsoft.Jet.OLEDB.4.0";

	private string connectionString;

	/// <summary>The <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" /> property overrides the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SqlCacheDependency" /> property.</summary>
	/// <returns>Throws a <see cref="T:System.NotSupportedException" />, in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to get or set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.SqlCacheDependency" />  property.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[MonoTODO("AccessDataSource does not support SQL Cache Dependencies")]
	public override string SqlCacheDependency
	{
		get
		{
			throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
		}
		set
		{
			throw new NotSupportedException("AccessDataSource does not supports SQL Cache Dependencies.");
		}
	}

	/// <summary>Gets the connection string that is used to connect to the Microsoft Access database.</summary>
	/// <returns>The OLE DB connection string that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to an Access database, through the <see cref="N:System.Data.OleDb" /> .NET data provider.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ConnectionString" /> property.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string ConnectionString
	{
		get
		{
			if (connectionString == null)
			{
				connectionString = "Provider=" + "Microsoft.Jet.OLEDB.4.0" + "; Data Source=" + GetPhysicalDataFilePath();
			}
			return connectionString;
		}
		set
		{
			throw new InvalidOperationException("The ConnectionString is automatically generated for AccessDataSource and hence cannot be set.");
		}
	}

	/// <summary>Gets or sets the location of the Microsoft Access .mdb file.</summary>
	/// <returns>The location of the Access .mdb file. Absolute, relative, and virtual paths are supported.</returns>
	/// <exception cref="T:System.ArgumentException">An invalid path was given.</exception>
	[UrlProperty]
	[DefaultValue("")]
	[WebCategory("Data")]
	[WebSysDescription("MS Office Access database file name")]
	[Editor("System.Web.UI.Design.MdbDataFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DataFile
	{
		get
		{
			return ViewState.GetString("DataFile", string.Empty);
		}
		set
		{
			ViewState["DataFile"] = value;
			connectionString = null;
		}
	}

	/// <summary>Gets the name of the .NET data provider that the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control uses to connect to a Microsoft Access database.</summary>
	/// <returns>The string "System.Data.OleDb".</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Web.UI.WebControls.AccessDataSource.ProviderName" /> property. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string ProviderName
	{
		get
		{
			return base.ProviderName;
		}
		set
		{
			throw new InvalidOperationException("Setting ProviderName on an AccessDataSource is not allowed");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class.</summary>
	public AccessDataSource()
	{
		base.ProviderName = "System.Data.OleDb";
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> class with the specified data file path and Select command.</summary>
	/// <param name="dataFile">The location of the Access .mdb file. The location can be relative to the current Web form's folder, an absolute physical path, or a virtual path.</param>
	/// <param name="selectCommand">The SQL query used to retrieve data from the Access database. If the SQL query is a parameterized SQL string, add <see cref="T:System.Web.UI.WebControls.Parameter" /> objects to the <see cref="P:System.Web.UI.WebControls.SqlDataSource.SelectParameters" /> collection. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="dataFile" /> is <see langword="null" /> or an empty string.</exception>
	public AccessDataSource(string dataFile, string selectCommand)
		: base(string.Empty, selectCommand)
	{
		ProviderName = "System.Data.OleDb";
	}

	/// <summary>Creates a data source view object that is associated with the data source control.</summary>
	/// <param name="viewName">The name of the data source view.</param>
	/// <returns>An <see cref="T:System.Web.UI.WebControls.AccessDataSourceView" /> object that is associated with the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> instance.</returns>
	protected override SqlDataSourceView CreateDataSourceView(string viewName)
	{
		AccessDataSourceView accessDataSourceView = new AccessDataSourceView(this, viewName, Context);
		if (base.IsTrackingViewState)
		{
			((IStateManager)accessDataSourceView).TrackViewState();
		}
		return accessDataSourceView;
	}

	/// <summary>Retrieves a <see cref="T:System.Data.Common.DbProviderFactory" /> object that is associated with the .NET data provider that is identified by the <see cref="P:System.Web.UI.WebControls.SqlDataSource.ProviderName" /> property.</summary>
	/// <returns>An <see cref="T:System.Data.OleDb.OleDbFactory" /> object.</returns>
	[MonoTODO("why override?  maybe it doesn't call DbProviderFactories.GetFactory?")]
	protected override DbProviderFactory GetDbProviderFactory()
	{
		return DbProviderFactories.GetFactory("System.Data.OleDb");
	}

	private string GetPhysicalDataFilePath()
	{
		if (string.IsNullOrEmpty(DataFile))
		{
			return string.Empty;
		}
		return HttpContext.Current.Request.MapPath(DataFile);
	}
}
