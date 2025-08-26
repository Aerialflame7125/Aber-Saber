using System.Security.Permissions;

namespace System.Web.Security;

/// <summary>Provides data for the DefaultAuthentication_OnAuthenticate event. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DefaultAuthenticationEventArgs : EventArgs
{
	private HttpContext _context;

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
	public HttpContext Context => _context;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.DefaultAuthenticationEventArgs" /> class.</summary>
	/// <param name="context">The context for the event. </param>
	public DefaultAuthenticationEventArgs(HttpContext context)
	{
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}
		_context = context;
	}
}
