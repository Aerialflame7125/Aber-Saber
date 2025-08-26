using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>A data bound list control that displays the items from data source in a table. The <see cref="T:System.Web.UI.WebControls.DataGrid" /> control allows you to select, sort, and edit these items.</summary>
[Editor("System.Web.UI.Design.WebControls.DataGridComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(ComponentEditor))]
[Designer("System.Web.UI.Design.WebControls.DataGridDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataGrid : BaseDataList, INamingContainer
{
	private sealed class NCollection : ICollection, IEnumerable
	{
		private int n;

		public int Count => n;

		public bool IsSynchronized => false;

		public object SyncRoot => this;

		public NCollection(int n)
		{
			this.n = n;
		}

		public IEnumerator GetEnumerator()
		{
			for (int i = 0; i < n; i++)
			{
				yield return i;
			}
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException("This should never be called");
		}
	}

	/// <summary>Represents the <see langword="Cancel" /> command name. This field is read-only.</summary>
	public const string CancelCommandName = "Cancel";

	/// <summary>Represents the Delete command name. This field is read-only.</summary>
	public const string DeleteCommandName = "Delete";

	/// <summary>Represents the Edit command name. This field is read-only.</summary>
	public const string EditCommandName = "Edit";

	/// <summary>Represents the Select command name. This field is read-only.</summary>
	public const string SelectCommandName = "Select";

	/// <summary>Represents the Sort command name. This field is read-only.</summary>
	public const string SortCommandName = "Sort";

	/// <summary>Represents the Update command name. This field is read-only.</summary>
	public const string UpdateCommandName = "Update";

	/// <summary>Represents the Page command name. This field is read-only.</summary>
	public const string PageCommandName = "Page";

	/// <summary>Represents the Next command argument. This field is read-only.</summary>
	public const string NextPageCommandArgument = "Next";

	/// <summary>Represents the Prev command argument. This field is read-only.</summary>
	public const string PrevPageCommandArgument = "Prev";

	private static readonly object CancelCommandEvent;

	private static readonly object DeleteCommandEvent;

	private static readonly object EditCommandEvent;

	private static readonly object ItemCommandEvent;

	private static readonly object ItemCreatedEvent;

	private static readonly object ItemDataBoundEvent;

	private static readonly object PageIndexChangedEvent;

	private static readonly object SortCommandEvent;

	private static readonly object UpdateCommandEvent;

	private TableItemStyle alt_item_style;

	private TableItemStyle edit_item_style;

	private TableItemStyle footer_style;

	private TableItemStyle header_style;

	private TableItemStyle item_style;

	private TableItemStyle selected_style;

	private DataGridPagerStyle pager_style;

	private ArrayList items_list;

	private DataGridItemCollection items;

	private ArrayList columns_list;

	private DataGridColumnCollection columns;

	private ArrayList data_source_columns_list;

	private DataGridColumnCollection data_source_columns;

	private Table render_table;

	private DataGridColumn[] render_columns;

	private PagedDataSource paged_data_source;

	private IEnumerator data_enumerator;

	private static Type[] item_args;

	/// <summary>Gets or sets a value that indicates whether custom paging is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if custom paging is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Paging")]
	public virtual bool AllowCustomPaging
	{
		get
		{
			return ViewState.GetBool("AllowCustomPaging", def: false);
		}
		set
		{
			ViewState["AllowCustomPaging"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether paging is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if paging is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Paging")]
	public virtual bool AllowPaging
	{
		get
		{
			return ViewState.GetBool("AllowPaging", def: false);
		}
		set
		{
			ViewState["AllowPaging"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether sorting is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if sorting is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AllowSorting
	{
		get
		{
			return ViewState.GetBool("AllowSorting", def: false);
		}
		set
		{
			ViewState["AllowSorting"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Web.UI.WebControls.BoundColumn" /> objects are automatically created and displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control for each field in the data source.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.UI.WebControls.BoundColumn" /> objects are automatically created and displayed; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool AutoGenerateColumns
	{
		get
		{
			return ViewState.GetBool("AutoGenerateColumns", def: true);
		}
		set
		{
			ViewState["AutoGenerateColumns"] = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display in the background of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The URL of an image to display in the background of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[UrlProperty]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string BackImageUrl
	{
		get
		{
			return TableStyle.BackImageUrl;
		}
		set
		{
			TableStyle.BackImageUrl = value;
		}
	}

	/// <summary>Gets or sets the index of the currently displayed page.</summary>
	/// <returns>The zero-based index of the page currently displayed.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified page index is a negative value. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public int CurrentPageIndex
	{
		get
		{
			return ViewState.GetInt("CurrentPageIndex", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["CurrentPageIndex"] = value;
		}
	}

	/// <summary>Gets or sets the index of an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control to edit.</summary>
	/// <returns>The index of an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control to edit. The default value is -1, which indicates that no item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control is being edited.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1. </exception>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public virtual int EditItemIndex
	{
		get
		{
			return ViewState.GetInt("EditItemIndex", -1);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["EditItemIndex"] = value;
		}
	}

	/// <summary>Gets the total number of pages required to display the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The total number of pages required to display the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public int PageCount
	{
		get
		{
			if (paged_data_source != null)
			{
				return paged_data_source.PageCount;
			}
			return ViewState.GetInt("PageCount", 0);
		}
	}

	/// <summary>Gets or sets the number of items to display on a single page of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The number of items to display on a single page of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is 10.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified page size less than 1. </exception>
	[DefaultValue(10)]
	[WebSysDescription("")]
	[WebCategory("Paging")]
	public virtual int PageSize
	{
		get
		{
			return ViewState.GetInt("PageSize", 10);
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["PageSize"] = value;
		}
	}

	/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1. </exception>
	[Bindable(true)]
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Paging")]
	public virtual int SelectedIndex
	{
		get
		{
			return ViewState.GetInt("SelectedIndex", -1);
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			int @int = ViewState.GetInt("SelectedIndex", -1);
			AdjustItemTypes(@int, value);
			ViewState["SelectedIndex"] = value;
		}
	}

	/// <summary>Gets the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that represents the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataGrid" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle AlternatingItemStyle
	{
		get
		{
			if (alt_item_style == null)
			{
				alt_item_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					alt_item_style.TrackViewState();
				}
			}
			return alt_item_style;
		}
	}

	/// <summary>Gets the style properties of the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle EditItemStyle
	{
		get
		{
			if (edit_item_style == null)
			{
				edit_item_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					edit_item_style.TrackViewState();
				}
			}
			return edit_item_style;
		}
	}

	/// <summary>Gets the style properties of the footer section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the footer section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
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
			if (footer_style == null)
			{
				footer_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					footer_style.TrackViewState();
				}
			}
			return footer_style;
		}
	}

	/// <summary>Gets the style properties of the heading section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the heading section in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
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
			if (header_style == null)
			{
				header_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					header_style.TrackViewState();
				}
			}
			return header_style;
		}
	}

	/// <summary>Gets the style properties of the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle ItemStyle
	{
		get
		{
			if (item_style == null)
			{
				item_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					item_style.TrackViewState();
				}
			}
			return item_style;
		}
	}

	/// <summary>Gets the style properties of the currently selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the currently selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual TableItemStyle SelectedItemStyle
	{
		get
		{
			if (selected_style == null)
			{
				selected_style = new TableItemStyle();
				if (base.IsTrackingViewState)
				{
					selected_style.TrackViewState();
				}
			}
			return selected_style;
		}
	}

	/// <summary>Gets the style properties of the paging section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object that contains the style properties of the paging section of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.DataGridPagerStyle" /> object.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[NotifyParentProperty(true)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual DataGridPagerStyle PagerStyle
	{
		get
		{
			if (pager_style == null)
			{
				pager_style = new DataGridPagerStyle();
				if (base.IsTrackingViewState)
				{
					pager_style.TrackViewState();
				}
			}
			return pager_style;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects that represent the individual items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItemCollection" /> that contains a collection of <see cref="T:System.Web.UI.WebControls.DataGridItem" /> objects representing the individual items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Style")]
	public virtual DataGridItemCollection Items
	{
		get
		{
			EnsureChildControls();
			if (items == null)
			{
				if (items_list == null)
				{
					items_list = new ArrayList();
				}
				items = new DataGridItemCollection(items_list);
			}
			return items;
		}
	}

	/// <summary>Gets a collection of objects that represent the columns of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridColumnCollection" /> object that contains a collection of objects that represent the columns of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	[DefaultValue(null)]
	[Editor("System.Web.UI.Design.WebControls.DataGridColumnCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual DataGridColumnCollection Columns
	{
		get
		{
			if (columns == null)
			{
				columns_list = new ArrayList();
				columns = new DataGridColumnCollection(this, columns_list);
				if (base.IsTrackingViewState)
				{
					((IStateManager)columns).TrackViewState();
				}
			}
			return columns;
		}
	}

	private DataGridColumnCollection DataSourceColumns
	{
		get
		{
			if (data_source_columns == null)
			{
				data_source_columns_list = new ArrayList();
				data_source_columns = new DataGridColumnCollection(this, data_source_columns_list);
				if (base.IsTrackingViewState)
				{
					((IStateManager)data_source_columns).TrackViewState();
				}
			}
			return data_source_columns;
		}
	}

	private Table RenderTable
	{
		get
		{
			if (render_table == null)
			{
				render_table = new ChildTable(this);
				render_table.AutoID = false;
			}
			return render_table;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that represents the selected item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Paging")]
	public virtual DataGridItem SelectedItem
	{
		get
		{
			if (SelectedIndex == -1)
			{
				return null;
			}
			return Items[SelectedIndex];
		}
	}

	/// <summary>Gets or sets a value that indicates whether the footer is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> to display the footer; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual bool ShowFooter
	{
		get
		{
			return ViewState.GetBool("ShowFooter", def: false);
		}
		set
		{
			ViewState["ShowFooter"] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the header is displayed in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>
	///     <see langword="true" /> to display the header; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual bool ShowHeader
	{
		get
		{
			return ViewState.GetBool("ShowHeader", def: true);
		}
		set
		{
			ViewState["ShowHeader"] = value;
		}
	}

	/// <summary>Gets or sets the virtual number of items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when custom paging is used.</summary>
	/// <returns>The virtual number of items in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control when custom paging is used.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is a negative number. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int VirtualItemCount
	{
		get
		{
			return ViewState.GetInt("VirtualItemCount", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["VirtualItemCount"] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <returns>Always returns <see langword="HtmlTextWriterTag.Table" />.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Table;

	private TableStyle TableStyle => (TableStyle)base.ControlStyle;

	/// <summary>Occurs when the <see langword="Cancel" /> button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridCommandEventHandler CancelCommand
	{
		add
		{
			base.Events.AddHandler(CancelCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CancelCommandEvent, value);
		}
	}

	/// <summary>Occurs when the Delete button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridCommandEventHandler DeleteCommand
	{
		add
		{
			base.Events.AddHandler(DeleteCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DeleteCommandEvent, value);
		}
	}

	/// <summary>Occurs when the Edit button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridCommandEventHandler EditCommand
	{
		add
		{
			base.Events.AddHandler(EditCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(EditCommandEvent, value);
		}
	}

	/// <summary>Occurs when any button is clicked in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridCommandEventHandler ItemCommand
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

	/// <summary>Occurs on the server when an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control is created.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridItemEventHandler ItemCreated
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

	/// <summary>Occurs after an item is data bound to the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridItemEventHandler ItemDataBound
	{
		add
		{
			base.Events.AddHandler(ItemDataBoundEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ItemDataBoundEvent, value);
		}
	}

	/// <summary>Occurs when one of the page selection elements is clicked.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridPageChangedEventHandler PageIndexChanged
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

	/// <summary>Occurs when a column is sorted.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridSortCommandEventHandler SortCommand
	{
		add
		{
			base.Events.AddHandler(SortCommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SortCommandEvent, value);
		}
	}

	/// <summary>Occurs when the Update button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event DataGridCommandEventHandler UpdateCommand
	{
		add
		{
			base.Events.AddHandler(UpdateCommandEvent, value);
		}
		remove
		{
			base.Events.AddHandler(UpdateCommandEvent, value);
		}
	}

	private void AdjustItemTypes(int prev_select, int new_select)
	{
		if (items_list == null)
		{
			return;
		}
		int count = items_list.Count;
		if (count == 0)
		{
			return;
		}
		if (prev_select >= 0 && prev_select < count)
		{
			DataGridItem dataGridItem = (DataGridItem)items_list[prev_select];
			if (dataGridItem.ItemType != ListItemType.EditItem)
			{
				if (dataGridItem.ItemIndex % 2 != 0)
				{
					dataGridItem.SetItemType(ListItemType.AlternatingItem);
				}
				else
				{
					dataGridItem.SetItemType(ListItemType.Item);
				}
			}
		}
		if (new_select != -1 && new_select < count)
		{
			DataGridItem dataGridItem = (DataGridItem)items_list[new_select];
			if (dataGridItem.ItemType != ListItemType.EditItem)
			{
				dataGridItem.SetItemType(ListItemType.SelectedItem);
			}
		}
	}

	private void AddColumnsFromSource(PagedDataSource data_source)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = null;
		Type type = null;
		bool flag = false;
		PropertyInfo property = data_source.GetType().GetProperty("Item", item_args);
		if (property == null)
		{
			IEnumerator enumerator = ((data_source.DataSource != null) ? data_source.GetEnumerator() : null);
			if (enumerator != null && enumerator.MoveNext())
			{
				object current = enumerator.Current;
				if (current is ICustomTypeDescriptor || !BaseDataList.IsBindableType(current.GetType()))
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(current);
				}
				else if (current != null)
				{
					type = current.GetType();
				}
				data_enumerator = enumerator;
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			type = property.PropertyType;
		}
		if (type != null)
		{
			AddPropertyToColumns();
			return;
		}
		if (propertyDescriptorCollection != null)
		{
			foreach (PropertyDescriptor item in propertyDescriptorCollection)
			{
				AddPropertyToColumns(item, tothis: false);
			}
			return;
		}
		if (flag)
		{
			return;
		}
		throw new HttpException($"DataGrid '{ID}' cannot autogenerate columns from the given datasource. {type}");
	}

	/// <summary>Creates the set of columns to be used to build up the control hierarchy. When <see cref="P:System.Web.UI.WebControls.DataGrid.AutoGenerateColumns" /> is true, the columns are created to match the data source and are appended to the set of columns defined in the <see cref="P:System.Web.UI.WebControls.DataGrid.Columns" /> collection.</summary>
	/// <param name="dataSource">The data source being used to create the control hierarchy </param>
	/// <param name="useDataSource">Whether to use the data source to generate columns automatically or to use saved state. </param>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the effective set of columns in the right order.</returns>
	protected virtual ArrayList CreateColumnSet(PagedDataSource dataSource, bool useDataSource)
	{
		ArrayList arrayList = new ArrayList();
		if (columns_list != null)
		{
			arrayList.AddRange(columns_list);
		}
		if (AutoGenerateColumns)
		{
			if (useDataSource)
			{
				data_enumerator = null;
				PropertyDescriptorCollection itemProperties = dataSource.GetItemProperties(null);
				DataSourceColumns.Clear();
				if (itemProperties != null)
				{
					foreach (PropertyDescriptor item in itemProperties)
					{
						AddPropertyToColumns(item, tothis: false);
					}
				}
				else
				{
					AddColumnsFromSource(dataSource);
				}
			}
			if (data_source_columns != null && data_source_columns.Count > 0)
			{
				arrayList.AddRange(data_source_columns);
			}
		}
		return arrayList;
	}

	private void AddPropertyToColumns()
	{
		BoundColumn boundColumn = new BoundColumn();
		if (base.IsTrackingViewState)
		{
			((IStateManager)boundColumn).TrackViewState();
		}
		boundColumn.Set_Owner(this);
		boundColumn.HeaderText = "Item";
		boundColumn.SortExpression = "Item";
		boundColumn.DataField = BoundColumn.thisExpr;
		DataSourceColumns.Add(boundColumn);
	}

	private void AddPropertyToColumns(PropertyDescriptor prop, bool tothis)
	{
		BoundColumn boundColumn = new BoundColumn();
		boundColumn.Set_Owner(this);
		if (base.IsTrackingViewState)
		{
			((IStateManager)boundColumn).TrackViewState();
		}
		boundColumn.HeaderText = prop.Name;
		boundColumn.DataField = (tothis ? BoundColumn.thisExpr : prop.Name);
		boundColumn.SortExpression = prop.Name;
		if (string.Compare(DataKeyField, boundColumn.DataField, StringComparison.OrdinalIgnoreCase) == 0)
		{
			boundColumn.ReadOnly = true;
		}
		DataSourceColumns.Add(boundColumn);
	}

	/// <summary>Marks the starting point to begin tracking and saving changes to the control as part of the control view state.</summary>
	protected override void TrackViewState()
	{
		base.TrackViewState();
		if (pager_style != null)
		{
			pager_style.TrackViewState();
		}
		if (header_style != null)
		{
			header_style.TrackViewState();
		}
		if (footer_style != null)
		{
			footer_style.TrackViewState();
		}
		if (item_style != null)
		{
			item_style.TrackViewState();
		}
		if (alt_item_style != null)
		{
			alt_item_style.TrackViewState();
		}
		if (selected_style != null)
		{
			selected_style.TrackViewState();
		}
		if (edit_item_style != null)
		{
			edit_item_style.TrackViewState();
		}
		if (base.ControlStyleCreated)
		{
			base.ControlStyle.TrackViewState();
		}
		((IStateManager)columns)?.TrackViewState();
	}

	/// <summary>Saves the current state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
	/// <returns>The saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</returns>
	protected override object SaveViewState()
	{
		object[] array = new object[11]
		{
			base.SaveViewState(),
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null
		};
		if (columns != null)
		{
			IStateManager stateManager = columns;
			array[1] = stateManager.SaveViewState();
		}
		if (pager_style != null)
		{
			array[2] = pager_style.SaveViewState();
		}
		if (header_style != null)
		{
			array[3] = header_style.SaveViewState();
		}
		if (footer_style != null)
		{
			array[4] = footer_style.SaveViewState();
		}
		if (item_style != null)
		{
			array[5] = item_style.SaveViewState();
		}
		if (alt_item_style != null)
		{
			array[6] = alt_item_style.SaveViewState();
		}
		if (selected_style != null)
		{
			array[7] = selected_style.SaveViewState();
		}
		if (edit_item_style != null)
		{
			array[8] = edit_item_style.SaveViewState();
		}
		if (base.ControlStyleCreated)
		{
			array[9] = base.ControlStyle.SaveViewState();
		}
		if (data_source_columns != null)
		{
			IStateManager stateManager2 = data_source_columns;
			array[10] = stateManager2.SaveViewState();
		}
		return array;
	}

	/// <summary>Loads a saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
	/// <param name="savedState">A saved state of the <see cref="T:System.Web.UI.WebControls.DataGrid" />. </param>
	protected override void LoadViewState(object savedState)
	{
		if (!(savedState is object[] array))
		{
			return;
		}
		base.LoadViewState(array[0]);
		if (columns != null)
		{
			((IStateManager)columns).LoadViewState(array[1]);
		}
		if (array[2] != null)
		{
			PagerStyle.LoadViewState(array[2]);
		}
		if (array[3] != null)
		{
			HeaderStyle.LoadViewState(array[3]);
		}
		if (array[4] != null)
		{
			FooterStyle.LoadViewState(array[4]);
		}
		if (array[5] != null)
		{
			ItemStyle.LoadViewState(array[5]);
		}
		if (array[6] != null)
		{
			AlternatingItemStyle.LoadViewState(array[6]);
		}
		if (array[7] != null)
		{
			SelectedItemStyle.LoadViewState(array[7]);
		}
		if (array[8] != null)
		{
			EditItemStyle.LoadViewState(array[8]);
		}
		if (array[9] != null)
		{
			base.ControlStyle.LoadViewState(array[8]);
		}
		if (array[10] != null)
		{
			object[] array2 = (object[])array[10];
			foreach (object state in array2)
			{
				BoundColumn boundColumn = new BoundColumn();
				((IStateManager)boundColumn).TrackViewState();
				boundColumn.Set_Owner(this);
				((IStateManager)boundColumn).LoadViewState(state);
				DataSourceColumns.Add(boundColumn);
			}
		}
		if (array[9] != null)
		{
			object[] array2 = (object[])array[9];
			foreach (object state2 in array2)
			{
				BoundColumn boundColumn2 = new BoundColumn();
				boundColumn2.Set_Owner(this);
				((IStateManager)boundColumn2).LoadViewState(state2);
				DataSourceColumns.Add(boundColumn2);
			}
		}
	}

	/// <summary>Creates new control style.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> the represents the new style.</returns>
	protected override Style CreateControlStyle()
	{
		return new TableStyle
		{
			GridLines = GridLines.Both,
			CellSpacing = 0
		};
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> to initialize.</param>
	/// <param name="columns">An array of <see cref="T:System.Web.UI.WebControls.DataGridColumn" /> objects that contains the columns in this <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</param>
	protected virtual void InitializeItem(DataGridItem item, DataGridColumn[] columns)
	{
		bool flag = UseAccessibleHeader && item.ItemType == ListItemType.Header;
		for (int i = 0; i < columns.Length; i++)
		{
			TableCell tableCell = null;
			if (flag)
			{
				tableCell = new TableHeaderCell();
				tableCell.Attributes["scope"] = "col";
			}
			else
			{
				tableCell = new TableCell();
			}
			columns[i].InitializeCell(tableCell, i, item.ItemType);
			item.Cells.Add(tableCell);
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object that contains the paging UI.</summary>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataGridItem" /> that contains the pager.</param>
	/// <param name="columnSpan">The number of columns to span the pager.</param>
	/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that contains the properties for the pager.</param>
	protected virtual void InitializePager(DataGridItem item, int columnSpan, PagedDataSource pagedDataSource)
	{
		TableCell child = ((PagerStyle.Mode != 0) ? InitializeNumericPager(item, columnSpan, pagedDataSource) : InitializeNextPrevPager(item, columnSpan, pagedDataSource));
		item.Controls.Add(child);
	}

	private TableCell InitializeNumericPager(DataGridItem item, int columnSpan, PagedDataSource paged)
	{
		TableCell tableCell = new TableCell();
		tableCell.ColumnSpan = columnSpan;
		int pageButtonCount = PagerStyle.PageButtonCount;
		int currentPageIndex = paged.CurrentPageIndex;
		int num = currentPageIndex - currentPageIndex % pageButtonCount;
		int num2 = num + pageButtonCount;
		if (num2 > paged.PageCount)
		{
			num2 = paged.PageCount;
		}
		if (num > 0)
		{
			LinkButton linkButton = new LinkButton();
			linkButton.Text = "...";
			linkButton.CommandName = "Page";
			linkButton.CommandArgument = num.ToString(Helpers.InvariantCulture);
			linkButton.CausesValidation = false;
			tableCell.Controls.Add(linkButton);
			tableCell.Controls.Add(new LiteralControl("&nbsp;"));
		}
		for (int i = num; i < num2; i++)
		{
			Control control = null;
			string text = (i + 1).ToString(Helpers.InvariantCulture);
			control = ((i == paged.CurrentPageIndex) ? ((WebControl)new Label
			{
				Text = text
			}) : ((WebControl)new LinkButton
			{
				Text = text,
				CommandName = "Page",
				CommandArgument = text,
				CausesValidation = false
			}));
			tableCell.Controls.Add(control);
			if (i < num2 - 1)
			{
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			}
		}
		if (num2 < paged.PageCount)
		{
			tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			LinkButton linkButton2 = new LinkButton();
			linkButton2.Text = "...";
			linkButton2.CommandName = "Page";
			linkButton2.CommandArgument = (num2 + 1).ToString(Helpers.InvariantCulture);
			linkButton2.CausesValidation = false;
			tableCell.Controls.Add(linkButton2);
		}
		return tableCell;
	}

	private TableCell InitializeNextPrevPager(DataGridItem item, int columnSpan, PagedDataSource paged)
	{
		TableCell obj = new TableCell
		{
			ColumnSpan = columnSpan
		};
		Control child = ((!paged.IsFirstPage) ? ((WebControl)new DataControlLinkButton
		{
			Text = PagerStyle.PrevPageText,
			CommandName = "Page",
			CommandArgument = "Prev",
			CausesValidation = false
		}) : ((WebControl)new Label
		{
			Text = PagerStyle.PrevPageText
		}));
		Control child2 = ((paged.Count <= 0 || paged.IsLastPage) ? ((WebControl)new Label
		{
			Text = PagerStyle.NextPageText
		}) : ((WebControl)new DataControlLinkButton
		{
			Text = PagerStyle.NextPageText,
			CommandName = "Page",
			CommandArgument = "Next",
			CausesValidation = false
		}));
		obj.Controls.Add(child);
		obj.Controls.Add(new LiteralControl("&nbsp;"));
		obj.Controls.Add(child2);
		return obj;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</summary>
	/// <param name="itemIndex">The index for the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</param>
	/// <param name="dataSourceIndex">The index of the data item from the data source.</param>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object.</returns>
	protected virtual DataGridItem CreateItem(int itemIndex, int dataSourceIndex, ListItemType itemType)
	{
		return new DataGridItem(itemIndex, dataSourceIndex, itemType);
	}

	private DataGridItem CreateItem(int item_index, int data_source_index, ListItemType type, bool data_bind, object data_item, PagedDataSource paged)
	{
		DataGridItem dataGridItem = CreateItem(item_index, data_source_index, type);
		DataGridItemEventArgs e = new DataGridItemEventArgs(dataGridItem);
		bool num = type != ListItemType.Pager;
		if (num)
		{
			InitializeItem(dataGridItem, render_columns);
			if (data_bind)
			{
				dataGridItem.DataItem = data_item;
			}
			OnItemCreated(e);
		}
		else
		{
			InitializePager(dataGridItem, render_columns.Length, paged);
			if (pager_style != null)
			{
				dataGridItem.ApplyStyle(pager_style);
			}
			OnItemCreated(e);
		}
		RenderTable.Controls.Add(dataGridItem);
		if (num && data_bind)
		{
			dataGridItem.DataBind();
			OnItemDataBound(e);
			dataGridItem.DataItem = null;
		}
		return dataGridItem;
	}

	/// <summary>Creates the control hierarchy that is used to render the <see cref="T:System.Web.UI.WebControls.DataGrid" />.</summary>
	/// <param name="useDataSource">Whether to use the data source to generate columns automatically or to use saved state. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="useDataSource" /> is <see langword="true" />, the value of <see cref="P:System.Web.UI.WebControls.DataGrid.VirtualItemCount" /> is not set, and the selected data source does not implement the <see cref="T:System.Collections.ICollection" /> interface.- or -
	///         <paramref name="useDataSource" /> is <see langword="true" /> and the data source has an invalid <see cref="P:System.Web.UI.WebControls.PagedDataSource.CurrentPageIndex" /> property.</exception>
	protected override void CreateControlHierarchy(bool useDataSource)
	{
		Controls.Clear();
		RenderTable.Controls.Clear();
		Controls.Add(RenderTable);
		ArrayList arrayList = null;
		IEnumerable enumerable;
		if (useDataSource)
		{
			enumerable = ((!base.IsBoundUsingDataSourceID) ? DataSourceResolver.ResolveDataSource(DataSource, base.DataMember) : GetData());
			if (enumerable == null)
			{
				Controls.Clear();
				return;
			}
			arrayList = base.DataKeysArray;
			arrayList.Clear();
		}
		else
		{
			enumerable = new NCollection(ViewState.GetInt("Items", 0));
		}
		paged_data_source = new PagedDataSource();
		PagedDataSource pagedDataSource = paged_data_source;
		pagedDataSource.AllowPaging = AllowPaging;
		pagedDataSource.AllowCustomPaging = AllowCustomPaging;
		pagedDataSource.DataSource = enumerable;
		pagedDataSource.CurrentPageIndex = CurrentPageIndex;
		pagedDataSource.PageSize = PageSize;
		pagedDataSource.VirtualCount = VirtualItemCount;
		if (pagedDataSource.IsPagingEnabled && pagedDataSource.PageCount < pagedDataSource.CurrentPageIndex)
		{
			Controls.Clear();
			throw new HttpException("Invalid DataGrid PageIndex");
		}
		ArrayList arrayList2 = CreateColumnSet(paged_data_source, useDataSource);
		if (arrayList2.Count == 0)
		{
			Controls.Clear();
			return;
		}
		Page?.RequiresPostBackScript();
		render_columns = new DataGridColumn[arrayList2.Count];
		for (int i = 0; i < arrayList2.Count; i++)
		{
			DataGridColumn dataGridColumn = (DataGridColumn)arrayList2[i];
			dataGridColumn.Set_Owner(this);
			dataGridColumn.Initialize();
			render_columns[i] = dataGridColumn;
		}
		if (pagedDataSource.IsPagingEnabled)
		{
			CreateItem(-1, -1, ListItemType.Pager, data_bind: false, null, pagedDataSource);
		}
		CreateItem(-1, -1, ListItemType.Header, useDataSource, null, pagedDataSource);
		if (items_list == null)
		{
			items_list = new ArrayList();
		}
		else
		{
			items_list.Clear();
		}
		bool flag = false;
		IEnumerator enumerator = null;
		if (data_enumerator == null)
		{
			enumerator = ((pagedDataSource.DataSource == null) ? null : pagedDataSource.GetEnumerator());
		}
		else
		{
			enumerator = data_enumerator;
			flag = true;
		}
		int num = 0;
		bool flag2 = true;
		string text = null;
		int num2 = pagedDataSource.FirstIndexInPage;
		int selectedIndex = SelectedIndex;
		int editItemIndex = EditItemIndex;
		while (enumerator != null && (flag || enumerator.MoveNext()))
		{
			if (flag2)
			{
				flag2 = false;
				text = DataKeyField;
				flag = false;
			}
			object current = enumerator.Current;
			if (useDataSource && text != "")
			{
				arrayList.Add(DataBinder.GetPropertyValue(current, text));
			}
			ListItemType type = ListItemType.Item;
			if (num == editItemIndex)
			{
				type = ListItemType.EditItem;
			}
			else if (num == selectedIndex)
			{
				type = ListItemType.SelectedItem;
			}
			else if (num % 2 != 0)
			{
				type = ListItemType.AlternatingItem;
			}
			items_list.Add(CreateItem(num, num2, type, useDataSource, current, pagedDataSource));
			num++;
			num2++;
		}
		CreateItem(-1, -1, ListItemType.Footer, useDataSource, null, paged_data_source);
		if (pagedDataSource.IsPagingEnabled)
		{
			CreateItem(-1, -1, ListItemType.Pager, data_bind: false, null, paged_data_source);
			if (useDataSource)
			{
				ViewState["Items"] = (pagedDataSource.IsCustomPagingEnabled ? num : pagedDataSource.DataSourceCount);
			}
		}
		else if (useDataSource)
		{
			ViewState["Items"] = num;
		}
	}

	private void ApplyColumnStyle(TableCellCollection cells, ListItemType type)
	{
		int num = Math.Min(cells.Count, render_columns.Length);
		if (num <= 0)
		{
			return;
		}
		for (int i = 0; i < num; i++)
		{
			Style style = null;
			TableCell tableCell = cells[i];
			DataGridColumn dataGridColumn = render_columns[i];
			if (!dataGridColumn.Visible)
			{
				tableCell.Visible = false;
				continue;
			}
			style = dataGridColumn.GetStyle(type);
			if (style != null)
			{
				tableCell.MergeStyle(style);
			}
		}
	}

	/// <summary>Sets up the control hierarchy for this <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	protected override void PrepareControlHierarchy()
	{
		if (!HasControls() || Controls.Count == 0)
		{
			return;
		}
		Table table = render_table;
		table.CopyBaseAttributes(this);
		table.ApplyStyle(base.ControlStyle);
		table.Caption = Caption;
		table.CaptionAlign = CaptionAlign;
		table.Enabled = base.IsEnabled;
		bool flag = true;
		foreach (DataGridItem row in table.Rows)
		{
			switch (row.ItemType)
			{
			case ListItemType.Item:
				ApplyItemStyle(row);
				break;
			case ListItemType.AlternatingItem:
				ApplyItemStyle(row);
				break;
			case ListItemType.EditItem:
				row.MergeStyle(edit_item_style);
				ApplyItemStyle(row);
				ApplyColumnStyle(row.Cells, ListItemType.EditItem);
				break;
			case ListItemType.Footer:
				if (!ShowFooter)
				{
					row.Visible = false;
					break;
				}
				if (footer_style != null)
				{
					row.MergeStyle(footer_style);
				}
				ApplyColumnStyle(row.Cells, ListItemType.Footer);
				break;
			case ListItemType.Header:
				if (!ShowHeader)
				{
					row.Visible = false;
					break;
				}
				if (header_style != null)
				{
					row.MergeStyle(header_style);
				}
				ApplyColumnStyle(row.Cells, ListItemType.Header);
				break;
			case ListItemType.SelectedItem:
				row.MergeStyle(selected_style);
				ApplyItemStyle(row);
				ApplyColumnStyle(row.Cells, ListItemType.SelectedItem);
				break;
			case ListItemType.Separator:
				ApplyColumnStyle(row.Cells, ListItemType.Separator);
				break;
			case ListItemType.Pager:
			{
				DataGridPagerStyle pagerStyle = PagerStyle;
				if (!pagerStyle.Visible || !paged_data_source.IsPagingEnabled)
				{
					row.Visible = false;
				}
				else
				{
					if (flag)
					{
						row.Visible = pagerStyle.Position != PagerPosition.Bottom;
					}
					else
					{
						row.Visible = pagerStyle.Position != PagerPosition.Top;
					}
					flag = false;
				}
				if (row.Visible)
				{
					row.MergeStyle(pager_style);
				}
				break;
			}
			}
		}
	}

	private void ApplyItemStyle(DataGridItem item)
	{
		if (item.ItemIndex % 2 != 0)
		{
			item.MergeStyle(alt_item_style);
		}
		item.MergeStyle(item_style);
		ApplyColumnStyle(item.Cells, ListItemType.Item);
	}

	/// <summary>Passes the event raised by a control within the container up the page's UI server control hierarchy.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	/// <returns>
	///     <see langword="true" /> to indicate that this method is passing an event raised by a control within the container up the page's UI server control hierarchy; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (!(e is DataGridCommandEventArgs { CommandName: var commandName } dataGridCommandEventArgs))
		{
			return false;
		}
		CultureInfo invariantCulture = Helpers.InvariantCulture;
		OnItemCommand(dataGridCommandEventArgs);
		if (string.Compare(commandName, "Cancel", ignoreCase: true, invariantCulture) == 0)
		{
			OnCancelCommand(dataGridCommandEventArgs);
		}
		else if (string.Compare(commandName, "Delete", ignoreCase: true, invariantCulture) == 0)
		{
			OnDeleteCommand(dataGridCommandEventArgs);
		}
		else if (string.Compare(commandName, "Edit", ignoreCase: true, invariantCulture) == 0)
		{
			OnEditCommand(dataGridCommandEventArgs);
		}
		else if (string.Compare(commandName, "Select", ignoreCase: true, invariantCulture) == 0)
		{
			SelectedIndex = dataGridCommandEventArgs.Item.ItemIndex;
			OnSelectedIndexChanged(dataGridCommandEventArgs);
		}
		else if (string.Compare(commandName, "Sort", ignoreCase: true, invariantCulture) == 0)
		{
			DataGridSortCommandEventArgs e2 = new DataGridSortCommandEventArgs(dataGridCommandEventArgs.CommandSource, dataGridCommandEventArgs);
			OnSortCommand(e2);
		}
		else if (string.Compare(commandName, "Update", ignoreCase: true, invariantCulture) == 0)
		{
			OnUpdateCommand(dataGridCommandEventArgs);
		}
		else if (string.Compare(commandName, "Page", ignoreCase: true, invariantCulture) == 0)
		{
			DataGridPageChangedEventArgs e3 = new DataGridPageChangedEventArgs(newPageIndex: (string.Compare((string)dataGridCommandEventArgs.CommandArgument, "Next", ignoreCase: true, invariantCulture) == 0) ? (CurrentPageIndex + 1) : ((string.Compare((string)dataGridCommandEventArgs.CommandArgument, "Prev", ignoreCase: true, invariantCulture) != 0) ? (int.Parse((string)dataGridCommandEventArgs.CommandArgument, invariantCulture) - 1) : (CurrentPageIndex - 1)), commandSource: dataGridCommandEventArgs.CommandSource);
			OnPageIndexChanged(e3);
		}
		return true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnCancelCommand(DataGridCommandEventArgs e)
	{
		((DataGridCommandEventHandler)base.Events[CancelCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnDeleteCommand(DataGridCommandEventArgs e)
	{
		((DataGridCommandEventHandler)base.Events[DeleteCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnEditCommand(DataGridCommandEventArgs e)
	{
		((DataGridCommandEventHandler)base.Events[EditCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnItemCommand(DataGridCommandEventArgs e)
	{
		((DataGridCommandEventHandler)base.Events[ItemCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCreated" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemCreated(DataGridItemEventArgs e)
	{
		((DataGridItemEventHandler)base.Events[ItemCreated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.ItemDataBound" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridItemEventArgs" /> that contains event data. </param>
	protected virtual void OnItemDataBound(DataGridItemEventArgs e)
	{
		((DataGridItemEventHandler)base.Events[ItemDataBound])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.PageIndexChanged" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridPageChangedEventArgs" /> that contains event data. </param>
	protected virtual void OnPageIndexChanged(DataGridPageChangedEventArgs e)
	{
		((DataGridPageChangedEventHandler)base.Events[PageIndexChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.SortCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridSortCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnSortCommand(DataGridSortCommandEventArgs e)
	{
		((DataGridSortCommandEventHandler)base.Events[SortCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains event data. </param>
	protected virtual void OnUpdateCommand(DataGridCommandEventArgs e)
	{
		((DataGridCommandEventHandler)base.Events[UpdateCommand])?.Invoke(this, e);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> class.</summary>
	public DataGrid()
	{
	}

	static DataGrid()
	{
		CancelCommand = new object();
		DeleteCommand = new object();
		EditCommand = new object();
		ItemCommand = new object();
		ItemCreated = new object();
		ItemDataBound = new object();
		PageIndexChanged = new object();
		SortCommand = new object();
		UpdateCommand = new object();
		item_args = new Type[1] { typeof(int) };
	}
}
