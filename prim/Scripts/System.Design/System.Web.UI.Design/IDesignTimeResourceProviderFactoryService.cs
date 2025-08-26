namespace System.Web.UI.Design;

/// <summary>Provides an interface for creating a custom <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> class.</summary>
public interface IDesignTimeResourceProviderFactoryService
{
	/// <summary>Creates a <see cref="T:System.Web.UI.Design.DesignTimeResourceProviderFactory" /> object.</summary>
	/// <returns>A design time resource provider factory.</returns>
	DesignTimeResourceProviderFactory GetFactory();
}
