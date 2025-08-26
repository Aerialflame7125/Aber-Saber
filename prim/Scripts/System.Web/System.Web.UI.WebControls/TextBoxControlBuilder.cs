namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TextBox" /> control.</summary>
public class TextBoxControlBuilder : ControlBuilder
{
	/// <summary>Specifies whether white-space literals are allowed.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Determines whether the literal string of the <see cref="T:System.Web.UI.WebControls.TextBox" /> control must be HTML decoded.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool HtmlDecodeLiterals()
	{
		return true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TextBoxControlBuilder" /> class. </summary>
	public TextBoxControlBuilder()
	{
	}
}
