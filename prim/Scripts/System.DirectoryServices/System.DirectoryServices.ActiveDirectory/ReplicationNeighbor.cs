namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> class contains information about a replication neighbor of an <see cref="T:System.DirectoryServices.ActiveDirectory.AdamInstance" /> or <see cref="T:System.DirectoryServices.ActiveDirectory.DomainController" /> object.</summary>
public class ReplicationNeighbor
{
	/// <summary>Specifies the options for a <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</summary>
	[Flags]
	public enum ReplicationNeighborOptions : long
	{
		/// <summary>The local copy of the naming context is writable.</summary>
		Writeable = 0x10L,
		/// <summary>Replication of this naming context from this source is attempted when the destination server is restarted. This normally only applies to intra-site neighbors.</summary>
		SyncOnStartup = 0x20L,
		/// <summary>Perform replication on a schedule. This option is normally set unless the schedule for this naming context/source is "never", that is, the empty schedule.</summary>
		ScheduledSync = 0x40L,
		/// <summary>Perform replication indirectly through the Inter-Site Messaging Service. This flag is set only when replicating over SMTP. This flag is not set when replicating over inter-site RPC/IP.</summary>
		UseInterSiteTransport = 0x80L,
		/// <summary>If set, this option indicates that when inbound replication is complete, the destination server must tell the source server to synchronize in the reverse direction. This feature is used in dial-up scenarios where only one of the two servers can initiate a dial-up connection. For example, this option would be used in a corporate headquarters and branch office, where the branch office connects to the corporate headquarters over the Internet by means of a dial-up ISP connection.</summary>
		TwoWaySync = 0x200L,
		/// <summary>This neighbor is in a state where it returns parent objects before children objects. It goes into this state after it receives a child object before its parent.</summary>
		ReturnObjectParent = 0x800L,
		/// <summary>The destination server is performing a full synchronization from the source server. Full synchronizations do not use vectors that create updates (DS_REPL_CURSORS) for filtering updates. Full synchronizations are not used as a part of the normal replication protocol.</summary>
		FullSyncInProgress = 0x10000L,
		/// <summary>The last packet from the source indicated that an object that has not yet been created by the destination server has been modified. The next packet that is requested will instruct the source server to put all attributes of the modified object into the packet.</summary>
		FullSyncNextPacket = 0x20000L,
		/// <summary>A synchronization has never been successfully completed from this source.</summary>
		NeverSynced = 0x200000L,
		/// <summary>The replication engine has temporarily stopped processing this neighbor in order to service another higher-priority neighbor, either for this partition or another partition. The replication engine will resume processing this neighbor after the higher-priority work is completed.</summary>
		Preempted = 0x1000000L,
		/// <summary>This option is set to disable notification based synchronizations. Within a site, domain controllers synchronize with each other based on notifications when changes occur. This setting prevents this neighbor from performing syncs that are triggered by notifications. The neighbor will still do synchronizations based on its schedule, or if requested manually.</summary>
		IgnoreChangeNotifications = 0x4000000L,
		/// <summary>This option is set to disable synchronizations based on its schedule. The only way this neighbor will perform synchronizations is in response to a change notification or to a manual request for synchronizations.</summary>
		DisableScheduledSync = 0x8000000L,
		/// <summary>Changes that are received from this source are compressed. This option is usually set only if the source server is in a different site.</summary>
		CompressChanges = 0x10000000L,
		/// <summary>No change notifications should be received from this source. This option is usually set only if the source server is in a different site.</summary>
		NoChangeNotifications = 0x20000000L,
		/// <summary>This neighbor is in a state where it is rebuilding the contents of this replica because of a change in the partial attribute set.</summary>
		PartialAttributeSet = 0x40000000L
	}

	/// <summary>Gets the partition name for this <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</summary>
	/// <returns>The name of the partition for this <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</returns>
	public string PartitionName
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

	/// <summary>Gets the transport type that was used for replication.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryTransportType" /> enumeration value that indicates the transport type that was used for the replication connection.</returns>
	public ActiveDirectoryTransportType TransportType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the attribute and object settings for this <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor.ReplicationNeighborOptions" /> value that contains the attribute and option settings for this <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighbor" /> object.</returns>
	public ReplicationNeighborOptions ReplicationNeighborOption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the invocation identifier that was used by the replication source server in the last replication attempt.</summary>
	/// <returns>The invocation identifier that was used by the replication source server in the last replication attempt.</returns>
	public Guid SourceInvocationId
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the update sequence number (USN) of the last object update that was received.</summary>
	/// <returns>The USN of the last update that was received.</returns>
	public long UsnLastObjectChangeSynced
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the update sequence number (USN) value for the last successful replication cycle.</summary>
	/// <returns>The USN value for the last successful replication cycle. If no replication succeeded, the value is zero.</returns>
	public long UsnAttributeFilter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time of the last successful replication cycle from this source.</summary>
	/// <returns>The time of the last successful synchronization. If no replication succeeded, the value is zero.</returns>
	public DateTime LastSuccessfulSync
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time of the last replication attempt from this source.</summary>
	/// <returns>The time of the last replication attempt. If no replication attempt occurred, the value is zero.</returns>
	public DateTime LastAttemptedSync
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the error code that is associated with the last replication attempt from this source.</summary>
	/// <returns>The error code for the last replication attempt. If the last attempt succeeded, the value is ERROR_SUCCESS.</returns>
	public int LastSyncResult
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the message that is associated with the last replication attempt from this source.</summary>
	/// <returns>The message that corresponds to the last replication attempt. If the last attempt succeeded, the value is an empty string.</returns>
	public string LastSyncMessage
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of replication failures since the last successful replication cycle.</summary>
	/// <returns>The number of failed replication attempts from this source since the last successful replication attempt. If no previous attempt was successful, the return value is the number of failed replication attempts since the source was added as a neighbor.</returns>
	public int ConsecutiveFailureCount
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
