namespace System.Web.UI;

internal sealed class PageThemeParser : UserControlParser
{
	private string[] linkedStyleSheets;

	public string[] LinkedStyleSheets
	{
		get
		{
			return linkedStyleSheets;
		}
		set
		{
			linkedStyleSheets = value;
		}
	}

	internal override string DefaultBaseTypeName => "System.Web.UI.PageTheme";

	internal PageThemeParser(VirtualPath virtualPath, HttpContext context)
		: base(virtualPath, virtualPath.PhysicalPath, context, "System.Web.UI.PageTheme")
	{
		AddDependency(virtualPath.Original);
	}

	internal override void HandleOptions(object obj)
	{
	}
}
