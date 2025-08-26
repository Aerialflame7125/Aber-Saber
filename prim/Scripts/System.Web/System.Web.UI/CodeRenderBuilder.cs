using System.Web.Compilation;

namespace System.Web.UI;

internal sealed class CodeRenderBuilder : CodeBuilder
{
	public bool HtmlEncode { get; private set; }

	public CodeRenderBuilder(string code, bool isAssign, ILocation location, bool doHtmlEncode)
		: base(code, isAssign, location)
	{
		HtmlEncode = doHtmlEncode;
	}

	public CodeRenderBuilder(string code, bool isAssign, ILocation location)
		: base(code, isAssign, location)
	{
	}
}
