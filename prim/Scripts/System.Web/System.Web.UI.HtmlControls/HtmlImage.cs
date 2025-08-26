using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Provides programmatic access for the HTML <see langword="&lt;img&gt;" /> element on the server.</summary>
[ControlBuilder(typeof(HtmlEmptyTagControlBuilder))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlImage : HtmlControl
{
	/// <summary>Gets or sets the alignment of the image relative to other Web page elements.</summary>
	/// <returns>A string that specifies the alignment of the image relative to other Web page elements.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public string Align
	{
		get
		{
			string text = base.Attributes["align"];
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
				base.Attributes.Remove("align");
			}
			else
			{
				base.Attributes["align"] = value;
			}
		}
	}

	/// <summary>Gets or sets the alternative caption the browser displays if an image is unavailable or currently downloading and not yet finished.</summary>
	/// <returns>A string that contains the alternative caption for the browser to use when the image is unavailable.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public string Alt
	{
		get
		{
			string text = base.Attributes["alt"];
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
				base.Attributes.Remove("alt");
			}
			else
			{
				base.Attributes["alt"] = value;
			}
		}
	}

	/// <summary>Gets or sets the width of a frame for an image.</summary>
	/// <returns>The width (in pixels) of a frame for an image.</returns>
	[DefaultValue(0)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Border
	{
		get
		{
			string text = base.Attributes["border"];
			if (text == null)
			{
				return -1;
			}
			return int.Parse(text, Helpers.InvariantCulture);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("border");
			}
			else
			{
				base.Attributes["border"] = value.ToString();
			}
		}
	}

	/// <summary>Gets or sets the height of the image.</summary>
	/// <returns>The height of the image.</returns>
	[DefaultValue(100)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Height
	{
		get
		{
			string text = base.Attributes["height"];
			if (text == null)
			{
				return -1;
			}
			return int.Parse(text, Helpers.InvariantCulture);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("height");
			}
			else
			{
				base.Attributes["height"] = value.ToString();
			}
		}
	}

	/// <summary>Gets or sets the source of the image file to display.</summary>
	/// <returns>A string that contains the path to an image file to display.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[UrlProperty]
	public string Src
	{
		get
		{
			string text = base.Attributes["src"];
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
				base.Attributes.Remove("src");
			}
			else
			{
				base.Attributes["src"] = value;
			}
		}
	}

	/// <summary>Gets or sets the width of the image.</summary>
	/// <returns>The width of the image.</returns>
	[DefaultValue(100)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int Width
	{
		get
		{
			string text = base.Attributes["width"];
			if (text == null)
			{
				return -1;
			}
			return int.Parse(text, Helpers.InvariantCulture);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("width");
			}
			else
			{
				base.Attributes["width"] = value.ToString();
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> class.</summary>
	public HtmlImage()
		: base("img")
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlImage.Src" /> property contains a malformed URL.</exception>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		PreProcessRelativeReference(writer, "src");
		string text = base.Attributes["src"];
		if (text == null || text.Length == 0)
		{
			base.Attributes.Remove("src");
		}
		base.RenderAttributes(writer);
		writer.Write(" /");
	}
}
