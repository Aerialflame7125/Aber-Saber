using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Provides programmatic access to the HTML <see langword="head" /> element in server code.</summary>
[ControlBuilder(typeof(HtmlHeadBuilder))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HtmlHead : HtmlGenericControl, IParserAccessor
{
	private string descriptionText;

	private string keywordsText;

	private HtmlMeta descriptionMeta;

	private HtmlMeta keywordsMeta;

	private string titleText;

	private HtmlTitle title;

	private StyleSheetBag styleSheet;

	/// <summary>Gets the content of the "description" <see langword="meta" /> element.</summary>
	/// <returns>The content of the "description" <see langword="meta" /> element.</returns>
	public string Description
	{
		get
		{
			if (descriptionMeta != null)
			{
				return descriptionMeta.Content;
			}
			return descriptionText;
		}
		set
		{
			if (descriptionMeta != null)
			{
				descriptionMeta.Content = value;
			}
			else
			{
				descriptionText = value;
			}
		}
	}

	/// <summary>Gets the content of the "keywords" <see langword="meta" /> element.</summary>
	/// <returns>The content of the "keywords" <see langword="meta" /> element.</returns>
	public string Keywords
	{
		get
		{
			if (keywordsMeta != null)
			{
				return keywordsMeta.Content;
			}
			return keywordsText;
		}
		set
		{
			if (keywordsMeta != null)
			{
				keywordsMeta.Content = value;
			}
			else
			{
				keywordsText = value;
			}
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.UI.IStyleSheet" /> instance that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</summary>
	/// <returns>An object that represents the style rules in the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> control.</returns>
	public IStyleSheet StyleSheet
	{
		get
		{
			if (styleSheet == null)
			{
				styleSheet = new StyleSheetBag();
			}
			return styleSheet;
		}
	}

	/// <summary>Gets the page title.</summary>
	/// <returns>The page title.</returns>
	public string Title
	{
		get
		{
			if (title != null)
			{
				return title.Text;
			}
			return titleText;
		}
		set
		{
			if (title != null)
			{
				title.Text = value;
			}
			else
			{
				titleText = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class.</summary>
	public HtmlHead()
		: base("head")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlHead" /> class by using the specified tag.</summary>
	/// <param name="tag">A string that specifies the tag name of the control.</param>
	public HtmlHead(string tag)
		: base(tag)
	{
	}

	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Page obj = Page ?? throw new HttpException("The <head runat=\"server\"> control requires a page.");
		if (obj.Header != null)
		{
			throw new HttpException("You can only have one <head runat=\"server\"> control on a page.");
		}
		obj.SetHeader(this);
	}

	protected internal override void RenderChildren(HtmlTextWriter writer)
	{
		base.RenderChildren(writer);
		if (title == null)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Title);
			if (!string.IsNullOrEmpty(titleText))
			{
				writer.Write(titleText);
			}
			writer.RenderEndTag();
		}
		if (descriptionMeta == null && descriptionText != null)
		{
			writer.AddAttribute("name", "description");
			writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(descriptionText));
			writer.RenderBeginTag(HtmlTextWriterTag.Meta);
			writer.RenderEndTag();
		}
		if (keywordsMeta == null && keywordsText != null)
		{
			writer.AddAttribute("name", "keywords");
			writer.AddAttribute("content", HttpUtility.HtmlAttributeEncode(keywordsText));
			writer.RenderBeginTag(HtmlTextWriterTag.Meta);
			writer.RenderEndTag();
		}
		if (styleSheet != null)
		{
			styleSheet.Render(writer);
		}
	}

	protected internal override void AddedControl(Control control, int index)
	{
		if (control is HtmlTitle htmlTitle)
		{
			if (title != null)
			{
				throw new HttpException("You can only have one <title> element within the <head> element.");
			}
			title = htmlTitle;
		}
		if (control is HtmlMeta htmlMeta)
		{
			if (string.Compare("keywords", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				keywordsMeta = htmlMeta;
			}
			else if (string.Compare("description", htmlMeta.Name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				descriptionMeta = htmlMeta;
			}
		}
		base.AddedControl(control, index);
	}

	protected internal override void RemovedControl(Control control)
	{
		if (title == control)
		{
			title = null;
		}
		if (keywordsMeta == control)
		{
			keywordsMeta = null;
		}
		else if (descriptionMeta == control)
		{
			descriptionMeta = null;
		}
		base.RemovedControl(control);
	}
}
