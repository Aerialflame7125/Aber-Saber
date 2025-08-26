namespace System.Web.UI;

internal class PageThemeBuilder : UserControlControlBuilder
{
	public override void AppendLiteralString(string s)
	{
		throw new HttpException($"Literal content ('{s}') not allowed within a skin file");
	}
}
