using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Configuration.nBrowser;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web;

internal sealed class HttpApplicationFactory
{
	private object this_lock = new object();

	private static HttpApplicationFactory theFactory = new HttpApplicationFactory();

	private object session_end;

	private bool needs_init = true;

	private bool app_start_needed = true;

	private bool have_app_events;

	private Type app_type;

	private HttpApplicationState app_state;

	private Hashtable app_event_handlers;

	private static ArrayList watchers = new ArrayList();

	private static object watchers_lock = new object();

	private static bool app_shutdown = false;

	private static bool app_disabled = false;

	private static string[] app_browsers_files = new string[0];

	private static string[] default_machine_browsers_files = new string[0];

	private static string[] app_mono_machine_browsers_files = new string[0];

	private Stack available = new Stack();

	private object next_free;

	private Stack available_for_end = new Stack();

	private static Build capabilities_processor = null;

	private static object capabilities_processor_lock = new object();

	internal static HttpApplicationState ApplicationState
	{
		get
		{
			if (theFactory.app_state == null)
			{
				HttpStaticObjectsCollection appObj = MakeStaticCollection(GlobalAsaxCompiler.ApplicationObjects);
				HttpStaticObjectsCollection sessionObj = MakeStaticCollection(GlobalAsaxCompiler.SessionObjects);
				theFactory.app_state = new HttpApplicationState(appObj, sessionObj);
			}
			return theFactory.app_state;
		}
	}

	internal static Type AppType => theFactory.app_type;

	internal static bool ContextAvailable
	{
		get
		{
			if (theFactory != null)
			{
				return !theFactory.app_start_needed;
			}
			return false;
		}
	}

	internal static bool ApplicationDisabled
	{
		get
		{
			return app_disabled;
		}
		set
		{
			app_disabled = value;
		}
	}

	internal static string[] AppBrowsersFiles => app_browsers_files;

	internal static ICapabilitiesProcess CapabilitiesProcessor
	{
		get
		{
			lock (capabilities_processor_lock)
			{
				if (capabilities_processor == null)
				{
					capabilities_processor = new Build();
					string[] array = app_mono_machine_browsers_files;
					if (array.Length == 0)
					{
						array = default_machine_browsers_files;
					}
					string[] array2 = array;
					foreach (string fileName in array2)
					{
						capabilities_processor.AddBrowserFile(fileName);
					}
					array2 = app_browsers_files;
					foreach (string fileName2 in array2)
					{
						capabilities_processor.AddBrowserFile(fileName2);
					}
				}
			}
			return capabilities_processor;
		}
	}

	private bool IsEventHandler(MethodInfo m)
	{
		int num = m.Name.IndexOf('_');
		if (num == -1 || m.Name.Length - 1 <= num)
		{
			return false;
		}
		if (m.ReturnType != typeof(void))
		{
			return false;
		}
		ParameterInfo[] parameters = m.GetParameters();
		switch (parameters.Length)
		{
		case 0:
			return true;
		default:
			return false;
		case 2:
			if (parameters[0].ParameterType != typeof(object) || !typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType))
			{
				return false;
			}
			return true;
		}
	}

	private void AddEvent(MethodInfo method, Hashtable appTypeEventHandlers)
	{
		string key = method.Name.Replace("_On", "_");
		if (appTypeEventHandlers[key] == null)
		{
			appTypeEventHandlers[key] = method;
			return;
		}
		MethodInfo methodInfo = appTypeEventHandlers[key] as MethodInfo;
		ArrayList arrayList;
		if (methodInfo != null)
		{
			arrayList = new ArrayList(4);
			arrayList.Add(methodInfo);
			appTypeEventHandlers[key] = arrayList;
		}
		else
		{
			arrayList = appTypeEventHandlers[key] as ArrayList;
		}
		arrayList.Add(method);
	}

	private ArrayList GetMethodsDeep(Type type)
	{
		ArrayList arrayList = new ArrayList();
		MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		arrayList.AddRange(methods);
		Type baseType = type.BaseType;
		while (baseType != null)
		{
			methods = baseType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			arrayList.AddRange(methods);
			baseType = baseType.BaseType;
		}
		return arrayList;
	}

	private Hashtable GetApplicationTypeEvents(Type type)
	{
		if (have_app_events)
		{
			return app_event_handlers;
		}
		lock (this_lock)
		{
			if (app_event_handlers != null)
			{
				return app_event_handlers;
			}
			app_event_handlers = new Hashtable();
			ArrayList methodsDeep = GetMethodsDeep(type);
			Hashtable hashtable = null;
			foreach (object item in methodsDeep)
			{
				MethodInfo methodInfo = item as MethodInfo;
				if (methodInfo.DeclaringType != typeof(HttpApplication) && IsEventHandler(methodInfo))
				{
					string key = methodInfo.ToString();
					if (hashtable == null)
					{
						hashtable = new Hashtable();
					}
					else if (hashtable.ContainsKey(key))
					{
						continue;
					}
					hashtable.Add(key, methodInfo);
					AddEvent(methodInfo, app_event_handlers);
				}
			}
			hashtable = null;
			have_app_events = true;
		}
		return app_event_handlers;
	}

	private Hashtable GetApplicationTypeEvents(HttpApplication app)
	{
		if (have_app_events)
		{
			return app_event_handlers;
		}
		return GetApplicationTypeEvents(app.GetType());
	}

	private bool FireEvent(string method_name, object target, object[] args)
	{
		MethodInfo methodInfo = GetApplicationTypeEvents((HttpApplication)target)[method_name] as MethodInfo;
		if (methodInfo == null)
		{
			return false;
		}
		if (methodInfo.GetParameters().Length == 0)
		{
			args = null;
		}
		methodInfo.Invoke(target, args);
		return true;
	}

	private HttpApplication FireOnAppStart(HttpContext context)
	{
		HttpApplication httpApplication2 = (context.ApplicationInstance = (HttpApplication)Activator.CreateInstance(app_type, nonPublic: true));
		httpApplication2.SetContext(context);
		object[] args = new object[2]
		{
			httpApplication2,
			EventArgs.Empty
		};
		httpApplication2.InApplicationStart = true;
		FireEvent("Application_Start", httpApplication2, args);
		httpApplication2.InApplicationStart = false;
		return httpApplication2;
	}

	private void FireOnAppEnd()
	{
		if (!(app_type == null))
		{
			HttpApplication httpApplication = (HttpApplication)Activator.CreateInstance(app_type, nonPublic: true);
			FireEvent("Application_End", httpApplication, new object[2]
			{
				new object(),
				EventArgs.Empty
			});
			httpApplication.DisposeInternal();
			app_type = null;
		}
	}

	public static void Dispose()
	{
		theFactory.FireOnAppEnd();
	}

	private static FileSystemWatcher CreateWatcher(string file, FileSystemEventHandler hnd, RenamedEventHandler reh)
	{
		FileSystemWatcher obj = new FileSystemWatcher
		{
			Path = Path.GetFullPath(Path.GetDirectoryName(file)),
			Filter = Path.GetFileName(file)
		};
		obj.NotifyFilter |= NotifyFilters.Size;
		obj.Changed += hnd;
		obj.Created += hnd;
		obj.Deleted += hnd;
		obj.Renamed += reh;
		obj.EnableRaisingEvents = true;
		return obj;
	}

	internal static void AttachEvents(HttpApplication app)
	{
		HttpApplicationFactory httpApplicationFactory = theFactory;
		Hashtable applicationTypeEvents = httpApplicationFactory.GetApplicationTypeEvents(app);
		foreach (string key2 in applicationTypeEvents.Keys)
		{
			int num = key2.IndexOf('_');
			string text2 = key2.Substring(0, num);
			object obj;
			if (text2 == "Application")
			{
				obj = app;
			}
			else
			{
				obj = app.Modules[text2];
				if (obj == null)
				{
					continue;
				}
			}
			string text3 = key2.Substring(num + 1);
			EventInfo @event = obj.GetType().GetEvent(text3);
			if (@event == null)
			{
				continue;
			}
			string key = text2 + "_" + text3;
			object obj2 = applicationTypeEvents[key];
			if (obj2 == null)
			{
				continue;
			}
			if (text3 == "End" && text2 == "Session")
			{
				Interlocked.CompareExchange(ref httpApplicationFactory.session_end, obj2, null);
				continue;
			}
			if (obj2 is MethodInfo)
			{
				httpApplicationFactory.AddHandler(@event, obj, app, (MethodInfo)obj2);
				continue;
			}
			foreach (MethodInfo item in (ArrayList)obj2)
			{
				httpApplicationFactory.AddHandler(@event, obj, app, item);
			}
		}
	}

	private void AddHandler(EventInfo evt, object target, HttpApplication app, MethodInfo method)
	{
		if (method.GetParameters().Length == 0)
		{
			NoParamsInvoker noParamsInvoker = new NoParamsInvoker(app, method);
			evt.AddEventHandler(target, noParamsInvoker.FakeDelegate);
		}
		else if (method.IsStatic)
		{
			evt.AddEventHandler(target, Delegate.CreateDelegate(evt.EventHandlerType, method));
		}
		else
		{
			evt.AddEventHandler(target, Delegate.CreateDelegate(evt.EventHandlerType, app, method));
		}
	}

	internal static void InvokeSessionEnd(object state)
	{
		InvokeSessionEnd(state, null, EventArgs.Empty);
	}

	internal static void InvokeSessionEnd(object state, object source, EventArgs e)
	{
		HttpApplicationFactory httpApplicationFactory = theFactory;
		MethodInfo methodInfo = null;
		HttpApplication httpApplication = null;
		lock (httpApplicationFactory.available_for_end)
		{
			methodInfo = (MethodInfo)httpApplicationFactory.session_end;
			if (methodInfo == null)
			{
				return;
			}
			httpApplication = GetApplicationForSessionEnd();
		}
		httpApplication.SetSession((HttpSessionState)state);
		try
		{
			methodInfo.Invoke(httpApplication, new object[2]
			{
				(source == null) ? httpApplication : source,
				e
			});
		}
		catch (Exception)
		{
		}
		RecycleForSessionEnd(httpApplication);
	}

	private static HttpStaticObjectsCollection MakeStaticCollection(ArrayList list)
	{
		if (list == null || list.Count == 0)
		{
			return null;
		}
		HttpStaticObjectsCollection httpStaticObjectsCollection = new HttpStaticObjectsCollection();
		foreach (ObjectTagBuilder item in list)
		{
			httpStaticObjectsCollection.Add(item);
		}
		return httpStaticObjectsCollection;
	}

	private void InitType(HttpContext context)
	{
		lock (this_lock)
		{
			if (!needs_init)
			{
				return;
			}
			try
			{
				string appDomainAppPath = HttpRuntime.AppDomainAppPath;
				string text = null;
				text = Path.Combine(appDomainAppPath, "Global.asax");
				if (!System.IO.File.Exists(text))
				{
					text = Path.Combine(appDomainAppPath, "global.asax");
					if (!System.IO.File.Exists(text))
					{
						text = null;
					}
				}
				BuildManager.CallPreStartMethods();
				BuildManager.CompilingTopLevelAssemblies = true;
				new AppResourcesCompiler(context).Compile();
				new AppWebReferencesCompiler().Compile();
				new AppCodeCompiler().Compile();
				BuildManager.AllowReferencedAssembliesCaching = true;
				string path = Path.Combine(HttpRuntime.MachineConfigurationDirectory, "Browsers");
				default_machine_browsers_files = new string[0];
				if (Directory.Exists(path))
				{
					default_machine_browsers_files = Directory.GetFiles(path, "*.browser");
				}
				string path2 = Path.Combine(Path.Combine(appDomainAppPath, "App_Data"), "Mono_Machine_Browsers");
				app_mono_machine_browsers_files = new string[0];
				if (Directory.Exists(path2))
				{
					app_mono_machine_browsers_files = Directory.GetFiles(path2, "*.browser");
				}
				string path3 = Path.Combine(appDomainAppPath, "App_Browsers");
				app_browsers_files = new string[0];
				if (Directory.Exists(path3))
				{
					app_browsers_files = Directory.GetFiles(path3, "*.browser");
				}
				BuildManager.CompilingTopLevelAssemblies = false;
				app_type = BuildManager.GetPrecompiledApplicationType();
				if (app_type == null && text != null)
				{
					app_type = BuildManager.GetCompiledType("~/" + Path.GetFileName(text));
					if (app_type == null)
					{
						throw new ApplicationException($"Error compiling application file ({text}).");
					}
				}
				else if (app_type == null)
				{
					app_type = typeof(HttpApplication);
					app_state = new HttpApplicationState();
				}
				WatchLocationForRestart("?lobal.asax");
				ThreadPool.QueueUserWorkItem(delegate
				{
					try
					{
						WatchLocationForRestart(string.Empty, "?eb.?onfig", watchSubdirs: true);
					}
					catch (Exception value)
					{
						Console.Error.WriteLine(value);
					}
				}, null);
				needs_init = false;
			}
			catch (Exception)
			{
				if (BuildManager.CodeAssemblies != null)
				{
					BuildManager.CodeAssemblies.Clear();
				}
				if (BuildManager.TopLevelAssemblies != null)
				{
					BuildManager.TopLevelAssemblies.Clear();
				}
				if (WebConfigurationManager.ExtraAssemblies != null)
				{
					WebConfigurationManager.ExtraAssemblies.Clear();
				}
				throw;
			}
		}
	}

	internal static HttpApplication GetApplication(HttpContext context)
	{
		HttpApplicationFactory httpApplicationFactory = theFactory;
		HttpApplication httpApplication = null;
		if (httpApplicationFactory.app_start_needed)
		{
			if (context == null)
			{
				return null;
			}
			httpApplicationFactory.InitType(context);
			lock (httpApplicationFactory)
			{
				if (httpApplicationFactory.app_start_needed)
				{
					string[] binDirs = HttpApplication.BinDirs;
					for (int i = 0; i < binDirs.Length; i++)
					{
						WatchLocationForRestart(binDirs[i], "*.dll");
					}
					WatchLocationForRestart(".", "App_Code");
					WatchLocationForRestart(".", "App_Browsers");
					WatchLocationForRestart(".", "App_GlobalResources");
					WatchLocationForRestart("App_Code", "*", watchSubdirs: true);
					WatchLocationForRestart("App_Browsers", "*");
					WatchLocationForRestart("App_GlobalResources", "*");
					httpApplication = httpApplicationFactory.FireOnAppStart(context);
					httpApplicationFactory.app_start_needed = false;
					return httpApplication;
				}
			}
		}
		httpApplication = (HttpApplication)Interlocked.Exchange(ref httpApplicationFactory.next_free, null);
		if (httpApplication != null)
		{
			httpApplication.RequestCompleted = false;
			return httpApplication;
		}
		lock (httpApplicationFactory.available)
		{
			if (httpApplicationFactory.available.Count > 0)
			{
				httpApplication = (HttpApplication)httpApplicationFactory.available.Pop();
				httpApplication.RequestCompleted = false;
				return httpApplication;
			}
		}
		return (HttpApplication)Activator.CreateInstance(httpApplicationFactory.app_type, nonPublic: true);
	}

	private static HttpApplication GetApplicationForSessionEnd()
	{
		HttpApplicationFactory httpApplicationFactory = theFactory;
		if (httpApplicationFactory.available_for_end.Count > 0)
		{
			return (HttpApplication)httpApplicationFactory.available_for_end.Pop();
		}
		HttpApplication obj = (HttpApplication)Activator.CreateInstance(httpApplicationFactory.app_type, nonPublic: true);
		obj.InitOnce(full_init: false);
		return obj;
	}

	internal static void RecycleForSessionEnd(HttpApplication app)
	{
		bool flag = false;
		HttpApplicationFactory httpApplicationFactory = theFactory;
		lock (httpApplicationFactory.available_for_end)
		{
			if (httpApplicationFactory.available_for_end.Count < 64)
			{
				httpApplicationFactory.available_for_end.Push(app);
			}
			else
			{
				flag = true;
			}
		}
		if (flag)
		{
			app.Dispose();
		}
	}

	internal static void Recycle(HttpApplication app)
	{
		bool flag = false;
		HttpApplicationFactory httpApplicationFactory = theFactory;
		if (Interlocked.CompareExchange(ref httpApplicationFactory.next_free, app, null) == null)
		{
			return;
		}
		lock (httpApplicationFactory.available)
		{
			if (httpApplicationFactory.available.Count < 64)
			{
				httpApplicationFactory.available.Push(app);
			}
			else
			{
				flag = true;
			}
		}
		if (flag)
		{
			app.Dispose();
		}
	}

	internal static bool WatchLocationForRestart(string filter)
	{
		return WatchLocationForRestart(string.Empty, filter, watchSubdirs: false);
	}

	internal static bool WatchLocationForRestart(string virtualPath, string filter)
	{
		return WatchLocationForRestart(virtualPath, filter, watchSubdirs: false);
	}

	internal static bool WatchLocationForRestart(string virtualPath, string filter, bool watchSubdirs)
	{
		string appDomainAppPath = HttpRuntime.AppDomainAppPath;
		appDomainAppPath = Path.Combine(appDomainAppPath, virtualPath);
		bool flag = Directory.Exists(appDomainAppPath);
		bool flag2 = !flag && System.IO.File.Exists(appDomainAppPath);
		if (flag || flag2)
		{
			FileSystemEventHandler hnd = OnFileChanged;
			RenamedEventHandler reh = OnFileRenamed;
			FileSystemWatcher fileSystemWatcher = CreateWatcher(Path.Combine(appDomainAppPath, filter), hnd, reh);
			if (flag)
			{
				fileSystemWatcher.IncludeSubdirectories = watchSubdirs;
			}
			lock (watchers_lock)
			{
				watchers.Add(fileSystemWatcher);
			}
			return true;
		}
		return false;
	}

	internal static void DisableWatchers()
	{
		lock (watchers_lock)
		{
			foreach (FileSystemWatcher watcher in watchers)
			{
				watcher.EnableRaisingEvents = false;
			}
		}
	}

	internal static void DisableWatcher(string virtualPath, string filter)
	{
		EnableWatcherEvents(virtualPath, filter, enable: false);
	}

	internal static void EnableWatcher(string virtualPath, string filter)
	{
		EnableWatcherEvents(virtualPath, filter, enable: true);
	}

	private static void EnableWatcherEvents(string virtualPath, string filter, bool enable)
	{
		lock (watchers_lock)
		{
			foreach (FileSystemWatcher watcher in watchers)
			{
				if (string.Compare(watcher.Path, virtualPath, StringComparison.Ordinal) == 0 && string.Compare(watcher.Filter, filter, StringComparison.Ordinal) == 0)
				{
					watcher.EnableRaisingEvents = enable;
				}
			}
		}
	}

	internal static void EnableWatchers()
	{
		lock (watchers_lock)
		{
			foreach (FileSystemWatcher watcher in watchers)
			{
				watcher.EnableRaisingEvents = true;
			}
		}
	}

	private static void OnFileRenamed(object sender, RenamedEventArgs args)
	{
		OnFileChanged(sender, args);
	}

	private static void OnFileChanged(object sender, FileSystemEventArgs args)
	{
		if (HttpRuntime.DomainUnloading)
		{
			return;
		}
		string name = args.Name;
		bool flag = false;
		if (StrUtils.EndsWith(name, "onfig", ignore_case: true))
		{
			if (string.Compare(Path.GetFileName(name), "web.config", ignoreCase: true, Helpers.InvariantCulture) != 0)
			{
				return;
			}
			flag = true;
		}
		else if (StrUtils.EndsWith(name, "lobal.asax", ignore_case: true) && string.Compare(name, "global.asax", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			return;
		}
		Console.WriteLine("Change: " + name);
		if ((sender is FileSystemWatcher fileSystemWatcher && string.Compare(fileSystemWatcher.Filter, "?eb.?onfig", ignoreCase: true, Helpers.InvariantCulture) == 0 && Directory.Exists(name)) || (flag && WebConfigurationManager.SuppressAppReload(newValue: true)))
		{
			return;
		}
		lock (watchers_lock)
		{
			if (!app_shutdown)
			{
				app_shutdown = true;
				DisableWatchers();
				HttpRuntime.UnloadAppDomain();
			}
		}
	}
}
