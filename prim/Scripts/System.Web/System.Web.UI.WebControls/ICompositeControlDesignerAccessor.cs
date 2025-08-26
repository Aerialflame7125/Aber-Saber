namespace System.Web.UI.WebControls;

/// <summary>Provides an interface to allow a composite-control designer to recreate the child controls of its associated control at design time.</summary>
public interface ICompositeControlDesignerAccessor
{
	/// <summary>In a control derived from <see cref="T:System.Web.UI.WebControls.CompositeControl" />, recreates the child controls at design time. Called by the control's associated designer.</summary>
	void RecreateChildControls();
}
