namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.All)]
internal sealed class IgnoreFileBuildProvider : BuildProvider
{
	public override void GenerateCode(AssemblyBuilder assemblyBuilder)
	{
	}
}
