namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies additional information about a forest trust collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> property value is <see cref="F:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType.TopLevelName" />.</summary>
[Flags]
public enum TopLevelNameCollisionOptions
{
	/// <summary>No action has occurred.</summary>
	None = 0,
	/// <summary>The forest trust account has been created and is disabled.</summary>
	NewlyCreated = 1,
	/// <summary>The forest trust account was disabled by administrative action.</summary>
	DisabledByAdmin = 2,
	/// <summary>The forest trust account was disabled due to a conflict with an existing forest trust account.</summary>
	DisabledByConflict = 4
}
