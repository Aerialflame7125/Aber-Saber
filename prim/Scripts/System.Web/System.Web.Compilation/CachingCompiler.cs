using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;

namespace System.Web.Compilation;

internal class CachingCompiler
{
	private static string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;

	private static Hashtable compilationTickets = new Hashtable();

	private const string cachePrefix = "@@Assembly";

	private const string cacheTypePrefix = "@@@Type";

	private static Hashtable assemblyCache = new Hashtable();

	public static void InsertTypeFileDep(Type type, string filename)
	{
		CacheDependency dependencies = new CacheDependency(filename);
		HttpRuntime.InternalCache.Insert("@@@Type" + filename, type, dependencies);
	}

	public static void InsertType(Type type, string filename)
	{
		string[] cachekeys = new string[1] { "@@Assembly" + filename };
		CacheDependency dependencies = new CacheDependency(null, cachekeys);
		HttpRuntime.InternalCache.Insert("@@@Type" + filename, type, dependencies);
	}

	public static Type GetTypeFromCache(string filename)
	{
		return (Type)HttpRuntime.InternalCache["@@@Type" + filename];
	}

	public static CompilerResults Compile(BaseCompiler compiler)
	{
		Cache internalCache = HttpRuntime.InternalCache;
		string key = "@@Assembly" + compiler.Parser.InputFile;
		CompilerResults compilerResults = (CompilerResults)internalCache[key];
		if (!compiler.IsRebuildingPartial && compilerResults != null)
		{
			return compilerResults;
		}
		object ticket;
		bool flag = AcquireCompilationTicket(key, out ticket);
		try
		{
			Monitor.Enter(ticket);
			compilerResults = (CompilerResults)internalCache[key];
			if (!compiler.IsRebuildingPartial && compilerResults != null)
			{
				return compilerResults;
			}
			CodeDomProvider provider = compiler.Provider;
			CompilerParameters compilerParameters = compiler.CompilerParameters;
			GetExtraAssemblies(compilerParameters);
			compilerResults = provider.CompileAssemblyFromDom(compilerParameters, compiler.CompileUnit);
			List<string> dependencies = compiler.Parser.Dependencies;
			if (dependencies != null && dependencies.Count > 0)
			{
				string[] array = dependencies.ToArray();
				HttpRequest httpRequest = HttpContext.Current?.Request;
				if (httpRequest == null)
				{
					throw new HttpException("No current context, cannot compile.");
				}
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = httpRequest.MapPath(array[i]);
				}
				internalCache.Insert(key, compilerResults, new CacheDependency(array));
			}
		}
		finally
		{
			Monitor.Exit(ticket);
			if (flag)
			{
				ReleaseCompilationTicket(key);
			}
		}
		return compilerResults;
	}

	public static CompilerResults Compile(WebServiceCompiler compiler)
	{
		string key = "@@Assembly" + compiler.Parser.PhysicalPath;
		Cache internalCache = HttpRuntime.InternalCache;
		CompilerResults compilerResults = (CompilerResults)internalCache[key];
		if (compilerResults != null)
		{
			return compilerResults;
		}
		object ticket;
		bool flag = AcquireCompilationTicket(key, out ticket);
		try
		{
			Monitor.Enter(ticket);
			compilerResults = (CompilerResults)internalCache[key];
			if (compilerResults != null)
			{
				return compilerResults;
			}
			CodeDomProvider provider = compiler.Provider;
			CompilerParameters compilerParameters = compiler.CompilerParameters;
			GetExtraAssemblies(compilerParameters);
			compilerResults = provider.CompileAssemblyFromFile(compilerParameters, compiler.InputFile);
			string[] filenames = (string[])compiler.Parser.Dependencies.ToArray(typeof(string));
			internalCache.Insert(key, compilerResults, new CacheDependency(filenames));
			return compilerResults;
		}
		finally
		{
			Monitor.Exit(ticket);
			if (flag)
			{
				ReleaseCompilationTicket(key);
			}
		}
	}

	internal static CompilerParameters GetOptions(ICollection assemblies)
	{
		CompilerParameters compilerParameters = new CompilerParameters();
		if (assemblies != null)
		{
			StringCollection referencedAssemblies = compilerParameters.ReferencedAssemblies;
			foreach (string assembly in assemblies)
			{
				referencedAssemblies.Add(assembly);
			}
		}
		GetExtraAssemblies(compilerParameters);
		return compilerParameters;
	}

	public static CompilerResults Compile(string language, string key, string file, ArrayList assemblies)
	{
		return Compile(language, key, file, assemblies, debug: false);
	}

	public static CompilerResults Compile(string language, string key, string file, ArrayList assemblies, bool debug)
	{
		Cache internalCache = HttpRuntime.InternalCache;
		CompilerResults compilerResults = (CompilerResults)internalCache["@@Assembly" + key];
		if (compilerResults != null)
		{
			return compilerResults;
		}
		if (!Directory.Exists(dynamicBase))
		{
			Directory.CreateDirectory(dynamicBase);
		}
		object ticket;
		bool flag = AcquireCompilationTicket("@@Assembly" + key, out ticket);
		try
		{
			Monitor.Enter(ticket);
			compilerResults = (CompilerResults)internalCache["@@Assembly" + key];
			if (compilerResults != null)
			{
				return compilerResults;
			}
			string compilerOptions;
			int warningLevel;
			string tempdir;
			CodeDomProvider obj = BaseCompiler.CreateProvider(language, out compilerOptions, out warningLevel, out tempdir) ?? throw new HttpException("Configuration error. Language not supported: " + language, 500);
			CompilerParameters options = GetOptions(assemblies);
			options.IncludeDebugInformation = debug;
			options.WarningLevel = warningLevel;
			options.CompilerOptions = compilerOptions;
			options.OutputAssembly = Path.Combine(path2: Path.GetFileName(new TempFileCollection(tempdir, keepFiles: true).AddExtension("dll", keepFile: true)), path1: dynamicBase);
			compilerResults = obj.CompileAssemblyFromFile(options, file);
			ArrayList arrayList = new ArrayList(assemblies.Count + 1);
			arrayList.Add(file);
			for (int num = assemblies.Count - 1; num >= 0; num--)
			{
				string text = (string)assemblies[num];
				if (Path.IsPathRooted(text))
				{
					arrayList.Add(text);
				}
			}
			string[] filenames = (string[])arrayList.ToArray(typeof(string));
			internalCache.Insert("@@Assembly" + key, compilerResults, new CacheDependency(filenames));
			return compilerResults;
		}
		finally
		{
			Monitor.Exit(ticket);
			if (flag)
			{
				ReleaseCompilationTicket("@@Assembly" + key);
			}
		}
	}

	public static Type CompileAndGetType(string typename, string language, string key, string file, ArrayList assemblies)
	{
		CompilerResults compilerResults = Compile(language, key, file, assemblies);
		if (compilerResults.NativeCompilerReturnValue != 0)
		{
			using (StreamReader streamReader = new StreamReader(file))
			{
				throw new CompilationException(file, compilerResults.Errors, streamReader.ReadToEnd());
			}
		}
		Assembly compiledAssembly = compilerResults.CompiledAssembly;
		if (compiledAssembly == null)
		{
			using (StreamReader streamReader2 = new StreamReader(file))
			{
				throw new CompilationException(file, compilerResults.Errors, streamReader2.ReadToEnd());
			}
		}
		Type type = compiledAssembly.GetType(typename, throwOnError: true);
		InsertType(type, file);
		return type;
	}

	private static void GetExtraAssemblies(CompilerParameters options)
	{
		StringCollection referencedAssemblies = options.ReferencedAssemblies;
		ArrayList extraAssemblies = WebConfigurationManager.ExtraAssemblies;
		if (extraAssemblies != null && extraAssemblies.Count > 0)
		{
			foreach (object item in extraAssemblies)
			{
				if (item is string value && !referencedAssemblies.Contains(value))
				{
					referencedAssemblies.Add(value);
				}
			}
		}
		IList codeAssemblies = BuildManager.CodeAssemblies;
		if (codeAssemblies != null && codeAssemblies.Count > 0)
		{
			foreach (object item2 in codeAssemblies)
			{
				Assembly assembly = item2 as Assembly;
				if (!(assembly == null))
				{
					string location = assembly.Location;
					if (location != null && !referencedAssemblies.Contains(location))
					{
						referencedAssemblies.Add(location);
					}
				}
			}
		}
		codeAssemblies = BuildManager.TopLevelAssemblies;
		if (codeAssemblies != null && codeAssemblies.Count > 0)
		{
			foreach (object item3 in codeAssemblies)
			{
				Assembly assembly = item3 as Assembly;
				if (item3 != null)
				{
					string location = assembly.Location;
					if (!referencedAssemblies.Contains(location))
					{
						referencedAssemblies.Add(location);
					}
				}
			}
		}
		AssemblyCollection assemblyCollection = ((WebConfigurationManager.GetWebApplicationSection("system.web/compilation") is CompilationSection compilationSection) ? compilationSection.Assemblies : null);
		if (assemblyCollection == null)
		{
			return;
		}
		foreach (AssemblyInfo item4 in assemblyCollection)
		{
			string assemblyLocationFromName = GetAssemblyLocationFromName(item4.Assembly);
			if (assemblyLocationFromName != null && !referencedAssemblies.Contains(assemblyLocationFromName))
			{
				referencedAssemblies.Add(assemblyLocationFromName);
			}
		}
	}

	private static string GetAssemblyLocationFromName(string name)
	{
		Assembly assembly = assemblyCache[name] as Assembly;
		if (assembly != null)
		{
			return assembly.Location;
		}
		try
		{
			assembly = Assembly.Load(name);
		}
		catch
		{
		}
		if (assembly == null)
		{
			return null;
		}
		assemblyCache[name] = assembly;
		return assembly.Location;
	}

	private static bool AcquireCompilationTicket(string key, out object ticket)
	{
		lock (compilationTickets.SyncRoot)
		{
			ticket = compilationTickets[key];
			if (ticket == null)
			{
				ticket = new object();
				compilationTickets[key] = ticket;
				return true;
			}
		}
		return false;
	}

	private static void ReleaseCompilationTicket(string key)
	{
		lock (compilationTickets.SyncRoot)
		{
			compilationTickets.Remove(key);
		}
	}
}
