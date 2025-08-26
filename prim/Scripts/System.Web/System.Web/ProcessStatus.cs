namespace System.Web;

/// <summary>Provides enumerated values that indicate the current status of a process.</summary>
public enum ProcessStatus
{
	/// <summary>Indicates that the process is running.</summary>
	Alive = 1,
	/// <summary>Indicates that the process has begun to shut down.</summary>
	ShuttingDown,
	/// <summary>Indicates that the process has shut down normally after receiving a shutdown message from the Internet Information Services (IIS) process.</summary>
	ShutDown,
	/// <summary>Indicates that the process was forced to terminate by the Internet Information Services (IIS) process.</summary>
	Terminated
}
