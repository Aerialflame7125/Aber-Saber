using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;
using Mono.Web.Util;

namespace System.Web;

/// <summary>Provides a set of ASP.NET run-time services for the current application. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpRuntime
{
	private static bool domainUnloading;

	private static SplitOrderedList<string, string> registeredAssemblies;

	private static QueueManager queue_manager;

	private static TraceManager trace_manager;

	private static Cache cache;

	private static Cache internalCache;

	private static WaitCallback do_RealProcessRequest;

	private static HttpWorkerRequest.EndOfSendNotification end_of_send_cb;

	private static Exception initialException;

	private static bool firstRun;

	private static bool assemblyMappingEnabled;

	private static object assemblyMappingLock;

	private static object appOfflineLock;

	private static HttpRuntimeSection runtime_section;

	private static string _actual_bin_directory;

	private static readonly string[] app_offline_files;

	private static string app_offline_file;

	private static string content503;

	internal static SplitOrderedList<string, string> RegisteredAssemblies => registeredAssemblies;

	internal static bool DomainUnloading => domainUnloading;

	/// <summary>Gets the folder path for the ASP.NET client script files.</summary>
	/// <returns>The folder path for the ASP.NET client script files.</returns>
	/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed.</exception>
	[MonoDocumentationNote("Currently returns path to the application root")]
	public static string AspClientScriptPhysicalPath => AppDomainAppPath;

	/// <summary>Gets the virtual path for the ASP.NET client script files.</summary>
	/// <returns>The virtual path for the ASP.NET client script files.</returns>
	[MonoDocumentationNote("Currently returns path to the application root")]
	public static string AspClientScriptVirtualPath => AppDomainAppVirtualPath;

	/// <summary>Gets the application identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> exists.</summary>
	/// <returns>The application identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> exists.</returns>
	public static string AppDomainAppId
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		get
		{
			string text = (string)AppDomain.CurrentDomain.GetData(".appId");
			if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}
	}

	/// <summary>Gets the physical drive path of the application directory for the application hosted in the current application domain.</summary>
	/// <returns>The physical drive path of the application directory for the application hosted in the current application domain.</returns>
	public static string AppDomainAppPath
	{
		get
		{
			string text = (string)AppDomain.CurrentDomain.GetData(".appPath");
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}
	}

	/// <summary>Gets the virtual path of the directory that contains the application hosted in the current application domain.</summary>
	/// <returns>The virtual path of the directory that contains the application hosted in the current application domain.</returns>
	public static string AppDomainAppVirtualPath => (string)AppDomain.CurrentDomain.GetData(".appVPath");

	/// <summary>Gets the domain identification of the application domain where the <see cref="T:System.Web.HttpRuntime" /> instance exists.</summary>
	/// <returns>The unique application domain identifier.</returns>
	public static string AppDomainId
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		get
		{
			return (string)AppDomain.CurrentDomain.GetData(".domainId");
		}
	}

	/// <summary>Gets the physical path of the directory where the ASP.NET executable files are installed.</summary>
	/// <returns>The physical path to the ASP.NET executable files.</returns>
	/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed on this computer.</exception>
	public static string AspInstallDirectory
	{
		get
		{
			string text = (string)AppDomain.CurrentDomain.GetData(".hostingInstallDir");
			if (text != null && text.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
			}
			return text;
		}
	}

	/// <summary>Gets the physical path to the /bin directory for the current application.</summary>
	/// <returns>The path to the current application's /bin directory.</returns>
	public static string BinDirectory
	{
		get
		{
			if (_actual_bin_directory == null)
			{
				string[] array = AppDomain.CurrentDomain.SetupInformation.PrivateBinPath.Split(';');
				string appDomainAppPath = AppDomainAppPath;
				string[] array2 = array;
				foreach (string path in array2)
				{
					string text = Path.Combine(appDomainAppPath, path);
					if (Directory.Exists(text))
					{
						_actual_bin_directory = text;
						break;
					}
				}
				if (_actual_bin_directory == null)
				{
					_actual_bin_directory = Path.Combine(appDomainAppPath, "bin");
				}
				if (_actual_bin_directory[_actual_bin_directory.Length - 1] != Path.DirectorySeparatorChar)
				{
					_actual_bin_directory += Path.DirectorySeparatorChar;
				}
			}
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, _actual_bin_directory).Demand();
			}
			return _actual_bin_directory;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> for the current application.</summary>
	/// <returns>The current <see cref="T:System.Web.Caching.Cache" />.</returns>
	/// <exception cref="T:System.Web.HttpException">ASP.NET is not installed.</exception>
	public static Cache Cache => cache;

	internal static Cache InternalCache => internalCache;

	/// <summary>Gets the physical path to the directory where the common language runtime executable files are installed.</summary>
	/// <returns>The physical path to the common language runtime executable files.</returns>
	public static string ClrInstallDirectory
	{
		get
		{
			string directoryName = Path.GetDirectoryName(typeof(object).Assembly.Location);
			if (directoryName != null && directoryName.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, directoryName).Demand();
			}
			return directoryName;
		}
	}

	/// <summary>Gets the physical path to the directory where ASP.NET stores temporary files (generated sources, compiled assemblies, and so on) for the current application.</summary>
	/// <returns>The physical path to the application's temporary file storage directory.</returns>
	public static string CodegenDir
	{
		get
		{
			string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, dynamicBase).Demand();
			}
			return dynamicBase;
		}
	}

	/// <summary>Gets a value that indicates whether the application is mapped to a universal naming convention (UNC) share.</summary>
	/// <returns>
	///     <see langword="true" /> if the application is mapped to a UNC share; otherwise, <see langword="false" />.</returns>
	public static bool IsOnUNCShare
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		get
		{
			return RuntimeHelpers.IsUncShare;
		}
	}

	/// <summary>Gets the physical path to the directory where the Machine.config file for the current application is located.</summary>
	/// <returns>The physical path to the Machine.config file for the current application.</returns>
	public static string MachineConfigurationDirectory
	{
		get
		{
			string directoryName = Path.GetDirectoryName(ICalls.GetMachineConfigPath());
			if (directoryName != null && directoryName.Length > 0 && SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, directoryName).Demand();
			}
			return directoryName;
		}
	}

	internal static HttpRuntimeSection Section => runtime_section;

	/// <summary>Gets a value that indicates whether the current application is running in the integrated-pipeline mode of IIS 7.0.</summary>
	/// <returns>
	///     <see langword="true" /> if the application is running in integrated-pipeline mode; otherwise, <see langword="false" />.</returns>
	public static bool UsingIntegratedPipeline => false;

	/// <summary>Gets the version of IIS that is hosting this application.</summary>
	/// <returns>The version of IIS that is hosting this application, or <see langword="null" /> if this application is not hosted by IIS.</returns>
	public static Version IISVersion => null;

	/// <summary>Gets the version of the .NET Framework that the current web application targets.</summary>
	/// <returns>The version of the .NET Framework that the current web application targets.</returns>
	public static Version TargetFramework => runtime_section.TargetFramework;

	internal static TraceManager TraceManager => trace_manager;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpRuntime" /> class.</summary>
	public HttpRuntime()
	{
	}

	static HttpRuntime()
	{
		assemblyMappingLock = new object();
		appOfflineLock = new object();
		app_offline_files = new string[3] { "app_offline.htm", "App_Offline.htm", "APP_OFFLINE.HTM" };
		content503 = "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\">\n<html><head>\n<title>503 Server Unavailable</title>\n</head><body>\n<h1>Server Unavailable</h1>\n</body></html>\n";
		firstRun = true;
		try
		{
			WebConfigurationManager.Init();
			SettingsMappingManager.Init();
			runtime_section = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
		}
		catch (Exception ex)
		{
			initialException = ex;
		}
		queue_manager = new QueueManager();
		if (queue_manager.HasException)
		{
			if (initialException == null)
			{
				initialException = queue_manager.InitialException;
			}
			else
			{
				Console.Error.WriteLine("Exception during QueueManager initialization:");
				Console.Error.WriteLine(queue_manager.InitialException);
			}
		}
		trace_manager = new TraceManager();
		if (trace_manager.HasException)
		{
			if (initialException == null)
			{
				initialException = trace_manager.InitialException;
			}
			else
			{
				Console.Error.WriteLine("Exception during TraceManager initialization:");
				Console.Error.WriteLine(trace_manager.InitialException);
			}
		}
		registeredAssemblies = new SplitOrderedList<string, string>(StringComparer.Ordinal);
		cache = new Cache();
		internalCache = new Cache();
		internalCache.DependencyCache = internalCache;
		do_RealProcessRequest = delegate(object state)
		{
			try
			{
				RealProcessRequest(state);
			}
			catch
			{
			}
		};
		end_of_send_cb = EndOfSend;
	}

	/// <summary>Shuts down the <see cref="T:System.Web.HttpRuntime" /> instance.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static void Close()
	{
	}

	internal static HttpWorkerRequest QueuePendingRequest(bool started_internally)
	{
		HttpWorkerRequest nextRequest = queue_manager.GetNextRequest(null);
		if (nextRequest == null)
		{
			return null;
		}
		if (!started_internally)
		{
			nextRequest.StartedInternally = true;
			ThreadPool.QueueUserWorkItem(do_RealProcessRequest, nextRequest);
			return null;
		}
		return nextRequest;
	}

	private static bool AppIsOffline(HttpContext context)
	{
		if (!HttpApplicationFactory.ApplicationDisabled || app_offline_file == null)
		{
			return false;
		}
		HttpResponse response = context.Response;
		response.Clear();
		response.ContentType = "text/html";
		response.ExpiresAbsolute = DateTime.UtcNow;
		response.StatusCode = 503;
		response.TransmitFile(app_offline_file, final_flush: true);
		context.Request.ReleaseResources();
		context.Response.ReleaseResources();
		HttpContext.Current = null;
		HttpApplication.requests_total_counter.Increment();
		return true;
	}

	private static void AppOfflineFileRenamed(object sender, RenamedEventArgs args)
	{
		AppOfflineFileChanged(sender, args);
	}

	private static void AppOfflineFileChanged(object sender, FileSystemEventArgs args)
	{
		lock (appOfflineLock)
		{
			bool offline;
			switch (args.ChangeType)
			{
			case WatcherChangeTypes.Created:
			case WatcherChangeTypes.Changed:
				offline = true;
				break;
			case WatcherChangeTypes.Deleted:
				offline = false;
				break;
			case WatcherChangeTypes.Renamed:
				offline = ((args is RenamedEventArgs renamedEventArgs && string.Compare(renamedEventArgs.Name, "app_offline.htm", StringComparison.OrdinalIgnoreCase) == 0) ? true : false);
				break;
			default:
				offline = false;
				break;
			}
			SetOfflineMode(offline, args.FullPath);
		}
	}

	private static void SetOfflineMode(bool offline, string filePath)
	{
		if (!offline)
		{
			app_offline_file = null;
			if (HttpApplicationFactory.ApplicationDisabled)
			{
				UnloadAppDomain();
			}
		}
		else
		{
			app_offline_file = filePath;
			HttpApplicationFactory.DisableWatchers();
			HttpApplicationFactory.ApplicationDisabled = true;
			InternalCache.InvokePrivateCallbacks();
			HttpApplicationFactory.Dispose();
		}
	}

	private static void SetupOfflineWatch()
	{
		lock (appOfflineLock)
		{
			FileSystemEventHandler value = AppOfflineFileChanged;
			RenamedEventHandler value2 = AppOfflineFileRenamed;
			string appDomainAppPath = AppDomainAppPath;
			string text = null;
			string[] array = app_offline_files;
			foreach (string text2 in array)
			{
				FileSystemWatcher obj = new FileSystemWatcher
				{
					Path = Path.GetDirectoryName(appDomainAppPath),
					Filter = Path.GetFileName(text2)
				};
				obj.NotifyFilter |= NotifyFilters.Size;
				obj.Deleted += value;
				obj.Changed += value;
				obj.Created += value;
				obj.Renamed += value2;
				obj.EnableRaisingEvents = true;
				string text3 = Path.Combine(appDomainAppPath, text2);
				if (File.Exists(text3))
				{
					text = text3;
				}
			}
			if (text != null)
			{
				SetOfflineMode(offline: true, text);
			}
		}
	}

	private static void RealProcessRequest(object o)
	{
		if (domainUnloading)
		{
			Console.Error.WriteLine("Domain is unloading, not processing the request.");
			return;
		}
		HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)o;
		bool startedInternally = httpWorkerRequest.StartedInternally;
		do
		{
			Process(httpWorkerRequest);
			httpWorkerRequest = QueuePendingRequest(startedInternally);
		}
		while (startedInternally && httpWorkerRequest != null);
	}

	private static void Process(HttpWorkerRequest req)
	{
		bool flag = false;
		if (firstRun)
		{
			firstRun = false;
			if (initialException != null)
			{
				FinishWithException(req, HttpException.NewWithCode("Initial exception", initialException, 3001));
				flag = true;
			}
			SetupOfflineWatch();
		}
		HttpContext httpContext2 = (HttpContext.Current = new HttpContext(req));
		if (AppIsOffline(httpContext2))
		{
			return;
		}
		HttpApplication httpApplication = null;
		if (!flag)
		{
			try
			{
				httpApplication = HttpApplicationFactory.GetApplication(httpContext2);
			}
			catch (Exception innerException)
			{
				FinishWithException(req, HttpException.NewWithCode(string.Empty, innerException, 3001));
				flag = true;
			}
		}
		if (flag)
		{
			httpContext2.Request.ReleaseResources();
			httpContext2.Response.ReleaseResources();
			HttpContext.Current = null;
		}
		else
		{
			httpContext2.ApplicationInstance = httpApplication;
			req.SetEndOfSendNotification(end_of_send_cb, httpContext2);
			((IHttpHandler)httpApplication).ProcessRequest(httpContext2);
			HttpApplicationFactory.Recycle(httpApplication);
		}
	}

	private static void EndOfSend(HttpWorkerRequest ignored1, object ignored2)
	{
	}

	/// <summary>Drives all ASP.NET Web processing execution.</summary>
	/// <param name="wr">An <see cref="T:System.Web.HttpWorkerRequest" /> for the current application. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="wr" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The Web application is running under IIS 7 in Integrated mode.</exception>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
	public static void ProcessRequest(HttpWorkerRequest wr)
	{
		if (wr == null)
		{
			throw new ArgumentNullException("wr");
		}
		HttpWorkerRequest nextRequest = queue_manager.GetNextRequest(wr);
		if (nextRequest != null)
		{
			QueuePendingRequest(started_internally: false);
			RealProcessRequest(nextRequest);
		}
	}

	/// <summary>Terminates the current application. The application restarts the next time a request is received for it.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static void UnloadAppDomain()
	{
		domainUnloading = true;
		HttpApplicationFactory.DisableWatchers();
		ThreadPool.QueueUserWorkItem(delegate
		{
			try
			{
				ShutdownAppDomain();
			}
			catch (Exception value)
			{
				Console.Error.WriteLine(value);
			}
		});
	}

	private static void ShutdownAppDomain()
	{
		queue_manager.Dispose();
		InternalCache.InvokePrivateCallbacks();
		HttpApplicationFactory.Dispose();
		ThreadPool.QueueUserWorkItem(delegate
		{
			try
			{
				DoUnload();
			}
			catch
			{
			}
		});
	}

	private static void DoUnload()
	{
		AppDomain.Unload(AppDomain.CurrentDomain);
	}

	private static void FinishWithException(HttpWorkerRequest wr, HttpException e)
	{
		int httpCode = e.GetHttpCode();
		wr.SendStatus(httpCode, HttpWorkerRequest.GetStatusDescription(httpCode));
		wr.SendUnknownResponseHeader("Connection", "close");
		Encoding aSCII = Encoding.ASCII;
		wr.SendUnknownResponseHeader("Content-Type", "text/html; charset=" + aSCII.WebName);
		string htmlErrorMessage = e.GetHtmlErrorMessage();
		byte[] bytes = aSCII.GetBytes(htmlErrorMessage);
		wr.SendUnknownResponseHeader("Content-Length", bytes.Length.ToString());
		wr.SendResponseFromMemory(bytes, bytes.Length);
		wr.FlushResponse(finalFlush: true);
		wr.CloseConnection();
		HttpApplication.requests_total_counter.Increment();
	}

	internal static void FinishUnavailable(HttpWorkerRequest wr)
	{
		wr.SendStatus(503, "Service unavailable");
		wr.SendUnknownResponseHeader("Connection", "close");
		Encoding aSCII = Encoding.ASCII;
		wr.SendUnknownResponseHeader("Content-Type", "text/html; charset=" + aSCII.WebName);
		byte[] bytes = aSCII.GetBytes(content503);
		wr.SendUnknownResponseHeader("Content-Length", bytes.Length.ToString());
		wr.SendResponseFromMemory(bytes, bytes.Length);
		wr.FlushResponse(finalFlush: true);
		wr.CloseConnection();
		HttpApplication.requests_total_counter.Increment();
	}

	/// <summary>Returns the set of permissions associated with code groups.</summary>
	/// <returns>A <see cref="T:System.Security.NamedPermissionSet" /> object containing the names and descriptions of permissions, or <see langword="null" /> if none exists.</returns>
	[MonoDocumentationNote("Always returns null on Mono")]
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Unrestricted)]
	public static NamedPermissionSet GetNamedPermissionSet()
	{
		return null;
	}

	internal static void WritePreservationFile(Assembly asm, string genericNameBase)
	{
		if (asm == null)
		{
			throw new ArgumentNullException("asm");
		}
		if (string.IsNullOrEmpty(genericNameBase))
		{
			throw new ArgumentNullException("genericNameBase");
		}
		string filePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.DynamicBase, genericNameBase + ".compiled");
		PreservationFile preservationFile = new PreservationFile();
		try
		{
			preservationFile.VirtualPath = "/" + genericNameBase + "/";
			AssemblyName name = asm.GetName();
			preservationFile.Assembly = name.Name;
			preservationFile.ResultType = BuildResultTypeCode.TopLevelAssembly;
			preservationFile.Save(filePath);
		}
		catch (Exception innerException)
		{
			throw new HttpException(string.Format("Failed to write preservation file {0}", genericNameBase + ".compiled"), innerException);
		}
	}

	private static Assembly ResolveAssemblyHandler(object sender, ResolveEventArgs e)
	{
		AssemblyName assemblyName = new AssemblyName(e.Name);
		string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		string text = Path.Combine(dynamicBase, assemblyName.Name + ".compiled");
		string data;
		if (!File.Exists(text))
		{
			string fullName = assemblyName.FullName;
			if (!RegisteredAssemblies.Find((uint)fullName.GetHashCode(), fullName, out data))
			{
				return null;
			}
		}
		else
		{
			PreservationFile preservationFile;
			try
			{
				preservationFile = new PreservationFile(text);
			}
			catch (Exception innerException)
			{
				throw new HttpException(string.Format("Failed to read preservation file {0}", assemblyName.Name + ".compiled"), innerException);
			}
			data = Path.Combine(dynamicBase, preservationFile.Assembly + ".dll");
		}
		if (string.IsNullOrEmpty(data))
		{
			return null;
		}
		Assembly result = null;
		try
		{
			result = Assembly.LoadFrom(data);
		}
		catch (Exception)
		{
		}
		return result;
	}

	internal static void EnableAssemblyMapping(bool enable)
	{
		lock (assemblyMappingLock)
		{
			if (assemblyMappingEnabled != enable)
			{
				if (enable)
				{
					AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblyHandler;
				}
				else
				{
					AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssemblyHandler;
				}
				assemblyMappingEnabled = enable;
			}
		}
	}
}
