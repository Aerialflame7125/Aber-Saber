using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Web.Security;

/// <summary>Ensures that an authentication object is present in the context. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DefaultAuthenticationModule : IHttpModule
{
	private static readonly object authenticateEvent = new object();

	private static IPrincipal generic_principal = new GenericPrincipal(new GenericIdentity("", ""), new string[0]);

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Occurs after the request has been authenticated.</summary>
	public event DefaultAuthenticationEventHandler Authenticate
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> class. </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public DefaultAuthenticationModule()
	{
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.DefaultAuthenticationModule" />.</summary>
	public void Dispose()
	{
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.DefaultAuthenticationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	public void Init(HttpApplication app)
	{
		app.DefaultAuthentication += OnDefaultAuthentication;
	}

	private void OnDefaultAuthentication(object sender, EventArgs args)
	{
		HttpContext context = ((HttpApplication)sender).Context;
		DefaultAuthenticationEventHandler defaultAuthenticationEventHandler = events[authenticateEvent] as DefaultAuthenticationEventHandler;
		if (context.User == null)
		{
			defaultAuthenticationEventHandler?.Invoke(this, new DefaultAuthenticationEventArgs(context));
		}
		if (context.User == null)
		{
			context.User = generic_principal;
		}
		Thread.CurrentPrincipal = context.User;
	}
}
