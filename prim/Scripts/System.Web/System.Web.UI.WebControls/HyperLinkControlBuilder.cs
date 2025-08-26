namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.</summary>
public class HyperLinkControlBuilder : ControlBuilder
{
	/// <summary>Gets a value that indicates whether white spaces are allowed in literals for this control.</summary>
	/// <returns>Overloaded to always returns <see langword="false" /> to indicate that white spaces are not allowed.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkControlBuilder" /> class. </summary>
	public HyperLinkControlBuilder()
	{
	}
}
