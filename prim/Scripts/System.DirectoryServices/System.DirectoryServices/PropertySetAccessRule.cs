using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.PropertySetAccessRule" /> class represents a specific type of access rule that is used to allow or deny access to an Active Directory Domain Services property set. For a list of property sets that are defined for Active Directory Domain Services, see the Property Sets article.</summary>
public sealed class PropertySetAccessRule : ActiveDirectoryAccessRule
{
	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertySetAccessRule" /> class with the specified identity reference, access control type, property access type, and property set identifier.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertySetType">The Rights-Guid of the property set that this access rule applies to. In the Active Directory Domain Services schema documentation, this information can be found in the Rights-GUID row on the reference page for each property set. For more information, see the Rights-Guid attribute article. 
	///   For a list of property sets, see the Property Sets article.</param>
	public PropertySetAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertySetType)
		: base(identity, 0, type, propertySetType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertySetAccessRule" /> class with the specified identity reference, access control type, property access type, property set identifier, and Active Directory Domain Services security inheritance information.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertySetType">The Rights-Guid of the property set that this access rule applies to. In the Active Directory Domain Services schema documentation, this information can be found in the Rights-GUID row on the reference page for each property set. For more information, see the Rights-Guid attribute article.  
	///  For a list of property sets, see the Property Sets article.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public PropertySetAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertySetType, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 0, type, propertySetType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.PropertySetAccessRule" /> class with the specified identity reference, access control type, property access type, property set identifier, inheritance type, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="access">One of the <see cref="T:System.DirectoryServices.PropertyAccess" /> enumeration values that specifies the property access type.</param>
	/// <param name="propertySetType">The Rights-Guid of the property set that this access rule applies to. In the Active Directory Domain Services schema documentation, this information can be found in the Rights-GUID row on the reference page for each property set. For more information, see the Rights-Guid attribute article.  
	///  For a list of property sets, see the Property Sets article.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public PropertySetAccessRule(IdentityReference identity, AccessControlType type, PropertyAccess access, Guid propertySetType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 0, type, propertySetType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}
}
