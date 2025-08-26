using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Manages storage of role membership information for an ASP.NET application in a SQL Server database.</summary>
public class SqlRoleProvider : RoleProvider
{
	private string applicationName;

	private bool schemaIsOk;

	private ConnectionStringSettings connectionString;

	private DbProviderFactory factory;

	/// <summary>Gets or sets the name of the application for which to store and retrieve role information.</summary>
	/// <returns>The name of the application for which to store and retrieve role information. The default is the <see cref="P:System.Web.HttpRequest.ApplicationPath" /> property value for the current <see cref="P:System.Web.HttpContext.Request" />.</returns>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.Security.SqlRoleProvider.ApplicationName" /> property by a caller that does not have <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> ASP.NET hosting permission.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Security.SqlRoleProvider.ApplicationName" /> to a string that is longer than 256 characters.</exception>
	public override string ApplicationName
	{
		get
		{
			return applicationName;
		}
		set
		{
			applicationName = value;
		}
	}

	private DbConnection CreateConnection()
	{
		if (!schemaIsOk && !(schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(factory, connectionString.ConnectionString, "role manager", "1")))
		{
			throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
		}
		DbConnection dbConnection = factory.CreateConnection();
		dbConnection.ConnectionString = connectionString.ConnectionString;
		dbConnection.Open();
		return dbConnection;
	}

	private static void AddParameter(DbCommand command, string parameterName, object parameterValue)
	{
		AddParameter(command, parameterName, ParameterDirection.Input, parameterValue);
	}

	private static DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}

	private static DbParameter AddParameter(DbCommand command, string parameterName, ParameterDirection direction, DbType type, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		dbParameter.DbType = type;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}

	/// <summary>Adds the specified user names to each of the specified roles.</summary>
	/// <param name="usernames">A string array of user names to be added to the specified roles.</param>
	/// <param name="roleNames">A string array of role names to add the specified user names to.</param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is <see langword="null" />.-or-One of the users in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma.-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma.-or-One of the roles in <paramref name="roleNames" /> is longer than 256 characters.-or-One of the users in <paramref name="usernames" /> is longer than 256 characters.-or-
	///         <paramref name="roleNames" /> contains a duplicate element.-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">One or more of the specified role names was not found.-or- One or more of the specified user names was not found.-or- One or more of the specified user names is already associated with one or more of the specified role names.-or- An unknown error occurred while communicating with the database.</exception>
	public override void AddUsersToRoles(string[] usernames, string[] roleNames)
	{
		Hashtable hashtable = new Hashtable();
		string[] array = usernames;
		foreach (string text in array)
		{
			if (text == null)
			{
				throw new ArgumentNullException("null element in usernames array");
			}
			if (hashtable.ContainsKey(text))
			{
				throw new ArgumentException("duplicate element in usernames array");
			}
			if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in usernames array in illegal format");
			}
			hashtable.Add(text, text);
		}
		hashtable = new Hashtable();
		array = roleNames;
		foreach (string text2 in array)
		{
			if (text2 == null)
			{
				throw new ArgumentNullException("null element in rolenames array");
			}
			if (hashtable.ContainsKey(text2))
			{
				throw new ArgumentException("duplicate element in rolenames array");
			}
			if (text2.Length == 0 || text2.Length > 256 || text2.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in rolenames array in illegal format");
			}
			hashtable.Add(text2, text2);
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_AddUsersToRoles";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@RoleNames", string.Join(",", roleNames));
		AddParameter(dbCommand, "@UserNames", string.Join(",", usernames));
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@CurrentTimeUtc", DateTime.UtcNow);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		switch ((int)dbParameter.Value)
		{
		case 0:
			break;
		case 2:
			throw new ProviderException("One or more of the specified user/role names was not found.");
		case 3:
			throw new ProviderException("One or more of the specified user names is already associated with one or more of the specified role names.");
		default:
			throw new ProviderException("Failed to create new user/role association.");
		}
	}

	/// <summary>Adds a new role to the role database.</summary>
	/// <param name="roleName">The name of the role to create. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="roleName" /> already exists in the database.-or- An unknown error occurred while communicating with the database. </exception>
	public override void CreateRole(string roleName)
	{
		if (roleName == null)
		{
			throw new ArgumentNullException("roleName");
		}
		if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
		{
			throw new ArgumentException("rolename is in invalid format");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_Roles_CreateRole";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@RoleName", roleName);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		if ((int)dbParameter.Value == 1)
		{
			throw new ProviderException(roleName + " already exists in the database");
		}
	}

	/// <summary>Removes a role from the role database.</summary>
	/// <param name="roleName">The name of the role to delete.</param>
	/// <param name="throwOnPopulatedRole">If <see langword="true" />, throws an exception if <paramref name="roleName" /> has one or more members.</param>
	/// <returns>
	///     <see langword="true" /> if the role was successfully deleted; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is <see langword="true" />.-or- An unknown error occurred while communicating with the database. </exception>
	public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
	{
		if (roleName == null)
		{
			throw new ArgumentNullException("roleName");
		}
		if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
		{
			throw new ArgumentException("rolename is in invalid format");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_Roles_DeleteRole";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@RoleName", roleName);
		AddParameter(dbCommand, "@DeleteOnlyIfRoleIsEmpty", throwOnPopulatedRole);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		int num = (int)dbParameter.Value;
		switch (num)
		{
		case 0:
			return true;
		case 1:
			return false;
		default:
			if (num == 2 && throwOnPopulatedRole)
			{
				throw new ProviderException(roleName + " is not empty");
			}
			return false;
		}
	}

	/// <summary>Gets an array of user names in a role where the user name contains the specified user name to match.</summary>
	/// <param name="roleName">The role to search in.</param>
	/// <param name="usernameToMatch">The user name to search for.</param>
	/// <returns>A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch" /> and the user is a member of the specified role.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).-or-
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="usernameToMatch" /> is an empty string.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.-or-
	///         <paramref name="usernameToMatch" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="roleName" /> was not found in the database.-or- An unknown error occurred while communicating with the database. </exception>
	public override string[] FindUsersInRole(string roleName, string usernameToMatch)
	{
		if (roleName == null)
		{
			throw new ArgumentNullException("roleName");
		}
		if (usernameToMatch == null)
		{
			throw new ArgumentNullException("usernameToMatch");
		}
		if (roleName.Length == 0 || roleName.Length > 256 || roleName.IndexOf(',') != -1)
		{
			throw new ArgumentException("roleName is in invalid format");
		}
		if (usernameToMatch.Length == 0 || usernameToMatch.Length > 256)
		{
			throw new ArgumentException("usernameToMatch is in invalid format");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_FindUsersInRole";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@RoleName", roleName);
		AddParameter(dbCommand, "@UsernameToMatch", usernameToMatch);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		ArrayList arrayList = new ArrayList();
		while (dbDataReader.Read())
		{
			arrayList.Add(dbDataReader.GetString(0));
		}
		dbDataReader.Close();
		return (string[])arrayList.ToArray(typeof(string));
	}

	/// <summary>Gets a list of all the roles for the application.</summary>
	/// <returns>A string array containing the names of all the roles stored in the database for a particular application.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database.</exception>
	public override string[] GetAllRoles()
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_Roles_GetAllRoles";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		ArrayList arrayList = new ArrayList();
		while (dbDataReader.Read())
		{
			arrayList.Add(dbDataReader.GetString(0));
		}
		dbDataReader.Close();
		return (string[])arrayList.ToArray(typeof(string));
	}

	/// <summary>Gets a list of the roles that a user is in.</summary>
	/// <param name="username">The user to return a list of roles for. </param>
	/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> contains a comma.-or-
	///         <paramref name="username" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
	public override string[] GetRolesForUser(string username)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_GetRolesForUser";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		ArrayList arrayList = new ArrayList();
		while (dbDataReader.Read())
		{
			arrayList.Add(dbDataReader.GetString(0));
		}
		dbDataReader.Close();
		return (string[])arrayList.ToArray(typeof(string));
	}

	/// <summary>Gets a list of users in the specified role.</summary>
	/// <param name="roleName">The name of the role to get the list of users for. </param>
	/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="rolename" /> was not found in the database.-or- An unknown error occurred while communicating with the database. </exception>
	public override string[] GetUsersInRole(string roleName)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_GetUsersInRoles";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@RoleName", roleName);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		DbDataReader dbDataReader = dbCommand.ExecuteReader();
		ArrayList arrayList = new ArrayList();
		while (dbDataReader.Read())
		{
			arrayList.Add(dbDataReader.GetString(0));
		}
		dbDataReader.Close();
		return (string[])arrayList.ToArray(typeof(string));
	}

	private string GetStringConfigValue(NameValueCollection config, string name, string def)
	{
		string result = def;
		string text = config[name];
		if (text != null)
		{
			result = text;
		}
		return result;
	}

	/// <summary>Initializes the SQL Server role provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Security.SqlRoleProvider" /> instance to initialize. </param>
	/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the role provider. </param>
	/// <exception cref="T:System.Web.HttpException">The ASP.NET application is not running at <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" />  trust or higher. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="config" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="connectionStringName" /> attribute is empty or does not exist in the application configuration file for this <see cref="T:System.Web.Security.SqlRoleProvider" /> instance.-or-The <see langword="applicationName" /> attribute exceeds 256 characters.-or-The application configuration file for this <see cref="T:System.Web.Security.SqlRoleProvider" /> instance contains an unrecognized attribute. </exception>
	public override void Initialize(string name, NameValueCollection config)
	{
		if (config == null)
		{
			throw new ArgumentNullException("config");
		}
		base.Initialize(name, config);
		applicationName = GetStringConfigValue(config, "applicationName", "/");
		string text = config["connectionStringName"];
		if (applicationName.Length > 256)
		{
			throw new ProviderException("The ApplicationName attribute must be 256 characters long or less.");
		}
		if (text == null || text.Length == 0)
		{
			throw new ProviderException("The ConnectionStringName attribute must be present and non-zero length.");
		}
		connectionString = WebConfigurationManager.ConnectionStrings[text];
		if (connectionString == null)
		{
			throw new ProviderException($"The connection name '{text}' was not found in the applications configuration or the connection string is empty.");
		}
		factory = (string.IsNullOrEmpty(connectionString.ProviderName) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(connectionString.ProviderName));
	}

	/// <summary>Gets a value indicating whether the specified user is in the specified role.</summary>
	/// <param name="username">The user name to search for. </param>
	/// <param name="roleName">The role to search in. </param>
	/// <returns>
	///     <see langword="true" /> if the specified user name is in the specified role; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="username" /> is contains a comma.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.-or-
	///         <paramref name="username" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
	public override bool IsUserInRole(string username, string roleName)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_IsUserInRole";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@RoleName", roleName);
		AddParameter(dbCommand, "@UserName", username);
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		if ((int)dbParameter.Value == 1)
		{
			return true;
		}
		return false;
	}

	/// <summary>Removes the specified user names from the specified roles.</summary>
	/// <param name="usernames">A string array of user names to be removed from the specified roles. </param>
	/// <param name="roleNames">A string array of role names to remove the specified user names from. </param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is <see langword="null" />.-or-One of the users in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma.-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma.-or-One of the roles in <paramref name="roleNames" /> is longer than 256 characters.-or-One of the users in <paramref name="usernames" /> is longer than 256 characters.-or-
	///         <paramref name="roleNames" /> contains a duplicate element.-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">One or more of the specified user names was not found.-or- One or more of the specified role names was not found.-or- One or more of the specified user names is not associated with one or more of the specified role names.-or- An unknown error occurred while communicating with the database. </exception>
	public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
	{
		Hashtable hashtable = new Hashtable();
		string[] array = usernames;
		foreach (string text in array)
		{
			if (text == null)
			{
				throw new ArgumentNullException("null element in usernames array");
			}
			if (hashtable.ContainsKey(text))
			{
				throw new ArgumentException("duplicate element in usernames array");
			}
			if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in usernames array in illegal format");
			}
			hashtable.Add(text, text);
		}
		hashtable = new Hashtable();
		array = roleNames;
		foreach (string text2 in array)
		{
			if (text2 == null)
			{
				throw new ArgumentNullException("null element in rolenames array");
			}
			if (hashtable.ContainsKey(text2))
			{
				throw new ArgumentException("duplicate element in rolenames array");
			}
			if (text2.Length == 0 || text2.Length > 256 || text2.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in rolenames array in illegal format");
			}
			hashtable.Add(text2, text2);
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_UsersInRoles_RemoveUsersFromRoles";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@UserNames", string.Join(",", usernames));
		AddParameter(dbCommand, "@RoleNames", string.Join(",", roleNames));
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		switch ((int)dbParameter.Value)
		{
		case 0:
			break;
		case 1:
			throw new ProviderException("One or more of the specified user names was not found.");
		case 2:
			throw new ProviderException("One or more of the specified role names was not found.");
		case 3:
			throw new ProviderException("One or more of the specified user names is not associated with one or more of the specified role names.");
		default:
			throw new ProviderException("Failed to remove users from roles");
		}
	}

	/// <summary>Gets a value indicating whether the specified role name already exists in the role database.</summary>
	/// <param name="roleName">The name of the role to search for in the database. </param>
	/// <returns>
	///     <see langword="true" /> if the role name already exists in the database; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma.-or-
	///         <paramref name="roleName" /> is longer than 256 characters.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An unknown error occurred while communicating with the database. </exception>
	public override bool RoleExists(string roleName)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = "dbo.aspnet_Roles_RoleExists";
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(dbCommand, "@ApplicationName", ApplicationName);
		AddParameter(dbCommand, "@RoleName", roleName);
		DbParameter dbParameter = AddParameter(dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, DbType.Int32, null);
		dbCommand.ExecuteNonQuery();
		if ((int)dbParameter.Value == 1)
		{
			return true;
		}
		return false;
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.SqlRoleProvider" /> class.</summary>
	public SqlRoleProvider()
	{
	}
}
