using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents a column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that allows you to customize the layout of controls in the column.</summary>
public class TemplateColumn : DataGridColumn
{
	private ITemplate editItemTemplate;

	private ITemplate footerTemplate;

	private ITemplate headerTemplate;

	private ITemplate itemTemplate;

	/// <summary>Gets or sets the template for displaying the item selected for editing in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the item being edited in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(DataGridItem))]
	[WebSysDescription("")]
	public virtual ITemplate EditItemTemplate
	{
		get
		{
			return editItemTemplate;
		}
		set
		{
			editItemTemplate = value;
		}
	}

	/// <summary>Gets or sets the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(DataGridItem))]
	[WebSysDescription("")]
	public virtual ITemplate FooterTemplate
	{
		get
		{
			return footerTemplate;
		}
		set
		{
			footerTemplate = value;
		}
	}

	/// <summary>Gets or sets the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(DataGridItem))]
	[WebSysDescription("")]
	public virtual ITemplate HeaderTemplate
	{
		get
		{
			return headerTemplate;
		}
		set
		{
			headerTemplate = value;
		}
	}

	/// <summary>Gets or sets the template for displaying a data item in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying a data item in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(DataGridItem))]
	[WebSysDescription("")]
	public virtual ITemplate ItemTemplate
	{
		get
		{
			return itemTemplate;
		}
		set
		{
			itemTemplate = value;
		}
	}

	/// <summary>Calls a <see cref="T:System.Web.UI.WebControls.TableCell" /> object's base class to initialize the instance and then applies a <see cref="T:System.Web.UI.WebControls.ListItemType" /> to the cell.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset.</param>
	/// <param name="columnIndex">The column number where the cell is located.</param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
	public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
	{
		base.InitializeCell(cell, columnIndex, itemType);
		ITemplate template = null;
		switch (itemType)
		{
		case ListItemType.Header:
			template = HeaderTemplate;
			break;
		case ListItemType.Footer:
			template = FooterTemplate;
			break;
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
			template = ItemTemplate;
			if (template == null)
			{
				cell.Text = "&nbsp;";
			}
			break;
		case ListItemType.EditItem:
			template = EditItemTemplate;
			if (template == null)
			{
				template = ItemTemplate;
			}
			if (template == null)
			{
				cell.Text = "&nbsp;";
			}
			break;
		}
		template?.InstantiateIn(cell);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> class.</summary>
	public TemplateColumn()
	{
	}
}
