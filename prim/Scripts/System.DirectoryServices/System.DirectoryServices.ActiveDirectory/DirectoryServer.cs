using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> class is an abstract class that represents an Active Directory Domain Services server or AD LDS instance.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public abstract class DirectoryServer : IDisposable
{
	/// <summary>Gets the name of the directory server.</summary>
	/// <returns>The name of the directory server.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the partitions on this directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyStringCollection" /> object that contains the distinguished names of the partitions on this directory server.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public ReadOnlyStringCollection Partitions
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves the IP address of this directory server.</summary>
	/// <returns>The Internet protocol (IP) address of this directory server in string form.</returns>
	public abstract string IPAddress
	{
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		get;
	}

	/// <summary>Gets the name of the site that this directory server belongs to.</summary>
	/// <returns>The name of the site that this directory server belongs to.</returns>
	public abstract string SiteName
	{
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		get;
	}

	/// <summary>Gets or sets the synchronization delegate for this directory server.</summary>
	/// <returns>The delegate that this directory server will use for synchronization notifications.</returns>
	public abstract SyncUpdateCallback SyncFromAllServersCallback
	{
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		get;
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		set;
	}

	/// <summary>Retrieves the inbound replication connections for this directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the inbound replication connections for this directory server.</returns>
	public abstract ReplicationConnectionCollection InboundConnections
	{
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		get;
	}

	/// <summary>Gets the outbound replication connections for this directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationConnectionCollection" /> object that contains the outbound replication connections for this directory server.</returns>
	public abstract ReplicationConnectionCollection OutboundConnections
	{
		[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		get;
	}

	internal DirectoryContext Context
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Releases all managed and unmanaged resources that are used by the object.</summary>
	public void Dispose()
	{
	}

	/// <summary>Releases all unmanaged resources and, optionally, all managed resources that are used by the object.</summary>
	/// <param name="disposing">Determines if the managed resources should be released. <see langword="true" /> if the managed resources are released; <see langword="false" /> if the managed resources are not released.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Retrieves the name of the directory server.</summary>
	/// <returns>The name of the server.</returns>
	public override string ToString()
	{
		throw new NotImplementedException();
	}

	/// <summary>Moves the directory server to another site within the forest or configuration set.</summary>
	/// <param name="siteName">The name of the site within the domain to which to move the directory server.</param>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryOperationException">A call to the underlying directory service resulted in an error.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="siteName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="siteName" /> is an empty string.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public void MoveToAnotherSite(string siteName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory server.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The object has been disposed.</exception>
	public DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Uses the Knowledge Consistency Checker (KCC) to verify and recalculate the replication topology for this server.</summary>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract void CheckReplicationConsistency();

	/// <summary>Retrieves the replication cursor information for the specified partition.</summary>
	/// <param name="partition">The distinguished name of the partition for which to retrieve the replication cursor information.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationCursorCollection" /> that contains the replication cursor information.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ReplicationCursorCollection GetReplicationCursors(string partition);

	/// <summary>Retrieves the current and pending replication operations for this directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> object that contains the current and pending replication operations.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ReplicationOperationInformation GetReplicationOperationInformation();

	/// <summary>Retrieves the replication neighbors of this directory server for the specified partition.</summary>
	/// <param name="partition">The distinguished name of the partition for which to retrieve the replication.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this object.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ReplicationNeighborCollection GetReplicationNeighbors(string partition);

	/// <summary>Retrieves all of the replication neighbors for this object.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationNeighborCollection" /> object that contains the replication neighbors for this object.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ReplicationNeighborCollection GetAllReplicationNeighbors();

	/// <summary>Retrieves a collection of the replication connection failures for this directory server.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationFailureCollection" /> object that contains the replication connection failures for this directory server.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ReplicationFailureCollection GetReplicationConnectionFailures();

	/// <summary>Retrieves the replication metadata for a specific Active Directory Domain Services object.</summary>
	/// <param name="objectPath">The path to the object for which to retrieve the replication metadata.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryReplicationMetadata" /> object that contains the replication metadata for the specified object.</returns>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract ActiveDirectoryReplicationMetadata GetReplicationMetadata(string objectPath);

	/// <summary>Causes this directory server to synchronize the specified partition with the specified directory server.</summary>
	/// <param name="partition">The distinguished name of the partition to synchronize.</param>
	/// <param name="sourceServer">The name of the server to synchronize the partition with.</param>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract void SyncReplicaFromServer(string partition, string sourceServer);

	/// <summary>Begins a synchronization of the specified partition.</summary>
	/// <param name="partition">The distinguished name of the partition to synchronize.</param>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract void TriggerSyncReplicaFromNeighbors(string partition);

	/// <summary>Causes this directory server to synchronize the specified partition with all other directory servers in the same site that hosts the partition.</summary>
	/// <param name="partition">The distinguished name of the partition to synchronize.</param>
	/// <param name="options">A combination of one or more of the <see cref="T:System.DirectoryServices.ActiveDirectory.SyncFromAllServersOptions" /> members.</param>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract void SyncReplicaFromAllServers(string partition, SyncFromAllServersOptions options);

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> class.</summary>
	protected DirectoryServer()
	{
	}
}
