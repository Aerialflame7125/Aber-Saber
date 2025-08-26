namespace System.Web.UI.Design.WebControls;

/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> Web server control.</summary>
public class HyperLinkDesigner : TextControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.HyperLinkDesigner" /> class.</summary>
	public HyperLinkDesigner()
	{
	}

	/// <summary>Gets the markup that is used to render the associated control at design time.</summary>
	/// <returns>A string containing the markup used to render the associated hyperlink control at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
