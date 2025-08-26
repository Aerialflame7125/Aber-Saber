using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents the style for a section of a <see cref="T:System.Web.UI.WebControls.Table" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TableSectionStyle : Style
{
	/// <summary>Gets or sets a value indicating whether the table section is displayed.</summary>
	/// <returns>
	///     <see langword="true" /> if the table section is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[NotifyParentProperty(true)]
	public bool Visible
	{
		get
		{
			object obj = base.ViewState["Visible"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return true;
		}
		set
		{
			base.ViewState["Visible"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TableSectionStyle" /> class. </summary>
	public TableSectionStyle()
	{
	}
}
