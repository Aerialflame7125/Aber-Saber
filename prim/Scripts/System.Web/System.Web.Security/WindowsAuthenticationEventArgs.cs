using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security;

/// <summary>Provides data for the WindowsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
public sealed class WindowsAuthenticationEventArgs : EventArgs
{
	private IPrincipal _User;

	private HttpContext _Context;

	private WindowsIdentity _Identity;

	/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</summary>
	/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the current request.</returns>
	public IPrincipal User
	{
		get
		{
			return _User;
		}
		[SecurityPermission(SecurityAction.Demand, ControlPrincipal = true)]
		set
		{
			_User = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
	public HttpContext Context => _Context;

	/// <summary>Gets the Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</summary>
	/// <returns>The Windows identity passed to the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> constructor.</returns>
	public WindowsIdentity Identity => _Identity;

	/// <summary>Initializes a newly created instance of the <see cref="T:System.Web.Security.WindowsAuthenticationEventArgs" /> class.</summary>
	/// <param name="identity">The Windows identity object. </param>
	/// <param name="context">The context for the event. </param>
	public WindowsAuthenticationEventArgs(WindowsIdentity identity, HttpContext context)
	{
		_Identity = identity;
		_Context = context;
	}
}
