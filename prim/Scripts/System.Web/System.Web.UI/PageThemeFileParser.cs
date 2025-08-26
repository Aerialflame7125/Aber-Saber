using System.Collections;

namespace System.Web.UI;

internal sealed class PageThemeFileParser : UserControlParser
{
	internal override string DefaultBaseTypeName => "System.Web.UI.PageTheme";

	internal PageThemeFileParser(VirtualPath virtualPath, string inputFile, HttpContext context)
		: base(virtualPath, inputFile, context, "System.Web.UI.PageTheme")
	{
	}

	internal override void HandleOptions(object obj)
	{
	}

	internal override void AddDirective(string directive, IDictionary atts)
	{
		if (string.Compare("Register", directive, StringComparison.OrdinalIgnoreCase) == 0)
		{
			base.AddDirective(directive, atts);
		}
		else
		{
			ThrowParseException("Unknown directive: " + directive);
		}
	}
}
