using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
internal sealed class MasterPageBuildProvider : TemplateBuildProvider
{
	protected override BaseCompiler CreateCompiler(TemplateParser parser)
	{
		return new MasterPageCompiler(parser as MasterPageParser);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
	{
		return CreateParser(virtualPath, physicalPath, OpenReader(virtualPath.Original), context);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
	{
		return new MasterPageParser(virtualPath, physicalPath, reader, context);
	}
}
