using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.SessionState;
using System.Web.Util;

namespace System.Web;

/// <summary>Defines the methods, properties, and events that are common to all application objects in an ASP.NET application. This class is the base class for applications that are defined by the user in the Global.asax file.</summary>
[ToolboxItem(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpApplication : IHttpAsyncHandler, IHttpHandler, IComponent, IDisposable
{
	private class Tim
	{
		private string name;

		private DateTime start;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public Tim()
		{
		}

		public Tim(string name)
		{
			this.name = name;
		}

		public void Start()
		{
			start = DateTime.UtcNow;
		}

		public void Stop()
		{
			Console.WriteLine("{0}: {1}ms", name, (DateTime.UtcNow - start).TotalMilliseconds);
		}
	}

	private static readonly object disposedEvent = new object();

	private static readonly object errorEvent = new object();

	internal static PerformanceCounter requests_total_counter = new PerformanceCounter("ASP.NET", "Requests Total");

	internal static readonly string[] BinDirs = new string[2] { "Bin", "bin" };

	private object this_lock = new object();

	private HttpContext context;

	private HttpSessionState session;

	private ISite isite;

	private volatile HttpModuleCollection modcoll;

	private string assemblyLocation;

	private IHttpHandlerFactory factory;

	private bool autoCulture;

	private bool autoUICulture;

	private bool stop_processing;

	private bool in_application_start;

	private IEnumerator pipeline;

	private ManualResetEvent done;

	private AsyncRequestState begin_iar;

	private AsyncInvoker current_ai;

	private EventHandlerList events;

	private EventHandlerList nonApplicationEvents = new EventHandlerList();

	private CultureInfo app_culture;

	private CultureInfo appui_culture;

	private CultureInfo prev_app_culture;

	private CultureInfo prev_appui_culture;

	private IPrincipal prev_user;

	private static string binDirectory;

	private static volatile Exception initialization_exception;

	private bool removeConfigurationFromCache;

	private bool fullInitComplete;

	private static DynamicModuleManager dynamicModuleManeger = new DynamicModuleManager();

	private bool must_yield;

	private bool in_begin;

	private static object PreSendRequestHeadersEvent;

	private static object PreSendRequestContentEvent;

	private static object AcquireRequestStateEvent;

	private static object AuthenticateRequestEvent;

	private static object AuthorizeRequestEvent;

	private static object BeginRequestEvent;

	private static object EndRequestEvent;

	private static object PostRequestHandlerExecuteEvent;

	private static object PreRequestHandlerExecuteEvent;

	private static object ReleaseRequestStateEvent;

	private static object ResolveRequestCacheEvent;

	private static object UpdateRequestCacheEvent;

	private static object PostAuthenticateRequestEvent;

	private static object PostAuthorizeRequestEvent;

	private static object PostResolveRequestCacheEvent;

	private static object PostMapRequestHandlerEvent;

	private static object PostAcquireRequestStateEvent;

	private static object PostReleaseRequestStateEvent;

	private static object PostUpdateRequestCacheEvent;

	private static object LogRequestEvent;

	private static object MapRequestHandlerEvent;

	private static object PostLogRequestEvent;

	private Tim tim;

	private const string HANDLER_CACHE = "@@HttpHandlerCache@@";

	internal bool InApplicationStart
	{
		get
		{
			return in_application_start;
		}
		set
		{
			in_application_start = value;
		}
	}

	internal string AssemblyLocation
	{
		get
		{
			if (assemblyLocation == null)
			{
				assemblyLocation = GetType().Assembly.Location;
			}
			return assemblyLocation;
		}
	}

	internal static Exception InitializationException => initialization_exception;

	/// <summary>Gets the current state of an application.</summary>
	/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> for the current request.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpApplicationState Application => HttpApplicationFactory.ApplicationState;

	/// <summary>Gets HTTP-specific information about the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpContext Context => context;

	/// <summary>Gets the list of event handler delegates that process all application events.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.EventHandlerList" /> that contains the names of the event handler delegates.</returns>
	protected EventHandlerList Events
	{
		get
		{
			if (events == null)
			{
				events = new EventHandlerList();
			}
			return events;
		}
	}

	/// <summary>Gets the collection of modules for the current application.</summary>
	/// <returns>An <see cref="T:System.Web.HttpModuleCollection" /> that contains the names of the modules for the application.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpModuleCollection Modules
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
		get
		{
			if (modcoll == null)
			{
				modcoll = new HttpModuleCollection();
			}
			return modcoll;
		}
	}

	/// <summary>Gets the intrinsic request object for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpRequest" /> object that the application is processing.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpRequest" /> object is <see langword="null" />.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpRequest Request
	{
		get
		{
			if (context == null)
			{
				throw HttpException.NewWithCode(Locale.GetText("No context is available."), 3001);
			}
			if (!HttpApplicationFactory.ContextAvailable)
			{
				throw HttpException.NewWithCode(Locale.GetText("Request is not available in this context."), 3001);
			}
			return context.Request;
		}
	}

	/// <summary>Gets the intrinsic response object for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpResponse" /> object that the application is processing.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.HttpResponse" /> object is <see langword="null" />. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpResponse Response
	{
		get
		{
			if (context == null)
			{
				throw HttpException.NewWithCode(Locale.GetText("No context is available."), 3001);
			}
			if (!HttpApplicationFactory.ContextAvailable)
			{
				throw HttpException.NewWithCode(Locale.GetText("Response is not available in this context."), 3001);
			}
			return context.Response;
		}
	}

	/// <summary>Gets the intrinsic server object for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object that the application is processing.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpServerUtility Server
	{
		get
		{
			if (context != null)
			{
				return context.Server;
			}
			return new HttpServerUtility(null);
		}
	}

	/// <summary>Gets the intrinsic session object that provides access to session data.</summary>
	/// <returns>The <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current session.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.SessionState.HttpSessionState" /> object is <see langword="null" />. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public HttpSessionState Session
	{
		get
		{
			if (session != null)
			{
				return session;
			}
			if (context == null)
			{
				throw HttpException.NewWithCode(Locale.GetText("No context is available."), 3001);
			}
			return context.Session ?? throw HttpException.NewWithCode(Locale.GetText("Session state is not available in the context."), 3001);
		}
	}

	/// <summary>Gets or sets a site interface for an <see cref="T:System.ComponentModel.IComponent" /> implementation.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.ISite" /> object that allows a container to manage and communicate with its child components.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ISite Site
	{
		get
		{
			return isite;
		}
		set
		{
			isite = value;
		}
	}

	/// <summary>Gets the intrinsic user object for the current request.</summary>
	/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object that represents the current authenticated or anonymous user.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Security.Principal.IPrincipal" /> object is <see langword="null" />. </exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IPrincipal User
	{
		get
		{
			if (context == null)
			{
				throw new HttpException(Locale.GetText("No context is available."));
			}
			if (context.User == null)
			{
				throw new HttpException(Locale.GetText("No currently authenticated user."));
			}
			return context.User;
		}
	}

	internal bool RequestCompleted
	{
		set
		{
			stop_processing = value;
		}
	}

	/// <summary>Gets a <see langword="Boolean" /> value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.IHttpHandler" /> object is reusable; otherwise, <see langword="false" />.</returns>
	bool IHttpHandler.IsReusable => true;

	internal static string BinDirectory
	{
		get
		{
			if (binDirectory == null)
			{
				string applicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
				string[] binDirs = BinDirs;
				foreach (string path in binDirs)
				{
					string path2 = Path.Combine(applicationBase, path);
					if (Directory.Exists(path2))
					{
						binDirectory = path2;
						break;
					}
				}
			}
			return binDirectory;
		}
	}

	internal static string[] BinDirectoryAssemblies
	{
		get
		{
			ArrayList arrayList = null;
			string text = BinDirectory;
			if (text != null)
			{
				arrayList = new ArrayList();
				string[] files = Directory.GetFiles(text, "*.dll");
				arrayList.AddRange(files);
			}
			if (arrayList == null)
			{
				return new string[0];
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
	}

	/// <summary>Occurs when the application is disposed.</summary>
	public virtual event EventHandler Disposed
	{
		add
		{
			nonApplicationEvents.AddHandler(disposedEvent, value);
		}
		remove
		{
			nonApplicationEvents.RemoveHandler(disposedEvent, value);
		}
	}

	/// <summary>Occurs when an unhandled exception is thrown.</summary>
	public virtual event EventHandler Error
	{
		add
		{
			nonApplicationEvents.AddHandler(errorEvent, value);
		}
		remove
		{
			nonApplicationEvents.RemoveHandler(errorEvent, value);
		}
	}

	/// <summary>Occurs just before ASP.NET sends HTTP headers to the client.</summary>
	public event EventHandler PreSendRequestHeaders
	{
		add
		{
			AddEventHandler(PreSendRequestHeadersEvent, value);
		}
		remove
		{
			RemoveEventHandler(PreSendRequestHeadersEvent, value);
		}
	}

	/// <summary>Occurs just before ASP.NET sends content to the client.</summary>
	public event EventHandler PreSendRequestContent
	{
		add
		{
			AddEventHandler(PreSendRequestContentEvent, value);
		}
		remove
		{
			RemoveEventHandler(PreSendRequestContentEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET acquires the current state (for example, session state) that is associated with the current request.</summary>
	public event EventHandler AcquireRequestState
	{
		add
		{
			AddEventHandler(AcquireRequestStateEvent, value);
		}
		remove
		{
			RemoveEventHandler(AcquireRequestStateEvent, value);
		}
	}

	/// <summary>Occurs when a security module has established the identity of the user.</summary>
	public event EventHandler AuthenticateRequest
	{
		add
		{
			AddEventHandler(AuthenticateRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(AuthenticateRequestEvent, value);
		}
	}

	/// <summary>Occurs when a security module has verified user authorization.</summary>
	public event EventHandler AuthorizeRequest
	{
		add
		{
			AddEventHandler(AuthorizeRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(AuthorizeRequestEvent, value);
		}
	}

	/// <summary>Occurs as the first event in the HTTP pipeline chain of execution when ASP.NET responds to a request.</summary>
	public event EventHandler BeginRequest
	{
		add
		{
			if (!InApplicationStart)
			{
				AddEventHandler(BeginRequestEvent, value);
			}
		}
		remove
		{
			if (!InApplicationStart)
			{
				RemoveEventHandler(BeginRequestEvent, value);
			}
		}
	}

	/// <summary>Occurs as the last event in the HTTP pipeline chain of execution when ASP.NET responds to a request.</summary>
	public event EventHandler EndRequest
	{
		add
		{
			if (!InApplicationStart)
			{
				AddEventHandler(EndRequestEvent, value);
			}
		}
		remove
		{
			if (!InApplicationStart)
			{
				RemoveEventHandler(EndRequestEvent, value);
			}
		}
	}

	/// <summary>Occurs when the ASP.NET event handler (for example, a page or an XML Web service) finishes execution.</summary>
	public event EventHandler PostRequestHandlerExecute
	{
		add
		{
			AddEventHandler(PostRequestHandlerExecuteEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostRequestHandlerExecuteEvent, value);
		}
	}

	/// <summary>Occurs just before ASP.NET starts executing an event handler (for example, a page or an XML Web service).</summary>
	public event EventHandler PreRequestHandlerExecute
	{
		add
		{
			AddEventHandler(PreRequestHandlerExecuteEvent, value);
		}
		remove
		{
			RemoveEventHandler(PreRequestHandlerExecuteEvent, value);
		}
	}

	/// <summary>Occurs after ASP.NET finishes executing all request event handlers. This event causes state modules to save the current state data.</summary>
	public event EventHandler ReleaseRequestState
	{
		add
		{
			AddEventHandler(ReleaseRequestStateEvent, value);
		}
		remove
		{
			RemoveEventHandler(ReleaseRequestStateEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET finishes an authorization event to let the caching modules serve requests from the cache, bypassing execution of the event handler (for example, a page or an XML Web service).</summary>
	public event EventHandler ResolveRequestCache
	{
		add
		{
			AddEventHandler(ResolveRequestCacheEvent, value);
		}
		remove
		{
			RemoveEventHandler(ResolveRequestCacheEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET finishes executing an event handler in order to let caching modules store responses that will be used to serve subsequent requests from the cache.</summary>
	public event EventHandler UpdateRequestCache
	{
		add
		{
			AddEventHandler(UpdateRequestCacheEvent, value);
		}
		remove
		{
			RemoveEventHandler(UpdateRequestCacheEvent, value);
		}
	}

	/// <summary>Occurs when a security module has established the identity of the user.</summary>
	public event EventHandler PostAuthenticateRequest
	{
		add
		{
			AddEventHandler(PostAuthenticateRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostAuthenticateRequestEvent, value);
		}
	}

	/// <summary>Occurs when the user for the current request has been authorized.</summary>
	public event EventHandler PostAuthorizeRequest
	{
		add
		{
			AddEventHandler(PostAuthorizeRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostAuthorizeRequestEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET bypasses execution of the current event handler and allows a caching module to serve a request from the cache.</summary>
	public event EventHandler PostResolveRequestCache
	{
		add
		{
			AddEventHandler(PostResolveRequestCacheEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostResolveRequestCacheEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET has mapped the current request to the appropriate event handler.</summary>
	public event EventHandler PostMapRequestHandler
	{
		add
		{
			AddEventHandler(PostMapRequestHandlerEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostMapRequestHandlerEvent, value);
		}
	}

	/// <summary>Occurs when the request state (for example, session state) that is associated with the current request has been obtained.</summary>
	public event EventHandler PostAcquireRequestState
	{
		add
		{
			AddEventHandler(PostAcquireRequestStateEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostAcquireRequestStateEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET has completed executing all request event handlers and the request state data has been stored.</summary>
	public event EventHandler PostReleaseRequestState
	{
		add
		{
			AddEventHandler(PostReleaseRequestStateEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostReleaseRequestStateEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET finishes updating caching modules and storing responses that are used to serve subsequent requests from the cache.</summary>
	public event EventHandler PostUpdateRequestCache
	{
		add
		{
			AddEventHandler(PostUpdateRequestCacheEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostUpdateRequestCacheEvent, value);
		}
	}

	/// <summary>Occurs just before ASP.NET performs any logging for the current request.</summary>
	public event EventHandler LogRequest
	{
		add
		{
			AddEventHandler(LogRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(LogRequestEvent, value);
		}
	}

	/// <summary>Occurs when the handler is selected to respond to the request.</summary>
	public event EventHandler MapRequestHandler
	{
		add
		{
			AddEventHandler(MapRequestHandlerEvent, value);
		}
		remove
		{
			RemoveEventHandler(MapRequestHandlerEvent, value);
		}
	}

	/// <summary>Occurs when ASP.NET has completed processing all the event handlers for the <see cref="E:System.Web.HttpApplication.LogRequest" /> event.</summary>
	public event EventHandler PostLogRequest
	{
		add
		{
			AddEventHandler(PostLogRequestEvent, value);
		}
		remove
		{
			RemoveEventHandler(PostLogRequestEvent, value);
		}
	}

	internal event EventHandler DefaultAuthentication;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpApplication" /> class.</summary>
	public HttpApplication()
	{
		done = new ManualResetEvent(initialState: false);
	}

	internal void InitOnce(bool full_init)
	{
		if (initialization_exception != null || modcoll != null)
		{
			return;
		}
		lock (this_lock)
		{
			if (initialization_exception != null || modcoll != null)
			{
				return;
			}
			bool flag = context == null;
			try
			{
				HttpModulesSection obj = (HttpModulesSection)WebConfigurationManager.GetWebApplicationSection("system.web/httpModules");
				HttpContext current = HttpContext.Current;
				HttpContext.Current = new HttpContext(new SimpleWorkerRequest(string.Empty, string.Empty, new StringWriter()));
				if (context == null)
				{
					context = HttpContext.Current;
				}
				HttpModuleCollection httpModuleCollection = obj.LoadModules(this);
				HttpModuleCollection httpModuleCollection2 = CreateDynamicModules();
				for (int i = 0; i < httpModuleCollection2.Count; i++)
				{
					httpModuleCollection.AddModule(httpModuleCollection2.GetKey(i), httpModuleCollection2.Get(i));
				}
				Interlocked.CompareExchange(ref modcoll, httpModuleCollection, null);
				HttpContext.Current = current;
				if (full_init)
				{
					HttpApplicationFactory.AttachEvents(this);
					Init();
					fullInitComplete = true;
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = (initialization_exception = ex);
				Console.Error.WriteLine("Exception while initOnce: " + ex2.ToString());
				Console.Error.WriteLine("Please restart your app to unlock it");
			}
			finally
			{
				if (flag)
				{
					context = null;
				}
			}
		}
	}

	internal void TriggerPreSendRequestHeaders()
	{
		if (Events[PreSendRequestHeaders] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	internal void TriggerPreSendRequestContent()
	{
		if (Events[PreSendRequestContent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />. </param>
	public void AddOnAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		AcquireRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
	public void AddOnAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		AuthenticateRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
	public void AddOnAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		AuthorizeRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.BeginRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
	public void AddOnBeginRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		BeginRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.EndRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
	public void AddOnEndRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		EndRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
	public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		PostRequestHandlerExecute += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
	public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		PreRequestHandlerExecute += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
	public void AddOnReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		ReleaseRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handler to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
	public void AddOnResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		ResolveRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
	public void AddOnUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AsyncInvoker @object = new AsyncInvoker(bh, eh, this);
		UpdateRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" />. </param>
	public void AddOnPostAuthenticateRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostAuthenticateRequestAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />.</param>
	public void AddOnPostAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostAuthenticateRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	public void AddOnPostAuthorizeRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostAuthorizeRequestAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAuthorizeRequest" />.</param>
	public void AddOnPostAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostAuthorizeRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
	public void AddOnPostResolveRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostResolveRequestCacheAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostResolveRequestCache" />.</param>
	public void AddOnPostResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostResolveRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
	public void AddOnPostMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostMapRequestHandlerAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostMapRequestHandler" /> collection.</param>
	public void AddOnPostMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostMapRequestHandler += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
	public void AddOnPostAcquireRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostAcquireRequestStateAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostAcquireRequestState" />.</param>
	public void AddOnPostAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostAcquireRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
	public void AddOnPostReleaseRequestStateAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostReleaseRequestStateAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostReleaseRequestState" />.</param>
	public void AddOnPostReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostReleaseRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
	public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostUpdateRequestCacheAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the event. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostUpdateRequestCache" />.</param>
	public void AddOnPostUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostUpdateRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
	public void AddOnAcquireRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		AcquireRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AuthenticateRequest" />.</param>
	public void AddOnAuthenticateRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		AuthenticateRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.AuthorizeRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.AuthorizeRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.AcquireRequestState" />.</param>
	public void AddOnAuthorizeRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		AuthorizeRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.BeginRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.BeginRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.BeginRequest" />.</param>
	public void AddOnBeginRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		BeginRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.EndRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.EndRequest" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.EndRequest" />.</param>
	public void AddOnEndRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		EndRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostRequestHandlerExecute" />.</param>
	public void AddOnPostRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostRequestHandlerExecute += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PreRequestHandlerExecute" /> collection.</param>
	public void AddOnPreRequestHandlerExecuteAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PreRequestHandlerExecute += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.ReleaseRequestState" />.</param>
	public void AddOnReleaseRequestStateAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		ReleaseRequestState += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handler to the collection of asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.ResolveRequestCache" />.</param>
	public void AddOnResolveRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		ResolveRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />. </param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.UpdateRequestCache" />.</param>
	public void AddOnUpdateRequestCacheAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		UpdateRequestCache += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.LogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
	public void AddOnLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnLogRequestAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.LogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.LogRequest" />.</param>
	public void AddOnLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		LogRequest += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
	public void AddOnMapRequestHandlerAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnMapRequestHandlerAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.MapRequestHandler" />.</param>
	public void AddOnMapRequestHandlerAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		MapRequestHandler += @object.Invoke;
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event handlers for the current request.</summary>
	/// <param name="bh">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
	/// <param name="eh">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
	public void AddOnPostLogRequestAsync(BeginEventHandler bh, EndEventHandler eh)
	{
		AddOnPostLogRequestAsync(bh, eh, null);
	}

	/// <summary>Adds the specified <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event to the collection of asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" /> event handlers for the current request.</summary>
	/// <param name="beginHandler">The <see cref="T:System.Web.BeginEventHandler" /> that starts asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
	/// <param name="endHandler">The <see cref="T:System.Web.EndEventHandler" /> that ends asynchronous processing of the <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
	/// <param name="state">The associated state to add to the asynchronous <see cref="E:System.Web.HttpApplication.PostLogRequest" />.</param>
	public void AddOnPostLogRequestAsync(BeginEventHandler beginHandler, EndEventHandler endHandler, object state)
	{
		AsyncInvoker @object = new AsyncInvoker(beginHandler, endHandler, this, state);
		PostLogRequest += @object.Invoke;
	}

	private void AddEventHandler(object key, EventHandler handler)
	{
		if (!fullInitComplete)
		{
			Events.AddHandler(key, handler);
		}
	}

	private void RemoveEventHandler(object key, EventHandler handler)
	{
		if (!fullInitComplete)
		{
			Events.RemoveHandler(key, handler);
		}
	}

	/// <summary>Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
	public void CompleteRequest()
	{
		stop_processing = true;
	}

	internal void DisposeInternal()
	{
		Dispose();
		HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
		Interlocked.Exchange(ref modcoll, httpModuleCollection);
		if (httpModuleCollection != null)
		{
			for (int num = httpModuleCollection.Count - 1; num >= 0; num--)
			{
				httpModuleCollection.Get(num).Dispose();
			}
			httpModuleCollection = null;
		}
		if (nonApplicationEvents[disposedEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
		done.Close();
		done = null;
	}

	/// <summary>Disposes the <see cref="T:System.Web.HttpApplication" /> instance.</summary>
	public virtual void Dispose()
	{
	}

	/// <summary>Gets the name of the default output-cache provider that is configured for a Web site. </summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects that are used to service HTTP requests.</param>
	/// <returns>The name of the default provider.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="context" /> is <see langword="null" /> or is an empty string.</exception>
	public virtual string GetOutputCacheProviderName(HttpContext context)
	{
		return OutputCache.DefaultProviderName;
	}

	/// <summary>Provides an application-wide implementation of the <see cref="P:System.Web.UI.PartialCachingAttribute.VaryByCustom" /> property.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that contains information about the current Web request. </param>
	/// <param name="custom">The custom string that specifies which cached response is used to respond to the current request. </param>
	/// <returns>If the value of the <paramref name="custom" /> parameter is <see langword="&quot;browser&quot;" />, the browser's <see cref="P:System.Web.Configuration.HttpCapabilitiesBase.Type" />; otherwise, <see langword="null" />.</returns>
	public virtual string GetVaryByCustomString(HttpContext context, string custom)
	{
		if (custom == null)
		{
			throw new NullReferenceException();
		}
		if (string.Compare(custom, "browser", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			return context.Request.Browser.Type;
		}
		return null;
	}

	private bool ShouldHandleException(Exception e)
	{
		if (e is ParseException)
		{
			return false;
		}
		return true;
	}

	internal void ProcessError(Exception e)
	{
		bool num = context.Error == null;
		context.AddError(e);
		if (num && ShouldHandleException(e) && nonApplicationEvents[errorEvent] is EventHandler eventHandler)
		{
			try
			{
				eventHandler(this, EventArgs.Empty);
				if (stop_processing)
				{
					context.ClearError();
				}
			}
			catch (ThreadAbortException ex)
			{
				context.ClearError();
				if (FlagEnd.Value == ex.ExceptionState || HttpRuntime.DomainUnloading)
				{
					Thread.ResetAbort();
				}
				else
				{
					context.AddError(ex);
				}
			}
			catch (Exception errorInfo)
			{
				context.AddError(errorInfo);
			}
		}
		stop_processing = true;
		if (e is HttpException ex2 && ex2.GetHttpCode() == 404)
		{
			removeConfigurationFromCache = true;
		}
	}

	internal void Tick()
	{
		try
		{
			if (pipeline.MoveNext() && (bool)pipeline.Current)
			{
				PipelineDone();
			}
		}
		catch (ThreadAbortException ex)
		{
			object exceptionState = ex.ExceptionState;
			Thread.ResetAbort();
			if (exceptionState is StepTimeout)
			{
				ProcessError(HttpException.NewWithCode("The request timed out.", 2002));
			}
			else
			{
				context.ClearError();
				if (FlagEnd.Value != exceptionState && !HttpRuntime.DomainUnloading)
				{
					context.AddError(ex);
				}
			}
			stop_processing = true;
			PipelineDone();
		}
		catch (Exception ex2)
		{
			if (ex2.InnerException is ThreadAbortException ex3 && FlagEnd.Value == ex3.ExceptionState && !HttpRuntime.DomainUnloading)
			{
				context.ClearError();
				Thread.ResetAbort();
			}
			else
			{
				ProcessError(ex2);
			}
			stop_processing = true;
			PipelineDone();
		}
	}

	private void Resume()
	{
		if (in_begin)
		{
			must_yield = false;
		}
		else
		{
			Tick();
		}
	}

	private void async_callback_completed_cb(IAsyncResult ar)
	{
		if (current_ai.end != null)
		{
			try
			{
				current_ai.end(ar);
			}
			catch (Exception e)
			{
				ProcessError(e);
			}
		}
		Resume();
	}

	private void async_handler_complete_cb(IAsyncResult ar)
	{
		IHttpAsyncHandler httpAsyncHandler = ((ar != null) ? (ar.AsyncState as IHttpAsyncHandler) : null);
		try
		{
			httpAsyncHandler?.EndProcessRequest(ar);
		}
		catch (Exception e)
		{
			ProcessError(e);
		}
		Resume();
	}

	private IEnumerable RunHooks(Delegate list)
	{
		Delegate[] invocationList = list.GetInvocationList();
		Delegate[] array = invocationList;
		for (int i = 0; i < array.Length; i++)
		{
			EventHandler d = (EventHandler)array[i];
			if (d.Target != null && d.Target is AsyncInvoker)
			{
				current_ai = (AsyncInvoker)d.Target;
				try
				{
					must_yield = true;
					in_begin = true;
					context.BeginTimeoutPossible();
					current_ai.begin(this, EventArgs.Empty, async_callback_completed_cb, current_ai.data);
				}
				finally
				{
					in_begin = false;
					context.EndTimeoutPossible();
				}
				if (must_yield)
				{
					yield return stop_processing;
				}
				else if (stop_processing)
				{
					yield return true;
				}
			}
			else
			{
				try
				{
					context.BeginTimeoutPossible();
					d(this, EventArgs.Empty);
				}
				finally
				{
					context.EndTimeoutPossible();
				}
				if (stop_processing)
				{
					yield return true;
				}
			}
		}
	}

	private static void FinalErrorWrite(HttpResponse response, string error)
	{
		try
		{
			response.Write(error);
			response.Flush(final_flush: true);
		}
		catch
		{
			response.Close();
		}
	}

	private void OutputPage()
	{
		if (context.Error == null)
		{
			try
			{
				context.Response.Flush(final_flush: true);
			}
			catch (Exception errorInfo)
			{
				context.AddError(errorInfo);
			}
		}
		Exception ex = context.Error;
		if (ex == null)
		{
			return;
		}
		HttpResponse response = context.Response;
		if (!response.HeadersSent)
		{
			response.ClearHeaders();
			response.ClearContent();
			if (ex is HttpException)
			{
				response.StatusCode = ((HttpException)ex).GetHttpCode();
			}
			else
			{
				ex = HttpException.NewWithCode(string.Empty, ex, 3009);
				response.StatusCode = 500;
			}
			HttpException httpEx = (HttpException)ex;
			if (!RedirectCustomError(ref httpEx))
			{
				FinalErrorWrite(response, httpEx.GetHtmlErrorMessage());
			}
			else
			{
				response.Flush(final_flush: true);
			}
		}
		else
		{
			if (!(ex is HttpException))
			{
				ex = HttpException.NewWithCode(string.Empty, ex, 3009);
			}
			FinalErrorWrite(response, ((HttpException)ex).GetHtmlErrorMessage());
		}
	}

	private void PipelineDone()
	{
		try
		{
			if (Events[EndRequest] is EventHandler eventHandler)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}
		catch (Exception e)
		{
			ProcessError(e);
		}
		try
		{
			OutputPage();
		}
		catch (ThreadAbortException e2)
		{
			ProcessError(e2);
			Thread.ResetAbort();
		}
		catch (Exception ex)
		{
			Console.WriteLine("Internal error: OutputPage threw an exception " + ex);
		}
		finally
		{
			context.WorkerRequest.EndOfRequest();
			if (factory != null && context.Handler != null)
			{
				factory.ReleaseHandler(context.Handler);
				context.Handler = null;
				factory = null;
			}
			context.PopHandler();
			pipeline = null;
			current_ai = null;
		}
		PostDone();
		if (begin_iar != null)
		{
			begin_iar.Complete();
		}
		else
		{
			done.Set();
		}
		requests_total_counter.Increment();
	}

	[Conditional("PIPELINE_TIMER")]
	private void StartTimer(string name)
	{
		if (tim == null)
		{
			tim = new Tim();
		}
		tim.Name = name;
		tim.Start();
	}

	[Conditional("PIPELINE_TIMER")]
	private void StopTimer()
	{
		tim.Stop();
	}

	private IEnumerator Pipeline()
	{
		if (stop_processing)
		{
			yield return true;
		}
		context.Request?.Validate();
		context.MapRequestHandlerDone = false;
		Delegate @delegate = Events[BeginRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item in RunHooks(@delegate))
			{
				yield return item;
			}
		}
		@delegate = Events[AuthenticateRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item2 in RunHooks(@delegate))
			{
				yield return item2;
			}
		}
		if (this.DefaultAuthentication != null)
		{
			foreach (bool item3 in RunHooks(this.DefaultAuthentication))
			{
				yield return item3;
			}
		}
		@delegate = Events[PostAuthenticateRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item4 in RunHooks(@delegate))
			{
				yield return item4;
			}
		}
		@delegate = Events[AuthorizeRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item5 in RunHooks(@delegate))
			{
				yield return item5;
			}
		}
		@delegate = Events[PostAuthorizeRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item6 in RunHooks(@delegate))
			{
				yield return item6;
			}
		}
		@delegate = Events[ResolveRequestCache];
		if ((object)@delegate != null)
		{
			foreach (bool item7 in RunHooks(@delegate))
			{
				yield return item7;
			}
		}
		@delegate = Events[PostResolveRequestCache];
		if ((object)@delegate != null)
		{
			foreach (bool item8 in RunHooks(@delegate))
			{
				yield return item8;
			}
		}
		@delegate = Events[MapRequestHandler];
		if ((object)@delegate != null)
		{
			foreach (bool item9 in RunHooks(@delegate))
			{
				yield return item9;
			}
		}
		context.MapRequestHandlerDone = true;
		IHttpHandler handler = null;
		try
		{
			handler = GetHandler(context, context.Request.CurrentExecutionFilePath);
			context.Handler = handler;
			context.PushHandler(handler);
		}
		catch (FileNotFoundException ex)
		{
			if (context.Request.IsLocal)
			{
				ProcessError(HttpException.NewWithCode(404, $"File not found {ex.FileName}", ex, context.Request.FilePath, 3001));
			}
			else
			{
				ProcessError(HttpException.NewWithCode(404, "File not found: " + Path.GetFileName(ex.FileName), context.Request.FilePath, 3001));
			}
		}
		catch (DirectoryNotFoundException innerException)
		{
			if (!context.Request.IsLocal)
			{
				innerException = null;
			}
			ProcessError(HttpException.NewWithCode(404, "Directory not found", innerException, 3001));
		}
		catch (Exception e)
		{
			ProcessError(e);
		}
		if (stop_processing)
		{
			yield return true;
		}
		@delegate = Events[PostMapRequestHandler];
		if ((object)@delegate != null)
		{
			foreach (bool item10 in RunHooks(@delegate))
			{
				yield return item10;
			}
		}
		@delegate = Events[AcquireRequestState];
		if ((object)@delegate != null)
		{
			foreach (bool item11 in RunHooks(@delegate))
			{
				yield return item11;
			}
		}
		@delegate = Events[PostAcquireRequestState];
		if ((object)@delegate != null)
		{
			foreach (bool item12 in RunHooks(@delegate))
			{
				yield return item12;
			}
		}
		@delegate = Events[PreRequestHandlerExecute];
		if ((object)@delegate != null)
		{
			foreach (bool item13 in RunHooks(@delegate))
			{
				if (!item13)
				{
					continue;
				}
				goto IL_09db;
			}
		}
		IHttpHandler handler2 = context.Handler;
		if (handler2 != null && handler != handler2)
		{
			context.PopHandler();
			handler = handler2;
			context.PushHandler(handler);
		}
		try
		{
			context.BeginTimeoutPossible();
			if (handler == null)
			{
				throw new InvalidOperationException("No handler for the current request.");
			}
			if (handler is IHttpAsyncHandler httpAsyncHandler)
			{
				must_yield = true;
				in_begin = true;
				httpAsyncHandler.BeginProcessRequest(context, async_handler_complete_cb, handler);
			}
			else
			{
				must_yield = false;
				handler.ProcessRequest(context);
			}
			if (context.Error != null)
			{
				throw new TargetInvocationException(context.Error);
			}
		}
		finally
		{
			in_begin = false;
			context.EndTimeoutPossible();
		}
		if (must_yield)
		{
			yield return stop_processing;
		}
		else if (stop_processing)
		{
			goto IL_09db;
		}
		@delegate = Events[PostRequestHandlerExecute];
		if ((object)@delegate != null)
		{
			{
				IEnumerator enumerator2 = RunHooks(@delegate).GetEnumerator();
				try
				{
					while (enumerator2.MoveNext() && !(bool)enumerator2.Current)
					{
					}
				}
				finally
				{
					IDisposable disposable = enumerator2 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		goto IL_09db;
		IL_09db:
		@delegate = Events[ReleaseRequestState];
		if ((object)@delegate != null)
		{
			foreach (bool item14 in RunHooks(@delegate))
			{
				_ = item14;
			}
		}
		if (stop_processing)
		{
			yield return true;
		}
		@delegate = Events[PostReleaseRequestState];
		if ((object)@delegate != null)
		{
			foreach (bool item15 in RunHooks(@delegate))
			{
				yield return item15;
			}
		}
		if (context.Error == null)
		{
			context.Response.DoFilter(close: true);
		}
		@delegate = Events[UpdateRequestCache];
		if ((object)@delegate != null)
		{
			foreach (bool item16 in RunHooks(@delegate))
			{
				yield return item16;
			}
		}
		@delegate = Events[PostUpdateRequestCache];
		if ((object)@delegate != null)
		{
			foreach (bool item17 in RunHooks(@delegate))
			{
				yield return item17;
			}
		}
		@delegate = Events[LogRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item18 in RunHooks(@delegate))
			{
				yield return item18;
			}
		}
		@delegate = Events[PostLogRequest];
		if ((object)@delegate != null)
		{
			foreach (bool item19 in RunHooks(@delegate))
			{
				yield return item19;
			}
		}
		PipelineDone();
	}

	internal CultureInfo GetThreadCulture(HttpRequest request, CultureInfo culture, bool isAuto)
	{
		if (!isAuto)
		{
			return culture;
		}
		CultureInfo cultureInfo = null;
		string[] userLanguages = request.UserLanguages;
		try
		{
			if (userLanguages != null && userLanguages.Length != 0)
			{
				cultureInfo = CultureInfo.CreateSpecificCulture(userLanguages[0]);
			}
		}
		catch
		{
		}
		if (cultureInfo == null)
		{
			cultureInfo = culture;
		}
		return cultureInfo;
	}

	private void PreStart()
	{
		GlobalizationSection globalizationSection = (GlobalizationSection)WebConfigurationManager.GetSection("system.web/globalization");
		app_culture = globalizationSection.GetCulture();
		autoCulture = globalizationSection.IsAutoCulture;
		appui_culture = globalizationSection.GetUICulture();
		autoUICulture = globalizationSection.IsAutoUICulture;
		context.StartTimeoutTimer();
		Thread currentThread = Thread.CurrentThread;
		if (app_culture != null)
		{
			prev_app_culture = currentThread.CurrentCulture;
			CultureInfo threadCulture = GetThreadCulture(Request, app_culture, autoCulture);
			if (!threadCulture.Equals(Helpers.InvariantCulture))
			{
				currentThread.CurrentCulture = threadCulture;
			}
		}
		if (appui_culture != null)
		{
			prev_appui_culture = currentThread.CurrentUICulture;
			CultureInfo threadCulture2 = GetThreadCulture(Request, appui_culture, autoUICulture);
			if (!threadCulture2.Equals(Helpers.InvariantCulture))
			{
				currentThread.CurrentUICulture = threadCulture2;
			}
		}
		prev_user = Thread.CurrentPrincipal;
	}

	private void PostDone()
	{
		if (removeConfigurationFromCache)
		{
			WebConfigurationManager.RemoveConfigurationFromCache(context);
			removeConfigurationFromCache = false;
		}
		Thread currentThread = Thread.CurrentThread;
		if (Thread.CurrentPrincipal != prev_user)
		{
			Thread.CurrentPrincipal = prev_user;
		}
		if (prev_appui_culture != null && prev_appui_culture != currentThread.CurrentUICulture)
		{
			currentThread.CurrentUICulture = prev_appui_culture;
		}
		if (prev_app_culture != null && prev_app_culture != currentThread.CurrentCulture)
		{
			currentThread.CurrentCulture = prev_app_culture;
		}
		if (context == null)
		{
			context = HttpContext.Current;
		}
		context.StopTimeoutTimer();
		context.Request.ReleaseResources();
		context.Response.ReleaseResources();
		context = null;
		session = null;
		HttpContext.Current = null;
	}

	private void Start(object x)
	{
		if (x is CultureInfo[] array && array.Length == 2)
		{
			Thread currentThread = Thread.CurrentThread;
			currentThread.CurrentCulture = array[0];
			currentThread.CurrentUICulture = array[1];
		}
		InitOnce(full_init: true);
		if (initialization_exception != null)
		{
			Exception innerException = initialization_exception;
			HttpException ex = HttpException.NewWithCode(string.Empty, innerException, 3001);
			context.Response.StatusCode = 500;
			FinalErrorWrite(context.Response, ex.GetHtmlErrorMessage());
			PipelineDone();
		}
		else
		{
			HttpContext.Current = Context;
			PreStart();
			pipeline = Pipeline();
			Tick();
		}
	}

	internal static Hashtable GetHandlerCache()
	{
		Cache internalCache = HttpRuntime.InternalCache;
		Hashtable hashtable = internalCache["@@HttpHandlerCache@@"] as Hashtable;
		if (hashtable == null)
		{
			hashtable = new Hashtable();
			internalCache.Insert("@@HttpHandlerCache@@", hashtable);
		}
		return hashtable;
	}

	internal static void ClearHandlerCache()
	{
		GetHandlerCache().Clear();
	}

	private object LocateHandler(HttpRequest req, string verb, string url)
	{
		Hashtable handlerCache = GetHandlerCache();
		string key = verb + url;
		object obj = handlerCache[key];
		if (obj != null)
		{
			return obj;
		}
		obj = (WebConfigurationManager.GetSection("system.web/httpHandlers", req.Path, req.Context) as HttpHandlersSection).LocateHandler(verb, url, out var allowCache);
		IHttpHandler httpHandler = obj as IHttpHandler;
		if (allowCache && httpHandler != null && httpHandler.IsReusable)
		{
			handlerCache[key] = obj;
		}
		return obj;
	}

	internal IHttpHandler GetHandler(HttpContext context, string url)
	{
		return GetHandler(context, url, ignoreContextHandler: false);
	}

	internal IHttpHandler GetHandler(HttpContext context, string url, bool ignoreContextHandler)
	{
		if (!ignoreContextHandler && context.Handler != null)
		{
			return context.Handler;
		}
		HttpRequest request = context.Request;
		string requestType = request.RequestType;
		IHttpHandler httpHandler = null;
		object obj = LocateHandler(request, requestType, url);
		factory = obj as IHttpHandlerFactory;
		if (factory == null)
		{
			return (IHttpHandler)obj;
		}
		return factory.GetHandler(context, requestType, url, request.MapPath(url));
	}

	/// <summary>Enables processing of HTTP Web requests by a custom HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to the intrinsic server objects that are used to service HTTP requests.</param>
	/// <exception cref="T:System.Web.HttpException">In all cases.</exception>
	void IHttpHandler.ProcessRequest(HttpContext context)
	{
		begin_iar = null;
		this.context = context;
		done.Reset();
		Start(null);
		done.WaitOne();
	}

	internal void SetContext(HttpContext context)
	{
		this.context = context;
	}

	internal void SetSession(HttpSessionState session)
	{
		this.session = session;
	}

	/// <summary>Initiates an asynchronous call to the HTTP event handler.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects that are used to service HTTP requests.</param>
	/// <param name="cb">The <see cref="T:System.AsyncCallback" /> to call when the asynchronous method call is complete. If the <paramref name="cb" /> parameter is <see langword="null" />, the delegate is not called.</param>
	/// <param name="extraData">Any extra data that is required to process the request.</param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
	IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
	{
		this.context = context;
		done.Reset();
		begin_iar = new AsyncRequestState(done, cb, extraData);
		_ = new CultureInfo[2]
		{
			Thread.CurrentThread.CurrentCulture,
			Thread.CurrentThread.CurrentUICulture
		};
		if (Thread.CurrentThread.IsThreadPoolThread)
		{
			Start(null);
		}
		else
		{
			ThreadPool.QueueUserWorkItem(delegate(object x)
			{
				try
				{
					Start(x);
				}
				catch (Exception value)
				{
					Console.Error.WriteLine(value);
				}
			});
		}
		return begin_iar;
	}

	/// <summary>Provides an asynchronous process <see langword="End" /> method when the process finishes.</summary>
	/// <param name="result">An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process. </param>
	void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
	{
		if (!result.IsCompleted)
		{
			result.AsyncWaitHandle.WaitOne();
		}
		begin_iar = null;
	}

	/// <summary>Executes custom initialization code after all event handler modules have been added.</summary>
	public virtual void Init()
	{
	}

	/// <summary>Registers an application module.</summary>
	/// <param name="moduleType">The type of the module.</param>
	public static void RegisterModule(Type moduleType)
	{
		if (!((HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime")).AllowDynamicModuleRegistration)
		{
			throw new InvalidOperationException("The Application has requested to register a dynamic Module, but dynamic module registration is disabled in web.config.");
		}
		dynamicModuleManeger.Add(moduleType);
	}

	private HttpModuleCollection CreateDynamicModules()
	{
		HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
		foreach (DynamicModuleInfo item in dynamicModuleManeger.LockAndGetModules())
		{
			IHttpModule httpModule = CreateModuleInstance(item.Type);
			httpModule.Init(this);
			httpModuleCollection.AddModule(item.Name, httpModule);
		}
		return httpModuleCollection;
	}

	private IHttpModule CreateModuleInstance(Type type)
	{
		return (IHttpModule)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
	}

	internal void ClearError()
	{
		context.ClearError();
	}

	private bool RedirectErrorPage(string error_page)
	{
		if (context.Request.QueryString["aspxerrorpath"] != null)
		{
			return false;
		}
		Response.Redirect(error_page + "?aspxerrorpath=" + Request.Path, endResponse: false);
		return true;
	}

	private bool RedirectCustomError(ref HttpException httpEx)
	{
		try
		{
			if (!context.IsCustomErrorEnabledUnsafe)
			{
				return false;
			}
			CustomErrorsSection customErrorsSection = (CustomErrorsSection)WebConfigurationManager.GetSection("system.web/customErrors");
			if (customErrorsSection == null)
			{
				if (context.ErrorPage != null)
				{
					return RedirectErrorPage(context.ErrorPage);
				}
				return false;
			}
			string text = customErrorsSection.Errors[context.Response.StatusCode.ToString()]?.Redirect;
			if (text == null)
			{
				text = context.ErrorPage;
				if (text == null)
				{
					text = customErrorsSection.DefaultRedirect;
				}
			}
			if (text == null)
			{
				return false;
			}
			if (customErrorsSection.RedirectMode == CustomErrorsRedirectMode.ResponseRewrite)
			{
				context.Server.Execute(text);
				return true;
			}
			return RedirectErrorPage(text);
		}
		catch (Exception innerException)
		{
			httpEx = HttpException.NewWithCode(500, string.Empty, innerException, 3009);
			return false;
		}
	}

	internal static Type LoadType(string typeName)
	{
		return LoadType(typeName, throwOnMissing: false);
	}

	internal static Type LoadType(string typeName, bool throwOnMissing)
	{
		Type type = Type.GetType(typeName);
		if (type != null)
		{
			return type;
		}
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			type = assemblies[i].GetType(typeName, throwOnError: false);
			if (type != null)
			{
				return type;
			}
		}
		IList topLevelAssemblies = BuildManager.TopLevelAssemblies;
		if (topLevelAssemblies != null && topLevelAssemblies.Count > 0)
		{
			foreach (Assembly item in topLevelAssemblies)
			{
				if (!(item == null))
				{
					type = item.GetType(typeName, throwOnError: false);
					if (type != null)
					{
						return type;
					}
				}
			}
		}
		Exception inner = null;
		try
		{
			type = null;
			type = LoadTypeFromBin(typeName);
		}
		catch (Exception ex)
		{
			inner = ex;
		}
		if (type != null)
		{
			return type;
		}
		if (throwOnMissing)
		{
			throw new TypeLoadException($"Type '{typeName}' cannot be found", inner);
		}
		return null;
	}

	internal static Type LoadType<TBaseType>(string typeName, bool throwOnMissing)
	{
		Type type = LoadType(typeName, throwOnMissing);
		if (typeof(TBaseType).IsAssignableFrom(type))
		{
			return type;
		}
		if (throwOnMissing)
		{
			throw new TypeLoadException($"Type '{typeName}' found but it doesn't derive from base type '{typeof(TBaseType)}'.");
		}
		return null;
	}

	internal static Type LoadTypeFromBin(string typeName)
	{
		Type type = null;
		string[] binDirectoryAssemblies = BinDirectoryAssemblies;
		foreach (string assemblyFile in binDirectoryAssemblies)
		{
			Assembly assembly = null;
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
			type = assembly.GetType(typeName, throwOnError: false);
			if (!(type == null))
			{
				return type;
			}
		}
		return null;
	}

	static HttpApplication()
	{
		PreSendRequestHeaders = new object();
		PreSendRequestContent = new object();
		AcquireRequestState = new object();
		AuthenticateRequest = new object();
		AuthorizeRequest = new object();
		BeginRequest = new object();
		EndRequest = new object();
		PostRequestHandlerExecute = new object();
		PreRequestHandlerExecute = new object();
		ReleaseRequestState = new object();
		ResolveRequestCache = new object();
		UpdateRequestCache = new object();
		PostAuthenticateRequest = new object();
		PostAuthorizeRequest = new object();
		PostResolveRequestCache = new object();
		PostMapRequestHandler = new object();
		PostAcquireRequestState = new object();
		PostReleaseRequestState = new object();
		PostUpdateRequestCache = new object();
		LogRequest = new object();
		MapRequestHandler = new object();
		PostLogRequest = new object();
	}
}
