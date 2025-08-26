namespace System.DirectoryServices.ActiveDirectory;

/// <summary>Represents the notification status of a replication connection.</summary>
public enum NotificationStatus
{
	/// <summary>Do not send notifications.</summary>
	NoNotification,
	/// <summary>Send notifications only for intra-site connections.</summary>
	IntraSiteOnly,
	/// <summary>Always send notifications.</summary>
	NotificationAlways
}
