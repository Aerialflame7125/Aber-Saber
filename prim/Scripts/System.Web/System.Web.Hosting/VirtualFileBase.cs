namespace System.Web.Hosting;

/// <summary>Provides the core implementation for the <see cref="T:System.Web.Hosting.VirtualFile" /> and <see cref="T:System.Web.Hosting.VirtualDirectory" /> objects. An abstract class, it cannot be instantiated.</summary>
public abstract class VirtualFileBase : MarshalByRefObject
{
	private string vpath;

	/// <summary>When overridden in a derived class, gets a value indicating whether the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance represents a virtual file or a virtual directory.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual directory; otherwise, <see langword="false" /> if the <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance is a virtual file.</returns>
	public abstract bool IsDirectory { get; }

	/// <summary>Gets the display name of the virtual resource.</summary>
	/// <returns>The display name of the virtual file.</returns>
	public virtual string Name => VirtualPathUtility.GetFileName(vpath);

	/// <summary>Gets the virtual file path.</summary>
	/// <returns>The path to the virtual file. </returns>
	public string VirtualPath => vpath;

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can be called only by an inherited class.</summary>
	protected VirtualFileBase()
	{
	}

	internal void SetVirtualPath(string vpath)
	{
		this.vpath = vpath;
	}

	/// <summary>Gives a <see cref="T:System.Web.Hosting.VirtualFileBase" /> instance an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>Always <see langword="null" />.</returns>
	public override object InitializeLifetimeService()
	{
		return null;
	}
}
