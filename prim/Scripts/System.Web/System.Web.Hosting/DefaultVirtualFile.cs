using System.IO;

namespace System.Web.Hosting;

internal class DefaultVirtualFile : VirtualFile
{
	internal DefaultVirtualFile(string virtualPath)
		: base(virtualPath)
	{
	}

	public override Stream Open()
	{
		return File.OpenRead(HostingEnvironment.MapPath(base.VirtualPath));
	}
}
