using System.Security;
using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>Allows declarative <see cref="N:System.DirectoryServices" /> permission checks.</summary>
[Serializable]
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
public class DirectoryServicesPermissionAttribute : CodeAccessSecurityAttribute
{
	private string path;

	private DirectoryServicesPermissionAccess access;

	/// <summary>Gets or sets a path to an Active Directory Domain Services node to which the permissions apply.</summary>
	/// <returns>The path to an Active Directory Domain Services node. The default is "*".</returns>
	/// <exception cref="T:System.ArgumentNullException">The property value is a null reference (<see langword="Nothing" /> in Visual Basic).</exception>
	public string Path
	{
		get
		{
			return path;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("Path");
			}
			path = value;
		}
	}

	/// <summary>Gets or sets the access levels that are used in creating permissions.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values. The default is Browse.</returns>
	public DirectoryServicesPermissionAccess PermissionAccess
	{
		get
		{
			return access;
		}
		set
		{
			access = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="M:System.DirectoryServices.DirectoryServicesPermissionAttribute.#ctor(System.Security.Permissions.SecurityAction)" /> class.</summary>
	/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values.</param>
	public DirectoryServicesPermissionAttribute(SecurityAction action)
		: base(action)
	{
		path = "*";
		access = DirectoryServicesPermissionAccess.Browse;
	}

	/// <summary>Creates permissions based on the attribute's specifications.</summary>
	/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the created permission.</returns>
	public override IPermission CreatePermission()
	{
		if (base.Unrestricted)
		{
			return new DirectoryServicesPermission(PermissionState.Unrestricted);
		}
		return new DirectoryServicesPermission(access, path);
	}
}
