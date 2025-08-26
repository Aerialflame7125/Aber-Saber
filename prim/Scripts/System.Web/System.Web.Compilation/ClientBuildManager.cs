using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation;

/// <summary>Provides compilation services for ASP.NET applications.</summary>
public sealed class ClientBuildManager : MarshalByRefObject, IDisposable
{
	private static readonly object appDomainShutdownEvent = new object();

	private static readonly object appDomainStartedEvent = new object();

	private static readonly object appDomainUnloadedEvent = new object();

	private string virt_dir;

	private string phys_src_dir;

	private BareApplicationHost host;

	private ApplicationManager manager;

	private string app_id;

	private string cache_path;

	private EventHandlerList events = new EventHandlerList();

	private static string[] shutdown_directories = new string[5] { "bin", "App_GlobalResources", "App_Code", "App_WebReferences", "App_Browsers" };

	private BareApplicationHost Host
	{
		get
		{
			if (host != null)
			{
				return host;
			}
			int num = virt_dir.GetHashCode();
			if (app_id != null)
			{
				num ^= int.Parse(app_id);
			}
			app_id = num.ToString(Helpers.InvariantCulture);
			host = manager.CreateHostWithCheck(app_id, virt_dir, phys_src_dir);
			cache_path = "";
			int num2 = virt_dir.GetHashCode() << 5 + phys_src_dir.GetHashCode();
			cache_path = Path.Combine(cache_path, num2.ToString(Helpers.InvariantCulture));
			Directory.CreateDirectory(cache_path);
			OnAppDomainStarted();
			return host;
		}
	}

	/// <summary>Gets the physical path to the directory used for code generation.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the directory path used for code generation.</returns>
	public string CodeGenDir => Host.GetCodeGenDir();

	/// <summary>Gets a value that indicates whether an application domain for compiling ASP.NET Web applications has been created.</summary>
	/// <returns>
	///     <see langword="true" /> if the application domain for compiling ASP.NET Web applications has been created; otherwise, <see langword="false" />.</returns>
	public bool IsHostCreated => host != null;

	/// <summary>Occurs when an application domain is shut down. </summary>
	public event BuildManagerHostUnloadEventHandler AppDomainShutdown
	{
		add
		{
			events.AddHandler(appDomainShutdownEvent, value);
		}
		remove
		{
			events.RemoveHandler(appDomainShutdownEvent, value);
		}
	}

	/// <summary>Occurs when an application domain is started. </summary>
	public event EventHandler AppDomainStarted
	{
		add
		{
			events.AddHandler(appDomainStartedEvent, value);
		}
		remove
		{
			events.RemoveHandler(appDomainStartedEvent, value);
		}
	}

	/// <summary>Occurs when an application domain is unloaded. </summary>
	public event BuildManagerHostUnloadEventHandler AppDomainUnloaded
	{
		add
		{
			events.AddHandler(appDomainUnloadedEvent, value);
		}
		remove
		{
			events.RemoveHandler(appDomainUnloadedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class without a specified target directory or precompilation flags. </summary>
	/// <param name="appVirtualDir">The virtual path to the application root.</param>
	/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
	public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir)
	{
		if (appVirtualDir == null || appVirtualDir == "")
		{
			throw new ArgumentNullException("appVirtualDir");
		}
		if (appPhysicalSourceDir == null || appPhysicalSourceDir == "")
		{
			throw new ArgumentNullException("appPhysicalSourceDir");
		}
		virt_dir = appVirtualDir;
		phys_src_dir = appPhysicalSourceDir;
		manager = ApplicationManager.GetApplicationManager();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory. </summary>
	/// <param name="appVirtualDir">The virtual path to the application root.</param>
	/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
	/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
	public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir)
		: this(appVirtualDir, appPhysicalSourceDir)
	{
		if (appPhysicalTargetDir == null || appPhysicalTargetDir == "")
		{
			throw new ArgumentNullException("appPhysicalTargetDir");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ClientBuildManager" /> class with the specified target directory and compilation parameter.</summary>
	/// <param name="appVirtualDir">The virtual path to the application root.</param>
	/// <param name="appPhysicalSourceDir">The physical path to the application root.</param>
	/// <param name="appPhysicalTargetDir">The target directory for precompilation.</param>
	/// <param name="parameter">Values that determine the precompilation behavior.</param>
	public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter)
		: this(appVirtualDir, appPhysicalSourceDir, appPhysicalTargetDir)
	{
	}

	private void OnAppDomainStarted()
	{
		if (events[appDomainStartedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	private void OnAppDomainShutdown(ApplicationShutdownReason reason)
	{
		if (events[appDomainShutdownEvent] is BuildManagerHostUnloadEventHandler buildManagerHostUnloadEventHandler)
		{
			BuildManagerHostUnloadEventArgs e = new BuildManagerHostUnloadEventArgs(reason);
			buildManagerHostUnloadEventHandler(this, e);
		}
	}

	/// <summary>Compiles application-dependent files, such as files in the App_Code directory, the Global.asax file, resource files, and Web references.</summary>
	[MonoTODO("Not implemented")]
	public void CompileApplicationDependencies()
	{
		throw new NotImplementedException();
	}

	/// <summary>Compiles the file represented by the virtual path.</summary>
	/// <param name="virtualPath">The path to the file to be compiled.</param>
	public void CompileFile(string virtualPath)
	{
		CompileFile(virtualPath, null);
	}

	/// <summary>Compiles the file represented by the virtual path and provides a callback class to receive status information about the build.</summary>
	/// <param name="virtualPath">The path to the file to be compiled.</param>
	/// <param name="callback">The object to receive status information from compilation.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public void CompileFile(string virtualPath, ClientBuildManagerCallback callback)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an object in the application domain of the ASP.NET runtime.</summary>
	/// <param name="type">The type of object to be created.</param>
	/// <param name="failIfExists">
	///       <see langword="true" /> to throw an exception if the object has already been created in the application domain of the ASP.NET runtime; otherwise, <see langword="false" />.</param>
	/// <returns>An object in the application domain of the ASP.NET runtime.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The object already exists in the application domain and <paramref name="failIfExists" /> is <see langword="true" />.</exception>
	public IRegisteredObject CreateObject(Type type, bool failIfExists)
	{
		return manager.CreateObject(app_id, type, virt_dir, phys_src_dir, failIfExists);
	}

	/// <summary>Generates code from the contents of a file.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="virtualFileString">The contents of the file.</param>
	/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
	/// <returns>A <see cref="T:System.String" /> containing the generated code.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Currently does not return the GeneratedCode")]
	public string GenerateCode(string virtualPath, string virtualFileString, out IDictionary linePragmasTable)
	{
		if (string.IsNullOrEmpty(virtualPath))
		{
			throw new ArgumentNullException("virtualPath");
		}
		GenerateCodeCompileUnit(virtualPath, virtualFileString, out var _, out var _, out linePragmasTable);
		return null;
	}

	/// <summary>Returns the contents, codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="virtualFileString">The contents of the file represented by the <paramref name="virtualPath" /> parameter.</param>
	/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
	/// <param name="compilerParameters">When this method returns, contains the properties that define how the file represented by the <paramref name="virtualPath" /> parameter will be compiled.</param>
	/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, string virtualFileString, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the codeDOM tree, compiler type, and compiler parameters for a file represented by a virtual path.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="codeDomProviderType">When this method returns, contains the codeDOM provider type used for code generation and compilation.</param>
	/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
	/// <param name="linePragmasTable">When this method returns, contains a dictionary of line pragmas.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> for the given file.</returns>
	public CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters, out IDictionary linePragmasTable)
	{
		return GenerateCodeCompileUnit(virtualPath, out codeDomProviderType, out compilerParameters, out linePragmasTable);
	}

	/// <summary>Gets the directories with files that, when changed, cause the application domain to shut down.</summary>
	/// <returns>A <see cref="T:System.String" /> array containing the top-level directory names.</returns>
	public string[] GetAppDomainShutdownDirectories()
	{
		return shutdown_directories;
	}

	/// <summary>Gets a collection of browser elements.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing browser elements.</returns>
	[MonoTODO("Not implemented")]
	public IDictionary GetBrowserDefinitions()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets information about the compiler type, compiler parameters, and the directory in which to store code files generated from non-code files such as .wsdl files.</summary>
	/// <param name="virtualCodeDir">The directory about which to retrieve information.</param>
	/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
	/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
	/// <param name="generatedFilesDir">When this method returns, contains the directory for files generated from non-code files.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualCodeDir" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public void GetCodeDirectoryInformation(string virtualCodeDir, out Type codeDomProviderType, out CompilerParameters compilerParameters, out string generatedFilesDir)
	{
		throw new NotImplementedException();
	}

	/// <summary>Compiles the file represented by the virtual path and returns its compiled type.</summary>
	/// <param name="virtualPath">The virtual path of the file to compile. </param>
	/// <returns>The <see cref="T:System.Type" /> of the compiled file.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public Type GetCompiledType(string virtualPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the compiler type and parameters that are used to build a file represented by a virtual path.</summary>
	/// <param name="virtualPath">The virtual path to the file.</param>
	/// <param name="codeDomProviderType">When this method returns, contains the provider type used for code generation and compilation.</param>
	/// <param name="compilerParameters">When this method returns, contains the properties that define how the file will be compiled.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public void GetCompilerParameters(string virtualPath, out Type codeDomProviderType, out CompilerParameters compilerParameters)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the virtual path of a generated file.</summary>
	/// <param name="filePath">The full physical path to a generated file.</param>
	/// <returns>A <see cref="T:System.String" /> containing the virtual path for <paramref name="filePath" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="filePath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public string GetGeneratedFileVirtualPath(string filePath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the physical path to the generated file for a virtual path. </summary>
	/// <param name="virtualPath">The virtual path of the file to retrieve.</param>
	/// <returns>A <see cref="T:System.String" /> that contains the physical path to the generated file.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public string GetGeneratedSourceFile(string virtualPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an array of the assemblies defined in the Bin directory and the <see langword="&lt;assembly&gt;" /> section of the Web configuration file.</summary>
	/// <param name="virtualPath">The configuration name and path.</param>
	/// <returns>A <see cref="T:System.String" /> array containing paths to code bases in the Bin directory and the <see langword="&lt;assembly&gt;" /> section of the Web configuration file. </returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public string[] GetTopLevelAssemblyReferences(string virtualPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the virtual paths to the App_Code directory and its subdirectories in an ASP.NET application.</summary>
	/// <returns>A <see cref="T:System.String" /> array containing all the virtual paths to code directories in an application.</returns>
	public string[] GetVirtualCodeDirectories()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gives the application domain an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>Always <see langword="null" />.</returns>
	public override object InitializeLifetimeService()
	{
		return null;
	}

	/// <summary>Indicates whether an assembly is a code assembly.</summary>
	/// <param name="assemblyName">The name of the assembly to be identified as a code assembly.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="assemblyName" /> parameter matches one of the generated code assemblies; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="assemblyName" /> is null.</exception>
	[MonoTODO("Not implemented")]
	public bool IsCodeAssembly(string assemblyName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Precompiles an ASP.NET application.</summary>
	[MonoTODO("Not implemented")]
	public void PrecompileApplication()
	{
		throw new NotImplementedException();
	}

	/// <summary>Precompiles an ASP.NET application and provides a callback method to receive status information about the build.</summary>
	/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
	[MonoTODO("Not implemented")]
	public void PrecompileApplication(ClientBuildManagerCallback callback)
	{
		throw new NotImplementedException();
	}

	/// <summary>Precompiles an ASP.NET application, provides a callback method to receive status information about the build, and indicates whether to create a clean build.</summary>
	/// <param name="callback">A <see cref="T:System.Web.Compilation.ClientBuildManagerCallback" /> containing the method to call when reporting the result of compilation.</param>
	/// <param name="forceCleanBuild">
	///       <see langword="true" /> to perform a clean build, which will first delete all object and intermediate files; <see langword="false" /> to rebuild only those files that have changed. Set to true if there is a chance that a dependency might not be picked up by the build environment.</param>
	[MonoTODO("Not implemented")]
	public void PrecompileApplication(ClientBuildManagerCallback callback, bool forceCleanBuild)
	{
		throw new NotImplementedException();
	}

	/// <summary>Unloads the application domain for compiling ASP.NET Web applications.</summary>
	/// <returns>
	///     <see langword="true" /> if the application domain is unloaded; otherwise, <see langword="false" />.</returns>
	public bool Unload()
	{
		if (host != null)
		{
			host.Shutdown();
			OnAppDomainShutdown(ApplicationShutdownReason.None);
			host = null;
		}
		return true;
	}

	/// <summary>Terminates the current ASP.NET application.</summary>
	void IDisposable.Dispose()
	{
		Unload();
	}
}
