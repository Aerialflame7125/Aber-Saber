using System.ComponentModel;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the base class for all ASP.NET version 2.0 data-bound controls that display their data in hierarchical form.</summary>
[Designer("System.Web.UI.Design.WebControls.HierarchicalDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public abstract class HierarchicalDataBoundControl : BaseDataBoundControl
{
	/// <summary>Gets or sets the ID of the control from which the data-bound control retrieves its list of data items.</summary>
	/// <returns>The ID of a control that represents the data source from which the data-bound control retrieves its data. The default is <see cref="F:System.String.Empty" />.</returns>
	[IDReferenceProperty(typeof(HierarchicalDataSourceControl))]
	public override string DataSourceID
	{
		get
		{
			object obj = ViewState["DataSourceID"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
			ViewState["DataSourceID"] = value;
		}
	}

	private bool IsDataBound
	{
		get
		{
			return ViewState.GetBool("DataBound", def: false);
		}
		set
		{
			ViewState["DataBound"] = value;
		}
	}

	/// <summary>Retrieves a <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations.</summary>
	/// <param name="viewPath">The hierarchical path of the view to retrieve.</param>
	/// <returns>The <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> object that the data-bound control uses to perform data operations. </returns>
	/// <exception cref="T:System.InvalidOperationException">A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> could not be retrieved for the specified <paramref name="viewPath" />.</exception>
	protected virtual HierarchicalDataSourceView GetData(string viewPath)
	{
		if (DataSource != null && !string.IsNullOrEmpty(DataSourceID))
		{
			throw new HttpException();
		}
		IHierarchicalDataSource hierarchicalDataSource = GetDataSource();
		if (hierarchicalDataSource != null)
		{
			return hierarchicalDataSource.GetHierarchicalView(viewPath);
		}
		if (DataSource is IHierarchicalEnumerable)
		{
			return new ReadOnlyDataSourceView((IHierarchicalEnumerable)DataSource);
		}
		return null;
	}

	/// <summary>Retrieves the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> that the data-bound control is associated with, if any.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that represents the data source identified by the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property. </returns>
	/// <exception cref="T:System.Web.HttpException">The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not exist in the current container.- or -The data source control identified by the <see cref="P:System.Web.UI.WebControls.HierarchicalDataBoundControl.DataSourceID" /> property does not implement the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
	protected virtual IHierarchicalDataSource GetDataSource()
	{
		if (base.IsBoundUsingDataSourceID)
		{
			Control obj = FindDataSource() ?? throw new HttpException($"A control with ID '{DataSourceID}' could not be found.");
			if (!(obj is IHierarchicalDataSource))
			{
				throw new HttpException($"The control with ID '{DataSourceID}' is not a control of type IHierarchicalDataSource.");
			}
			return (IHierarchicalDataSource)obj;
		}
		return DataSource as IHierarchicalDataSource;
	}

	/// <summary>Sets the state of the control in view state as successfully bound to data.</summary>
	protected void MarkAsDataBound()
	{
		IsDataBound = true;
	}

	/// <summary>Called when one of the base data source identification properties is changed, to re-bind the data-bound control to its data.</summary>
	protected override void OnDataPropertyChanged()
	{
		base.RequiresDataBinding = true;
	}

	/// <summary>Called when the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> instance that the data-bound control works with raises the <see cref="E:System.Web.UI.IDataSource.DataSourceChanged" /> event.</summary>
	/// <param name="sender">The source of the event, the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> object. </param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
	protected virtual void OnDataSourceChanged(object sender, EventArgs e)
	{
		base.RequiresDataBinding = true;
	}

	/// <summary>Handles the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
	protected internal override void OnLoad(EventArgs e)
	{
		if (!base.Initialized)
		{
			Initialize();
			ConfirmInitState();
		}
		base.OnLoad(e);
	}

	private void Initialize()
	{
		if (!Page.IsPostBack || (base.IsViewStateEnabled && !IsDataBound))
		{
			base.RequiresDataBinding = true;
		}
		IHierarchicalDataSource hierarchicalDataSource = GetDataSource();
		if (hierarchicalDataSource != null && DataSourceID != "")
		{
			hierarchicalDataSource.DataSourceChanged += OnDataSourceChanged;
		}
	}

	/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
	/// <param name="sender">The <see cref="T:System.Web.UI.Page" /> that raised the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnPagePreLoad(object sender, EventArgs e)
	{
		base.OnPagePreLoad(sender, e);
		Initialize();
	}

	protected void InternalPerformDataBinding()
	{
		if (base.Adapter is HierarchicalDataBoundControlAdapter hierarchicalDataBoundControlAdapter)
		{
			hierarchicalDataBoundControlAdapter.PerformDataBinding();
		}
		else
		{
			PerformDataBinding();
		}
	}

	/// <summary>When overridden in a derived class, binds data from the data source to the control.</summary>
	protected internal virtual void PerformDataBinding()
	{
	}

	/// <summary>Retrieves data from the associated data source.</summary>
	protected override void PerformSelect()
	{
		OnDataBinding(EventArgs.Empty);
		InternalPerformDataBinding();
		base.RequiresDataBinding = false;
		MarkAsDataBound();
		OnDataBound(EventArgs.Empty);
	}

	/// <summary>Verifies that the object a data-bound control binds to is one it can work with.</summary>
	/// <param name="dataSource">An object set to the <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" /> property.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="dataSource" /> is not <see langword="null" /> and implements neither the <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> nor the <see cref="T:System.Web.UI.IHierarchicalDataSource" /> interface.</exception>
	protected override void ValidateDataSource(object dataSource)
	{
		if (dataSource == null || dataSource is IHierarchicalDataSource || dataSource is IHierarchicalEnumerable)
		{
			return;
		}
		throw new InvalidOperationException("Invalid data source");
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> class.</summary>
	protected HierarchicalDataBoundControl()
	{
	}
}
