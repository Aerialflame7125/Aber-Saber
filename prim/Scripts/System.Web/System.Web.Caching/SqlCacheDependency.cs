using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>Establishes a relationship between an item stored in an ASP.NET application's <see cref="T:System.Web.Caching.Cache" /> object and either a specific SQL Server database table or the results of a SQL Server 2005 query. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class SqlCacheDependency : CacheDependency
{
	private string uniqueId = Guid.NewGuid().ToString();

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class, using the supplied <see cref="T:System.Data.SqlClient.SqlCommand" /> to create a cache-key dependency.</summary>
	/// <param name="sqlCmd">A <see cref="T:System.Data.SqlClient.SqlCommand" /> that is used to create a <see cref="T:System.Web.Caching.SqlCacheDependency" /> object.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sqlCmd" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> instance has its <see cref="P:System.Data.SqlClient.SqlCommand.NotificationAutoEnlist" /> property set to <see langword="true" /> and there is an  directive on the page with the <see langword="SqlDependency" /> attribute set to CommandNotification.</exception>
	[MonoTODO("What to do with the sqlCmd?")]
	public SqlCacheDependency(SqlCommand sqlCmd)
	{
		if (sqlCmd == null)
		{
			throw new ArgumentNullException("sqlCmd");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.SqlCacheDependency" /> class, using the supplied parameters to create a cache-key dependency.</summary>
	/// <param name="databaseEntryName">The name of a database defined in the databases element of the application's Web.config file. </param>
	/// <param name="tableName">The name of the database table that the <see cref="T:System.Web.Caching.SqlCacheDependency" /> is associated with. </param>
	/// <exception cref="T:System.Web.HttpException">The internal check for <see cref="T:System.Data.SqlClient.SqlClientPermission" /> failed.- or -The <paramref name="databaseEntryName" /> was not found in the list of databases configured for table-based notifications.- or -The <see cref="T:System.Web.Caching.SqlCacheDependency" /> object could not connect to the database during initialization.- or -The <see cref="T:System.Web.Caching.SqlCacheDependency" /> object encountered a permission-denied error either on the database or on the database stored procedures that support the <see cref="T:System.Web.Caching.SqlCacheDependency" /> object.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="tableName" /> parameter is <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Polling is not enabled for the <see cref="T:System.Web.Caching.SqlCacheDependency" />. - or -The polling interval is not correctly configured.- or -No connection string was specified in the application's configuration file.- or -The connection string specified in the application's configuration file could not be found.- or -The connection string specified in the application's configuration file is an empty string.</exception>
	/// <exception cref="T:System.Web.Caching.DatabaseNotEnabledForNotificationException">The database specified in the <paramref name="databaseEntryName" /> parameter is not enabled for change notifications. </exception>
	/// <exception cref="T:System.Web.Caching.TableNotEnabledForNotificationException">The database table specified in the <paramref name="tableName" /> parameter is not enabled for change notifications. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="databaseEntryName" /> is <see langword="null" />.- or -
	///         <paramref name="tableName" /> is <see langword="null" />.</exception>
	[MonoTODO("What are the params good for?")]
	public SqlCacheDependency(string databaseEntryName, string tableName)
	{
		if (databaseEntryName == null)
		{
			throw new ArgumentNullException("databaseEntryName");
		}
		if (tableName == null)
		{
			throw new ArgumentNullException("tableName");
		}
	}

	/// <summary>Creates a dependency relationship between an item that is stored in an ASP.NET application's <see cref="T:System.Web.Caching.OutputCache" /> object and a SQL Server database table.</summary>
	/// <param name="dependency">The output-cache dependency directive.</param>
	/// <returns>The new dependency object.</returns>
	[MonoTODO("Needs more testing - especially the return value and database+table lookup.")]
	public static CacheDependency CreateOutputCacheDependency(string dependency)
	{
		if (dependency == null)
		{
			throw new HttpException(InvalidDependencyFormatMessage(dependency));
		}
		if (dependency.Length == 0)
		{
			throw new ArgumentException(InvalidDependencyFormatMessage(dependency), "dependency");
		}
		string[] array = dependency.Split(';');
		List<SqlCacheDependency> list = new List<SqlCacheDependency>();
		string[] array2 = array;
		foreach (string text in array2)
		{
			int num = text.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException(InvalidDependencyFormatMessage(dependency), "dependency");
			}
			list.Add(new SqlCacheDependency(text.Substring(0, num), text.Substring(num + 1)));
		}
		switch (list.Count)
		{
		case 0:
			return null;
		case 1:
			return list[0];
		default:
		{
			AggregateCacheDependency aggregateCacheDependency = new AggregateCacheDependency();
			aggregateCacheDependency.Add(list.ToArray());
			return aggregateCacheDependency;
		}
		}
	}

	private static string InvalidDependencyFormatMessage(string dependency)
	{
		return string.Format("The '' SqlDependency attribute for OutputCache directive is invalid.\n\nFor SQL Server 7.0 and SQL Server 2000, the valid format is \"database:tablename\", and table name must conform to the format of regular identifiers in SQL. To specify multiple pairs of values, use the ';' separator between pairs. (To specify ':', '\\' or ';', prefix it with the '\\' escape character.)\n\nFor dependencies that use SQL Server 9.0 notifications, specify the value 'CommandNotification'.", dependency);
	}

	protected override void DependencyDispose()
	{
		base.DependencyDispose();
	}

	/// <summary>Retrieves a unique identifier for a <see cref="T:System.Web.Caching.SqlCacheDependency" /> object.</summary>
	/// <returns>The unique identifier for the <see cref="T:System.Web.Caching.SqlCacheDependency" /> object, or null if no identifier can be generated.</returns>
	public override string GetUniqueID()
	{
		return uniqueId;
	}
}
