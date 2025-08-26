using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.Adapters;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in list or tabular form.</summary>
[Designer("System.Web.UI.Design.WebControls.DataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class DataBoundControl : BaseDataBoundControl
{
	private DataSourceSelectArguments selectArguments;

	private DataSourceView currentView;

	/// <summary>Gets or sets the name of the list of data that the data-bound control binds to, in cases where the data source contains more than one distinct list of data items.</summary>
	/// <returns>The name of the specific list of data that the data-bound control binds to, if more than one list is supplied by a data source control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebCategory("Data")]
	public virtual string DataMember
	{
		get
		{
			return ViewState.GetString("DataMember", string.Empty);
		}
		set
		{
			ViewState["DataMember"] = value;
		}
	}

	/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
	/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data.</returns>
	[IDReferenceProperty(typeof(DataSourceControl))]
	public override string DataSourceID
	{
		get
		{
			return ViewState.GetString("DataSourceID", string.Empty);
		}
		set
		{
			ViewState["DataSourceID"] = value;
			base.DataSourceID = value;
		}
	}

	/// <summary>Gets an object that implements the <see cref="T:System.Web.UI.IDataSource" /> interface, which provides access to the object's data content.</summary>
	/// <returns>An object with access to its data content.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IDataSource DataSourceObject => GetDataSource();

	/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that the data-bound control uses when retrieving data from a data source control. </summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> value used by the data-bound control to retrieve data. The default is <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
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
		private set
		{
			selectArguments = value;
		}
	}

	private bool IsDataBound
	{
		get
		{
			object obj = ViewState["DataBound"];
			if (obj == null)
			{
				return false;
			}
			return (bool)obj;
		}
		set
		{
			ViewState["DataBound"] = value;
		}
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected DataBoundControl()
	{
	}

	internal DataBoundControl(HtmlTextWriterTag tag)
		: base(tag)
	{
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.IDataSource" /> interface that the data-bound control is associated with, if any.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IDataSource" /> that represents the data source identified by <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" />. </returns>
	/// <exception cref="T:System.Web.HttpException">The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The control identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IDataSource" /> interface.</exception>
	protected virtual IDataSource GetDataSource()
	{
		if (base.IsBoundUsingDataSourceID)
		{
			Control obj = FindDataSource() ?? throw new HttpException($"A control with ID '{DataSourceID}' could not be found.");
			if (!(obj is IDataSource))
			{
				throw new HttpException($"The control with ID '{DataSourceID}' is not a control of type IDataSource.");
			}
			return (IDataSource)obj;
		}
		if (DataSource is IDataSource result)
		{
			return result;
		}
		return new CollectionDataSource(DataSourceResolver.ResolveDataSource(DataSource, DataMember));
	}

	/// <summary>Retrieves a <see cref="T:System.Web.UI.DataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
	/// <returns>The <see cref="T:System.Web.UI.DataSourceView" /> that the data-bound control uses to perform data operations. If the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set, a specific, named <see cref="T:System.Web.UI.DataSourceView" /> is returned; otherwise, the default <see cref="T:System.Web.UI.DataSourceView" /> is returned.</returns>
	/// <exception cref="T:System.InvalidOperationException">Both the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> and <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSourceID" /> properties are set.- or -The <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" /> property is set but a <see cref="T:System.Web.UI.DataSourceView" /> object by that name does not exist.</exception>
	protected virtual DataSourceView GetData()
	{
		if (currentView == null)
		{
			UpdateViewData();
		}
		return currentView;
	}

	private DataSourceView InternalGetData()
	{
		if (currentView != null)
		{
			return currentView;
		}
		if (DataSource != null && base.IsBoundUsingDataSourceID)
		{
			throw new HttpException("Control bound using both DataSourceID and DataSource properties.");
		}
		return GetDataSource()?.GetView(DataMember);
	}

	/// <summary>Rebinds the data-bound control to its data after one of the base data source identification properties changes.</summary>
	protected override void OnDataPropertyChanged()
	{
		base.OnDataPropertyChanged();
		currentView = null;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
	/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.DataSourceView" />.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
	{
		base.RequiresDataBinding = true;
	}

	/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
	/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnPagePreLoad(object sender, EventArgs e)
	{
		base.OnPagePreLoad(sender, e);
		Initialize();
	}

	private void Initialize()
	{
		Page page = Page;
		if (page != null && !IsDataBound)
		{
			if (!page.IsPostBack)
			{
				base.RequiresDataBinding = true;
			}
			else if (base.IsViewStateEnabled)
			{
				base.RequiresDataBinding = true;
			}
		}
	}

	private void UpdateViewData()
	{
		if (currentView != null)
		{
			currentView.DataSourceViewChanged -= OnDataSourceViewChanged;
		}
		DataSourceView dataSourceView = InternalGetData();
		if (dataSourceView != currentView)
		{
			currentView = dataSourceView;
		}
		if (currentView != null)
		{
			currentView.DataSourceViewChanged += OnDataSourceViewChanged;
		}
	}

	/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
	protected internal override void OnLoad(EventArgs e)
	{
		UpdateViewData();
		if (!base.Initialized)
		{
			Initialize();
			ConfirmInitState();
		}
		base.OnLoad(e);
	}

	/// <summary>When overridden in a derived class, binds data from the data source to the control. </summary>
	/// <param name="data">The <see cref="T:System.Collections.IEnumerable" /> list of data returned from a <see cref="M:System.Web.UI.WebControls.DataBoundControl.PerformSelect" /> method call.</param>
	protected internal virtual void PerformDataBinding(IEnumerable data)
	{
	}

	/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
	/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
	/// <exception cref="T:System.InvalidOperationException">The object passed by the <paramref name="dataSource" /> parameter is not <see langword="null" /> or a recognized type.</exception>
	protected override void ValidateDataSource(object dataSource)
	{
		if (dataSource == null || dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
		{
			return;
		}
		throw new ArgumentException("Invalid data source source type. The data source must be of type IListSource, IEnumerable or IDataSource.");
	}

	/// <summary>Retrieves data from the associated data source.</summary>
	protected override void PerformSelect()
	{
		if (!base.IsBoundUsingDataSourceID)
		{
			OnDataBinding(EventArgs.Empty);
		}
		base.RequiresDataBinding = false;
		SelectArguments = CreateDataSourceSelectArguments();
		GetData().Select(SelectArguments, OnSelect);
		MarkAsDataBound();
		OnDataBound(EventArgs.Empty);
	}

	private void OnSelect(IEnumerable data)
	{
		if (base.IsBoundUsingDataSourceID)
		{
			OnDataBinding(EventArgs.Empty);
		}
		InternalPerformDataBinding(data);
	}

	internal void InternalPerformDataBinding(IEnumerable data)
	{
		if (base.Adapter is DataBoundControlAdapter dataBoundControlAdapter)
		{
			dataBoundControlAdapter.PerformDataBinding(data);
		}
		else
		{
			PerformDataBinding(data);
		}
	}

	/// <summary>Creates a default <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used by the data-bound control if no arguments are specified.</summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> initialized to <see cref="P:System.Web.UI.DataSourceSelectArguments.Empty" />. </returns>
	protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
	{
		return DataSourceSelectArguments.Empty;
	}

	/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
	protected void MarkAsDataBound()
	{
		IsDataBound = true;
	}
}
