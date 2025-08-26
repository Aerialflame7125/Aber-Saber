using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Web.Hosting;
using System.Web.Properties;
using Mono.Data.Sqlite;

namespace System.Web.Security;

internal class SqliteRoleProvider : RoleProvider
{
	private const string m_RolesTableName = "Roles";

	private const string m_UserInRolesTableName = "UsersInRoles";

	private string m_ConnectionString = string.Empty;

	private string m_ApplicationName = string.Empty;

	public override string ApplicationName
	{
		get
		{
			return m_ApplicationName;
		}
		set
		{
			m_ApplicationName = value;
		}
	}

	private DbParameter AddParameter(DbCommand command, string parameterName)
	{
		return AddParameter(command, parameterName, null);
	}

	private DbParameter AddParameter(DbCommand command, string parameterName, object parameterValue)
	{
		return AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
	}

	private DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}

	public override void Initialize(string name, NameValueCollection config)
	{
		if (config == null)
		{
			throw new ArgumentNullException("Config", System.Web.Properties.Resources.ErrArgumentNull);
		}
		if (string.IsNullOrEmpty(name))
		{
			name = System.Web.Properties.Resources.RoleProviderDefaultName;
		}
		if (string.IsNullOrEmpty(config["description"]))
		{
			config.Remove("description");
			config.Add("description", System.Web.Properties.Resources.RoleProviderDefaultDescription);
		}
		base.Initialize(name, config);
		m_ApplicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
		string text = config["connectionStringName"];
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentOutOfRangeException("ConnectionStringName", System.Web.Properties.Resources.ErrArgumentNullOrEmpty);
		}
		ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
		if (connectionStringSettings == null || string.IsNullOrEmpty(connectionStringSettings.ConnectionString.Trim()))
		{
			throw new ProviderException(System.Web.Properties.Resources.ErrConnectionStringNullOrEmpty);
		}
		m_ConnectionString = connectionStringSettings.ConnectionString;
	}

	public override void AddUsersToRoles(string[] userNames, string[] roleNames)
	{
		string[] array = roleNames;
		foreach (string text in array)
		{
			if (!RoleExists(text))
			{
				throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrRoleNotExist, text));
			}
		}
		array = userNames;
		foreach (string text2 in array)
		{
			string[] array2 = roleNames;
			foreach (string text3 in array2)
			{
				if (IsUserInRole(text2, text3))
				{
					throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrUserAlreadyInRole, text2, text3));
				}
			}
		}
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"Username\", \"Rolename\", \"ApplicationName\") Values (@Username, @Rolename, @ApplicationName)", "UsersInRoles");
		AddParameter(sqliteCommand, "@Username");
		AddParameter(sqliteCommand, "@Rolename");
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		SqliteTransaction sqliteTransaction = null;
		try
		{
			sqliteConnection.Open();
			sqliteCommand.Prepare();
			using (sqliteTransaction = sqliteConnection.BeginTransaction())
			{
				array = userNames;
				foreach (string value in array)
				{
					string[] array2 = roleNames;
					foreach (string value2 in array2)
					{
						sqliteCommand.Parameters["@Username"].Value = value;
						sqliteCommand.Parameters["@Rolename"].Value = value2;
						sqliteCommand.ExecuteNonQuery();
					}
				}
				sqliteTransaction.Commit();
			}
		}
		catch (SqliteException)
		{
			try
			{
				sqliteTransaction.Rollback();
			}
			catch (SqliteException)
			{
			}
			throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
		}
		finally
		{
			sqliteConnection?.Close();
		}
	}

	public override void CreateRole(string roleName)
	{
		if (RoleExists(roleName))
		{
			throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrRoleAlreadyExist, roleName));
		}
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"Rolename\", \"ApplicationName\") Values (@Rolename, @ApplicationName)", "Roles");
		AddParameter(sqliteCommand, "@Rolename", roleName);
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		try
		{
			sqliteConnection.Open();
			sqliteCommand.Prepare();
			sqliteCommand.ExecuteNonQuery();
		}
		catch (SqliteException)
		{
			throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
		}
		finally
		{
			sqliteConnection?.Close();
		}
	}

	public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
	{
		if (!RoleExists(roleName))
		{
			throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrRoleNotExist, roleName));
		}
		if (throwOnPopulatedRole && GetUsersInRole(roleName).Length != 0)
		{
			throw new ProviderException(System.Web.Properties.Resources.ErrCantDeletePopulatedRole);
		}
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "Roles");
			AddParameter(sqliteCommand, "@Rolename", roleName);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			SqliteTransaction sqliteTransaction = null;
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using (sqliteTransaction = sqliteConnection.BeginTransaction())
				{
					sqliteCommand.ExecuteNonQuery();
					sqliteTransaction.Commit();
				}
			}
			catch (SqliteException)
			{
				try
				{
					sqliteTransaction.Rollback();
				}
				catch (SqliteException)
				{
				}
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return true;
	}

	public override string[] FindUsersInRole(string roleName, string usernameToMatch)
	{
		List<string> list = new List<string>();
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT \"Username\" FROM \"{0}\" WHERE \"Username\" LIKE @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName ORDER BY \"Username\" ASC", "UsersInRoles");
			AddParameter(sqliteCommand, "@Username", usernameToMatch);
			AddParameter(sqliteCommand, "@Rolename", roleName);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
				if (sqliteDataReader.HasRows)
				{
					while (sqliteDataReader.Read())
					{
						list.Add(sqliteDataReader.GetString(0));
					}
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return list.ToArray();
	}

	public override string[] GetAllRoles()
	{
		List<string> list = new List<string>();
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT \"Rolename\" FROM \"{0}\" WHERE \"ApplicationName\" = @ApplicationName ORDER BY \"Rolename\" ASC", "Roles");
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
				while (sqliteDataReader.Read())
				{
					list.Add(sqliteDataReader.GetString(0));
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return list.ToArray();
	}

	public override string[] GetRolesForUser(string username)
	{
		List<string> list = new List<string>();
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT \"Rolename\" FROM \"{0}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName ORDER BY \"Rolename\" ASC", "UsersInRoles");
			AddParameter(sqliteCommand, "@Username", username);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
				if (sqliteDataReader.HasRows)
				{
					while (sqliteDataReader.Read())
					{
						list.Add(sqliteDataReader.GetString(0));
					}
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return list.ToArray();
	}

	public override string[] GetUsersInRole(string roleName)
	{
		List<string> list = new List<string>();
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT \"Username\" FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName ORDER BY \"Username\" ASC", "UsersInRoles");
			AddParameter(sqliteCommand, "@Rolename", roleName);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
				if (sqliteDataReader.HasRows)
				{
					while (sqliteDataReader.Read())
					{
						list.Add(sqliteDataReader.GetString(0));
					}
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return list.ToArray();
	}

	public override bool IsUserInRole(string userName, string roleName)
	{
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Username\" = @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "UsersInRoles");
			AddParameter(sqliteCommand, "@Username", userName);
			AddParameter(sqliteCommand, "@Rolename", roleName);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				int result = 0;
				int.TryParse(sqliteCommand.ExecuteScalar().ToString(), out result);
				if (result > 0)
				{
					return true;
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return false;
	}

	public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
	{
		string[] array = roleNames;
		foreach (string text in array)
		{
			if (!RoleExists(text))
			{
				throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrRoleNotExist, text));
			}
		}
		array = userNames;
		foreach (string text2 in array)
		{
			string[] array2 = roleNames;
			foreach (string text3 in array2)
			{
				if (!IsUserInRole(text2, text3))
				{
					throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrUserIsNotInRole, text2, text3));
				}
			}
		}
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Username\" = @Username AND \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "UsersInRoles");
		AddParameter(sqliteCommand, "@Username");
		AddParameter(sqliteCommand, "@Rolename");
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		SqliteTransaction sqliteTransaction = null;
		try
		{
			sqliteConnection.Open();
			sqliteCommand.Prepare();
			using (sqliteTransaction = sqliteConnection.BeginTransaction())
			{
				array = userNames;
				foreach (string value in array)
				{
					string[] array2 = roleNames;
					foreach (string value2 in array2)
					{
						sqliteCommand.Parameters["@Username"].Value = value;
						sqliteCommand.Parameters["@Rolename"].Value = value2;
						sqliteCommand.ExecuteNonQuery();
					}
				}
				sqliteTransaction.Commit();
			}
		}
		catch (SqliteException)
		{
			try
			{
				sqliteTransaction.Rollback();
			}
			catch (SqliteException)
			{
			}
			throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
		}
		finally
		{
			sqliteConnection?.Close();
		}
	}

	public override bool RoleExists(string roleName)
	{
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Rolename\" = @Rolename AND \"ApplicationName\" = @ApplicationName", "Roles");
			AddParameter(sqliteCommand, "@Rolename", roleName);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				int result = 0;
				int.TryParse(sqliteCommand.ExecuteScalar().ToString(), out result);
				if (result > 0)
				{
					return true;
				}
			}
			catch (SqliteException)
			{
				throw new ProviderException(System.Web.Properties.Resources.ErrOperationAborted);
			}
			finally
			{
				sqliteConnection?.Close();
			}
		}
		return false;
	}

	private string GetConfigValue(string configValue, string defaultValue)
	{
		if (string.IsNullOrEmpty(configValue))
		{
			return defaultValue;
		}
		return configValue;
	}
}
