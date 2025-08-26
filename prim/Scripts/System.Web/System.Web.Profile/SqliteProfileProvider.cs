using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Web.Hosting;
using System.Web.Properties;
using System.Web.Util;
using Mono.Data.Sqlite;

namespace System.Web.Profile;

internal class SqliteProfileProvider : ProfileProvider
{
	private const string m_ProfilesTableName = "Profiles";

	private const string m_ProfileDataTableName = "ProfileData";

	private string m_ConnectionString = string.Empty;

	private SerializationHelper m_serializationHelper = new SerializationHelper();

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
			name = System.Web.Properties.Resources.ProfileProviderDefaultName;
		}
		if (string.IsNullOrEmpty(config["description"]))
		{
			config.Remove("description");
			config.Add("description", System.Web.Properties.Resources.ProfileProviderDefaultDescription);
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

	public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		throw new Exception("DeleteInactiveProfiles: The method or operation is not implemented.");
	}

	public override int DeleteProfiles(string[] usernames)
	{
		throw new Exception("DeleteProfiles1: The method or operation is not implemented.");
	}

	public override int DeleteProfiles(ProfileInfoCollection profiles)
	{
		throw new Exception("DeleteProfiles2: The method or operation is not implemented.");
	}

	public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new Exception("FindInactiveProfilesByUserName: The method or operation is not implemented.");
	}

	public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new Exception("FindProfilesByUserName: The method or operation is not implemented.");
	}

	public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new Exception("GetAllInactiveProfiles: The method or operation is not implemented.");
	}

	public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new Exception("GetAllProfiles: The method or operation is not implemented.");
	}

	public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		throw new Exception("GetNumberOfInactiveProfiles: The method or operation is not implemented.");
	}

	public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
	{
		SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
		string text = (string)context["UserName"];
		bool flag = (bool)context["IsAuthenticated"];
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT \"Name\", \"ValueString\", \"ValueBinary\" FROM \"{0}\" WHERE \"Profile\" = (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated)", "ProfileData", "Profiles");
			AddParameter(sqliteCommand, "@Username", text);
			AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
			AddParameter(sqliteCommand, "@IsAuthenticated", !flag);
			try
			{
				sqliteConnection.Open();
				sqliteCommand.Prepare();
				using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
				while (sqliteDataReader.Read())
				{
					object value = null;
					if (!sqliteDataReader.IsDBNull(1))
					{
						value = sqliteDataReader.GetValue(1);
					}
					else if (!sqliteDataReader.IsDBNull(2))
					{
						value = sqliteDataReader.GetValue(2);
					}
					dictionary.Add(sqliteDataReader.GetString(0), value);
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
		foreach (SettingsProperty item in collection)
		{
			if (item.SerializeAs == SettingsSerializeAs.ProviderSpecific)
			{
				if (item.PropertyType.IsPrimitive || item.PropertyType.Equals(typeof(string)))
				{
					item.SerializeAs = SettingsSerializeAs.String;
				}
				else
				{
					item.SerializeAs = SettingsSerializeAs.Xml;
				}
			}
			SettingsPropertyValue settingsPropertyValue = new SettingsPropertyValue(item);
			if (dictionary.ContainsKey(item.Name) && dictionary[item.Name] != null)
			{
				if (item.SerializeAs == SettingsSerializeAs.String)
				{
					settingsPropertyValue.PropertyValue = m_serializationHelper.DeserializeFromBase64((string)dictionary[item.Name]);
				}
				else if (item.SerializeAs == SettingsSerializeAs.Xml)
				{
					settingsPropertyValue.PropertyValue = m_serializationHelper.DeserializeFromXml((string)dictionary[item.Name]);
				}
				else if (item.SerializeAs == SettingsSerializeAs.Binary)
				{
					settingsPropertyValue.PropertyValue = m_serializationHelper.DeserializeFromBinary((byte[])dictionary[item.Name]);
				}
			}
			settingsPropertyValue.IsDirty = false;
			settingsPropertyValueCollection.Add(settingsPropertyValue);
		}
		UpdateActivityDates(text, flag, activityOnly: true);
		return settingsPropertyValueCollection;
	}

	public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
	{
		string text = (string)context["UserName"];
		bool flag = (bool)context["IsAuthenticated"];
		if (collection.Count < 1)
		{
			return;
		}
		if (!ProfileExists(text))
		{
			CreateProfileForUser(text, flag);
		}
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		using SqliteCommand sqliteCommand2 = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = string.Format("DELETE FROM \"{0}\" WHERE \"Name\" = @Name AND \"Profile\" = (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated)", "ProfileData", "Profiles");
		AddParameter(sqliteCommand, "@Name");
		AddParameter(sqliteCommand, "@Username", text);
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		AddParameter(sqliteCommand, "@IsAuthenticated", !flag);
		sqliteCommand2.CommandText = string.Format("INSERT INTO \"{0}\" (\"pId\", \"Profile\", \"Name\", \"ValueString\", \"ValueBinary\") VALUES (@pId, (SELECT \"pId\" FROM \"{1}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated), @Name, @ValueString, @ValueBinary)", "ProfileData", "Profiles");
		AddParameter(sqliteCommand2, "@pId");
		AddParameter(sqliteCommand2, "@Name");
		AddParameter(sqliteCommand2, "@ValueString");
		sqliteCommand2.Parameters["@ValueString"].IsNullable = true;
		AddParameter(sqliteCommand2, "@ValueBinary");
		sqliteCommand2.Parameters["@ValueBinary"].IsNullable = true;
		AddParameter(sqliteCommand2, "@Username", text);
		AddParameter(sqliteCommand2, "@ApplicationName", m_ApplicationName);
		AddParameter(sqliteCommand2, "@IsAuthenticated", !flag);
		SqliteTransaction sqliteTransaction = null;
		try
		{
			sqliteConnection.Open();
			sqliteCommand.Prepare();
			sqliteCommand2.Prepare();
			using (sqliteTransaction = sqliteConnection.BeginTransaction())
			{
				foreach (SettingsPropertyValue item in collection)
				{
					if (item.IsDirty)
					{
						sqliteCommand.Parameters["@Name"].Value = item.Name;
						sqliteCommand2.Parameters["@pId"].Value = Guid.NewGuid().ToString();
						sqliteCommand2.Parameters["@Name"].Value = item.Name;
						if (item.Property.SerializeAs == SettingsSerializeAs.String)
						{
							sqliteCommand2.Parameters["@ValueString"].Value = m_serializationHelper.SerializeToBase64(item.PropertyValue);
							sqliteCommand2.Parameters["@ValueBinary"].Value = DBNull.Value;
						}
						else if (item.Property.SerializeAs == SettingsSerializeAs.Xml)
						{
							item.SerializedValue = m_serializationHelper.SerializeToXml(item.PropertyValue);
							sqliteCommand2.Parameters["@ValueString"].Value = item.SerializedValue;
							sqliteCommand2.Parameters["@ValueBinary"].Value = DBNull.Value;
						}
						else if (item.Property.SerializeAs == SettingsSerializeAs.Binary)
						{
							item.SerializedValue = m_serializationHelper.SerializeToBinary(item.PropertyValue);
							sqliteCommand2.Parameters["@ValueString"].Value = DBNull.Value;
							sqliteCommand2.Parameters["@ValueBinary"].Value = item.SerializedValue;
						}
						sqliteCommand.ExecuteNonQuery();
						sqliteCommand2.ExecuteNonQuery();
					}
				}
				UpdateActivityDates(text, flag, activityOnly: false);
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

	private void CreateProfileForUser(string username, bool isAuthenticated)
	{
		if (ProfileExists(username))
		{
			throw new ProviderException(string.Format(System.Web.Properties.Resources.ErrProfileAlreadyExist, username));
		}
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		sqliteCommand.CommandText = string.Format("INSERT INTO \"{0}\" (\"pId\", \"Username\", \"ApplicationName\", \"IsAnonymous\", \"LastActivityDate\", \"LastUpdatedDate\") Values (@pId, @Username, @ApplicationName, @IsAuthenticated, @LastActivityDate, @LastUpdatedDate)", "Profiles");
		AddParameter(sqliteCommand, "@pId", Guid.NewGuid().ToString());
		AddParameter(sqliteCommand, "@Username", username);
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		AddParameter(sqliteCommand, "@IsAuthenticated", !isAuthenticated);
		AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
		AddParameter(sqliteCommand, "@LastUpdatedDate", DateTime.Now);
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

	private bool ProfileExists(string username)
	{
		using (SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString))
		{
			using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
			sqliteCommand.CommandText = string.Format("SELECT COUNT(*) FROM \"{0}\" WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName", "Profiles");
			AddParameter(sqliteCommand, "@Username", username);
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

	private void UpdateActivityDates(string username, bool isAuthenticated, bool activityOnly)
	{
		using SqliteConnection sqliteConnection = new SqliteConnection(m_ConnectionString);
		using SqliteCommand sqliteCommand = sqliteConnection.CreateCommand();
		if (activityOnly)
		{
			sqliteCommand.CommandText = string.Format("UPDATE \"{0}\" SET \"LastActivityDate\" = @LastActivityDate WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated", "Profiles");
			AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
		}
		else
		{
			sqliteCommand.CommandText = string.Format("UPDATE \"{0}\" SET \"LastActivityDate\" = @LastActivityDate, \"LastUpdatedDate\" = @LastUpdatedDate WHERE \"Username\" = @Username AND \"ApplicationName\" = @ApplicationName AND \"IsAnonymous\" = @IsAuthenticated", "Profiles");
			AddParameter(sqliteCommand, "@LastActivityDate", DateTime.Now);
			AddParameter(sqliteCommand, "@LastUpdatedDate", DateTime.Now);
		}
		AddParameter(sqliteCommand, "@Username", username);
		AddParameter(sqliteCommand, "@ApplicationName", m_ApplicationName);
		AddParameter(sqliteCommand, "@IsAuthenticated", !isAuthenticated);
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

	private string GetConfigValue(string configValue, string defaultValue)
	{
		if (string.IsNullOrEmpty(configValue))
		{
			return defaultValue;
		}
		return configValue;
	}
}
