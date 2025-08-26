namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Contains information about a <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOperationException" />.</summary>
public enum SyncFromAllServersErrorCategory
{
	/// <summary>The server could not be contacted for replication.</summary>
	ErrorContactingServer,
	/// <summary>The replication operation failed to complete.</summary>
	ErrorReplicating,
	/// <summary>The server is not reachable.</summary>
	ServerUnreachable
}
