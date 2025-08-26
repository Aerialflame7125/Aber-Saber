using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ListChildrenAccessRule" /> class represents a specific type of access rule that is used to allow or deny an Active Directory Domain Services object the right to list child objects.</summary>
public sealed class ListChildrenAccessRule : ActiveDirectoryAccessRule
{
	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ListChildrenAccessRule" /> class with the specified identity reference and access control type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	public ListChildrenAccessRule(IdentityReference identity, AccessControlType type)
		: base(identity, 4, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ListChildrenAccessRule" /> class with the specified identity reference, access control type, and Active Directory Domain Services security inheritance information.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public ListChildrenAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 4, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ListChildrenAccessRule" /> class with the specified identity reference, access control type, Active Directory Domain Services security inheritance information, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public ListChildrenAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 4, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}
}
