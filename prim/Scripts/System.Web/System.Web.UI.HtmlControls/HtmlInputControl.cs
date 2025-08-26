using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Serves as the abstract base class that defines the methods, properties, and events common to all HTML input controls, such as the <see langword="&lt;input type=text&gt;" />, <see langword="&lt;input type=submit&gt;" />, and <see langword="&lt;input type= file&gt;" /> elements.</summary>
[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HtmlInputControl : HtmlControl
{
	/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
	/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string Name
	{
		get
		{
			return UniqueID;
		}
		set
		{
		}
	}

	/// <summary>Gets the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</summary>
	/// <returns>A string that contains the type of an <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public string Type => base.Attributes["type"];

	/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control.</summary>
	/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Value
	{
		get
		{
			string text = base.Attributes["value"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("value");
			}
			else
			{
				base.Attributes["value"] = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> class.</summary>
	/// <param name="type">The type of input control. </param>
	protected HtmlInputControl(string type)
		: base("input")
	{
		if (type == null)
		{
			type = string.Empty;
		}
		base.Attributes["type"] = type;
	}

	/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render to the client.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		if (base.Attributes["name"] == null)
		{
			writer.WriteAttribute("name", Name);
		}
		base.RenderAttributes(writer);
		writer.Write(" /");
	}
}
