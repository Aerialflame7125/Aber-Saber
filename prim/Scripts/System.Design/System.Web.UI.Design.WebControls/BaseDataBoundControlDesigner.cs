using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides design-time support in a visual designer for controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
public abstract class BaseDataBoundControlDesigner : ControlDesigner
{
	/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property for the associated control.</summary>
	/// <returns>The data-binding expression used by the associated control derived from <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />.</returns>
	[System.MonoNotSupported("")]
	public string DataSource
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property of the underlying <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object.</summary>
	/// <returns>The ID of the <see cref="T:System.Web.UI.DataSourceControl" /> associated with the underlying <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />.</returns>
	[System.MonoNotSupported("")]
	public string DataSourceID
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner" /> class.</summary>
	[System.MonoNotSupported("")]
	protected BaseDataBoundControlDesigner()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner" /> object, and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoNotSupported("")]
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates the markup that is used to render the control at design time.</summary>
	/// <returns>The markup used to render the control at design time.</returns>
	[System.MonoNotSupported("")]
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
	/// <param name="component">A control derived from <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" />, which implements <see cref="T:System.ComponentModel.IComponent" />.</param>
	[System.MonoNotSupported("")]
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>In a design host, such as Visual Studio 2005, displays a dialog box to assist the user in creating a data source.</summary>
	/// <param name="controlDesigner">A reference to this designer.</param>
	/// <param name="dataSourceType">The type of data source.</param>
	/// <param name="configure">
	///   <see langword="true" /> to enable editing of the configuration, or <see langword="false" /> to disable configuration editing.</param>
	/// <param name="dataSourceID">The ID of a <see cref="T:System.Web.UI.DataSourceControl" /> control on the page.</param>
	/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> object.</returns>
	[System.MonoNotSupported("")]
	public static DialogResult ShowCreateDataSourceDialog(ControlDesigner controlDesigner, Type dataSourceType, bool configure, out string dataSourceID)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, typically unhooks events from the previous data source, and then attaches new events to the new data source.</summary>
	/// <returns>
	///   <see langword="true" /> if a connection to a new data source was performed, typically; <see langword="false" /> if the old and new data sources are the same.</returns>
	protected abstract bool ConnectToDataSource();

	/// <summary>When overridden in a derived class, creates a new data source for the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object.</summary>
	protected abstract void CreateDataSource();

	/// <summary>When overridden in a derived class, performs the necessary actions to set up the associated control that is derived from the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
	/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> with which this designer is associated.</param>
	protected abstract void DataBind(BaseDataBoundControl dataBoundControl);

	/// <summary>When overridden in a derived class, unhooks events from the current data source.</summary>
	protected abstract void DisconnectFromDataSource();

	/// <summary>Provides the markup that is used to render the control at design time if the control is empty or if the data source cannot be retrieved.</summary>
	/// <returns>The markup used to render the control at design time with an empty data source.</returns>
	[System.MonoNotSupported("")]
	protected override string GetEmptyDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides the markup that is used to render the control at design time when an error has occurred.</summary>
	/// <param name="e">The <see cref="T:System.Exception" /> that was thrown.</param>
	/// <returns>The markup used to render the control at design time when an error has occurred.</returns>
	[System.MonoNotSupported("")]
	protected override string GetErrorDesignTimeHtml(Exception e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when the data source of the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object changes.</summary>
	/// <param name="forceUpdateView">
	///   <see langword="true" /> to force the update of design-time markup; otherwise, <see langword="false" />.</param>
	[System.MonoNotSupported("")]
	protected virtual void OnDataSourceChanged(bool forceUpdateView)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when the data source of the associated <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> object loads a new schema.</summary>
	[System.MonoNotSupported("")]
	protected virtual void OnSchemaRefreshed()
	{
		throw new NotImplementedException();
	}

	/// <summary>Used by the designer to remove or add additional properties for display in the Properties grid or to shadow properties of the associated control.</summary>
	/// <param name="properties">The <see cref="T:System.Collections.IDictionary" /> containing the properties to filter.</param>
	[System.MonoNotSupported("")]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
