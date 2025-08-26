using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>Sets the identity of the user for an ASP.NET application when Windows authentication is enabled. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class WindowsAuthenticationModule : IHttpModule
{
	private static readonly object authenticateEvent = new object();

	private EventHandlerList events = new EventHandlerList();

	/// <summary>Occurs when the application authenticates the current request.</summary>
	public event WindowsAuthenticationEventHandler Authenticate
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

	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> class.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public WindowsAuthenticationModule()
	{
	}

	/// <summary>Releases all resources, other than memory, used by the <see cref="T:System.Web.Security.WindowsAuthenticationModule" />.</summary>
	public void Dispose()
	{
		events.Dispose();
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.WindowsAuthenticationModule" /> object.</summary>
	/// <param name="app">The current <see cref="T:System.Web.HttpApplication" /> instance. </param>
	[MonoTODO("Not implemented")]
	public void Init(HttpApplication app)
	{
		throw new NotImplementedException();
	}
}
