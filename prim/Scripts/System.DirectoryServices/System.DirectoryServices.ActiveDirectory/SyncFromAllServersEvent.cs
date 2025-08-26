namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Used in the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncUpdateCallback" /> delegate to specify the type of synchronization event.</summary>
public enum SyncFromAllServersEvent
{
	/// <summary>An error occurred.</summary>
	Error,
	/// <summary>Synchronization of two servers has started.</summary>
	SyncStarted,
	/// <summary>Synchronization of two servers has just completed.</summary>
	SyncCompleted,
	/// <summary>The entire replication process has completed.</summary>
	Finished
}
