using System.Security.Permissions;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class allows you to control code access security permissions for <see cref="N:System.DirectoryServices" />.</summary>
[Serializable]
public sealed class DirectoryServicesPermission : ResourcePermissionBase
{
	private DirectoryServicesPermissionEntryCollection innerCollection;

	/// <summary>Gets the collection of permission entries for this permission.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntryCollection" /> object that contains the permission entries for this permission.</returns>
	public DirectoryServicesPermissionEntryCollection PermissionEntries
	{
		get
		{
			if (innerCollection == null)
			{
				innerCollection = new DirectoryServicesPermissionEntryCollection(this);
			}
			return innerCollection;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class.</summary>
	public DirectoryServicesPermission()
	{
		SetUp();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified permission access level entries.</summary>
	/// <param name="permissionAccessEntries">An array of <see cref="T:System.DirectoryServices.DirectoryServicesPermissionEntry" /> objects. The <see cref="P:System.DirectoryServices.DirectoryServicesPermission.PermissionEntries" /> property is set to this value.</param>
	/// <exception cref="T:System.ArgumentNullException">The specified value for the <paramref name="permissionAccessEntries" /> parameter is <see langword="null" />.</exception>
	public DirectoryServicesPermission(DirectoryServicesPermissionEntry[] permissionAccessEntries)
	{
		SetUp();
		innerCollection = new DirectoryServicesPermissionEntryCollection(this);
		innerCollection.AddRange(permissionAccessEntries);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified permission state.</summary>
	/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="State" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
	public DirectoryServicesPermission(PermissionState state)
		: base(state)
	{
		SetUp();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryServicesPermission" /> class with the specified access levels and specified path to an Active Directory Domain Services node.</summary>
	/// <param name="permissionAccess">One of the <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> values.</param>
	/// <param name="path">The path of the Active Directory Domain Services object, otherwise known as the ADsPath, to which the permissions apply.</param>
	public DirectoryServicesPermission(DirectoryServicesPermissionAccess permissionAccess, string path)
	{
		SetUp();
		innerCollection = new DirectoryServicesPermissionEntryCollection(this);
		innerCollection.Add(new DirectoryServicesPermissionEntry(permissionAccess, path));
	}

	private void SetUp()
	{
		base.PermissionAccessType = typeof(DirectoryServicesPermissionAccess);
		base.TagNames = new string[1] { "Path" };
	}

	internal ResourcePermissionBaseEntry[] GetEntries()
	{
		return GetPermissionEntries();
	}

	internal void ClearEntries()
	{
		Clear();
	}

	internal void Add(object obj)
	{
		DirectoryServicesPermissionEntry directoryServicesPermissionEntry = obj as DirectoryServicesPermissionEntry;
		AddPermissionAccess(directoryServicesPermissionEntry.GetBaseEntry());
	}

	internal void Remove(object obj)
	{
		DirectoryServicesPermissionEntry directoryServicesPermissionEntry = obj as DirectoryServicesPermissionEntry;
		RemovePermissionAccess(directoryServicesPermissionEntry.GetBaseEntry());
	}
}
