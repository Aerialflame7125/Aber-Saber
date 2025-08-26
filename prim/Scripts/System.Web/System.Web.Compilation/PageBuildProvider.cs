using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
internal sealed class PageBuildProvider : TemplateBuildProvider
{
	protected override string MapPath(VirtualPath virtualPath)
	{
		if (virtualPath.IsFake)
		{
			return virtualPath.PhysicalPath;
		}
		return base.MapPath(virtualPath);
	}

	protected override TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
	{
		if (virtualPath.IsFake)
		{
			physicalPath = virtualPath.PhysicalPath;
			return new StreamReader(physicalPath);
		}
		physicalPath = null;
		return base.SpecialOpenReader(virtualPath, out physicalPath);
	}

	protected override BaseCompiler CreateCompiler(TemplateParser parser)
	{
		return new PageCompiler(parser as PageParser);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
	{
		return CreateParser(virtualPath, physicalPath, OpenReader(virtualPath.Original), context);
	}

	protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
	{
		return new PageParser(virtualPath, physicalPath, reader, context);
	}
}
