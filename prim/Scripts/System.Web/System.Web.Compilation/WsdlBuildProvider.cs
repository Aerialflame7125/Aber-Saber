using System.CodeDom;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Discovery;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
internal sealed class WsdlBuildProvider : BuildProvider
{
	private CompilerType _compilerType;

	public override CompilerType CodeCompilerType
	{
		get
		{
			if (_compilerType == null)
			{
				if (!(WebConfigurationManager.GetWebApplicationSection("system.web/compilation") is CompilationSection compilationSection))
				{
					throw new HttpException("Unable to determine default compilation language.");
				}
				_compilerType = BuildManager.GetDefaultCompilerTypeForLanguage(compilationSection.DefaultLanguage, compilationSection);
			}
			return _compilerType;
		}
	}

	public override void GenerateCode(AssemblyBuilder assemblyBuilder)
	{
		CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
		CodeNamespace codeNamespace = new CodeNamespace();
		codeCompileUnit.Namespaces.Add(codeNamespace);
		ServiceDescription value = ServiceDescription.Read(OpenReader());
		DiscoveryClientDocumentCollection documents = new DiscoveryClientDocumentCollection { { base.VirtualPath, value } };
		ServiceDescriptionImporter.GenerateWebReferences(new WebReferenceCollection
		{
			new WebReference(documents, codeNamespace)
		}, options: new WebReferenceOptions
		{
			Style = ServiceDescriptionImportStyle.Client
		}, codeProvider: assemblyBuilder.CodeDomProvider, codeCompileUnit: codeCompileUnit);
		assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
	}
}
