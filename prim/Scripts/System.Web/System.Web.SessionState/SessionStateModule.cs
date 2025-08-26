using System.ComponentModel;
using System.Configuration;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.SessionState;

/// <summary>Provides session-state services for an application. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class SessionStateModule : IHttpModule
{
	internal const string HeaderName = "AspFilterSessionId";

	internal const string CookielessFlagName = "_SessionIDManager_IsCookieLess";

	private static readonly object startEvent = new object();

	private static readonly object endEvent = new object();

	private SessionStateSection config;

	private SessionStateStoreProviderBase handler;

	private ISessionIDManager idManager;

	private bool supportsExpiration;

	private HttpApplication app;

	private bool storeLocked;

	private TimeSpan storeLockAge;

	private object storeLockId;

	private SessionStateActions storeSessionAction;

	private bool storeIsNew;

	private SessionStateStoreData storeData;

	private HttpSessionStateContainer container;

	private TimeSpan executionTimeout;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>The event that occurs when a session is created.</summary>
	public event EventHandler Start
	{
		add
		{
			events.AddHandler(startEvent, value);
		}
		remove
		{
			events.RemoveHandler(startEvent, value);
		}
	}

	/// <summary>Occurs when a session ends.</summary>
	public event EventHandler End
	{
		add
		{
			events.AddHandler(endEvent, value);
		}
		remove
		{
			events.RemoveHandler(endEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SessionState.SessionStateModule" /> class.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public SessionStateModule()
	{
	}

	/// <summary>Executes final cleanup code before the <see cref="T:System.Web.SessionState.SessionStateModule" /> object is released from memory.</summary>
	public void Dispose()
	{
		app.BeginRequest -= OnBeginRequest;
		app.AcquireRequestState -= OnAcquireRequestState;
		app.ReleaseRequestState -= OnReleaseRequestState;
		app.EndRequest -= OnEndRequest;
		handler.Dispose();
	}

	/// <summary>Calls initialization code when a <see cref="T:System.Web.SessionState.SessionStateModule" /> object is created.</summary>
	/// <param name="app">The current application. </param>
	/// <exception cref="T:System.Web.HttpException">The <see langword="mode" /> attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.StateServer" /> or <see cref="F:System.Web.SessionState.SessionStateMode.SQLServer" />, and the ASP.NET application has less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> trust.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The <see langword="mode" /> attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.Custom" /> and the <see langword="customProvider" /> attribute is empty or does not exist.-or-The <see langword="mode" /> attribute in the sessionState Element (ASP.NET Settings Schema) configuration element is set to <see cref="F:System.Web.SessionState.SessionStateMode.Custom" /> and the provider identified by name in the <see langword="customProvider" /> attribute has not been added to the providers Element for sessionState (ASP.NET Settings Schema) sub-element.</exception>
	[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_XSP_STATIC_SESSION")]
	public void Init(HttpApplication app)
	{
		config = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
		ProviderSettings providerSettings;
		switch (config.Mode)
		{
		case SessionStateMode.Custom:
			providerSettings = config.Providers[config.CustomProvider];
			if (providerSettings == null)
			{
				throw new HttpException($"Cannot find '{config.CustomProvider}' provider.");
			}
			break;
		case SessionStateMode.Off:
			return;
		case SessionStateMode.InProc:
			providerSettings = new ProviderSettings(null, typeof(SessionInProcHandler).AssemblyQualifiedName);
			break;
		case SessionStateMode.SQLServer:
			providerSettings = new ProviderSettings(null, typeof(SessionSQLServerHandler).AssemblyQualifiedName);
			break;
		case SessionStateMode.StateServer:
			providerSettings = new ProviderSettings(null, typeof(SessionStateServerHandler).AssemblyQualifiedName);
			break;
		default:
			throw new NotImplementedException($"The mode '{config.Mode}' is not implemented.");
		}
		handler = (SessionStateStoreProviderBase)ProvidersHelper.InstantiateProvider(providerSettings, typeof(SessionStateStoreProviderBase));
		if (string.IsNullOrEmpty(config.SessionIDManagerType))
		{
			idManager = new SessionIDManager();
		}
		else
		{
			Type type = HttpApplication.LoadType(config.SessionIDManagerType, throwOnMissing: true);
			idManager = (ISessionIDManager)Activator.CreateInstance(type);
		}
		try
		{
			idManager.Initialize();
		}
		catch (Exception innerException)
		{
			throw new HttpException("Failed to initialize session ID manager.", innerException);
		}
		supportsExpiration = handler.SetItemExpireCallback(OnSessionExpired);
		HttpRuntimeSection section = HttpRuntime.Section;
		executionTimeout = section.ExecutionTimeout;
		this.app = app;
		app.BeginRequest += OnBeginRequest;
		app.AcquireRequestState += OnAcquireRequestState;
		app.ReleaseRequestState += OnReleaseRequestState;
		app.EndRequest += OnEndRequest;
	}

	internal static bool IsCookieLess(HttpContext context, SessionStateSection config)
	{
		if (config.Cookieless == HttpCookieMode.UseCookies)
		{
			return false;
		}
		if (config.Cookieless == HttpCookieMode.UseUri)
		{
			return true;
		}
		object obj = context.Items["_SessionIDManager_IsCookieLess"];
		if (obj == null)
		{
			return false;
		}
		return (bool)obj;
	}

	private void OnBeginRequest(object o, EventArgs args)
	{
		HttpContext context = ((HttpApplication)o).Context;
		string filePath = context.Request.FilePath;
		string directory = VirtualPathUtility.GetDirectory(filePath);
		string sessionId = UrlUtils.GetSessionId(directory);
		if (sessionId != null)
		{
			string filePath2 = UrlUtils.RemoveSessionId(directory, filePath);
			context.Request.SetFilePath(filePath2);
			context.Request.SetHeader("AspFilterSessionId", sessionId);
			context.Response.SetAppPathModifier(sessionId);
		}
	}

	private void OnAcquireRequestState(object o, EventArgs args)
	{
		HttpContext context = ((HttpApplication)o).Context;
		if (!(context.Handler is IRequiresSessionState))
		{
			return;
		}
		bool isReadOnly = context.Handler is IReadOnlySessionState;
		if (idManager.InitializeRequest(context, suppressAutoDetectRedirect: false, out var supportSessionIDReissue))
		{
			return;
		}
		string text = idManager.GetSessionID(context);
		handler.InitializeRequest(context);
		storeData = GetStoreData(context, text, isReadOnly);
		storeIsNew = false;
		if (storeData == null && !storeLocked)
		{
			storeIsNew = true;
			text = idManager.CreateSessionID(context);
			idManager.SaveSessionID(context, text, out var redirected, out var _);
			if (redirected)
			{
				if (supportSessionIDReissue)
				{
					handler.CreateUninitializedItem(context, text, (int)config.Timeout.TotalMinutes);
				}
				context.Response.End();
				return;
			}
			storeData = handler.CreateNewStoreData(context, (int)config.Timeout.TotalMinutes);
		}
		else if (storeData == null && storeLocked)
		{
			WaitForStoreUnlock(context, text, isReadOnly);
		}
		else if (storeData != null && !storeLocked && storeSessionAction == SessionStateActions.InitializeItem && IsCookieLess(context, config))
		{
			storeData = handler.CreateNewStoreData(context, (int)config.Timeout.TotalMinutes);
		}
		container = CreateContainer(text, storeData, storeIsNew, isReadOnly);
		SessionStateUtility.AddHttpSessionStateToContext(app.Context, container);
		if (storeIsNew)
		{
			OnSessionStart();
			HttpSessionState session = app.Session;
			if (session != null)
			{
				storeData.Timeout = session.Timeout;
			}
		}
		supportsExpiration = handler.SetItemExpireCallback(OnSessionExpired);
	}

	private void OnReleaseRequestState(object o, EventArgs args)
	{
		HttpContext context = ((HttpApplication)o).Context;
		if (!(context.Handler is IRequiresSessionState))
		{
			return;
		}
		try
		{
			if (!container.IsAbandoned)
			{
				if (!container.IsReadOnly)
				{
					handler.SetAndReleaseItemExclusive(context, container.SessionID, storeData, storeLockId, storeIsNew);
				}
				else
				{
					handler.ReleaseItemExclusive(context, container.SessionID, storeLockId);
				}
				handler.ResetItemTimeout(context, container.SessionID);
			}
			else
			{
				handler.RemoveItem(context, container.SessionID, storeLockId, storeData);
				handler.ReleaseItemExclusive(context, container.SessionID, storeLockId);
				if (supportsExpiration)
				{
					handler.SetItemExpireCallback(null);
				}
				SessionStateUtility.RaiseSessionEnd(container, this, args);
			}
			SessionStateUtility.RemoveHttpSessionStateFromContext(context);
		}
		finally
		{
			container = null;
			storeData = null;
		}
	}

	private void OnEndRequest(object o, EventArgs args)
	{
		if (handler != null)
		{
			if (container != null)
			{
				OnReleaseRequestState(o, args);
			}
			if (o is HttpApplication httpApplication && handler != null)
			{
				handler.EndRequest(httpApplication.Context);
			}
		}
	}

	private SessionStateStoreData GetStoreData(HttpContext context, string sessionId, bool isReadOnly)
	{
		SessionStateStoreData result = (isReadOnly ? handler.GetItem(context, sessionId, out storeLocked, out storeLockAge, out storeLockId, out storeSessionAction) : handler.GetItemExclusive(context, sessionId, out storeLocked, out storeLockAge, out storeLockId, out storeSessionAction));
		if (storeLockId == null)
		{
			storeLockId = 0;
		}
		return result;
	}

	private void WaitForStoreUnlock(HttpContext context, string sessionId, bool isReadOnly)
	{
		DateTime now = DateTime.Now;
		while (DateTime.Now - now < executionTimeout)
		{
			Thread.Sleep(500);
			storeData = GetStoreData(context, sessionId, isReadOnly);
			if (storeData == null && storeLocked && storeLockAge > executionTimeout)
			{
				handler.ReleaseItemExclusive(context, sessionId, storeLockId);
				break;
			}
			if (storeData != null && !storeLocked)
			{
				break;
			}
		}
	}

	private HttpSessionStateContainer CreateContainer(string sessionId, SessionStateStoreData data, bool isNew, bool isReadOnly)
	{
		if (data == null)
		{
			return new HttpSessionStateContainer(sessionId, null, null, 0, isNew, config.Cookieless, config.Mode, isReadOnly);
		}
		return new HttpSessionStateContainer(sessionId, data.Items, data.StaticObjects, data.Timeout, isNew, config.Cookieless, config.Mode, isReadOnly);
	}

	private void OnSessionExpired(string id, SessionStateStoreData item)
	{
		SessionStateUtility.RaiseSessionEnd(CreateContainer(id, item, isNew: false, isReadOnly: true), this, EventArgs.Empty);
	}

	private void OnSessionStart()
	{
		if (events[startEvent] is EventHandler eventHandler)
		{
			eventHandler(this, EventArgs.Empty);
		}
	}
}
