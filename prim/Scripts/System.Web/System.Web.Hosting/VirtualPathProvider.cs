using System.Collections;
using System.IO;
using System.Web.Caching;

namespace System.Web.Hosting;

/// <summary>Provides a set of methods that enable a Web application to retrieve resources from a virtual file system.</summary>
public abstract class VirtualPathProvider : MarshalByRefObject
{
	private VirtualPathProvider prev;

	/// <summary>Gets a reference to a previously registered <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</summary>
	/// <returns>The next <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object in the compilation system.</returns>
	protected internal VirtualPathProvider Previous => prev;

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can be called only by an inherited class.</summary>
	protected VirtualPathProvider()
	{
	}

	/// <summary>Initializes the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance.</summary>
	protected virtual void Initialize()
	{
	}

	internal void InitializeAndSetPrevious(VirtualPathProvider prev)
	{
		this.prev = prev;
		Initialize();
	}

	/// <summary>Combines a base path with a relative path to return a complete path to a virtual resource.</summary>
	/// <param name="basePath">The base path for the application.</param>
	/// <param name="relativePath">The path to the virtual resource, relative to the base path.</param>
	/// <returns>The complete path to a virtual resource.</returns>
	public virtual string CombineVirtualPaths(string basePath, string relativePath)
	{
		return VirtualPathUtility.Combine(basePath, relativePath);
	}

	/// <summary>Gets a value that indicates whether a directory exists in the virtual file system.</summary>
	/// <param name="virtualDir">The path to the virtual directory.</param>
	/// <returns>
	///     <see langword="true" /> if the directory exists in the virtual file system; otherwise, <see langword="false" />.</returns>
	public virtual bool DirectoryExists(string virtualDir)
	{
		if (prev != null)
		{
			return prev.DirectoryExists(virtualDir);
		}
		return false;
	}

	/// <summary>Gets a value that indicates whether a file exists in the virtual file system.</summary>
	/// <param name="virtualPath">The path to the virtual file.</param>
	/// <returns>
	///     <see langword="true" /> if the file exists in the virtual file system; otherwise, <see langword="false" />.</returns>
	public virtual bool FileExists(string virtualPath)
	{
		if (prev != null)
		{
			return prev.FileExists(virtualPath);
		}
		return false;
	}

	/// <summary>Creates a cache dependency based on the specified virtual paths.</summary>
	/// <param name="virtualPath">The path to the primary virtual resource.</param>
	/// <param name="virtualPathDependencies">An array of paths to other resources required by the primary virtual resource.</param>
	/// <param name="utcStart">The UTC time at which the virtual resources were read.</param>
	/// <returns>A <see cref="T:System.Web.Caching.CacheDependency" /> object for the specified virtual resources.</returns>
	public virtual CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
	{
		if (prev != null)
		{
			return prev.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
		}
		return null;
	}

	/// <summary>Returns a cache key to use for the specified virtual path.</summary>
	/// <param name="virtualPath">The path to the virtual resource.</param>
	/// <returns>A cache key for the specified virtual resource.</returns>
	public virtual string GetCacheKey(string virtualPath)
	{
		if (prev != null)
		{
			return prev.GetCacheKey(virtualPath);
		}
		return null;
	}

	/// <summary>Gets a virtual directory from the virtual file system.</summary>
	/// <param name="virtualDir">The path to the virtual directory.</param>
	/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class that represents a directory in the virtual file system.</returns>
	public virtual VirtualDirectory GetDirectory(string virtualDir)
	{
		if (prev != null)
		{
			return prev.GetDirectory(virtualDir);
		}
		return null;
	}

	/// <summary>Gets a virtual file from the virtual file system.</summary>
	/// <param name="virtualPath">The path to the virtual file.</param>
	/// <returns>A descendent of the <see cref="T:System.Web.Hosting.VirtualFile" /> class that represents a file in the virtual file system.</returns>
	public virtual VirtualFile GetFile(string virtualPath)
	{
		if (prev != null)
		{
			return prev.GetFile(virtualPath);
		}
		return null;
	}

	/// <summary>Returns a hash of the specified virtual paths.</summary>
	/// <param name="virtualPath">The path to the primary virtual resource.</param>
	/// <param name="virtualPathDependencies">An array of paths to other virtual resources required by the primary virtual resource.</param>
	/// <returns>A hash of the specified virtual paths.</returns>
	public virtual string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
	{
		if (prev != null)
		{
			return prev.GetFileHash(virtualPath, virtualPathDependencies);
		}
		return null;
	}

	/// <summary>Gives the <see cref="T:System.Web.Hosting.VirtualPathProvider" /> object an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>Always <see langword="null" />.</returns>
	public override object InitializeLifetimeService()
	{
		return null;
	}

	/// <summary>Returns a stream from a virtual file.</summary>
	/// <param name="virtualPath">The path to the virtual file.</param>
	/// <returns>A read-only <see cref="T:System.IO.Stream" /> object for the specified virtual file or resource.</returns>
	public static Stream OpenFile(string virtualPath)
	{
		return HostingEnvironment.VirtualPathProvider.GetFile(virtualPath)?.Open();
	}
}
