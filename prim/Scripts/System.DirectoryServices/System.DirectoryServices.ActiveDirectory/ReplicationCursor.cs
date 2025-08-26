namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursor" /> class represents a replication operation occurrence.</summary>
public class ReplicationCursor
{
	/// <summary>Gets the name of the partition to which this replication operation was applied.</summary>
	/// <returns>The name of the partition represented by this replication cursor.</returns>
	public string PartitionName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the invocation identifier of the replication source server.</summary>
	/// <returns>The invocation identifier of the replication source server</returns>
	public Guid SourceInvocationId
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the maximum update sequence number (USN) for which the destination server has accepted changes from the source server.</summary>
	/// <returns>The maximum update sequence number (USN) for which the destination server has accepted changes from the source server.</returns>
	public long UpToDatenessUsn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the replication source server.</summary>
	/// <returns>The name of the replication source server.</returns>
	public string SourceServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time of the last successful replication synchronization with the replication source server.</summary>
	/// <returns>The time at which the last successful replication synchronization occurred.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This property is not supported on Windows 2000.</exception>
	public DateTime LastSuccessfulSyncTime
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
