namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryServicesPermissionAccess" /> enumeration defines access levels that are used by <see cref="N:System.DirectoryServices" /> permission classes. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
[Serializable]
[Flags]
public enum DirectoryServicesPermissionAccess
{
	/// <summary>No permissions are allowed.</summary>
	None = 0,
	/// <summary>Reading the Active Directory Domain Services tree is allowed.</summary>
	Browse = 2,
	/// <summary>Reading, writing, deleting, changing, and adding to the Active Directory Domain Srevices tree are allowed.</summary>
	Write = 6
}
