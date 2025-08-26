using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.Compilation;

internal class AppWebReferencesCompiler
{
	private const string ResourcesDirName = "App_WebReferences";

	public void Compile()
	{
		string path = Path.Combine(HttpRuntime.AppDomainAppPath, "App_WebReferences");
		if (!Directory.Exists(path))
		{
			return;
		}
		string[] files = Directory.GetFiles(path, "*.wsdl", SearchOption.AllDirectories);
		if (files == null || files.Length == 0)
		{
			return;
		}
		if (!(WebConfigurationManager.GetWebApplicationSection("system.web/compilation") is CompilationSection compilationSection))
		{
			throw new HttpException("Unable to determine default compilation language.");
		}
		CompilerType defaultCompilerTypeForLanguage = BuildManager.GetDefaultCompilerTypeForLanguage(compilationSection.DefaultLanguage, compilationSection);
		CodeDomProvider codeDomProvider = null;
		Exception innerException = null;
		try
		{
			codeDomProvider = Activator.CreateInstance(defaultCompilerTypeForLanguage.CodeDomProviderType) as CodeDomProvider;
		}
		catch (Exception ex)
		{
			innerException = ex;
		}
		if (codeDomProvider == null)
		{
			throw new HttpException("Unable to instantiate default compilation language provider.", innerException);
		}
		AssemblyBuilder assemblyBuilder = new AssemblyBuilder(codeDomProvider, "App_WebReferences_");
		assemblyBuilder.CompilerOptions = defaultCompilerTypeForLanguage.CompilerParameters;
		string[] array = files;
		for (int i = 0; i < array.Length; i++)
		{
			VirtualPath virtualPath = VirtualPath.PhysicalToVirtual(array[i]);
			if (virtualPath != null)
			{
				WsdlBuildProvider wsdlBuildProvider = new WsdlBuildProvider();
				wsdlBuildProvider.SetVirtualPath(virtualPath);
				wsdlBuildProvider.GenerateCode(assemblyBuilder);
			}
		}
		CompilerResults compilerResults;
		try
		{
			compilerResults = assemblyBuilder.BuildAssembly();
		}
		catch (CompilationException innerException2)
		{
			throw new HttpException("Failed to compile web references.", innerException2);
		}
		if (compilerResults != null)
		{
			Assembly compiledAssembly = compilerResults.CompiledAssembly;
			BuildManager.TopLevelAssemblies.Add(compiledAssembly);
		}
	}
}
