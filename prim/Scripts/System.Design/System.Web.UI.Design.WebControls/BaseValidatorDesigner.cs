namespace System.Web.UI.Design.WebControls;

/// <summary>Provides design-time support in a visual designer for Web server controls that are derived from the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> class.</summary>
public class BaseValidatorDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.BaseValidatorDesigner" /> class.</summary>
	public BaseValidatorDesigner()
	{
	}

	/// <summary>Gets the markup that is used to render the associated control at design time.</summary>
	/// <returns>A string containing the markup used to render the <see cref="T:System.Web.UI.WebControls.BaseValidator" /> at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
