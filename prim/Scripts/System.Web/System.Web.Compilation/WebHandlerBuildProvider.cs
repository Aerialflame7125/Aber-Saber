using System.IO;
using System.Web.UI;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
internal sealed class WebHandlerBuildProvider : SimpleBuildProvider
{
	protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
	{
		return new WebHandlerParser(context, virtualPath, physicalPath, reader);
	}

	protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
	{
		return new WebHandlerParser(context, virtualPath, physicalPath, OpenReader(virtualPath.Original));
	}
}
