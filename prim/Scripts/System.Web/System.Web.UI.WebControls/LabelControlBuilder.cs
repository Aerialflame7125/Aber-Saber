namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.Label" /> control.</summary>
public class LabelControlBuilder : ControlBuilder
{
	/// <summary>Specifies whether white space literals are allowed.</summary>
	/// <returns>
	///     <see langword="false" /> for all cases.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LabelControlBuilder" /> class. </summary>
	public LabelControlBuilder()
	{
	}
}
