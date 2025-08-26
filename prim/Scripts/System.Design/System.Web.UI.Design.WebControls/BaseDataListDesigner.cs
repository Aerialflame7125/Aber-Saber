using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design.WebControls;

/// <summary>Provides a base class for components that provide design-time support in a designer host for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
public abstract class BaseDataListDesigner : TemplatedControlDesigner, IDataSourceProvider
{
	private string data_key_field;

	private string data_member;

	private string data_source;

	/// <summary>Gets or sets the value of the data key field of the associated control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataKeyField" /> value of the associated control.</returns>
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

	/// <summary>Gets or sets the value of the data member field of the associated control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataMember" /> value of the associated control.</returns>
	public string DataMember
	{
		get
		{
			return data_member;
		}
		set
		{
			data_member = value;
		}
	}

	/// <summary>Gets or sets the value of the data source property of the associated control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> value of the associated control.</returns>
	public string DataSource
	{
		get
		{
			return data_source;
		}
		set
		{
			data_source = value;
		}
	}

	public override bool DesignTimeHtmlRequiresLoadComplete
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	public override DesignerVerbCollection Verbs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseDataListDesigner" /> class.</summary>
	public BaseDataListDesigner()
	{
	}

	/// <summary>Releases the unmanaged resources that are used by the designer and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates an object that can be used as a data source at design time.</summary>
	/// <param name="minimumRows">The minimum number of rows of sample data that the data source should contain.</param>
	/// <param name="dummyDataSource">
	///   <see langword="true" /> if the returned data source contains dummy data; <see langword="false" /> if the returned data source contains data from an actual data source.</param>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface that serves as a data source for use at design time.</returns>
	protected IEnumerable GetDesignTimeDataSource(int minimumRows, out bool dummyDataSource)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates an object that can be used as a data source at design time.</summary>
	/// <param name="selectedDataSource">An object implementing an <see cref="T:System.Collections.IEnumerable" /> that is used as a template for the data format.</param>
	/// <param name="minimumRows">The minimum number of rows of sample data that the data source data should contain.</param>
	/// <param name="dummyDataSource">
	///   <see langword="true" /> if the returned data source contains dummy data; <see langword="false" /> if the returned data source contains data from an actual data source.</param>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface that serves as a data source for use at design time.</returns>
	protected IEnumerable GetDesignTimeDataSource(IEnumerable selectedDataSource, int minimumRows, out bool dummyDataSource)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data source component from the associated control container, resolved to a specific data member.</summary>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> of the associated control, resolved to the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataMember" /> parameter; otherwise, <see langword="null" /> if a data source is not found.</returns>
	public virtual IEnumerable GetResolvedSelectedDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data source component from the associated control container.</summary>
	/// <returns>An object implementing an <see cref="T:System.Collections.IEnumerable" /> interface containing the design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property of the associated control; otherwise, <see langword="null" /> if a data source is not found.</returns>
	public virtual object GetSelectedDataSource()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data source of the template's container.</summary>
	/// <param name="templateName">A <see cref="T:System.String" /> that specifies the name of the template for which to get the data source.</param>
	/// <returns>An object that implements an <see cref="T:System.Collections.IEnumerable" /> interface containing a design-time <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property.</returns>
	public override IEnumerable GetTemplateContainerDataSource(string templateName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Prepares the designer to view, edit, and design the associated control.</summary>
	/// <param name="component">A control derived from the <see cref="T:System.Web.UI.WebControls.BaseDataList" />, which implements an <see cref="T:System.ComponentModel.IComponent" />.</param>
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Invokes a component editor for the associated control.</summary>
	/// <param name="initialPage">The index of the page with which to initialize the component editor.</param>
	protected internal void InvokePropertyBuilder(int initialPage)
	{
		throw new NotImplementedException();
	}

	/// <summary>Handles the <see langword="AutoFormat" /> event.</summary>
	/// <param name="sender">The <see cref="T:System.Object" /> that raised the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected void OnAutoFormat(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when there is a change to the associated control.</summary>
	/// <param name="sender">The <see cref="T:System.Object" /> that is the source of the event.</param>
	/// <param name="e">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that contains the event data.</param>
	public override void OnComponentChanged(object sender, ComponentChangedEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Called when the data source for the associated control has changed.</summary>
	protected internal virtual void OnDataSourceChanged()
	{
		throw new NotImplementedException();
	}

	/// <summary>Represents the method that handles the property-builder event.</summary>
	/// <param name="sender">An <see cref="T:System.Object" /> that is the source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected void OnPropertyBuilder(object sender, EventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Can be overridden to implement functionality that should occur when a style of the associated control has changed.</summary>
	protected internal void OnStylesChanged()
	{
		throw new NotImplementedException();
	}

	/// <summary>Can be overridden to implement functionality that should occur when the designer template-editing verbs have changed.</summary>
	protected abstract void OnTemplateEditingVerbsChanged();

	/// <summary>Used by the designer to remove properties from or add additional properties to the display in the Properties grid or to shadow properties of the associated control.</summary>
	/// <param name="properties">A collection implementing an <see cref="T:System.Collections.IDictionary" /> interface of the added and shadowed properties.</param>
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
