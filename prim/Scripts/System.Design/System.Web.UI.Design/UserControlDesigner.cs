using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides designer functionality for user controls.</summary>
public class UserControlDesigner : ControlDesigner
{
	/// <summary>Gets the action list collection for the user control designer.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the action list tags for the control designer.</returns>
	public override DesignerActionListCollection ActionLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the user control can be resized.</summary>
	/// <returns>
	///   <see langword="false" /> .</returns>
	public override bool AllowResize => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.UserControlDesigner" /> class.</summary>
	public UserControlDesigner()
	{
	}

	/// <summary>Gets the HTML markup that is used to represent the user control at design time.</summary>
	/// <returns>The markup that is used to represent the control at design time.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Control.ID" /> property of a child control is empty or <see langword="null" />.</exception>
	public override string GetDesignTimeHtml()
	{
		return CreatePlaceHolderDesignTimeHtml();
	}
}
