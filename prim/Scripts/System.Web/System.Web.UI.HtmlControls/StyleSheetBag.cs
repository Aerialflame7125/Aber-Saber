using System.Collections;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls;

internal class StyleSheetBag : IStyleSheet
{
	internal class StyleEntry
	{
		public Style Style;

		public string Selection;

		public IUrlResolutionService UrlResolver;
	}

	private ArrayList entries = new ArrayList();

	public void CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selection)
	{
		StyleEntry styleEntry = new StyleEntry();
		styleEntry.Style = style;
		styleEntry.UrlResolver = urlResolver;
		styleEntry.Selection = selection;
		entries.Add(styleEntry);
	}

	public void RegisterStyle(Style style, IUrlResolutionService urlResolver)
	{
		for (int i = 0; i < entries.Count; i++)
		{
			if (((StyleEntry)entries[i]).Style == style)
			{
				return;
			}
		}
		string text = "aspnet_" + entries.Count;
		style.SetRegisteredCssClass(text);
		CreateStyleRule(style, urlResolver, "." + text);
	}

	public void Render(HtmlTextWriter writer)
	{
		writer.AddAttribute("type", "text/css", fEndode: false);
		writer.RenderBeginTag(HtmlTextWriterTag.Style);
		foreach (StyleEntry entry in entries)
		{
			CssStyleCollection styleAttributes = entry.Style.GetStyleAttributes(entry.UrlResolver);
			writer.Write("\n" + entry.Selection + " {" + styleAttributes.Value + "}");
		}
		writer.RenderEndTag();
	}
}
