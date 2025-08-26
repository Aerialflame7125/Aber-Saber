using System.ComponentModel;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Manages a <see cref="T:System.Web.Security.RolePrincipal" /> instance for the current user. This class cannot be inherited.</summary>
public sealed class RoleManagerModule : IHttpModule
{
	private static readonly object getRolesEvent = new object();

	private RoleManagerSection _config;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>A global application event that is raised when the <see cref="T:System.Web.Security.RoleManagerModule" /> is ready to create a <see cref="T:System.Web.Security.RolePrincipal" /> that represents the current user.</summary>
	public event RoleManagerEventHandler GetRoles
	{
		add
		{
			events.AddHandler(getRolesEvent, value);
		}
		remove
		{
			events.RemoveHandler(getRolesEvent, value);
		}
	}

	/// <summary>Called by the HTTP runtime to dispose of the role-manager module.</summary>
	public void Dispose()
	{
	}

	private void ClearCookie(HttpApplication app, string cookieName)
	{
		HttpCookie httpCookie = new HttpCookie(_config.CookieName, "");
		httpCookie.Path = _config.CookiePath;
		httpCookie.Expires = DateTime.MinValue;
		httpCookie.Domain = _config.Domain;
		httpCookie.Secure = _config.CookieRequireSSL;
		app.Response.SetCookie(httpCookie);
	}

	private void OnPostAuthenticateRequest(object sender, EventArgs args)
	{
		HttpApplication httpApplication = (HttpApplication)sender;
		if (_config == null || !_config.Enabled)
		{
			return;
		}
		if (events[getRolesEvent] is RoleManagerEventHandler roleManagerEventHandler)
		{
			RoleManagerEventArgs roleManagerEventArgs = new RoleManagerEventArgs(httpApplication.Context);
			roleManagerEventHandler(this, roleManagerEventArgs);
			if (roleManagerEventArgs.RolesPopulated)
			{
				return;
			}
		}
		HttpCookie httpCookie = httpApplication.Request.Cookies[_config.CookieName];
		IIdentity identity = httpApplication.Context.User.Identity;
		RolePrincipal rolePrincipal;
		if (httpApplication.Request.IsAuthenticated)
		{
			if (httpCookie != null)
			{
				if (!_config.CacheRolesInCookie)
				{
					httpCookie = null;
				}
				else if (_config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection)
				{
					httpCookie = null;
					ClearCookie(httpApplication, _config.CookieName);
				}
			}
			rolePrincipal = ((httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value)) ? new RolePrincipal(identity, httpCookie.Value) : new RolePrincipal(identity));
		}
		else
		{
			if (httpCookie != null)
			{
				ClearCookie(httpApplication, _config.CookieName);
			}
			rolePrincipal = new RolePrincipal(identity);
		}
		httpApplication.Context.User = rolePrincipal;
		Thread.CurrentPrincipal = rolePrincipal;
	}

	private void OnEndRequest(object sender, EventArgs args)
	{
		HttpApplication httpApplication = (HttpApplication)sender;
		if (_config == null || !_config.Enabled || !_config.CacheRolesInCookie || !httpApplication.Request.IsAuthenticated || (_config.CookieRequireSSL && !httpApplication.Request.IsSecureConnection) || !(httpApplication.Context.User is RolePrincipal { CachedListChanged: not false } rolePrincipal))
		{
			return;
		}
		string text = rolePrincipal.ToEncryptedTicket();
		if (text == null || text.Length > 4096)
		{
			ClearCookie(httpApplication, _config.CookieName);
			return;
		}
		HttpCookie httpCookie = new HttpCookie(_config.CookieName, text);
		httpCookie.HttpOnly = true;
		if (!string.IsNullOrEmpty(_config.Domain))
		{
			httpCookie.Domain = _config.Domain;
		}
		if (_config.CookieRequireSSL)
		{
			httpCookie.Secure = true;
		}
		if (_config.CookiePath.Length > 1)
		{
			httpCookie.Path = _config.CookiePath;
		}
		httpApplication.Response.SetCookie(httpCookie);
	}

	/// <summary>Associates the role manager with the specified application.</summary>
	/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> to associate the <see cref="T:System.Web.Security.RoleManagerModule" /> with.</param>
	public void Init(HttpApplication app)
	{
		_config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
		app.PostAuthenticateRequest += OnPostAuthenticateRequest;
		app.EndRequest += OnEndRequest;
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
	public RoleManagerModule()
	{
	}
}
