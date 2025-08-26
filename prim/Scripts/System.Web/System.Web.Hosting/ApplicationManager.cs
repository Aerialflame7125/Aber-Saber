using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Hosting;

/// <summary>Manages ASP.NET application domains for an ASP.NET hosting application.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ApplicationManager : MarshalByRefObject
{
	private static ApplicationManager instance = new ApplicationManager();

	private int users;

	private Dictionary<string, BareApplicationHost> id_to_host;

	private ApplicationManager()
	{
		id_to_host = new Dictionary<string, BareApplicationHost>();
	}

	/// <summary>Shuts down all application domains.</summary>
	public void Close()
	{
		if (Interlocked.Decrement(ref users) == 0)
		{
			ShutdownAll();
		}
	}

	/// <summary>Creates an object for the specified application domain, based on type.</summary>
	/// <param name="appHost">An <see cref="T:System.Web.Hosting.IApplicationHost" /> object.</param>
	/// <param name="type">The type of the object to create.</param>
	/// <returns>A new object of the type specified in <paramref name="type" />.</returns>
	/// <exception cref="T:System.ArgumentException">A physical path for the application does not exist.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appHost" /> is <see langword="null" />.- or -
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	[MonoTODO("Need to take advantage of the configuration mapping capabilities of IApplicationHost")]
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	public IRegisteredObject CreateObject(IApplicationHost appHost, Type type)
	{
		if (appHost == null)
		{
			throw new ArgumentNullException("appHost");
		}
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		return CreateObject(appHost.GetSiteID(), type, appHost.GetVirtualPath(), appHost.GetPhysicalPath(), failIfExists: true, throwOnError: true);
	}

	/// <summary>Creates an object for the specified application domain based on type, virtual and physical paths, and a Boolean value indicating failure behavior when an object of the specified type already exists.</summary>
	/// <param name="appId">The unique identifier for the application that owns the object.</param>
	/// <param name="type">The type of the object to create.</param>
	/// <param name="virtualPath">The virtual path to the application.</param>
	/// <param name="physicalPath">The physical path to the application.</param>
	/// <param name="failIfExists">
	///       <see langword="true" /> to throw an exception if an object of the specified type is currently registered; <see langword="false" /> to return the existing registered object of the specified type.</param>
	/// <returns>A new object of the specified <paramref name="type" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="physicalPath" /> is <see langword="null" />- or -
	///         <paramref name="physicalPath" /> is not a valid application path.- or -
	///         <paramref name="type" /> does not implement the <see cref="T:System.Web.Hosting.IRegisteredObject" /> interface.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appID" /> is <see langword="null" />.- or -
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="failIfExists" /> is <see langword="true" /> and an object of the specified type is already registered.</exception>
	public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists)
	{
		return CreateObject(appId, type, virtualPath, physicalPath, failIfExists, throwOnError: true);
	}

	/// <summary>Creates an object for the specified application domain based on type, virtual and physical paths, a Boolean value indicating failure behavior when an object of the specified type already exists, and a Boolean value indicating whether hosting initialization error exceptions are thrown.</summary>
	/// <param name="appId">The unique identifier for the application that owns the object.</param>
	/// <param name="type">The type of the object to create.</param>
	/// <param name="virtualPath">The virtual path to the application.</param>
	/// <param name="physicalPath">The physical path to the application.</param>
	/// <param name="failIfExists">
	///       <see langword="true" /> to throw an exception if an object of the specified type is currently registered; <see langword="false" /> to return the existing registered object of the specified type.</param>
	/// <param name="throwOnError">
	///       <see langword="true" /> to throw exceptions for hosting initialization errors; <see langword="false" /> to not throw hosting initialization exceptions.</param>
	/// <returns>A new object of the specified <paramref name="type" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="physicalPath" /> is <see langword="null" />- or -
	///         <paramref name="physicalPath" /> is not a valid application path.- or -
	///         <paramref name="type" /> does not implement the <see cref="T:System.Web.Hosting.IRegisteredObject" /> interface.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appID" /> is <see langword="null" />.- or -
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="failIfExists" /> is <see langword="true" /> and an object of the specified type is already registered.</exception>
	public IRegisteredObject CreateObject(string appId, Type type, string virtualPath, string physicalPath, bool failIfExists, bool throwOnError)
	{
		if (appId == null)
		{
			throw new ArgumentNullException("appId");
		}
		if (!VirtualPathUtility.IsAbsolute(virtualPath))
		{
			throw new ArgumentException("Relative path no allowed.", "virtualPath");
		}
		if (string.IsNullOrEmpty(physicalPath))
		{
			throw new ArgumentException("Cannot be null or empty", "physicalPath");
		}
		if (!typeof(IRegisteredObject).IsAssignableFrom(type))
		{
			throw new ArgumentException("Type '" + type.Name + "' does not implement IRegisteredObject.", "type");
		}
		BareApplicationHost bareApplicationHost = null;
		if (id_to_host.ContainsKey(appId))
		{
			bareApplicationHost = id_to_host[appId];
		}
		IRegisteredObject registeredObject = null;
		if (bareApplicationHost != null)
		{
			registeredObject = CheckIfExists(bareApplicationHost, type, failIfExists);
			if (registeredObject != null)
			{
				return registeredObject;
			}
		}
		try
		{
			if (bareApplicationHost == null)
			{
				bareApplicationHost = CreateHost(appId, virtualPath, physicalPath);
			}
			registeredObject = bareApplicationHost.CreateInstance(type);
		}
		catch (Exception)
		{
			if (throwOnError)
			{
				throw;
			}
		}
		if (registeredObject != null && bareApplicationHost.GetObject(type) == null)
		{
			bareApplicationHost.RegisterObject(registeredObject, auto_clean: true);
		}
		return registeredObject;
	}

	internal BareApplicationHost CreateHostWithCheck(string appId, string vpath, string ppath)
	{
		if (id_to_host.ContainsKey(appId))
		{
			throw new InvalidOperationException("Already have a host with the same appId");
		}
		return CreateHost(appId, vpath, ppath);
	}

	private BareApplicationHost CreateHost(string appId, string vpath, string ppath)
	{
		BareApplicationHost bareApplicationHost = (BareApplicationHost)ApplicationHost.CreateApplicationHost(typeof(BareApplicationHost), vpath, ppath);
		bareApplicationHost.Manager = this;
		bareApplicationHost.AppID = appId;
		id_to_host[appId] = bareApplicationHost;
		return bareApplicationHost;
	}

	internal void RemoveHost(string appId)
	{
		id_to_host.Remove(appId);
	}

	private IRegisteredObject CheckIfExists(BareApplicationHost host, Type type, bool failIfExists)
	{
		IRegisteredObject @object = host.GetObject(type);
		if (@object == null)
		{
			return null;
		}
		if (failIfExists)
		{
			throw new InvalidOperationException("Well known object of type '" + type.Name + "' already exists in this domain.");
		}
		return @object;
	}

	/// <summary>Returns the single instance of the <see cref="T:System.Web.Hosting.ApplicationManager" /> object associated with this ASP.NET host process.</summary>
	/// <returns>The single instance of the <see cref="T:System.Web.Hosting.ApplicationManager" /> object associated with the ASP.NET host process that is running.</returns>
	public static ApplicationManager GetApplicationManager()
	{
		return instance;
	}

	/// <summary>Returns the registered object of the specified type from the specified application.</summary>
	/// <param name="appId">The unique identifier for the application that owns the object.</param>
	/// <param name="type">The type of the object to return.</param>
	/// <returns>The registered object of the specified type; or <see langword="null" /> if the type has not been registered through a call to the <see cref="M:System.Web.Hosting.ApplicationManager.CreateObject(System.String,System.Type,System.String,System.String,System.Boolean)" /> method.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appId" /> is <see langword="null" />.—or—
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	public IRegisteredObject GetObject(string appId, Type type)
	{
		if (appId == null)
		{
			throw new ArgumentNullException("appId");
		}
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (!id_to_host.ContainsKey(appId))
		{
			return null;
		}
		return id_to_host[appId].GetObject(type);
	}

	/// <summary>Returns a snapshot of running applications.</summary>
	/// <returns>An array of <see cref="T:System.Web.Hosting.ApplicationInfo" /> objects that contain information about the applications managed by this <see cref="T:System.Web.Hosting.ApplicationManager" /> instance.</returns>
	public ApplicationInfo[] GetRunningApplications()
	{
		Dictionary<string, BareApplicationHost>.KeyCollection keys = id_to_host.Keys;
		string[] array = new string[((ICollection<string>)keys).Count];
		((ICollection<string>)keys).CopyTo(array, 0);
		ApplicationInfo[] array2 = new ApplicationInfo[((ICollection<string>)keys).Count];
		int num = 0;
		string[] array3 = array;
		foreach (string text in array3)
		{
			BareApplicationHost bareApplicationHost = id_to_host[text];
			array2[num++] = new ApplicationInfo(text, bareApplicationHost.PhysicalPath, bareApplicationHost.VirtualPath);
		}
		return array2;
	}

	/// <summary>Gives the application domain an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>Always <see langword="null" />.</returns>
	public override object InitializeLifetimeService()
	{
		return null;
	}

	/// <summary>Returns a value indicating whether all applications hosted by the process are idle and not processing requests.</summary>
	/// <returns>
	///     <see langword="true" /> if all applications in the process are idle; otherwise, <see langword="false" />.</returns>
	public bool IsIdle()
	{
		throw new NotImplementedException();
	}

	/// <summary>Makes a thread-safe increment to the user reference count of the application manager instance.</summary>
	public void Open()
	{
		Interlocked.Increment(ref users);
	}

	/// <summary>Unloads all application resources.</summary>
	public void ShutdownAll()
	{
		Dictionary<string, BareApplicationHost>.KeyCollection keys = id_to_host.Keys;
		string[] array = new string[((ICollection<string>)keys).Count];
		((ICollection<string>)keys).CopyTo(array, 0);
		string[] array2 = array;
		foreach (string key in array2)
		{
			id_to_host[key].Shutdown();
		}
		id_to_host.Clear();
	}

	/// <summary>Unloads the specified application.</summary>
	/// <param name="appId">The unique identifier of the application to unload.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appId" /> is <see langword="null" />.</exception>
	public void ShutdownApplication(string appId)
	{
		if (appId == null)
		{
			throw new ArgumentNullException("appId");
		}
		id_to_host[appId]?.Shutdown();
	}

	/// <summary>Removes the specified object from the list of registered objects in an application. If the object to be removed is the last remaining object in the list of registered objects in an application, the application is unloaded.</summary>
	/// <param name="appId">The unique identifier for the application that owns the object.</param>
	/// <param name="type">The type of the object to unload.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="appId" /> is <see langword="null" />.- or -
	///         <paramref name="type" /> is <see langword="null" />.</exception>
	public void StopObject(string appId, Type type)
	{
		if (appId == null)
		{
			throw new ArgumentNullException("appId");
		}
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (id_to_host.ContainsKey(appId))
		{
			id_to_host[appId]?.StopObject(type);
		}
	}
}
