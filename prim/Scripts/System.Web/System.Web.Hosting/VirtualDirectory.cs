using System.Collections;

namespace System.Web.Hosting;

/// <summary>Represents a directory object in a virtual file or resource space.</summary>
public abstract class VirtualDirectory : VirtualFileBase
{
	/// <summary>Gets a list of the files and subdirectories contained in this virtual directory.</summary>
	/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.</returns>
	public abstract IEnumerable Children { get; }

	/// <summary>Gets a list of all the subdirectories contained in this directory.</summary>
	/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects.</returns>
	public abstract IEnumerable Directories { get; }

	/// <summary>Gets a list of all files contained in this directory.</summary>
	/// <returns>An object implementing the <see cref="T:System.Collections.IEnumerable" /> interface containing <see cref="T:System.Web.Hosting.VirtualFile" /> objects.</returns>
	public abstract IEnumerable Files { get; }

	/// <summary>Gets a value that indicates that this is a virtual resource that should be treated as a directory.</summary>
	/// <returns>Always <see langword="true" />.</returns>
	public override bool IsDirectory => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.VirtualDirectory" /> class. </summary>
	/// <param name="virtualPath">The virtual path to the resource represented by this instance.</param>
	protected VirtualDirectory(string virtualPath)
	{
		SetVirtualPath(virtualPath);
	}
}
