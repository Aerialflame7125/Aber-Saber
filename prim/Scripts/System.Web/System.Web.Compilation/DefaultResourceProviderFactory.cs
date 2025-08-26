namespace System.Web.Compilation;

internal sealed class DefaultResourceProviderFactory : ResourceProviderFactory
{
	public override IResourceProvider CreateGlobalResourceProvider(string classKey)
	{
		return new DefaultResourceProvider(classKey, isGlobal: true);
	}

	public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
	{
		return new DefaultResourceProvider(virtualPath, isGlobal: false);
	}
}
