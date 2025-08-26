namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> class contains information about an Active Directory Domain Services replication operation.</summary>
public class ReplicationOperationInformation
{
	/// <summary>Gets the time that this replication operation started.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time that this replication operation started.</returns>
	public DateTime OperationStartTime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the current replication operation.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperation" /> object that represents the current replication operation.</returns>
	public ReplicationOperation CurrentOperation
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the replication operations that have not been run.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationCollection" /> object that contains the pending replication operations.</returns>
	public ReplicationOperationCollection PendingOperations
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.ActiveDirectory.ReplicationOperationInformation" /> class.</summary>
	public ReplicationOperationInformation()
	{
	}
}
