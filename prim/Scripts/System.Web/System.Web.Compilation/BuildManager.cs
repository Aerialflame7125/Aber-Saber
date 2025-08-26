using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;
using System.Xml;

namespace System.Web.Compilation;

/// <summary>Provides a set of methods to help manage the compilation of an ASP.NET application.</summary>
public sealed class BuildManager
{
	private class PreCompilationData
	{
		public string VirtualPath;

		public string AssemblyFileName;

		public string TypeName;

		public Type Type;
	}

	internal const string FAKE_VIRTUAL_PATH_PREFIX = "/@@MonoFakeVirtualPath@@";

	private const string BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX = "@@Build_Manager@@";

	private static int BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX_LENGTH;

	private static readonly object bigCompilationLock;

	private static readonly object virtualPathsToIgnoreLock;

	private static readonly char[] virtualPathsToIgnoreSplitChars;

	private static EventHandlerList events;

	private static object buildManagerRemoveEntryEvent;

	private static bool hosted;

	private static Dictionary<string, bool> virtualPathsToIgnore;

	private static bool virtualPathsToIgnoreChecked;

	private static bool haveVirtualPathsToIgnore;

	private static List<Assembly> AppCode_Assemblies;

	private static List<Assembly> TopLevel_Assemblies;

	private static Dictionary<Type, CodeDomProvider> codeDomProviders;

	private static Dictionary<string, BuildManagerCacheItem> buildCache;

	private static List<Assembly> referencedAssemblies;

	private static List<Assembly> configReferencedAssemblies;

	private static bool getReferencedAssembliesInvoked;

	private static int buildCount;

	private static bool is_precompiled;

	private static bool allowReferencedAssembliesCaching;

	private static List<Assembly> dynamicallyRegisteredAssemblies;

	private static bool? batchCompilationEnabled;

	private static FrameworkName targetFramework;

	private static bool preStartMethodsDone;

	private static bool preStartMethodsRunning;

	private static Dictionary<string, PreCompilationData> precompiled;

	internal static bool suppressDebugModeMessages;

	private static ReaderWriterLockSlim buildCacheLock;

	private static ulong recursionDepth;

	internal static bool AllowReferencedAssembliesCaching
	{
		get
		{
			return allowReferencedAssembliesCaching;
		}
		set
		{
			allowReferencedAssembliesCaching = value;
		}
	}

	internal static bool IsPrecompiled => is_precompiled;

	internal static bool CompilingTopLevelAssemblies { get; set; }

	internal static bool PreStartMethodsRunning => preStartMethodsRunning;

	/// <summary>Gets or sets a value that indicates whether batch compilation is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if batch compilation is always enabled, <see langword="false" /> if batch compilation is never enabled, or <see langword="null" /> if the compilation setting is determined from the configuration file. The default value is <see langword="null" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The property was not set in the <see langword="PreApplicationStart" /> method.</exception>
	public static bool? BatchCompilationEnabled
	{
		get
		{
			return batchCompilationEnabled;
		}
		set
		{
			if (preStartMethodsDone)
			{
				throw new InvalidOperationException("This method cannot be called after the application's pre-start initialization stage.");
			}
			batchCompilationEnabled = value;
		}
	}

	/// <summary>Gets the target version of the .NET Framework for the current Web site.</summary>
	/// <returns>The target version of the .NET Framework for the current Web site.</returns>
	public static FrameworkName TargetFramework
	{
		get
		{
			if (targetFramework == null)
			{
				string text = CompilationConfig?.TargetFramework;
				if (string.IsNullOrEmpty(text))
				{
					targetFramework = new FrameworkName(".NETFramework,Version=v4.0");
				}
				else
				{
					targetFramework = new FrameworkName(text);
				}
			}
			return targetFramework;
		}
	}

	internal static bool BatchMode
	{
		get
		{
			if (batchCompilationEnabled.HasValue)
			{
				return batchCompilationEnabled.Value;
			}
			if (!hosted)
			{
				return false;
			}
			return CompilationConfig?.Batch ?? true;
		}
	}

	/// <summary>Gets a list of assemblies built from the App_Code directory.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> collection that contains the assemblies built from the App_Code directory.</returns>
	public static IList CodeAssemblies => AppCode_Assemblies;

	internal static CompilationSection CompilationConfig => WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;

	internal static bool HaveResources { get; set; }

	internal static IList TopLevelAssemblies => TopLevel_Assemblies;

	internal static event BuildManagerRemoveEntryEventHandler RemoveEntry
	{
		add
		{
			events.AddHandler(buildManagerRemoveEntryEvent, value);
		}
		remove
		{
			events.RemoveHandler(buildManagerRemoveEntryEvent, value);
		}
	}

	static BuildManager()
	{
		BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX_LENGTH = "@@Build_Manager@@".Length;
		bigCompilationLock = new object();
		virtualPathsToIgnoreLock = new object();
		virtualPathsToIgnoreSplitChars = new char[1] { ',' };
		events = new EventHandlerList();
		buildManagerRemoveEntryEvent = new object();
		AppCode_Assemblies = new List<Assembly>();
		TopLevel_Assemblies = new List<Assembly>();
		hosted = AppDomain.CurrentDomain.GetData(".:!MonoAspNetHostedApp!:.") as string == "yes";
		buildCache = new Dictionary<string, BuildManagerCacheItem>(RuntimeHelpers.StringEqualityComparer);
		buildCacheLock = new ReaderWriterLockSlim();
		referencedAssemblies = new List<Assembly>();
		recursionDepth = 0uL;
		string appDomainAppPath = HttpRuntime.AppDomainAppPath;
		string precomp_config = null;
		is_precompiled = !string.IsNullOrEmpty(appDomainAppPath) && File.Exists(precomp_config = Path.Combine(appDomainAppPath, "PrecompiledApp.config"));
		if (is_precompiled)
		{
			is_precompiled = LoadPrecompilationInfo(precomp_config);
		}
	}

	internal static void AssertPreStartMethodsRunning()
	{
		if (!PreStartMethodsRunning)
		{
			throw new InvalidOperationException("This method must be called during the application's pre-start initialization stage.");
		}
	}

	private static void FixVirtualPaths()
	{
		if (precompiled == null)
		{
			return;
		}
		int num = -1;
		string text = VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath);
		foreach (string key in precompiled.Keys)
		{
			string[] array = key.Split('/');
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					VirtualPath absoluteVirtualPath = GetAbsoluteVirtualPath(text + string.Join("/", array, i, array.Length - i));
					if (absoluteVirtualPath != null && File.Exists(absoluteVirtualPath.PhysicalPath))
					{
						num = i - 1;
						break;
					}
				}
			}
		}
		string text2 = HttpRuntime.AppDomainAppVirtualPath;
		switch (num)
		{
		case 0:
			if (text2 == "/")
			{
				return;
			}
			break;
		case -1:
			return;
		}
		if (!text2.EndsWith("/"))
		{
			text2 += "/";
		}
		Dictionary<string, PreCompilationData> dictionary = new Dictionary<string, PreCompilationData>(precompiled);
		precompiled.Clear();
		foreach (KeyValuePair<string, PreCompilationData> item in dictionary)
		{
			string[] array = item.Key.Split('/');
			string text3 = ((!string.IsNullOrEmpty(array[0])) ? (text2 + string.Join("/", array, num, array.Length - num)) : (text2 + string.Join("/", array, num + 1, array.Length - num - 1)));
			item.Value.VirtualPath = text3;
			precompiled.Add(text3, item.Value);
		}
	}

	private static bool LoadPrecompilationInfo(string precomp_config)
	{
		using (XmlTextReader xmlTextReader = new XmlTextReader(precomp_config))
		{
			xmlTextReader.MoveToContent();
			if (xmlTextReader.Name != "precompiledApp")
			{
				return false;
			}
		}
		string[] files = Directory.GetFiles(HttpRuntime.BinDirectory, "*.compiled");
		for (int i = 0; i < files.Length; i++)
		{
			LoadCompiled(files[i]);
		}
		FixVirtualPaths();
		return true;
	}

	private static void LoadCompiled(string filename)
	{
		using XmlTextReader xmlTextReader = new XmlTextReader(filename);
		xmlTextReader.MoveToContent();
		if (!(xmlTextReader.Name == "preserve") || !xmlTextReader.HasAttributes)
		{
			return;
		}
		xmlTextReader.MoveToNextAttribute();
		string value = xmlTextReader.Value;
		if (xmlTextReader.Name == "resultType")
		{
			switch (value)
			{
			case "2":
			case "3":
			case "8":
				LoadPageData(xmlTextReader, store: true);
				return;
			}
		}
		switch (value)
		{
		case "1":
		case "6":
		{
			PreCompilationData preCompilationData = LoadPageData(xmlTextReader, store: false);
			CodeAssemblies.Add(Assembly.Load(preCompilationData.AssemblyFileName));
			break;
		}
		case "9":
			HttpContext.AppGlobalResourcesAssembly = Assembly.Load(LoadPageData(xmlTextReader, store: false).AssemblyFileName);
			break;
		}
	}

	private static PreCompilationData LoadPageData(XmlTextReader reader, bool store)
	{
		PreCompilationData preCompilationData = new PreCompilationData();
		while (reader.MoveToNextAttribute())
		{
			switch (reader.Name)
			{
			case "virtualPath":
				preCompilationData.VirtualPath = VirtualPathUtility.RemoveTrailingSlash(reader.Value);
				break;
			case "assembly":
				preCompilationData.AssemblyFileName = reader.Value;
				break;
			case "type":
				preCompilationData.TypeName = reader.Value;
				break;
			}
		}
		if (store)
		{
			if (precompiled == null)
			{
				precompiled = new Dictionary<string, PreCompilationData>(RuntimeHelpers.StringEqualityComparerCulture);
			}
			precompiled.Add(preCompilationData.VirtualPath, preCompilationData);
		}
		return preCompilationData;
	}

	private static void AddAssembly(Assembly asm, List<Assembly> al)
	{
		if (!al.Contains(asm))
		{
			al.Add(asm);
		}
	}

	private static void AddPathToIgnore(string vp)
	{
		if (virtualPathsToIgnore == null)
		{
			virtualPathsToIgnore = new Dictionary<string, bool>(RuntimeHelpers.StringEqualityComparerCulture);
		}
		VirtualPath absoluteVirtualPath = GetAbsoluteVirtualPath(vp);
		string absolute = absoluteVirtualPath.Absolute;
		if (!virtualPathsToIgnore.ContainsKey(absolute))
		{
			virtualPathsToIgnore.Add(absolute, value: true);
			haveVirtualPathsToIgnore = true;
		}
		string appRelative = absoluteVirtualPath.AppRelative;
		if (!virtualPathsToIgnore.ContainsKey(appRelative))
		{
			virtualPathsToIgnore.Add(appRelative, value: true);
			haveVirtualPathsToIgnore = true;
		}
		if (!virtualPathsToIgnore.ContainsKey(vp))
		{
			virtualPathsToIgnore.Add(vp, value: true);
			haveVirtualPathsToIgnore = true;
		}
	}

	internal static void AddToReferencedAssemblies(Assembly asm)
	{
	}

	private static void AssertVirtualPathExists(VirtualPath virtualPath)
	{
		bool flag = false;
		if (virtualPath.IsFake)
		{
			string physicalPath = virtualPath.PhysicalPath;
			if (!File.Exists(physicalPath) && !Directory.Exists(physicalPath))
			{
				flag = true;
			}
		}
		else
		{
			VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
			string absolute = virtualPath.Absolute;
			if (!virtualPathProvider.FileExists(absolute) && !virtualPathProvider.DirectoryExists(absolute))
			{
				flag = true;
			}
		}
		if (flag)
		{
			throw new HttpException(404, string.Concat("The file '", virtualPath, "' does not exist."), virtualPath.Absolute);
		}
	}

	private static void Build(VirtualPath vp)
	{
		AssertVirtualPathExists(vp);
		CompilationSection compilationConfig = CompilationConfig;
		lock (bigCompilationLock)
		{
			if (HasCachedItemNoLock(vp.Absolute, out var entryExists))
			{
				return;
			}
			if (recursionDepth == 0L)
			{
				referencedAssemblies.Clear();
			}
			recursionDepth++;
			try
			{
				BuildInner(vp, compilationConfig?.Debug ?? false);
				if (entryExists && recursionDepth <= 1)
				{
					buildCount++;
				}
			}
			finally
			{
				if (buildCount > compilationConfig.NumRecompilesBeforeAppRestart)
				{
					HttpRuntime.UnloadAppDomain();
				}
				recursionDepth--;
			}
		}
	}

	private static void BuildInner(VirtualPath vp, bool debug)
	{
		BuildManagerDirectoryBuilder buildManagerDirectoryBuilder = new BuildManagerDirectoryBuilder(vp);
		bool flag = recursionDepth > 1;
		List<BuildProviderGroup> list = buildManagerDirectoryBuilder.Build(IsSingleBuild(vp, flag));
		if (list == null)
		{
			return;
		}
		string absolute = vp.Absolute;
		int num = (absolute.GetHashCode() | (int)DateTime.Now.Ticks) + (int)recursionDepth;
		foreach (BuildProviderGroup item in list)
		{
			bool flag2 = false;
			CompilationException ex = null;
			string text = null;
			bool flag3;
			if (item.Count == 1)
			{
				if (flag || !item.Master)
				{
					text = $"{item.NamePrefix}_{VirtualPathUtility.GetFileName(item[0].VirtualPath)}.{num:x}.";
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			if (text == null)
			{
				text = item.NamePrefix + "_";
			}
			CompilerType compilerType = item.CompilerType;
			int num2 = 3;
			while (num2 > 0)
			{
				AssemblyBuilder assemblyBuilder = new AssemblyBuilder(vp, CreateDomProvider(compilerType), text);
				assemblyBuilder.CompilerOptions = compilerType.CompilerParameters;
				assemblyBuilder.AddAssemblyReference(GetReferencedAssemblies() as List<Assembly>);
				try
				{
					GenerateAssembly(assemblyBuilder, item, vp, debug);
					num2 = 0;
				}
				catch (CompilationException ex2)
				{
					num2--;
					if (flag3)
					{
						throw new HttpException("Single file build failed.", ex2);
					}
					if (num2 == 0)
					{
						flag2 = true;
						ex = ex2;
						break;
					}
					CompilerResults results = ex2.Results;
					if (results == null)
					{
						throw new HttpException("No results returned from failed compilation.", ex2);
					}
					RemoveFailedAssemblies(absolute, ex2, assemblyBuilder, item, results, debug);
				}
			}
			if (!flag2)
			{
				continue;
			}
			if (HasCachedItemNoLock(absolute))
			{
				if (debug)
				{
					DescribeCompilationError("Path '{0}' built successfully, but a compilation exception has been thrown for other files:", ex, absolute);
				}
				break;
			}
			Build(vp);
			if (HasCachedItemNoLock(absolute))
			{
				if (debug)
				{
					DescribeCompilationError("Path '{0}' built successfully, but a compilation exception has been thrown for other files:", ex, absolute);
				}
				break;
			}
			throw new HttpException("Requested virtual path build failed.", ex);
		}
	}

	private static CodeDomProvider CreateDomProvider(CompilerType ct)
	{
		if (codeDomProviders == null)
		{
			codeDomProviders = new Dictionary<Type, CodeDomProvider>();
		}
		Type codeDomProviderType = ct.CodeDomProviderType;
		if (codeDomProviderType == null)
		{
			CompilationSection compilationConfig = CompilationConfig;
			CompilerType defaultCompilerTypeForLanguage = GetDefaultCompilerTypeForLanguage(compilationConfig.DefaultLanguage, compilationConfig);
			if (defaultCompilerTypeForLanguage != null)
			{
				codeDomProviderType = defaultCompilerTypeForLanguage.CodeDomProviderType;
			}
		}
		if (codeDomProviderType == null)
		{
			return null;
		}
		if (codeDomProviders.TryGetValue(codeDomProviderType, out var value))
		{
			return value;
		}
		if (!(Activator.CreateInstance(codeDomProviderType) is CodeDomProvider codeDomProvider))
		{
			return null;
		}
		codeDomProviders.Add(codeDomProviderType, codeDomProvider);
		return codeDomProvider;
	}

	internal static void CallPreStartMethods()
	{
		if (preStartMethodsDone)
		{
			return;
		}
		preStartMethodsRunning = true;
		MethodInfo methodInfo = null;
		try
		{
			List<MethodInfo> list = LoadPreStartMethodsFromAssemblies(GetReferencedAssemblies() as List<Assembly>);
			if (list == null || list.Count == 0)
			{
				return;
			}
			foreach (MethodInfo item in list)
			{
				(methodInfo = item).Invoke(null, null);
			}
		}
		catch (Exception ex)
		{
			throw new HttpException(string.Format("The pre-application start initialization method {0} on type {1} threw an exception with the following error message: {2}", (methodInfo != null) ? methodInfo.Name : "UNKNOWN", (methodInfo != null) ? methodInfo.DeclaringType.FullName : "UNKNOWN", ex.Message), ex);
		}
		finally
		{
			preStartMethodsRunning = false;
			preStartMethodsDone = true;
		}
	}

	private static List<MethodInfo> LoadPreStartMethodsFromAssemblies(List<Assembly> assemblies)
	{
		if (assemblies == null || assemblies.Count == 0)
		{
			return null;
		}
		List<MethodInfo> list = new List<MethodInfo>();
		foreach (Assembly assembly in assemblies)
		{
			PreApplicationStartMethodAttribute preApplicationStartMethodAttribute;
			Type type;
			try
			{
				object[] customAttributes = assembly.GetCustomAttributes(typeof(PreApplicationStartMethodAttribute), inherit: false);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					preApplicationStartMethodAttribute = customAttributes[0] as PreApplicationStartMethodAttribute;
					type = preApplicationStartMethodAttribute.Type;
					if (!(type == null))
					{
						goto IL_0068;
					}
				}
			}
			catch
			{
			}
			continue;
			IL_0068:
			Exception innerException = null;
			MethodInfo methodInfo;
			try
			{
				methodInfo = ((!type.IsPublic) ? null : type.GetMethod(preApplicationStartMethodAttribute.MethodName, BindingFlags.Static | BindingFlags.Public, null, new Type[0], null));
			}
			catch (Exception ex)
			{
				innerException = ex;
				methodInfo = null;
			}
			if (methodInfo == null)
			{
				throw new HttpException($"The method specified by the PreApplicationStartMethodAttribute on assembly '{assembly.FullName}' cannot be resolved. Type: '{type.FullName}', MethodName: '{preApplicationStartMethodAttribute.MethodName}'. Verify that the type is public and the method is public and static (Shared in Visual Basic).", innerException);
			}
			list.Add(methodInfo);
		}
		return list;
	}

	/// <summary>Gets an object that represents the compiled type for the Global.asax file.</summary>
	/// <returns>An object that represents the compiled type for the Global.asax file.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt was made to call this method before the Global.asax page was compiled.</exception>
	public static Type GetGlobalAsaxType()
	{
		Type appType = HttpApplicationFactory.AppType;
		if (appType == null)
		{
			return typeof(HttpApplication);
		}
		return appType;
	}

	/// <summary>Creates a cached file.</summary>
	/// <param name="fileName">The name of the file to create.</param>
	/// <returns>The <see cref="T:System.IO.Stream" /> object for the new file.</returns>
	public static Stream CreateCachedFile(string fileName)
	{
		if (fileName != null && (fileName == string.Empty || fileName.IndexOf(Path.DirectorySeparatorChar) != -1))
		{
			throw new ArgumentException("Value does not fall within the expected range.");
		}
		return new FileStream(Path.Combine(HttpRuntime.CodegenDir, fileName), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
	}

	/// <summary>Reads a cached file.</summary>
	/// <param name="fileName">The name of the file to read.</param>
	/// <returns>The <see cref="T:System.IO.Stream" /> object for the file, or <see langword="null" /> if the file does not exist.</returns>
	public static Stream ReadCachedFile(string fileName)
	{
		if (fileName != null && (fileName == string.Empty || fileName.IndexOf(Path.DirectorySeparatorChar) != -1))
		{
			throw new ArgumentException("Value does not fall within the expected range.");
		}
		string path = Path.Combine(HttpRuntime.CodegenDir, fileName);
		if (!File.Exists(path))
		{
			return null;
		}
		return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
	}

	/// <summary>Adds an assembly to the application's set of referenced assemblies.</summary>
	/// <param name="assembly">The assembly to add.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="assembly" /> parameter is <see langword="null" /> or empty.</exception>
	/// <exception cref="T:System.InvalidOperationException">The method was not called before the <see langword="Application_Start" /> event in the Global.asax file occurred.</exception>
	[MonoDocumentationNote("Fully implemented but no info on application pre-init stage is available yet.")]
	public static void AddReferencedAssembly(Assembly assembly)
	{
		if (assembly == null)
		{
			throw new ArgumentNullException("assembly");
		}
		if (preStartMethodsDone)
		{
			throw new InvalidOperationException("This method cannot be called after the application's pre-start initialization stage.");
		}
		if (dynamicallyRegisteredAssemblies == null)
		{
			dynamicallyRegisteredAssemblies = new List<Assembly>();
		}
		if (!dynamicallyRegisteredAssemblies.Contains(assembly))
		{
			dynamicallyRegisteredAssemblies.Add(assembly);
		}
	}

	/// <summary>Gets an object factory for the specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path.</param>
	/// <param name="throwIfNotFound">
	///       <see langword="true" /> to throw an error if the virtual path does not exist; otherwise, <see langword="false" />. If the virtual path does not exist and <paramref name="throwIfNotFound" /> is <see langword="false" />, this method returns <see langword="null" />.</param>
	/// <returns>The object factory.</returns>
	/// <exception cref="T:System.Web.HttpException">The virtual path does not exist.-or-A higher-level exception already existed when this method was called.-or-This method was called while the compilation process was building top-level files.-or-This is a precompiled application and the virtual path was not found in the cache.-or-A circular reference was detected.</exception>
	[MonoDocumentationNote("Not used by Mono internally. Needed for MVC3")]
	public static IWebObjectFactory GetObjectFactory(string virtualPath, bool throwIfNotFound)
	{
		if (CompilingTopLevelAssemblies)
		{
			throw new HttpException("Method must not be called while compiling the top level assemblies.");
		}
		Type precompiledType;
		if (is_precompiled)
		{
			precompiledType = GetPrecompiledType(virtualPath);
			if (precompiledType == null)
			{
				if (throwIfNotFound)
				{
					throw new HttpException($"Virtual path '{virtualPath}' not found in precompiled application type cache.");
				}
				return null;
			}
			return new SimpleWebObjectFactory(precompiledType);
		}
		Exception innerException = null;
		try
		{
			precompiledType = GetCompiledType(virtualPath);
		}
		catch (Exception ex)
		{
			innerException = ex;
			precompiledType = null;
		}
		if (precompiledType == null)
		{
			if (throwIfNotFound)
			{
				throw new HttpException($"Virtual path '{virtualPath}' does not exist.", innerException);
			}
			return null;
		}
		return new SimpleWebObjectFactory(precompiledType);
	}

	/// <summary>Processes a file, given its virtual path, and creates an instance of the result.</summary>
	/// <param name="virtualPath">The virtual path of the file to create an instance of.</param>
	/// <param name="requiredBaseType">The base type that defines the object to be created.</param>
	/// <returns>The <see cref="T:System.Object" /> that represents the instance of the processed file.</returns>
	public static object CreateInstanceFromVirtualPath(string virtualPath, Type requiredBaseType)
	{
		return CreateInstanceFromVirtualPath(GetAbsoluteVirtualPath(virtualPath), requiredBaseType);
	}

	internal static object CreateInstanceFromVirtualPath(VirtualPath virtualPath, Type requiredBaseType)
	{
		if (requiredBaseType == null)
		{
			throw new NullReferenceException();
		}
		Type compiledType = GetCompiledType(virtualPath);
		if (compiledType == null)
		{
			return null;
		}
		if (!requiredBaseType.IsAssignableFrom(compiledType))
		{
			throw new HttpException(500, $"Type '{compiledType.FullName}' does not inherit from '{requiredBaseType.FullName}'.");
		}
		return Activator.CreateInstance(compiledType, null);
	}

	private static void DescribeCompilationError(string format, CompilationException ex, params object[] parms)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string newLine = Environment.NewLine;
		if (parms != null)
		{
			stringBuilder.AppendFormat(format + newLine, parms);
		}
		else
		{
			stringBuilder.Append(format + newLine);
		}
		CompilerResults compilerResults = ex?.Results;
		if (compilerResults == null)
		{
			stringBuilder.Append("No compiler error information present." + newLine);
		}
		else
		{
			stringBuilder.Append("Compiler errors:" + newLine);
			foreach (CompilerError error in compilerResults.Errors)
			{
				stringBuilder.Append("  " + error.ToString() + newLine);
			}
		}
		if (ex != null)
		{
			stringBuilder.Append(newLine + "Exception thrown:" + newLine);
			stringBuilder.Append(ex.ToString());
		}
		ShowDebugModeMessage(stringBuilder.ToString());
	}

	private static BuildProvider FindBuildProviderForPhysicalPath(string path, BuildProviderGroup group, HttpRequest req)
	{
		if (req == null || string.IsNullOrEmpty(path))
		{
			return null;
		}
		foreach (BuildProvider item in group)
		{
			if (string.Compare(path, req.MapPath(item.VirtualPath), RuntimeHelpers.StringComparison) == 0)
			{
				return item;
			}
		}
		return null;
	}

	private static void GenerateAssembly(AssemblyBuilder abuilder, BuildProviderGroup group, VirtualPath vp, bool debug)
	{
		string absolute = vp.Absolute;
		int num = 0;
		string text;
		StringBuilder stringBuilder;
		if (debug)
		{
			text = Environment.NewLine;
			stringBuilder = new StringBuilder("Code generation for certain virtual paths in a batch failed. Those files have been removed from the batch." + text);
			stringBuilder.Append("Since you're running in debug mode, here's some more information about the error:" + text);
		}
		else
		{
			text = null;
			stringBuilder = null;
		}
		List<BuildProvider> list = null;
		StringComparison stringComparison = RuntimeHelpers.StringComparison;
		foreach (BuildProvider item in group)
		{
			string virtualPath = item.VirtualPath;
			if (HasCachedItemNoLock(virtualPath))
			{
				continue;
			}
			try
			{
				item.GenerateCode(abuilder);
			}
			catch (Exception ex)
			{
				if (string.Compare(virtualPath, absolute, stringComparison) == 0)
				{
					if (ex is CompilationException || ex is ParseException)
					{
						throw;
					}
					throw new HttpException("Code generation failed.", ex);
				}
				if (list == null)
				{
					list = new List<BuildProvider>();
				}
				list.Add(item);
				num++;
				if (stringBuilder != null)
				{
					if (num > 1)
					{
						stringBuilder.Append(text);
					}
					stringBuilder.AppendFormat("Failed file virtual path: {0}; Exception: {1}{2}{1}", item.VirtualPath, text, ex);
				}
				continue;
			}
			IDictionary<string, bool> dictionary = item.ExtractDependencies();
			if (dictionary == null)
			{
				continue;
			}
			foreach (KeyValuePair<string, bool> item2 in dictionary)
			{
				BuildManagerCacheItem cachedItemNoLock = GetCachedItemNoLock(item2.Key);
				if (cachedItemNoLock != null && !(cachedItemNoLock.BuiltAssembly == null))
				{
					abuilder.AddAssemblyReference(cachedItemNoLock.BuiltAssembly);
				}
			}
		}
		if (stringBuilder != null && num > 0)
		{
			ShowDebugModeMessage(stringBuilder.ToString());
		}
		if (list != null)
		{
			foreach (BuildProvider item3 in list)
			{
				group.Remove(item3);
			}
		}
		foreach (Assembly referencedAssembly in referencedAssemblies)
		{
			if (!(referencedAssembly == null))
			{
				abuilder.AddAssemblyReference(referencedAssembly);
			}
		}
		CompilerResults compilerResults = abuilder.BuildAssembly(vp);
		Assembly assembly = compilerResults?.CompiledAssembly;
		try
		{
			buildCacheLock.EnterWriteLock();
			if (assembly != null)
			{
				referencedAssemblies.Add(assembly);
			}
			foreach (BuildProvider item4 in group)
			{
				if (!HasCachedItemNoLock(item4.VirtualPath))
				{
					StoreInCache(item4, assembly, compilerResults);
				}
			}
		}
		finally
		{
			buildCacheLock.ExitWriteLock();
		}
	}

	private static VirtualPath GetAbsoluteVirtualPath(string virtualPath)
	{
		string vpath;
		if (!VirtualPathUtility.IsRooted(virtualPath))
		{
			HttpRequest httpRequest = HttpContext.Current?.Request;
			if (httpRequest == null)
			{
				throw new HttpException("No context, cannot map paths.");
			}
			string filePath = httpRequest.FilePath;
			filePath = ((string.IsNullOrEmpty(filePath) || string.Compare(filePath, "/", StringComparison.Ordinal) == 0) ? "/" : VirtualPathUtility.GetDirectory(filePath));
			vpath = VirtualPathUtility.Combine(filePath, virtualPath);
		}
		else
		{
			vpath = virtualPath;
		}
		return new VirtualPath(vpath);
	}

	/// <summary>Returns a build dependency set for a virtual path if the path is located in the ASP.NET cache.</summary>
	/// <param name="context">The context of the request.</param>
	/// <param name="virtualPath">The virtual path from which to determine the build dependency set.</param>
	/// <returns>A <see cref="T:System.Web.Compilation.BuildDependencySet" /> object that is stored in the cache, or <see langword="null" /> if the <see cref="T:System.Web.Compilation.BuildDependencySet" /> object cannot be retrieved from the cache.</returns>
	[MonoTODO("Not implemented, always returns null")]
	public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath)
	{
		return null;
	}

	/// <summary>Returns a build dependency set for a virtual path if the path is located in the ASP.NET cache, even if the content is not current. </summary>
	/// <param name="context">The context of the request.</param>
	/// <param name="virtualPath">The virtual path from which to determine the build dependency set.</param>
	/// <param name="ensureIsUpToDate">
	///       <see langword="true" /> to specify that only a current build dependency set should be returned, or <see langword="false" /> to indicate that any available build dependency set should be returned, even if it is not current. The default is <see langword="true" />.</param>
	/// <returns>A <see cref="T:System.Web.Compilation.BuildDependencySet" /> object that is stored in the cache, or <see langword="null" /> if the <see cref="T:System.Web.Compilation.BuildDependencySet" /> object cannot be retrieved from the cache.</returns>
	[MonoTODO("Not implemented, always returns null")]
	public static BuildDependencySet GetCachedBuildDependencySet(HttpContext context, string virtualPath, bool ensureIsUpToDate)
	{
		return null;
	}

	private static BuildManagerCacheItem GetCachedItem(string vp)
	{
		try
		{
			buildCacheLock.EnterReadLock();
			return GetCachedItemNoLock(vp);
		}
		finally
		{
			buildCacheLock.ExitReadLock();
		}
	}

	private static BuildManagerCacheItem GetCachedItemNoLock(string vp)
	{
		if (buildCache.TryGetValue(vp, out var value))
		{
			return value;
		}
		return null;
	}

	internal static Type GetCodeDomProviderType(BuildProvider provider)
	{
		Type type = null;
		CompilerType codeCompilerType = provider.CodeCompilerType;
		if (codeCompilerType != null)
		{
			type = codeCompilerType.CodeDomProviderType;
		}
		if (type == null)
		{
			throw new HttpException(string.Concat("Provider '", provider, " 'fails to specify the compiler type."));
		}
		return type;
	}

	private static Type GetPrecompiledType(string virtualPath)
	{
		if (precompiled == null || precompiled.Count == 0)
		{
			return null;
		}
		VirtualPath virtualPath2 = new VirtualPath(virtualPath);
		if (!precompiled.TryGetValue(virtualPath2.Absolute, out var value) && !precompiled.TryGetValue(virtualPath, out value))
		{
			return null;
		}
		if (value.Type == null)
		{
			value.Type = Type.GetType(value.TypeName + ", " + value.AssemblyFileName, throwOnError: true);
		}
		return value.Type;
	}

	internal static Type GetPrecompiledApplicationType()
	{
		if (!is_precompiled)
		{
			return null;
		}
		string basePath = VirtualPathUtility.AppendTrailingSlash(HttpRuntime.AppDomainAppVirtualPath);
		Type precompiledType = GetPrecompiledType(VirtualPathUtility.Combine(basePath, "global.asax"));
		if (precompiledType == null)
		{
			precompiledType = GetPrecompiledType(VirtualPathUtility.Combine(basePath, "Global.asax"));
		}
		return precompiledType;
	}

	/// <summary>Compiles a file into an assembly using the specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path to build into an assembly.</param>
	/// <returns>An <see cref="T:System.Reflection.Assembly" /> object that is compiled from the specified virtual path, which is cached to either memory or to disk.</returns>
	public static Assembly GetCompiledAssembly(string virtualPath)
	{
		return GetCompiledAssembly(GetAbsoluteVirtualPath(virtualPath));
	}

	internal static Assembly GetCompiledAssembly(VirtualPath virtualPath)
	{
		string absolute = virtualPath.Absolute;
		if (is_precompiled)
		{
			Type precompiledType = GetPrecompiledType(absolute);
			if (precompiledType != null)
			{
				return precompiledType.Assembly;
			}
		}
		BuildManagerCacheItem cachedItem = GetCachedItem(absolute);
		if (cachedItem != null)
		{
			return cachedItem.BuiltAssembly;
		}
		Build(virtualPath);
		return GetCachedItem(absolute)?.BuiltAssembly;
	}

	/// <summary>Compiles a file, given its virtual path, and returns the compiled type.</summary>
	/// <param name="virtualPath">The virtual path to build into a type.</param>
	/// <returns>A <see cref="T:System.Type" /> object that represents the type generated from compiling the virtual path.</returns>
	/// <exception cref="T:System.Web.HttpException">An error occurred when compiling the virtual path.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" />.</exception>
	public static Type GetCompiledType(string virtualPath)
	{
		return GetCompiledType(GetAbsoluteVirtualPath(virtualPath));
	}

	internal static Type GetCompiledType(VirtualPath virtualPath)
	{
		string absolute = virtualPath.Absolute;
		if (is_precompiled)
		{
			Type precompiledType = GetPrecompiledType(absolute);
			if (precompiledType != null)
			{
				return precompiledType;
			}
		}
		BuildManagerCacheItem cachedItem = GetCachedItem(absolute);
		if (cachedItem != null)
		{
			ReferenceAssemblyInCompilation(cachedItem);
			return cachedItem.Type;
		}
		Build(virtualPath);
		cachedItem = GetCachedItem(absolute);
		if (cachedItem != null)
		{
			ReferenceAssemblyInCompilation(cachedItem);
			return cachedItem.Type;
		}
		return null;
	}

	/// <summary>Compiles a file, given its virtual path, and returns a custom string that the build provider persists in cache.</summary>
	/// <param name="virtualPath">The virtual path of the file to build.</param>
	/// <returns>A string, as returned by the <see cref="M:System.Web.Compilation.BuildProvider.GetCustomString(System.CodeDom.Compiler.CompilerResults)" /> method, that is cached to disk or memory.</returns>
	public static string GetCompiledCustomString(string virtualPath)
	{
		return GetCompiledCustomString(GetAbsoluteVirtualPath(virtualPath));
	}

	internal static string GetCompiledCustomString(VirtualPath virtualPath)
	{
		string absolute = virtualPath.Absolute;
		BuildManagerCacheItem cachedItem = GetCachedItem(absolute);
		if (cachedItem != null)
		{
			return cachedItem.CompiledCustomString;
		}
		Build(virtualPath);
		return GetCachedItem(absolute)?.CompiledCustomString;
	}

	internal static CompilerType GetDefaultCompilerTypeForLanguage(string language, CompilationSection configSection)
	{
		return GetDefaultCompilerTypeForLanguage(language, configSection, throwOnMissing: true);
	}

	internal static CompilerType GetDefaultCompilerTypeForLanguage(string language, CompilationSection configSection, bool throwOnMissing)
	{
		if (language == null || language.Length == 0)
		{
			throw new ArgumentNullException("language");
		}
		CompilationSection compilationSection = ((configSection != null) ? configSection : (WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection));
		Compiler compiler = compilationSection.Compilers.Get(language);
		if (compiler != null)
		{
			Type type = HttpApplication.LoadType(compiler.Type, throwOnMissing: true);
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.CompilerOptions = compiler.CompilerOptions;
			compilerParameters.WarningLevel = compiler.WarningLevel;
			SetCommonParameters(compilationSection, compilerParameters, type, language);
			return new CompilerType(type, compilerParameters);
		}
		if (CodeDomProvider.IsDefinedLanguage(language))
		{
			CompilerInfo compilerInfo = CodeDomProvider.GetCompilerInfo(language);
			CompilerParameters compilerParameters = compilerInfo.CreateDefaultCompilerParameters();
			Type type = compilerInfo.CodeDomProviderType;
			SetCommonParameters(compilationSection, compilerParameters, type, language);
			return new CompilerType(type, compilerParameters);
		}
		if (throwOnMissing)
		{
			throw new HttpException("No compiler for language '" + language + "'.");
		}
		return null;
	}

	/// <summary>Returns a list of assembly references that all page compilations must reference.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> collection of assembly references.</returns>
	public static ICollection GetReferencedAssemblies()
	{
		if (getReferencedAssembliesInvoked)
		{
			return configReferencedAssemblies;
		}
		if (allowReferencedAssembliesCaching)
		{
			getReferencedAssembliesInvoked = true;
		}
		if (configReferencedAssemblies == null)
		{
			configReferencedAssemblies = new List<Assembly>();
		}
		else if (getReferencedAssembliesInvoked)
		{
			configReferencedAssemblies.Clear();
		}
		if (!(WebConfigurationManager.GetWebApplicationSection("system.web/compilation") is CompilationSection compilationSection))
		{
			return configReferencedAssemblies;
		}
		bool flag = false;
		foreach (AssemblyInfo assembly in compilationSection.Assemblies)
		{
			if (assembly.Assembly == "*")
			{
				flag = !is_precompiled;
			}
			else
			{
				LoadAssembly(assembly, configReferencedAssemblies);
			}
		}
		foreach (Assembly topLevelAssembly in TopLevelAssemblies)
		{
			configReferencedAssemblies.Add(topLevelAssembly);
		}
		foreach (string extraAssembly in WebConfigurationManager.ExtraAssemblies)
		{
			LoadAssembly(extraAssembly, configReferencedAssemblies);
		}
		if (dynamicallyRegisteredAssemblies != null)
		{
			foreach (Assembly dynamicallyRegisteredAssembly in dynamicallyRegisteredAssemblies)
			{
				configReferencedAssemblies.Add(dynamicallyRegisteredAssembly);
			}
		}
		if (is_precompiled || flag)
		{
			string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
			foreach (string path in binDirectoryAssemblies)
			{
				try
				{
					LoadAssembly(path, configReferencedAssemblies);
				}
				catch (BadImageFormatException)
				{
				}
			}
		}
		return configReferencedAssemblies;
	}

	/// <summary>Finds a type in the top-level assemblies or in assemblies that are defined in configuration, and optionally throws an exception on failure.</summary>
	/// <param name="typeName">The name of the type.</param>
	/// <param name="throwOnError">
	///       <see langword="true" /> to throw an exception if a <see cref="T:System.Type" /> object cannot be generated for the type name; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.Type" /> object that represents the requested <paramref name="typeName" /> parameter.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="typeName" /> is invalid.- or -
	///         <paramref name="typeName" /> is ambiguous.- or -
	///         <paramref name="typeName" /> could not be found, and <paramref name="throwOnError" /> is <see langword="true" />.</exception>
	public static Type GetType(string typeName, bool throwOnError)
	{
		return GetType(typeName, throwOnError, ignoreCase: false);
	}

	/// <summary>Finds a type in the top-level assemblies, or in assemblies that are defined in configuration, by using a case-insensitive search and optionally throwing an exception on failure.</summary>
	/// <param name="typeName">The name of the type.</param>
	/// <param name="throwOnError">
	///       <see langword="true" /> to throw an exception if a <see cref="T:System.Type" /> cannot be generated for the type name; otherwise, <see langword="false" />.</param>
	/// <param name="ignoreCase">
	///       <see langword="true" /> if <paramref name="typeName" /> is case-sensitive; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.Type" /> object that represents the requested <paramref name="typeName" /> parameter.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="typeName" /> is invalid.- or -
	///         <paramref name="typeName" /> is ambiguous.- or -
	///         <paramref name="typeName" /> could not be found, and <paramref name="throwOnError" /> is <see langword="true" />.</exception>
	public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
	{
		if (string.IsNullOrEmpty(typeName))
		{
			throw new HttpException("Type name must not be empty.");
		}
		Type type = null;
		Exception innerException = null;
		try
		{
			int num = typeName.IndexOf(',');
			string text;
			string name;
			if (num > 0 && num < typeName.Length - 1)
			{
				text = new AssemblyName(typeName.Substring(num + 1)).ToString();
				name = typeName.Substring(0, num);
			}
			else
			{
				text = null;
				name = typeName;
			}
			List<Assembly> list = new List<Assembly>();
			list.AddRange(GetReferencedAssemblies() as List<Assembly>);
			list.AddRange(TopLevel_Assemblies);
			Type appType = HttpApplicationFactory.AppType;
			if (appType != null)
			{
				list.Add(appType.Assembly);
			}
			foreach (Assembly item in list)
			{
				if (item == null)
				{
					continue;
				}
				if (text != null)
				{
					if (string.Compare(text, item.GetName().ToString(), StringComparison.Ordinal) == 0)
					{
						type = item.GetType(name, throwOnError, ignoreCase);
						if (type != null)
						{
							return type;
						}
					}
				}
				else
				{
					type = item.GetType(name, throwOnError: false, ignoreCase);
					if (type != null)
					{
						return type;
					}
				}
			}
		}
		catch (Exception ex)
		{
			innerException = ex;
		}
		if (throwOnError)
		{
			throw new HttpException("Failed to find the specified type.", innerException);
		}
		return null;
	}

	/// <summary>Provides a collection of virtual-path dependencies for a specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path used to determine the dependencies.</param>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> collection of files represented by virtual paths that are caching dependencies for the virtual path.</returns>
	public static ICollection GetVirtualPathDependencies(string virtualPath)
	{
		return GetVirtualPathDependencies(virtualPath, null);
	}

	internal static ICollection GetVirtualPathDependencies(string virtualPath, BuildProvider bprovider)
	{
		BuildProvider buildProvider = bprovider;
		if (buildProvider == null)
		{
			CompilationSection compilationConfig = CompilationConfig;
			if (compilationConfig == null)
			{
				return null;
			}
			buildProvider = BuildManagerDirectoryBuilder.GetBuildProvider(virtualPath, compilationConfig.BuildProviders);
		}
		if (buildProvider == null)
		{
			return null;
		}
		IDictionary<string, bool> dictionary = buildProvider.ExtractDependencies();
		if (dictionary == null)
		{
			return null;
		}
		return (ICollection)dictionary.Keys;
	}

	internal static bool HasCachedItemNoLock(string vp, out bool entryExists)
	{
		if (buildCache.TryGetValue(vp, out var value))
		{
			entryExists = true;
			return value != null;
		}
		entryExists = false;
		return false;
	}

	internal static bool HasCachedItemNoLock(string vp)
	{
		bool entryExists;
		return HasCachedItemNoLock(vp, out entryExists);
	}

	internal static bool IgnoreVirtualPath(string virtualPath)
	{
		if (!virtualPathsToIgnoreChecked)
		{
			lock (virtualPathsToIgnoreLock)
			{
				if (!virtualPathsToIgnoreChecked)
				{
					LoadVirtualPathsToIgnore();
				}
				virtualPathsToIgnoreChecked = true;
			}
		}
		if (!haveVirtualPathsToIgnore)
		{
			return false;
		}
		if (virtualPathsToIgnore.ContainsKey(virtualPath))
		{
			return true;
		}
		return false;
	}

	private static bool IsSingleBuild(VirtualPath vp, bool recursive)
	{
		if (string.Compare(vp.AppRelative, "~/global.asax", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return true;
		}
		if (!BatchMode)
		{
			return true;
		}
		return recursive;
	}

	private static void LoadAssembly(string path, List<Assembly> al)
	{
		AddAssembly(Assembly.LoadFrom(path), al);
	}

	private static void LoadAssembly(AssemblyInfo info, List<Assembly> al)
	{
		AddAssembly(Assembly.Load(info.Assembly), al);
	}

	private static void LoadVirtualPathsToIgnore()
	{
		NameValueCollection appSettings = WebConfigurationManager.AppSettings;
		if (appSettings == null)
		{
			return;
		}
		string text = appSettings["MonoAspnetBatchCompileIgnorePaths"];
		string text2 = appSettings["MonoAspnetBatchCompileIgnoreFromFile"];
		string[] array;
		if (!string.IsNullOrEmpty(text))
		{
			array = text.Split(virtualPathsToIgnoreSplitChars);
			for (int i = 0; i < array.Length; i++)
			{
				string text3 = array[i].Trim();
				if (text3.Length != 0)
				{
					AddPathToIgnore(text3);
				}
			}
		}
		if (string.IsNullOrEmpty(text2))
		{
			return;
		}
		string path = (HttpContext.Current?.Request ?? throw new HttpException("Missing context, cannot continue.")).MapPath(text2);
		if (!File.Exists(path))
		{
			return;
		}
		string[] array2 = File.ReadAllLines(path);
		if (array2 == null || array2.Length == 0)
		{
			return;
		}
		array = array2;
		for (int i = 0; i < array.Length; i++)
		{
			string text4 = array[i].Trim();
			if (text4.Length != 0)
			{
				AddPathToIgnore(text4);
			}
		}
	}

	private static void OnEntryRemoved(string vp)
	{
		if (events[buildManagerRemoveEntryEvent] is BuildManagerRemoveEntryEventHandler buildManagerRemoveEntryEventHandler)
		{
			buildManagerRemoveEntryEventHandler(new BuildManagerRemoveEntryEventArgs(vp, HttpContext.Current));
		}
	}

	private static void OnVirtualPathChanged(string key, object value, CacheItemRemovedReason removedReason)
	{
		if (!StrUtils.StartsWith(key, "@@Build_Manager@@"))
		{
			return;
		}
		string text = key.Substring(BUILD_MANAGER_VIRTUAL_PATH_CACHE_PREFIX_LENGTH);
		try
		{
			buildCacheLock.EnterWriteLock();
			if (HasCachedItemNoLock(text))
			{
				buildCache[text] = null;
				OnEntryRemoved(text);
			}
		}
		finally
		{
			buildCacheLock.ExitWriteLock();
		}
	}

	private static void ReferenceAssemblyInCompilation(BuildManagerCacheItem bmci)
	{
		if (recursionDepth != 0L && !referencedAssemblies.Contains(bmci.BuiltAssembly))
		{
			referencedAssemblies.Add(bmci.BuiltAssembly);
		}
	}

	private static void RemoveFailedAssemblies(string requestedVirtualPath, CompilationException ex, AssemblyBuilder abuilder, BuildProviderGroup group, CompilerResults results, bool debug)
	{
		string text;
		StringBuilder stringBuilder;
		if (debug)
		{
			text = Environment.NewLine;
			stringBuilder = new StringBuilder("Compilation of certain files in a batch failed. Another attempt to compile the batch will be made." + text);
			stringBuilder.Append("Since you're running in debug mode, here's some more information about the error:" + text);
		}
		else
		{
			text = null;
			stringBuilder = null;
		}
		List<BuildProvider> list = new List<BuildProvider>();
		HttpRequest req = HttpContext.Current?.Request;
		bool flag = false;
		foreach (CompilerError error in results.Errors)
		{
			if (error.IsWarning)
			{
				continue;
			}
			BuildProvider buildProvider = abuilder.GetBuildProviderForPhysicalFilePath(error.FileName);
			if (buildProvider == null)
			{
				buildProvider = FindBuildProviderForPhysicalPath(error.FileName, group, req);
				if (buildProvider == null)
				{
					continue;
				}
			}
			if (string.Compare(buildProvider.VirtualPath, requestedVirtualPath, StringComparison.Ordinal) == 0)
			{
				flag = true;
			}
			if (!list.Contains(buildProvider))
			{
				list.Add(buildProvider);
				stringBuilder?.AppendFormat("\t{0}{1}", buildProvider.VirtualPath, text);
			}
			stringBuilder?.AppendFormat("\t\t{0}{1}", error, text);
		}
		foreach (BuildProvider item in list)
		{
			group.Remove(item);
		}
		if (stringBuilder != null)
		{
			stringBuilder.AppendFormat("{0}The following exception has been thrown for the file(s) listed above:{0}{1}", text, ex.ToString());
			ShowDebugModeMessage(stringBuilder.ToString());
			stringBuilder = null;
		}
		if (flag)
		{
			throw new HttpException("Compilation failed.", ex);
		}
	}

	private static void SetCommonParameters(CompilationSection config, CompilerParameters p, Type compilerType, string language)
	{
		p.IncludeDebugInformation = config.Debug;
		if (WebConfigurationManager.GetSection("system.web/monoSettings") is MonoSettingsSection { UseCompilersCompatibility: not false } monoSettingsSection)
		{
			Compiler compiler = monoSettingsSection.CompilersCompatibility.Get(language);
			if (compiler != null && !(HttpApplication.LoadType(compiler.Type, throwOnMissing: false) != compilerType))
			{
				p.CompilerOptions = p.CompilerOptions + " " + compiler.CompilerOptions;
			}
		}
	}

	private static void ShowDebugModeMessage(string msg)
	{
		if (!suppressDebugModeMessages)
		{
			Console.Error.WriteLine();
			Console.Error.WriteLine("******* DEBUG MODE MESSAGE *******");
			Console.Error.WriteLine(msg);
			Console.Error.WriteLine("******* DEBUG MODE MESSAGE *******");
			Console.Error.WriteLine();
		}
	}

	private static void StoreInCache(BuildProvider bp, Assembly compiledAssembly, CompilerResults results)
	{
		string virtualPath = bp.VirtualPath;
		BuildManagerCacheItem value = new BuildManagerCacheItem(compiledAssembly, bp, results);
		if (buildCache.ContainsKey(virtualPath))
		{
			buildCache[virtualPath] = value;
		}
		else
		{
			buildCache.Add(virtualPath, value);
		}
		HttpRequest httpRequest = HttpContext.Current?.Request;
		CacheDependency dependencies;
		if (httpRequest != null)
		{
			IDictionary<string, bool> dictionary = bp.ExtractDependencies();
			List<string> list = new List<string>();
			string text = httpRequest.MapPath(virtualPath);
			if (File.Exists(text))
			{
				list.Add(text);
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				foreach (KeyValuePair<string, bool> item in dictionary)
				{
					text = httpRequest.MapPath(item.Key);
					if (File.Exists(text) && !list.Contains(text))
					{
						list.Add(text);
					}
				}
			}
			dependencies = new CacheDependency(list.ToArray());
		}
		else
		{
			dependencies = null;
		}
		HttpRuntime.InternalCache.Add("@@Build_Manager@@" + virtualPath, true, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, OnVirtualPathChanged);
	}
}
