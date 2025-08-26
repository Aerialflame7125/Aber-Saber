namespace System.Web;

/// <summary>Provides enumerated values that indicate why a process has shut down.</summary>
public enum ProcessShutdownReason
{
	/// <summary>Indicates that the process has not shut down.</summary>
	None,
	/// <summary>Indicates that the process shut down unexpectedly.</summary>
	Unexpected,
	/// <summary>Indicates that requests executed by the process exceeded the allowable limit.</summary>
	RequestsLimit,
	/// <summary>Indicates that requests assigned to the process exceeded the allowable number in the queue.</summary>
	RequestQueueLimit,
	/// <summary>Indicates that the process restarted because it was active longer than allowed.</summary>
	Timeout,
	/// <summary>Indicates that the process exceeded the allowable idle time.</summary>
	IdleTimeout,
	/// <summary>Indicates that the process exceeded the per-process memory limit.</summary>
	MemoryLimitExceeded,
	/// <summary>Indicates that the worker process did not respond to a ping from the Internet Information Services (IIS) process.</summary>
	PingFailed,
	/// <summary>Indicates that a deadlock was suspected because the response time limit was exceeded with requests in the queue.</summary>
	DeadlockSuspected
}
