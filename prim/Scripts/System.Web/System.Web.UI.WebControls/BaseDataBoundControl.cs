using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class for controls that bind to data using an ASP.NET data source control.</summary>
[DefaultProperty("DataSourceID")]
[Designer("System.Web.UI.Design.WebControls.BaseDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class BaseDataBoundControl : WebControl
{
	private static readonly object dataBoundEvent = new object();

	private EventHandlerList events = new EventHandlerList();

	private object dataSource;

	private bool initialized;

	private bool preRendered;

	private bool requiresDataBinding;

	/// <summary>Gets or sets the object from which the data-bound control retrieves its list of data items.</summary>
	/// <returns>An object that represents the data source from which the data-bound control retrieves its data. The default is <see langword="null" />.</returns>
	[Bindable(true)]
	[Themeable(false)]
	[DefaultValue(null)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual object DataSource
	{
		get
		{
			return dataSource;
		}
		set
		{
			if (value != null)
			{
				ValidateDataSource(value);
			}
			dataSource = value;
			OnDataPropertyChanged();
		}
	}

	/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
	/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Themeable(false)]
	public virtual string DataSourceID
	{
		get
		{
			return ViewState.GetString("DataSourceID", string.Empty);
		}
		set
		{
			ViewState["DataSourceID"] = value;
			OnDataPropertyChanged();
		}
	}

	/// <summary>Gets a value indicating whether the data-bound control has been initialized.</summary>
	/// <returns>
	///     <see langword="true" /> if the data-bound control has been initialized; otherwise, <see langword="false" />.</returns>
	protected bool Initialized => initialized;

	/// <summary>Gets a value indicating whether the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set.</summary>
	/// <returns>The value<see langword=" true" /> is returned if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set to value other than <see cref="F:System.String.Empty" />; otherwise, the value is <see langword="false" />.</returns>
	protected bool IsBoundUsingDataSourceID => DataSourceID.Length > 0;

	/// <summary>Gets or sets a value indicating whether the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called. </summary>
	/// <returns>The returned value is<see langword=" true" /> if the data-bound control's <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method should be called before the control is rendered; otherwise, the value is <see langword="false" />.</returns>
	protected bool RequiresDataBinding
	{
		get
		{
			return requiresDataBinding;
		}
		set
		{
			if (value && preRendered && IsBoundUsingDataSourceID && Page != null && !Page.IsCallback)
			{
				requiresDataBinding = true;
				EnsureDataBound();
			}
			else
			{
				requiresDataBinding = value;
			}
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Occurs after the server control binds to a data source.</summary>
	public event EventHandler DataBound
	{
		add
		{
			events.AddHandler(dataBoundEvent, value);
		}
		remove
		{
			events.RemoveHandler(dataBoundEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BaseDataBoundControl" /> class.</summary>
	protected BaseDataBoundControl()
	{
	}

	internal BaseDataBoundControl(HtmlTextWriterTag tag)
		: base(tag)
	{
	}

	/// <summary>Sets the initialized state of the data-bound control.</summary>
	protected void ConfirmInitState()
	{
		initialized = true;
	}

	/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
	public override void DataBind()
	{
		PerformSelect();
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.BaseDataBoundControl.DataBind" /> method if the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> property is set and the data-bound control is marked to require binding.</summary>
	protected virtual void EnsureDataBound()
	{
		if (RequiresDataBinding && IsBoundUsingDataSourceID)
		{
			DataBind();
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BaseDataBoundControl.DataBound" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnDataBound(EventArgs e)
	{
		if (events[dataBoundEvent] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to change the property value during the data-binding phase of the control.</exception>
	protected virtual void OnDataPropertyChanged()
	{
		if (Initialized)
		{
			RequiresDataBinding = true;
		}
	}

	/// <summary>Handles the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Page.PreLoad += OnPagePreLoad;
		if (!base.IsViewStateEnabled && Page != null && Page.IsPostBack)
		{
			RequiresDataBinding = true;
		}
	}

	/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
	/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnPagePreLoad(object sender, EventArgs e)
	{
		ConfirmInitState();
	}

	/// <summary>Handles the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		preRendered = true;
		EnsureDataBound();
		base.OnPreRender(e);
	}

	internal Control FindDataSource()
	{
		Control namingContainer = NamingContainer;
		string dataSourceID = DataSourceID;
		while (namingContainer != null)
		{
			Control control = namingContainer.FindControl(dataSourceID);
			if (control != null)
			{
				return control;
			}
			namingContainer = namingContainer.NamingContainer;
		}
		return null;
	}

	/// <summary>When overridden in a derived class, controls how data is retrieved and bound to the control.</summary>
	protected abstract void PerformSelect();

	/// <summary>When overridden in a derived class, verifies that the object a data-bound control binds to is one it can work with.</summary>
	/// <param name="dataSource">The object to verify. Typically an instance of <see cref="T:System.Collections.IEnumerable" />, <see cref="T:System.ComponentModel.IListSource" />, <see cref="T:System.Web.UI.IDataSource" />, or <see cref="T:System.Web.UI.IHierarchicalDataSource" />.</param>
	protected abstract void ValidateDataSource(object dataSource);
}
