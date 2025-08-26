namespace System.Web.UI.Design;

/// <summary>Extends design-time behavior for Web server controls that have a <see langword="Text" /> property that is persisted as inner text.</summary>
public class TextControlDesigner : ControlDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TextControlDesigner" /> class.</summary>
	public TextControlDesigner()
	{
	}

	/// <summary>Gets the markup that is used to represent the associated control at design time.</summary>
	/// <returns>The markup that is used to represent the control at design time.</returns>
	[System.MonoTODO]
	public override string GetDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
