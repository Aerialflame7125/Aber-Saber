using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
internal sealed class UserControlBuildProvider : TemplateBuildProvider
{
	protected override BaseCompiler CreateCompiler(TemplateParser parser)
	{
		return new UserControlCompiler(parser as UserControlParser);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
	{
		return CreateParser(virtualPath, physicalPath, OpenReader(virtualPath.Original), context);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
	{
		return new UserControlParser(virtualPath, physicalPath, reader, context);
	}
}
