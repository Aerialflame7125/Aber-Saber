using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security;

/// <summary>Sets the identity of the user for an ASP.NET application when forms authentication is enabled. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class FormsAuthenticationModule : IHttpModule
{
	private static readonly object authenticateEvent = new object();

	private static bool _fAuthChecked;

	private static bool _fAuthRequired;

	private AuthenticationSection _config;

	private bool isConfigInitialized;

	private EventHandlerList events = new EventHandlerList();

	internal static bool FormsAuthRequired => _fAuthRequired;

	/// <summary>Occurs when the application authenticates the current request.</summary>
	public event FormsAuthenticationEventHandler Authenticate
	{
		add
		{
			events.AddHandler(authenticateEvent, value);
		}
		remove
		{
			events.RemoveHandler(authenticateEvent, value);
		}
	}

	private void InitConfig(HttpContext context)
	{
		if (!isConfigInitialized)
		{
			_config = (AuthenticationSection)WebConfigurationManager.GetSection("system.web/authentication");
			if (!_fAuthChecked)
			{
				_fAuthRequired = _config.Mode == AuthenticationMode.Forms;
				_fAuthChecked = true;
			}
			isConfigInitialized = true;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> class. </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public FormsAuthenticationModule()
	{
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.FormsAuthenticationModule" />.</summary>
	public void Dispose()
	{
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthenticationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	public void Init(HttpApplication app)
	{
		app.AuthenticateRequest += OnAuthenticateRequest;
		app.EndRequest += OnEndRequest;
	}

	private void OnAuthenticateRequest(object sender, EventArgs args)
	{
		HttpContext context = ((HttpApplication)sender).Context;
		InitConfig(context);
		if (_config == null || _config.Mode != AuthenticationMode.Forms)
		{
			return;
		}
		string name = _config.Forms.Name;
		string path = _config.Forms.Path;
		string text = _config.Forms.LoginUrl;
		bool slidingExpiration = _config.Forms.SlidingExpiration;
		if (!VirtualPathUtility.IsRooted(text))
		{
			text = "~/" + text;
		}
		string strA = string.Empty;
		string strB = null;
		try
		{
			strA = context.Request.PhysicalPath;
			strB = context.Request.MapPath(text);
		}
		catch
		{
		}
		context.SkipAuthorization = string.Compare(strA, strB, RuntimeHelpers.CaseInsensitive, Helpers.InvariantCulture) == 0;
		string filePath = context.Request.FilePath;
		if (filePath.Length > 15 && string.CompareOrdinal("WebResource.axd", 0, filePath, filePath.Length - 15, 15) == 0)
		{
			context.SkipAuthorization = true;
		}
		FormsAuthenticationEventArgs formsAuthenticationEventArgs = new FormsAuthenticationEventArgs(context);
		if (events[authenticateEvent] is FormsAuthenticationEventHandler formsAuthenticationEventHandler)
		{
			formsAuthenticationEventHandler(this, formsAuthenticationEventArgs);
		}
		bool flag = context.User == null;
		if (formsAuthenticationEventArgs.User != null || !flag)
		{
			if (flag)
			{
				context.User = formsAuthenticationEventArgs.User;
			}
			return;
		}
		HttpCookie httpCookie = context.Request.Cookies[name];
		if (httpCookie == null || (httpCookie.Expires != DateTime.MinValue && httpCookie.Expires < DateTime.Now))
		{
			return;
		}
		FormsAuthenticationTicket formsAuthenticationTicket = null;
		try
		{
			formsAuthenticationTicket = FormsAuthentication.Decrypt(httpCookie.Value);
		}
		catch (ArgumentException)
		{
			return;
		}
		if (formsAuthenticationTicket == null || (!formsAuthenticationTicket.IsPersistent && formsAuthenticationTicket.Expired))
		{
			return;
		}
		FormsAuthenticationTicket formsAuthenticationTicket2 = formsAuthenticationTicket;
		if (slidingExpiration)
		{
			formsAuthenticationTicket = FormsAuthentication.RenewTicketIfOld(formsAuthenticationTicket);
		}
		context.User = new GenericPrincipal(new FormsIdentity(formsAuthenticationTicket), new string[0]);
		if (!(httpCookie.Expires == DateTime.MinValue) || formsAuthenticationTicket2 != formsAuthenticationTicket)
		{
			httpCookie.Value = FormsAuthentication.Encrypt(formsAuthenticationTicket);
			httpCookie.Path = path;
			if (formsAuthenticationTicket.IsPersistent)
			{
				httpCookie.Expires = formsAuthenticationTicket.Expiration;
			}
			context.Response.Cookies.Add(httpCookie);
		}
	}

	private void OnEndRequest(object sender, EventArgs args)
	{
		HttpContext context = ((HttpApplication)sender).Context;
		if (context.Response.StatusCode == 401 && context.Request.QueryString["ReturnUrl"] == null && (context.Response.StatusCode != 401 || !context.Response.SuppressFormsAuthenticationRedirect))
		{
			InitConfig(context);
			string loginUrl = _config.Forms.LoginUrl;
			if (_config != null && _config.Mode == AuthenticationMode.Forms)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(UrlUtils.Combine(context.Request.ApplicationPath, loginUrl));
				stringBuilder.AppendFormat("?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl));
				context.Response.Redirect(stringBuilder.ToString(), endResponse: false);
			}
		}
	}
}
