using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Configuration;

namespace System.Web.Hosting;

/// <summary>Provides application-management functions and application services to a managed application within its application domain. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.High)]
public sealed class HostingEnvironment : MarshalByRefObject
{
	private static bool is_hosted;

	private static string site_name;

	private static ApplicationShutdownReason shutdown_reason;

	internal static BareApplicationHost Host;

	private static VirtualPathProvider vpath_provider = ((HttpRuntime.AppDomainAppVirtualPath == null) ? null : new DefaultVirtualPathProvider());

	private static int busy_count;

	private static BackgroundWorkScheduler _backgroundWorkScheduler = null;

	private static readonly Task<object> _completedTask = Task.FromResult<object>(null);

	internal static bool HaveCustomVPP { get; private set; }

	/// <summary>Gets the unique identifier of the application.</summary>
	/// <returns>The unique identifier of the application.</returns>
	public static string ApplicationID => HttpRuntime.AppDomainAppId;

	/// <summary>Gets the physical path on disk to the application's directory.</summary>
	/// <returns>The physical path on disk to the application's directory.</returns>
	public static string ApplicationPhysicalPath => HttpRuntime.AppDomainAppPath;

	/// <summary>Gets the root virtual path of the application.</summary>
	/// <returns>The root virtual path of the application with no trailing slash (/).</returns>
	public static string ApplicationVirtualPath => HttpRuntime.AppDomainAppVirtualPath;

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> instance for the current application.</summary>
	/// <returns>The current <see cref="T:System.Web.Caching.Cache" /> instance.</returns>
	public static Cache Cache => HttpRuntime.Cache;

	/// <summary>Gets any exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object.</summary>
	/// <returns>The exception thrown during initialization of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object. If no exception was thrown, returns <see langword="null" />.</returns>
	public static Exception InitializationException => HttpApplication.InitializationException;

	/// <summary>Gets a value indicating whether the current application domain is being hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> if the application domain is hosted by an <see cref="T:System.Web.Hosting.ApplicationManager" /> object; otherwise, <see langword="false" />.</returns>
	public static bool IsHosted
	{
		get
		{
			return is_hosted;
		}
		internal set
		{
			is_hosted = value;
		}
	}

	/// <summary>Returns an enumerated value that indicates why the application terminated.</summary>
	/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> values.</returns>
	public static ApplicationShutdownReason ShutdownReason => shutdown_reason;

	/// <summary>Gets the name of the site.</summary>
	/// <returns>The name of the site.</returns>
	public static string SiteName
	{
		get
		{
			return site_name;
		}
		internal set
		{
			site_name = value;
		}
	}

	/// <summary>Gets the virtual path provider for this application.</summary>
	/// <returns>The <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance for this application.</returns>
	public static VirtualPathProvider VirtualPathProvider => vpath_provider;

	/// <summary>Gets a value that indicates whether the hosting environment has access to the ASP.NET build system.</summary>
	/// <returns>
	///     <see langword="true" /> if the application domain is the ASP.NET hosted application domain used in <see langword="ClientBuildManager" /> scenarios; otherwise, <see langword="false" />.</returns>
	public static bool InClientBuildManager => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.HostingEnvironment" /> class. </summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Hosting.HostingEnvironment.#ctor" /> constructor is called more than once.</exception>
	public HostingEnvironment()
	{
		throw new InvalidOperationException();
	}

	/// <summary>Reduces the count of busy objects in the hosted environment by one.</summary>
	public static void DecrementBusyCount()
	{
		Interlocked.Decrement(ref busy_count);
	}

	/// <summary>Impersonates the user represented by the application identity.</summary>
	/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
	/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
	[MonoTODO("Not implemented")]
	public static IDisposable Impersonate()
	{
		throw new NotImplementedException();
	}

	/// <summary>Impersonates the user represented by the specified user token.</summary>
	/// <param name="token">The handle of a Windows account token.</param>
	/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
	/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
	[MonoTODO("Not implemented")]
	public static IDisposable Impersonate(IntPtr token)
	{
		throw new NotImplementedException();
	}

	/// <summary>Impersonates the user specified by the configuration settings for the specified virtual path, or the specified user token.</summary>
	/// <param name="userToken">The handle of a Windows account token.</param>
	/// <param name="virtualPath">The path to the requested resource.</param>
	/// <returns>An <see cref="T:System.IDisposable" /> object that represents the Windows user prior to impersonation; this object can be used to revert to the original user's context.</returns>
	/// <exception cref="T:System.Web.HttpException">The process cannot impersonate.</exception>
	[MonoTODO("Not implemented")]
	public static IDisposable Impersonate(IntPtr userToken, string virtualPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Increases the count of busy objects in the hosted environment by one.</summary>
	public static void IncrementBusyCount()
	{
		Interlocked.Increment(ref busy_count);
	}

	/// <summary>Gives the <see cref="T:System.Web.Hosting.HostingEnvironment" /> object an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>Always <see langword="null" />.</returns>
	public override object InitializeLifetimeService()
	{
		return null;
	}

	/// <summary>Starts shutting down the web application associated with this host and removes registered objects from the system.</summary>
	public static void InitiateShutdown()
	{
		HttpRuntime.UnloadAppDomain();
	}

	/// <summary>Maps a virtual path to a physical path on the server.</summary>
	/// <param name="virtualPath">The virtual path (absolute or relative).</param>
	/// <returns>The physical path on the server specified by <paramref name="virtualPath" />.</returns>
	public static string MapPath(string virtualPath)
	{
		if (virtualPath == null || virtualPath == "")
		{
			throw new ArgumentNullException("virtualPath");
		}
		return (HttpContext.Current?.Request)?.MapPath(virtualPath);
	}

	/// <summary>Places an object in the list of registered objects for the application.</summary>
	/// <param name="obj">The object to register.</param>
	public static void RegisterObject(IRegisteredObject obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		if (Host != null)
		{
			Host.RegisterObject(obj, auto_clean: false);
		}
	}

	/// <summary>Registers a new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance with the ASP.NET compilation system.</summary>
	/// <param name="virtualPathProvider">The new <see cref="T:System.Web.Hosting.VirtualPathProvider" /> instance to add to the compilation system.</param>
	public static void RegisterVirtualPathProvider(VirtualPathProvider virtualPathProvider)
	{
		if (HttpRuntime.AppDomainAppVirtualPath == null)
		{
			throw new InvalidOperationException();
		}
		if (virtualPathProvider == null)
		{
			throw new ArgumentNullException("virtualPathProvider");
		}
		VirtualPathProvider prev = vpath_provider;
		vpath_provider = virtualPathProvider;
		vpath_provider.InitializeAndSetPrevious(prev);
		if (!(virtualPathProvider is DefaultVirtualPathProvider))
		{
			HaveCustomVPP = true;
		}
		else
		{
			HaveCustomVPP = false;
		}
	}

	/// <summary>Sets the current thread to the culture of the specified virtual path.</summary>
	/// <param name="virtualPath">The path that contains the culture information.</param>
	/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
	public static IDisposable SetCultures(string virtualPath)
	{
		GlobalizationSection obj = WebConfigurationManager.GetSection("system.web/globalization", virtualPath) as GlobalizationSection;
		IDisposable result = Thread.CurrentThread.CurrentCulture as IDisposable;
		string culture = obj.Culture;
		if (string.IsNullOrEmpty(culture))
		{
			return result;
		}
		Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
		return result;
	}

	/// <summary>Sets the current thread to the culture specified in the application configuration file.</summary>
	/// <returns>An <see cref="T:System.IDisposable" /> object that represents the culture prior to changing; this object can be used to revert to the previous culture.</returns>
	public static IDisposable SetCultures()
	{
		return SetCultures("~/");
	}

	/// <summary>Removes an object from the list of registered objects for the application.</summary>
	/// <param name="obj">The object to remove.</param>
	public static void UnregisterObject(IRegisteredObject obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		if (Host != null)
		{
			Host.UnregisterObject(obj);
		}
	}

	/// <summary>[Supported in the .NET Framework 4.5.2 and later versions]Schedules a task which can run in the background, independent of any request.</summary>
	/// <param name="workItem">A unit of execution.</param>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public static void QueueBackgroundWorkItem(Action<CancellationToken> workItem)
	{
		if (workItem == null)
		{
			throw new ArgumentNullException("workItem");
		}
		QueueBackgroundWorkItem(delegate(CancellationToken ct)
		{
			workItem(ct);
			return _completedTask;
		});
	}

	/// <summary>[Supported in the .NET Framework 4.5.2 and later versions]Schedules a task which can run in the background, independent of any request.</summary>
	/// <param name="workItem">A unit of execution.</param>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	public static void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
	{
		if (workItem == null)
		{
			throw new ArgumentNullException("workItem");
		}
		if (Host == null)
		{
			throw new InvalidOperationException();
		}
		QueueBackgroundWorkItemInternal(workItem);
	}

	private static void QueueBackgroundWorkItemInternal(Func<CancellationToken, Task> workItem)
	{
		BackgroundWorkScheduler backgroundWorkScheduler = Volatile.Read(ref _backgroundWorkScheduler);
		if (backgroundWorkScheduler == null)
		{
			BackgroundWorkScheduler backgroundWorkScheduler2 = new BackgroundWorkScheduler(UnregisterObject, WriteUnhandledException);
			backgroundWorkScheduler = Interlocked.CompareExchange(ref _backgroundWorkScheduler, backgroundWorkScheduler2, null) ?? backgroundWorkScheduler2;
			if (backgroundWorkScheduler == backgroundWorkScheduler2)
			{
				RegisterObject(backgroundWorkScheduler);
			}
		}
		backgroundWorkScheduler.ScheduleWorkItem(workItem);
	}

	private static void WriteUnhandledException(AppDomain appDomain, Exception exception)
	{
		Console.Error.WriteLine("Error in background work item: " + exception);
	}
}
