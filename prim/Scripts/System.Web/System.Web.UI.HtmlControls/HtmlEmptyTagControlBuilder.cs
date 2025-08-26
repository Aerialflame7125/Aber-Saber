using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Interacts with the page parser to build HTML server controls that do not have a body or closing tag. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HtmlEmptyTagControlBuilder : ControlBuilder
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlEmptyTagControlBuilder" /> class.</summary>
	public HtmlEmptyTagControlBuilder()
	{
	}

	/// <summary>Indicates that the controls built with the <see cref="T:System.Web.UI.HtmlControls.HtmlEmptyTagControlBuilder" /> control do not have closing tags.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool HasBody()
	{
		return false;
	}
}
