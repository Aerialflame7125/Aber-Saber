using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays an image on a Web page.</summary>
[DefaultProperty("ImageUrl")]
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Image : WebControl
{
	/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip.</summary>
	/// <returns>The alternate text displayed in the <see cref="T:System.Web.UI.WebControls.Image" /> control when the image is unavailable.</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string AlternateText
	{
		get
		{
			string text = (string)ViewState["AlternateText"];
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
				ViewState.Remove("AlternateText");
			}
			else
			{
				ViewState["AlternateText"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is enabled; otherwise <see langword="false" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	/// <summary>Gets the font properties for the text associated with the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.FontInfo" /> that contains the properties for the text associated with the control.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override FontInfo Font => base.Font;

	/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.WebControls.Image" /> control in relation to other elements on the Web page.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. The default is <see langword="NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.ImageAlign" /> values. </exception>
	[DefaultValue(ImageAlign.NotSet)]
	[WebSysDescription("")]
	[WebCategory("Layout")]
	public virtual ImageAlign ImageAlign
	{
		get
		{
			object obj = ViewState["ImageAlign"];
			if (obj != null)
			{
				return (ImageAlign)obj;
			}
			return ImageAlign.NotSet;
		}
		set
		{
			if (value < ImageAlign.NotSet || value > ImageAlign.TextTop)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Invalid ImageAlign value."));
			}
			ViewState["ImageAlign"] = value;
		}
	}

	/// <summary>Gets or sets the URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</summary>
	/// <returns>The URL that provides the path to an image to display in the <see cref="T:System.Web.UI.WebControls.Image" /> control.</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string ImageUrl
	{
		get
		{
			string text = (string)ViewState["ImageUrl"];
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
				ViewState.Remove("ImageUrl");
			}
			else
			{
				ViewState["ImageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the location to a detailed description for the image.</summary>
	/// <returns>The URL for the file that contains a detailed description for the image. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual string DescriptionUrl
	{
		get
		{
			string text = (string)ViewState["DescriptionUrl"];
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
				ViewState.Remove("DescriptionUrl");
			}
			else
			{
				ViewState["DescriptionUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the control generates an alternate text attribute for an empty string value.</summary>
	/// <returns>
	///     <see langword="true" /> if the control generates the alternate text attribute for an empty string value; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Accessibility")]
	public virtual bool GenerateEmptyAlternateText
	{
		get
		{
			object obj = ViewState["GenerateEmptyAlternateText"];
			if (obj != null)
			{
				return (bool)obj;
			}
			return false;
		}
		set
		{
			ViewState["GenerateEmptyAlternateText"] = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Image" /> class.</summary>
	public Image()
		: base(HtmlTextWriterTag.Img)
	{
	}

	/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.Image" /> to the output stream for rendering on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client browser. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		writer.AddAttribute(HtmlTextWriterAttribute.Src, ResolveClientUrl(ImageUrl));
		string alternateText = AlternateText;
		if (alternateText.Length > 0 || GenerateEmptyAlternateText)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, alternateText);
		}
		alternateText = DescriptionUrl;
		if (alternateText.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Longdesc, ResolveClientUrl(alternateText));
		}
		switch (ImageAlign)
		{
		case ImageAlign.Left:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "left", fEncode: false);
			break;
		case ImageAlign.Right:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "right", fEncode: false);
			break;
		case ImageAlign.Baseline:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "baseline", fEncode: false);
			break;
		case ImageAlign.Top:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "top", fEncode: false);
			break;
		case ImageAlign.Middle:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "middle", fEncode: false);
			break;
		case ImageAlign.Bottom:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "bottom", fEncode: false);
			break;
		case ImageAlign.AbsBottom:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "absbottom", fEncode: false);
			break;
		case ImageAlign.AbsMiddle:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "absmiddle", fEncode: false);
			break;
		case ImageAlign.TextTop:
			writer.AddAttribute(HtmlTextWriterAttribute.Align, "texttop", fEncode: false);
			break;
		}
	}

	/// <summary>Renders the image control contents to the specified writer.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		base.RenderContents(writer);
	}
}
