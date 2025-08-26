using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Compilation;
using System.Web.Security;
using System.Web.UI;

namespace System.Web.Routing;

/// <summary>Provides properties and methods for defining how a URL maps to a physical file.</summary>
public class PageRouteHandler : IRouteHandler
{
	private bool _useRouteVirtualPath;

	private Route _routeVirtualPath;

	/// <summary>Gets the virtual path of the Web page that is associated with this route.</summary>
	/// <returns>The URL of the Web page, before substitutions have been applied for any replacement parameters.</returns>
	public string VirtualPath { get; private set; }

	/// <summary>Gets a value that determines whether authorization rules are applied to the physical file's URL.</summary>
	/// <returns>
	///     <see langword="true" /> if authorization is checked for the URL of the physical file that is associated with the route; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool CheckPhysicalUrlAccess { get; private set; }

	private Route RouteVirtualPath
	{
		get
		{
			if (_routeVirtualPath == null)
			{
				_routeVirtualPath = new Route(VirtualPath.Substring(2), this);
			}
			return _routeVirtualPath;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
	/// <param name="virtualPath">The virtual path of the physical file for this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is <see langword="null" /> or is an empty string or does not start with "~/".</exception>
	public PageRouteHandler(string virtualPath)
		: this(virtualPath, checkPhysicalUrlAccess: true)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Routing.PageRouteHandler" /> class. </summary>
	/// <param name="virtualPath">The virtual path of the physical file of this <see cref="P:System.Web.Routing.RouteData.Route" /> object. The file must be located in the current application. Therefore, the path must begin with a tilde (~).</param>
	/// <param name="checkPhysicalUrlAccess">If this property is set to <see langword="false" />, authorization rules will be applied to the request URL and not to the URL of the physical page. If this property is set to <see langword="true" />, authorization rules will be applied to both the request URL and to the URL of the physical page.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="virtualPath" /> parameter is <see langword="null" /> or is an empty string or does not start with "~/".</exception>
	public PageRouteHandler(string virtualPath, bool checkPhysicalUrlAccess)
	{
		if (string.IsNullOrEmpty(virtualPath) || !virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
		{
			throw new ArgumentException(global::SR.GetString("VirtualPath must be a non-empty string starting with ~/."), "virtualPath");
		}
		VirtualPath = virtualPath;
		CheckPhysicalUrlAccess = checkPhysicalUrlAccess;
		_useRouteVirtualPath = VirtualPath.Contains("{");
	}

	private bool CheckUrlAccess(string virtualPath, RequestContext requestContext)
	{
		IPrincipal principal = requestContext.HttpContext.User;
		if (principal == null)
		{
			principal = new GenericPrincipal(new GenericIdentity(string.Empty, string.Empty), new string[0]);
		}
		return CheckUrlAccessWithAssert(virtualPath, requestContext, principal);
	}

	[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
	private bool CheckUrlAccessWithAssert(string virtualPath, RequestContext requestContext, IPrincipal user)
	{
		return UrlAuthorizationModule.CheckUrlAccessForPrincipal(virtualPath, user, requestContext.HttpContext.Request.HttpMethod);
	}

	/// <summary>Returns the object that processes the request.</summary>
	/// <param name="requestContext">An object that encapsulates information about the request.</param>
	/// <returns>The object that processes the request.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is <see langword="null" />.</exception>
	public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
	{
		if (requestContext == null)
		{
			throw new ArgumentNullException("requestContext");
		}
		string text = GetSubstitutedVirtualPath(requestContext);
		int num = text.IndexOf('?');
		if (num != -1)
		{
			text = text.Substring(0, num);
		}
		if (CheckPhysicalUrlAccess && !CheckUrlAccess(text, requestContext))
		{
			return new UrlAuthFailureHandler();
		}
		return BuildManager.CreateInstanceFromVirtualPath(text, typeof(Page)) as Page;
	}

	/// <summary>Returns the virtual path of the physical file for the route after substitutions have been applied to any replacement parameters.</summary>
	/// <param name="requestContext">An object that encapsulates information about the request.</param>
	/// <returns>The URL of the physical file that was generated from a route.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is <see langword="null" />.</exception>
	public string GetSubstitutedVirtualPath(RequestContext requestContext)
	{
		if (requestContext == null)
		{
			throw new ArgumentNullException("requestContext");
		}
		if (!_useRouteVirtualPath)
		{
			return VirtualPath;
		}
		VirtualPathData virtualPath = RouteVirtualPath.GetVirtualPath(requestContext, requestContext.RouteData.Values);
		if (virtualPath == null)
		{
			return VirtualPath;
		}
		return "~/" + virtualPath.VirtualPath;
	}
}
