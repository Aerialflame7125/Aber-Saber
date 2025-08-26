namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperation" /> class represents an Active Directory Domain Services replication operation.</summary>
public class ReplicationOperation
{
	/// <summary>Contains the time that this replication operation was added to the operation queue.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time that this replication operation was added to the operation queue.</returns>
	public DateTime TimeEnqueued
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains the operation number of this replication operation.</summary>
	/// <returns>An integer that contains the operation number of this replication operation.</returns>
	public int OperationNumber
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains the priority of this replication operation.</summary>
	/// <returns>An integer that contains the priority of this replication operation.</returns>
	public int Priority
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains the type of replication operation that this operation represents.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationType" /> members that indicates the type of replication operation that this operation represents.</returns>
	public ReplicationOperationType OperationType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains the distinguished name of the partition that is associated with this replication operation.</summary>
	/// <returns>A string that contains the distinguished name of the partition that is associated with this replication operation.</returns>
	public string PartitionName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains the DNS name of the source server for this replication operation.</summary>
	/// <returns>A string that contains the DNS name of the source server for this replication operation.</returns>
	public string SourceServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
