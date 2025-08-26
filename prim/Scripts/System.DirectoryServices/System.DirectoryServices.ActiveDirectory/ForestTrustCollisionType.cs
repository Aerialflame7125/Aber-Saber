namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the collision type of a <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision" /> object.</summary>
public enum ForestTrustCollisionType
{
	/// <summary>The collision is between top-level domains. This collision type indicates a collision with a namespace element of another forest.</summary>
	TopLevelName,
	/// <summary>The collision is between domain cross-references. This collision type indicates a collision with a domain in the same forest.</summary>
	Domain,
	/// <summary>The collision is not a collision between top-level domains or domain cross references.</summary>
	Other
}
