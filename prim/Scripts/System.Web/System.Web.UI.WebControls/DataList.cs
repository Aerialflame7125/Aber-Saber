using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>A data bound list control that displays items using templates.</summary>
[Designer("System.Web.UI.Design.WebControls.DataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlValueProperty("SelectedValue")]
[Editor("System.Web.UI.Design.WebControls.DataListComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.ComponentEditor, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataList : BaseDataList, INamingContainer, IRepeatInfoUser
{
	/// <summary>Represents the <see langword="Cancel" /> command name. This field is read-only.</summary>
	public const string CancelCommandName = "Cancel";

	/// <summary>Represents the <see langword="Delete" /> command name. This field is read-only.</summary>
	public const string DeleteCommandName = "Delete";

	/// <summary>Represents the <see langword="Edit" /> command name. This field is read-only.</summary>
	public const string EditCommandName = "Edit";

	/// <summary>Represents the <see langword="Select" /> command name. This field is read-only.</summary>
	public const string SelectCommandName = "Select";

	/// <summary>Represents the <see langword="Update" /> command name. This field is read-only.</summary>
	public const string UpdateCommandName = "Update";

	private static readonly object cancelCommandEvent = new object();

	private static readonly object deleteCommandEvent = new object();

	private static readonly object editCommandEvent = new object();

	private static readonly object itemCommandEvent = new object();

	private static readonly object itemCreatedEvent = new object();

	private static readonly object itemDataBoundEvent = new object();

	private static readonly object updateCommandEvent = new object();

	private TableItemStyle alternatingItemStyle;

	private TableItemStyle editItemStyle;

	private TableItemStyle footerStyle;

	private TableItemStyle headerStyle;

	private TableItemStyle itemStyle;

	private TableItemStyle selectedItemStyle;

	private TableItemStyle separatorStyle;

	private ITemplate alternatingItemTemplate;

	private ITemplate editItemTemplate;

	private ITemplate footerTemplate;

	private ITemplate headerTemplate;

	private ITemplate itemTemplate;

	private ITemplate selectedItemTemplate;

	private ITemplate separatorTemplate;

	private DataListItemCollection items;

	private ArrayList list;

	private int idx;

	/// <summary>Gets the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that represents the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle AlternatingItemStyle
	{
		get
		{
			if (alternatingItemStyle == null)
			{
				alternatingItemStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					alternatingItemStyle.TrackViewState();
				}
			}
			return alternatingItemStyle;
		}
	}

	/// <summary>Gets or sets the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" />.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual ITemplate AlternatingItemTemplate
	{
		get
		{
			return alternatingItemTemplate;
		}
		set
		{
			alternatingItemTemplate = value;
		}
	}

	/// <summary>Gets or sets the index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</summary>
	/// <returns>The index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see langword="0" />.</exception>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual int EditItemIndex
	{
		get
		{
			object obj = ViewState["EditItemIndex"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("EditItemIndex", "< -1");
			}
			ViewState["EditItemIndex"] = value;
		}
	}

	/// <summary>Gets the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle EditItemStyle
	{
		get
		{
			if (editItemStyle == null)
			{
				editItemStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					editItemStyle.TrackViewState();
				}
			}
			return editItemStyle;
		}
	}

	/// <summary>Gets or sets the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
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

	/// <summary>Gets or sets a value that indicates whether the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual bool ExtractTemplateRows
	{
		get
		{
			object obj = ViewState["ExtractTemplateRows"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["ExtractTemplateRows"] = value;
		}
	}

	/// <summary>Gets the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle FooterStyle
	{
		get
		{
			if (footerStyle == null)
			{
				footerStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					footerStyle.TrackViewState();
				}
			}
			return footerStyle;
		}
	}

	/// <summary>Gets or sets the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
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

	/// <summary>Gets or sets the grid line style for the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> property is set to <see langword="RepeatLayout.Table" />.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is <see langword="None" />.</returns>
	[DefaultValue(GridLines.None)]
	public override GridLines GridLines
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return GridLines.None;
			}
			return TableStyle.GridLines;
		}
		set
		{
			TableStyle.GridLines = value;
		}
	}

	/// <summary>Gets the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle HeaderStyle
	{
		get
		{
			if (headerStyle == null)
			{
				headerStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					headerStyle.TrackViewState();
				}
			}
			return headerStyle;
		}
	}

	/// <summary>Gets or sets the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
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

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> that contains a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual DataListItemCollection Items
	{
		get
		{
			if (items == null)
			{
				items = new DataListItemCollection(ItemList);
			}
			return items;
		}
	}

	/// <summary>Gets the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle ItemStyle
	{
		get
		{
			if (itemStyle == null)
			{
				itemStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					itemStyle.TrackViewState();
				}
			}
			return itemStyle;
		}
	}

	/// <summary>Gets or sets the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
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

	/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="0" />, which indicates that the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control are displayed in a single row or column, based on the value of the <see cref="P:System.Web.UI.WebControls.DataList.RepeatDirection" /> property.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of columns is a negative value. </exception>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int RepeatColumns
	{
		get
		{
			object obj = ViewState["RepeatColumns"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", "RepeatColumns value has to be 0 for 'not set' or > 0.");
			}
			ViewState["RepeatColumns"] = value;
		}
	}

	/// <summary>Gets or sets whether the <see cref="T:System.Web.UI.WebControls.DataList" /> control displays vertically or horizontally.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is <see langword="Vertical" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
	[DefaultValue(RepeatDirection.Vertical)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual RepeatDirection RepeatDirection
	{
		get
		{
			object obj = ViewState["RepeatDirection"];
			if (obj != null)
			{
				return (RepeatDirection)obj;
			}
			return RepeatDirection.Vertical;
		}
		set
		{
			ViewState["RepeatDirection"] = value;
		}
	}

	/// <summary>Gets or sets whether the control is displayed in a table or flow layout.</summary>
	/// <returns>A value that specifies whether the control is displayed in a table or in flow layout.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the supported <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values.</exception>
	[DefaultValue(RepeatLayout.Table)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual RepeatLayout RepeatLayout
	{
		get
		{
			object obj = ViewState["RepeatLayout"];
			if (obj != null)
			{
				return (RepeatLayout)obj;
			}
			return RepeatLayout.Table;
		}
		set
		{
			if (value == RepeatLayout.OrderedList || value == RepeatLayout.UnorderedList)
			{
				throw new ArgumentOutOfRangeException($"DataList does not support the '{value}' layout.");
			}
			ViewState["RepeatLayout"] = value;
		}
	}

	/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than <see langword="-1" />.</exception>
	[Bindable(true)]
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int SelectedIndex
	{
		get
		{
			object obj = ViewState["SelectedIndex"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("SelectedIndex", "< -1");
			}
			ViewState["SelectedIndex"] = value;
		}
	}

	/// <summary>Gets the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> that represents the item selected in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual DataListItem SelectedItem
	{
		get
		{
			if (SelectedIndex < 0)
			{
				return null;
			}
			if (SelectedIndex >= Items.Count)
			{
				throw new ArgumentOutOfRangeException("SelectedItem", ">= Items.Count");
			}
			return items[SelectedIndex];
		}
	}

	/// <summary>Gets the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle SelectedItemStyle
	{
		get
		{
			if (selectedItemStyle == null)
			{
				selectedItemStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					selectedItemStyle.TrackViewState();
				}
			}
			return selectedItemStyle;
		}
	}

	/// <summary>Gets or sets the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual ITemplate SelectedItemTemplate
	{
		get
		{
			return selectedItemTemplate;
		}
		set
		{
			selectedItemTemplate = value;
		}
	}

	/// <summary>Gets the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle SeparatorStyle
	{
		get
		{
			if (separatorStyle == null)
			{
				separatorStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					separatorStyle.TrackViewState();
				}
			}
			return separatorStyle;
		}
	}

	/// <summary>Gets or sets the template for the separator between the items of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the separator between items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[TemplateContainer(typeof(DataListItem))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual ITemplate SeparatorTemplate
	{
		get
		{
			return separatorTemplate;
		}
		set
		{
			separatorTemplate = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the footer section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the footer section is displayed; otherwise, <see langword="false" />. The default value is <see langword="true" />, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.FooterTemplate" /> property is not <see langword="null" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual bool ShowFooter
	{
		get
		{
			object obj = ViewState["ShowFooter"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["ShowFooter"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the header section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> if the header is displayed; otherwise, <see langword="false" />. The default value is <see langword="true" />, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.HeaderTemplate" /> property is not <see langword="null" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual bool ShowHeader
	{
		get
		{
			object obj = ViewState["ShowHeader"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			ViewState["ShowHeader"] = value;
		}
	}

	/// <summary>Gets the value of the key field for the selected data list item.</summary>
	/// <returns>The key field value for the selected data list item. The default is <see langword="null" />, which indicates that no data list item is currently selected.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataKeyField" /> property has not been set.</exception>
	[MonoTODO("incomplete")]
	[Browsable(false)]
	public object SelectedValue
	{
		get
		{
			if (DataKeyField.Length == 0)
			{
				throw new InvalidOperationException(Locale.GetText("No DataKeyField present."));
			}
			int selectedIndex = SelectedIndex;
			if (selectedIndex >= 0 && selectedIndex < base.DataKeys.Count)
			{
				return base.DataKeys[selectedIndex];
			}
			return null;
		}
	}

	/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <returns>Returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" /> tag if the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> is set to <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />; otherwise, returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Span" /> tag. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	private TableStyle TableStyle => (TableStyle)base.ControlStyle;

	private ArrayList ItemList
	{
		get
		{
			if (list == null)
			{
				list = new ArrayList();
			}
			return list;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasFooter" />.</summary>
	bool IRepeatInfoUser.HasFooter
	{
		get
		{
			if (ShowFooter)
			{
				return footerTemplate != null;
			}
			return false;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasHeader" />.</summary>
	bool IRepeatInfoUser.HasHeader
	{
		get
		{
			if (ShowHeader)
			{
				return headerTemplate != null;
			}
			return false;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasSeparators" />.</summary>
	bool IRepeatInfoUser.HasSeparators => separatorTemplate != null;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.RepeatedItemCount" />.</summary>
	int IRepeatInfoUser.RepeatedItemCount
	{
		get
		{
			if (idx == -1)
			{
				object obj = ViewState["Items"];
				idx = ((obj != null) ? ((int)obj) : 0);
			}
			return idx;
		}
	}

	/// <summary>Occurs when the <see langword="Cancel" /> button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListCommandEventHandler CancelCommand
	{
		add
		{
			base.Events.AddHandler(cancelCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(cancelCommandEvent, value);
		}
	}

	/// <summary>Occurs when the <see langword="Delete" /> button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListCommandEventHandler DeleteCommand
	{
		add
		{
			base.Events.AddHandler(deleteCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(deleteCommandEvent, value);
		}
	}

	/// <summary>Occurs when the <see langword="Edit" /> button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListCommandEventHandler EditCommand
	{
		add
		{
			base.Events.AddHandler(editCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(editCommandEvent, value);
		}
	}

	/// <summary>Occurs when any button is clicked in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListCommandEventHandler ItemCommand
	{
		add
		{
			base.Events.AddHandler(itemCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(itemCommandEvent, value);
		}
	}

	/// <summary>Occurs on the server when an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control is created.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListItemEventHandler ItemCreated
	{
		add
		{
			base.Events.AddHandler(itemCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(itemCreatedEvent, value);
		}
	}

	/// <summary>Occurs when an item is data bound to the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListItemEventHandler ItemDataBound
	{
		add
		{
			base.Events.AddHandler(itemDataBoundEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(itemDataBoundEvent, value);
		}
	}

	/// <summary>Occurs when the <see langword="Update" /> button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataListCommandEventHandler UpdateCommand
	{
		add
		{
			base.Events.AddHandler(updateCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(updateCommandEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataList" /> class.</summary>
	public DataList()
	{
		idx = -1;
	}

	private void DoItem(int i, ListItemType t, object d, bool databind)
	{
		DataListItem dataListItem = CreateItem(i, t);
		if (databind)
		{
			dataListItem.DataItem = d;
		}
		DataListItemEventArgs e = new DataListItemEventArgs(dataListItem);
		InitializeItem(dataListItem);
		Controls.Add(dataListItem);
		if (i != -1)
		{
			ItemList.Add(dataListItem);
		}
		OnItemCreated(e);
		if (databind)
		{
			dataListItem.DataBind();
			OnItemDataBound(e);
			dataListItem.DataItem = null;
		}
	}

	private void DoItemInLoop(int i, object d, bool databind, ListItemType type)
	{
		DoItem(i, type, d, databind);
		if (SeparatorTemplate != null)
		{
			DoItem(i, ListItemType.Separator, null, databind);
		}
	}

	/// <summary>Creates the control hierarchy that is used to render the data list control, with or without the specified data source.</summary>
	/// <param name="useDataSource">
	///       <see langword="true" /> to use the control's data source; <see langword="false" /> to indicate that the control is being recreated from view state and should not be data-bound.</param>
	protected override void CreateControlHierarchy(bool useDataSource)
	{
		Controls.Clear();
		ItemList.Clear();
		IEnumerable enumerable = null;
		ArrayList arrayList = null;
		if (useDataSource)
		{
			idx = 0;
			enumerable = ((!base.IsBoundUsingDataSourceID) ? DataSourceResolver.ResolveDataSource(DataSource, base.DataMember) : GetData());
			arrayList = base.DataKeysArray;
			arrayList.Clear();
		}
		else
		{
			idx = (int)ViewState["Items"];
		}
		if (enumerable == null && idx == 0)
		{
			return;
		}
		if (headerTemplate != null)
		{
			DoItem(-1, ListItemType.Header, null, useDataSource);
		}
		int selectedIndex = SelectedIndex;
		int editItemIndex = EditItemIndex;
		if (enumerable != null)
		{
			string dataKeyField = DataKeyField;
			foreach (object item in enumerable)
			{
				if (useDataSource && !string.IsNullOrEmpty(dataKeyField))
				{
					arrayList.Add(DataBinder.GetPropertyValue(item, dataKeyField));
				}
				ListItemType type = ListItemType.Item;
				if (idx == editItemIndex)
				{
					type = ListItemType.EditItem;
				}
				else if (idx == selectedIndex)
				{
					type = ListItemType.SelectedItem;
				}
				else if ((idx & 1) != 0)
				{
					type = ListItemType.AlternatingItem;
				}
				DoItemInLoop(idx, item, useDataSource, type);
				idx++;
			}
		}
		else
		{
			for (int i = 0; i < idx; i++)
			{
				ListItemType type = ListItemType.Item;
				if (i == editItemIndex)
				{
					type = ListItemType.EditItem;
				}
				else if (i == selectedIndex)
				{
					type = ListItemType.SelectedItem;
				}
				else if ((i & 1) != 0)
				{
					type = ListItemType.AlternatingItem;
				}
				DoItemInLoop(i, null, useDataSource, type);
			}
		}
		if (footerTemplate != null)
		{
			DoItem(-1, ListItemType.Footer, null, useDataSource);
		}
		ViewState["Items"] = idx;
	}

	/// <summary>Creates the default style object that is used internally by the <see cref="T:System.Web.UI.WebControls.DataList" /> control to implement all style related properties.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableStyle" /> that contains the default style properties for the control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle
		{
			CellSpacing = 0
		};
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object.</summary>
	/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.DataList" /> to place the created item.</param>
	/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the item to create.</param>
	/// <returns>A new <see cref="T:System.Web.UI.WebControls.DataListItem" /> created with the specified list-item type.</returns>
	protected virtual DataListItem CreateItem(int itemIndex, ListItemType itemType)
	{
		return new DataListItem(itemIndex, itemType);
	}

	/// <summary>Initializes a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object based on the specified templates and styles for the list-item type.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataListItem" /> to initialize.</param>
	protected virtual void InitializeItem(DataListItem item)
	{
		ITemplate template = null;
		switch (item.ItemType)
		{
		case ListItemType.Header:
			template = HeaderTemplate;
			break;
		case ListItemType.Footer:
			template = FooterTemplate;
			break;
		case ListItemType.Separator:
			template = SeparatorTemplate;
			break;
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
		case ListItemType.EditItem:
			template = ((item.ItemType == ListItemType.EditItem && EditItemTemplate != null) ? EditItemTemplate : ((item.ItemType == ListItemType.SelectedItem && SelectedItemTemplate != null) ? SelectedItemTemplate : ((item.ItemType != ListItemType.AlternatingItem || AlternatingItemTemplate == null) ? ItemTemplate : AlternatingItemTemplate)));
			break;
		}
		template?.InstantiateIn(item);
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.DataList" />.</param>
	protected override void LoadViewState(object savedState)
	{
		object[] array = (object[])savedState;
		base.LoadViewState(array[0]);
		if (array[1] != null)
		{
			ItemStyle.LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			SelectedItemStyle.LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			AlternatingItemStyle.LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			EditItemStyle.LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			SeparatorStyle.LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			HeaderStyle.LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			FooterStyle.LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			base.ControlStyle.LoadViewState(array[8]);
		}
	}

	/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">The event data. </param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (!(e is DataListCommandEventArgs { CommandName: var commandName } dataListCommandEventArgs))
		{
			return false;
		}
		CultureInfo invariantCulture = Helpers.InvariantCulture;
		OnItemCommand(dataListCommandEventArgs);
		if (string.Compare(commandName, "Cancel", ignoreCase: true, invariantCulture) == 0)
		{
			OnCancelCommand(dataListCommandEventArgs);
		}
		else if (string.Compare(commandName, "Delete", ignoreCase: true, invariantCulture) == 0)
		{
			OnDeleteCommand(dataListCommandEventArgs);
		}
		else if (string.Compare(commandName, "Edit", ignoreCase: true, invariantCulture) == 0)
		{
			OnEditCommand(dataListCommandEventArgs);
		}
		else if (string.Compare(commandName, "Select", ignoreCase: true, invariantCulture) == 0)
		{
			SelectedIndex = dataListCommandEventArgs.Item.ItemIndex;
			OnSelectedIndexChanged(dataListCommandEventArgs);
		}
		else if (string.Compare(commandName, "Update", ignoreCase: true, invariantCulture) == 0)
		{
			OnUpdateCommand(dataListCommandEventArgs);
		}
		return true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnCancelCommand(DataListCommandEventArgs e)
	{
		((DataListCommandEventHandler)base.Events[cancelCommandEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnDeleteCommand(DataListCommandEventArgs e)
	{
		((DataListCommandEventHandler)base.Events[deleteCommandEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnEditCommand(DataListCommandEventArgs e)
	{
		((DataListCommandEventHandler)base.Events[editCommandEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page?.RegisterRequiresControlState(this);
		base.OnInit(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnItemCommand(DataListCommandEventArgs e)
	{
		((DataListCommandEventHandler)base.Events[itemCommandEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCreated" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemCreated(DataListItemEventArgs e)
	{
		((DataListItemEventHandler)base.Events[itemCreatedEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemDataBound" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemDataBound(DataListItemEventArgs e)
	{
		((DataListItemEventHandler)base.Events[itemDataBoundEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
	protected virtual void OnUpdateCommand(DataListCommandEventArgs e)
	{
		((DataListCommandEventHandler)base.Events[updateCommandEvent])?.Invoke(this, e);
	}

	/// <summary>Prepares the control hierarchy for rendering in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	protected override void PrepareControlHierarchy()
	{
		if (!HasControls() || Controls.Count == 0)
		{
			return;
		}
		Style style = null;
		foreach (DataListItem control in Controls)
		{
			switch (control.ItemType)
			{
			case ListItemType.Item:
				control.MergeStyle(itemStyle);
				break;
			case ListItemType.AlternatingItem:
				if (style == null)
				{
					if (alternatingItemStyle != null)
					{
						style = new TableItemStyle();
						style.CopyFrom(itemStyle);
						style.CopyFrom(alternatingItemStyle);
					}
					else
					{
						style = itemStyle;
					}
				}
				control.MergeStyle(style);
				break;
			case ListItemType.EditItem:
				if (editItemStyle != null)
				{
					control.MergeStyle(editItemStyle);
				}
				else
				{
					control.MergeStyle(itemStyle);
				}
				break;
			case ListItemType.Footer:
				if (!ShowFooter)
				{
					control.Visible = false;
				}
				else if (footerStyle != null)
				{
					control.MergeStyle(footerStyle);
				}
				break;
			case ListItemType.Header:
				if (!ShowHeader)
				{
					control.Visible = false;
				}
				else if (headerStyle != null)
				{
					control.MergeStyle(headerStyle);
				}
				break;
			case ListItemType.SelectedItem:
				if (selectedItemStyle != null)
				{
					control.MergeStyle(selectedItemStyle);
				}
				else
				{
					control.MergeStyle(itemStyle);
				}
				break;
			case ListItemType.Separator:
				if (separatorStyle != null)
				{
					control.MergeStyle(separatorStyle);
				}
				else
				{
					control.MergeStyle(itemStyle);
				}
				break;
			}
		}
	}

	/// <summary>Renders the list items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (Items.Count == 0)
		{
			return;
		}
		RepeatInfo repeatInfo = new RepeatInfo();
		repeatInfo.RepeatColumns = RepeatColumns;
		repeatInfo.RepeatDirection = RepeatDirection;
		repeatInfo.RepeatLayout = RepeatLayout;
		repeatInfo.CaptionAlign = CaptionAlign;
		repeatInfo.Caption = Caption;
		repeatInfo.UseAccessibleHeader = UseAccessibleHeader;
		if (ExtractTemplateRows)
		{
			repeatInfo.OuterTableImplied = true;
			writer.AddAttribute(HtmlTextWriterAttribute.Id, ClientID);
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.AddAttributesToRender(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
			writer.RenderEndTag();
		}
		else
		{
			repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
		}
	}

	/// <summary>Saves the changes to the control view state since the time the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> view state. </returns>
	protected override object SaveViewState()
	{
		object[] array = new object[9]
		{
			base.SaveViewState(),
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		};
		if (itemStyle != null)
		{
			array[1] = itemStyle.SaveViewState();
		}
		if (selectedItemStyle != null)
		{
			array[2] = selectedItemStyle.SaveViewState();
		}
		if (alternatingItemStyle != null)
		{
			array[3] = alternatingItemStyle.SaveViewState();
		}
		if (editItemStyle != null)
		{
			array[4] = editItemStyle.SaveViewState();
		}
		if (separatorStyle != null)
		{
			array[5] = separatorStyle.SaveViewState();
		}
		if (headerStyle != null)
		{
			array[6] = headerStyle.SaveViewState();
		}
		if (footerStyle != null)
		{
			array[7] = footerStyle.SaveViewState();
		}
		if (base.ControlStyleCreated)
		{
			array[8] = base.ControlStyle.SaveViewState();
		}
		return array;
	}

	/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> control so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (alternatingItemStyle != null)
		{
			alternatingItemStyle.TrackViewState();
		}
		if (editItemStyle != null)
		{
			editItemStyle.TrackViewState();
		}
		if (footerStyle != null)
		{
			footerStyle.TrackViewState();
		}
		if (headerStyle != null)
		{
			headerStyle.TrackViewState();
		}
		if (itemStyle != null)
		{
			itemStyle.TrackViewState();
		}
		if (selectedItemStyle != null)
		{
			selectedItemStyle.TrackViewState();
		}
		if (separatorStyle != null)
		{
			separatorStyle.TrackViewState();
		}
		if (base.ControlStyleCreated)
		{
			base.ControlStyle.TrackViewState();
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values.</param>
	/// <param name="repeatIndex">The index of the item in the list control.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
	Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
	{
		DataListItem dataListItem = null;
		switch (itemType)
		{
		case ListItemType.Header:
		case ListItemType.Footer:
			if (repeatIndex >= 0 && (!HasControls() || repeatIndex >= Controls.Count))
			{
				throw new ArgumentOutOfRangeException();
			}
			dataListItem = FindFirstItem(itemType);
			break;
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
		case ListItemType.EditItem:
			if (repeatIndex >= 0 && (!HasControls() || repeatIndex >= Controls.Count))
			{
				throw new ArgumentOutOfRangeException();
			}
			dataListItem = FindBestItem(repeatIndex);
			break;
		case ListItemType.Separator:
			if (repeatIndex >= 0 && (!HasControls() || repeatIndex >= Controls.Count))
			{
				throw new ArgumentOutOfRangeException();
			}
			dataListItem = FindSpecificItem(itemType, repeatIndex);
			break;
		default:
			dataListItem = null;
			break;
		}
		if (dataListItem == null || !dataListItem.ControlStyleCreated)
		{
			return null;
		}
		return dataListItem.ControlStyle;
	}

	private DataListItem FindFirstItem(ListItemType itemType)
	{
		for (int i = 0; i < Controls.Count; i++)
		{
			if (Controls[i] is DataListItem dataListItem && dataListItem.ItemType == itemType)
			{
				return dataListItem;
			}
		}
		return null;
	}

	private DataListItem FindSpecificItem(ListItemType itemType, int repeatIndex)
	{
		for (int i = 0; i < Controls.Count; i++)
		{
			if (Controls[i] is DataListItem dataListItem && dataListItem.ItemType == itemType && dataListItem.ItemIndex == repeatIndex)
			{
				return dataListItem;
			}
		}
		return null;
	}

	private DataListItem FindBestItem(int repeatIndex)
	{
		for (int i = 0; i < Controls.Count; i++)
		{
			if (Controls[i] is DataListItem dataListItem && dataListItem.ItemIndex == repeatIndex)
			{
				ListItemType itemType = dataListItem.ItemType;
				if ((uint)(itemType - 2) <= 3u)
				{
					return dataListItem;
				}
				return null;
			}
		}
		return null;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />.</summary>
	/// <param name="itemType">The type of the item.</param>
	/// <param name="repeatIndex">The index of the item.</param>
	/// <param name="repeatInfo">Information that is used to render the item.</param>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render the item.</param>
	void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
	{
		if (!HasControls())
		{
			return;
		}
		DataListItem dataListItem = null;
		switch (itemType)
		{
		case ListItemType.Header:
		case ListItemType.Footer:
			dataListItem = FindFirstItem(itemType);
			break;
		case ListItemType.Item:
		case ListItemType.AlternatingItem:
		case ListItemType.SelectedItem:
		case ListItemType.EditItem:
			dataListItem = FindBestItem(repeatIndex);
			break;
		case ListItemType.Separator:
			dataListItem = FindSpecificItem(itemType, repeatIndex);
			break;
		}
		if (dataListItem == null)
		{
			return;
		}
		bool extractTemplateRows = ExtractTemplateRows;
		bool flag = RepeatLayout == RepeatLayout.Table;
		if (!flag || extractTemplateRows)
		{
			Style style = ((IRepeatInfoUser)this).GetItemStyle(itemType, repeatIndex);
			if (style != null)
			{
				dataListItem.ControlStyle.CopyFrom(style);
			}
		}
		dataListItem.RenderItem(writer, extractTemplateRows, flag);
	}
}
