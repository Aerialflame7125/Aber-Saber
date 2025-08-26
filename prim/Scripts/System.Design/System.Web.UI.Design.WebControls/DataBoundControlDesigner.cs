using System.Collections;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a base class for design-time support for controls that derive from <see cref="T:System.Web.UI.WebControls.DataBoundControl" />.</summary>
public class DataBoundControlDesigner : BaseDataBoundControlDesigner, IDataBindingSchemaProvider, IDataSourceProvider
{
	/// <summary>Gets the <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object for this designer.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> object associated with this designer.</returns>
	[System.MonoNotSupported("")]
	public override DesignerActionListCollection ActionLists
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the shadowed <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property of the underlying data-bound control.</summary>
	/// <returns>The shadowed <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> of the underlying data-bound control.</returns>
	[System.MonoNotSupported("")]
	public string DataMember
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

	/// <summary>Gets the designer of the data source of the underlying data-bound control.</summary>
	/// <returns>The designer of the data source of the underlying data-bound control.</returns>
	[System.MonoNotSupported("")]
	public IDataSourceDesigner DataSourceDesigner
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object associated with the data source of this designer.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerDataSourceView" /> object associated with the data source of this designer.</returns>
	[System.MonoNotSupported("")]
	public DesignerDataSourceView DesignerView
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of rows that the data-bound control displays on the design surface.</summary>
	/// <returns>The number of rows that the data-bound control displays on the design surface.</returns>
	[System.MonoNotSupported("")]
	protected virtual int SampleRowCount
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the designer should include "Choose a data source" in its action list.</summary>
	/// <returns>
	///   <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	protected virtual bool UseDataSourcePickerActionList
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.Design.IDataBindingSchemaProvider.CanRefreshSchema" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer can refresh the data source; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	bool IDataBindingSchemaProvider.CanRefreshSchema
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.Design.IDataBindingSchemaProvider.Schema" />.</summary>
	/// <returns>An <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> object that describes the data source.</returns>
	[System.MonoNotSupported("")]
	IDataSourceViewSchema IDataBindingSchemaProvider.Schema
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataBoundControlDesigner" /> class.</summary>
	[System.MonoNotSupported("")]
	public DataBoundControlDesigner()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Web.UI.Design.WebControls.DataBoundControlDesigner" /> object and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoNotSupported("")]
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disconnects events from the previous data source and connects them to the current data source.</summary>
	/// <returns>
	///   <see langword="true" /> if the data-bound control connected to a new data source; <see langword="false" /> if the data source did not change.</returns>
	[System.MonoNotSupported("")]
	protected override bool ConnectToDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Invokes the standard dialog box to create a new data source control, and sets the new data source control's ID to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property of the data-bound control.</summary>
	[System.MonoNotSupported("")]
	protected override void CreateDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Binds the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to the data source.</summary>
	/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to bind to the data source.</param>
	[System.MonoNotSupported("")]
	protected override void DataBind(BaseDataBoundControl dataBoundControl)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disconnects the data-bound control from data source events.</summary>
	[System.MonoNotSupported("")]
	protected override void DisconnectFromDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the design-time data source from either the associated <see langword="DataSourceDesigner" /> or the <see langword="DataSource" /> property.</summary>
	/// <returns>An object that implements an <see cref="T:System.Collections.IEnumerable" /> interface referencing the design-time data source.</returns>
	[System.MonoNotSupported("")]
	protected virtual IEnumerable GetDesignTimeDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets dummy sample data to render the data-bound control on the design surface if sample data cannot be created from the <see langword="DataSourceDesigner" /> or <see langword="DataSource" /> properties.</summary>
	/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerable" /> interface containing dummy sample data used to render the data-bound control on the design surface.</returns>
	[System.MonoNotSupported("")]
	protected virtual IEnumerable GetSampleDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Overridden by the designer to shadow run-time properties of the data-bound control with corresponding properties implemented by the designer.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> object containing the properties to filter.</param>
	[System.MonoNotSupported("")]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataBindingSchemaProvider.RefreshSchema(System.Boolean)" />.</summary>
	/// <param name="preferSilent">Indicates whether to suppress any events raised while refreshing the schema.</param>
	[System.MonoNotSupported("")]
	void IDataBindingSchemaProvider.RefreshSchema(bool preferSilent)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceProvider.GetResolvedSelectedDataSource" />.</summary>
	/// <returns>The selected data member from the selected data source, if the control allows the user to select an <see langword="IListSource" /> object (such as a <see cref="T:System.Data.DataSet" /> object) for the data source, and provides a <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property to select a particular list (or <see cref="T:System.Data.DataTable" /> object) within the data source.</returns>
	[System.MonoNotSupported("")]
	IEnumerable IDataSourceProvider.GetResolvedSelectedDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Design.IDataSourceProvider.GetSelectedDataSource" />.</summary>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property of the associated control, or <see langword="null" /> if a data source is not found.</returns>
	[System.MonoNotSupported("")]
	object IDataSourceProvider.GetSelectedDataSource()
	{
		throw new NotImplementedException();
	}
}
