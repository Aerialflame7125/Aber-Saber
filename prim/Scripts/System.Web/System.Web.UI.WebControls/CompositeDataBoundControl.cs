using System.Collections;

namespace System.Web.UI.WebControls;

/// <summary>Represents the base class for a tabular data-bound control that is composed of other server controls.</summary>
public abstract class CompositeDataBoundControl : DataBoundControl, INamingContainer
{
	/// <summary>Gets a collection of the child controls within the composite data-bound control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls within the composite data-bound control.</returns>
	public override ControlCollection Controls
	{
		get
		{
			EnsureChildControls();
			return base.Controls;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CompositeDataBoundControl" /> class.</summary>
	protected CompositeDataBoundControl()
	{
	}

	/// <summary>Creates the control hierarchy that is used to render a composite data-bound control based on the values that are stored in view state.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		object obj = ViewState["_!ItemCount"];
		if (obj != null)
		{
			object[] array = new object[(int)obj];
			CreateChildControls(array, dataBinding: false);
		}
		else if (base.RequiresDataBinding)
		{
			EnsureDataBound();
		}
	}

	/// <summary>Binds the data from the data source to the composite data-bound control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the composite data-bound control.</param>
	protected internal override void PerformDataBinding(IEnumerable data)
	{
		base.PerformDataBinding(data);
		Controls.Clear();
		ViewState["_!ItemCount"] = CreateChildControls(data, dataBinding: true);
	}

	/// <summary>When overridden in an abstract class, creates the control hierarchy that is used to render the composite data-bound control based on the values from the specified data source.</summary>
	/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the control.</param>
	/// <param name="dataBinding">
	///       <see langword="true" /> to indicate that the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" /> is called during data binding; otherwise, <see langword="false" />.</param>
	/// <returns>The number of items created by the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" />.</returns>
	protected abstract int CreateChildControls(IEnumerable dataSource, bool dataBinding);
}
