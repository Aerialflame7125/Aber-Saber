namespace System.Web.UI.WebControls.Adapters;

/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> object with which this control adapter is associated, for specific browser requests.</summary>
public class HierarchicalDataBoundControlAdapter : WebControlAdapter
{
	/// <summary>Retrieves a strongly typed reference to the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> control associated with this <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> object.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> associated with the current instance of <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" />.</returns>
	protected new HierarchicalDataBoundControl Control => (HierarchicalDataBoundControl)control;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> class.</summary>
	public HierarchicalDataBoundControlAdapter()
	{
	}

	internal HierarchicalDataBoundControlAdapter(HierarchicalDataBoundControl c)
		: base(c)
	{
	}

	/// <summary>Binds the data in the data source of the associated hierarchical data-bound control to the adapter.</summary>
	protected internal virtual void PerformDataBinding()
	{
		Control.PerformDataBinding();
	}
}
