using System.Security.AccessControl;
using System.Security.Principal;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> is used to set an access control entry (ACE) on a system access control list (SACL). The <see cref="T:System.DirectoryServices.ActiveDirectoryAccessRule" /> contains the trustee, which is represented as an <see cref="P:System.Security.AccessControl.AuthorizationRule.IdentityReference" /> object. It also contains information about the access control type, access mask, and other properties such as inheritance flags. This rule is set on an <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object. After the <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> is committed to the directory store, it will modify the security descriptor object according to the rules that are set on <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />.</summary>
public class ActiveDirectoryAuditRule : ObjectAuditRule
{
	/// <summary>Gets the rights for the audit rule.</summary>
	/// <returns>One or more of the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration values that specifies the rights for the audit rule.</returns>
	public ActiveDirectoryRights ActiveDirectoryRights
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the inheritance type of the audit rule.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration values that specifies the inheritance type of the audit rule.</returns>
	public ActiveDirectorySecurityInheritance InheritanceType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, and flags.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains the combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter may not be zero.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags)
		: this(identity, (int)adRights, auditFlags, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, flags, and object type.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains the combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter can not be zero.</param>
	/// <param name="objectType">Specifies a <see cref="T:System.Guid" /> object that contains the GUID of the object to which the access permissions apply.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags, Guid objectType)
		: this(identity, (int)adRights, auditFlags, objectType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, flags, and inheritance type.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains a combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter can not be zero.</param>
	/// <param name="inheritanceType">Specifies the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> object that contains inheritance information. The allowable flags for this parameter are found in the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags, ActiveDirectorySecurityInheritance inheritanceType)
		: this(identity, (int)adRights, auditFlags, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, flags, object type, and inheritance type.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains the combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter may not be zero.</param>
	/// <param name="objectType">Specifies a <see cref="T:System.Guid" /> object which contains the GUID of the object to which the access permissions apply.</param>
	/// <param name="inheritanceType">Specifies the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> object that contains inheritance information. The allowable flags for this parameter are found in the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags, Guid objectType, ActiveDirectorySecurityInheritance inheritanceType)
		: this(identity, (int)adRights, auditFlags, objectType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, Guid.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, flags, inheritance type, and inherited object type.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> data type object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains the combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter may not be zero.</param>
	/// <param name="inheritanceType">Specifies the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> object that contains inheritance information. The allowable flags for this parameter are found in the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration.</param>
	/// <param name="inheritedObjectType">Specifies the <see cref="T:System.Guid" /> object that identifies the type of child object that can inherit this access rule.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: this(identity, (int)adRights, auditFlags, Guid.Empty, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" /> class with the specified identity, rights, flags, object type, inheritance type, and inherited object type.</summary>
	/// <param name="identity">Specifies an <see cref="T:System.Security.Principal.IdentityReference" /> object, such as an NTAccount object, that resolves to a security identifier (SID).</param>
	/// <param name="adRights">Specifies an <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> object, which is used to define all the rights that can be set on a directory object, as defined in the <see cref="T:System.DirectoryServices.ActiveDirectoryRights" /> enumeration.</param>
	/// <param name="auditFlags">Specifies an <see cref="T:System.Security.AccessControl.AuditFlags" /> object, which contains the combination of one or more audit flags to add to this <see cref="T:System.DirectoryServices.ActiveDirectoryAuditRule" />. The allowable flags are <see cref="F:System.Security.AccessControl.AuditFlags.Success" /> and <see cref="F:System.Security.AccessControl.AuditFlags.Failure" />. This parameter may not be zero.</param>
	/// <param name="objectType">Specifies a <see cref="T:System.Guid" /> object that contains the GUID of the object to which the access permissions apply.</param>
	/// <param name="inheritanceType">Specifies the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> object that contains inheritance information. The allowable flags for this parameter are found in the <see cref="T:System.DirectoryServices.ActiveDirectorySecurityInheritance" /> enumeration.</param>
	/// <param name="inheritedObjectType">Specifies the <see cref="T:System.Guid" /> object that identifies the type of child object that can inherit this access rule.</param>
	public ActiveDirectoryAuditRule(IdentityReference identity, ActiveDirectoryRights adRights, AuditFlags auditFlags, Guid objectType, ActiveDirectorySecurityInheritance inheritanceType, Guid inheritedObjectType)
		: this(identity, (int)adRights, auditFlags, objectType, isInherited: false, InheritanceFlags.None, PropagationFlags.None, inheritedObjectType)
	{
	}

	internal ActiveDirectoryAuditRule(IdentityReference identity, int accessMask, AuditFlags auditFlags, Guid objectGuid, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid inheritedObjectType)
		: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, objectGuid, inheritedObjectType, auditFlags)
	{
	}
}
