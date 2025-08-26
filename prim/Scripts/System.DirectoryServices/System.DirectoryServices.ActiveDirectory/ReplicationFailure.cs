namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Contains information about a failed replication attempt.</summary>
public class ReplicationFailure
{
	/// <summary>Gets the DNS name of the source server.</summary>
	/// <returns>The DNS name of the source server.</returns>
	public string SourceServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the date and time that the first failure occurred.</summary>
	/// <returns>The date and time that the first failure occurred when replicating from the source server.</returns>
	public DateTime FirstFailureTime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of consecutive failures since the last successful replication.</summary>
	/// <returns>The number of consecutive failures since the last successful replication.</returns>
	public int ConsecutiveFailureCount
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the error code for the most recent failure.</summary>
	/// <returns>An HRESULT that contains the error code that is associated with the most recent failure. This will be ERROR_SUCCESS if the specific error is unavailable.</returns>
	public int LastErrorCode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the error message for the most recent failure.</summary>
	/// <returns>The error message for the most recent failure.</returns>
	public string LastErrorMessage
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
