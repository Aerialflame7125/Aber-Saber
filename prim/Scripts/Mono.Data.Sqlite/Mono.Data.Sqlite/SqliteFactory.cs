using System;
using System.Data.Common;
using System.Reflection;
using System.Security.Permissions;

namespace Mono.Data.Sqlite;

public sealed class SqliteFactory : DbProviderFactory, IServiceProvider
{
	private static Type _dbProviderServicesType;

	private static object _sqliteServices;

	public static readonly SqliteFactory Instance;

	static SqliteFactory()
	{
		Instance = new SqliteFactory();
		_dbProviderServicesType = Type.GetType("System.Data.Common.DbProviderServices, System.Data.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", throwOnError: false);
	}

	object IServiceProvider.GetService(Type serviceType)
	{
		if (serviceType == typeof(ISQLiteSchemaExtensions) || (_dbProviderServicesType != null && serviceType == _dbProviderServicesType))
		{
			return GetSQLiteProviderServicesInstance();
		}
		return null;
	}

	[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
	private object GetSQLiteProviderServicesInstance()
	{
		if (_sqliteServices == null)
		{
			Type type = Type.GetType("Mono.Data.Sqlite.SQLiteProviderServices, Mono.Data.Sqlite.Linq, Version=2.0.38.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", throwOnError: false);
			if (type != null)
			{
				_sqliteServices = type.GetField("Instance", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
			}
		}
		return _sqliteServices;
	}

	public override DbCommand CreateCommand()
	{
		return new SqliteCommand();
	}

	public override DbCommandBuilder CreateCommandBuilder()
	{
		return new SqliteCommandBuilder();
	}

	public override DbConnection CreateConnection()
	{
		return new SqliteConnection();
	}

	public override DbConnectionStringBuilder CreateConnectionStringBuilder()
	{
		return new SqliteConnectionStringBuilder();
	}

	public override DbDataAdapter CreateDataAdapter()
	{
		return new SqliteDataAdapter();
	}

	public override DbParameter CreateParameter()
	{
		return new SqliteParameter();
	}
}
