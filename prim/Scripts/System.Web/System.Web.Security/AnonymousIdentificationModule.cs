using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Manages anonymous identifiers for the ASP.NET application.</summary>
public sealed class AnonymousIdentificationModule : IHttpModule
{
	private static readonly object creatingEvent = new object();

	private HttpApplication app;

	private EventHandlerList events = new EventHandlerList();

	private static AnonymousIdentificationSection Config = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");

	/// <summary>Gets a value indicating whether anonymous identification is enabled for the ASP.NET application.</summary>
	/// <returns>
	///     <see langword="true" /> if anonymous identification is enabled for the ASP.NET application; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool Enabled
	{
		get
		{
			if (Config == null)
			{
				return false;
			}
			return Config.Enabled;
		}
	}

	/// <summary>Occurs when a new anonymous identifier is created.</summary>
	public event AnonymousIdentificationEventHandler Creating
	{
		add
		{
			events.AddHandler(creatingEvent, value);
		}
		remove
		{
			events.RemoveHandler(creatingEvent, value);
		}
	}

	/// <summary>Clears the anonymous cookie or identifier associated with a session.</summary>
	/// <exception cref="T:System.NotSupportedException">Calling <see cref="M:System.Web.Security.AnonymousIdentificationModule.ClearAnonymousIdentifier" /> when the anonymous identification is not enabled.-or-The user for the current request is anonymous.</exception>
	public static void ClearAnonymousIdentifier()
	{
		if (Config == null || !Config.Enabled)
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.AnonymousIdentificationModule" />.</summary>
	public void Dispose()
	{
		app.PostAuthenticateRequest -= OnEnter;
		app = null;
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.AnonymousIdentificationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	public void Init(HttpApplication app)
	{
		this.app = app;
		app.PostAuthenticateRequest += OnEnter;
	}

	[MonoTODO("cookieless userid")]
	private void OnEnter(object source, EventArgs eventArgs)
	{
		if (!Enabled)
		{
			return;
		}
		string text = null;
		HttpCookie httpCookie = app.Request.Cookies[Config.CookieName];
		if (httpCookie != null && (httpCookie.Expires == DateTime.MinValue || httpCookie.Expires > DateTime.Now))
		{
			try
			{
				text = Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value));
			}
			catch
			{
			}
		}
		if (text == null)
		{
			if (events[creatingEvent] is AnonymousIdentificationEventHandler anonymousIdentificationEventHandler)
			{
				AnonymousIdentificationEventArgs anonymousIdentificationEventArgs = new AnonymousIdentificationEventArgs(HttpContext.Current);
				anonymousIdentificationEventHandler(this, anonymousIdentificationEventArgs);
				text = anonymousIdentificationEventArgs.AnonymousID;
			}
			if (text == null)
			{
				text = Guid.NewGuid().ToString();
			}
			HttpCookie httpCookie2 = new HttpCookie(Config.CookieName);
			httpCookie2.Path = app.Request.ApplicationPath;
			httpCookie2.Expires = DateTime.Now + Config.CookieTimeout;
			httpCookie2.Value = Convert.ToBase64String(Encoding.Unicode.GetBytes(text));
			app.Response.AppendCookie(httpCookie2);
		}
		app.Request.AnonymousID = text;
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.AnonymousIdentificationModule" /> class.</summary>
	public AnonymousIdentificationModule()
	{
	}
}
