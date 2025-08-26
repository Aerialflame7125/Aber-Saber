using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Profile;

/// <summary>Manages the creation of the user profile and profile events. This class cannot be inherited.</summary>
public sealed class ProfileModule : IHttpModule
{
	private static readonly object migrateAnonymousEvent = new object();

	private static readonly object personalizeEvent = new object();

	private static readonly object profileAutoSavingEvent = new object();

	private HttpApplication app;

	private ProfileBase profile;

	private string anonymousCookieName;

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Occurs when the anonymous user for a profile logs in.</summary>
	public event ProfileMigrateEventHandler MigrateAnonymous
	{
		add
		{
			events.AddHandler(migrateAnonymousEvent, value);
		}
		remove
		{
			events.RemoveHandler(migrateAnonymousEvent, value);
		}
	}

	/// <summary>Occurs before the user profile is created.</summary>
	[MonoTODO("implement event rising")]
	public event ProfileEventHandler Personalize
	{
		add
		{
			events.AddHandler(personalizeEvent, value);
		}
		remove
		{
			events.RemoveHandler(personalizeEvent, value);
		}
	}

	/// <summary>Occurs at the end of page execution if automatic profile saving is enabled.</summary>
	public event ProfileAutoSaveEventHandler ProfileAutoSaving
	{
		add
		{
			events.AddHandler(profileAutoSavingEvent, value);
		}
		remove
		{
			events.RemoveHandler(profileAutoSavingEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
	public ProfileModule()
	{
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
	public void Dispose()
	{
		app.EndRequest -= OnLeave;
		app.PostMapRequestHandler -= OnEnter;
	}

	/// <summary>Calls initialization code when a <see cref="T:System.Web.Profile.ProfileModule" /> object is created.</summary>
	/// <param name="app">The current application. </param>
	public void Init(HttpApplication app)
	{
		this.app = app;
		app.PostMapRequestHandler += OnEnter;
		app.EndRequest += OnLeave;
		AnonymousIdentificationSection anonymousIdentificationSection = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");
		if (anonymousIdentificationSection != null)
		{
			anonymousCookieName = anonymousIdentificationSection.CookieName;
		}
	}

	private void OnEnter(object o, EventArgs eventArgs)
	{
		if (!ProfileManager.Enabled || !HttpContext.Current.Request.IsAuthenticated)
		{
			return;
		}
		HttpCookie httpCookie = app.Request.Cookies[anonymousCookieName];
		if (httpCookie != null && httpCookie.Expires != DateTime.MinValue && httpCookie.Expires > DateTime.Now)
		{
			if (events[migrateAnonymousEvent] is ProfileMigrateEventHandler profileMigrateEventHandler)
			{
				ProfileMigrateEventArgs e = new ProfileMigrateEventArgs(HttpContext.Current, Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value)));
				profileMigrateEventHandler(this, e);
			}
			HttpCookie httpCookie2 = new HttpCookie(anonymousCookieName);
			httpCookie2.Path = app.Request.ApplicationPath;
			httpCookie2.Expires = new DateTime(1970, 1, 1);
			httpCookie2.Value = "";
			app.Response.AppendCookie(httpCookie2);
		}
	}

	private void OnLeave(object o, EventArgs eventArgs)
	{
		if (!ProfileManager.Enabled || !app.Context.ProfileInitialized || !ProfileManager.AutomaticSaveEnabled)
		{
			return;
		}
		profile = app.Context.Profile;
		if (profile == null)
		{
			return;
		}
		if (events[profileAutoSavingEvent] is ProfileAutoSaveEventHandler profileAutoSaveEventHandler)
		{
			ProfileAutoSaveEventArgs profileAutoSaveEventArgs = new ProfileAutoSaveEventArgs(app.Context);
			profileAutoSaveEventHandler(this, profileAutoSaveEventArgs);
			if (!profileAutoSaveEventArgs.ContinueWithProfileAutoSave)
			{
				return;
			}
		}
		profile.Save();
	}
}
