using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security;

/// <summary>
///     Represents a user identity authenticated using forms authentication. This class cannot be inherited.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class FormsIdentity : IIdentity
{
	private FormsAuthenticationTicket ticket;

	/// <summary>Gets the type of authenticated identity.</summary>
	/// <returns>The type of authenticated identity. This property always returns "Forms".</returns>
	public string AuthenticationType => "Forms";

	/// <summary>Gets a value that indicates whether authentication took place.</summary>
	/// <returns>This property always returns <see langword="true" />.</returns>
	public bool IsAuthenticated => true;

	/// <summary>Gets the user name of the forms identity.</summary>
	/// <returns>The user name of the forms identity.</returns>
	public string Name => ticket.Name;

	/// <summary>Gets the <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> for the forms-authentication user identity.</summary>
	/// <returns>The <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> supplied to the <see cref="M:System.Web.Security.FormsIdentity.#ctor(System.Web.Security.FormsAuthenticationTicket)" /> constructor for the current object.</returns>
	public FormsAuthenticationTicket Ticket => ticket;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsIdentity" /> class.</summary>
	/// <param name="ticket">The authentication ticket upon which this identity is based. </param>
	public FormsIdentity(FormsAuthenticationTicket ticket)
	{
		this.ticket = ticket;
	}
}
