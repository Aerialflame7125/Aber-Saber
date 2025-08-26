namespace System.Web.SessionState;

/// <summary>Identifies whether a session item from a data store is for a session that requires initialization.</summary>
public enum SessionStateActions
{
	/// <summary>No initialization actions need to be performed by the calling code.</summary>
	None,
	/// <summary>The session item from the data store is for a session that requires initialization.</summary>
	InitializeItem
}
