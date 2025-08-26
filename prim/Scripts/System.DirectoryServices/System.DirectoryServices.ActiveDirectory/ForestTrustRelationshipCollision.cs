namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Represents a collision record resulting from a collision between forest trust records.</summary>
public class ForestTrustRelationshipCollision
{
	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" /> value for the forest trust collision.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" /> value indicating the collision type.</returns>
	public ForestTrustCollisionType CollisionType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollisionOptions" /> value for the forest trust collision.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.TopLevelNameCollisionOptions" /> value that provides information about the collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> type is <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" />.</returns>
	public TopLevelNameCollisionOptions TopLevelNameCollisionOption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollisionOptions" /> value for the forest trust collision.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DomainCollisionOptions" /> value that provides information about the collision when the <see cref="P:System.DirectoryServices.ActiveDirectory.ForestTrustRelationshipCollision.CollisionType" /> type is <see cref="T:System.DirectoryServices.ActiveDirectory.ForestTrustCollisionType" />.</returns>
	public DomainCollisionOptions DomainCollisionOption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the collision record from the underlying Active Directory Domain Services service.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the collision record resulting from a collision between forest trust records.</returns>
	public string CollisionRecord
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
