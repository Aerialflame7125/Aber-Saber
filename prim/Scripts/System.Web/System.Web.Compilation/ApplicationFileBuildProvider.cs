using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
internal class ApplicationFileBuildProvider : TemplateBuildProvider
{
	protected override BaseCompiler CreateCompiler(TemplateParser parser)
	{
		return new GlobalAsaxCompiler(parser as ApplicationFileParser);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
	{
		return CreateParser(virtualPath, physicalPath, OpenReader(virtualPath.Original), context);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
	{
		return new ApplicationFileParser(virtualPath, physicalPath, reader, context);
	}
}
