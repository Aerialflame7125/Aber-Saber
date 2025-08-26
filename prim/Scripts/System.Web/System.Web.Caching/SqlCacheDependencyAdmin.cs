using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>Performs administrative tasks required on a SQL Server database to support the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class when using polling-based dependencies. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.High)]
public static class SqlCacheDependencyAdmin
{
	/// <summary>Retrieves a string array containing the name of every table that is enabled for change notifications in a SQL Server database.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <returns>A string array that contains the names of the SQL Server database tables.</returns>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database is not enabled for change notifications</exception>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static string[] GetTablesEnabledForNotifications(string connectionString)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disables <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notifications for the specified database.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.  </param>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void DisableNotifications(string connectionString)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disables <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notifications on a SQL Server database table.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <param name="table">The database table on which to disable change notifications. </param>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database is not enabled for change notifications. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="table" /> is an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="table" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void DisableTableForNotifications(string connectionString, string table)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disables <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notifications on an array of SQL Server database tables.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <param name="tables">The array of SQL Server database tables on which to disable change notifications. </param>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database is not enabled for change notifications.</exception>
	/// <exception cref="T:System.ArgumentException">One of the values in the <paramref name="tables" /> parameter is <see langword="null" />.-or-One of the values in the <paramref name="tables" /> parameter is an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="tables" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void DisableTableForNotifications(string connectionString, string[] tables)
	{
		throw new NotImplementedException();
	}

	/// <summary>Enables <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notifications on the specified database.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void EnableNotifications(string connectionString)
	{
		throw new NotImplementedException();
	}

	/// <summary>Connects to the specified SQL Server database and enables the specified database table for <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notifications.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <param name="table">The database table on which to enable change notifications.</param>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database is not enabled for change notifications.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="table" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void EnableTableForNotifications(string connectionString, string table)
	{
		throw new NotImplementedException();
	}

	/// <summary>Connects to the specified SQL Server database and enables the specified array of database tables for <see cref="T:System.Web.Caching.SqlCacheDependency" /> change notification.</summary>
	/// <param name="connectionString">A connection string used to connect to the SQL Server database.</param>
	/// <param name="tables">The array of SQL Server database tables on which to enable change notifications. </param>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database is not enabled for change notifications.</exception>
	/// <exception cref="T:System.ArgumentException">One of the values in the <paramref name="tables" /> parameter is <see langword="null" />.-or-One of the values in the <paramref name="tables" /> parameter is an empty string ("").</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="tables" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">A connection to the database could not be established.-or-The security context of the ASP.NET application does not have permission to connect to the database.-or-The security context of the ASP.NET application does not have permission to disable notifications for the database.</exception>
	public static void EnableTableForNotifications(string connectionString, string[] tables)
	{
		throw new NotImplementedException();
	}
}
