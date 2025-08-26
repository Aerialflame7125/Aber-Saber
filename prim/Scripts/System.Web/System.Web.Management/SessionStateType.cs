namespace System.Web.Management;

/// <summary>Describes the session-state type used when installing a session-state database provider.</summary>
public enum SessionStateType
{
	/// <summary>Session state data is stored in tempdb, and stored procedures are placed in the "ASPState" database. Session state data will not survive a restart of SQL Server.</summary>
	Temporary,
	/// <summary>Session-state data and stored procedures are placed in the "ASPState" database. Session-state data will survive a restart of the database server.</summary>
	Persisted,
	/// <summary>Session-state data and stored procedures are placed in a custom data store.</summary>
	Custom
}
