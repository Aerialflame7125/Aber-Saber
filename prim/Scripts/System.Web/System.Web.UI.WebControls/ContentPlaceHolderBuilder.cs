using System.Collections;

namespace System.Web.UI.WebControls;

internal class ContentPlaceHolderBuilder : ControlBuilder
{
	public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
	{
		string value = null;
		foreach (object key in attribs.Keys)
		{
			string text = key as string;
			if (!string.IsNullOrEmpty(text) && string.Compare(text, "id", StringComparison.OrdinalIgnoreCase) == 0)
			{
				value = attribs[text] as string;
				break;
			}
		}
		base.Init(parser, parentBuilder, type, tagName, ID, attribs);
		if (parser is MasterPageParser masterPageParser && !string.IsNullOrEmpty(value))
		{
			masterPageParser.AddContentPlaceHolderId(value);
		}
	}
}
