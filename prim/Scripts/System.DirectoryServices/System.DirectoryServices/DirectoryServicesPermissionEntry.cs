using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> class defines the smallest unit of a code access security permission set for <see cref="N:System.DirectoryServices" />.</summary>
[Serializable]
public class DirectoryServicesPermissionEntry
{
	private DirectoryServicesPermissionAccess permissionAccess;

	private string path;

	/// <summary>The <see cref="P:System.DirectoryServices.DirectoryServicesPermissionEntry.Path" /> property gets a path to an Active Directory Domain Services node to which the permissions apply.</summary>
	/// <returns>The path to an Active Directory Domain Services node.</returns>
	public string Path => path;

	/// <summary>The <see cref="P:System.DirectoryServices.DirectoryServicesPermissionEntry.PermissionAccess" /> property gets the access levels used in creating permissions.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</returns>
	public DirectoryServicesPermissionAccess PermissionAccess => permissionAccess;

	/// <summary>The <see cref="M:System.DirectoryServices.DirectoryServicesPermissionEntry.#ctor(System.DirectoryServices.DirectoryServicesPermissionAccess,System.String)" /> constructor initializes a new instance of the <see cref="M:System.DirectoryServices.DirectoryServicesPermissionEntry.#ctor(System.DirectoryServices.DirectoryServicesPermissionAccess,System.String)" /> class.</summary>
	/// <param name="permissionAccess">One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</param>
	/// <param name="path">The path of the Active Directory Domain Services node to which the permissions apply.</param>
	public DirectoryServicesPermissionEntry(DirectoryServicesPermissionAccess permissionAccess, string path)
	{
		this.permissionAccess = permissionAccess;
		this.path = path;
	}

	internal ResourcePermissionBaseEntry GetBaseEntry()
	{
		return new ResourcePermissionBaseEntry((int)permissionAccess, new string[1] { path });
	}
}
