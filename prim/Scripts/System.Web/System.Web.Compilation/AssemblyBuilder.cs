using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation;

/// <summary>Provides a container for building an assembly from one or more virtual paths within an ASP.NET project.</summary>
public class AssemblyBuilder
{
	private struct CodeUnit
	{
		public readonly BuildProvider BuildProvider;

		public readonly CodeCompileUnit Unit;

		public CodeUnit(BuildProvider bp, CodeCompileUnit unit)
		{
			BuildProvider = bp;
			Unit = unit;
		}
	}

	private interface ICodePragmaGenerator
	{
		int ReserveSpace(string filename);

		void DecorateFile(string path, string filename, MD5 checksum, Encoding enc);
	}

	private class CSharpCodePragmaGenerator : ICodePragmaGenerator
	{
		private const int pragmaChecksumStaticCount = 23;

		private const int pragmaLineStaticCount = 8;

		private const int md5ChecksumCount = 32;

		private string QuoteSnippetString(string value)
		{
			string text = value.Replace("\\", "\\\\");
			text = text.Replace("\"", "\\\"");
			text = text.Replace("\t", "\\t");
			text = text.Replace("\r", "\\r");
			text = text.Replace("\n", "\\n");
			return "\"" + text + "\"";
		}

		private string ChecksumToHex(MD5 checksum)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] hash = checksum.Hash;
			foreach (byte b in hash)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		public int ReserveSpace(string filename)
		{
			return 63 + QuoteSnippetString(filename).Length * 2 + Environment.NewLine.Length * 3 + BaseCompiler.HashMD5.ToString("B").Length;
		}

		public void DecorateFile(string path, string filename, MD5 checksum, Encoding enc)
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("#pragma checksum {0} \"{1}\" \"{2}\"{3}{3}", QuoteSnippetString(filename), BaseCompiler.HashMD5.ToString("B"), ChecksumToHex(checksum), newLine);
			stringBuilder.AppendFormat("#line 1 {0}{1}", QuoteSnippetString(filename), newLine);
			byte[] bytes = enc.GetBytes(stringBuilder.ToString());
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write))
			{
				fileStream.Seek(enc.GetPreamble().Length, SeekOrigin.Begin);
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = null;
				stringBuilder.Length = 0;
				stringBuilder.AppendFormat("{0}#line default{0}#line hidden{0}", newLine);
				bytes = Encoding.UTF8.GetBytes(stringBuilder.ToString());
				fileStream.Seek(0L, SeekOrigin.End);
				fileStream.Write(bytes, 0, bytes.Length);
			}
			stringBuilder = null;
			bytes = null;
		}
	}

	private class VBCodePragmaGenerator : ICodePragmaGenerator
	{
		private const int pragmaExternalSourceCount = 21;

		public int ReserveSpace(string filename)
		{
			return 21 + filename.Length + Environment.NewLine.Length;
		}

		public void DecorateFile(string path, string filename, MD5 checksum, Encoding enc)
		{
			string newLine = Environment.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("#ExternalSource(\"{0}\",1){1}", filename, newLine);
			byte[] bytes = enc.GetBytes(stringBuilder.ToString());
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Write))
			{
				fileStream.Seek(enc.GetPreamble().Length, SeekOrigin.Begin);
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = null;
				stringBuilder.Length = 0;
				stringBuilder.AppendFormat("{0}#End ExternalSource{0}", newLine);
				bytes = enc.GetBytes(stringBuilder.ToString());
				fileStream.Seek(0L, SeekOrigin.End);
				fileStream.Write(bytes, 0, bytes.Length);
			}
			stringBuilder = null;
			bytes = null;
		}
	}

	private const string DEFAULT_ASSEMBLY_BASE_NAME = "App_Web_";

	private const int COPY_BUFFER_SIZE = 8192;

	private static bool KeepFiles = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;

	private CodeDomProvider provider;

	private CompilerParameters parameters;

	private Dictionary<string, bool> code_files;

	private Dictionary<string, List<CompileUnitPartialType>> partial_types;

	private Dictionary<string, BuildProvider> path_to_buildprovider;

	private List<CodeUnit> units;

	private List<string> source_files;

	private List<Assembly> referenced_assemblies;

	private Dictionary<string, string> resource_files;

	private TempFileCollection temp_files;

	private string outputFilesPrefix;

	private string outputAssemblyPrefix;

	private string outputAssemblyName;

	internal string OutputFilesPrefix
	{
		get
		{
			if (outputFilesPrefix == null)
			{
				outputFilesPrefix = "App_Web_";
			}
			return outputFilesPrefix;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				outputFilesPrefix = "App_Web_";
			}
			else
			{
				outputFilesPrefix = value;
			}
			outputAssemblyPrefix = null;
			outputAssemblyName = null;
		}
	}

	internal string OutputAssemblyPrefix
	{
		get
		{
			if (outputAssemblyPrefix == null)
			{
				string basePath = temp_files.BasePath;
				string fileName = Path.GetFileName(basePath);
				string directoryName = Path.GetDirectoryName(basePath);
				outputAssemblyPrefix = Path.Combine(directoryName, OutputFilesPrefix + fileName);
			}
			return outputAssemblyPrefix;
		}
	}

	internal string OutputAssemblyName
	{
		get
		{
			if (outputAssemblyName == null)
			{
				outputAssemblyName = OutputAssemblyPrefix + ".dll";
			}
			return outputAssemblyName;
		}
	}

	internal TempFileCollection TempFiles => temp_files;

	internal CompilerParameters CompilerOptions
	{
		get
		{
			return parameters;
		}
		set
		{
			parameters = value;
		}
	}

	internal Dictionary<string, List<CompileUnitPartialType>> PartialTypes
	{
		get
		{
			if (partial_types == null)
			{
				partial_types = new Dictionary<string, List<CompileUnitPartialType>>();
			}
			return partial_types;
		}
	}

	private Dictionary<string, bool> CodeFiles
	{
		get
		{
			if (code_files == null)
			{
				code_files = new Dictionary<string, bool>();
			}
			return code_files;
		}
	}

	private List<string> SourceFiles
	{
		get
		{
			if (source_files == null)
			{
				source_files = new List<string>();
			}
			return source_files;
		}
	}

	private Dictionary<string, string> ResourceFiles
	{
		get
		{
			if (resource_files == null)
			{
				resource_files = new Dictionary<string, string>();
			}
			return resource_files;
		}
	}

	/// <summary>Gets the compiler used to build source code into an assembly.</summary>
	/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation used for compiling source code contributed by each build provider into an assembly.</returns>
	public CodeDomProvider CodeDomProvider => provider;

	private List<Assembly> ReferencedAssemblies
	{
		get
		{
			if (referenced_assemblies == null)
			{
				referenced_assemblies = new List<Assembly>();
			}
			return referenced_assemblies;
		}
	}

	internal AssemblyBuilder(CodeDomProvider provider)
		: this(null, provider, "App_Web_")
	{
	}

	internal AssemblyBuilder(CodeDomProvider provider, string assemblyBaseName)
		: this(null, provider, assemblyBaseName)
	{
	}

	internal AssemblyBuilder(VirtualPath virtualPath, CodeDomProvider provider)
		: this(virtualPath, provider, "App_Web_")
	{
	}

	internal AssemblyBuilder(VirtualPath virtualPath, CodeDomProvider provider, string assemblyBaseName)
	{
		this.provider = provider;
		outputFilesPrefix = assemblyBaseName ?? "App_Web_";
		units = new List<CodeUnit>();
		CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
		string text = compilationSection.TempDirectory;
		if (string.IsNullOrEmpty(text))
		{
			text = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		}
		if (!KeepFiles)
		{
			KeepFiles = compilationSection.Debug;
		}
		temp_files = new TempFileCollection(text, KeepFiles);
	}

	private CodeUnit[] GetUnitsAsArray()
	{
		CodeUnit[] array = new CodeUnit[units.Count];
		units.CopyTo(array, 0);
		return array;
	}

	internal BuildProvider GetBuildProviderForPhysicalFilePath(string path)
	{
		if (string.IsNullOrEmpty(path) || path_to_buildprovider == null || path_to_buildprovider.Count == 0)
		{
			return null;
		}
		if (path_to_buildprovider.TryGetValue(path, out var value))
		{
			return value;
		}
		return null;
	}

	/// <summary>Adds an assembly that is referenced by source code generated for a file.</summary>
	/// <param name="a">An assembly referenced by a code compile unit or source file included in the assembly compilation.</param>
	public void AddAssemblyReference(Assembly a)
	{
		if (a == null)
		{
			throw new ArgumentNullException("a");
		}
		List<Assembly> referencedAssemblies = ReferencedAssemblies;
		if (!referencedAssemblies.Contains(a))
		{
			referencedAssemblies.Add(a);
		}
	}

	internal void AddAssemblyReference(string assemblyLocation)
	{
		try
		{
			Assembly assembly = Assembly.LoadFrom(assemblyLocation);
			if (!(assembly == null))
			{
				AddAssemblyReference(assembly);
			}
		}
		catch
		{
		}
	}

	internal void AddAssemblyReference(ICollection asmcoll)
	{
		if (asmcoll == null || asmcoll.Count == 0)
		{
			return;
		}
		foreach (object item in asmcoll)
		{
			Assembly assembly = item as Assembly;
			if (!(assembly == null))
			{
				AddAssemblyReference(assembly);
			}
		}
	}

	internal void AddAssemblyReference(List<Assembly> asmlist)
	{
		if (asmlist == null)
		{
			return;
		}
		foreach (Assembly item in asmlist)
		{
			if (!(item == null))
			{
				AddAssemblyReference(item);
			}
		}
	}

	internal void AddCodeCompileUnit(CodeCompileUnit compileUnit)
	{
		if (compileUnit == null)
		{
			throw new ArgumentNullException("compileUnit");
		}
		units.Add(CheckForPartialTypes(new CodeUnit(null, compileUnit)));
	}

	/// <summary>Adds source code for the assembly in the form of a CodeDOM graph.</summary>
	/// <param name="buildProvider">The build provider generating <paramref name="compileUnit" />.</param>
	/// <param name="compileUnit">The code compile unit to include in the assembly compilation.</param>
	public void AddCodeCompileUnit(BuildProvider buildProvider, CodeCompileUnit compileUnit)
	{
		if (buildProvider == null)
		{
			throw new ArgumentNullException("buildProvider");
		}
		if (compileUnit == null)
		{
			throw new ArgumentNullException("compileUnit");
		}
		units.Add(CheckForPartialTypes(new CodeUnit(buildProvider, compileUnit)));
	}

	private void AddPathToBuilderMap(string path, BuildProvider bp)
	{
		if (path_to_buildprovider == null)
		{
			path_to_buildprovider = new Dictionary<string, BuildProvider>();
		}
		if (!path_to_buildprovider.ContainsKey(path))
		{
			path_to_buildprovider.Add(path, bp);
		}
	}

	/// <summary>Allows a build provider to create a temporary source file, and include the source file in the assembly compilation.</summary>
	/// <param name="buildProvider">The build provider generating the code source file.</param>
	/// <returns>An open <see cref="T:System.IO.TextWriter" /> that can be used to write source code to a temporary file.</returns>
	public TextWriter CreateCodeFile(BuildProvider buildProvider)
	{
		if (buildProvider == null)
		{
			throw new ArgumentNullException("buildProvider");
		}
		string tempFilePhysicalPath = GetTempFilePhysicalPath(provider.FileExtension);
		SourceFiles.Add(tempFilePhysicalPath);
		AddPathToBuilderMap(tempFilePhysicalPath, buildProvider);
		return new StreamWriter(File.OpenWrite(tempFilePhysicalPath));
	}

	internal void AddCodeFile(string path)
	{
		AddCodeFile(path, null, isVirtual: false);
	}

	internal void AddCodeFile(string path, BuildProvider bp)
	{
		AddCodeFile(path, bp, isVirtual: false);
	}

	internal void AddCodeFile(string path, BuildProvider bp, bool isVirtual)
	{
		if (string.IsNullOrEmpty(path))
		{
			return;
		}
		Dictionary<string, bool> codeFiles = CodeFiles;
		if (codeFiles.ContainsKey(path))
		{
			return;
		}
		codeFiles.Add(path, value: true);
		string extension = Path.GetExtension(path);
		if (extension == null || extension.Length == 0)
		{
			return;
		}
		extension = extension.Substring(1);
		string tempFilePhysicalPath = GetTempFilePhysicalPath(extension);
		string text = extension.ToLowerInvariant();
		ICodePragmaGenerator codePragmaGenerator = ((text == "cs") ? ((ICodePragmaGenerator)new CSharpCodePragmaGenerator()) : ((ICodePragmaGenerator)((!(text == "vb")) ? null : new VBCodePragmaGenerator())));
		if (isVirtual)
		{
			VirtualFile file = HostingEnvironment.VirtualPathProvider.GetFile(path);
			if (file == null)
			{
				throw new HttpException(404, "Virtual file '" + path + "' does not exist.");
			}
			if (file is DefaultVirtualFile)
			{
				path = HostingEnvironment.MapPath(path);
			}
			CopyFileWithChecksum(file.Open(), tempFilePhysicalPath, path, codePragmaGenerator);
		}
		else
		{
			CopyFileWithChecksum(path, tempFilePhysicalPath, path, codePragmaGenerator);
		}
		if (codePragmaGenerator != null)
		{
			if (bp != null)
			{
				AddPathToBuilderMap(tempFilePhysicalPath, bp);
			}
			SourceFiles.Add(tempFilePhysicalPath);
		}
	}

	private void CopyFileWithChecksum(string input, string to, string from, ICodePragmaGenerator pragmaGenerator)
	{
		CopyFileWithChecksum(new FileStream(input, FileMode.Open, FileAccess.Read), to, from, pragmaGenerator);
	}

	private void CopyFileWithChecksum(Stream input, string to, string from, ICodePragmaGenerator pragmaGenerator)
	{
		if (pragmaGenerator == null)
		{
			string value;
			using (StreamReader streamReader = new StreamReader(input, WebEncoding.FileEncoding))
			{
				value = streamReader.ReadToEnd();
			}
			CodeSnippetCompileUnit codeSnippetCompileUnit = new CodeSnippetCompileUnit(value);
			codeSnippetCompileUnit.LinePragma = new CodeLinePragma(from, 1);
			value = null;
			AddCodeCompileUnit(codeSnippetCompileUnit);
			codeSnippetCompileUnit = null;
			return;
		}
		MD5 checksum = MD5.Create();
		using (FileStream stream = new FileStream(to, FileMode.Create, FileAccess.Write))
		{
			using StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
			using StreamReader streamReader2 = new StreamReader(input, WebEncoding.FileEncoding);
			int num = pragmaGenerator.ReserveSpace(from);
			char[] array = ((num <= 8192) ? new char[8192] : new char[num]);
			streamWriter.Write(array, 0, num);
			while (true)
			{
				num = streamReader2.Read(array, 0, 8192);
				if (num == 0)
				{
					break;
				}
				streamWriter.Write(array, 0, num);
				UpdateChecksum(array, num, checksum, final: false);
			}
			UpdateChecksum(array, 0, checksum, final: true);
			array = null;
		}
		pragmaGenerator.DecorateFile(to, from, checksum, Encoding.UTF8);
	}

	private void UpdateChecksum(char[] buf, int count, MD5 checksum, bool final)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(buf, 0, count);
		if (final)
		{
			checksum.TransformFinalBlock(bytes, 0, bytes.Length);
		}
		else
		{
			checksum.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
		}
		bytes = null;
	}

	/// <summary>Allows a build provider to create a resource file to include in the assembly compilation.</summary>
	/// <param name="buildProvider">The build provider generating the resource.</param>
	/// <param name="name">The name of the resource file to be created.</param>
	/// <returns>An open <see cref="T:System.IO.Stream" /> that can be used to write resources, which are included in the assembly compilation.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="name" /> is not a valid file name.</exception>
	public Stream CreateEmbeddedResource(BuildProvider buildProvider, string name)
	{
		if (buildProvider == null)
		{
			throw new ArgumentNullException("buildProvider");
		}
		if (name == null || name == "")
		{
			throw new ArgumentNullException("name");
		}
		string tempFilePhysicalPath = GetTempFilePhysicalPath("resource");
		FileStream result = File.OpenWrite(tempFilePhysicalPath);
		ResourceFiles[name] = tempFilePhysicalPath;
		return result;
	}

	/// <summary>Inserts a fast object factory template for a type into the compiled assembly.</summary>
	/// <param name="typeName">The name of the type to generate.</param>
	[MonoTODO("Not implemented, does nothing")]
	public void GenerateTypeFactory(string typeName)
	{
	}

	/// <summary>Generates a temporary file path.</summary>
	/// <param name="extension">The file extension to use for the temporary file.</param>
	/// <returns>A path to a temporary file, with the specified file extension.</returns>
	public string GetTempFilePhysicalPath(string extension)
	{
		if (extension == null)
		{
			throw new ArgumentNullException("extension");
		}
		string text = OutputAssemblyPrefix + "_" + temp_files.Count + "." + extension;
		temp_files.AddFile(text, KeepFiles);
		return text;
	}

	private CodeUnit CheckForPartialTypes(CodeUnit codeUnit)
	{
		Dictionary<string, List<CompileUnitPartialType>> partialTypes = PartialTypes;
		foreach (CodeNamespace @namespace in codeUnit.Unit.Namespaces)
		{
			if (@namespace == null)
			{
				continue;
			}
			CodeTypeDeclarationCollection types = @namespace.Types;
			if (types == null || types.Count == 0)
			{
				continue;
			}
			foreach (CodeTypeDeclaration item in types)
			{
				if (item != null && item.IsPartial)
				{
					CompileUnitPartialType compileUnitPartialType = new CompileUnitPartialType(codeUnit.Unit, @namespace, item);
					string typeName = compileUnitPartialType.TypeName;
					if (!partialTypes.TryGetValue(typeName, out var value))
					{
						value = new List<CompileUnitPartialType>(1);
						partialTypes.Add(typeName, value);
					}
					value.Add(compileUnitPartialType);
				}
			}
		}
		return codeUnit;
	}

	private void ProcessPartialTypes()
	{
		Dictionary<string, List<CompileUnitPartialType>> partialTypes = PartialTypes;
		if (partialTypes.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<string, List<CompileUnitPartialType>> item in partialTypes)
		{
			ProcessType(item.Value);
		}
	}

	private void ProcessType(List<CompileUnitPartialType> typeList)
	{
		CompileUnitPartialType[] array = new CompileUnitPartialType[typeList.Count];
		int num = 0;
		foreach (CompileUnitPartialType type in typeList)
		{
			if (num == 0)
			{
				array[0] = type;
				num++;
				continue;
			}
			for (int i = 0; i < num; i++)
			{
				CompareTypes(array[i], type);
			}
			array[num++] = type;
		}
	}

	private void CompareTypes(CompileUnitPartialType source, CompileUnitPartialType target)
	{
		CodeTypeDeclaration partialType = source.PartialType;
		CodeTypeMemberCollection members = target.PartialType.Members;
		List<CodeTypeMember> list = new List<CodeTypeMember>();
		foreach (CodeTypeMember item in members)
		{
			if (TypeHasMember(partialType, item))
			{
				list.Add(item);
			}
		}
		foreach (CodeTypeMember item2 in list)
		{
			members.Remove(item2);
		}
	}

	private bool TypeHasMember(CodeTypeDeclaration type, CodeTypeMember member)
	{
		if (type == null || member == null)
		{
			return false;
		}
		return FindMemberByName(type, member.Name) != null;
	}

	private CodeTypeMember FindMemberByName(CodeTypeDeclaration type, string name)
	{
		foreach (CodeTypeMember member in type.Members)
		{
			if (member != null && !(member.Name != name))
			{
				return member;
			}
		}
		return null;
	}

	internal CompilerResults BuildAssembly()
	{
		return BuildAssembly(null, CompilerOptions);
	}

	internal CompilerResults BuildAssembly(VirtualPath virtualPath)
	{
		return BuildAssembly(virtualPath, CompilerOptions);
	}

	internal CompilerResults BuildAssembly(CompilerParameters options)
	{
		return BuildAssembly(null, options);
	}

	internal CompilerResults BuildAssembly(VirtualPath virtualPath, CompilerParameters options)
	{
		if (options == null)
		{
			throw new ArgumentNullException("options");
		}
		options.TempFiles = temp_files;
		if (options.OutputAssembly == null)
		{
			options.OutputAssembly = OutputAssemblyName;
		}
		ProcessPartialTypes();
		CodeUnit[] unitsAsArray = GetUnitsAsArray();
		List<string> sourceFiles = SourceFiles;
		Dictionary<string, string> resourceFiles = ResourceFiles;
		if (unitsAsArray.Length == 0 && sourceFiles.Count == 0 && resourceFiles.Count == 0 && options.EmbeddedResources.Count == 0)
		{
			return null;
		}
		string text = options.CompilerOptions;
		if (options.IncludeDebugInformation)
		{
			if (string.IsNullOrEmpty(text))
			{
				text = "/d:DEBUG";
			}
			else if (text.IndexOf("d:DEBUG", StringComparison.OrdinalIgnoreCase) == -1)
			{
				text += " /d:DEBUG";
			}
			options.CompilerOptions = text;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "/noconfig";
		}
		else if (text.IndexOf("noconfig", StringComparison.OrdinalIgnoreCase) == -1)
		{
			text += " /noconfig";
		}
		options.CompilerOptions = text;
		StreamWriter streamWriter = null;
		CodeUnit[] array = unitsAsArray;
		for (int i = 0; i < array.Length; i++)
		{
			CodeUnit codeUnit = array[i];
			string tempFilePhysicalPath = GetTempFilePhysicalPath(provider.FileExtension);
			try
			{
				streamWriter = new StreamWriter(File.OpenWrite(tempFilePhysicalPath), Encoding.UTF8);
				provider.GenerateCodeFromCompileUnit(codeUnit.Unit, streamWriter, null);
				sourceFiles.Add(tempFilePhysicalPath);
			}
			catch
			{
				throw;
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Flush();
					streamWriter.Close();
				}
			}
			if (codeUnit.BuildProvider != null)
			{
				AddPathToBuilderMap(tempFilePhysicalPath, codeUnit.BuildProvider);
			}
		}
		foreach (KeyValuePair<string, string> item in resourceFiles)
		{
			options.EmbeddedResources.Add(item.Value);
		}
		AddAssemblyReference(BuildManager.GetReferencedAssemblies());
		List<Assembly> list = new List<Assembly>();
		Dictionary<Guid, bool> moduleGuidCache = new Dictionary<Guid, bool>();
		StringCollection referencedAssemblies = options.ReferencedAssemblies;
		ReferenceAssemblies(moduleGuidCache, list, ReferencedAssemblies);
		ReferenceAssemblies(moduleGuidCache, list, referencedAssemblies);
		Type appType = HttpApplicationFactory.AppType;
		if (appType != null)
		{
			ReferenceAssembly(moduleGuidCache, list, appType.Assembly);
		}
		referencedAssemblies.Clear();
		foreach (Assembly item2 in list)
		{
			string localPath = new Uri(item2.CodeBase).LocalPath;
			string location = item2.Location;
			if (!referencedAssemblies.Contains(localPath) && !referencedAssemblies.Contains(location))
			{
				referencedAssemblies.Add(localPath);
			}
		}
		CompilerResults compilerResults = provider.CompileAssemblyFromFile(options, sourceFiles.ToArray());
		if (compilerResults.NativeCompilerReturnValue != 0)
		{
			string fileText = null;
			CompilerErrorCollection errors = compilerResults.Errors;
			try
			{
				if (errors != null && errors.Count > 0)
				{
					using StreamReader streamReader = File.OpenText(compilerResults.Errors[0].FileName);
					fileText = streamReader.ReadToEnd();
				}
			}
			catch (Exception)
			{
			}
			throw new CompilationException((virtualPath != null) ? virtualPath.Original : string.Empty, compilerResults, fileText);
		}
		if (compilerResults.CompiledAssembly == null)
		{
			if (!File.Exists(options.OutputAssembly))
			{
				compilerResults.TempFiles.Delete();
				throw new CompilationException((virtualPath != null) ? virtualPath.Original : string.Empty, compilerResults.Errors, "No assembly returned after compilation!?");
			}
			try
			{
				compilerResults.CompiledAssembly = Assembly.LoadFrom(options.OutputAssembly);
			}
			catch (Exception innerException)
			{
				compilerResults.TempFiles.Delete();
				throw new HttpException("Unable to load compiled assembly", innerException);
			}
		}
		if (!KeepFiles)
		{
			compilerResults.TempFiles.Delete();
		}
		return compilerResults;
	}

	private void ReferenceAssembly(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, Assembly asm)
	{
		Guid moduleVersionId = asm.ManifestModule.ModuleVersionId;
		if (!moduleGuidCache.ContainsKey(moduleVersionId))
		{
			moduleGuidCache[moduleVersionId] = true;
			assemblies.Add(asm);
		}
	}

	private void ReferenceAssemblies(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, List<Assembly> references)
	{
		if (references == null || references.Count == 0)
		{
			return;
		}
		foreach (Assembly reference in references)
		{
			ReferenceAssembly(moduleGuidCache, assemblies, reference);
		}
	}

	private void ReferenceAssemblies(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, StringCollection references)
	{
		if (references == null || references.Count == 0)
		{
			return;
		}
		StringEnumerator enumerator = references.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				string current = enumerator.Current;
				ReferenceAssembly(moduleGuidCache, assemblies, current);
			}
		}
		finally
		{
			if (enumerator is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}
	}

	private void ReferenceAssembly(Dictionary<Guid, bool> moduleGuidCache, List<Assembly> assemblies, string asmLocation)
	{
		Assembly assembly = Assembly.LoadFrom(asmLocation);
		if (!(assembly == null))
		{
			ReferenceAssembly(moduleGuidCache, assemblies, assembly);
		}
	}
}
