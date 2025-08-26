using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AppResourcesAssemblyBuilder
{
	private CompilationSection config;

	private CompilerInfo ci;

	private CodeDomProvider _provider;

	private string baseAssemblyPath;

	private string baseAssemblyDirectory;

	private string canonicAssemblyName;

	private Assembly mainAssembly;

	private AppResourcesCompiler appResourcesCompiler;

	public CodeDomProvider Provider
	{
		get
		{
			if (_provider == null)
			{
				_provider = ci.CreateProvider();
				if (_provider == null)
				{
					throw new ApplicationException("Failed to instantiate the default compiler.");
				}
				return _provider;
			}
			return _provider;
		}
	}

	public Assembly MainAssembly => mainAssembly;

	public AppResourcesAssemblyBuilder(string canonicAssemblyName, string baseAssemblyPath, AppResourcesCompiler appres)
	{
		appResourcesCompiler = appres;
		this.baseAssemblyPath = baseAssemblyPath;
		baseAssemblyDirectory = Path.GetDirectoryName(baseAssemblyPath);
		this.canonicAssemblyName = canonicAssemblyName;
		config = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
		if (config == null || !CodeDomProvider.IsDefinedLanguage(config.DefaultLanguage))
		{
			throw new ApplicationException("Could not get the default compiler.");
		}
		ci = CodeDomProvider.GetCompilerInfo(config.DefaultLanguage);
		if (ci == null || !ci.IsCodeDomProviderTypeValid)
		{
			throw new ApplicationException("Failed to obtain the default compiler information.");
		}
	}

	public void Build()
	{
		Build(null);
	}

	public void Build(CodeCompileUnit unit)
	{
		Dictionary<string, List<string>> cultureFiles = appResourcesCompiler.CultureFiles;
		List<string> defaultCultureFiles = appResourcesCompiler.DefaultCultureFiles;
		if (defaultCultureFiles != null)
		{
			BuildDefaultAssembly(defaultCultureFiles, unit);
		}
		foreach (KeyValuePair<string, List<string>> item in cultureFiles)
		{
			BuildSatelliteAssembly(item.Key, item.Value);
		}
	}

	private void BuildDefaultAssembly(List<string> files, CodeCompileUnit unit)
	{
		AssemblyBuilder assemblyBuilder = new AssemblyBuilder(Provider);
		if (unit != null)
		{
			assemblyBuilder.AddCodeCompileUnit(unit);
		}
		CompilerParameters compilerParameters = ci.CreateDefaultCompilerParameters();
		compilerParameters.OutputAssembly = baseAssemblyPath;
		compilerParameters.GenerateExecutable = false;
		compilerParameters.TreatWarningsAsErrors = true;
		compilerParameters.IncludeDebugInformation = config.Debug;
		foreach (string file in files)
		{
			compilerParameters.EmbeddedResources.Add(file);
		}
		CompilerResults compilerResults = assemblyBuilder.BuildAssembly(compilerParameters);
		if (compilerResults == null)
		{
			return;
		}
		if (compilerResults.NativeCompilerReturnValue == 0)
		{
			mainAssembly = compilerResults.CompiledAssembly;
			BuildManager.TopLevelAssemblies.Add(mainAssembly);
			HttpRuntime.WritePreservationFile(mainAssembly, canonicAssemblyName);
			HttpRuntime.EnableAssemblyMapping(enable: true);
			return;
		}
		if (HttpContext.Current.IsCustomErrorEnabled)
		{
			throw new ApplicationException("An error occurred while compiling global resources.");
		}
		throw new CompilationException(null, compilerResults.Errors, null);
	}

	private void BuildSatelliteAssembly(string cultureName, List<string> files)
	{
		string text = BuildAssemblyPath(cultureName);
		ProcessStartInfo processStartInfo = new ProcessStartInfo();
		Process process = new Process();
		StringBuilder stringBuilder = new StringBuilder(SetAlPath(processStartInfo));
		stringBuilder.Append("/c:\"" + cultureName + "\" ");
		stringBuilder.Append("/t:lib ");
		stringBuilder.Append("/out:\"" + text + "\" ");
		if (mainAssembly != null)
		{
			stringBuilder.Append("/template:\"" + mainAssembly.Location + "\" ");
		}
		string text2 = text + ".response";
		using (FileStream stream = File.OpenWrite(text2))
		{
			using StreamWriter streamWriter = new StreamWriter(stream);
			foreach (string file in files)
			{
				streamWriter.WriteLine("/embed:\"" + file + "\" ");
			}
		}
		stringBuilder.Append("@\"" + text2 + "\"");
		processStartInfo.Arguments = stringBuilder.ToString();
		processStartInfo.CreateNoWindow = true;
		processStartInfo.UseShellExecute = false;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.RedirectStandardError = true;
		process.StartInfo = processStartInfo;
		StringCollection alOutput = new StringCollection();
		Mutex alMutex = new Mutex();
		DataReceivedEventHandler value = delegate(object sender, DataReceivedEventArgs args)
		{
			if (args.Data != null)
			{
				alMutex.WaitOne();
				alOutput.Add(args.Data);
				alMutex.ReleaseMutex();
			}
		};
		process.ErrorDataReceived += value;
		process.OutputDataReceived += value;
		try
		{
			process.Start();
		}
		catch (Exception innerException)
		{
			throw new HttpException($"Error running {process.StartInfo.FileName}", innerException);
		}
		Exception ex = null;
		int num = 0;
		try
		{
			process.BeginOutputReadLine();
			process.BeginErrorReadLine();
			process.WaitForExit();
			num = process.ExitCode;
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		finally
		{
			process.CancelErrorRead();
			process.CancelOutputRead();
			process.Close();
		}
		if (num == 0 && ex == null)
		{
			return;
		}
		CompilerErrorCollection compilerErrorCollection = null;
		if (alOutput.Count != 0)
		{
			StringEnumerator enumerator2 = alOutput.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					string current2 = enumerator2.Current;
					if (current2.StartsWith("ALINK: error ", StringComparison.Ordinal))
					{
						if (compilerErrorCollection == null)
						{
							compilerErrorCollection = new CompilerErrorCollection();
						}
						int num2 = current2.IndexOf(':', 13);
						string errorNumber = ((num2 != -1) ? current2.Substring(13, num2 - 13) : "Unknown");
						string errorText = ((num2 != -1) ? current2.Substring(num2 + 1) : current2.Substring(13));
						compilerErrorCollection.Add(new CompilerError(Path.GetFileName(text), 0, 0, errorNumber, errorText));
					}
				}
			}
			finally
			{
				if (enumerator2 is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
		}
		throw new CompilationException(Path.GetFileName(text), compilerErrorCollection, null);
	}

	private string SetAlPath(ProcessStartInfo info)
	{
		if (RuntimeHelpers.RunningOnWindows)
		{
			info.FileName = System.MonoToolsLocator.Mono;
			return System.MonoToolsLocator.AssemblyLinker + " ";
		}
		info.FileName = System.MonoToolsLocator.AssemblyLinker;
		return string.Empty;
	}

	private string BuildAssemblyPath(string cultureName)
	{
		string text = Path.Combine(baseAssemblyDirectory, cultureName);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string path = Path.GetFileNameWithoutExtension(baseAssemblyPath) + ".resources.dll";
		return Path.Combine(text, path);
	}

	private CodeCompileUnit GenerateAssemblyInfo(string cultureName)
	{
		CodeAttributeArgument[] arguments = new CodeAttributeArgument[1]
		{
			new CodeAttributeArgument(new CodePrimitiveExpression(cultureName))
		};
		CodeCompileUnit obj = new CodeCompileUnit
		{
			AssemblyCustomAttributes = 
			{
				new CodeAttributeDeclaration(new CodeTypeReference("System.Reflection.AssemblyCultureAttribute"), arguments)
			}
		};
		arguments = new CodeAttributeArgument[2]
		{
			new CodeAttributeArgument(new CodePrimitiveExpression("ASP.NET")),
			new CodeAttributeArgument(new CodePrimitiveExpression(Environment.Version.ToString()))
		};
		obj.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.CodeDom.Compiler.GeneratedCodeAttribute"), arguments));
		return obj;
	}
}
