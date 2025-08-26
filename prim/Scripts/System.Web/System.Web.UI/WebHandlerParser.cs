using System.IO;
using System.Web.Compilation;

namespace System.Web.UI;

internal class WebHandlerParser : SimpleWebHandlerParser
{
	protected override string DefaultDirectiveName => "webhandler";

	private WebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
		: base(context, virtualPath, physicalPath)
	{
	}

	internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
		: this(context, virtualPath, null, reader)
	{
	}

	internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
		: base(context, virtualPath.Original, physicalPath, reader)
	{
	}

	public static Type GetCompiledType(HttpContext context, string virtualPath, string physicalPath)
	{
		WebHandlerParser webHandlerParser = new WebHandlerParser(context, virtualPath, physicalPath);
		Type compiledTypeFromCache = webHandlerParser.GetCompiledTypeFromCache();
		if (compiledTypeFromCache != null)
		{
			return compiledTypeFromCache;
		}
		return WebServiceCompiler.CompileIntoType(webHandlerParser);
	}
}
