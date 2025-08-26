using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Provides base functionality for parsing Web handler files.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class SimpleWebHandlerParser
{
	private HttpContext context;

	private string vPath;

	private string physPath;

	private string className;

	private bool debug;

	private string language;

	private string program;

	private bool gotDefault;

	private ArrayList assemblies;

	private ArrayList dependencies;

	private Hashtable anames;

	private string baseDir;

	private string baseVDir;

	private TextReader reader;

	private int appAssemblyIndex = -1;

	private Type cachedType;

	/// <summary>When overridden in a derived class, gets the name of the main directive from a &lt;%@ %&gt; block.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the default directive name.</returns>
	protected abstract string DefaultDirectiveName { get; }

	internal HttpContext Context => context;

	internal string VirtualPath => vPath;

	internal string PhysicalPath => physPath;

	internal string ClassName => className;

	internal bool Debug => debug;

	internal string Language => language;

	internal string Program
	{
		get
		{
			if (program != null)
			{
				return program;
			}
			return string.Empty;
		}
	}

	internal ArrayList Assemblies
	{
		get
		{
			if (appAssemblyIndex != -1)
			{
				object value = assemblies[appAssemblyIndex];
				assemblies.RemoveAt(appAssemblyIndex);
				assemblies.Add(value);
				appAssemblyIndex = -1;
			}
			return assemblies;
		}
	}

	internal ArrayList Dependencies => dependencies;

	internal string BaseDir
	{
		get
		{
			if (baseDir == null)
			{
				baseDir = context.Request.MapPath(BaseVirtualDir);
			}
			return baseDir;
		}
	}

	internal virtual string BaseVirtualDir
	{
		get
		{
			if (baseVDir == null)
			{
				baseVDir = UrlUtils.GetDirectory(context.Request.FilePath);
			}
			return baseVDir;
		}
	}

	private CompilationSection CompilationConfig
	{
		get
		{
			string virtualPath = VirtualPath;
			if (string.IsNullOrEmpty(virtualPath))
			{
				return WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			}
			return WebConfigurationManager.GetSection("system.web/compilation", virtualPath) as CompilationSection;
		}
	}

	internal TextReader Reader
	{
		get
		{
			return reader;
		}
		set
		{
			reader = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SimpleWebHandlerParser" /> class. </summary>
	/// <param name="context">Pass <see langword="null" />. Parameter is now obsolete.</param>
	/// <param name="virtualPath">The path of the current virtual directory.</param>
	/// <param name="physicalPath">Pass <see langword="null" />. Parameter is now obsolete.</param>
	protected SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
		: this(context, virtualPath, physicalPath, null)
	{
	}

	internal SimpleWebHandlerParser(HttpContext context, string virtualPath, string physicalPath, TextReader reader)
	{
		this.reader = reader;
		cachedType = CachingCompiler.GetTypeFromCache(physicalPath);
		if (cachedType != null)
		{
			return;
		}
		if (context != null)
		{
			this.context = context;
		}
		else
		{
			this.context = HttpContext.Current;
		}
		vPath = virtualPath;
		AddDependency(virtualPath);
		if (physicalPath != null && physicalPath.Length > 0)
		{
			physPath = physicalPath;
		}
		else
		{
			HttpRequest httpRequest = ((this.context != null) ? context.Request : null);
			if (httpRequest != null)
			{
				physPath = httpRequest.MapPath(virtualPath);
			}
		}
		assemblies = new ArrayList();
		string assemblyLocation = Context.ApplicationInstance.AssemblyLocation;
		if (assemblyLocation != typeof(TemplateParser).Assembly.Location)
		{
			appAssemblyIndex = assemblies.Add(assemblyLocation);
		}
		bool flag = false;
		foreach (AssemblyInfo assembly in CompilationConfig.Assemblies)
		{
			if (assembly.Assembly == "*")
			{
				flag = true;
			}
			else
			{
				AddAssemblyByName(assembly.Assembly, null);
			}
		}
		if (flag)
		{
			AddAssembliesInBin();
		}
		language = CompilationConfig.DefaultLanguage;
		GetDirectivesAndContent();
	}

	/// <summary>Returns the type for the compiled object from the virtual path.</summary>
	/// <returns>The <see cref="T:System.Type" /> assigned to the virtual path.</returns>
	protected Type GetCompiledTypeFromCache()
	{
		return cachedType;
	}

	private void GetDirectivesAndContent()
	{
		bool flag = false;
		bool flag2 = false;
		StringBuilder stringBuilder = null;
		StringBuilder stringBuilder2 = new StringBuilder();
		StreamReader streamReader = ((reader == null) ? new StreamReader(File.OpenRead(physPath), WebEncoding.FileEncoding) : (reader as StreamReader));
		using (streamReader)
		{
			string text;
			while ((text = streamReader.ReadLine()) != null && cachedType == null)
			{
				int length = text.Length;
				if (length == 0)
				{
					stringBuilder2.Append("\n");
					continue;
				}
				int num = text.IndexOf("<%");
				if (num > -1)
				{
					int num2 = text.IndexOf("%>");
					if (num > 0)
					{
						stringBuilder2.Append(text.Substring(0, num));
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					else
					{
						stringBuilder.Length = 0;
					}
					if (num2 <= -1)
					{
						flag2 = true;
						flag = false;
						stringBuilder.Append(text.Substring(num));
						continue;
					}
					flag = true;
					flag2 = false;
					stringBuilder.Append(text.Substring(num, num2 - num + 2));
					if (num2 < length - 2)
					{
						stringBuilder2.Append(text.Substring(num2 + 2, length - num2 - 2));
					}
				}
				if (flag2)
				{
					int num3 = text.IndexOf("%>");
					if (num3 <= -1)
					{
						stringBuilder.Append(text);
						continue;
					}
					stringBuilder.Append(text.Substring(0, num3 + 2));
					if (num3 < length)
					{
						stringBuilder2.Append(text.Substring(num3 + 2) + "\n");
					}
					flag2 = false;
					flag = true;
				}
				if (flag)
				{
					ParseDirective(stringBuilder.ToString());
					flag = false;
					if (gotDefault)
					{
						cachedType = CachingCompiler.GetTypeFromCache(physPath);
						if (cachedType != null)
						{
							break;
						}
					}
				}
				else
				{
					stringBuilder2.Append(text + "\n");
				}
			}
			stringBuilder = null;
		}
		if (!gotDefault)
		{
			throw new ParseException(null, "No @" + DefaultDirectiveName + " directive found");
		}
		if (cachedType == null)
		{
			program = stringBuilder2.ToString();
		}
	}

	private void TagParsed(ILocation location, TagType tagtype, string tagid, TagAttributes attributes)
	{
		if (tagtype != TagType.Directive)
		{
			throw new ParseException(location, "Unexpected tag");
		}
		if (tagid == null || tagid.Length == 0 || string.Compare(tagid, DefaultDirectiveName, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			AddDefaultDirective(location, attributes);
			return;
		}
		if (string.Compare(tagid, "Assembly", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			AddAssemblyDirective(location, attributes);
			return;
		}
		throw new ParseException(location, "Unexpected directive: " + tagid);
	}

	private void TextParsed(ILocation location, string text)
	{
		if (text.Trim() != "")
		{
			throw new ParseException(location, "Text not allowed here");
		}
	}

	private void ParseError(ILocation location, string message)
	{
		throw new ParseException(location, message);
	}

	private static string GetAndRemove(IDictionary table, string key)
	{
		string result = table[key] as string;
		table.Remove(key);
		return result;
	}

	private void ParseDirective(string line)
	{
		AspParser aspParser;
		using (StringReader input = new StringReader(line))
		{
			aspParser = new AspParser(physPath, input);
		}
		aspParser.Error += ParseError;
		aspParser.TagParsed += TagParsed;
		aspParser.TextParsed += TextParsed;
		aspParser.Parse();
	}

	internal virtual void AddDefaultDirective(ILocation location, TagAttributes attrs)
	{
		CompilationSection compilationConfig = CompilationConfig;
		if (gotDefault)
		{
			throw new ParseException(location, "duplicate " + DefaultDirectiveName + " directive");
		}
		gotDefault = true;
		IDictionary dictionary = attrs.GetDictionary(null);
		className = GetAndRemove(dictionary, "class");
		if (className == null)
		{
			throw new ParseException(null, "No Class attribute found.");
		}
		string andRemove = GetAndRemove(dictionary, "debug");
		if (andRemove != null)
		{
			debug = string.Compare(andRemove, "true", ignoreCase: true, Helpers.InvariantCulture) == 0;
			if (!debug && string.Compare(andRemove, "false", ignoreCase: true, Helpers.InvariantCulture) != 0)
			{
				throw new ParseException(null, "Invalid value for Debug attribute");
			}
		}
		else
		{
			debug = compilationConfig.Debug;
		}
		language = GetAndRemove(dictionary, "language");
		if (language == null)
		{
			language = compilationConfig.DefaultLanguage;
		}
		GetAndRemove(dictionary, "codebehind");
		if (dictionary.Count > 0)
		{
			throw new ParseException(location, "Unrecognized attribute in " + DefaultDirectiveName + " directive");
		}
	}

	internal virtual void AddAssemblyDirective(ILocation location, TagAttributes attrs)
	{
		IDictionary dictionary = attrs.GetDictionary(null);
		string andRemove = GetAndRemove(dictionary, "Name");
		string andRemove2 = GetAndRemove(dictionary, "Src");
		if (andRemove == null && andRemove2 == null)
		{
			throw new ParseException(location, "You gotta specify Src or Name");
		}
		if (andRemove != null && andRemove2 != null)
		{
			throw new ParseException(location, "Src and Name cannot be used together");
		}
		if (andRemove != null)
		{
			AddAssemblyByName(andRemove, location);
		}
		else
		{
			GetAssemblyFromSource(andRemove2, location);
		}
		if (dictionary.Count > 0)
		{
			throw new ParseException(location, "Unrecognized attribute in Assembly directive");
		}
	}

	internal virtual void AddAssembly(Assembly assembly, bool fullPath)
	{
		if (assembly == null)
		{
			throw new ArgumentNullException("assembly");
		}
		if (anames == null)
		{
			anames = new Hashtable();
		}
		string name = assembly.GetName().Name;
		string location = assembly.Location;
		if (fullPath)
		{
			if (!assemblies.Contains(location))
			{
				assemblies.Add(location);
			}
			anames[name] = location;
			anames[location] = assembly;
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

	internal virtual Assembly AddAssemblyByName(string name, ILocation location)
	{
		if (anames == null)
		{
			anames = new Hashtable();
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
		Assembly assembly = LoadAssemblyFromBin(name);
		if (assembly != null)
		{
			AddAssembly(assembly, fullPath: true);
			return assembly;
		}
		Exception inner = null;
		try
		{
			assembly = Assembly.LoadWithPartialName(name);
		}
		catch (Exception ex)
		{
			inner = ex;
			assembly = null;
		}
		if (assembly == null)
		{
			throw new ParseException(location, $"Assembly '{name}' not found", inner);
		}
		AddAssembly(assembly, fullPath: true);
		return assembly;
	}

	private void AddAssembliesInBin()
	{
		string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
		foreach (string text in binDirectoryAssemblies)
		{
			Exception ex = null;
			try
			{
				Assembly assembly = Assembly.LoadFrom(text);
				AddAssembly(assembly, fullPath: true);
			}
			catch (FileLoadException ex2)
			{
				ex = ex2;
			}
			catch (BadImageFormatException ex3)
			{
				ex = ex3;
			}
			catch (Exception innerException)
			{
				throw new Exception("Error while loading " + text, innerException);
			}
			if (ex != null && RuntimeHelpers.DebuggingEnabled)
			{
				Console.WriteLine("**** DEBUG MODE *****");
				Console.WriteLine("Bad assembly found in bin/. Exception (ignored):");
				Console.WriteLine(ex);
			}
		}
	}

	private Assembly LoadAssemblyFromBin(string name)
	{
		string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
		foreach (string text in binDirectoryAssemblies)
		{
			if (!(Path.ChangeExtension(Path.GetFileName(text), null) != name))
			{
				return Assembly.LoadFrom(text);
			}
		}
		return null;
	}

	private Assembly GetAssemblyFromSource(string vpath, ILocation location)
	{
		vpath = UrlUtils.Combine(BaseVirtualDir, vpath);
		string text = context.Request.MapPath(vpath);
		if (!File.Exists(text))
		{
			throw new ParseException(location, "File " + vpath + " not found");
		}
		AddDependency(vpath);
		CompilerResults compilerResults = CachingCompiler.Compile(language, text, text, assemblies);
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

	internal Type GetTypeFromBin(string tname)
	{
		if (tname == null || tname.Length == 0)
		{
			throw new ArgumentNullException("tname");
		}
		Type type = null;
		int num = tname.IndexOf(',');
		string text;
		string text2;
		if (num != -1)
		{
			text = tname.Substring(0, num).Trim();
			text2 = tname.Substring(num + 1).Trim();
		}
		else
		{
			text = tname;
			text2 = null;
		}
		Type type2 = null;
		Assembly assembly = null;
		if (text2 != null)
		{
			assembly = Assembly.Load(text2);
			if (assembly != null)
			{
				type2 = assembly.GetType(text, throwOnError: false);
			}
			if (type2 != null)
			{
				return type2;
			}
		}
		IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
		if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
		{
			foreach (Assembly item in topLevelAssemblies)
			{
				type2 = item.GetType(text, throwOnError: false);
				if (type2 != null)
				{
					if (type != null)
					{
						throw new HttpException($"Type {text} is not unique.");
					}
					type = type2;
				}
			}
		}
		string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
		foreach (string assemblyFile in binDirectoryAssemblies)
		{
			try
			{
				assembly = Assembly.LoadFrom(assemblyFile);
			}
			catch (FileLoadException)
			{
				continue;
			}
			catch (BadImageFormatException)
			{
				continue;
			}
			type2 = assembly.GetType(text, throwOnError: false);
			if (type2 != null)
			{
				if (type != null)
				{
					throw new HttpException($"Type {text} is not unique.");
				}
				type = type2;
			}
		}
		if (type == null)
		{
			throw new HttpException($"Type {text} not found.");
		}
		return type;
	}

	internal virtual void AddDependency(string filename)
	{
		if (dependencies == null)
		{
			dependencies = new ArrayList();
		}
		if (!dependencies.Contains(filename))
		{
			dependencies.Add(filename);
		}
	}
}
