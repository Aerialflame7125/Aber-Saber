using System.Web.Compilation;

namespace System.Web.UI.Design;

/// <summary>Used by control localization to read and write resources at design time.</summary>
public abstract class DesignTimeResourceProviderFactory
{
	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> class.</summary>
	protected DesignTimeResourceProviderFactory()
	{
	}

	/// <summary>When overridden in a derived class, creates a global resource provider using the provided <see cref="T:System.IServiceProvider" /> interface and resource class name.</summary>
	/// <param name="serviceProvider">A reference to the design host.</param>
	/// <param name="classKey">The name of the resource class.</param>
	/// <returns>Either an <see cref="T:System.Web.Compilation.IResourceProvider" /> or an <see cref="T:System.Web.UI.Design.IDesignTimeResourceWriter" />.</returns>
	public abstract IResourceProvider CreateDesignTimeGlobalResourceProvider(IServiceProvider serviceProvider, string classKey);

	/// <summary>When overridden in a derived class, creates a local resource provider using the provided reference to the design host.</summary>
	/// <param name="serviceProvider">A reference to the design host.</param>
	/// <returns>An <see cref="T:System.Web.Compilation.IResourceProvider" /> or a class derived from <see cref="T:System.Web.Compilation.IResourceProvider" />.</returns>
	public abstract IResourceProvider CreateDesignTimeLocalResourceProvider(IServiceProvider serviceProvider);

	/// <summary>When overridden in a derived class, creates a local resource writer for using the provided reference to the design host.</summary>
	/// <param name="serviceProvider">A reference to the design host.</param>
	/// <returns>A local resource writer for using the provided reference to the design host.</returns>
	public abstract IDesignTimeResourceWriter CreateDesignTimeLocalResourceWriter(IServiceProvider serviceProvider);
}
