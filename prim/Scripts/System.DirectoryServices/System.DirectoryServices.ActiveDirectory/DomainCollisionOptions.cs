namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies additional information about a forest trust collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> property value is <see cref="F:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType.Domain" />.</summary>
[Flags]
public enum DomainCollisionOptions
{
	/// <summary>No action has occurred.</summary>
	None = 0,
	/// <summary>The forest trust SID was disabled by administrative action.</summary>
	SidDisabledByAdmin = 1,
	/// <summary>The forest trust SID was disabled due to a conflict with an existing SID.</summary>
	SidDisabledByConflict = 2,
	/// <summary>The forest trust NetBIOS record was disabled by administrative action.</summary>
	NetBiosNameDisabledByAdmin = 4,
	/// <summary>The forest trust NetBIOS record was disabled due to a conflict with an existing NetBIOS record.</summary>
	NetBiosNameDisabledByConflict = 8
}
