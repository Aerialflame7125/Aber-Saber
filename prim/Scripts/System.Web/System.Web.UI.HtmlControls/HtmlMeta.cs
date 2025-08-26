using System.ComponentModel;
using System.Web.Configuration;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;meta&gt;" /> tag on the server. </summary>
[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
public class HtmlMeta : HtmlControl
{
	/// <summary>Gets or sets the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
	/// <returns>The metadata property value. </returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Content
	{
		get
		{
			string text = base.Attributes["content"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("content");
			}
			else
			{
				base.Attributes["content"] = value;
			}
		}
	}

	/// <summary>Gets or sets an <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control property that is included in the HTTP response header.</summary>
	/// <returns>The name of the HTTP response header item. </returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string HttpEquiv
	{
		get
		{
			string text = base.Attributes["http-equiv"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("http-equiv");
			}
			else
			{
				base.Attributes["http-equiv"] = value;
			}
		}
	}

	/// <summary>Gets or sets the metadata property name defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control. </summary>
	/// <returns>The property name. </returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Name
	{
		get
		{
			string text = base.Attributes["name"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("name");
			}
			else
			{
				base.Attributes["name"] = value;
			}
		}
	}

	/// <summary>Gets or sets a <see langword="scheme" /> attribute used to interpret the metadata property value defined by the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control.</summary>
	/// <returns>The <see langword="scheme" /> attribute. </returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string Scheme
	{
		get
		{
			string text = base.Attributes["scheme"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("scheme");
			}
			else
			{
				base.Attributes["scheme"] = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> class. </summary>
	public HtmlMeta()
		: base("meta")
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the <see cref="T:System.Web.UI.HtmlControls.HtmlMeta" /> control's content on the client's browser.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (WebConfigurationManager.GetSection("system.web/xhtmlConformance") is XhtmlConformanceSection { Mode: XhtmlConformanceMode.Legacy })
		{
			base.Render(writer);
			return;
		}
		writer.WriteBeginTag(TagName);
		RenderAttributes(writer);
		writer.Write("/>");
	}
}
