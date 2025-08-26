using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices;

/// <summary>Represents a specific type of access rule that is used to allow or deny an Active Directory object an extended right. Extended rights are special operations that are not covered by the standard set of access rights. An example of an extended right is Send-As, which gives a user the right to send email for another user. For a list of possible extended rights, see the Extended Rights article. For more information about extended rights, see the Control Access Rights.</summary>
public sealed class ExtendedRightAccessRule : ActiveDirectoryAccessRule
{
	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference and access control type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that  identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type)
		: base(identity, 256, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, and extended right identifier.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="extendedRightType">The Rights-Guid of the extended right that this access rule applies to. For more information, see the Rights-Guid article. In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of possible extended rights, see the Extended Rights article.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType)
		: base(identity, 256, type, extendedRightType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, and Active Directory security inheritance.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 256, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, extended right identifier, and Active Directory security inheritance.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="extendedRightType">The Rights-Guid of the extended right that this access rule applies to. For more information, see the Rights-Guid article. In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of possible extended rights, see the Extended Rights article.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType, ActiveDirectorySecurityInheritance inheritanceType)
		: base(identity, 256, type, extendedRightType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, Active Directory security inheritance, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 256, type, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ExtendedRightAccessRule" /> class with the specified identity reference, access control type, extended right identifier, Active Directory security inheritance, and inherited object type.</summary>
	/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that identifies the trustee of the access rule.</param>
	/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> enumeration values that specifies the access rule type.</param>
	/// <param name="extendedRightType">The Rights-Guid attribute of the extended right that this access rule applies to. For more information, see the Rights-Guid article. In the Active Directory schema documentation, this information can be found in the Rights-GUID row on the reference page for each extended right. If this parameter is <see cref="F:System.Guid.Empty" />, the access rule applies to all extended rights. For a list of possible extended rights, see the Extended Rights article.</param>
	/// <param name="inheritanceType">One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the access rule.</param>
	/// <param name="inheritedObjectType">The schema GUID of the child object type that can inherit this access rule.</param>
	public ExtendedRightAccessRule(IdentityReference identity, AccessControlType type, Guid extendedRightType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: base(identity, 256, type, extendedRightType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}
}
