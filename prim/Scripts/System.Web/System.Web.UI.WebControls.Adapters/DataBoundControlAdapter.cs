using System.Collections;

namespace System.Web.UI.WebControls.Adapters;

/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object with which the adapter is associated for specific browser requests.</summary>
public class DataBoundControlAdapter : WebControlAdapter
{
	/// <summary>Retrieves a strongly-typed reference to the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object associated with this control adapter.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> is attached.</returns>
	protected new DataBoundControl Control => (DataBoundControl)control;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> class.</summary>
	public DataBoundControlAdapter()
	{
	}

	internal DataBoundControlAdapter(DataBoundControl c)
		: base(c)
	{
	}

	/// <summary>Binds the data in the data source of the associated <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to the control adapter.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> of <see cref="T:System.Object" /> to be bound to the derived <see cref="T:System.Web.UI.WebControls.DataBoundControl" />.</param>
	protected internal virtual void PerformDataBinding(IEnumerable data)
	{
		Control.PerformDataBinding(data);
	}
}
