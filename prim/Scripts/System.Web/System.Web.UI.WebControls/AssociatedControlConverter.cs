namespace System.Web.UI.WebControls;

/// <summary>Provides a type converter that retrieves a list of <see cref="T:System.Web.UI.WebControls.WebControl" /> controls in the current container.</summary>
public class AssociatedControlConverter : ControlIDConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AssociatedControlConverter" /> class. </summary>
	public AssociatedControlConverter()
	{
	}

	/// <summary>Indicates whether the provided control inherits from <see cref="T:System.Web.UI.WebControls.WebControl" />.</summary>
	/// <param name="control">The control instance to test whether it is a <see cref="T:System.Web.UI.WebControls.WebControl" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="control" /> inherits from the <see cref="T:System.Web.UI.WebControls.WebControl" /> class; otherwise, <see langword="false" />.</returns>
	protected override bool FilterControl(Control control)
	{
		if (control is WebControl)
		{
			return true;
		}
		return false;
	}
}
