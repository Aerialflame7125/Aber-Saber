using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing;

/// <summary>Specifies the event log channel for the event.</summary>
[FriendAccessAllowed]
public enum EventChannel : byte
{
	/// <summary>No channel specified.</summary>
	None = 0,
	/// <summary>The administrator log channel.</summary>
	Admin = 16,
	/// <summary>The operational channel.</summary>
	Operational = 17,
	/// <summary>The analytic channel.</summary>
	Analytic = 18,
	/// <summary>The debug channel.</summary>
	Debug = 19
}
