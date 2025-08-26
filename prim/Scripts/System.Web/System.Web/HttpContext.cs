using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.Util;

namespace System.Web;

/// <summary>Encapsulates all HTTP-specific information about an individual HTTP request.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpContext : IServiceProvider
{
	internal HttpWorkerRequest WorkerRequest;

	private HttpApplication app_instance;

	private HttpRequest request;

	private HttpResponse response;

	private HttpSessionState session_state;

	private HttpServerUtility server;

	private TraceContext trace_context;

	private IHttpHandler handler;

	private string error_page;

	private bool skip_authorization;

	private IPrincipal user;

	private object errors;

	private Hashtable items;

	private object config_timeout;

	private int timeout_possible;

	private DateTime time_stamp = DateTime.UtcNow;

	private Timer timer;

	private Thread thread;

	private bool _isProcessingInclude;

	[ThreadStatic]
	private static ResourceProviderFactory provider_factory;

	[ThreadStatic]
	private static DefaultResourceProviderFactory default_provider_factory;

	[ThreadStatic]
	private static Dictionary<string, IResourceProvider> resource_providers;

	internal static Assembly AppGlobalResourcesAssembly;

	private ProfileBase profile;

	private LinkedList<IHttpHandler> handlers;

	private static DefaultResourceProviderFactory DefaultProviderFactory
	{
		get
		{
			if (default_provider_factory == null)
			{
				default_provider_factory = new DefaultResourceProviderFactory();
			}
			return default_provider_factory;
		}
	}

	internal bool IsProcessingInclude
	{
		get
		{
			return _isProcessingInclude;
		}
		set
		{
			_isProcessingInclude = value;
		}
	}

	/// <summary>Gets an array of errors accumulated while processing an HTTP request.</summary>
	/// <returns>An array of <see cref="T:System.Exception" /> objects for the current HTTP request.</returns>
	public Exception[] AllErrors
	{
		get
		{
			if (errors == null)
			{
				return null;
			}
			if (errors is Exception)
			{
				return new Exception[1] { (Exception)errors };
			}
			return (Exception[])((ArrayList)errors).ToArray(typeof(Exception));
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpApplicationState" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> for the current HTTP request.To get the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request, use <see cref="P:System.Web.HttpContext.ApplicationInstance" />. (ASP.NET uses <see langword="ApplicationInstance" /> instead of <see langword="Application" /> as a property name to refer to the current <see cref="T:System.Web.HttpApplication" /> instance in order to avoid confusion between ASP.NET and classic ASP. In classic ASP, <see langword="Application" /> refers to the global application state dictionary.) </returns>
	public HttpApplicationState Application => HttpApplicationFactory.ApplicationState;

	/// <summary>Gets or sets the <see cref="T:System.Web.HttpApplication" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpApplication" /> for the current HTTP request.ASP.NET uses <see langword="ApplicationInstance" /> instead of <see langword="Application" /> as a property name to refer to the current <see cref="T:System.Web.HttpApplication" /> instance in order to avoid confusion between ASP.NET and classic ASP. In classic ASP, <see langword="Application" /> refers to the global application state dictionary.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Web application is running under IIS 7.0 in Integrated mode, and an attempt was made to change the property value from a non-null value to <see langword="null" />.</exception>
	public HttpApplication ApplicationInstance
	{
		get
		{
			return app_instance;
		}
		set
		{
			app_instance = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object for the current application domain.</summary>
	/// <returns>The <see cref="T:System.Web.Caching.Cache" /> for the current application domain.</returns>
	public Cache Cache => HttpRuntime.Cache;

	internal Cache InternalCache => HttpRuntime.InternalCache;

	/// <summary>Gets or sets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> instance for the current HTTP request.</returns>
	public static HttpContext Current
	{
		get
		{
			return (HttpContext)CallContext.GetData("c");
		}
		set
		{
			CallContext.SetData("c", value);
		}
	}

	/// <summary>Gets the first error (if any) accumulated during HTTP request processing.</summary>
	/// <returns>The first <see cref="T:System.Exception" /> for the current HTTP request/response process; otherwise, <see langword="null" /> if no errors were accumulated during the HTTP request processing. The default is <see langword="null" />.</returns>
	public Exception Error
	{
		get
		{
			if (errors == null || errors is Exception)
			{
				return (Exception)errors;
			}
			return (Exception)((ArrayList)errors)[0];
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.IHttpHandler" /> object responsible for processing the HTTP request.</summary>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> responsible for processing the HTTP request.</returns>
	public IHttpHandler Handler
	{
		get
		{
			return handler;
		}
		set
		{
			handler = value;
		}
	}

	/// <summary>Gets a value indicating whether custom errors are enabled for the current HTTP request.</summary>
	/// <returns>
	///     <see langword="true" /> if custom errors are enabled; otherwise, <see langword="false" />.</returns>
	public bool IsCustomErrorEnabled
	{
		get
		{
			try
			{
				return IsCustomErrorEnabledUnsafe;
			}
			catch
			{
				return false;
			}
		}
	}

	internal bool IsCustomErrorEnabledUnsafe
	{
		get
		{
			CustomErrorsSection customErrorsSection = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");
			if (customErrorsSection.Mode == CustomErrorsMode.On)
			{
				return true;
			}
			if (customErrorsSection.Mode == CustomErrorsMode.RemoteOnly)
			{
				return !Request.IsLocal;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the current HTTP request is in debug mode.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is in debug mode; otherwise, <see langword="false" />.</returns>
	public bool IsDebuggingEnabled => RuntimeHelpers.DebuggingEnabled;

	/// <summary>Gets a key/value collection that can be used to organize and share data between an <see cref="T:System.Web.IHttpModule" /> interface and an <see cref="T:System.Web.IHttpHandler" /> interface during an HTTP request.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> key/value collection that provides access to an individual value in the collection by a specified key.</returns>
	public IDictionary Items
	{
		get
		{
			if (items == null)
			{
				items = new Hashtable();
			}
			return items;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpRequest" /> for the current HTTP request.</returns>
	/// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
	public HttpRequest Request => request;

	/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the current HTTP response.</summary>
	/// <returns>The <see cref="T:System.Web.HttpResponse" /> for the current HTTP response.</returns>
	/// <exception cref="T:System.Web.HttpException">The Web application is running under IIS 7 in Integrated mode.</exception>
	public HttpResponse Response => response;

	/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object that provides methods used in processing Web requests.</summary>
	/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> for the current HTTP request.</returns>
	public HttpServerUtility Server
	{
		get
		{
			if (server == null)
			{
				server = new HttpServerUtility(this);
			}
			return server;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current HTTP request.</returns>
	public HttpSessionState Session => session_state;

	/// <summary>Gets or sets a value that specifies whether the <see cref="T:System.Web.Security.UrlAuthorizationModule" /> object should skip the authorization check for the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.Security.UrlAuthorizationModule" /> should skip the authorization check; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool SkipAuthorization
	{
		get
		{
			return skip_authorization;
		}
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		set
		{
			skip_authorization = value;
		}
	}

	/// <summary>Gets the initial timestamp of the current HTTP request.</summary>
	/// <returns>The timestamp of the current HTTP request.</returns>
	public DateTime Timestamp => time_stamp.ToLocalTime();

	/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current HTTP response.</summary>
	/// <returns>The <see cref="T:System.Web.TraceContext" /> for the current HTTP response.</returns>
	public TraceContext Trace
	{
		get
		{
			if (trace_context == null)
			{
				trace_context = new TraceContext(this);
			}
			return trace_context;
		}
	}

	/// <summary>Gets or sets security information for the current HTTP request.</summary>
	/// <returns>Security information for the current HTTP request.</returns>
	public IPrincipal User
	{
		get
		{
			return user;
		}
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		set
		{
			user = value;
		}
	}

	internal bool MapRequestHandlerDone { get; set; }

	/// <summary>Gets a <see cref="T:System.Web.RequestNotification" /> value that indicates the current <see cref="T:System.Web.HttpApplication" /> event that is processing. </summary>
	/// <returns>One of the <see cref="T:System.Web.RequestNotification" /> values.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
	public RequestNotification CurrentNotification
	{
		get
		{
			throw new PlatformNotSupportedException("This property is not supported on Mono.");
		}
	}

	/// <summary>Gets a value that is the current processing point in the ASP.NET pipeline just after an <see cref="T:System.Web.HttpApplication" /> event has finished processing. </summary>
	/// <returns>
	///     <see langword="true" /> if custom errors are enabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework 3.0.</exception>
	public bool IsPostNotification
	{
		get
		{
			throw new PlatformNotSupportedException("This property is not supported on Mono.");
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object that represents the currently executing handler.</summary>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> that represents the currently executing handler. </returns>
	public IHttpHandler CurrentHandler => GetCurrentHandler();

	/// <summary>Gets the <see cref="T:System.Web.IHttpHandler" /> object for the parent handler.</summary>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> instance, or <see langword="null" /> if no previous handler was found.</returns>
	public IHttpHandler PreviousHandler => GetPreviousHandler();

	internal bool ProfileInitialized => profile != null;

	/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileBase" /> object for the current user profile.</summary>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileBase" /> if the application configuration file contains a definition for the profile's properties; otherwise, <see langword="null" />.</returns>
	public ProfileBase Profile
	{
		get
		{
			if (profile == null)
			{
				if (Request.IsAuthenticated)
				{
					profile = ProfileBase.Create(User.Identity.Name);
				}
				else
				{
					profile = ProfileBase.Create(Request.AnonymousID, isAuthenticated: false);
				}
			}
			return profile;
		}
		internal set
		{
			profile = value;
		}
	}

	internal string ErrorPage
	{
		get
		{
			return error_page;
		}
		set
		{
			error_page = value;
		}
	}

	internal TimeSpan ConfigTimeout
	{
		get
		{
			if (config_timeout == null)
			{
				config_timeout = HttpRuntime.Section.ExecutionTimeout;
			}
			return (TimeSpan)config_timeout;
		}
		set
		{
			config_timeout = value;
			if (timer != null)
			{
				long num = Math.Max((long)(value - (DateTime.UtcNow - time_stamp)).TotalMilliseconds, 0L);
				if (num > 4294967294u)
				{
					num = 4294967294L;
				}
				timer.Change(num, -1L);
			}
		}
	}

	internal SessionStateBehavior SessionStateBehavior { get; private set; }

	internal bool TimeoutPossible => Interlocked.CompareExchange(ref timeout_possible, 1, 1) == 1;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContext" /> class that uses the specified worker-request object.</summary>
	/// <param name="wr">The <see cref="T:System.Web.HttpWorkerRequest" /> object for the current HTTP request.</param>
	public HttpContext(HttpWorkerRequest wr)
	{
		WorkerRequest = wr;
		request = new HttpRequest(WorkerRequest, this);
		response = new HttpResponse(WorkerRequest, this);
		SessionStateBehavior = SessionStateBehavior.Default;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpContext" /> class by using the specified request and response objects.</summary>
	/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> object for the current HTTP request.</param>
	/// <param name="response">The <see cref="T:System.Web.HttpResponse" /> object for the current HTTP request.</param>
	public HttpContext(HttpRequest request, HttpResponse response)
	{
		this.request = request;
		this.response = response;
		this.request.Context = this;
		this.response.Context = this;
		SessionStateBehavior = SessionStateBehavior.Default;
	}

	internal void PushHandler(IHttpHandler handler)
	{
		if (handler != null)
		{
			if (handlers == null)
			{
				handlers = new LinkedList<IHttpHandler>();
			}
			handlers.AddLast(handler);
		}
	}

	internal void PopHandler()
	{
		if (handlers != null && handlers.Count != 0)
		{
			handlers.RemoveLast();
		}
	}

	private IHttpHandler GetCurrentHandler()
	{
		if (handlers == null || handlers.Count == 0)
		{
			return null;
		}
		return handlers.Last.Value;
	}

	private IHttpHandler GetPreviousHandler()
	{
		if (handlers == null || handlers.Count <= 1)
		{
			return null;
		}
		return handlers.Last.Previous?.Value;
	}

	/// <summary>Adds an exception to the exception collection for the current HTTP request.</summary>
	/// <param name="errorInfo">The <see cref="T:System.Exception" /> to add to the exception collection.</param>
	public void AddError(Exception errorInfo)
	{
		if (errors == null)
		{
			errors = errorInfo;
			return;
		}
		ArrayList arrayList;
		if (errors is Exception)
		{
			arrayList = new ArrayList();
			arrayList.Add(errors);
			errors = arrayList;
		}
		else
		{
			arrayList = (ArrayList)errors;
		}
		arrayList.Add(errorInfo);
	}

	internal void ClearError(Exception e)
	{
		if (errors == e)
		{
			errors = null;
		}
	}

	internal bool HasError(Exception e)
	{
		if (errors == e)
		{
			return true;
		}
		if (!(errors is ArrayList))
		{
			return false;
		}
		return ((ArrayList)errors).Contains(e);
	}

	/// <summary>Clears all errors for the current HTTP request.</summary>
	public void ClearError()
	{
		errors = null;
	}

	/// <summary>Returns requested configuration information for the current application.</summary>
	/// <param name="name">The application configuration tag for which information is requested.</param>
	/// <returns>An object containing configuration information. (Cast the returned configuration section to the appropriate configuration type before use.)</returns>
	[Obsolete("The recommended alternative is System.Web.Configuration.WebConfigurationManager.GetWebApplicationSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
	public static object GetAppConfig(string name)
	{
		return ConfigurationSettings.GetConfig(name);
	}

	/// <summary>Returns requested configuration information for the current HTTP request.</summary>
	/// <param name="name">The configuration tag for which information is requested.</param>
	/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" />, <see langword="null" /> if the section does not exist, or an internal object if the section is not accessible at run time. (Cast the returned object to the appropriate configuration type before use.) </returns>
	[Obsolete("The recommended alternative is System.Web.HttpContext.GetSection in System.Web.dll. http://go.microsoft.com/fwlink/?linkid=14202")]
	public object GetConfig(string name)
	{
		return GetSection(name);
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the requested application-level resource object; otherwise, null if a resource object is not found or if a resource object is found but it does not have the requested property.</returns>
	/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object with the specified <paramref name="classKey" /> parameter was not found.- or -The main assembly does not contain the resources for the neutral culture, and these resources are required because the appropriate satellite assembly is missing.</exception>
	public static object GetGlobalResourceObject(string classKey, string resourceKey)
	{
		return GetGlobalResourceObject(classKey, resourceKey, Thread.CurrentThread.CurrentUICulture);
	}

	private static bool EnsureProviderFactory()
	{
		if (resource_providers == null)
		{
			resource_providers = new Dictionary<string, IResourceProvider>();
		}
		if (provider_factory != null)
		{
			return true;
		}
		if (!(WebConfigurationManager.GetSection("system.web/globalization") is GlobalizationSection { ResourceProviderFactoryType: var text }))
		{
			return false;
		}
		bool flag = false;
		if (string.IsNullOrEmpty(text))
		{
			flag = true;
			text = typeof(DefaultResourceProviderFactory).AssemblyQualifiedName;
		}
		ResourceProviderFactory resourceProviderFactory = Activator.CreateInstance(HttpApplication.LoadType(text, throwOnMissing: true)) as ResourceProviderFactory;
		if (resourceProviderFactory == null && flag)
		{
			return false;
		}
		provider_factory = resourceProviderFactory;
		if (flag)
		{
			default_provider_factory = resourceProviderFactory as DefaultResourceProviderFactory;
		}
		return true;
	}

	internal static IResourceProvider GetResourceProvider(string virtualPath, bool isLocal)
	{
		if (!EnsureProviderFactory())
		{
			return null;
		}
		IResourceProvider value = null;
		if (!resource_providers.TryGetValue(virtualPath, out value))
		{
			value = ((!isLocal) ? provider_factory.CreateGlobalResourceProvider(virtualPath) : provider_factory.CreateLocalResourceProvider(virtualPath));
			if (value == null)
			{
				value = ((!isLocal) ? DefaultProviderFactory.CreateGlobalResourceProvider(virtualPath) : DefaultProviderFactory.CreateLocalResourceProvider(virtualPath));
				if (value == null)
				{
					return null;
				}
			}
			resource_providers.Add(virtualPath, value);
		}
		return value;
	}

	private static object GetGlobalObjectFromFactory(string classKey, string resourceKey, CultureInfo culture)
	{
		return GetResourceProvider(classKey, isLocal: false)?.GetObject(resourceKey, culture);
	}

	/// <summary>Gets an application-level resource object based on the specified <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="classKey">A string that represents the <see cref="P:System.Web.Compilation.ResourceExpressionFields.ClassKey" /> property of the requested resource object.</param>
	/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the requested application-level resource object, which is localized for the specified culture; otherwise, <see langword="null" /> if a resource object is not found or if a resource object is found but it does not have the requested property.</returns>
	/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object for which the specified <paramref name="classKey" /> parameter was not found.- or -The main assembly does not contain the resources for the neutral culture, and these resources are required because the appropriate satellite assembly is missing.</exception>
	public static object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
	{
		return GetGlobalObjectFromFactory("Resources." + classKey, resourceKey, culture);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties.</summary>
	/// <param name="virtualPath">The <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property for the local resource object.</param>
	/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the requested page-level resource object; otherwise, <see langword="null" /> if a matching resource object is found but not a <paramref name="resourceKey" /> parameter.</returns>
	/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object was not found for the specified <paramref name="virtualPath" /> parameter.</exception>
	/// <exception cref="T:System.ArgumentException">The specified <paramref name="virtualPath" /> parameter is not in the current application's root directory.</exception>
	/// <exception cref="T:System.InvalidOperationException">The resource class for the page was not found.</exception>
	public static object GetLocalResourceObject(string virtualPath, string resourceKey)
	{
		return GetLocalResourceObject(virtualPath, resourceKey, Thread.CurrentThread.CurrentUICulture);
	}

	private static object GetLocalObjectFromFactory(string virtualPath, string resourceKey, CultureInfo culture)
	{
		return GetResourceProvider(virtualPath, isLocal: true)?.GetObject(resourceKey, culture);
	}

	/// <summary>Gets a page-level resource object based on the specified <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> and <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" /> properties, and on the <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
	/// <param name="virtualPath">The <see cref="P:System.Web.Compilation.ExpressionBuilderContext.VirtualPath" /> property for the local resource object.</param>
	/// <param name="resourceKey">A string that represents a <see cref="P:System.Web.Compilation.ResourceExpressionFields.ResourceKey" />   property of the requested resource object.</param>
	/// <param name="culture">A string that represents the <see cref="T:System.Globalization.CultureInfo" /> object of the requested resource object.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the requested local resource object, which is localized for the specified culture; otherwise <see langword="null" /> if a matching resource object is found but not a <paramref name="resourceKey" /> parameter.</returns>
	/// <exception cref="T:System.Resources.MissingManifestResourceException">A resource object was not found for the specified <paramref name="virtualPath" /> Parameter.</exception>
	/// <exception cref="T:System.ArgumentException">The specified <paramref name="virtualPath" /> parameter is not in the current application's root directory.</exception>
	/// <exception cref="T:System.InvalidOperationException">The resource class for the page was not found.</exception>
	public static object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
	{
		if (!VirtualPathUtility.IsAbsolute(virtualPath))
		{
			throw new ArgumentException("The specified virtualPath was not rooted.");
		}
		return GetLocalObjectFromFactory(virtualPath, resourceKey, culture);
	}

	/// <summary>Gets a specified configuration section for the current application's default configuration. </summary>
	/// <param name="sectionName">The configuration section path (in XPath format) and the configuration element name.</param>
	/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" />, <see langword="null" /> if the section does not exist, or an internal object if the section is not accessible at run time.</returns>
	public object GetSection(string sectionName)
	{
		return WebConfigurationManager.GetSection(sectionName);
	}

	/// <summary>Returns an object for the current service type.</summary>
	/// <param name="service">A type of <see cref="T:System.Web.HttpContext" /> service to set the service provider to.</param>
	/// <returns>A <see cref="T:System.Web.HttpContext" />; otherwise, <see langword="null" /> if no service is found.</returns>
	object IServiceProvider.GetService(Type service)
	{
		if (service == typeof(HttpWorkerRequest))
		{
			return WorkerRequest;
		}
		if (service == typeof(HttpApplication))
		{
			return ApplicationInstance;
		}
		if (service == typeof(HttpRequest))
		{
			return Request;
		}
		if (service == typeof(HttpResponse))
		{
			return Response;
		}
		if (service == typeof(HttpSessionState))
		{
			return Session;
		}
		if (service == typeof(HttpApplicationState))
		{
			return Application;
		}
		if (service == typeof(IPrincipal))
		{
			return User;
		}
		if (service == typeof(Cache))
		{
			return Cache;
		}
		if (service == typeof(HttpContext))
		{
			return Current;
		}
		if (service == typeof(IHttpHandler))
		{
			return Handler;
		}
		if (service == typeof(HttpServerUtility))
		{
			return Server;
		}
		if (service == typeof(TraceContext))
		{
			return Trace;
		}
		return null;
	}

	/// <summary>Enables you to specify a handler for the request.</summary>
	/// <param name="handler">The object that should process the request.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpContext.RemapHandler(System.Web.IHttpHandler)" /> method was called after the <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event occurred.</exception>
	public void RemapHandler(IHttpHandler handler)
	{
		if (MapRequestHandlerDone)
		{
			throw new InvalidOperationException("The RemapHandler method was called after the MapRequestHandler event occurred.");
		}
		Handler = handler;
	}

	/// <summary>Rewrites the URL using the given path.</summary>
	/// <param name="path">The internal rewrite path.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
	public void RewritePath(string path)
	{
		RewritePath(path, rebaseClientPath: true);
	}

	/// <summary>Rewrites the URL by using the given path, path information, and query string information.</summary>
	/// <param name="filePath">The internal rewrite path.</param>
	/// <param name="pathInfo">Additional path information for a resource. For more information, see <see cref="P:System.Web.HttpRequest.PathInfo" />.</param>
	/// <param name="queryString">The request query string.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
	public void RewritePath(string filePath, string pathInfo, string queryString)
	{
		RewritePath(filePath, pathInfo, queryString, setClientFilePath: false);
	}

	/// <summary>Rewrites the URL using the given path and a Boolean value that specifies whether the virtual path for server resources is modified.</summary>
	/// <param name="path">The internal rewrite path.</param>
	/// <param name="rebaseClientPath">
	///       <see langword="true" /> to reset the virtual path; <see langword="false" /> to keep the virtual path unchanged.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
	public void RewritePath(string path, bool rebaseClientPath)
	{
		int num = path.IndexOf('?');
		if (num != -1)
		{
			RewritePath(path.Substring(0, num), string.Empty, path.Substring(num + 1), rebaseClientPath);
		}
		else
		{
			RewritePath(path, null, null, rebaseClientPath);
		}
	}

	/// <summary>Rewrites the URL using the given virtual path, path information, query string information, and a Boolean value that specifies whether the client file path is set to the rewrite path. </summary>
	/// <param name="filePath">The virtual path to the resource that services the request.</param>
	/// <param name="pathInfo">Additional path information to use for the URL redirect. For more information, see <see cref="P:System.Web.HttpRequest.PathInfo" />.</param>
	/// <param name="queryString">The request query string to use for the URL redirect.</param>
	/// <param name="setClientFilePath">
	///       <see langword="true" /> to set the file path used for client resources to the value of the <paramref name="filePath" /> parameter; otherwise <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is not in the current application's root directory.</exception>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="filePath" /> parameter is not in the current application's root directory.</exception>
	public void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
	{
		if (filePath == null)
		{
			throw new ArgumentNullException("filePath");
		}
		if (!VirtualPathUtility.IsValidVirtualPath(filePath))
		{
			throw new HttpException("'" + HttpUtility.HtmlEncode(filePath) + "' is not a valid virtual path.");
		}
		filePath = VirtualPathUtility.Canonize(filePath);
		bool flag = VirtualPathUtility.IsAppRelative(filePath);
		bool flag2 = !flag && VirtualPathUtility.IsAbsolute(filePath);
		HttpRequest httpRequest = Request;
		if (httpRequest == null)
		{
			return;
		}
		if (flag || flag2)
		{
			if (flag)
			{
				filePath = VirtualPathUtility.ToAbsolute(filePath);
			}
		}
		else
		{
			filePath = VirtualPathUtility.AppendTrailingSlash(httpRequest.BaseVirtualDir) + filePath;
		}
		if (!StrUtils.StartsWith(filePath, HttpRuntime.AppDomainAppVirtualPath))
		{
			throw new HttpException(404, "The virtual path '" + HttpUtility.HtmlEncode(filePath) + "' maps to another application.", filePath);
		}
		httpRequest.SetCurrentExePath(filePath);
		httpRequest.SetFilePath(filePath);
		if (setClientFilePath)
		{
			httpRequest.ClientFilePath = filePath;
		}
		if (pathInfo != null)
		{
			httpRequest.SetPathInfo(pathInfo);
		}
		if (queryString != null)
		{
			httpRequest.QueryStringRaw = queryString;
		}
	}

	/// <summary>Sets the type of session state behavior that is required in order to support an HTTP request.</summary>
	/// <param name="sessionStateBehavior">One of the enumeration values that specifies what type of session state behavior is required.</param>
	/// <exception cref="T:System.InvalidOperationException">The method was called after the <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event was raised. </exception>
	public void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
	{
		SessionStateBehavior = sessionStateBehavior;
	}

	internal void SetSession(HttpSessionState state)
	{
		session_state = state;
	}

	private void TimeoutReached(object state)
	{
		HttpRuntime.QueuePendingRequest(started_internally: false);
		if (Interlocked.CompareExchange(ref timeout_possible, 0, 0) == 0)
		{
			if (timer != null)
			{
				timer.Change(2000, 0);
			}
		}
		else
		{
			StopTimeoutTimer();
			thread.Abort(new StepTimeout());
		}
	}

	internal void StartTimeoutTimer()
	{
		thread = Thread.CurrentThread;
		timer = new Timer(TimeoutReached, null, (int)ConfigTimeout.TotalMilliseconds, -1);
	}

	internal void StopTimeoutTimer()
	{
		if (timer != null)
		{
			timer.Dispose();
			timer = null;
		}
	}

	internal void BeginTimeoutPossible()
	{
		timeout_possible = 1;
	}

	internal void EndTimeoutPossible()
	{
		Interlocked.CompareExchange(ref timeout_possible, 0, 1);
	}
}
