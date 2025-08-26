using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlHeadBuilder : ControlBuilder
{
	/// <summary>Determines whether the literal white space characters in the control must be processed or ignored.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control's child controls. </summary>
	/// <param name="tagName">The tag name of the child control.</param>
	/// <param name="attribs">An array of attributes contained in the child control.</param>
	/// <returns>The <see cref="T:System.Type" /> of the specified control's child control.</returns>
	public override Type GetChildControlType(string tagName, IDictionary attribs)
	{
		if (string.Compare(tagName, "title", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return typeof(HtmlTitle);
		}
		if (string.Compare(tagName, "link", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return typeof(HtmlLink);
		}
		if (string.Compare(tagName, "meta", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return typeof(HtmlMeta);
		}
		return null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHeadBuilder" /> class.</summary>
	public HtmlHeadBuilder()
	{
	}
}
