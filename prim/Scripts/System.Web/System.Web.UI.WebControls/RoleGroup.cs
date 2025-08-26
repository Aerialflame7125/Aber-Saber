using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls;

/// <summary>Associates a content template in a <see cref="T:System.Web.UI.WebControls.LoginView" /> control with one or more roles defined for the Web site. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class RoleGroup
{
	private ITemplate contentTemplate;

	private string[] roles;

	/// <summary>Gets or sets the content template associated with this role group.</summary>
	/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> associated with this role group. The default value is <see langword="null" />.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(LoginView))]
	public ITemplate ContentTemplate
	{
		get
		{
			return contentTemplate;
		}
		set
		{
			contentTemplate = value;
		}
	}

	/// <summary>Gets or sets the roles associated with this role group.</summary>
	/// <returns>A comma-separated list of roles associated with this role group. The default is <see langword="null" />.</returns>
	[TypeConverter(typeof(StringArrayConverter))]
	public string[] Roles
	{
		get
		{
			if (roles == null)
			{
				roles = new string[0];
			}
			return roles;
		}
		set
		{
			roles = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RoleGroup" /> class. </summary>
	public RoleGroup()
	{
	}

	/// <summary>Indicates whether the specified user is a member of any of the roles in the role group.</summary>
	/// <param name="user">The user name to look for in the role group. </param>
	/// <returns>
	///     <see langword="true" /> if the user is a member of one of the roles associated with this role group; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="User" /> is <see langword="null" />.</exception>
	public bool ContainsUser(IPrincipal user)
	{
		if (user == null)
		{
			throw new ArgumentNullException("user");
		}
		if (roles != null)
		{
			string[] array = roles;
			foreach (string role in array)
			{
				if (user.IsInRole(role))
				{
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>Returns a comma-separated list of the roles associated with this role group.</summary>
	/// <returns>A comma-separated list of the roles associated with this role group.</returns>
	public override string ToString()
	{
		if (roles == null || roles.Length == 0)
		{
			return string.Empty;
		}
		if (roles.Length == 1)
		{
			return roles[0];
		}
		return string.Join(",", roles);
	}
}
