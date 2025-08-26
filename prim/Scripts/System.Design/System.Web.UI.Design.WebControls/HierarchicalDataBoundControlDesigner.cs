using System.Collections;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides design-time support in a designer host for the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> control.</summary>
public class HierarchicalDataBoundControlDesigner : BaseDataBoundControlDesigner
{
	/// <summary>Gets the designer action list collection for this designer.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> associated with this designer.</returns>
	public override DesignerActionListCollection ActionLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Provides access to the designer of the data source, when one is selected for data binding.</summary>
	/// <returns>The designer for the data source of the associated control derived from the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />, which implements the <see cref="T:System.Web.UI.Design.IHierarchicalDataSourceDesigner" />.</returns>
	public IHierarchicalDataSourceDesigner DataSourceDesigner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the default view of the data source that is bound to the associated control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Design.DesignerHierarchicalDataSourceView" /> representing the default view of the data source.</returns>
	public DesignerHierarchicalDataSourceView DesignerView
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the control should render its default action lists, which contain a data source ID drop-down list and related tasks.</summary>
	/// <returns>Always <see langword="true" />.</returns>
	protected virtual bool UseDataSourcePickerActionList
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.HierarchicalDataBoundControlDesigner" /> class.</summary>
	public HierarchicalDataBoundControlDesigner()
	{
	}

	/// <summary>Performs the actions that are necessary to connect to the current data source.</summary>
	/// <returns>
	///   <see langword="true" /> if a connection to a new data source was performed; <see langword="false" /> if the old and new data source are the same.</returns>
	protected override bool ConnectToDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a new data source for the associated control.</summary>
	protected override void CreateDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Binds the associated control to the design-time data source.</summary>
	/// <param name="dataBoundControl">The <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> to bind to the design-time data source.</param>
	protected override void DataBind(BaseDataBoundControl dataBoundControl)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs the actions that are necessary to disconnect from the current data source.</summary>
	protected override void DisconnectFromDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a data source that can be used at design time for the associated control.</summary>
	/// <returns>An object implementing the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface that can be used as a data source for controls derived from the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />.</returns>
	protected virtual IHierarchicalEnumerable GetDesignTimeDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Constructs a sample data source that can be used at design time for the associated control.</summary>
	/// <returns>An object implementing the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> interface that can be used as a data source for controls derived from <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" />.</returns>
	protected virtual IHierarchicalEnumerable GetSampleDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Used by the designer to remove properties from or add additional properties to the display in the Properties grid or to shadow properties of the associated control.</summary>
	/// <param name="properties">A collection implementing the <see cref="T:System.Collections.IDictionary" /> of the added and shadowed properties.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
