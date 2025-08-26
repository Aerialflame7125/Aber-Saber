using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security;

/// <summary>Provides data for the FormsAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
public sealed class FormsAuthenticationEventArgs : EventArgs
{
	private IPrincipal _User;

	private HttpContext _Context;

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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthenticationEventArgs" /> class.</summary>
	/// <param name="context">The context for the event. </param>
	public FormsAuthenticationEventArgs(HttpContext context)
	{
		_Context = context;
	}
}
