namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Specifies additional options when performing a synchronization.</summary>
[Flags]
public enum SyncFromAllServersOptions
{
	/// <summary>No synchronization options.</summary>
	None = 0,
	/// <summary>Aborts the synchronization if any server cannot be contacted or if any server is unreachable.</summary>
	AbortIfServerUnavailable = 1,
	/// <summary>Disables transitive replication. Synchronization is performed only with adjacent servers.</summary>
	SyncAdjacentServerOnly = 2,
	/// <summary>Disables all synchronization. The topology is analyzed and unavailable or unreachable servers are identified.</summary>
	CheckServerAlivenessOnly = 8,
	/// <summary>Assumes that all servers are responding. This will speed up the operation of this method, but if some servers are not responding, some transitive replications might be blocked.</summary>
	SkipInitialCheck = 0x10,
	/// <summary>Pushes changes from the home server out to all partners using transitive replication. This reverses the direction of replication and the order of execution of the replication sets from the usual mode of execution.</summary>
	PushChangeOutward = 0x20,
	/// <summary>Synchronizes across site boundaries. By default, this method attempts to synchronize only with domain controllers in the same site as the home system. Set this flag to attempt to synchronize with all domain controllers in the enterprise forest. However, the domain controllers can be synchronized only if connected by a synchronous (RPC) transport.</summary>
	CrossSite = 0x40
}
