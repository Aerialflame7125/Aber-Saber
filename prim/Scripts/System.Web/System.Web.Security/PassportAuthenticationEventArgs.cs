using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security;

/// <summary>The event argument passed to the <see cref="E:System.Web.Security.PassportAuthenticationModule.Authenticate" /> event by a <see cref="T:System.Web.Security.PassportAuthenticationModule" />. Since there is already an identity at this point, this is useful mainly for attaching a custom <see cref="T:System.Security.Principal.IPrincipal" /> object to the context using the supplied identity. This class is deprecated.</summary>
[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
public sealed class PassportAuthenticationEventArgs : EventArgs
{
	private IPrincipal _User;

	private HttpContext _Context;

	private PassportIdentity _Identity;

	/// <summary>Gets or sets the <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request. This class is deprecated.</summary>
	/// <returns>The <see cref="T:System.Security.Principal.IPrincipal" /> object to be associated with the request.</returns>
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

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request. This class is deprecated.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
	public HttpContext Context => _Context;

	/// <summary>Gets an authenticated Passport identity. This class is deprecated.</summary>
	/// <returns>An authenticated Passport identity.</returns>
	public PassportIdentity Identity => _Identity;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportAuthenticationEventArgs" /> class. This class is deprecated.</summary>
	/// <param name="identity">The identity object </param>
	/// <param name="context">The context for the event. </param>
	public PassportAuthenticationEventArgs(PassportIdentity identity, HttpContext context)
	{
		_Identity = identity;
		_Context = context;
	}
}
