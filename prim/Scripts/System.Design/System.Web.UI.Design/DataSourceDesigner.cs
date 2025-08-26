using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides design-time support in a design host for the <see cref="T:System.Web.UI.DataSourceControl" /> class.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class DataSourceDesigner : ControlDesigner, IDataSourceDesigner
{
	/// <summary>Gets a list of items that are used to create an action list menu at design time.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object containing the action list items for the control designer.</returns>
	[System.MonoTODO]
	public override DesignerActionListCollection ActionLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.DataSourceDesigner.Configure" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.Web.UI.Design.DataSourceDesigner.Configure" /> can be called; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool CanConfigure
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> can be called; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool CanRefreshSchema
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> event or the <see cref="M:System.Web.UI.Design.DataSourceDesigner.RefreshSchema(System.Boolean)" /> method occurs.</summary>
	/// <returns>
	///   <see langword="true" /> if events are being suppressed; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	protected bool SuppressingDataSourceEvents
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Occurs when any property of the associated data source changes.</summary>
	public event EventHandler DataSourceChanged;

	/// <summary>Occurs after the schema has been refreshed.</summary>
	public event EventHandler SchemaRefreshed;

	/// <summary>Provides a value that indicates whether two schemas are equal.</summary>
	/// <param name="schema1">The first schema to compare (derived from the <see cref="T:System.Web.UI.Design.IDataSourceSchema" />).</param>
	/// <param name="schema2">The second schema to compare.</param>
	/// <returns>
	///   <see langword="true" /> if both schemas are equivalent; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public static bool SchemasEquivalent(IDataSourceSchema schema1, IDataSourceSchema schema2)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides a value that determines whether two schema views are equal.</summary>
	/// <param name="viewSchema1">The first view to compare (derived from the <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" />).</param>
	/// <param name="viewSchema2">The second view to compare.</param>
	/// <returns>
	///   <see langword="true" /> if both views are equivalent; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public static bool ViewSchemasEquivalent(IDataSourceViewSchema viewSchema1, IDataSourceViewSchema viewSchema2)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataSourceDesigner" /> class.</summary>
	public DataSourceDesigner()
	{
	}

	/// <summary>Launches the data source configuration utility in the design host.</summary>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to invoke this method in the base class.</exception>
	[System.MonoTODO]
	public virtual void Configure()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the HTML markup for displaying the associated data source control at design time.</summary>
	/// <returns>The markup for the design-time display.</returns>
	[System.MonoTODO]
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object that is identified by the view name.</summary>
	/// <param name="viewName">The name of the view.</param>
	/// <returns>This implementation always returns <see langword="null" />.</returns>
	[System.MonoTODO]
	public virtual DesignerDataSourceView GetView(string viewName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an array of the view names that are available in this data source.</summary>
	/// <returns>An array of view names.</returns>
	[System.MonoTODO]
	public virtual string[] GetViewNames()
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> event when the properties of the data source have changed and the <see cref="P:System.Web.UI.Design.DataSourceDesigner.SuppressingDataSourceEvents" /> value is <see langword="false" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object provided by the calling object.</param>
	protected virtual void OnDataSourceChanged(EventArgs e)
	{
		if (this.DataSourceChanged != null)
		{
			this.DataSourceChanged(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Design.DataSourceDesigner.SchemaRefreshed" /> event when the schema of the data source has changed and the <see cref="P:System.Web.UI.Design.DataSourceDesigner.SuppressingDataSourceEvents" /> value is <see langword="false" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
	protected virtual void OnSchemaRefreshed(EventArgs e)
	{
		if (this.SchemaRefreshed != null)
		{
			this.SchemaRefreshed(this, e);
		}
	}

	/// <summary>Refreshes the schema from the data source, while optionally suppressing events.</summary>
	/// <param name="preferSilent">
	///   <see langword="true" /> to allow events when refreshing the schema; <see langword="false" /> to disable the <see cref="E:System.Web.UI.Design.DataSourceDesigner.DataSourceChanged" /> and <see cref="E:System.Web.UI.Design.DataSourceDesigner.SchemaRefreshed" /> events when refreshing the schema.</param>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to invoke this method in the base class.</exception>
	[System.MonoTODO]
	public virtual void RefreshSchema(bool preferSilent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Restores data source events after the data source events have been suppressed.</summary>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to invoke this method in the base class.</exception>
	[System.MonoTODO]
	public virtual void ResumeDataSourceEvents()
	{
		throw new NotImplementedException();
	}

	/// <summary>Postpones all data source events until after the <see cref="M:System.Web.UI.Design.DataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
	[System.MonoTODO]
	public virtual void SuppressDataSourceEvents()
	{
		throw new NotImplementedException();
	}
}
