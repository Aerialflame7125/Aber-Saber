namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies the bit identifiers for the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySite" /> object site options.</summary>
[Flags]
public enum ActiveDirectorySiteOptions
{
	/// <summary>No site options are set.</summary>
	None = 0,
	/// <summary>Inter-site topology generation is disabled.</summary>
	AutoTopologyDisabled = 1,
	/// <summary>Topology cleanup is disabled.</summary>
	TopologyCleanupDisabled = 2,
	/// <summary>Automatic minimum hops topology is disabled.</summary>
	AutoMinimumHopDisabled = 4,
	/// <summary>Stale server detection is disabled.</summary>
	StaleServerDetectDisabled = 8,
	/// <summary>Automatic intra-site topology generation is disabled.</summary>
	AutoInterSiteTopologyDisabled = 0x10,
	/// <summary>Group memberships for users is enabled.</summary>
	GroupMembershipCachingEnabled = 0x20,
	/// <summary>The KCC (Knowledge Consistency Checker) is forced to operate in Windows Server 2003 behavior mode.</summary>
	ForceKccWindows2003Behavior = 0x40,
	/// <summary>The KCC is forced to use the Windows 2000 ISTG election algorithm.</summary>
	UseWindows2000IstgElection = 0x80,
	/// <summary>The KCC can randomly pick a bridgehead server when creating a connection.</summary>
	RandomBridgeHeaderServerSelectionDisabled = 0x100,
	/// <summary>The KCC is allowed to use hashing when creating a replication schedule.</summary>
	UseHashingForReplicationSchedule = 0x200,
	/// <summary>Creation of static failover connections is enabled.</summary>
	RedundantServerTopologyEnabled = 0x400
}
