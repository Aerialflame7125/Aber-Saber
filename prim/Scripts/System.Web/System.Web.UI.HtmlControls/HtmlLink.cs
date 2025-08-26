using System.ComponentModel;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="link" /> element on the server.</summary>
[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
public class HtmlLink : HtmlControl
{
	/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control. </summary>
	/// <returns>The URL target of the link.</returns>
	[DefaultValue("")]
	[UrlProperty]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Href
	{
		get
		{
			string text = base.Attributes["href"];
			if (text == null)
			{
				return "";
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("href");
			}
			else
			{
				base.Attributes["href"] = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> class.</summary>
	public HtmlLink()
		: base("link")
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		writer.WriteBeginTag(TagName);
		RenderAttributes(writer);
		writer.Write(" />");
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlLink" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	[MonoTODO("why override?")]
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		if (Href.Length > 0)
		{
			Href = ResolveClientUrl(Href);
		}
		base.RenderAttributes(writer);
	}
}
