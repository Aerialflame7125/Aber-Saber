using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;title&gt;" /> element on the server.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTitle : HtmlControl
{
	private string text;

	/// <summary>Gets or sets the text of the HTML <see langword="&lt;title&gt;" /> element.</summary>
	/// <returns>The text of the HTML <see langword="&lt;title&gt;" /> element. The default value is an empty string ("").</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[Localizable(true)]
	public virtual string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> collection of the control.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	protected override void AddParsedSubObject(object obj)
	{
		if (obj is LiteralControl literalControl)
		{
			text = literalControl.Text;
		}
		else
		{
			base.AddParsedSubObject(obj);
		}
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new ControlCollection(this);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		writer.RenderBeginTag(HtmlTextWriterTag.Title);
		if (HasControls() || HasRenderMethodDelegate())
		{
			RenderChildren(writer);
		}
		else
		{
			writer.Write(text);
		}
		writer.RenderEndTag();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTitle" /> class.</summary>
	public HtmlTitle()
	{
	}
}
