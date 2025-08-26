using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data.Design;
using System.IO;

namespace System.Web.Compilation;

[BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
internal sealed class XsdBuildProvider : BuildProvider
{
	public override void GenerateCode(AssemblyBuilder assemblyBuilder)
	{
		CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
		CodeNamespace codeNamespace = new CodeNamespace(null);
		codeCompileUnit.Namespaces.Add(codeNamespace);
		StreamReader streamReader = new StreamReader(HttpContext.Current.Request.MapPath(base.VirtualPath));
		CodeDomProvider codeDomProvider = assemblyBuilder.CodeDomProvider;
		if (codeDomProvider == null)
		{
			throw new HttpException("Assembly builder has no code provider");
		}
		TypedDataSetGenerator.Generate(streamReader.ReadToEnd(), codeCompileUnit, codeNamespace, codeDomProvider);
		assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
	}
}
