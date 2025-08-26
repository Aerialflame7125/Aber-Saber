namespace System.Web.UI.Design.WebControls;

/// <summary>Used to provide design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Button" /> Web server control.</summary>
public class ButtonDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.ButtonDesigner" /> class.</summary>
	public ButtonDesigner()
	{
	}

	/// <summary>Gets the markup that is used to render the associated control at design time.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the markup used to render the <see cref="T:System.Web.UI.WebControls.Button" /> at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
