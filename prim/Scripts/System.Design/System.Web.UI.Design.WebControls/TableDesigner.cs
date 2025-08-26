namespace System.Web.UI.Design.WebControls;

/// <summary>Extends design-time behavior for the <see cref="T:System.Web.UI.WebControls.Table" /> Web server control.</summary>
public class TableDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.TableDesigner" /> class.</summary>
	public TableDesigner()
	{
	}

	/// <summary>Gets the HTML that is used to represent the control at design time.</summary>
	/// <returns>The HTML used to represent the control at design time.</returns>
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}

	public override string GetPersistInnerHtml()
	{
		throw new NotImplementedException();
	}
}
