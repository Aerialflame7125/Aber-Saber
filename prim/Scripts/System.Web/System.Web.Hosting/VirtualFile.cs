using System.IO;

namespace System.Web.Hosting;

/// <summary>Represents a file object in a virtual file or resource space.</summary>
public abstract class VirtualFile : VirtualFileBase
{
	/// <summary>Gets a value that indicates that this is a virtual resource that should be treated as a file.</summary>
	/// <returns>Always <see langword="false" />. </returns>
	public override bool IsDirectory => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualFile" /> class. </summary>
	/// <param name="virtualPath">The virtual path to the resource represented by this instance. </param>
	protected VirtualFile(string virtualPath)
	{
		SetVirtualPath(virtualPath);
	}

	/// <summary>When overridden in a derived class, returns a read-only stream to the virtual resource.</summary>
	/// <returns>A read-only stream to the virtual file.</returns>
	public abstract Stream Open();
}
