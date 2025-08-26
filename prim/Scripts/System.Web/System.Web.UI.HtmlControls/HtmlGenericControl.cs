using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Defines the methods, properties, and events for all HTML server control elements not represented by a specific .NET Framework class.</summary>
[ConstructorNeedsTag(true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlGenericControl : HtmlContainerControl
{
	/// <summary>Gets or sets the name of the HTML element represented by the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> control.</summary>
	/// <returns>The tag name of an element.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public new string TagName
	{
		get
		{
			return _tagName;
		}
		set
		{
			_tagName = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with default values.</summary>
	public HtmlGenericControl()
		: this("span")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> class with the specified tag.</summary>
	/// <param name="tag">The name of the element for which this instance of the class is created. </param>
	public HtmlGenericControl(string tag)
	{
		if (tag == null)
		{
			tag = "";
		}
		_tagName = tag;
	}
}
