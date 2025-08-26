namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
public class MultiViewControlBuilder : ControlBuilder
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MultiViewControlBuilder" /> class. </summary>
	public MultiViewControlBuilder()
	{
	}

	/// <summary>Adds builders to the <see cref="T:System.Web.UI.ControlBuilder" /> object for any child controls that belong to the <see cref="T:System.Web.UI.WebControls.MultiView" /> control.</summary>
	/// <param name="subBuilder">The <see langword="ControlBuilder" /> object assigned to the child control. </param>
	public override void AppendSubBuilder(ControlBuilder subBuilder)
	{
		base.AppendSubBuilder(subBuilder);
	}
}
