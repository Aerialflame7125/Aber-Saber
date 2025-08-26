namespace System.Web.Configuration;

/// <summary>Specifies the event types to be logged to the event log.</summary>
public enum ProcessModelLogLevel
{
	/// <summary>Specifies that no events are logged. This field is constant.</summary>
	None,
	/// <summary>Specifies that all process events are logged. This field is constant.</summary>
	All,
	/// <summary>Specifies that only unexpected shutdowns, memory-limit shutdowns, and deadlock shutdowns are logged. This field is constant.</summary>
	Errors
}
