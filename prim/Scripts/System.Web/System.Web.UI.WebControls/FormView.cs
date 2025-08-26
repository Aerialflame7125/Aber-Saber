using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays the values of a single record from a data source using user-defined templates. The <see cref="T:System.Web.UI.WebControls.FormView" /> control allows you to edit, delete, and insert records.</summary>
[SupportsEventValidation]
[Designer("System.Web.UI.Design.WebControls.FormViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ControlValueProperty("SelectedValue")]
[DefaultEvent("PageIndexChanging")]
[DataKeyProperty("DataKey")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class FormView : CompositeDataBoundControl, IDataItemContainer, INamingContainer, IPostBackEventHandler, IPostBackContainer, IDataBoundItemControl, IDataBoundControl, IRenderOuterTable
{
	private object dataItem;

	private Table table;

	private FormViewRow headerRow;

	private FormViewRow footerRow;

	private FormViewRow bottomPagerRow;

	private FormViewRow topPagerRow;

	private FormViewRow itemRow;

	private IOrderedDictionary currentEditRowKeys;

	private IOrderedDictionary currentEditNewValues;

	private IOrderedDictionary currentEditOldValues;

	private ITemplate pagerTemplate;

	private ITemplate emptyDataTemplate;

	private ITemplate headerTemplate;

	private ITemplate footerTemplate;

	private ITemplate editItemTemplate;

	private ITemplate insertItemTemplate;

	private ITemplate itemTemplate;

	private PropertyDescriptor[] cachedKeyProperties;

	private readonly string[] emptyKeys = new string[0];

	private readonly string unhandledEventExceptionMessage = "The FormView '{0}' fired event {1} which wasn't handled.";

	private PagerSettings pagerSettings;

	private TableItemStyle editRowStyle;

	private TableItemStyle insertRowStyle;

	private TableItemStyle emptyDataRowStyle;

	private TableItemStyle footerStyle;

	private TableItemStyle headerStyle;

	private TableItemStyle pagerStyle;

	private TableItemStyle rowStyle;

	private IOrderedDictionary _keyTable;

	private DataKey key;

	private DataKey oldEditValues;

	private bool renderOuterTable = true;

	private static readonly object PageIndexChangedEvent;

	private static readonly object PageIndexChangingEvent;

	private static readonly object ItemCommandEvent;

	private static readonly object ItemCreatedEvent;

	private static readonly object ItemDeletedEvent;

	private static readonly object ItemDeletingEvent;

	private static readonly object ItemInsertedEvent;

	private static readonly object ItemInsertingEvent;

	private static readonly object ModeChangingEvent;

	private static readonly object ModeChangedEvent;

	private static readonly object ItemUpdatedEvent;

	private static readonly object ItemUpdatingEvent;

	private int pageIndex;

	private FormViewMode currentMode;

	private bool hasCurrentMode;

	private int pageCount;

	private FormViewMode defaultMode;

	private string[] dataKeyNames;

	/// <summary>Gets or sets a value indicating whether the paging feature is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> to enable the paging feature; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[WebCategory("Paging")]
	[DefaultValue(false)]
	public virtual bool AllowPaging
	{
		get
		{
			object obj = ViewState["AllowPaging"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["AllowPaging"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets or sets the URL to an image to display in the background of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The URL to an image to display in the background of the <see cref="T:System.Web.UI.WebControls.FormView" /> control. The default is an empty string (""), which indicates that this property is not set.</returns>
	[UrlProperty]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string BackImageUrl
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).BackImageUrl;
			}
			return string.Empty;
		}
		set
		{
			((TableStyle)base.ControlStyle).BackImageUrl = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the pager row displayed at the bottom of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the bottom pager row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual FormViewRow BottomPagerRow
	{
		get
		{
			EnsureChildControls();
			return bottomPagerRow;
		}
	}

	/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
	/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. The default value is an empty string ("").</returns>
	[WebCategory("Accessibility")]
	[DefaultValue("")]
	[Localizable(true)]
	public virtual string Caption
	{
		get
		{
			object obj = ViewState["Caption"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["Caption"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values. The default is <see langword="TableCaptionAlign.NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values.</exception>
	[WebCategory("Accessibility")]
	[DefaultValue(TableCaptionAlign.NotSet)]
	public virtual TableCaptionAlign CaptionAlign
	{
		get
		{
			object obj = ViewState["CaptionAlign"];
			if (obj != null)
			{
				return (TableCaptionAlign)obj;
			}
			return TableCaptionAlign.NotSet;
		}
		set
		{
			ViewState["CaptionAlign"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
	/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
	[WebCategory("Layout")]
	[DefaultValue(-1)]
	public virtual int CellPadding
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			return -1;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellPadding = value;
		}
	}

	/// <summary>Gets or sets the amount of space between cells.</summary>
	/// <returns>The amount of space, in pixels, between cells. The default value is 0.</returns>
	[WebCategory("Layout")]
	[DefaultValue(0)]
	public virtual int CellSpacing
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			return 0;
		}
		set
		{
			((TableStyle)base.ControlStyle).CellSpacing = value;
		}
	}

	/// <summary>Gets the current data-entry mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> values.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public FormViewMode CurrentMode
	{
		get
		{
			if (!hasCurrentMode)
			{
				return DefaultMode;
			}
			return currentMode;
		}
		private set
		{
			hasCurrentMode = true;
			currentMode = value;
		}
	}

	/// <summary>Gets or sets the data-entry mode to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control returns after an update, insert, or cancel operation.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> values. The default is <see langword="FormViewMode.ReadOnly" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</exception>
	[DefaultValue(FormViewMode.ReadOnly)]
	[WebCategory("Behavior")]
	public virtual FormViewMode DefaultMode
	{
		get
		{
			return defaultMode;
		}
		set
		{
			defaultMode = value;
			RequireBinding();
		}
	}

	/// <summary>Gets or sets an array that contains the names of the key fields for the data source.</summary>
	/// <returns>An array that contains the names of the key fields for the data source.</returns>
	[DefaultValue(null)]
	[WebCategory("Data")]
	[TypeConverter(typeof(StringArrayConverter))]
	[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string[] DataKeyNames
	{
		get
		{
			if (dataKeyNames == null)
			{
				return emptyKeys;
			}
			return dataKeyNames;
		}
		set
		{
			dataKeyNames = value;
			RequireBinding();
		}
	}

	private IOrderedDictionary KeyTable
	{
		get
		{
			if (_keyTable == null)
			{
				_keyTable = new OrderedDictionary(DataKeyNames.Length);
			}
			return _keyTable;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKey" /> object that represents the primary key of the displayed record.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKey" /> object that represents the primary key of the displayed record.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual DataKey DataKey
	{
		get
		{
			if (key == null)
			{
				key = new DataKey(KeyTable);
			}
			return key;
		}
	}

	private DataKey OldEditValues
	{
		get
		{
			if (oldEditValues == null)
			{
				oldEditValues = new DataKey(new OrderedDictionary());
			}
			return oldEditValues;
		}
	}

	/// <summary>Gets or sets the custom content for an item in edit mode.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
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
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DefaultValue(null)]
	public TableItemStyle EditRowStyle
	{
		get
		{
			if (editRowStyle == null)
			{
				editRowStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					editRowStyle.TrackViewState();
				}
			}
			return editRowStyle;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the empty data row displayed when the data source bound to a <see cref="T:System.Web.UI.WebControls.FormView" /> control does not contain any records.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that allows you to set the appearance of the empty data row.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DefaultValue(null)]
	public TableItemStyle EmptyDataRowStyle
	{
		get
		{
			if (emptyDataRowStyle == null)
			{
				emptyDataRowStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					emptyDataRowStyle.TrackViewState();
				}
			}
			return emptyDataRowStyle;
		}
	}

	/// <summary>Gets or sets the user-defined content for the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to a data source that does not contain any records.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the empty data row. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate EmptyDataTemplate
	{
		get
		{
			return emptyDataTemplate;
		}
		set
		{
			emptyDataTemplate = value;
		}
	}

	/// <summary>Gets or sets the text to display in the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to a data source that does not contain any records.</summary>
	/// <returns>The text to display in the empty data row. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	public virtual string EmptyDataText
	{
		get
		{
			object obj = ViewState["EmptyDataText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["EmptyDataText"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual FormViewRow FooterRow
	{
		get
		{
			EnsureChildControls();
			return footerRow;
		}
	}

	/// <summary>Gets or sets the user-defined content for the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the footer row. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
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
		}
	}

	/// <summary>Gets or sets the text to display in the footer row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The text to display in the footer row. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	public virtual string FooterText
	{
		get
		{
			object obj = ViewState["FooterText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["FooterText"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableItemStyle FooterStyle
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

	/// <summary>Gets or sets the gridline style for a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default is <see langword="GridLines.None" />.</returns>
	[WebCategory("Appearance")]
	[DefaultValue(GridLines.None)]
	public virtual GridLines GridLines
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			return GridLines.None;
		}
		set
		{
			((TableStyle)base.ControlStyle).GridLines = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual FormViewRow HeaderRow
	{
		get
		{
			EnsureChildControls();
			return headerRow;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableItemStyle HeaderStyle
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

	/// <summary>Gets or sets the user-defined content for the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the header row. The default value is <see langword="null" />, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
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
		}
	}

	/// <summary>Gets or sets the text to display in the header row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The text to display in the header row. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	public virtual string HeaderText
	{
		get
		{
			object obj = ViewState["HeaderText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			ViewState["HeaderText"] = value;
			RequireBinding();
		}
	}

	/// <summary>Gets or sets the horizontal alignment of a <see cref="T:System.Web.UI.WebControls.FormView" /> control on the page.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is <see langword="HorizontalAlign.NotSet" />.</returns>
	[Category("Layout")]
	[DefaultValue(HorizontalAlign.NotSet)]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (base.ControlStyleCreated)
			{
				return ((TableStyle)base.ControlStyle).HorizontalAlign;
			}
			return HorizontalAlign.NotSet;
		}
		set
		{
			((TableStyle)base.ControlStyle).HorizontalAlign = value;
		}
	}

	/// <summary>Gets or sets the custom content for an item in insert mode.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in insert mode. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
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
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in insert mode.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in insert mode.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DefaultValue(null)]
	public TableItemStyle InsertRowStyle
	{
		get
		{
			if (insertRowStyle == null)
			{
				insertRowStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					insertRowStyle.TrackViewState();
				}
			}
			return insertRowStyle;
		}
	}

	/// <summary>Gets or sets the custom content for the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in read-only mode. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
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
		}
	}

	/// <summary>Gets the total number of pages required to display every record in the data source.</summary>
	/// <returns>The number of items in the underlying data source.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual int PageCount
	{
		get
		{
			return pageCount;
		}
		private set
		{
			pageCount = value;
		}
	}

	/// <summary>Gets or sets the index of the displayed page.</summary>
	/// <returns>The zero-based index of the data item being displayed in a <see cref="T:System.Web.UI.WebControls.FormView" /> control from the underlying data source.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
	[WebCategory("Paging")]
	[Bindable(true, BindingDirection.OneWay)]
	[DefaultValue(0)]
	public virtual int PageIndex
	{
		get
		{
			return pageIndex;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("PageIndex must be non-negative");
			}
			if (pageIndex != value && value != -1)
			{
				pageIndex = value;
				RequireBinding();
			}
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[WebCategory("Paging")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	public virtual PagerSettings PagerSettings
	{
		get
		{
			if (pagerSettings == null)
			{
				pagerSettings = new PagerSettings(this);
				if (base.IsTrackingViewState)
				{
					((IStateManager)pagerSettings).TrackViewState();
				}
			}
			return pagerSettings;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableItemStyle PagerStyle
	{
		get
		{
			if (pagerStyle == null)
			{
				pagerStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					pagerStyle.TrackViewState();
				}
			}
			return pagerStyle;
		}
	}

	/// <summary>Gets or sets the custom content for the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the pager row. The default value is null, which indicates that this property is not set.</returns>
	[DefaultValue(null)]
	[TemplateContainer(typeof(FormView))]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Browsable(false)]
	public virtual ITemplate PagerTemplate
	{
		get
		{
			return pagerTemplate;
		}
		set
		{
			pagerTemplate = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual FormViewRow Row
	{
		get
		{
			EnsureChildControls();
			return itemRow;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</returns>
	[WebCategory("Styles")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[NotifyParentProperty(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[DefaultValue(null)]
	public TableItemStyle RowStyle
	{
		get
		{
			if (rowStyle == null)
			{
				rowStyle = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					rowStyle.TrackViewState();
				}
			}
			return rowStyle;
		}
	}

	/// <summary>Gets the data key value of the current record in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The data key value of the current record in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object SelectedValue => DataKey.Value;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the pager row displayed at the top of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the top pager row in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual FormViewRow TopPagerRow
	{
		get
		{
			EnsureChildControls();
			return topPagerRow;
		}
	}

	/// <summary>Gets the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual object DataItem => dataItem;

	/// <summary>Gets the number of data items in the data source.</summary>
	/// <returns>The number of data items in the data source.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public int DataItemCount => PageCount;

	/// <summary>Gets the index of the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</summary>
	/// <returns>The index of the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual int DataItemIndex => PageIndex;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
	/// <returns>An object that represents the display index.</returns>
	int IDataItemContainer.DataItemIndex => DataItemIndex;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
	/// <returns>Always returns 0.</returns>
	int IDataItemContainer.DisplayIndex => PageIndex;

	/// <summary>Gets or sets a value that indicates whether a validator control will handle exceptions that occur during insert or update operations.</summary>
	/// <returns>
	///     <see langword="true" /> if a validator control will handle exceptions that occur during insert or update operations; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[MonoTODO("Make use of it in the code")]
	[DefaultValue(true)]
	public virtual bool EnableModelValidation { get; set; }

	/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a <see langword="table" /> element in order to apply inline styles.</summary>
	/// <returns>
	///     <see langword="true" /> if the control encloses rendered HTML in a <see langword="table" /> element; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public virtual bool RenderOuterTable
	{
		get
		{
			return renderOuterTable;
		}
		set
		{
			renderOuterTable = value;
		}
	}

	/// <summary>Gets the current mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The current mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	DataBoundControlMode IDataBoundItemControl.Mode => CurrentMode switch
	{
		FormViewMode.ReadOnly => DataBoundControlMode.ReadOnly, 
		FormViewMode.Edit => DataBoundControlMode.Edit, 
		FormViewMode.Insert => DataBoundControlMode.Insert, 
		_ => throw new InvalidOperationException("Unsupported mode value."), 
	};

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>Always returns <see langword="HtmlTextWriterTag.Table" />.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.FormView.PageIndex" /> property changes after a paging operation.</summary>
	public event EventHandler PageIndexChanged
	{
		add
		{
			base.Events.AddHandler(PageIndexChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PageIndexChangedEvent, value);
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.FormView.PageIndex" /> property changes before a paging operation.</summary>
	public event FormViewPageEventHandler PageIndexChanging
	{
		add
		{
			base.Events.AddHandler(PageIndexChangingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PageIndexChangingEvent, value);
		}
	}

	/// <summary>Occurs when a button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked.</summary>
	public event FormViewCommandEventHandler ItemCommand
	{
		add
		{
			base.Events.AddHandler(ItemCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemCommandEvent, value);
		}
	}

	/// <summary>Occurs after all the rows are created in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	public event EventHandler ItemCreated
	{
		add
		{
			base.Events.AddHandler(ItemCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemCreatedEvent, value);
		}
	}

	/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the delete operation.</summary>
	public event FormViewDeletedEventHandler ItemDeleted
	{
		add
		{
			base.Events.AddHandler(ItemDeletedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemDeletedEvent, value);
		}
	}

	/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the delete operation.</summary>
	public event FormViewDeleteEventHandler ItemDeleting
	{
		add
		{
			base.Events.AddHandler(ItemDeletingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemDeletingEvent, value);
		}
	}

	/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the insert operation.</summary>
	public event FormViewInsertedEventHandler ItemInserted
	{
		add
		{
			base.Events.AddHandler(ItemInsertedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemInsertedEvent, value);
		}
	}

	/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the insert operation.</summary>
	public event FormViewInsertEventHandler ItemInserting
	{
		add
		{
			base.Events.AddHandler(ItemInsertingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemInsertingEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.FormView" /> control switches between edit, insert, and read-only mode, but before the mode changes.</summary>
	public event FormViewModeEventHandler ModeChanging
	{
		add
		{
			base.Events.AddHandler(ModeChangingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ModeChangingEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.FormView" /> control switches between edit, insert, and read-only mode, but after the mode has changed.</summary>
	public event EventHandler ModeChanged
	{
		add
		{
			base.Events.AddHandler(ModeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ModeChangedEvent, value);
		}
	}

	/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the update operation.</summary>
	public event FormViewUpdatedEventHandler ItemUpdated
	{
		add
		{
			base.Events.AddHandler(ItemUpdatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemUpdatedEvent, value);
		}
	}

	/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the update operation.</summary>
	public event FormViewUpdateEventHandler ItemUpdating
	{
		add
		{
			base.Events.AddHandler(ItemUpdatingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemUpdatingEvent, value);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnPageIndexChanged(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[PageIndexChanged])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanging" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewPageEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the paging operation was not canceled, and an event handler is not registered for the event.</exception>
	protected virtual void OnPageIndexChanging(FormViewPageEventArgs e)
	{
		if (base.Events != null)
		{
			FormViewPageEventHandler formViewPageEventHandler = (FormViewPageEventHandler)base.Events[PageIndexChanging];
			if (formViewPageEventHandler != null)
			{
				formViewPageEventHandler(this, e);
				return;
			}
		}
		if (!base.IsBoundUsingDataSourceID)
		{
			throw new HttpException(string.Format(unhandledEventExceptionMessage, ID, "PageIndexChanging"));
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemCommand" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewCommandEventArgs" /> that contains the event data.</param>
	protected virtual void OnItemCommand(FormViewCommandEventArgs e)
	{
		if (base.Events != null)
		{
			((FormViewCommandEventHandler)base.Events[ItemCommand])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemCreated" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnItemCreated(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[ItemCreated])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleted" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs" /> that contains the event data.</param>
	protected virtual void OnItemDeleted(FormViewDeletedEventArgs e)
	{
		if (base.Events != null)
		{
			((FormViewDeletedEventHandler)base.Events[ItemDeleted])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserted" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewInsertedEventArgs" /> that contains the event data.</param>
	protected virtual void OnItemInserted(FormViewInsertedEventArgs e)
	{
		if (base.Events != null)
		{
			((FormViewInsertedEventHandler)base.Events[ItemInserted])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserting" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the insert operation, and an event handler is not registered for the event.</exception>
	protected virtual void OnItemInserting(FormViewInsertEventArgs e)
	{
		if (base.Events != null)
		{
			FormViewInsertEventHandler formViewInsertEventHandler = (FormViewInsertEventHandler)base.Events[ItemInserting];
			if (formViewInsertEventHandler != null)
			{
				formViewInsertEventHandler(this, e);
				return;
			}
		}
		if (!base.IsBoundUsingDataSourceID)
		{
			throw new HttpException(string.Format(unhandledEventExceptionMessage, ID, "ItemInserting"));
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleting" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewDeleteEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the delete operation, and an event handler is not registered for the event.</exception>
	protected virtual void OnItemDeleting(FormViewDeleteEventArgs e)
	{
		if (base.Events != null)
		{
			FormViewDeleteEventHandler formViewDeleteEventHandler = (FormViewDeleteEventHandler)base.Events[ItemDeleting];
			if (formViewDeleteEventHandler != null)
			{
				formViewDeleteEventHandler(this, e);
				return;
			}
		}
		if (!base.IsBoundUsingDataSourceID)
		{
			throw new HttpException(string.Format(unhandledEventExceptionMessage, ID, "ItemDeleting"));
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnModeChanged(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[ModeChanged])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewModeEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the mode change was not canceled, and an event handler is not registered for the event.</exception>
	protected virtual void OnModeChanging(FormViewModeEventArgs e)
	{
		if (base.Events != null)
		{
			FormViewModeEventHandler formViewModeEventHandler = (FormViewModeEventHandler)base.Events[ModeChanging];
			if (formViewModeEventHandler != null)
			{
				formViewModeEventHandler(this, e);
				return;
			}
		}
		if (!base.IsBoundUsingDataSourceID)
		{
			throw new HttpException(string.Format(unhandledEventExceptionMessage, ID, "ModeChanging"));
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdated" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs" /> that contains the event data.</param>
	protected virtual void OnItemUpdated(FormViewUpdatedEventArgs e)
	{
		if (base.Events != null)
		{
			((FormViewUpdatedEventHandler)base.Events[ItemUpdated])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdating" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs" /> that contains the event data.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the update operation, and an event handler is not registered for the event.</exception>
	protected virtual void OnItemUpdating(FormViewUpdateEventArgs e)
	{
		if (base.Events != null)
		{
			FormViewUpdateEventHandler formViewUpdateEventHandler = (FormViewUpdateEventHandler)base.Events[ItemUpdating];
			if (formViewUpdateEventHandler != null)
			{
				formViewUpdateEventHandler(this, e);
				return;
			}
		}
		if (!base.IsBoundUsingDataSourceID)
		{
			throw new HttpException(string.Format(unhandledEventExceptionMessage, ID, "ItemUpdating"));
		}
	}

	/// <summary>Determines whether the table-specific CSS style rules that are associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control are set to their default values.</summary>
	/// <returns>The default CSS style rules that are associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control. </returns>
	protected internal virtual string ModifiedOuterTableStylePropertyName()
	{
		if (BackImageUrl != string.Empty)
		{
			return "BackImageUrl";
		}
		if (CellPadding != -1)
		{
			return "CellPadding";
		}
		if (CellSpacing != 0)
		{
			return "CellSpacing";
		}
		if (GridLines != 0)
		{
			return "GridLines";
		}
		if (HorizontalAlign != 0)
		{
			return "HorizontalAlign";
		}
		if (base.ControlStyle.CheckBit(65024))
		{
			return "Font";
		}
		return string.Empty;
	}

	internal override string InlinePropertiesSet()
	{
		string text = base.InlinePropertiesSet();
		string text2 = ModifiedOuterTableStylePropertyName();
		if (string.IsNullOrEmpty(text2))
		{
			return text;
		}
		if (string.IsNullOrEmpty(text))
		{
			return text2;
		}
		return text + ", " + text2;
	}

	/// <summary>Determines whether the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to check.</param>
	/// <returns>
	///     <see langword="true" /> if the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.FormView" /> control; otherwise, <see langword="false" />.</returns>
	public virtual bool IsBindableType(Type type)
	{
		if (!type.IsPrimitive && !(type == typeof(string)) && !(type == typeof(DateTime)) && !(type == typeof(Guid)))
		{
			return type == typeof(decimal);
		}
		return true;
	}

	/// <summary>Creates the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that contains the arguments that are passed to the data source for processing.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that contains the arguments that are passed to the data source.</returns>
	protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
	{
		DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
		DataSourceView data = GetData();
		if (AllowPaging && data.CanPage)
		{
			dataSourceSelectArguments.StartRowIndex = PageIndex;
			if (data.CanRetrieveTotalRowCount)
			{
				dataSourceSelectArguments.RetrieveTotalRowCount = true;
				dataSourceSelectArguments.MaximumRows = 1;
			}
			else
			{
				dataSourceSelectArguments.MaximumRows = -1;
			}
		}
		return dataSourceSelectArguments;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object using the specified item index, row type, and row state.</summary>
	/// <param name="itemIndex">The zero-based index of the data item to display.</param>
	/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
	/// <param name="rowState">A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> with the specified item index, row type, and row state.</returns>
	protected virtual FormViewRow CreateRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
	{
		if (rowType == DataControlRowType.Pager)
		{
			return new FormViewPagerRow(itemIndex, rowType, rowState);
		}
		return new FormViewRow(itemIndex, rowType, rowState);
	}

	private void RequireBinding()
	{
		if (base.Initialized)
		{
			base.RequiresDataBinding = true;
		}
	}

	/// <summary>Creates the containing table for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Table" /> that represents the containing table for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	protected virtual Table CreateTable()
	{
		return new ContainedTable(this);
	}

	/// <summary>Makes certain that the <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to data when appropriate.</summary>
	protected override void EnsureDataBound()
	{
		if (CurrentMode == FormViewMode.Insert)
		{
			if (base.RequiresDataBinding)
			{
				OnDataBinding(EventArgs.Empty);
				base.RequiresDataBinding = false;
				InternalPerformDataBinding(null);
				MarkAsDataBound();
				OnDataBound(EventArgs.Empty);
			}
		}
		else
		{
			base.EnsureDataBound();
		}
	}

	/// <summary>Creates a default table style object for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the default table style for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle(ViewState)
		{
			CellSpacing = 0
		};
	}

	/// <summary>Creates the control hierarchy used to render the <see cref="T:System.Web.UI.WebControls.FormView" /> control with the specified data source.</summary>
	/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source used to create the control hierarchy.</param>
	/// <param name="dataBinding">
	///       <see langword="true" /> to indicate that the control hierarchy is created directly from the data source; <see langword="false" /> to indicate the control hierarchy is created from the view state.</param>
	/// <returns>The number of items created from the data source.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.DataSourceView" /> of the <see cref="T:System.Web.UI.IDataSource" /> to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound is <see langword="null" />.</exception>
	protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
	{
		PagedDataSource pagedDataSource = new PagedDataSource();
		pagedDataSource.DataSource = ((CurrentMode != FormViewMode.Insert) ? dataSource : null);
		pagedDataSource.AllowPaging = AllowPaging;
		pagedDataSource.PageSize = 1;
		pagedDataSource.CurrentPageIndex = PageIndex;
		if (dataBinding && CurrentMode != FormViewMode.Insert)
		{
			DataSourceView data = GetData();
			if (data != null && data.CanPage)
			{
				pagedDataSource.AllowServerPaging = true;
				if (base.SelectArguments.RetrieveTotalRowCount)
				{
					pagedDataSource.VirtualCount = base.SelectArguments.TotalRowCount;
				}
			}
		}
		PagerSettings pagerSettings = PagerSettings;
		bool flag = AllowPaging && pagerSettings.Visible && pagedDataSource.PageCount > 1;
		Controls.Clear();
		table = CreateTable();
		Controls.Add(table);
		headerRow = null;
		footerRow = null;
		topPagerRow = null;
		bottomPagerRow = null;
		if (AllowPaging)
		{
			PageCount = pagedDataSource.DataSourceCount;
			if (PageIndex >= PageCount && PageCount > 0)
			{
				int num2 = (pagedDataSource.CurrentPageIndex = PageCount - 1);
				pageIndex = num2;
			}
			if (pagedDataSource.DataSource != null)
			{
				IEnumerator enumerator = pagedDataSource.GetEnumerator();
				if (enumerator.MoveNext())
				{
					dataItem = enumerator.Current;
				}
			}
		}
		else
		{
			int num3 = 0;
			object obj = null;
			if (pagedDataSource.DataSource != null)
			{
				IEnumerator enumerator2 = pagedDataSource.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					obj = enumerator2.Current;
					if (num3 == PageIndex)
					{
						dataItem = enumerator2.Current;
					}
					num3++;
				}
			}
			PageCount = num3;
			if (PageIndex >= PageCount && PageCount > 0)
			{
				pageIndex = PageCount - 1;
				dataItem = obj;
			}
		}
		bool flag2 = PageCount == 0 && CurrentMode != FormViewMode.Insert;
		if (!flag2)
		{
			headerRow = CreateRow(-1, DataControlRowType.Header, DataControlRowState.Normal);
			InitializeRow(headerRow);
			table.Rows.Add(headerRow);
		}
		if ((flag && pagerSettings.Position == PagerPosition.Top) || pagerSettings.Position == PagerPosition.TopAndBottom)
		{
			topPagerRow = CreateRow(-1, DataControlRowType.Pager, DataControlRowState.Normal);
			InitializePager(topPagerRow, pagedDataSource);
			table.Rows.Add(topPagerRow);
		}
		if (PageCount > 0)
		{
			DataControlRowState rowState = GetRowState();
			itemRow = CreateRow(0, DataControlRowType.DataRow, rowState);
			InitializeRow(itemRow);
			table.Rows.Add(itemRow);
		}
		else
		{
			switch (CurrentMode)
			{
			case FormViewMode.Edit:
				itemRow = CreateRow(-1, DataControlRowType.EmptyDataRow, DataControlRowState.Edit);
				break;
			case FormViewMode.Insert:
				itemRow = CreateRow(-1, DataControlRowType.DataRow, DataControlRowState.Insert);
				break;
			default:
				itemRow = CreateRow(-1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
				break;
			}
			InitializeRow(itemRow);
			table.Rows.Add(itemRow);
		}
		if (!flag2)
		{
			footerRow = CreateRow(-1, DataControlRowType.Footer, DataControlRowState.Normal);
			InitializeRow(footerRow);
			table.Rows.Add(footerRow);
		}
		if ((flag && pagerSettings.Position == PagerPosition.Bottom) || pagerSettings.Position == PagerPosition.TopAndBottom)
		{
			bottomPagerRow = CreateRow(0, DataControlRowType.Pager, DataControlRowState.Normal);
			InitializePager(bottomPagerRow, pagedDataSource);
			table.Rows.Add(bottomPagerRow);
		}
		OnItemCreated(EventArgs.Empty);
		if (dataBinding)
		{
			DataBind(raiseOnDataBinding: false);
		}
		return PageCount;
	}

	private DataControlRowState GetRowState()
	{
		DataControlRowState dataControlRowState = DataControlRowState.Normal;
		if (CurrentMode == FormViewMode.Edit)
		{
			dataControlRowState |= DataControlRowState.Edit;
		}
		else if (CurrentMode == FormViewMode.Insert)
		{
			dataControlRowState |= DataControlRowState.Insert;
		}
		return dataControlRowState;
	}

	/// <summary>Creates the pager row for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that contains the pager row.</param>
	/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that contains the data for the current page.</param>
	protected virtual void InitializePager(FormViewRow row, PagedDataSource pagedDataSource)
	{
		TableCell tableCell = new TableCell();
		tableCell.ColumnSpan = 2;
		if (pagerTemplate != null)
		{
			pagerTemplate.InstantiateIn(tableCell);
		}
		else
		{
			tableCell.Controls.Add(PagerSettings.CreatePagerControl(pagedDataSource.CurrentPageIndex, pagedDataSource.PageCount));
		}
		row.Cells.Add(tableCell);
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
	/// <param name="row">The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> to initialize.</param>
	protected virtual void InitializeRow(FormViewRow row)
	{
		TableCell tableCell = new TableCell();
		if (row.RowType == DataControlRowType.DataRow)
		{
			if ((row.RowState & DataControlRowState.Edit) != 0)
			{
				if (editItemTemplate != null)
				{
					editItemTemplate.InstantiateIn(tableCell);
				}
				else
				{
					row.Visible = false;
				}
			}
			else if ((row.RowState & DataControlRowState.Insert) != 0)
			{
				if (insertItemTemplate != null)
				{
					insertItemTemplate.InstantiateIn(tableCell);
				}
				else
				{
					row.Visible = false;
				}
			}
			else if (itemTemplate != null)
			{
				itemTemplate.InstantiateIn(tableCell);
			}
			else
			{
				row.Visible = false;
			}
		}
		else if (row.RowType == DataControlRowType.EmptyDataRow)
		{
			if (emptyDataTemplate != null)
			{
				emptyDataTemplate.InstantiateIn(tableCell);
			}
			else if (!string.IsNullOrEmpty(EmptyDataText))
			{
				tableCell.Text = EmptyDataText;
			}
			else
			{
				row.Visible = false;
			}
		}
		else if (row.RowType == DataControlRowType.Footer)
		{
			if (footerTemplate != null)
			{
				footerTemplate.InstantiateIn(tableCell);
			}
			else if (!string.IsNullOrEmpty(FooterText))
			{
				tableCell.Text = FooterText;
			}
			else
			{
				row.Visible = false;
			}
		}
		else if (row.RowType == DataControlRowType.Header)
		{
			if (headerTemplate != null)
			{
				headerTemplate.InstantiateIn(tableCell);
			}
			else if (!string.IsNullOrEmpty(HeaderText))
			{
				tableCell.Text = HeaderText;
			}
			else
			{
				row.Visible = false;
			}
		}
		tableCell.ColumnSpan = 2;
		row.Cells.Add(tableCell);
		row.RenderJustCellContents = !RenderOuterTable;
	}

	private void FillRowDataKey(object dataItem)
	{
		KeyTable.Clear();
		if (cachedKeyProperties == null)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			cachedKeyProperties = new PropertyDescriptor[DataKeyNames.Length];
			for (int i = 0; i < DataKeyNames.Length; i++)
			{
				PropertyDescriptor propertyDescriptor = properties.Find(DataKeyNames[i], ignoreCase: true);
				if (propertyDescriptor == null)
				{
					throw new InvalidOperationException("Property '" + DataKeyNames[i] + "' not found in object of type " + dataItem.GetType());
				}
				cachedKeyProperties[i] = propertyDescriptor;
			}
		}
		PropertyDescriptor[] array = cachedKeyProperties;
		foreach (PropertyDescriptor propertyDescriptor2 in array)
		{
			KeyTable[propertyDescriptor2.Name] = propertyDescriptor2.GetValue(dataItem);
		}
	}

	private IOrderedDictionary GetRowValues(bool includePrimaryKey)
	{
		OrderedDictionary orderedDictionary = new OrderedDictionary();
		ExtractRowValues(orderedDictionary, includePrimaryKey);
		return orderedDictionary;
	}

	/// <summary>Retrieves the values of each field declared within the data row and stores them in the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object.</summary>
	/// <param name="fieldValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the field values of the current data item.</param>
	/// <param name="includeKeys">
	///       <see langword="true" /> to include key fields; otherwise, <see langword="false" />.</param>
	protected virtual void ExtractRowValues(IOrderedDictionary fieldValues, bool includeKeys)
	{
		FormViewRow row = Row;
		if (row == null)
		{
			return;
		}
		DataControlRowState rowState = row.RowState;
		IBindableTemplate bindableTemplate;
		if ((rowState & DataControlRowState.Insert) != 0)
		{
			bindableTemplate = insertItemTemplate as IBindableTemplate;
		}
		else
		{
			if ((rowState & DataControlRowState.Edit) == 0)
			{
				return;
			}
			bindableTemplate = editItemTemplate as IBindableTemplate;
		}
		if (bindableTemplate == null)
		{
			return;
		}
		IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(row.Cells[0]);
		if (orderedDictionary == null)
		{
			return;
		}
		foreach (DictionaryEntry item in orderedDictionary)
		{
			if (includeKeys || Array.IndexOf(DataKeyNames, item.Key) == -1)
			{
				fieldValues[item.Key] = item.Value;
			}
		}
	}

	/// <summary>Binds the data source to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	public sealed override void DataBind()
	{
		cachedKeyProperties = null;
		base.DataBind();
		if (pageCount > 0)
		{
			if (CurrentMode == FormViewMode.Edit)
			{
				oldEditValues = new DataKey(GetRowValues(includePrimaryKey: true));
			}
			FillRowDataKey(dataItem);
			key = new DataKey(KeyTable);
		}
	}

	/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
	protected internal override void PerformDataBinding(IEnumerable data)
	{
		base.PerformDataBinding(data);
	}

	/// <summary>Sets up the control hierarchy of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	protected internal virtual void PrepareControlHierarchy()
	{
		if (table == null)
		{
			return;
		}
		table.Caption = Caption;
		table.CaptionAlign = CaptionAlign;
		foreach (FormViewRow row in table.Rows)
		{
			switch (row.RowType)
			{
			case DataControlRowType.Header:
				if (headerStyle != null && !headerStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(headerStyle);
				}
				break;
			case DataControlRowType.Footer:
				if (footerStyle != null && !footerStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(footerStyle);
				}
				break;
			case DataControlRowType.Pager:
				if (pagerStyle != null && !pagerStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(pagerStyle);
				}
				break;
			case DataControlRowType.EmptyDataRow:
				if (emptyDataRowStyle != null && !emptyDataRowStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(emptyDataRowStyle);
				}
				break;
			case DataControlRowType.DataRow:
				if (rowStyle != null && !rowStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(rowStyle);
				}
				if ((row.RowState & (DataControlRowState.Edit | DataControlRowState.Insert)) != 0 && editRowStyle != null && !editRowStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(editRowStyle);
				}
				if ((row.RowState & DataControlRowState.Insert) != 0 && insertRowStyle != null && !insertRowStyle.IsEmpty)
				{
					row.ControlStyle.CopyFrom(insertRowStyle);
				}
				break;
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		Page.RegisterRequiresControlState(this);
		base.OnInit(e);
	}

	/// <summary>Handles an event passed up through the control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	/// <returns>
	///     <see langword="true" /> to indicate the event should be passed further up the control hierarchy; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (e is FormViewCommandEventArgs formViewCommandEventArgs)
		{
			bool causesValidation = false;
			if (formViewCommandEventArgs.CommandSource is IButtonControl { CausesValidation: not false } buttonControl)
			{
				Page.Validate(buttonControl.ValidationGroup);
				causesValidation = true;
			}
			ProcessCommand(formViewCommandEventArgs, causesValidation);
			return true;
		}
		return base.OnBubbleEvent(source, e);
	}

	private void ProcessCommand(FormViewCommandEventArgs args, bool causesValidation)
	{
		OnItemCommand(args);
		ProcessEvent(args.CommandName, args.CommandArgument as string, causesValidation);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.FormView" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises the appropriate events for the <see cref="T:System.Web.UI.WebControls.FormView" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The event argument from which to create a <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> for the event or events that are raised.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		int num = eventArgument.IndexOf('$');
		CommandEventArgs originalArgs = ((num == -1) ? new CommandEventArgs(eventArgument, null) : new CommandEventArgs(eventArgument.Substring(0, num), eventArgument.Substring(num + 1)));
		ProcessCommand(new FormViewCommandEventArgs(this, originalArgs), causesValidation: false);
	}

	private void ProcessEvent(string eventName, string param, bool causesValidation)
	{
		switch (eventName)
		{
		case "Page":
		{
			int num = -1;
			switch (param)
			{
			case "First":
				num = 0;
				break;
			case "Last":
				num = PageCount - 1;
				break;
			case "Next":
				num = PageIndex + 1;
				break;
			case "Prev":
				num = PageIndex - 1;
				break;
			default:
			{
				int result = 0;
				int.TryParse(param, out result);
				num = result - 1;
				break;
			}
			}
			SetPageIndex(num);
			break;
		}
		case "First":
			SetPageIndex(0);
			break;
		case "Last":
			SetPageIndex(PageCount - 1);
			break;
		case "Next":
			if (PageIndex < PageCount - 1)
			{
				SetPageIndex(PageIndex + 1);
			}
			break;
		case "Prev":
			if (PageIndex > 0)
			{
				SetPageIndex(PageIndex - 1);
			}
			break;
		case "Edit":
			ProcessChangeMode(FormViewMode.Edit, cancelingEdit: false);
			break;
		case "New":
			ProcessChangeMode(FormViewMode.Insert, cancelingEdit: false);
			break;
		case "Update":
			UpdateItem(param, causesValidation);
			break;
		case "Cancel":
			CancelEdit();
			break;
		case "Delete":
			DeleteItem();
			break;
		case "Insert":
			InsertItem(causesValidation);
			break;
		}
	}

	/// <summary>Sets the index of the currently displayed page in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="index">The index to set.</param>
	public void SetPageIndex(int index)
	{
		FormViewPageEventArgs formViewPageEventArgs = new FormViewPageEventArgs(index);
		OnPageIndexChanging(formViewPageEventArgs);
		if (!formViewPageEventArgs.Cancel && base.IsBoundUsingDataSourceID)
		{
			int newPageIndex = formViewPageEventArgs.NewPageIndex;
			if (newPageIndex >= 0 && newPageIndex < PageCount)
			{
				EndRowEdit(switchToDefaultMode: false, cancelingEdit: false);
				PageIndex = newPageIndex;
				OnPageIndexChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Switches the <see cref="T:System.Web.UI.WebControls.FormView" /> control to the specified data-entry mode.</summary>
	/// <param name="newMode">One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</param>
	public void ChangeMode(FormViewMode newMode)
	{
		if (CurrentMode != newMode)
		{
			CurrentMode = newMode;
			RequireBinding();
		}
	}

	private void ProcessChangeMode(FormViewMode newMode, bool cancelingEdit)
	{
		FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(newMode, cancelingEdit);
		OnModeChanging(formViewModeEventArgs);
		if (!formViewModeEventArgs.Cancel && base.IsBoundUsingDataSourceID)
		{
			ChangeMode(formViewModeEventArgs.NewMode);
			OnModeChanged(EventArgs.Empty);
		}
	}

	private void CancelEdit()
	{
		EndRowEdit(switchToDefaultMode: true, cancelingEdit: true);
	}

	/// <summary>Updates the current record in the data source.</summary>
	/// <param name="causesValidation">
	///       <see langword="true" /> to perform page validation when the method is called; otherwise <see langword="false" />.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not in edit mode.-or-The <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control is null.</exception>
	public virtual void UpdateItem(bool causesValidation)
	{
		UpdateItem(null, causesValidation);
	}

	private void UpdateItem(string param, bool causesValidation)
	{
		if (!causesValidation || Page == null || Page.IsValid)
		{
			if (currentMode != FormViewMode.Edit)
			{
				throw new HttpException("Must be in Edit mode");
			}
			currentEditOldValues = OldEditValues.Values;
			currentEditRowKeys = DataKey.Values;
			currentEditNewValues = GetRowValues(includePrimaryKey: true);
			FormViewUpdateEventArgs formViewUpdateEventArgs = new FormViewUpdateEventArgs(param, currentEditRowKeys, currentEditOldValues, currentEditNewValues);
			OnItemUpdating(formViewUpdateEventArgs);
			if (!formViewUpdateEventArgs.Cancel && base.IsBoundUsingDataSourceID)
			{
				(GetData() ?? throw new HttpException("The DataSourceView associated to data bound control was null")).Update(currentEditRowKeys, currentEditNewValues, currentEditOldValues, UpdateCallback);
			}
		}
	}

	private bool UpdateCallback(int recordsAffected, Exception exception)
	{
		FormViewUpdatedEventArgs formViewUpdatedEventArgs = new FormViewUpdatedEventArgs(recordsAffected, exception, currentEditRowKeys, currentEditOldValues, currentEditNewValues);
		OnItemUpdated(formViewUpdatedEventArgs);
		if (!formViewUpdatedEventArgs.KeepInEditMode)
		{
			EndRowEdit(switchToDefaultMode: true, cancelingEdit: false);
		}
		return formViewUpdatedEventArgs.ExceptionHandled;
	}

	/// <summary>Inserts the current record in the data source.</summary>
	/// <param name="causesValidation">
	///       <see langword="true" /> to perform page validation when the method is called; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not in insert mode.-or-The <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control is null.</exception>
	public virtual void InsertItem(bool causesValidation)
	{
		InsertItem(null, causesValidation);
	}

	private void InsertItem(string param, bool causesValidation)
	{
		if (!causesValidation || Page == null || Page.IsValid)
		{
			if (currentMode != FormViewMode.Insert)
			{
				throw new HttpException("Must be in Insert mode");
			}
			currentEditNewValues = GetRowValues(includePrimaryKey: true);
			FormViewInsertEventArgs formViewInsertEventArgs = new FormViewInsertEventArgs(param, currentEditNewValues);
			OnItemInserting(formViewInsertEventArgs);
			if (!formViewInsertEventArgs.Cancel && base.IsBoundUsingDataSourceID)
			{
				(GetData() ?? throw new HttpException("The DataSourceView associated to data bound control was null")).Insert(currentEditNewValues, InsertCallback);
			}
		}
	}

	private bool InsertCallback(int recordsAffected, Exception exception)
	{
		FormViewInsertedEventArgs formViewInsertedEventArgs = new FormViewInsertedEventArgs(recordsAffected, exception, currentEditNewValues);
		OnItemInserted(formViewInsertedEventArgs);
		if (!formViewInsertedEventArgs.KeepInInsertMode)
		{
			EndRowEdit(switchToDefaultMode: true, cancelingEdit: false);
		}
		return formViewInsertedEventArgs.ExceptionHandled;
	}

	/// <summary>Deletes the current record in the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</summary>
	public virtual void DeleteItem()
	{
		currentEditRowKeys = DataKey.Values;
		currentEditNewValues = GetRowValues(includePrimaryKey: true);
		FormViewDeleteEventArgs formViewDeleteEventArgs = new FormViewDeleteEventArgs(PageIndex, currentEditRowKeys, currentEditNewValues);
		OnItemDeleting(formViewDeleteEventArgs);
		if (!formViewDeleteEventArgs.Cancel && base.IsBoundUsingDataSourceID)
		{
			if (PageIndex > 0 && PageIndex == PageCount - 1)
			{
				PageIndex--;
			}
			RequireBinding();
			DataSourceView data = GetData();
			if (data != null)
			{
				data.Delete(currentEditRowKeys, currentEditNewValues, DeleteCallback);
				return;
			}
			FormViewDeletedEventArgs e = new FormViewDeletedEventArgs(0, null, currentEditRowKeys, currentEditNewValues);
			OnItemDeleted(e);
		}
	}

	private bool DeleteCallback(int recordsAffected, Exception exception)
	{
		FormViewDeletedEventArgs formViewDeletedEventArgs = new FormViewDeletedEventArgs(recordsAffected, exception, currentEditRowKeys, currentEditNewValues);
		OnItemDeleted(formViewDeletedEventArgs);
		return formViewDeletedEventArgs.ExceptionHandled;
	}

	private void EndRowEdit(bool switchToDefaultMode, bool cancelingEdit)
	{
		if (switchToDefaultMode)
		{
			ProcessChangeMode(DefaultMode, cancelingEdit);
		}
		oldEditValues = new DataKey(new OrderedDictionary());
		currentEditRowKeys = null;
		currentEditOldValues = null;
		currentEditNewValues = null;
		RequireBinding();
	}

	/// <summary>Loads the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control properties that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to <see langword="false" />.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
	protected internal override void LoadControlState(object savedState)
	{
		if (savedState != null)
		{
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			pageIndex = (int)array[1];
			pageCount = (int)array[2];
			CurrentMode = (FormViewMode)array[3];
			defaultMode = (FormViewMode)array[4];
			dataKeyNames = (string[])array[5];
			if (array[6] != null)
			{
				((IStateManager)DataKey).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)OldEditValues).LoadViewState(array[7]);
			}
		}
	}

	/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control properties that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to <see langword="false" />.</summary>
	/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns <see langword="null" />.</returns>
	protected internal override object SaveControlState()
	{
		object obj = base.SaveControlState();
		return new object[8]
		{
			obj,
			pageIndex,
			pageCount,
			CurrentMode,
			defaultMode,
			dataKeyNames,
			(key == null) ? null : ((IStateManager)key).SaveViewState(),
			(oldEditValues == null) ? null : ((IStateManager)oldEditValues).SaveViewState()
		};
	}

	/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (pagerSettings != null)
		{
			((IStateManager)pagerSettings).TrackViewState();
		}
		if (footerStyle != null)
		{
			((IStateManager)footerStyle).TrackViewState();
		}
		if (headerStyle != null)
		{
			((IStateManager)headerStyle).TrackViewState();
		}
		if (pagerStyle != null)
		{
			((IStateManager)pagerStyle).TrackViewState();
		}
		if (rowStyle != null)
		{
			((IStateManager)rowStyle).TrackViewState();
		}
		if (editRowStyle != null)
		{
			((IStateManager)editRowStyle).TrackViewState();
		}
		if (insertRowStyle != null)
		{
			((IStateManager)insertRowStyle).TrackViewState();
		}
		if (emptyDataRowStyle != null)
		{
			((IStateManager)emptyDataRowStyle).TrackViewState();
		}
	}

	/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[10]
		{
			base.SaveViewState(),
			(pagerSettings == null) ? null : ((IStateManager)pagerSettings).SaveViewState(),
			(footerStyle == null) ? null : ((IStateManager)footerStyle).SaveViewState(),
			(headerStyle == null) ? null : ((IStateManager)headerStyle).SaveViewState(),
			(pagerStyle == null) ? null : ((IStateManager)pagerStyle).SaveViewState(),
			(rowStyle == null) ? null : ((IStateManager)rowStyle).SaveViewState(),
			(insertRowStyle == null) ? null : ((IStateManager)insertRowStyle).SaveViewState(),
			(editRowStyle == null) ? null : ((IStateManager)editRowStyle).SaveViewState(),
			(emptyDataRowStyle == null) ? null : ((IStateManager)emptyDataRowStyle).SaveViewState(),
			null
		};
		for (int num = array.Length - 1; num >= 0; num--)
		{
			if (array[num] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState == null)
		{
			base.LoadViewState((object)null);
			return;
		}
		object[] array = (object[])savedState;
		base.LoadViewState(array[0]);
		if (array[1] != null)
		{
			((IStateManager)PagerSettings).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			((IStateManager)FooterStyle).LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			((IStateManager)HeaderStyle).LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			((IStateManager)PagerStyle).LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			((IStateManager)RowStyle).LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			((IStateManager)InsertRowStyle).LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			((IStateManager)EditRowStyle).LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			((IStateManager)EmptyDataRowStyle).LoadViewState(array[8]);
		}
	}

	/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.FormView" /> control on the client.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		VerifyInlinePropertiesNotSet();
		if (RenderOuterTable)
		{
			PrepareControlHierarchy();
			if (table != null)
			{
				table.Render(writer);
			}
		}
		else if (table != null)
		{
			table.RenderChildren(writer);
		}
	}

	/// <summary>Determines the postback event options for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <param name="buttonControl">The button control that posted the page back to the server.</param>
	/// <returns>The postback event options for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	/// <exception cref="T:System.ArgumentNullException">The object contained in the <paramref name="buttonControl" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.IButtonControl.CausesValidation" /> property of <paramref name="buttonControl" /> is <see langword="true" />.</exception>
	PostBackOptions IPostBackContainer.GetPostBackOptions(IButtonControl control)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		if (control.CausesValidation)
		{
			throw new InvalidOperationException("A button that causes validation in FormView '" + ID + "' is attempting to use the container GridView as the post back target.  The button should either turn off validation or use itself as the post back container.");
		}
		return new PostBackOptions(this)
		{
			Argument = control.CommandName + "$" + control.CommandArgument,
			RequiresJavaScriptProtocol = true
		};
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormView" /> class.</summary>
	public FormView()
	{
	}

	static FormView()
	{
		PageIndexChanged = new object();
		PageIndexChanging = new object();
		ItemCommand = new object();
		ItemCreated = new object();
		ItemDeleted = new object();
		ItemDeleting = new object();
		ItemInserted = new object();
		ItemInserting = new object();
		ModeChanging = new object();
		ModeChanged = new object();
		ItemUpdated = new object();
		ItemUpdating = new object();
	}
}
