using System.Collections;
using System.Security.Permissions;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Interacts with the parser to build an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlSelectBuilder : ControlBuilder
{
	/// <summary>Determines whether the white space literals in an <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control are to be processed or ignored.</summary>
	/// <returns>This method always returns <see langword="false" />, indicating that white space in the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control is ignored.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Obtains the <see cref="T:System.Type" /> for the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's child controls.</summary>
	/// <param name="tagName">The tag name of the child control. </param>
	/// <param name="attribs">An array of attributes contained in the child control. </param>
	/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelect" /> control's specified child control.</returns>
	public override Type GetChildControlType(string tagName, IDictionary attribs)
	{
		if (string.Compare(tagName, "option", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			return null;
		}
		if (attribs["selected"] is string { Length: >0 } text && string.Compare(text, "selected", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			attribs["selected"] = "true";
		}
		return typeof(ListItem);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlSelectBuilder" /> class.</summary>
	public HtmlSelectBuilder()
	{
	}
}
