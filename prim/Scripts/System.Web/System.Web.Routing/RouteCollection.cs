using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web.Hosting;
using System.Web.UI;

namespace System.Web.Routing;

/// <summary>Provides a collection of routes for ASP.NET routing.</summary>
[TypeForwardedFrom("System.Web.Routing, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class RouteCollection : Collection<RouteBase>
{
	private class ReadLockDisposable : IDisposable
	{
		private ReaderWriterLockSlim _rwLock;

		public ReadLockDisposable(ReaderWriterLockSlim rwLock)
		{
			_rwLock = rwLock;
		}

		void IDisposable.Dispose()
		{
			_rwLock.ExitReadLock();
		}
	}

	private class WriteLockDisposable : IDisposable
	{
		private ReaderWriterLockSlim _rwLock;

		public WriteLockDisposable(ReaderWriterLockSlim rwLock)
		{
			_rwLock = rwLock;
		}

		void IDisposable.Dispose()
		{
			_rwLock.ExitWriteLock();
		}
	}

	private sealed class IgnoreRouteInternal : Route
	{
		public IgnoreRouteInternal(string url)
			: base(url, new StopRoutingHandler())
		{
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary routeValues)
		{
			return null;
		}
	}

	private Dictionary<string, RouteBase> _namedMap = new Dictionary<string, RouteBase>(StringComparer.OrdinalIgnoreCase);

	private VirtualPathProvider _vpp;

	private ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

	/// <summary>Gets or sets a value that indicates whether trailing slashes are added when virtual paths are normalized.</summary>
	/// <returns>
	///     <see langword="true" /> if trailing slashes are added; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool AppendTrailingSlash { get; set; }

	/// <summary>Gets or sets a value that indicates whether URLs are converted to lower case when virtual paths are normalized.</summary>
	/// <returns>
	///     <see langword="true" /> to convert URLs to lower case; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool LowercaseUrls { get; set; }

	/// <summary>Gets or sets a value that indicates whether ASP.NET routing should handle URLs that match an existing file.</summary>
	/// <returns>
	///     <see langword="true" /> if ASP.NET routing handles all requests, even those that match an existing file; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool RouteExistingFiles { get; set; }

	private VirtualPathProvider VPP
	{
		get
		{
			if (_vpp == null)
			{
				return HostingEnvironment.VirtualPathProvider;
			}
			return _vpp;
		}
		set
		{
			_vpp = value;
		}
	}

	/// <summary>Gets the route in the collection that has the specified name.</summary>
	/// <param name="name">The value that identifies the route to get.</param>
	/// <returns>An object that has the specified name, or <see langword="null" /> if <paramref name="name" /> is <see langword="null" />, is an empty string, or does not match any route in the collection.</returns>
	public RouteBase this[string name]
	{
		get
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (_namedMap.TryGetValue(name, out var value))
			{
				return value;
			}
			return null;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteCollection" /> class. </summary>
	public RouteCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.RouteCollection" /> class by using the specified virtual path provider. </summary>
	/// <param name="virtualPathProvider">A provider for retrieving resources from a virtual file system.</param>
	public RouteCollection(VirtualPathProvider virtualPathProvider)
	{
		VPP = virtualPathProvider;
	}

	/// <summary>Adds a route to the end of the <see cref="T:System.Web.Routing.RouteCollection" /> object and assigns the specified name to the route.</summary>
	/// <param name="name">The value that identifies the route. The value can be <see langword="null" /> or an empty string.</param>
	/// <param name="item">The route to add to the end of the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="item" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="name" /> is already used in the collection.</exception>
	public void Add(string name, RouteBase item)
	{
		if (item == null)
		{
			throw new ArgumentNullException("item");
		}
		if (!string.IsNullOrEmpty(name) && _namedMap.ContainsKey(name))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("A route named '{0}' is already in the route collection. Route names must be unique."), name), "name");
		}
		Add(item);
		if (!string.IsNullOrEmpty(name))
		{
			_namedMap[name] = item;
		}
		if (item is Route { RouteHandler: not null } route)
		{
			TelemetryLogger.LogHttpHandler(route.RouteHandler.GetType());
		}
	}

	/// <summary>Provides a way to define routes for Web Forms applications.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeUrl">The URL pattern for the route.</param>
	/// <param name="physicalFile">The physical URL for the route.</param>
	/// <returns>The route that is added to the route collection.</returns>
	public Route MapPageRoute(string routeName, string routeUrl, string physicalFile)
	{
		return MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess: true, null, null, null);
	}

	/// <summary>Provides a way to define routes for Web Forms applications.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeUrl">The URL pattern for the route.</param>
	/// <param name="physicalFile">The physical URL for the route.</param>
	/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
	/// <returns>The route that is added to the route collection.</returns>
	public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess)
	{
		return MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, null, null, null);
	}

	/// <summary>Provides a way to define routes for Web Forms applications.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeUrl">The URL pattern for the route.</param>
	/// <param name="physicalFile">The physical URL for the route.</param>
	/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
	/// <param name="defaults">Default values for the route parameters.</param>
	/// <returns>The route that is added to the route collection.</returns>
	public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults)
	{
		return MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, null, null);
	}

	/// <summary>Provides a way to define routes for Web Forms applications.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeUrl">The URL pattern for the route.</param>
	/// <param name="physicalFile">The physical URL for the route.</param>
	/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
	/// <param name="defaults">Default values for the route.</param>
	/// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
	/// <returns>The route that is added to the route collection.</returns>
	public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints)
	{
		return MapPageRoute(routeName, routeUrl, physicalFile, checkPhysicalUrlAccess, defaults, constraints, null);
	}

	/// <summary>Provides a way to define routes for Web Forms applications.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeUrl">The URL pattern for the route.</param>
	/// <param name="physicalFile">The physical URL for the route.</param>
	/// <param name="checkPhysicalUrlAccess">A value that indicates whether ASP.NET should validate that the user has authority to access the physical URL (the route URL is always checked). This parameter sets the <see cref="P:System.Web.Routing.PageRouteHandler.CheckPhysicalUrlAccess" /> property.</param>
	/// <param name="defaults">Default values for the route parameters.</param>
	/// <param name="constraints">Constraints that a URL request must meet in order to be processed as this route.</param>
	/// <param name="dataTokens">Values that are associated with the route that are not used to determine whether a route matches a URL pattern.</param>
	/// <returns>The route that is added to the route collection.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="routeUrl" /> parameter is <see langword="null" />.</exception>
	public Route MapPageRoute(string routeName, string routeUrl, string physicalFile, bool checkPhysicalUrlAccess, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens)
	{
		if (routeUrl == null)
		{
			throw new ArgumentNullException("routeUrl");
		}
		Route route = new Route(routeUrl, defaults, constraints, dataTokens, new PageRouteHandler(physicalFile, checkPhysicalUrlAccess));
		Add(routeName, route);
		return route;
	}

	/// <summary>Removes all the elements from the <see cref="T:System.Web.Routing.RouteCollection" /> object.</summary>
	protected override void ClearItems()
	{
		_namedMap.Clear();
		base.ClearItems();
	}

	/// <summary>Provides an object for managing thread safety when you retrieve an object from the collection.</summary>
	/// <returns>An object that manages thread safety.</returns>
	public IDisposable GetReadLock()
	{
		_rwLock.EnterReadLock();
		return new ReadLockDisposable(_rwLock);
	}

	private RequestContext GetRequestContext(RequestContext requestContext)
	{
		if (requestContext != null)
		{
			return requestContext;
		}
		return new RequestContext(new HttpContextWrapper(HttpContext.Current ?? throw new InvalidOperationException(global::SR.GetString("HttpContext.Current must be non-null when a RequestContext is not provided."))), new RouteData());
	}

	private bool IsRouteToExistingFile(HttpContextBase httpContext)
	{
		string appRelativeCurrentExecutionFilePath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
		if (appRelativeCurrentExecutionFilePath != "~/" && VPP != null)
		{
			if (!VPP.FileExists(appRelativeCurrentExecutionFilePath))
			{
				return VPP.DirectoryExists(appRelativeCurrentExecutionFilePath);
			}
			return true;
		}
		return false;
	}

	/// <summary>Returns information about the route in the collection that matches the specified values.</summary>
	/// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
	/// <returns>An object that contains the values from the route definition.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="context" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.HttpContextBase.Request" /> property of the object in the <paramref name="context" /> parameter is <see langword="null" />.</exception>
	public RouteData GetRouteData(HttpContextBase httpContext)
	{
		if (httpContext == null)
		{
			throw new ArgumentNullException("httpContext");
		}
		if (httpContext.Request == null)
		{
			throw new ArgumentException(global::SR.GetString("The context does not contain any request data."), "httpContext");
		}
		if (base.Count == 0)
		{
			return null;
		}
		bool flag = false;
		bool flag2 = false;
		if (!RouteExistingFiles)
		{
			flag = IsRouteToExistingFile(httpContext);
			flag2 = true;
			if (flag)
			{
				return null;
			}
		}
		using (GetReadLock())
		{
			using IEnumerator<RouteBase> enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				RouteBase current = enumerator.Current;
				RouteData routeData = current.GetRouteData(httpContext);
				if (routeData == null)
				{
					continue;
				}
				if (!current.RouteExistingFiles)
				{
					if (!flag2)
					{
						flag = IsRouteToExistingFile(httpContext);
						flag2 = true;
					}
					if (flag)
					{
						return null;
					}
				}
				return routeData;
			}
		}
		return null;
	}

	private string NormalizeVirtualPath(RequestContext requestContext, string virtualPath)
	{
		string text = System.Web.UI.Util.GetUrlWithApplicationPath(requestContext.HttpContext, virtualPath);
		if (LowercaseUrls || AppendTrailingSlash)
		{
			int num = text.IndexOfAny(new char[2] { '?', '#' });
			string text2;
			string text3;
			if (num >= 0)
			{
				text2 = text.Substring(0, num);
				text3 = text.Substring(num);
			}
			else
			{
				text2 = text;
				text3 = "";
			}
			if (LowercaseUrls)
			{
				text2 = text2.ToLowerInvariant();
			}
			if (AppendTrailingSlash && !text2.EndsWith("/"))
			{
				text2 += "/";
			}
			text = text2 + text3;
		}
		return text;
	}

	/// <summary>Returns information about the URL path that is associated with the route, given the specified context and parameter values.</summary>
	/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
	/// <param name="values">An object that contains the parameters for a route.</param>
	/// <returns>An object that contains information about the URL path that is associated with the route.</returns>
	public VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
	{
		requestContext = GetRequestContext(requestContext);
		using (GetReadLock())
		{
			using IEnumerator<RouteBase> enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				VirtualPathData virtualPath = enumerator.Current.GetVirtualPath(requestContext, values);
				if (virtualPath != null)
				{
					virtualPath.VirtualPath = NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
					return virtualPath;
				}
			}
		}
		return null;
	}

	/// <summary>Returns information about the URL path that is associated with the named route, given the specified context, route name, and parameter values.</summary>
	/// <param name="requestContext">An object that encapsulates information about the requested route.</param>
	/// <param name="name">The name of the route to use when information about the URL path is retrieved.</param>
	/// <param name="values">An object that contains the parameters for a route.</param>
	/// <returns>An object that contains information about the URL path that is associated with the route.</returns>
	/// <exception cref="T:System.ArgumentException">No route could be found that has the name specified in the <paramref name="name" /> parameter.</exception>
	public VirtualPathData GetVirtualPath(RequestContext requestContext, string name, RouteValueDictionary values)
	{
		requestContext = GetRequestContext(requestContext);
		if (!string.IsNullOrEmpty(name))
		{
			bool flag;
			RouteBase value;
			using (GetReadLock())
			{
				flag = _namedMap.TryGetValue(name, out value);
			}
			if (flag)
			{
				VirtualPathData virtualPath = value.GetVirtualPath(requestContext, values);
				if (virtualPath != null)
				{
					virtualPath.VirtualPath = NormalizeVirtualPath(requestContext, virtualPath.VirtualPath);
					return virtualPath;
				}
				return null;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, global::SR.GetString("A route named '{0}' could not be found in the route collection."), name), "name");
		}
		return GetVirtualPath(requestContext, values);
	}

	/// <summary>Provides an object for managing thread safety when you add or remove elements in the collection.</summary>
	/// <returns>An object that manages thread safety.</returns>
	public IDisposable GetWriteLock()
	{
		_rwLock.EnterWriteLock();
		return new WriteLockDisposable(_rwLock);
	}

	/// <summary>Defines a URL pattern that should not be checked for matches against routes.</summary>
	/// <param name="url">The URL pattern to be ignored.</param>
	public void Ignore(string url)
	{
		Ignore(url, null);
	}

	/// <summary>Defines a URL pattern that should not be checked for matches against routes if a request URL meets the specified constraints.</summary>
	/// <param name="url">The URL pattern to be ignored.</param>
	/// <param name="constraints">Additional criteria that determine whether a request that matches the URL pattern will be ignored.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is <see langword="null" />.</exception>
	public void Ignore(string url, object constraints)
	{
		if (url == null)
		{
			throw new ArgumentNullException("url");
		}
		IgnoreRouteInternal item = new IgnoreRouteInternal(url)
		{
			Constraints = new RouteValueDictionary(constraints)
		};
		Add(item);
	}

	/// <summary>Inserts the specified route into the <see cref="T:System.Web.Routing.RouteCollection" /> object at the specified index.</summary>
	/// <param name="index">The zero-based index at which <paramref name="item" /> is inserted.</param>
	/// <param name="item">The route to insert.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="item" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="item" /> is already in the collection.</exception>
	protected override void InsertItem(int index, RouteBase item)
	{
		if (item == null)
		{
			throw new ArgumentNullException("item");
		}
		if (Contains(item))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The route provided already exists in the route collection. The collection may not contain duplicate routes.")), "item");
		}
		base.InsertItem(index, item);
	}

	/// <summary>Removes the route from the <see cref="T:System.Web.Routing.RouteCollection" /> object at the specified index.</summary>
	/// <param name="index">The zero-based index of the route to remove.</param>
	protected override void RemoveItem(int index)
	{
		RemoveRouteName(index);
		base.RemoveItem(index);
	}

	private void RemoveRouteName(int index)
	{
		RouteBase routeBase = base[index];
		foreach (KeyValuePair<string, RouteBase> item in _namedMap)
		{
			if (item.Value == routeBase)
			{
				_namedMap.Remove(item.Key);
				break;
			}
		}
	}

	/// <summary>Replaces the route at the specified index.</summary>
	/// <param name="index">The zero-based index of the route to replace.</param>
	/// <param name="item">The route to add at the specified index.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="item" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="item" /> is already in the collection.</exception>
	protected override void SetItem(int index, RouteBase item)
	{
		if (item == null)
		{
			throw new ArgumentNullException("item");
		}
		if (Contains(item))
		{
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, global::SR.GetString("The route provided already exists in the route collection. The collection may not contain duplicate routes.")), "item");
		}
		RemoveRouteName(index);
		base.SetItem(index, item);
	}
}
