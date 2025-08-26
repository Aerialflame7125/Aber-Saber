using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Serves as the abstract base class for ASP.NET file parsers. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class TemplateParser : BaseParser
{
	[Flags]
	internal enum OutputCacheParsedParams
	{
		Location = 1,
		CacheProfile = 2,
		NoStore = 4,
		SqlDependency = 8,
		VaryByCustom = 0x10,
		VaryByHeader = 0x20,
		VaryByControl = 0x40,
		VaryByContentEncodings = 0x80
	}

	private string inputFile;

	private string text;

	private IDictionary mainAttributes;

	private List<string> dependencies;

	private List<string> assemblies;

	private IDictionary anames;

	private string[] binDirAssemblies;

	private Dictionary<string, bool> namespacesCache;

	private Dictionary<string, bool> imports;

	private List<string> interfaces;

	private List<ServerSideScript> scripts;

	private Type baseType;

	private bool baseTypeIsGlobal = true;

	private string className;

	private RootBuilder rootBuilder;

	private bool debug;

	private string compilerOptions;

	private string language;

	private bool implicitLanguage;

	private bool strictOn;

	private bool explicitOn;

	private bool linePragmasOn = true;

	private bool output_cache;

	private int oc_duration;

	private string oc_header;

	private string oc_custom;

	private string oc_param;

	private string oc_controls;

	private string oc_content_encodings;

	private string oc_cacheprofile;

	private string oc_sqldependency;

	private bool oc_nostore;

	private OutputCacheParsedParams oc_parsed_params;

	private bool oc_shared;

	private OutputCacheLocation oc_location;

	internal int allowedMainDirectives;

	private byte[] md5checksum;

	private string src;

	private bool srcIsLegacy;

	private string partialClassName;

	private string codeFileBaseClass;

	private string metaResourceKey;

	private Type codeFileBaseClassType;

	private Type pageParserFilterType;

	private PageParserFilter pageParserFilter;

	private List<UnknownAttributeDescriptor> unknownMainAttributes;

	private Stack<string> includeDirs;

	private List<string> registeredTagNames;

	private ILocation directiveLocation;

	private int appAssemblyIndex = -1;

	private static long autoClassCounter;

	internal abstract string DefaultBaseTypeName { get; }

	internal abstract string DefaultDirectiveName { get; }

	internal bool LinePragmasOn => linePragmasOn;

	internal byte[] MD5Checksum
	{
		get
		{
			return md5checksum;
		}
		set
		{
			md5checksum = value;
		}
	}

	internal PageParserFilter PageParserFilter
	{
		get
		{
			if (pageParserFilter != null)
			{
				return pageParserFilter;
			}
			Type type = PageParserFilterType;
			if (type == null)
			{
				return null;
			}
			pageParserFilter = Activator.CreateInstance(type) as PageParserFilter;
			pageParserFilter.Initialize(this);
			return pageParserFilter;
		}
	}

	internal Type PageParserFilterType
	{
		get
		{
			if (pageParserFilterType == null)
			{
				pageParserFilterType = PageParser.DefaultPageParserFilterType;
				if (pageParserFilterType != null)
				{
					return pageParserFilterType;
				}
				string text = PagesConfig.PageParserFilterType;
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				pageParserFilterType = HttpApplication.LoadType(text, throwOnMissing: true);
			}
			return pageParserFilterType;
		}
	}

	internal virtual Type DefaultBaseType => Type.GetType(DefaultBaseTypeName, throwOnError: true);

	internal ILocation DirectiveLocation => directiveLocation;

	internal string ParserDir
	{
		get
		{
			if (includeDirs == null || includeDirs.Count == 0)
			{
				return base.BaseDir;
			}
			return includeDirs.Peek();
		}
	}

	internal string InputFile
	{
		get
		{
			return inputFile;
		}
		set
		{
			inputFile = value;
		}
	}

	internal bool IsPartial
	{
		get
		{
			if (!srcIsLegacy)
			{
				return src != null;
			}
			return false;
		}
	}

	internal string CodeBehindSource
	{
		get
		{
			if (srcIsLegacy)
			{
				return null;
			}
			return src;
		}
	}

	internal string PartialClassName => partialClassName;

	internal string CodeFileBaseClass => codeFileBaseClass;

	internal string MetaResourceKey => metaResourceKey;

	internal Type CodeFileBaseClassType => codeFileBaseClassType;

	internal List<UnknownAttributeDescriptor> UnknownMainAttributes => unknownMainAttributes;

	/// <summary>Gets the string that contains the data to be parsed.</summary>
	/// <returns>The data to be parsed.</returns>
	internal string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	internal Type BaseType
	{
		get
		{
			if (baseType == null)
			{
				SetBaseType(DefaultBaseTypeName);
			}
			return baseType;
		}
	}

	internal bool BaseTypeIsGlobal
	{
		get
		{
			return baseTypeIsGlobal;
		}
		set
		{
			baseTypeIsGlobal = value;
		}
	}

	internal string ClassName
	{
		get
		{
			if (className != null)
			{
				return className;
			}
			string physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
			string text;
			if (string.IsNullOrEmpty(inputFile))
			{
				text = null;
				using StreamReader streamReader = Reader as StreamReader;
				if (streamReader != null && streamReader.BaseStream is FileStream fileStream)
				{
					text = fileStream.Name;
				}
			}
			else
			{
				text = inputFile;
			}
			if (string.IsNullOrEmpty(text))
			{
				long num = Interlocked.Increment(ref autoClassCounter);
				className = $"autoclass_nosource_{num:x}";
				return className;
			}
			if (StrUtils.StartsWith(text, physicalApplicationPath))
			{
				className = inputFile.Substring(physicalApplicationPath.Length).ToLower(Helpers.InvariantCulture);
			}
			else
			{
				className = Path.GetFileName(inputFile);
			}
			className = EncodeIdentifier(className);
			return className;
		}
	}

	internal List<ServerSideScript> Scripts
	{
		get
		{
			if (scripts == null)
			{
				scripts = new List<ServerSideScript>();
			}
			return scripts;
		}
	}

	internal Dictionary<string, bool> Imports => imports;

	internal List<string> Interfaces => interfaces;

	internal List<string> Assemblies
	{
		get
		{
			if (appAssemblyIndex != -1)
			{
				string item = assemblies[appAssemblyIndex];
				assemblies.RemoveAt(appAssemblyIndex);
				assemblies.Add(item);
				appAssemblyIndex = -1;
			}
			return assemblies;
		}
	}

	internal RootBuilder RootBuilder
	{
		get
		{
			if (rootBuilder != null)
			{
				return rootBuilder;
			}
			AspGenerator aspGenerator = AspGenerator;
			if (aspGenerator != null)
			{
				rootBuilder = aspGenerator.RootBuilder;
			}
			return rootBuilder;
		}
		set
		{
			rootBuilder = value;
		}
	}

	internal List<string> Dependencies
	{
		get
		{
			return dependencies;
		}
		set
		{
			dependencies = value;
		}
	}

	internal string CompilerOptions => compilerOptions;

	internal string Language => language;

	internal bool ImplicitLanguage => implicitLanguage;

	internal bool StrictOn => strictOn;

	internal bool ExplicitOn => explicitOn;

	internal bool Debug => debug;

	internal bool OutputCache => output_cache;

	internal int OutputCacheDuration => oc_duration;

	internal OutputCacheParsedParams OutputCacheParsedParameters => oc_parsed_params;

	internal string OutputCacheSqlDependency => oc_sqldependency;

	internal string OutputCacheCacheProfile => oc_cacheprofile;

	internal string OutputCacheVaryByContentEncodings => oc_content_encodings;

	internal bool OutputCacheNoStore => oc_nostore;

	internal virtual TextReader Reader
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	internal string OutputCacheVaryByHeader => oc_header;

	internal string OutputCacheVaryByCustom => oc_custom;

	internal string OutputCacheVaryByControls => oc_controls;

	internal bool OutputCacheShared => oc_shared;

	internal OutputCacheLocation OutputCacheLocation => oc_location;

	internal string OutputCacheVaryByParam => oc_param;

	internal List<string> RegisteredTagNames => registeredTagNames;

	internal PagesSection PagesConfig => GetConfigSection<PagesSection>("system.web/pages");

	internal AspGenerator AspGenerator { get; set; }

	internal TemplateParser()
	{
		imports = new Dictionary<string, bool>(StringComparer.Ordinal);
		LoadConfigDefaults();
		assemblies = new List<string>();
		CompilationSection compilationConfig = base.CompilationConfig;
		foreach (AssemblyInfo assembly in compilationConfig.Assemblies)
		{
			if (assembly.Assembly != "*")
			{
				AddAssemblyByName(assembly.Assembly);
			}
		}
		language = compilationConfig.DefaultLanguage;
		implicitLanguage = true;
	}

	internal virtual void LoadConfigDefaults()
	{
		AddNamespaces(imports);
		debug = base.CompilationConfig.Debug;
	}

	internal void AddApplicationAssembly()
	{
		if (base.Context.ApplicationInstance != null)
		{
			string assemblyLocation = base.Context.ApplicationInstance.AssemblyLocation;
			if (assemblyLocation != typeof(TemplateParser).Assembly.Location)
			{
				assemblies.Add(assemblyLocation);
				appAssemblyIndex = assemblies.Count - 1;
			}
		}
	}

	internal abstract Type CompileIntoType();

	internal void AddControl(Type type, IDictionary attributes)
	{
		AspGenerator?.AddControl(type, attributes);
	}

	private void AddNamespaces(Dictionary<string, bool> imports)
	{
		if (BuildManager.HaveResources)
		{
			imports.Add("System.Resources", value: true);
		}
		PagesSection pagesConfig = PagesConfig;
		if (pagesConfig == null)
		{
			return;
		}
		NamespaceCollection namespaces = pagesConfig.Namespaces;
		if (namespaces == null || namespaces.Count == 0)
		{
			return;
		}
		foreach (NamespaceInfo item in namespaces)
		{
			string @namespace = item.Namespace;
			if (!imports.ContainsKey(@namespace))
			{
				imports.Add(@namespace, value: true);
			}
		}
	}

	internal void RegisterCustomControl(string tagPrefix, string tagName, string src)
	{
		string strA = null;
		bool flag = false;
		VirtualFile virtualFile = null;
		VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
		VirtualPath virtualPath = new VirtualPath(src, BaseVirtualDir);
		string virtualPath2 = virtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, virtualPath.Absolute);
		if (virtualPathProvider.FileExists(virtualPath2))
		{
			flag = true;
			virtualFile = virtualPathProvider.GetFile(virtualPath2);
			if (virtualFile != null)
			{
				strA = MapPath(virtualFile.VirtualPath);
			}
		}
		if (!flag)
		{
			ThrowParseFileNotFound(src);
		}
		if (string.Compare(strA, inputFile, StringComparison.Ordinal) == 0)
		{
			return;
		}
		string virtualPath3 = virtualFile.VirtualPath;
		try
		{
			RegisterTagName(tagPrefix + ":" + tagName);
			RootBuilder.Foundry.RegisterFoundry(tagPrefix, tagName, virtualPath3);
			AddDependency(virtualPath3);
		}
		catch (ParseException ex)
		{
			if (this is UserControlParser)
			{
				throw new ParseException(base.Location, ex.Message, ex);
			}
			throw;
		}
	}

	internal void RegisterNamespace(string tagPrefix, string ns, string assembly)
	{
		AddImport(ns);
		Assembly assembly2 = null;
		if (assembly != null && assembly.Length > 0)
		{
			assembly2 = AddAssemblyByName(assembly);
		}
		RootBuilder.Foundry.RegisterFoundry(tagPrefix, assembly2, ns);
	}

	internal virtual void HandleOptions(object obj)
	{
	}

	internal static string GetOneKey(IDictionary tbl)
	{
		IEnumerator enumerator = tbl.Keys.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current.ToString();
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	internal virtual void AddDirective(string directive, IDictionary atts)
	{
		PageParserFilter pageParserFilter = PageParserFilter;
		if (string.Compare(directive, DefaultDirectiveName, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			bool flag = allowedMainDirectives > 0;
			if (mainAttributes != null && !flag)
			{
				ThrowParseException("Only 1 " + DefaultDirectiveName + " is allowed");
			}
			allowedMainDirectives--;
			if (mainAttributes == null)
			{
				pageParserFilter?.PreprocessDirective(directive.ToLower(Helpers.InvariantCulture), atts);
				mainAttributes = atts;
				ProcessMainAttributes(mainAttributes);
			}
			return;
		}
		pageParserFilter?.PreprocessDirective(directive.ToLower(Helpers.InvariantCulture), atts);
		if (string.Compare("Assembly", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			string @string = BaseParser.GetString(atts, "Name", null);
			string string2 = BaseParser.GetString(atts, "Src", null);
			if (atts.Count > 0)
			{
				ThrowParseException("Attribute " + GetOneKey(atts) + " unknown.");
			}
			if (@string == null && string2 == null)
			{
				ThrowParseException("You gotta specify Src or Name");
			}
			if (@string != null && string2 != null)
			{
				ThrowParseException("Src and Name cannot be used together");
			}
			if (@string != null)
			{
				AddAssemblyByName(@string);
			}
			else
			{
				GetAssemblyFromSource(string2);
			}
		}
		else if (string.Compare("Import", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			string string3 = BaseParser.GetString(atts, "Namespace", null);
			if (atts.Count > 0)
			{
				ThrowParseException("Attribute " + GetOneKey(atts) + " unknown.");
			}
			AddImport(string3);
		}
		else if (string.Compare("Implements", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			string string4 = BaseParser.GetString(atts, "Interface", "");
			if (atts.Count > 0)
			{
				ThrowParseException("Attribute " + GetOneKey(atts) + " unknown.");
			}
			Type type = LoadType(string4);
			if (type == null)
			{
				ThrowParseException("Cannot find type " + string4);
			}
			if (!type.IsInterface)
			{
				ThrowParseException(string.Concat(type, " is not an interface"));
			}
			AddInterface(type.FullName);
		}
		else if (string.Compare("OutputCache", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			HttpContext.Current.Response?.Cache.SetValidUntilExpires(validUntilExpires: true);
			output_cache = true;
			ProcessOutputCacheAttributes(atts);
		}
		else
		{
			ThrowParseException("Unknown directive: " + directive);
		}
	}

	internal virtual void ProcessOutputCacheAttributes(IDictionary atts)
	{
		if (atts["Duration"] == null)
		{
			ThrowParseException("The directive is missing a 'duration' attribute.");
		}
		if (atts["VaryByParam"] == null && atts["VaryByControl"] == null)
		{
			ThrowParseException("This directive is missing 'VaryByParam' or 'VaryByControl' attribute, which should be set to \"none\", \"*\", or a list of name/value pairs.");
		}
		foreach (DictionaryEntry att in atts)
		{
			string text = (string)att.Key;
			if (text == null)
			{
				continue;
			}
			switch (text.ToLower(Helpers.InvariantCulture))
			{
			case "duration":
				oc_duration = int.Parse((string)att.Value);
				if (oc_duration < 1)
				{
					ThrowParseException("The 'duration' attribute must be set to a positive integer value");
				}
				continue;
			case "sqldependency":
				oc_sqldependency = (string)att.Value;
				continue;
			case "nostore":
				try
				{
					oc_nostore = bool.Parse((string)att.Value);
					oc_parsed_params |= OutputCacheParsedParams.NoStore;
				}
				catch
				{
					ThrowParseException("The 'NoStore' attribute is case sensitive and must be set to 'true' or 'false'.");
				}
				continue;
			case "cacheprofile":
				oc_cacheprofile = (string)att.Value;
				oc_parsed_params |= OutputCacheParsedParams.CacheProfile;
				continue;
			case "varybycontentencodings":
				oc_content_encodings = (string)att.Value;
				oc_parsed_params |= OutputCacheParsedParams.VaryByContentEncodings;
				continue;
			case "varybyparam":
				oc_param = (string)att.Value;
				if (string.Compare(oc_param, "none", ignoreCase: true, Helpers.InvariantCulture) == 0)
				{
					oc_param = null;
				}
				continue;
			case "varybyheader":
				oc_header = (string)att.Value;
				oc_parsed_params |= OutputCacheParsedParams.VaryByHeader;
				continue;
			case "varybycustom":
				oc_custom = (string)att.Value;
				oc_parsed_params |= OutputCacheParsedParams.VaryByCustom;
				continue;
			case "location":
				if (this is PageParser)
				{
					try
					{
						oc_location = (OutputCacheLocation)Enum.Parse(typeof(OutputCacheLocation), (string)att.Value, ignoreCase: true);
						oc_parsed_params |= OutputCacheParsedParams.Location;
					}
					catch
					{
						ThrowParseException("The 'location' attribute is case sensitive and must be one of the following values: Any, Client, Downstream, Server, None, ServerAndClient.");
					}
					continue;
				}
				break;
			case "varybycontrol":
				oc_controls = (string)att.Value;
				oc_parsed_params |= OutputCacheParsedParams.VaryByControl;
				continue;
			case "shared":
				if (!(this is PageParser))
				{
					try
					{
						oc_shared = bool.Parse((string)att.Value);
					}
					catch
					{
						ThrowParseException("The 'shared' attribute is case sensitive and must be set to 'true' or 'false'.");
					}
					continue;
				}
				break;
			}
			ThrowParseException("The '" + text + "' attribute is not supported by the 'Outputcache' directive.");
		}
	}

	internal Type LoadType(string typeName)
	{
		Type type = HttpApplication.LoadType(typeName);
		if (type == null)
		{
			return null;
		}
		Assembly assembly = type.Assembly;
		string directoryName = Path.GetDirectoryName(assembly.Location);
		bool flag = true;
		if (directoryName == HttpApplication.BinDirectory)
		{
			flag = false;
		}
		if (flag)
		{
			AddAssembly(assembly, fullPath: true);
		}
		return type;
	}

	internal virtual void AddInterface(string iface)
	{
		if (interfaces == null)
		{
			interfaces = new List<string>();
		}
		if (!interfaces.Contains(iface))
		{
			interfaces.Add(iface);
		}
	}

	internal virtual void AddImport(string namesp)
	{
		if (namesp != null && namesp.Length != 0)
		{
			if (imports == null)
			{
				imports = new Dictionary<string, bool>(StringComparer.Ordinal);
			}
			if (!imports.ContainsKey(namesp))
			{
				imports.Add(namesp, value: true);
				AddAssemblyForNamespace(namesp);
			}
		}
	}

	private void AddAssemblyForNamespace(string namesp)
	{
		if (binDirAssemblies == null)
		{
			binDirAssemblies = HttpApplication.BinDirectoryAssemblies;
		}
		if (binDirAssemblies.Length == 0)
		{
			return;
		}
		if (namespacesCache == null)
		{
			namespacesCache = new Dictionary<string, bool>();
		}
		else if (namespacesCache.ContainsKey(namesp))
		{
			return;
		}
		Assembly[] array = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly asm in array)
		{
			if (FindNamespaceInAssembly(asm, namesp))
			{
				return;
			}
		}
		IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
		if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
		{
			foreach (Assembly item in topLevelAssemblies)
			{
				if (FindNamespaceInAssembly(item, namesp))
				{
					return;
				}
			}
		}
		string[] array2 = binDirAssemblies;
		for (int i = 0; i < array2.Length; i++)
		{
			Assembly asm3 = Assembly.LoadFrom(array2[i]);
			if (FindNamespaceInAssembly(asm3, namesp))
			{
				break;
			}
		}
	}

	private bool FindNamespaceInAssembly(Assembly asm, string namesp)
	{
		Type[] types;
		try
		{
			types = asm.GetTypes();
		}
		catch (ReflectionTypeLoadException)
		{
			return false;
		}
		Type[] array = types;
		for (int i = 0; i < array.Length; i++)
		{
			if (string.Compare(array[i].Namespace, namesp, StringComparison.Ordinal) == 0)
			{
				namespacesCache.Add(namesp, value: true);
				AddAssembly(asm, fullPath: true);
				return true;
			}
		}
		return false;
	}

	internal virtual void AddSourceDependency(string filename)
	{
		if (dependencies != null && dependencies.Contains(filename))
		{
			ThrowParseException("Circular file references are not allowed. File: " + filename);
		}
		AddDependency(filename);
	}

	internal virtual void AddDependency(string filename)
	{
		AddDependency(filename, combinePaths: true);
	}

	internal virtual void AddDependency(string filename, bool combinePaths)
	{
		if (!string.IsNullOrEmpty(filename))
		{
			if (dependencies == null)
			{
				dependencies = new List<string>();
			}
			if (combinePaths)
			{
				filename = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, filename);
			}
			if (!dependencies.Contains(filename))
			{
				dependencies.Add(filename);
			}
		}
	}

	internal virtual void AddAssembly(Assembly assembly, bool fullPath)
	{
		if (assembly == null || assembly.Location == string.Empty)
		{
			return;
		}
		if (anames == null)
		{
			anames = new Dictionary<string, object>();
		}
		string name = assembly.GetName().Name;
		string text = assembly.Location;
		if (fullPath)
		{
			if (!assemblies.Contains(text))
			{
				assemblies.Add(text);
			}
			anames[name] = text;
			anames[text] = assembly;
		}
		else
		{
			if (!assemblies.Contains(name))
			{
				assemblies.Add(name);
			}
			anames[name] = assembly;
		}
	}

	internal virtual Assembly AddAssemblyByFileName(string filename)
	{
		Assembly assembly = null;
		Exception inner = null;
		try
		{
			assembly = Assembly.LoadFrom(filename);
		}
		catch (Exception ex)
		{
			inner = ex;
		}
		if (assembly == null)
		{
			ThrowParseException("Assembly " + filename + " not found", inner);
		}
		AddAssembly(assembly, fullPath: true);
		return assembly;
	}

	internal virtual Assembly AddAssemblyByName(string name)
	{
		if (anames == null)
		{
			anames = new Dictionary<string, object>();
		}
		if (anames.Contains(name))
		{
			object obj = anames[name];
			if (obj is string)
			{
				obj = anames[obj];
			}
			return (Assembly)obj;
		}
		Assembly assembly = null;
		Exception inner = null;
		try
		{
			assembly = Assembly.Load(name);
		}
		catch (Exception ex)
		{
			inner = ex;
		}
		if (assembly == null)
		{
			try
			{
				assembly = Assembly.LoadWithPartialName(name);
			}
			catch (Exception ex2)
			{
				inner = ex2;
			}
		}
		if (assembly == null)
		{
			ThrowParseException("Assembly " + name + " not found", inner);
		}
		AddAssembly(assembly, fullPath: true);
		return assembly;
	}

	internal virtual void ProcessMainAttributes(IDictionary atts)
	{
		directiveLocation = new Location(base.Location);
		CompilationSection compilationConfig = base.CompilationConfig;
		atts.Remove("Description");
		atts.Remove("CodeBehind");
		atts.Remove("AspCompat");
		debug = GetBool(atts, "Debug", compilationConfig.Debug);
		compilerOptions = BaseParser.GetString(atts, "CompilerOptions", string.Empty);
		language = BaseParser.GetString(atts, "Language", "");
		if (language.Length != 0)
		{
			implicitLanguage = false;
		}
		else
		{
			language = compilationConfig.DefaultLanguage;
		}
		strictOn = GetBool(atts, "Strict", compilationConfig.Strict);
		explicitOn = GetBool(atts, "Explicit", compilationConfig.Explicit);
		if (atts.Contains("LinePragmas"))
		{
			linePragmasOn = GetBool(atts, "LinePragmas", deflt: true);
		}
		string @string = BaseParser.GetString(atts, "Inherits", null);
		string text = null;
		src = BaseParser.GetString(atts, "CodeFile", null);
		codeFileBaseClass = BaseParser.GetString(atts, "CodeFileBaseClass", null);
		if (src == null && codeFileBaseClass != null)
		{
			ThrowParseException("The 'CodeFileBaseClass' attribute cannot be used without a 'CodeFile' attribute");
		}
		string string2 = BaseParser.GetString(atts, "Src", null);
		VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
		if (string2 != null)
		{
			string2 = virtualPathProvider.CombineVirtualPaths(BaseVirtualDir, string2);
			GetAssemblyFromSource(string2);
			if (src == null)
			{
				src = string2;
				string2 = MapPath(string2, allowCrossAppMapping: false);
				text = string2;
				if (!File.Exists(text))
				{
					ThrowParseException("File " + src + " not found");
				}
				srcIsLegacy = true;
			}
			else
			{
				string2 = MapPath(string2, allowCrossAppMapping: false);
			}
			AddDependency(string2, combinePaths: false);
		}
		if (!srcIsLegacy && src != null && @string != null)
		{
			src = virtualPathProvider.CombineVirtualPaths(BaseVirtualDir, src);
			text = MapPath(src, allowCrossAppMapping: false);
			if (!virtualPathProvider.FileExists(src))
			{
				ThrowParseException("File " + src + " not found");
			}
			partialClassName = @string;
			compilerOptions = compilerOptions + " \"" + text + "\"";
			if (codeFileBaseClass != null)
			{
				try
				{
					codeFileBaseClassType = LoadType(codeFileBaseClass);
				}
				catch (Exception)
				{
				}
				if (codeFileBaseClassType == null)
				{
					ThrowParseException("Could not load type '{0}'", codeFileBaseClass);
				}
			}
		}
		else if (@string != null)
		{
			SetBaseType(@string);
		}
		if (src != null)
		{
			if (VirtualPathUtility.IsAbsolute(src))
			{
				src = VirtualPathUtility.ToAppRelative(src);
			}
			AddDependency(src, combinePaths: false);
		}
		className = BaseParser.GetString(atts, "ClassName", null);
		if (className != null)
		{
			string[] array = className.Split('.');
			for (int i = 0; i < array.Length; i++)
			{
				if (!CodeGenerator.IsValidLanguageIndependentIdentifier(array[i]))
				{
					ThrowParseException($"'{className}' is not a valid value for attribute 'classname'.");
				}
			}
		}
		if (this is TemplateControlParser)
		{
			metaResourceKey = BaseParser.GetString(atts, "meta:resourcekey", null);
		}
		if (@string != null && (this is PageParser || this is UserControlParser) && atts.Count > 0)
		{
			if (unknownMainAttributes == null)
			{
				unknownMainAttributes = new List<UnknownAttributeDescriptor>();
			}
			{
				foreach (DictionaryEntry att in atts)
				{
					string text2 = att.Key as string;
					string text3 = att.Value as string;
					if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
					{
						CheckUnknownAttribute(text2, text3, @string);
					}
				}
				return;
			}
		}
		if (atts.Count > 0)
		{
			ThrowParseException("Unknown attribute: " + GetOneKey(atts));
		}
	}

	private void RegisterTagName(string tagName)
	{
		if (registeredTagNames == null)
		{
			registeredTagNames = new List<string>();
		}
		if (!registeredTagNames.Contains(tagName))
		{
			registeredTagNames.Add(tagName);
		}
	}

	private void CheckUnknownAttribute(string name, string val, string inherits)
	{
		MemberInfo memberInfo = null;
		bool flag = false;
		string text = name.Trim().ToLower(Helpers.InvariantCulture);
		Type type = codeFileBaseClassType;
		if (type == null)
		{
			type = baseType;
		}
		try
		{
			MemberInfo[] member = type.GetMember(text, MemberTypes.Field | MemberTypes.Property, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			if (member.Length != 0)
			{
				MemberInfo[] array = member;
				foreach (MemberInfo memberInfo2 in array)
				{
					if (memberInfo2 is PropertyInfo)
					{
						memberInfo = memberInfo2;
						break;
					}
				}
				if (memberInfo == null)
				{
					memberInfo = member[0];
				}
			}
			else
			{
				flag = true;
			}
		}
		catch (Exception)
		{
			flag = true;
		}
		if (flag)
		{
			ThrowParseException("Error parsing attribute '{0}': Type '{1}' does not have a public property named '{0}'", text, inherits);
		}
		Type type2 = null;
		if (memberInfo is PropertyInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (!propertyInfo.CanWrite)
			{
				ThrowParseException("Error parsing attribute '{0}': The '{0}' property is read-only and cannot be set.", text);
			}
			type2 = propertyInfo.PropertyType;
		}
		else if (memberInfo is FieldInfo)
		{
			type2 = ((FieldInfo)memberInfo).FieldType;
		}
		else
		{
			ThrowParseException("Could not determine member the kind of '{0}' in base type '{1}", text, inherits);
		}
		TypeConverter converter = TypeDescriptor.GetConverter(type2);
		bool flag2 = true;
		object value = null;
		if (converter == null || !converter.CanConvertFrom(typeof(string)))
		{
			flag2 = false;
		}
		if (flag2)
		{
			try
			{
				value = converter.ConvertFromInvariantString(val);
			}
			catch (Exception)
			{
				flag2 = false;
			}
		}
		if (!flag2)
		{
			ThrowParseException("Error parsing attribute '{0}': Cannot create an object of type '{1}' from its string representation '{2}' for the '{3}' property.", text, type2, val, memberInfo.Name);
		}
		UnknownAttributeDescriptor item = new UnknownAttributeDescriptor(memberInfo, value);
		unknownMainAttributes.Add(item);
	}

	internal void SetBaseType(string type)
	{
		Type type2 = ((type != null && !(type == DefaultBaseTypeName)) ? null : DefaultBaseType);
		if (type2 == null)
		{
			type2 = LoadType(type);
			if (type2 == null)
			{
				ThrowParseException("Cannot find type " + type);
			}
			if (!DefaultBaseType.IsAssignableFrom(type2))
			{
				ThrowParseException("The parent type '" + type + "' does not derive from " + DefaultBaseType);
			}
		}
		PageParserFilter pageParserFilter = PageParserFilter;
		if (pageParserFilter != null && !pageParserFilter.AllowBaseType(type2))
		{
			throw new HttpException(string.Concat("Base type '", type2, "' is not allowed."));
		}
		baseType = type2;
	}

	internal void SetLanguage(string language)
	{
		this.language = language;
		implicitLanguage = false;
	}

	internal void PushIncludeDir(string dir)
	{
		if (includeDirs == null)
		{
			includeDirs = new Stack<string>(1);
		}
		includeDirs.Push(dir);
	}

	internal string PopIncludeDir()
	{
		if (includeDirs == null || includeDirs.Count == 0)
		{
			return null;
		}
		return includeDirs.Pop();
	}

	private Assembly GetAssemblyFromSource(string vpath)
	{
		vpath = UrlUtils.Combine(BaseVirtualDir, vpath);
		string text = MapPath(vpath, allowCrossAppMapping: false);
		if (!File.Exists(text))
		{
			ThrowParseException("File " + vpath + " not found");
		}
		AddSourceDependency(vpath);
		CompilerParameters par;
		string tempdir;
		AssemblyBuilder assemblyBuilder = new AssemblyBuilder(BaseCompiler.CreateProvider(HttpContext.Current, language, out par, out tempdir) ?? throw new HttpException("Cannot find provider for language '" + language + "'."));
		assemblyBuilder.CompilerOptions = par;
		assemblyBuilder.AddAssemblyReference(BuildManager.GetReferencedAssemblies() as List<Assembly>);
		assemblyBuilder.AddCodeFile(text);
		CompilerResults compilerResults = assemblyBuilder.BuildAssembly(new VirtualPath(vpath));
		if (compilerResults.NativeCompilerReturnValue != 0)
		{
			using (StreamReader streamReader = new StreamReader(text))
			{
				throw new CompilationException(text, compilerResults.Errors, streamReader.ReadToEnd());
			}
		}
		AddAssembly(compilerResults.CompiledAssembly, fullPath: true);
		return compilerResults.CompiledAssembly;
	}

	internal string EncodeIdentifier(string value)
	{
		if (value == null || value.Length == 0 || CodeGenerator.IsValidLanguageIndependentIdentifier(value))
		{
			return value;
		}
		StringBuilder stringBuilder = new StringBuilder();
		char c = value[0];
		switch (char.GetUnicodeCategory(c))
		{
		case UnicodeCategory.UppercaseLetter:
		case UnicodeCategory.LowercaseLetter:
		case UnicodeCategory.TitlecaseLetter:
		case UnicodeCategory.ModifierLetter:
		case UnicodeCategory.OtherLetter:
		case UnicodeCategory.LetterNumber:
		case UnicodeCategory.ConnectorPunctuation:
			stringBuilder.Append(c);
			break;
		case UnicodeCategory.DecimalDigitNumber:
			stringBuilder.Append('_');
			stringBuilder.Append(c);
			break;
		default:
			stringBuilder.Append('_');
			break;
		}
		for (int i = 1; i < value.Length; i++)
		{
			c = value[i];
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.Format:
			case UnicodeCategory.ConnectorPunctuation:
				stringBuilder.Append(c);
				break;
			default:
				stringBuilder.Append('_');
				break;
			}
		}
		return stringBuilder.ToString();
	}
}
