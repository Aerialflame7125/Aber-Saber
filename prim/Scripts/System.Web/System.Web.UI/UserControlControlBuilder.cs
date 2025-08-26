using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Supports the ASP.NET page parser in building an instance of a user control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class UserControlControlBuilder : ControlBuilder
{
	/// <summary>Determines whether the control builder needs to get the control's inner text. </summary>
	/// <returns>
	///     <see langword="true" /> if the control builder requires the control's inner text; otherwise, <see langword="false" />. </returns>
	public override bool NeedsTagInnerText()
	{
		return false;
	}

	/// <summary>Provides the <see cref="T:System.Web.UI.UserControlControlBuilder" /> object with the inner text of the control tag.</summary>
	/// <param name="text">The text to be provided.</param>
	[MonoTODO("Not implemented, does nothing")]
	public override void SetTagInnerText(string text)
	{
	}

	/// <summary>Builds an instance of the control identified by the <see cref="P:System.Web.UI.ControlBuilder.ControlType" /> property. </summary>
	/// <returns>An instance of a user control identified by <see cref="P:System.Web.UI.ControlBuilder.ControlType" />.</returns>
	public override object BuildObject()
	{
		return base.BuildObject();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.UserControlControlBuilder" /> class. </summary>
	public UserControlControlBuilder()
	{
	}
}
