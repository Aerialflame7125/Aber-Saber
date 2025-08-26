namespace System.Web.SessionState;

/// <summary>Specifies the session-state mode.</summary>
public enum SessionStateMode
{
	/// <summary>Session state is disabled.</summary>
	Off,
	/// <summary>Session state is in process with an ASP.NET worker process.</summary>
	InProc,
	/// <summary>Session state is using the out-of-process ASP.NET State Service to store state information.</summary>
	StateServer,
	/// <summary>Session state is using an out-of-process SQL Server database to store state information.</summary>
	SQLServer,
	/// <summary>Session state is using a custom data store to store session-state information.</summary>
	Custom
}
