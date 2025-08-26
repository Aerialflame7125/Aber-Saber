using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>Provides a wrapper around Passport Authentication services. This class cannot be inherited. This class is deprecated.</summary>
[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PassportAuthenticationModule : IHttpModule
{
	private static readonly object authenticateEvent = new object();

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Raised during authentication. This is a Global.asax event that must be named <see langword="PassportAuthentication_OnAuthenticate" />. This class is deprecated</summary>
	public event PassportAuthenticationEventHandler Authenticate
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

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationModule" /> class. This class is deprecated.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public PassportAuthenticationModule()
	{
	}

	/// <summary>Disposes of the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated.</summary>
	public void Dispose()
	{
		events.Dispose();
	}

	/// <summary>Initializes the module derived from <see cref="T:System.Web.IHttpModule" /> when called by the <see cref="T:System.Web.HttpRuntime" />. This class is deprecated</summary>
	/// <param name="app">The <see cref="T:System.Web.HttpApplication" /> module </param>
	[MonoTODO("Will we ever implement this? :-)")]
	public void Init(HttpApplication app)
	{
		throw new NotImplementedException();
	}
}
