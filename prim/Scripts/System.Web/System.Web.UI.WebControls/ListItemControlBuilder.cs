namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.ListItem" /> control.</summary>
public class ListItemControlBuilder : ControlBuilder
{
	/// <summary>Determines whether white spaces in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are represented by <see cref="T:System.Web.UI.LiteralControl" /> objects.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Determines whether HTML entities in the text associated with the <see cref="T:System.Web.UI.WebControls.ListItem" /> are converted to their equivalent characters when the text is parsed.</summary>
	/// <returns>
	///     <see langword="true" /> for all cases.</returns>
	public override bool HtmlDecodeLiterals()
	{
		return true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListItemControlBuilder" /> class. </summary>
	public ListItemControlBuilder()
	{
	}
}
