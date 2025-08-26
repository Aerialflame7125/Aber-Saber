using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>A data-bound list control that allows custom layout by repeating a specified template for each item displayed in the list.</summary>
[DefaultEvent("ItemCommand")]
[DefaultProperty("DataSource")]
[Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(true)]
[PersistChildren(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Repeater : Control, INamingContainer
{
	private object dataSource;

	private IDataSource boundDataSource;

	private bool initialized;

	private bool preRendered;

	private bool requiresDataBinding;

	private DataSourceSelectArguments selectArguments;

	private IEnumerable data;

	private RepeaterItemCollection itemscol;

	private ArrayList items;

	private ITemplate alt_itm_tmpl;

	private ITemplate footer_tmpl;

	private ITemplate header_tmpl;

	private ITemplate item_tmpl;

	private ITemplate separator_tmpl;

	private static readonly object ItemCommandEvent;

	private static readonly object ItemCreatedEvent;

	private static readonly object ItemDataBoundEvent;

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the child controls of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
	public override ControlCollection Controls
	{
		get
		{
			EnsureChildControls();
			return base.Controls;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	/// <returns>A collection of <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> objects. The default is an empty <see cref="T:System.Web.UI.WebControls.RepeaterItemCollection" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	public virtual RepeaterItemCollection Items
	{
		get
		{
			if (itemscol == null)
			{
				if (items == null)
				{
					items = new ArrayList();
				}
				itemscol = new RepeaterItemCollection(items);
			}
			return itemscol;
		}
	}

	/// <summary>Gets or sets the specific table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> to bind to the control.</summary>
	/// <returns>A string that specifies a table in the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataMember
	{
		get
		{
			return ViewState.GetString("DataMember", "");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("DataMember");
			}
			else
			{
				ViewState["DataMember"] = value;
			}
			if (!Initialized)
			{
				OnDataPropertyChanged();
			}
		}
	}

	/// <summary>Gets or sets the data source that provides data for populating the list.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> object that contains a collection of values used to supply data to this control. The default value is <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> object specified is not a supported source of data for the <see cref="T:System.Web.UI.WebControls.Repeater" /> control. </exception>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property. </exception>
	[Bindable(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual object DataSource
	{
		get
		{
			return dataSource;
		}
		set
		{
			if (value == null || value is IListSource || value is IEnumerable)
			{
				dataSource = value;
				if (!Initialized)
				{
					OnDataPropertyChanged();
				}
				return;
			}
			throw new ArgumentException($"An invalid data source is being used for {ID}. A valid data source must implement either IListSource or IEnumerable");
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control should use to retrieve its data source.</summary>
	/// <returns>The <see langword="ID" /> property of the data source control.</returns>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved for one of the following reasons:A value is specified for both the <see cref="P:System.Web.UI.WebControls.Repeater.DataSource" /> and <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> properties.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property cannot be found on the page.The data source specified by the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property does not implement <see cref="T:System.Web.UI.IDataSource" />.</exception>
	[DefaultValue("")]
	[IDReferenceProperty(typeof(DataSourceControl))]
	public virtual string DataSourceID
	{
		get
		{
			return ViewState.GetString("DataSourceID", "");
		}
		set
		{
			if (dataSource != null)
			{
				throw new HttpException("Only one of DataSource and DataSourceID can be specified.");
			}
			ViewState["DataSourceID"] = value;
			if (!Initialized)
			{
				OnDataPropertyChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether themes are applied to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="false" />. </returns>
	[Browsable(true)]
	public override bool EnableTheming
	{
		get
		{
			return base.EnableTheming;
		}
		set
		{
			base.EnableTheming = value;
		}
	}

	/// <summary>Gets or sets the object implementing <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items in the control are displayed.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how alternating items are displayed. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(RepeaterItem))]
	[WebSysDescription("")]
	public virtual ITemplate AlternatingItemTemplate
	{
		get
		{
			return alt_itm_tmpl;
		}
		set
		{
			alt_itm_tmpl = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the footer section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(RepeaterItem))]
	[WebSysDescription("")]
	public virtual ITemplate FooterTemplate
	{
		get
		{
			return footer_tmpl;
		}
		set
		{
			footer_tmpl = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how the header section of the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is displayed. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(RepeaterItem))]
	[WebSysDescription("")]
	public virtual ITemplate HeaderTemplate
	{
		get
		{
			return header_tmpl;
		}
		set
		{
			header_tmpl = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that defines how items in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control are displayed. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(RepeaterItem))]
	[WebSysDescription("")]
	public virtual ITemplate ItemTemplate
	{
		get
		{
			return item_tmpl;
		}
		set
		{
			item_tmpl = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.UI.ITemplate" /> interface that defines how the separator between items is displayed.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that defines how the separator between items is displayed. The default is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(RepeaterItem))]
	[WebSysDescription("")]
	public virtual ITemplate SeparatorTemplate
	{
		get
		{
			return separator_tmpl;
		}
		set
		{
			separator_tmpl = value;
		}
	}

	/// <summary>Returns a value indicating whether the control has been initialized.</summary>
	/// <returns>
	///     <see langword="true" />, if the control has been initialized, otherwise, <see langword="false" />.</returns>
	protected bool Initialized => initialized;

	/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set. </summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.WebControls.Repeater.DataSourceID" /> property is set to a value other than an empty string (""); otherwise, <see langword="false" />. </returns>
	protected bool IsBoundUsingDataSourceID => DataSourceID.Length != 0;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to its specified data source.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.Repeater" /> control needs to bind to a data source; otherwise, <see langword="false" />.</returns>
	protected bool RequiresDataBinding
	{
		get
		{
			return requiresDataBinding;
		}
		set
		{
			requiresDataBinding = value;
			if (value && preRendered && IsBoundUsingDataSourceID && Page != null && !Page.IsCallback)
			{
				EnsureDataBound();
			}
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control uses when retrieving data from a data source control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used to retrieve data. The default is the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </returns>
	protected DataSourceSelectArguments SelectArguments
	{
		get
		{
			if (selectArguments == null)
			{
				selectArguments = CreateDataSourceSelectArguments();
			}
			return selectArguments;
		}
	}

	/// <summary>Occurs when a button is clicked in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event RepeaterCommandEventHandler ItemCommand
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

	/// <summary>Occurs when an item is created in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public event RepeaterItemEventHandler ItemCreated
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

	/// <summary>Occurs after an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control is data-bound but before it is rendered on the page.</summary>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public event RepeaterItemEventHandler ItemDataBound
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

	/// <summary>Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		if (ViewState["Items"] != null)
		{
			CreateControlHierarchy(useDataSource: false);
		}
	}

	/// <summary>Raises the <see langword="DataBinding" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(EventArgs.Empty);
		Controls.Clear();
		ClearChildViewState();
		TrackViewState();
		CreateControlHierarchy(useDataSource: true);
		base.ChildControlsCreated = true;
	}

	private void DoItem(int i, ListItemType t, object d, bool databind)
	{
		RepeaterItem repeaterItem = CreateItem(i, t);
		if (t == ListItemType.Item || t == ListItemType.AlternatingItem)
		{
			items.Add(repeaterItem);
		}
		repeaterItem.DataItem = d;
		RepeaterItemEventArgs e = new RepeaterItemEventArgs(repeaterItem);
		InitializeItem(repeaterItem);
		Controls.Add(repeaterItem);
		OnItemCreated(e);
		if (databind)
		{
			repeaterItem.DataBind();
			OnItemDataBound(e);
		}
	}

	/// <summary>Creates a control hierarchy, with or without the specified data source.</summary>
	/// <param name="useDataSource">Indicates whether to use the specified data source. </param>
	protected virtual void CreateControlHierarchy(bool useDataSource)
	{
		items = new ArrayList();
		itemscol = null;
		IEnumerable enumerable = ((!useDataSource) ? new object[(int)ViewState["Items"]] : GetData());
		if (enumerable == null)
		{
			return;
		}
		if (HeaderTemplate != null)
		{
			DoItem(-1, ListItemType.Header, null, useDataSource);
		}
		int num = 0;
		foreach (object item in enumerable)
		{
			if (num != 0 && SeparatorTemplate != null)
			{
				DoItem(num - 1, ListItemType.Separator, null, useDataSource);
			}
			DoItem(num, (num % 2 == 0) ? ListItemType.Item : ListItemType.AlternatingItem, item, useDataSource);
			num++;
		}
		if (FooterTemplate != null)
		{
			DoItem(-1, ListItemType.Footer, null, useDataSource);
		}
		ViewState["Items"] = num;
	}

	/// <summary>Binds the <see cref="T:System.Web.UI.WebControls.Repeater" /> control and all its child controls to the specified data source.</summary>
	public override void DataBind()
	{
		OnDataBinding(EventArgs.Empty);
		RequiresDataBinding = false;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object with the specified item type and location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.Repeater" /> control to place the created item. </param>
	/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the <see cref="T:System.Web.UI.WebControls.Repeater" /> item to create. </param>
	/// <returns>The new <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object.</returns>
	protected virtual RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
	{
		return new RepeaterItem(itemIndex, itemType);
	}

	/// <summary>Populates iteratively the specified <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> with a sub-hierarchy of child controls.</summary>
	/// <param name="item">The control to be initialized from an inline template. </param>
	protected virtual void InitializeItem(RepeaterItem item)
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
		case ListItemType.Item:
			template = ItemTemplate;
			break;
		case ListItemType.AlternatingItem:
			template = AlternatingItemTemplate;
			if (template == null)
			{
				template = ItemTemplate;
			}
			break;
		case ListItemType.Separator:
			template = SeparatorTemplate;
			break;
		}
		template?.InstantiateIn(item);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event if the <paramref name="EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> was raised, otherwise <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object sender, EventArgs e)
	{
		if (e is RepeaterCommandEventArgs e2)
		{
			OnItemCommand(e2);
			return true;
		}
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> object that contains the event data. </param>
	protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
	{
		((RepeaterCommandEventHandler)base.Events[ItemCommand])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCreated" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
	protected virtual void OnItemCreated(RepeaterItemEventArgs e)
	{
		((RepeaterItemEventHandler)base.Events[ItemCreated])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Repeater.ItemDataBound" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
	protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
	{
		((RepeaterItemEventHandler)base.Events[ItemDataBound])?.Invoke(this, e);
	}

	/// <summary>Returns the <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value. </summary>
	/// <returns>The <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" /> value.</returns>
	protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
	{
		return DataSourceSelectArguments.Empty;
	}

	/// <summary>Verifies that the <see cref="T:System.Web.UI.WebControls.Repeater" /> control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.Repeater.DataBind" /> method.</summary>
	protected void EnsureDataBound()
	{
		if (IsBoundUsingDataSourceID && RequiresDataBinding)
		{
			DataBind();
		}
	}

	private void SelectCallback(IEnumerable data)
	{
		this.data = data;
	}

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" /> interface from the data source.</summary>
	/// <returns>An object implementing <see cref="T:System.Collections.IEnumerable" /> that represents the data from the data source.</returns>
	protected virtual IEnumerable GetData()
	{
		IEnumerable result;
		if (IsBoundUsingDataSourceID)
		{
			if (DataSourceID.Length == 0)
			{
				return null;
			}
			if (boundDataSource == null)
			{
				return null;
			}
			boundDataSource.GetView(string.Empty).Select(SelectArguments, SelectCallback);
			result = data;
			data = null;
		}
		else
		{
			result = DataSourceResolver.ResolveDataSource(DataSource, DataMember);
		}
		return result;
	}

	/// <summary>Determines whether data binding is required.</summary>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="M:System.Web.UI.WebControls.Repeater.OnDataPropertyChanged" /> is called during the data-binding phase of the control.</exception>
	protected virtual void OnDataPropertyChanged()
	{
		if (Initialized)
		{
			RequiresDataBinding = true;
		}
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.WebControls.Repeater.RequiresDataBinding" /> property to <see langword="true" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
	{
		RequiresDataBinding = true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Page page = Page;
		if (page != null)
		{
			page.PreLoad += OnPagePreLoad;
			if (!base.IsViewStateEnabled && page.IsPostBack)
			{
				RequiresDataBinding = true;
			}
		}
	}

	private void OnPagePreLoad(object sender, EventArgs e)
	{
		Initialize();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event and performs other initialization. </summary>
	/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object that contains the event data. </param>
	protected internal override void OnLoad(EventArgs e)
	{
		if (!Initialized)
		{
			Initialize();
		}
		base.OnLoad(e);
	}

	private void Initialize()
	{
		Page page = Page;
		if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && ViewState["Items"] == null)))
		{
			RequiresDataBinding = true;
		}
		if (IsBoundUsingDataSourceID)
		{
			ConnectToDataSource();
		}
		initialized = true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> object contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		preRendered = true;
		EnsureDataBound();
		base.OnPreRender(e);
	}

	private void ConnectToDataSource()
	{
		object obj = null;
		if (Parent != null)
		{
			obj = Parent.FindControl(DataSourceID);
		}
		if (obj == null || !(obj is IDataSource))
		{
			string format = ((obj != null) ? "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  '{1}' is not an IDataSource." : "DataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.");
			throw new HttpException(string.Format(format, ID, DataSourceID));
		}
		boundDataSource = (IDataSource)obj;
		boundDataSource.GetView(string.Empty).DataSourceViewChanged += OnDataSourceViewChanged;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Repeater" /> class.</summary>
	public Repeater()
	{
	}

	static Repeater()
	{
		ItemCommand = new object();
		ItemCreated = new object();
		ItemDataBound = new object();
	}
}
