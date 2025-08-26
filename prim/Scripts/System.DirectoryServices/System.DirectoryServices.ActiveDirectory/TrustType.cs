namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates the type of a <see cref="T:System.DirectoryServices.ActiveDirectory.TrustRelationshipInformation" /> object.</summary>
public enum TrustType
{
	/// <summary>One of the domains in the trust relationship is a tree root.</summary>
	TreeRoot,
	/// <summary>The trust relationship is between a parent and a child domain.</summary>
	ParentChild,
	/// <summary>The trust relationship is a shortcut between two domains that exists to optimize the authentication processing between two domains that are in separate domain trees.</summary>
	CrossLink,
	/// <summary>The trust relationship is with a domain outside of the current forest.</summary>
	External,
	/// <summary>The trust relationship is between two forest root domains in separate Windows Server 2003 forests.</summary>
	Forest,
	/// <summary>The trusted domain is an MIT Kerberos realm.</summary>
	Kerberos,
	/// <summary>The trust is a non-specific type.</summary>
	Unknown
}
