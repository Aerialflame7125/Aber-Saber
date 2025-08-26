using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>The exception that is thrown when a SQL Server database is not enabled to support dependencies associated with the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class. This class cannot be inherited. </summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DatabaseNotEnabledForNotificationException : SystemException
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
	public DatabaseNotEnabledForNotificationException()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class.</summary>
	/// <param name="message">A string that describes the error. </param>
	public DatabaseNotEnabledForNotificationException(string message)
		: base(message)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="innerException">The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter is not <see langword="null" />, the current exception is raised in a catch block that handles the inner exception.</param>
	public DatabaseNotEnabledForNotificationException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
