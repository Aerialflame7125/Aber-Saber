namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Indicates a specific type of replication operation.</summary>
public enum ReplicationOperationType
{
	/// <summary>Indicates an inbound replication over an existing replication agreement from a direct replication partner.</summary>
	Sync,
	/// <summary>Indicates the addition of a replication agreement for a new direct replication partner.</summary>
	Add,
	/// <summary>Indicates the removal of a replication agreement for an existing direct replication partner.</summary>
	Delete,
	/// <summary>Indicates the modification of a replication agreement for an existing direct replication partner.</summary>
	Modify,
	/// <summary>Indicates the addition, deletion, or update of outbound change notification data for a direct replication partner.</summary>
	UpdateReference
}
