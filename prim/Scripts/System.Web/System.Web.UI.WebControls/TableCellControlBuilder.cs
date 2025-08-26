namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.TableCell" /> control.</summary>
public class TableCellControlBuilder : ControlBuilder
{
	/// <summary>Specifies whether white space literals are allowed.</summary>
	/// <returns>
	///     <see langword="false" />.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableCellControlBuilder" /> class. </summary>
	public TableCellControlBuilder()
	{
	}
}
