using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a field that displays custom content in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TemplateField : DataControlField
{
	private ITemplate alternatingItemTemplate;

	private ITemplate editItemTemplate;

	private ITemplate footerTemplate;

	private ITemplate headerTemplate;

	private ITemplate insertItemTemplate;

	private ITemplate itemTemplate;

	/// <summary>Gets or sets the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate AlternatingItemTemplate
	{
		get
		{
			return alternatingItemTemplate;
		}
		set
		{
			alternatingItemTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> object is bound to should be converted to <see langword="null" /> if it is <see cref="F:System.String.Empty" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> is bound to should be converted to <see langword="null" /> when it is <see cref="F:System.String.Empty" />; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(true)]
	[WebCategory("Behavior")]
	public virtual bool ConvertEmptyStringToNull
	{
		get
		{
			object obj = base.ViewState["ConvertEmptyStringToNull"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			base.ViewState["ConvertEmptyStringToNull"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate EditItemTemplate
	{
		get
		{
			return editItemTemplate;
		}
		set
		{
			editItemTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate FooterTemplate
	{
		get
		{
			return footerTemplate;
		}
		set
		{
			footerTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> in a data-bound control. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate HeaderTemplate
	{
		get
		{
			return headerTemplate;
		}
		set
		{
			headerTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate InsertItemTemplate
	{
		get
		{
			return insertItemTemplate;
		}
		set
		{
			insertItemTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the template for displaying an item in a data-bound control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate ItemTemplate
	{
		get
		{
			return itemTemplate;
		}
		set
		{
			itemTemplate = value;
			OnFieldChanged();
		}
	}

	/// <summary>Adds text or controls to a cell's controls collection.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
	/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
	public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		base.InitializeCell(cell, cellType, rowState, rowIndex);
		switch (cellType)
		{
		case DataControlCellType.Header:
			if (headerTemplate != null && ShowHeader)
			{
				cell.Text = string.Empty;
				headerTemplate.InstantiateIn(cell);
			}
			return;
		case DataControlCellType.Footer:
			if (footerTemplate != null)
			{
				cell.Text = string.Empty;
				footerTemplate.InstantiateIn(cell);
			}
			return;
		}
		cell.Text = string.Empty;
		if ((rowState & DataControlRowState.Insert) != 0 && insertItemTemplate != null)
		{
			insertItemTemplate.InstantiateIn(cell);
		}
		else if ((rowState & DataControlRowState.Edit) != 0 && editItemTemplate != null)
		{
			editItemTemplate.InstantiateIn(cell);
		}
		else if ((rowState & DataControlRowState.Alternate) != 0 && alternatingItemTemplate != null)
		{
			alternatingItemTemplate.InstantiateIn(cell);
		}
		else if (itemTemplate != null)
		{
			itemTemplate.InstantiateIn(cell);
		}
		else
		{
			cell.Text = "&nbsp;";
		}
	}

	/// <summary>Extracts the value of the data control fields as specified by one or more two-way binding statements (<see langword="DataBind" />) from the current table cell and adds the values to the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
	/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.TemplateField" />.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="includeReadOnly">
	///       <see langword="true" /> to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, <see langword="false" />.</param>
	public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
	{
		IBindableTemplate bindableTemplate = (((rowState & DataControlRowState.Insert) != 0) ? (insertItemTemplate as IBindableTemplate) : (((rowState & DataControlRowState.Edit) != 0) ? (editItemTemplate as IBindableTemplate) : ((alternatingItemTemplate == null || (rowState & DataControlRowState.Alternate) == 0) ? (itemTemplate as IBindableTemplate) : (alternatingItemTemplate as IBindableTemplate))));
		if (bindableTemplate == null)
		{
			return;
		}
		IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(cell);
		if (orderedDictionary == null)
		{
			return;
		}
		foreach (DictionaryEntry item in orderedDictionary)
		{
			dictionary[item.Key] = item.Value;
		}
	}

	/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object support page callbacks.</summary>
	/// <exception cref="T:System.NotSupportedException">The default implementation of this method is called. </exception>
	public override void ValidateSupportsCallback()
	{
		throw new NotSupportedException("Callback not supported on TemplateField. Turn disable callbacks on '" + base.Control.ID + "'.");
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
	/// <returns>Always returns a new <see cref="T:System.Web.UI.WebControls.TemplateField" />.</returns>
	protected override DataControlField CreateField()
	{
		return new TemplateField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" /> to.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		TemplateField obj = (TemplateField)newField;
		obj.AlternatingItemTemplate = AlternatingItemTemplate;
		obj.ConvertEmptyStringToNull = ConvertEmptyStringToNull;
		obj.EditItemTemplate = EditItemTemplate;
		obj.FooterTemplate = FooterTemplate;
		obj.HeaderTemplate = HeaderTemplate;
		obj.InsertItemTemplate = InsertItemTemplate;
		obj.ItemTemplate = ItemTemplate;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TemplateField" /> class.</summary>
	public TemplateField()
	{
	}
}
