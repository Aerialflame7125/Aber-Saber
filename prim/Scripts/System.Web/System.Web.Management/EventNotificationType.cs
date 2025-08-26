namespace System.Web.Management;

/// <summary>Specifies the type of event notification.</summary>
public enum EventNotificationType
{
	/// <summary>The notification of an event is triggered on a regularly scheduled interval.</summary>
	Regular,
	/// <summary>Notification triggered by exceeding the urgent event threshold.</summary>
	Urgent,
	/// <summary>The notification of an event is triggered by a requested flush.</summary>
	Flush,
	/// <summary>Every event is treated as if a flush has occurred.</summary>
	Unbuffered
}
