using System.Collections.Generic;

namespace System.Web.UI.WebControls;

internal sealed class StyleBlock : Control
{
	private List<NamedCssStyleCollection> cssStyles;

	private Dictionary<string, NamedCssStyleCollection> cssStyleIndex;

	private string stylePrefix;

	private List<NamedCssStyleCollection> CssStyles
	{
		get
		{
			if (cssStyles == null)
			{
				cssStyles = new List<NamedCssStyleCollection>();
				cssStyleIndex = new Dictionary<string, NamedCssStyleCollection>(StringComparer.Ordinal);
			}
			return cssStyles;
		}
	}

	public StyleBlock(string stylePrefix)
	{
		if (string.IsNullOrEmpty(stylePrefix))
		{
			throw new ArgumentNullException("stylePrefix");
		}
		this.stylePrefix = stylePrefix;
	}

	public NamedCssStyleCollection RegisterStyle(string name = null)
	{
		if (name == null)
		{
			name = string.Empty;
		}
		return GetStyle(name);
	}

	public NamedCssStyleCollection RegisterStyle(Style style, string name = null)
	{
		if (style == null)
		{
			throw new ArgumentNullException("style");
		}
		if (name == null)
		{
			name = string.Empty;
		}
		NamedCssStyleCollection style2 = GetStyle(name);
		style2.CopyFrom(style.GetStyleAttributes(null));
		return style2;
	}

	public NamedCssStyleCollection RegisterStyle(HtmlTextWriterStyle key, string value, string styleName = null)
	{
		if (styleName == null)
		{
			styleName = string.Empty;
		}
		NamedCssStyleCollection style = GetStyle(styleName);
		style.Add(key, value);
		return style;
	}

	private NamedCssStyleCollection GetStyle(string name)
	{
		List<NamedCssStyleCollection> list = CssStyles;
		if (!cssStyleIndex.TryGetValue(name, out var value))
		{
			value = new NamedCssStyleCollection(name);
			cssStyleIndex.Add(name, value);
			list.Add(value);
		}
		if (value == null)
		{
			throw new InvalidOperationException($"Internal error. Stylesheet for style {name} is null.");
		}
		return value;
	}

	protected internal override void Render(HtmlTextWriter writer)
	{
		if (cssStyles == null || cssStyles.Count == 0)
		{
			return;
		}
		writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
		writer.RenderBeginTag(HtmlTextWriterTag.Style);
		writer.WriteLine("/* <![CDATA[ */");
		foreach (NamedCssStyleCollection cssStyle in cssStyles)
		{
			string value = cssStyle.Collection.Value;
			if (!string.IsNullOrEmpty(value))
			{
				string text = cssStyle.Name;
				if (text != string.Empty)
				{
					text += " ";
				}
				writer.WriteLine("#{0} {1}{{ {2} }}", stylePrefix, text, value);
			}
		}
		writer.WriteLine("/* ]]> */");
		writer.RenderEndTag();
	}
}
