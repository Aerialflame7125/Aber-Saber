using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web.Caching;
using System.Web.Util;

namespace System.Web.Compilation;

internal class AppResourcesCompiler
{
	private class TypeResolutionService : ITypeResolutionService
	{
		private List<Assembly> referencedAssemblies;

		private Dictionary<string, Type> mappedTypes;

		public Assembly GetAssembly(AssemblyName name)
		{
			return GetAssembly(name, throwOnError: false);
		}

		public Assembly GetAssembly(AssemblyName name, bool throwOnError)
		{
			try
			{
				return Assembly.Load(name);
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}
			}
			return null;
		}

		public void ReferenceAssembly(AssemblyName name)
		{
			if (referencedAssemblies == null)
			{
				referencedAssemblies = new List<Assembly>();
			}
			Assembly assembly = GetAssembly(name, throwOnError: false);
			if (!(assembly == null) && !referencedAssemblies.Contains(assembly))
			{
				referencedAssemblies.Add(assembly);
			}
		}

		public string GetPathOfAssembly(AssemblyName name)
		{
			if (name == null)
			{
				return null;
			}
			Assembly assembly = GetAssembly(name, throwOnError: false);
			if (assembly == null)
			{
				return null;
			}
			return assembly.Location;
		}

		public Type GetType(string name)
		{
			return GetType(name, throwOnError: false, ignoreCase: false);
		}

		public Type GetType(string name, bool throwOnError)
		{
			return GetType(name, throwOnError, ignoreCase: false);
		}

		public Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(name))
			{
				if (throwOnError)
				{
					throw new ArgumentNullException("name");
				}
				return null;
			}
			int num = name.IndexOf(',');
			Type type = null;
			if (num == -1)
			{
				type = MapType(name, full: false);
				if (type != null)
				{
					return type;
				}
				type = FindInAssemblies(name, ignoreCase);
				if (type == null)
				{
					if (throwOnError)
					{
						throw new InvalidOperationException("Type '" + name + "' is not fully qualified and there are no referenced assemblies.");
					}
					return null;
				}
				return type;
			}
			type = MapType(name, full: true);
			if (type != null)
			{
				return type;
			}
			return Type.GetType(name, throwOnError, ignoreCase);
		}

		private Type MapType(string name, bool full)
		{
			if (mappedTypes == null)
			{
				mappedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
			}
			if (mappedTypes.TryGetValue(name, out var value))
			{
				return value;
			}
			if (!full)
			{
				if (string.Compare(name, "ResXDataNode", StringComparison.Ordinal) == 0)
				{
					return AddMappedType(name, typeof(ResXDataNode));
				}
				if (string.Compare(name, "ResXFileRef", StringComparison.Ordinal) == 0)
				{
					return AddMappedType(name, typeof(ResXFileRef));
				}
				if (string.Compare(name, "ResXNullRef", StringComparison.Ordinal) == 0)
				{
					return AddMappedType(name, typeof(ResXNullRef));
				}
				if (string.Compare(name, "ResXResourceReader", StringComparison.Ordinal) == 0)
				{
					return AddMappedType(name, typeof(ResXResourceReader));
				}
				if (string.Compare(name, "ResXResourceWriter", StringComparison.Ordinal) == 0)
				{
					return AddMappedType(name, typeof(ResXResourceWriter));
				}
				return null;
			}
			if (name.IndexOf("System.Windows.Forms") == -1)
			{
				return null;
			}
			if (name.IndexOf("ResXDataNode", StringComparison.Ordinal) != -1)
			{
				return AddMappedType(name, typeof(ResXDataNode));
			}
			if (name.IndexOf("ResXFileRef", StringComparison.Ordinal) != -1)
			{
				return AddMappedType(name, typeof(ResXFileRef));
			}
			if (name.IndexOf("ResXNullRef", StringComparison.Ordinal) != -1)
			{
				return AddMappedType(name, typeof(ResXNullRef));
			}
			if (name.IndexOf("ResXResourceReader", StringComparison.Ordinal) != -1)
			{
				return AddMappedType(name, typeof(ResXResourceReader));
			}
			if (name.IndexOf("ResXResourceWriter", StringComparison.Ordinal) != -1)
			{
				return AddMappedType(name, typeof(ResXResourceWriter));
			}
			return null;
		}

		private Type AddMappedType(string name, Type type)
		{
			mappedTypes.Add(name, type);
			return type;
		}

		private Type FindInAssemblies(string name, bool ignoreCase)
		{
			Type type = Type.GetType(name, throwOnError: false);
			if (type != null)
			{
				return type;
			}
			if (referencedAssemblies == null || referencedAssemblies.Count == 0)
			{
				return null;
			}
			foreach (Assembly referencedAssembly in referencedAssemblies)
			{
				type = referencedAssembly.GetType(name, throwOnError: false, ignoreCase);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}
	}

	private const string cachePrefix = "@@LocalResourcesAssemblies";

	private bool isGlobal;

	private AppResourceFilesCollection files;

	private string tempDirectory;

	private string virtualPath;

	private Dictionary<string, List<string>> cultureFiles;

	private List<string> defaultCultureFiles;

	private string TempDirectory
	{
		get
		{
			if (tempDirectory != null)
			{
				return tempDirectory;
			}
			return tempDirectory = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}
	}

	public Dictionary<string, List<string>> CultureFiles => cultureFiles;

	public List<string> DefaultCultureFiles => defaultCultureFiles;

	static AppResourcesCompiler()
	{
		if (!BuildManager.IsPrecompiled)
		{
			return;
		}
		string[] binDirectoryAssemblies = HttpApplication.BinDirectoryAssemblies;
		if (binDirectoryAssemblies == null || binDirectoryAssemblies.Length == 0)
		{
			return;
		}
		string[] array = binDirectoryAssemblies;
		foreach (string text in array)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			string fileName = Path.GetFileName(text);
			if (fileName.StartsWith("App_LocalResources.", StringComparison.OrdinalIgnoreCase))
			{
				string precompiledVirtualPath = GetPrecompiledVirtualPath(text);
				if (!string.IsNullOrEmpty(precompiledVirtualPath))
				{
					Assembly assembly = LoadAssembly(text);
					if (!(assembly == null))
					{
						AddAssemblyToCache(precompiledVirtualPath, assembly);
					}
				}
			}
			else if (string.Compare(fileName, "App_GlobalResources.dll", StringComparison.OrdinalIgnoreCase) == 0)
			{
				Assembly assembly = LoadAssembly(text);
				if (!(assembly == null))
				{
					HttpContext.AppGlobalResourcesAssembly = assembly;
				}
			}
		}
	}

	public AppResourcesCompiler(HttpContext context)
	{
		isGlobal = true;
		files = new AppResourceFilesCollection(context);
		cultureFiles = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
	}

	public AppResourcesCompiler(string virtualPath)
	{
		this.virtualPath = virtualPath;
		isGlobal = false;
		files = new AppResourceFilesCollection(HttpContext.Current.Request.MapPath(virtualPath));
		cultureFiles = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
	}

	private static Assembly LoadAssembly(string asmPath)
	{
		try
		{
			return Assembly.LoadFrom(asmPath);
		}
		catch (BadImageFormatException)
		{
			return null;
		}
	}

	private static string GetPrecompiledVirtualPath(string asmPath)
	{
		string text = Path.ChangeExtension(asmPath, ".compiled");
		if (!File.Exists(text))
		{
			return null;
		}
		string text2 = new PreservationFile(text).VirtualPath;
		if (string.IsNullOrEmpty(text2))
		{
			return "/";
		}
		if (text2.EndsWith("/App_LocalResources/", StringComparison.OrdinalIgnoreCase))
		{
			text2 = text2.Substring(0, text2.Length - 19);
		}
		return text2;
	}

	public Assembly Compile()
	{
		files.Collect();
		if (!files.HasFiles)
		{
			return null;
		}
		if (isGlobal)
		{
			return CompileGlobal();
		}
		return CompileLocal();
	}

	private Assembly CompileGlobal()
	{
		if (!(FileUtils.CreateTemporaryFile(TempDirectory, "App_GlobalResources", "dll", OnCreateRandomFile) is string baseAssemblyPath))
		{
			throw new ApplicationException("Failed to create global resources assembly");
		}
		List<string>[] array = GroupGlobalFiles();
		if (array == null || array.Length == 0)
		{
			return null;
		}
		CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
		CodeNamespace codeNamespace = new CodeNamespace(null);
		codeNamespace.Imports.Add(new CodeNamespaceImport("System"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Globalization"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Reflection"));
		codeNamespace.Imports.Add(new CodeNamespaceImport("System.Resources"));
		codeCompileUnit.Namespaces.Add(codeNamespace);
		AppResourcesAssemblyBuilder appResourcesAssemblyBuilder = new AppResourcesAssemblyBuilder("App_GlobalResources", baseAssemblyPath, this);
		CodeDomProvider provider = appResourcesAssemblyBuilder.Provider;
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		List<string>[] array2 = array;
		foreach (List<string> list in array2)
		{
			DomFromResource(list[0], codeCompileUnit, dictionary, provider);
		}
		foreach (KeyValuePair<string, bool> item in dictionary)
		{
			codeCompileUnit.ReferencedAssemblies.Add(item.Key);
		}
		appResourcesAssemblyBuilder.Build(codeCompileUnit);
		HttpContext.AppGlobalResourcesAssembly = appResourcesAssemblyBuilder.MainAssembly;
		return appResourcesAssemblyBuilder.MainAssembly;
	}

	private Assembly CompileLocal()
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			return null;
		}
		Assembly cachedLocalResourcesAssembly = GetCachedLocalResourcesAssembly(virtualPath);
		if (cachedLocalResourcesAssembly != null)
		{
			return cachedLocalResourcesAssembly;
		}
		if (!(FileUtils.CreateTemporaryFile(prefix: (!(virtualPath == "/")) ? ("App_LocalResources" + virtualPath.Replace('/', '.')) : "App_LocalResources.root", tempdir: TempDirectory, extension: "dll", createFile: OnCreateRandomFile) is string baseAssemblyPath))
		{
			throw new ApplicationException("Failed to create local resources assembly");
		}
		foreach (AppResourceFileInfo file in files.Files)
		{
			GetResourceFile(file, local: true);
		}
		AppResourcesAssemblyBuilder appResourcesAssemblyBuilder = new AppResourcesAssemblyBuilder("App_LocalResources", baseAssemblyPath, this);
		appResourcesAssemblyBuilder.Build();
		Assembly mainAssembly = appResourcesAssemblyBuilder.MainAssembly;
		if (mainAssembly != null)
		{
			AddAssemblyToCache(virtualPath, mainAssembly);
		}
		return mainAssembly;
	}

	internal static Assembly GetCachedLocalResourcesAssembly(string path)
	{
		if (!(HttpRuntime.InternalCache["@@LocalResourcesAssemblies"] is Dictionary<string, Assembly> dictionary) || !dictionary.ContainsKey(path))
		{
			return null;
		}
		return dictionary[path];
	}

	private static void AddAssemblyToCache(string path, Assembly asm)
	{
		Cache internalCache = HttpRuntime.InternalCache;
		Dictionary<string, Assembly> dictionary = internalCache["@@LocalResourcesAssemblies"] as Dictionary<string, Assembly>;
		if (dictionary == null)
		{
			dictionary = new Dictionary<string, Assembly>();
		}
		dictionary[path] = asm;
		internalCache.Insert("@@LocalResourcesAssemblies", dictionary);
	}

	private uint CountChars(char c, string s)
	{
		uint num = 0u;
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == c)
			{
				num++;
			}
		}
		return num;
	}

	private string IsFileCultureValid(string fileName)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
		fileNameWithoutExtension = Path.GetExtension(fileNameWithoutExtension);
		if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > 0)
		{
			fileNameWithoutExtension = fileNameWithoutExtension.Substring(1);
			try
			{
				CultureInfo.GetCultureInfo(fileNameWithoutExtension);
				return fileNameWithoutExtension;
			}
			catch
			{
				return null;
			}
		}
		return null;
	}

	private string GetResourceFile(AppResourceFileInfo arfi, bool local)
	{
		string text = ((arfi.Kind != AppResourceFileKind.ResX) ? arfi.Info.FullName : CompileResource(arfi, local));
		if (!string.IsNullOrEmpty(text))
		{
			string text2 = IsFileCultureValid(text);
			List<string> list;
			if (text2 != null)
			{
				if (cultureFiles.ContainsKey(text2))
				{
					list = cultureFiles[text2];
				}
				else
				{
					list = new List<string>(1);
					cultureFiles[text2] = list;
				}
			}
			else
			{
				if (defaultCultureFiles == null)
				{
					defaultCultureFiles = new List<string>();
				}
				list = defaultCultureFiles;
			}
			list.Add(text);
		}
		return text;
	}

	private List<string>[] GroupGlobalFiles()
	{
		List<AppResourceFileInfo> list = files.Files;
		List<List<string>> list2 = new List<List<string>>();
		AppResourcesLengthComparer<List<string>> comparer = new AppResourcesLengthComparer<List<string>>();
		foreach (AppResourceFileInfo item in list)
		{
			if (item.Kind != AppResourceFileKind.ResX && item.Kind != AppResourceFileKind.Resource)
			{
				continue;
			}
			string fullName = item.Info.FullName;
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullName);
			uint num = CountChars('.', fileNameWithoutExtension);
			AppResourceFileInfo appResourceFileInfo = null;
			foreach (AppResourceFileInfo item2 in list)
			{
				if (item2.Seen)
				{
					continue;
				}
				string fullName2 = item2.Info.FullName;
				if (fullName2 == null || fullName == fullName2)
				{
					continue;
				}
				string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(fullName2);
				if (CountChars('.', fileNameWithoutExtension2) == num + 1 && fileNameWithoutExtension2.StartsWith(fileNameWithoutExtension))
				{
					if (IsFileCultureValid(fullName2) != null)
					{
						appResourceFileInfo = item;
						break;
					}
					item2.Seen = true;
				}
			}
			if (appResourceFileInfo != null)
			{
				List<string> list3 = new List<string>();
				list3.Add(GetResourceFile(item, local: false));
				item.Seen = true;
				list2.Add(list3);
			}
		}
		list2.Sort(comparer);
		foreach (List<string> item3 in list2)
		{
			string fullName = item3[0];
			string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(fullName);
			if (fileNameWithoutExtension2.StartsWith("Resources."))
			{
				fileNameWithoutExtension2 = fileNameWithoutExtension2.Substring(10);
			}
			foreach (AppResourceFileInfo item4 in list)
			{
				if (!item4.Seen)
				{
					fullName = item4.Info.FullName;
					if (fullName != null && item4.Info.Name.StartsWith(fileNameWithoutExtension2))
					{
						item3.Add(GetResourceFile(item4, local: false));
						item4.Seen = true;
					}
				}
			}
		}
		foreach (AppResourceFileInfo item5 in list)
		{
			if (!item5.Seen && IsFileCultureValid(item5.Info.FullName) == null)
			{
				List<string> list4 = new List<string>();
				list4.Add(GetResourceFile(item5, local: false));
				list2.Add(list4);
			}
		}
		list2.Sort(comparer);
		return list2.ToArray();
	}

	private void DomFromResource(string resfile, CodeCompileUnit unit, Dictionary<string, bool> assemblies, CodeDomProvider provider)
	{
		if (string.IsNullOrEmpty(resfile))
		{
			return;
		}
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resfile);
		string text = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
		string extension = Path.GetExtension(fileNameWithoutExtension);
		if (extension == null || extension.Length == 0)
		{
			extension = text;
			text = "Resources";
		}
		else
		{
			if (!text.StartsWith("Resources", StringComparison.InvariantCulture))
			{
				text = "Resources." + text;
			}
			extension = extension.Substring(1);
		}
		if (!string.IsNullOrEmpty(extension))
		{
			extension = extension.Replace('.', '_');
		}
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace('.', '_');
		}
		if (!provider.IsValidIdentifier(text) || !provider.IsValidIdentifier(extension))
		{
			throw new ApplicationException("Invalid resource file name.");
		}
		ResourceReader resourceReader;
		try
		{
			resourceReader = new ResourceReader(resfile);
		}
		catch (ArgumentException)
		{
			return;
		}
		CodeNamespace codeNamespace = new CodeNamespace(text);
		CodeTypeDeclaration codeTypeDeclaration = new CodeTypeDeclaration(extension);
		codeTypeDeclaration.IsClass = true;
		codeTypeDeclaration.TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
		CodeMemberField codeMemberField = new CodeMemberField(typeof(CultureInfo), "_culture");
		codeMemberField.InitExpression = new CodePrimitiveExpression(null);
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeTypeDeclaration.Members.Add(codeMemberField);
		codeMemberField = new CodeMemberField(typeof(ResourceManager), "_resourceManager");
		codeMemberField.InitExpression = new CodePrimitiveExpression(null);
		codeMemberField.Attributes = (MemberAttributes)20483;
		codeTypeDeclaration.Members.Add(codeMemberField);
		CodeMemberProperty codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Attributes = (MemberAttributes)24579;
		codeMemberProperty.Name = "ResourceManager";
		codeMemberProperty.HasGet = true;
		codeMemberProperty.Type = new CodeTypeReference(typeof(ResourceManager));
		CodePropertyResourceManagerGet(codeMemberProperty.GetStatements, resfile, extension);
		codeTypeDeclaration.Members.Add(codeMemberProperty);
		codeMemberProperty = new CodeMemberProperty();
		codeMemberProperty.Attributes = (MemberAttributes)24578;
		codeMemberProperty.Attributes = (MemberAttributes)24579;
		codeMemberProperty.Name = "Culture";
		codeMemberProperty.HasGet = true;
		codeMemberProperty.HasSet = true;
		codeMemberProperty.Type = new CodeTypeReference(typeof(CultureInfo));
		CodePropertyGenericGet(codeMemberProperty.GetStatements, "_culture", extension);
		CodePropertyGenericSet(codeMemberProperty.SetStatements, "_culture", extension);
		codeTypeDeclaration.Members.Add(codeMemberProperty);
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		try
		{
			foreach (DictionaryEntry item in resourceReader)
			{
				Type type = item.Value.GetType();
				if (!dictionary.ContainsKey(type.Namespace))
				{
					dictionary[type.Namespace] = true;
				}
				string name = new AssemblyName(type.Assembly.FullName).Name;
				if (!assemblies.ContainsKey(name))
				{
					assemblies[name] = true;
				}
				codeMemberProperty = new CodeMemberProperty();
				codeMemberProperty.Attributes = (MemberAttributes)24579;
				codeMemberProperty.Name = SanitizeResourceName(provider, (string)item.Key);
				codeMemberProperty.HasGet = true;
				CodePropertyResourceGet(codeMemberProperty.GetStatements, (string)item.Key, type, extension);
				codeMemberProperty.Type = new CodeTypeReference(type);
				codeTypeDeclaration.Members.Add(codeMemberProperty);
			}
		}
		catch (Exception innerException)
		{
			throw new ApplicationException("Failed to compile global resources.", innerException);
		}
		foreach (KeyValuePair<string, bool> item2 in dictionary)
		{
			codeNamespace.Imports.Add(new CodeNamespaceImport(item2.Key));
		}
		codeNamespace.Types.Add(codeTypeDeclaration);
		unit.Namespaces.Add(codeNamespace);
	}

	private static bool is_identifier_start_character(int c)
	{
		if ((c < 97 || c > 122) && (c < 65 || c > 90) && c != 95)
		{
			return char.IsLetter((char)c);
		}
		return true;
	}

	private static bool is_identifier_part_character(char c)
	{
		if (c >= 'a' && c <= 'z')
		{
			return true;
		}
		if (c >= 'A' && c <= 'Z')
		{
			return true;
		}
		switch (c)
		{
		case '0':
		case '1':
		case '2':
		case '3':
		case '4':
		case '5':
		case '6':
		case '7':
		case '8':
		case '9':
		case '_':
			return true;
		default:
			if (c < '\u0080')
			{
				return false;
			}
			if (!char.IsLetter(c))
			{
				return char.GetUnicodeCategory(c) == UnicodeCategory.ConnectorPunctuation;
			}
			return true;
		}
	}

	private string SanitizeResourceName(CodeDomProvider provider, string name)
	{
		if (provider.IsValidIdentifier(name))
		{
			return provider.CreateEscapedIdentifier(name);
		}
		StringBuilder stringBuilder = new StringBuilder();
		char c = name[0];
		if (is_identifier_start_character(c))
		{
			stringBuilder.Append(c);
		}
		else
		{
			stringBuilder.Append('_');
			if (c >= '0' && c <= '9')
			{
				stringBuilder.Append(c);
			}
		}
		for (int i = 1; i < name.Length; i++)
		{
			c = name[i];
			if (is_identifier_part_character(c))
			{
				stringBuilder.Append(c);
			}
			else
			{
				stringBuilder.Append('_');
			}
		}
		return provider.CreateEscapedIdentifier(stringBuilder.ToString());
	}

	private CodeObjectCreateExpression NewResourceManager(string name, string typename)
	{
		CodeExpression codeExpression = new CodePrimitiveExpression(name);
		CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression(new CodeTypeOfExpression(new CodeTypeReference(typename)), "Assembly");
		return new CodeObjectCreateExpression("System.Resources.ResourceManager", codeExpression, codePropertyReferenceExpression);
	}

	private void CodePropertyResourceManagerGet(CodeStatementCollection csc, string resfile, string typename)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resfile);
		CodeExpression codeExpression = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), "_resourceManager");
		CodeStatement value = new CodeConditionStatement(new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.IdentityInequality, new CodePrimitiveExpression(null)), new CodeMethodReturnStatement(codeExpression));
		csc.Add(value);
		value = new CodeAssignStatement(codeExpression, NewResourceManager(fileNameWithoutExtension, typename));
		csc.Add(value);
		csc.Add(new CodeMethodReturnStatement(codeExpression));
	}

	private void CodePropertyResourceGet(CodeStatementCollection csc, string resname, Type restype, string typename)
	{
		CodeStatement value = new CodeVariableDeclarationStatement(typeof(ResourceManager), "rm", new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(typename), "ResourceManager"));
		csc.Add(value);
		value = new CodeConditionStatement(new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("rm"), CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(null)), new CodeMethodReturnStatement(new CodePrimitiveExpression(null)));
		csc.Add(value);
		bool flag = restype == typeof(string);
		CodeExpression codeExpression = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("rm"), flag ? "GetString" : "GetObject", new CodePrimitiveExpression(resname), new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), "_culture"));
		value = new CodeVariableDeclarationStatement(restype, "obj", flag ? codeExpression : new CodeCastExpression(restype, codeExpression));
		csc.Add(value);
		csc.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("obj")));
	}

	private void CodePropertyGenericGet(CodeStatementCollection csc, string field, string typename)
	{
		csc.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), field)));
	}

	private void CodePropertyGenericSet(CodeStatementCollection csc, string field, string typename)
	{
		csc.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(typename), field), new CodeVariableReferenceExpression("value")));
	}

	private string CompileResource(AppResourceFileInfo arfi, bool local)
	{
		string fullName = arfi.Info.FullName;
		string text = Path.GetFileNameWithoutExtension(fullName) + ".resources";
		if (!local)
		{
			text = "Resources." + text;
		}
		string text2 = Path.Combine(TempDirectory, text);
		FileStream fileStream = null;
		FileStream fileStream2 = null;
		IResourceReader resourceReader = null;
		ResourceWriter resourceWriter = null;
		try
		{
			fileStream = new FileStream(fullName, FileMode.Open, FileAccess.Read);
			fileStream2 = new FileStream(text2, FileMode.Create, FileAccess.Write);
			resourceReader = GetReaderForKind(arfi.Kind, fileStream, fullName);
			resourceWriter = new ResourceWriter(fileStream2);
			foreach (DictionaryEntry item in resourceReader)
			{
				object value = item.Value;
				if (value is string)
				{
					resourceWriter.AddResource((string)item.Key, (string)value);
				}
				else
				{
					resourceWriter.AddResource((string)item.Key, value);
				}
			}
			return text2;
		}
		catch (Exception innerException)
		{
			throw new HttpException("Failed to compile resource file", innerException);
		}
		finally
		{
			resourceReader?.Dispose();
			fileStream?.Dispose();
			resourceWriter?.Dispose();
			fileStream2?.Dispose();
		}
	}

	private IResourceReader GetReaderForKind(AppResourceFileKind kind, Stream stream, string path)
	{
		switch (kind)
		{
		case AppResourceFileKind.ResX:
		{
			ResXResourceReader resXResourceReader = new ResXResourceReader(stream, new TypeResolutionService());
			if (!string.IsNullOrEmpty(path))
			{
				resXResourceReader.BasePath = Path.GetDirectoryName(path);
			}
			return resXResourceReader;
		}
		case AppResourceFileKind.Resource:
			return new ResourceReader(stream);
		default:
			return null;
		}
	}

	private object OnCreateRandomFile(string path)
	{
		new FileStream(path, FileMode.CreateNew).Close();
		return path;
	}
}
