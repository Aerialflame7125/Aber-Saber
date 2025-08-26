namespace System.Windows.Forms;

/// <summary>Specifies the system power status.</summary>
public enum PowerLineStatus
{
	/// <summary>The system is offline.</summary>
	Offline = 0,
	/// <summary>The system is online.</summary>
	Online = 1,
	/// <summary>The power status of the system is unknown.</summary>
	Unknown = 255
}
