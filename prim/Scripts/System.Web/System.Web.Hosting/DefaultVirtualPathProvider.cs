using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting;

internal sealed class DefaultVirtualPathProvider : VirtualPathProvider
{
	internal DefaultVirtualPathProvider()
	{
	}

	protected override void Initialize()
	{
	}

	public override bool DirectoryExists(string virtualDir)
	{
		if (string.IsNullOrEmpty(virtualDir))
		{
			throw new ArgumentNullException("virtualDir");
		}
		return Directory.Exists(HostingEnvironment.MapPath(virtualDir));
	}

	public override bool FileExists(string virtualPath)
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		return File.Exists(HostingEnvironment.MapPath(virtualPath));
	}

	public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
	{
		return null;
	}

	public override string GetCacheKey(string virtualPath)
	{
		return null;
	}

	public override VirtualDirectory GetDirectory(string virtualDir)
	{
		if (string.IsNullOrEmpty(virtualDir))
		{
			throw new ArgumentNullException("virtualDir");
		}
		return new DefaultVirtualDirectory(virtualDir);
	}

	public override VirtualFile GetFile(string virtualPath)
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		return new DefaultVirtualFile(virtualPath);
	}

	public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
	{
		if (virtualPath == null || virtualPathDependencies == null)
		{
			throw new NullReferenceException();
		}
		return virtualPath;
	}
}
