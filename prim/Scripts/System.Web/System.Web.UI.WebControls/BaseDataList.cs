using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the abstract base class for data listing controls, such as <see cref="T:System.Web.UI.WebControls.DataList" /> and <see cref="T:System.Web.UI.WebControls.DataGrid" />. This class provides the methods and properties common to all data listing controls.</summary>
[DefaultEvent("SelectedIndexChanged")]
[DefaultProperty("DataSource")]
[Designer("System.Web.UI.Design.WebControls.BaseDataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class BaseDataList : WebControl
{
	private static readonly object selectedIndexChangedEvent = new object();

	private DataKeyCollection keycoll;

	private object source;

	private IDataSource boundDataSource;

	private bool initialized;

	private bool requiresDataBinding;

	private DataSourceSelectArguments selectArguments;

	private IEnumerable data;

	/// <summary>Gets or sets the text to render in an HTML caption element in the control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
	/// <returns>A string that represents the text to render in an HTML caption element in the control. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual string Caption
	{
		get
		{
			return ViewState.GetString("Caption", string.Empty);
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("Caption");
			}
			else
			{
				ViewState["Caption"] = value;
			}
		}
	}

	/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. The default value is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values. </exception>
	[DefaultValue(TableCaptionAlign.NotSet)]
	public virtual TableCaptionAlign CaptionAlign
	{
		get
		{
			return (TableCaptionAlign)ViewState.GetInt("CaptionAlign", 0);
		}
		set
		{
			if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid TableCaptionAlign value."));
			}
			ViewState["CaptionAlign"] = value;
		}
	}

	/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
	/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
	[DefaultValue(-1)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellPadding
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return -1;
			}
			return TableStyle.CellPadding;
		}
		set
		{
			TableStyle.CellPadding = value;
		}
	}

	/// <summary>Gets or sets the amount of space between cells.</summary>
	/// <returns>The amount of space (in pixels) between cells. The default value is 0.</returns>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual int CellSpacing
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return 0;
			}
			return TableStyle.CellSpacing;
		}
		set
		{
			TableStyle.CellSpacing = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains a collection of child controls in a data listing control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains a collection of child controls in a data listing control.</returns>
	public override ControlCollection Controls
	{
		get
		{
			EnsureChildControls();
			return base.Controls;
		}
	}

	/// <summary>Gets or sets the key field in the data source specified by the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property.</summary>
	/// <returns>The name of the key field in the data source specified by <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" />.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	[MonoTODO("incomplete")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataKeyField
	{
		get
		{
			return ViewState.GetString("DataKeyField", string.Empty);
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("DataKeyField");
			}
			else
			{
				ViewState["DataKeyField"] = value;
			}
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> object that stores the key values of each record in a data listing control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKeyCollection" /> that stores the key values of each record in a data listing control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public DataKeyCollection DataKeys
	{
		get
		{
			if (keycoll == null)
			{
				keycoll = new DataKeyCollection(DataKeysArray);
			}
			return keycoll;
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> object that contains the key values of each record in a data listing control.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains the key values of each record in a data listing control.</returns>
	protected ArrayList DataKeysArray
	{
		get
		{
			ArrayList arrayList = (ArrayList)ViewState["DataKeys"];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				ViewState["DataKeys"] = arrayList;
			}
			return arrayList;
		}
	}

	/// <summary>Gets or sets the specific data member in a multimember data source to bind to a data listing control.</summary>
	/// <returns>A data member from a multimember data source. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public string DataMember
	{
		get
		{
			return ViewState.GetString("DataMember", string.Empty);
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
			OnDataPropertyChanged();
		}
	}

	/// <summary>Gets or sets the source containing a list of values used to populate the items within the control.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> or <see cref="T:System.ComponentModel.IListSource" /> that contains a collection of values used to supply data to this control. The default value is <see langword="null" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
	/// <exception cref="T:System.ArgumentException">The data source is of an invalid type. The data source must be <see langword="null" /> or implement either the <see cref="T:System.Collections.IEnumerable" /> or the <see cref="T:System.ComponentModel.IListSource" /> interface.</exception>
	[Bindable(true)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual object DataSource
	{
		get
		{
			return source;
		}
		set
		{
			if (value == null || value is IEnumerable || value is IListSource)
			{
				source = value;
				OnDataPropertyChanged();
				return;
			}
			throw new ArgumentException(Locale.GetText("Invalid data source. This requires an object implementing {0} or {1}.", "IEnumerable", "IListSource"));
		}
	}

	/// <summary>Gets or sets a value that specifies whether the border between the cells of a data listing control is displayed.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default value is <see langword="Both" />.</returns>
	[DefaultValue(GridLines.Both)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual GridLines GridLines
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return GridLines.Both;
			}
			return TableStyle.GridLines;
		}
		set
		{
			TableStyle.GridLines = value;
		}
	}

	/// <summary>Gets or sets the horizontal alignment of a data listing control within its container.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default value is <see langword="NotSet" />.</returns>
	[Category("Layout")]
	[DefaultValue(HorizontalAlign.NotSet)]
	[WebSysDescription("")]
	public virtual HorizontalAlign HorizontalAlign
	{
		get
		{
			if (!base.ControlStyleCreated)
			{
				return HorizontalAlign.NotSet;
			}
			return TableStyle.HorizontalAlign;
		}
		set
		{
			TableStyle.HorizontalAlign = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the data listing control renders its header in an accessible format. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
	/// <returns>
	///     <see langword="true" /> if the control renders its header in an accessible format; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public virtual bool UseAccessibleHeader
	{
		get
		{
			return ViewState.GetBool("UseAccessibleHeader", def: false);
		}
		set
		{
			ViewState["UseAccessibleHeader"] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.Control.ID" /> property of the data source control that the data listing control should use to retrieve its data source.</summary>
	/// <returns>The programmatic identifier assigned to the data source control.</returns>
	/// <exception cref="T:System.Web.HttpException">The data source cannot be resolved because a value is specified for both the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSource" /> property and the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property. </exception>
	[DefaultValue("")]
	[IDReferenceProperty(typeof(DataSourceControl))]
	[Themeable(false)]
	public virtual string DataSourceID
	{
		get
		{
			return ViewState.GetString("DataSourceID", string.Empty);
		}
		set
		{
			if (source != null)
			{
				throw new InvalidOperationException(Locale.GetText("DataSource is already set."));
			}
			ViewState["DataSourceID"] = value;
			OnDataPropertyChanged();
		}
	}

	/// <summary>Gets a value indicating whether the control has been initialized.</summary>
	/// <returns>
	///     <see langword="true" /> if the control has been initialized; otherwise, <see langword="false" />.</returns>
	protected bool Initialized => initialized;

	/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> property is set.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="P:System.Web.UI.WebControls.BaseDataList.DataSourceID" /> is set to a value other than <see cref="F:System.String.Empty" />; otherwise, <see langword="false" />.</returns>
	protected bool IsBoundUsingDataSourceID => DataSourceID.Length != 0;

	/// <summary>Gets or sets a value indicating whether the data listing control needs to bind to its specified data source.</summary>
	/// <returns>
	///     <see langword="true" /> if the control needs to bind to a data source; otherwise, <see langword="false" />.</returns>
	protected bool RequiresDataBinding
	{
		get
		{
			return requiresDataBinding;
		}
		set
		{
			requiresDataBinding = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used by the data-bound control to retrieve data. The default is to return the value from <see cref="M:System.Web.UI.WebControls.BaseDataList.CreateDataSourceSelectArguments" />.</returns>
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

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	private TableStyle TableStyle => (TableStyle)base.ControlStyle;

	private bool IsDataBound
	{
		get
		{
			return ViewState.GetBool("_DataBound", def: false);
		}
		set
		{
			ViewState["_DataBound"] = value;
		}
	}

	/// <summary>Occurs when a different item is selected in a data listing control between posts to the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler SelectedIndexChanged
	{
		add
		{
			base.Events.AddHandler(selectedIndexChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(selectedIndexChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
	protected BaseDataList()
	{
	}

	/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> collection.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	protected override void AddParsedSubObject(object obj)
	{
	}

	/// <summary>Creates a child control using the view state.</summary>
	protected internal override void CreateChildControls()
	{
		if (HasControls())
		{
			base.Controls.Clear();
		}
		if (IsDataBound)
		{
			CreateControlHierarchy(useDataSource: false);
		}
		else if (RequiresDataBinding)
		{
			EnsureDataBound();
		}
	}

	/// <summary>When overridden in a derived class, creates the control hierarchy for the data listing control with or without the specified data source.</summary>
	/// <param name="useDataSource">
	///       <see langword="true" /> to use the control's data source; otherwise, <see langword="false" />.</param>
	protected abstract void CreateControlHierarchy(bool useDataSource);

	/// <summary>Binds the control and all its child controls to the specified data source.</summary>
	public override void DataBind()
	{
		OnDataBinding(EventArgs.Empty);
		if (HasControls())
		{
			Controls.Clear();
		}
		if (base.HasChildViewState)
		{
			ClearChildViewState();
		}
		if (!base.IsTrackingViewState)
		{
			TrackViewState();
		}
		CreateControlHierarchy(useDataSource: true);
		base.ChildControlsCreated = true;
		RequiresDataBinding = false;
		IsDataBound = true;
	}

	/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />.</returns>
	protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
	{
		return DataSourceSelectArguments.Empty;
	}

	/// <summary>Verifies that the data listing control requires data binding and that a valid data source control is specified before calling the <see cref="M:System.Web.UI.WebControls.BaseDataList.DataBind" /> method.</summary>
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

	/// <summary>Returns an <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" />-implemented object that represents the data source.</returns>
	protected virtual IEnumerable GetData()
	{
		if (DataSourceID.Length == 0)
		{
			return null;
		}
		if (boundDataSource == null)
		{
			ConnectToDataSource();
		}
		boundDataSource.GetView(string.Empty).Select(SelectArguments, SelectCallback);
		return data;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnDataBinding(EventArgs e)
	{
		base.OnDataBinding(e);
	}

	/// <summary>Called when one of the base data source identification properties is changed, to rebind the data-bound control to its data.</summary>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
	protected virtual void OnDataPropertyChanged()
	{
		if (Initialized)
		{
			RequiresDataBinding = true;
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
	{
		RequiresDataBinding = true;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
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
		if (!Initialized)
		{
			Initialize();
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
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
		if (page != null && (!page.IsPostBack || (base.IsViewStateEnabled && !IsDataBound)))
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
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		EnsureDataBound();
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataList.SelectedIndexChanged" /> event of a <see cref="T:System.Web.UI.WebControls.BaseDataList" /> control. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectedIndexChanged(EventArgs e)
	{
		((EventHandler)base.Events[selectedIndexChangedEvent])?.Invoke(this, e);
	}

	/// <summary>Sets up the control hierarchy for the data-bound control.</summary>
	protected abstract void PrepareControlHierarchy();

	/// <summary>Renders the control to the specified HTML writer.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		PrepareControlHierarchy();
		RenderContents(writer);
	}

	/// <summary>Determines whether the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> that contains the data type to test. </param>
	/// <returns>
	///     <see langword="true" /> if the specified data type can be bound to a list control that derives from the <see cref="T:System.Web.UI.WebControls.BaseDataList" /> class; otherwise, <see langword="false" />.</returns>
	public static bool IsBindableType(Type type)
	{
		if (type == null)
		{
			throw new NullReferenceException();
		}
		TypeCode typeCode = Type.GetTypeCode(type);
		if ((uint)(typeCode - 3) <= 13u || typeCode == TypeCode.String)
		{
			return true;
		}
		return false;
	}

	private void ConnectToDataSource()
	{
		if (NamingContainer != null)
		{
			boundDataSource = NamingContainer.FindControl(DataSourceID) as IDataSource;
		}
		if (boundDataSource == null)
		{
			if (Parent != null)
			{
				boundDataSource = Parent.FindControl(DataSourceID) as IDataSource;
			}
			if (boundDataSource == null)
			{
				throw new HttpException(Locale.GetText("Coulnd't find a DataSource named '{0}'.", DataSourceID));
			}
		}
		boundDataSource.GetView(string.Empty).DataSourceViewChanged += OnDataSourceViewChanged;
	}
}
