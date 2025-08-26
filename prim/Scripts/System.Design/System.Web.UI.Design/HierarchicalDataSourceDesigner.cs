using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.HierarchicalDataSourceControl" /> control.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class HierarchicalDataSourceDesigner : ControlDesigner, IHierarchicalDataSourceDesigner
{
	/// <summary>Gets the action list collection for the control designer.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object that contains the <see cref="T:System.ComponentModel.Design.DesignerActionList" /> items for the control designer.</returns>
	[System.MonoTODO]
	public override DesignerActionListCollection ActionLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.Configure" /> method can be called.</summary>
	/// <returns>This implementation always returns <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool CanConfigure
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="M:System.Web.UI.Design.IHierarchicalDataSourceDesigner.RefreshSchema(System.Boolean)" /> method can be called.</summary>
	/// <returns>This implementation always returns <see langword="false" />.</returns>
	[System.MonoTODO]
	public virtual bool CanRefreshSchema
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Indicates whether data source events have been disabled.</summary>
	/// <returns>
	///   <see langword="true" /> if the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.DataSourceChanged" /> or <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.SchemaRefreshed" /> event has been disabled; otherwise, <see langword="false" />.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.HierarchicalDataSourceDesigner" /> class.</summary>
	public HierarchicalDataSourceDesigner()
	{
	}

	/// <summary>Launches the configuration wizard for the underlying data source.</summary>
	/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
	public virtual void Configure()
	{
		throw new NotSupportedException();
	}

	/// <summary>Gets the HTML markup that is used to represent the control at design time.</summary>
	/// <returns>The HTML markup used to represent the control at design time.</returns>
	[System.MonoTODO]
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the named data source view associated with the data source control.</summary>
	/// <param name="viewPath">The unique path to the block of data to use in creating the view.</param>
	/// <returns>A <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> object.</returns>
	[System.MonoTODO]
	public virtual DesignerHierarchicalDataSourceView GetView(string viewPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.DataSourceChanged" /> event when the properties of the data source have changed and the <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property value is <see langword="false" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object provided by the calling object.</param>
	[System.MonoTODO]
	protected virtual void OnDataSourceChanged(EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Design.HierarchicalDataSourceDesigner.SchemaRefreshed" /> event when the schema of the data source has changed and the <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property value is <see langword="false" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object.</param>
	[System.MonoTODO]
	protected virtual void OnSchemaRefreshed(EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Refreshes the schema of the data.</summary>
	/// <param name="preferSilent">This parameter is not used in this implementation. However, it should be supported in derived classes.</param>
	/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
	[System.MonoTODO]
	public virtual void RefreshSchema(bool preferSilent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Restores data source events after they have been suppressed.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Design.HierarchicalDataSourceDesigner.SuppressingDataSourceEvents" /> property is <see langword="false" />.</exception>
	[System.MonoTODO]
	public virtual void ResumeDataSourceEvents()
	{
		throw new NotImplementedException();
	}

	/// <summary>Postpones all data source events until after the <see cref="M:System.Web.UI.Design.HierarchicalDataSourceDesigner.ResumeDataSourceEvents" /> method is called.</summary>
	[System.MonoTODO]
	public virtual void SuppressDataSourceEvents()
	{
		throw new NotImplementedException();
	}
}
