using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AppCodeAssembly
{
	private List<string> files;

	private List<CodeCompileUnit> units;

	private string name;

	private string path;

	private bool validAssembly;

	private string outputAssemblyName;

	public string OutputAssemblyName => outputAssemblyName;

	public bool IsValid => validAssembly;

	public string SourcePath => path;

	public string Name => name;

	public List<string> Files => files;

	public AppCodeAssembly(string name, string path)
	{
		files = new List<string>();
		units = new List<CodeCompileUnit>();
		validAssembly = true;
		this.name = name;
		this.path = path;
	}

	public void AddFile(string path)
	{
		files.Add(path);
	}

	public void AddUnit(CodeCompileUnit unit)
	{
		units.Add(unit);
	}

	private object OnCreateTemporaryAssemblyFile(string path)
	{
		new FileStream(path, FileMode.CreateNew).Close();
		return path;
	}

	public void Build(string[] binAssemblies)
	{
		Type type = null;
		CompilerInfo compilerInfo = null;
		string text = null;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		bool flag = false;
		foreach (string file in files)
		{
			flag = true;
			string text2 = null;
			string extension = Path.GetExtension(file);
			if (string.IsNullOrEmpty(extension) || !CodeDomProvider.IsDefinedExtension(extension))
			{
				flag = false;
			}
			if (flag)
			{
				text2 = CodeDomProvider.GetLanguageFromExtension(extension);
				if (!CodeDomProvider.IsDefinedLanguage(text2))
				{
					flag = false;
				}
			}
			if (!flag || text2 == null)
			{
				list2.Add(file);
				continue;
			}
			CompilerInfo compilerInfo2 = CodeDomProvider.GetCompilerInfo(text2);
			if (compilerInfo2 != null && compilerInfo2.IsCodeDomProviderTypeValid)
			{
				if (type == null)
				{
					text = file;
					type = compilerInfo2.CodeDomProviderType;
					compilerInfo = compilerInfo2;
				}
				else if (type != compilerInfo2.CodeDomProviderType)
				{
					throw new HttpException($"Files {Path.GetFileName(text)} and {Path.GetFileName(file)} are in different languages - they cannot be compiled into the same assembly");
				}
				list.Add(file);
			}
		}
		CompilationSection compilationSection = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
		if (compilerInfo == null)
		{
			if (!CodeDomProvider.IsDefinedLanguage(compilationSection.DefaultLanguage))
			{
				throw new HttpException("Failed to retrieve default source language");
			}
			compilerInfo = CodeDomProvider.GetCompilerInfo(compilationSection.DefaultLanguage);
			if (compilerInfo == null || !compilerInfo.IsCodeDomProviderTypeValid)
			{
				throw new HttpException("Internal error while initializing application");
			}
		}
		AssemblyBuilder assemblyBuilder = new AssemblyBuilder(compilerInfo.CreateProvider() ?? throw new HttpException("A code provider error occurred while initializing application."));
		foreach (string item in list)
		{
			assemblyBuilder.AddCodeFile(item);
		}
		foreach (CodeCompileUnit unit in units)
		{
			assemblyBuilder.AddCodeCompileUnit(unit);
		}
		CompilerParameters compilerParameters = compilerInfo.CreateDefaultCompilerParameters();
		compilerParameters.IncludeDebugInformation = compilationSection.Debug;
		if (binAssemblies != null && binAssemblies.Length != 0)
		{
			StringCollection referencedAssemblies = compilerParameters.ReferencedAssemblies;
			foreach (string value in binAssemblies)
			{
				if (!referencedAssemblies.Contains(value))
				{
					referencedAssemblies.Add(value);
				}
			}
		}
		if (compilationSection != null)
		{
			foreach (AssemblyInfo assembly2 in compilationSection.Assemblies)
			{
				if (assembly2.Assembly != "*")
				{
					try
					{
						compilerParameters.ReferencedAssemblies.Add(AssemblyPathResolver.GetAssemblyPath(assembly2.Assembly));
					}
					catch (Exception innerException)
					{
						throw new HttpException($"Could not find assembly {assembly2.Assembly}.", innerException);
					}
				}
			}
			BuildProviderCollection buildProviders = compilationSection.BuildProviders;
			foreach (string item2 in list2)
			{
				GetBuildProviderFor(item2, buildProviders)?.GenerateCode(assemblyBuilder);
			}
		}
		if (list.Count == 0 && list2.Count == 0 && units.Count == 0)
		{
			return;
		}
		outputAssemblyName = (string)FileUtils.CreateTemporaryFile(AppDomain.CurrentDomain.SetupInformation.DynamicBase, name, "dll", OnCreateTemporaryAssemblyFile);
		compilerParameters.OutputAssembly = outputAssemblyName;
		foreach (Assembly topLevelAssembly in BuildManager.TopLevelAssemblies)
		{
			compilerParameters.ReferencedAssemblies.Add(topLevelAssembly.Location);
		}
		CompilerResults compilerResults = assemblyBuilder.BuildAssembly(compilerParameters);
		if (compilerResults == null)
		{
			return;
		}
		if (compilerResults.NativeCompilerReturnValue == 0)
		{
			BuildManager.CodeAssemblies.Add(compilerResults.CompiledAssembly);
			BuildManager.TopLevelAssemblies.Add(compilerResults.CompiledAssembly);
			HttpRuntime.WritePreservationFile(compilerResults.CompiledAssembly, name);
			return;
		}
		if (HttpContext.Current.IsCustomErrorEnabled)
		{
			throw new HttpException("An error occurred while initializing application.");
		}
		throw new CompilationException(null, compilerResults.Errors, null);
	}

	private VirtualPath PhysicalToVirtual(string file)
	{
		return new VirtualPath(file.Replace(HttpRuntime.AppDomainAppPath, "~/").Replace(Path.DirectorySeparatorChar, '/'));
	}

	private BuildProvider GetBuildProviderFor(string file, BuildProviderCollection buildProviders)
	{
		if (file == null || file.Length == 0 || buildProviders == null || buildProviders.Count == 0)
		{
			return null;
		}
		BuildProvider providerInstanceForExtension = buildProviders.GetProviderInstanceForExtension(Path.GetExtension(file));
		if (providerInstanceForExtension != null && IsCorrectBuilderType(providerInstanceForExtension))
		{
			providerInstanceForExtension.SetVirtualPath(PhysicalToVirtual(file));
			return providerInstanceForExtension;
		}
		return null;
	}

	private bool IsCorrectBuilderType(BuildProvider bp)
	{
		if (bp == null)
		{
			return false;
		}
		object[] customAttributes = bp.GetType().GetCustomAttributes(inherit: true);
		if (customAttributes == null)
		{
			return false;
		}
		bool flag = false;
		object[] array = customAttributes;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is BuildProviderAppliesToAttribute buildProviderAppliesToAttribute)
			{
				flag = true;
				if ((buildProviderAppliesToAttribute.AppliesTo & BuildProviderAppliesTo.All) == BuildProviderAppliesTo.All || (buildProviderAppliesToAttribute.AppliesTo & BuildProviderAppliesTo.Code) == BuildProviderAppliesTo.Code)
				{
					return true;
				}
			}
		}
		if (flag)
		{
			return false;
		}
		return true;
	}
}
