using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class represents a specific type of access rule that is used to allow or deny access to an Active Directory Domain Services property.</summary>
public sealed class PropertyAccessRule : ActiveDirectoryAccessRule
{
	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, and property access.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access)
		: base(identity, 0, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, property access, and property type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertyType">The schema GUID of the property that this access rule applies to. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all property types.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertyType)
		: base(identity, 0, type, propertyType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, property access, and Active Directory Domain Services security inheritance.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 0, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, property access, property type, and Active Directory Domain Services security inheritance.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertyType">The schema GUID of the property that this access rule applies to. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all property types.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertyType, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 0, type, propertyType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, property access, Active Directory Domain Services security inheritance, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 0, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertyAccessRule" /> class with the specified identity reference, access control type, property access, property type, Active Directory Domain Services security inheritance, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object which identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values which specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertyType">The schema GUID of the property that this access rule applies to. If this is <see cref="F:System.Guid.Empty" />, then the access rule applies to all property types.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public PropertyAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertyType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 0, type, propertyType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}
}
