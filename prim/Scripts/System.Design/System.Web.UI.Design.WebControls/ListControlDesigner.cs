using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls;

/// <summary>Serves as the base class for designers that provide design-time support in the Visual Web Designer for controls that are derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> abstract class.</summary>
public class ListControlDesigner : DataBoundControlDesigner
{
	private string data_key_field;

	private string data_text_field;

	private string data_value_field;

	/// <summary>Gets the designer action list collection for the designer.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> associated with the designer.</returns>
	public override DesignerActionListCollection ActionLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the associated control should render its default action lists.</summary>
	/// <returns>Always returns <see langword="false" />.</returns>
	protected override bool UseDataSourcePickerActionList
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public string DataKeyField
	{
		get
		{
			return data_key_field;
		}
		set
		{
			data_key_field = value;
		}
	}

	/// <summary>Gets or sets the data text field of the control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.DataTextField" /> of the list control.</returns>
	public string DataTextField
	{
		get
		{
			return data_text_field;
		}
		set
		{
			data_text_field = value;
		}
	}

	/// <summary>Gets or sets the data value field of the control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.DataValueField" /> of the list control.</returns>
	public string DataValueField
	{
		get
		{
			return data_value_field;
		}
		set
		{
			data_value_field = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ListControlDesigner" /> class.</summary>
	public ListControlDesigner()
	{
	}

	/// <summary>Binds the specified control to the design-time data source.</summary>
	/// <param name="dataBoundControl">The associated control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" />, or a copy of that control.</param>
	protected override void DataBind(BaseDataBoundControl dataBoundControl)
	{
		throw new NotImplementedException();
	}

	/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
	/// <param name="component">A control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> that implements an <see cref="T:System.ComponentModel.IComponent" />.</param>
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the markup used to render the control derived from the <see cref="T:System.Web.UI.WebControls.ListControl" /> at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data source component from the associated control container, resolved to a specific data member.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> containing the design-time <see cref="P:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner.DataSource" />, resolved to the <see cref="P:System.Web.UI.Design.WebControls.DataBoundControlDesigner.DataMember" /> of the associated control.</returns>
	public virtual IEnumerable GetResolvedSelectedDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data source component from the associated control container.</summary>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface and containing the design-time <see cref="P:System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner.DataSource" /> of the associated control.</returns>
	public virtual object GetSelectedDataSource()
	{
		throw new NotImplementedException();
	}

	public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when the data source for the associated control has changed.</summary>
	protected internal virtual void OnDataSourceChanged()
	{
		throw new NotImplementedException();
	}

	/// <summary>Used by the designer to remove additional properties from or add properties to the display in the Properties grid or to shadow properties of the associated control.</summary>
	/// <param name="properties">A collection implementing the <see cref="T:System.Collections.IDictionary" /> of the added or shadowed properties to expose for the associated control at design time.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
