namespace System.Web.Compilation;

/// <summary>Serves as the base class for classes that create resource providers.</summary>
public abstract class ResourceProviderFactory
{
	/// <summary>When implemented in a derived class, initializes a new instance of the derived class. </summary>
	protected ResourceProviderFactory()
	{
	}

	/// <summary>When overridden in a derived class, creates a global resource provider. </summary>
	/// <param name="classKey">The name of the resource class.</param>
	/// <returns>A global resource provider.</returns>
	public abstract IResourceProvider CreateGlobalResourceProvider(string classKey);

	/// <summary>When overridden in a derived class, creates a local resource provider. </summary>
	/// <param name="virtualPath">The path to a resource file.</param>
	/// <returns>A local resource provider.</returns>
	public abstract IResourceProvider CreateLocalResourceProvider(string virtualPath);
}
