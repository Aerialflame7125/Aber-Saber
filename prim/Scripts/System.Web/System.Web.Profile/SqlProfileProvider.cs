using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Util;

namespace System.Web.Profile;

/// <summary>Manages storage of profile information for an ASP.NET application in a SQL Server database.</summary>
public class SqlProfileProvider : ProfileProvider
{
	private ConnectionStringSettings connectionString;

	private DbProviderFactory factory;

	private string applicationName;

	private bool schemaIsOk;

	/// <summary>Gets or sets the name of the application for which to store and retrieve profile information.</summary>
	/// <returns>The name of the application for which to store and retrieve profile information. The default is the <see cref="P:System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath" /> value.</returns>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.Profile.SqlProfileProvider.ApplicationName" /> property by a caller that does not have <see cref="F:System.Web.AspNetHostingPermissionLevel.High" /> ASP.NET hosting permission.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">An attempt was made to set the <see cref="P:System.Web.Profile.SqlProfileProvider.ApplicationName" /> property to a string that is longer than 256 characters.</exception>
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

	/// <summary>Deletes user profile data for profiles in which the last activity date occurred before the specified date and time.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_DeleteInactiveProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
		DbParameter returnValue = AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
		dbCommand.ExecuteNonQuery();
		return GetReturnValue(returnValue);
	}

	/// <summary>Deletes profile properties and information for the supplied list of profiles from the data source.</summary>
	/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  that contains profile information for profiles to be deleted.</param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfoCollection.Count" /> value of zero.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is an empty string (""), exceeds a length of 256 characters, or contains a comma.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="profiles" /> is <see langword="null" />.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is <see langword="null" />.</exception>
	public override int DeleteProfiles(ProfileInfoCollection profiles)
	{
		if (profiles == null)
		{
			throw new ArgumentNullException("prfoles");
		}
		if (profiles.Count == 0)
		{
			throw new ArgumentException("prfoles");
		}
		string[] array = new string[profiles.Count];
		int num = 0;
		foreach (ProfileInfo profile in profiles)
		{
			if (profile.UserName == null)
			{
				throw new ArgumentNullException("element in profiles collection is null");
			}
			if (profile.UserName.Length == 0 || profile.UserName.Length > 256 || profile.UserName.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in profiles collection in illegal format");
			}
			array[num++] = profile.UserName;
		}
		return DeleteProfilesInternal(array);
	}

	/// <summary>Deletes profile properties and information from the data source for the supplied list of user names.</summary>
	/// <param name="usernames">A string array of user names for profiles to be deleted. </param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	/// <exception cref="T:System.ArgumentException">The length of <paramref name="usernames" /> is zero.- or -One of the items in <paramref name="usernames" /> is an empty string (""), exceeds a length of 256 characters, or contains a comma.- or -Two or more items in <paramref name="usernames" /> have the same value.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernames" /> is <see langword="null" />.- or -One of the items in <paramref name="usernames" /> is <see langword="null" />.</exception>
	public override int DeleteProfiles(string[] usernames)
	{
		if (usernames == null)
		{
			throw new ArgumentNullException("usernames");
		}
		Hashtable hashtable = new Hashtable();
		foreach (string text in usernames)
		{
			if (text == null)
			{
				throw new ArgumentNullException("element in usernames array is null");
			}
			if (text.Length == 0 || text.Length > 256 || text.IndexOf(',') != -1)
			{
				throw new ArgumentException("element in usernames array in illegal format");
			}
			if (hashtable.ContainsKey(text))
			{
				throw new ArgumentException("duplicate element in usernames array");
			}
			hashtable.Add(text, text);
		}
		return DeleteProfilesInternal(usernames);
	}

	private int DeleteProfilesInternal(string[] usernames)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_DeleteProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "UserNames", string.Join(",", usernames));
		DbParameter returnValue = AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
		dbCommand.ExecuteNonQuery();
		return GetReturnValue(returnValue);
	}

	/// <summary>Retrieves profile information for profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("") or exceeds 256 characters.- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		CheckParam("usernameToMatch", usernameToMatch, 256);
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex is less than zero");
		}
		if (pageSize < 1)
		{
			throw new ArgumentException("pageIndex is less than one");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_GetProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "PageIndex", pageIndex);
		AddParameter(dbCommand, "PageSize", pageSize);
		AddParameter(dbCommand, "UserNameToMatch", usernameToMatch);
		AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
		using DbDataReader reader = dbCommand.ExecuteReader();
		return BuildProfileInfoCollection(reader, out totalRecords);
	}

	/// <summary>Retrieves profile information for profiles in which the user name matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("") or exceeds 256 characters.- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		CheckParam("usernameToMatch", usernameToMatch, 256);
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex is less than zero");
		}
		if (pageSize < 1)
		{
			throw new ArgumentException("pageIndex is less than one");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_GetProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "PageIndex", pageIndex);
		AddParameter(dbCommand, "PageSize", pageSize);
		AddParameter(dbCommand, "UserNameToMatch", usernameToMatch);
		AddParameter(dbCommand, "InactiveSinceDate", null);
		using DbDataReader reader = dbCommand.ExecuteReader();
		return BuildProfileInfoCollection(reader, out totalRecords);
	}

	/// <summary>Retrieves user profile data for profiles in which the last activity date occurred on or before the specified date and time.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex is less than zero");
		}
		if (pageSize < 1)
		{
			throw new ArgumentException("pageIndex is less than one");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_GetProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "PageIndex", pageIndex);
		AddParameter(dbCommand, "PageSize", pageSize);
		AddParameter(dbCommand, "UserNameToMatch", null);
		AddParameter(dbCommand, "InactiveSinceDate", null);
		using DbDataReader reader = dbCommand.ExecuteReader();
		return BuildProfileInfoCollection(reader, out totalRecords);
	}

	/// <summary>Retrieves user profile data for profiles in the data source.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than one.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
	{
		if (pageIndex < 0)
		{
			throw new ArgumentException("pageIndex is less than zero");
		}
		if (pageSize < 1)
		{
			throw new ArgumentException("pageIndex is less than one");
		}
		if (pageIndex * pageSize + pageSize - 1 > int.MaxValue)
		{
			throw new ArgumentException("pageIndex and pageSize are too large");
		}
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_GetProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "PageIndex", pageIndex);
		AddParameter(dbCommand, "PageSize", pageSize);
		AddParameter(dbCommand, "UserNameToMatch", null);
		AddParameter(dbCommand, "InactiveSinceDate", null);
		using DbDataReader reader = dbCommand.ExecuteReader();
		return BuildProfileInfoCollection(reader, out totalRecords);
	}

	/// <summary>Gets the number of profiles in the data source where the last activity date occurred on or before the specified <paramref name="userInactiveSinceDate" />.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>The number of profiles in the data source for which the last activity date occurred before the specified date and time.</returns>
	public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_GetNumberOfInactiveProfiles";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "ProfileAuthOptions", authenticationOption);
		AddParameter(dbCommand, "InactiveSinceDate", userInactiveSinceDate);
		int result = 0;
		using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
		{
			if (dbDataReader.Read())
			{
				result = dbDataReader.GetInt32(0);
			}
		}
		return result;
	}

	/// <summary>Retrieves profile property information and values from a SQL Server profile database.</summary>
	/// <param name="sc">The <see cref="T:System.Configuration.SettingsContext" /> that contains user profile information.</param>
	/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyCollection" /> containing profile information for the properties to be retrieved.</param>
	/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing profile property information and values.</returns>
	public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext sc, SettingsPropertyCollection properties)
	{
		SettingsPropertyValueCollection settingsPropertyValueCollection = new SettingsPropertyValueCollection();
		if (properties.Count == 0)
		{
			return settingsPropertyValueCollection;
		}
		foreach (SettingsProperty property in properties)
		{
			if (property.SerializeAs == SettingsSerializeAs.ProviderSpecific)
			{
				if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
				{
					property.SerializeAs = SettingsSerializeAs.String;
				}
				else
				{
					property.SerializeAs = SettingsSerializeAs.Xml;
				}
			}
			settingsPropertyValueCollection.Add(new SettingsPropertyValue(property));
		}
		string parameterValue = (string)sc["UserName"];
		using (DbConnection connection = CreateConnection())
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.Connection = connection;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = "aspnet_Profile_GetProperties";
			AddParameter(dbCommand, "ApplicationName", ApplicationName);
			AddParameter(dbCommand, "UserName", parameterValue);
			AddParameter(dbCommand, "CurrentTimeUtc", DateTime.UtcNow);
			using DbDataReader dbDataReader = dbCommand.ExecuteReader();
			if (dbDataReader.Read())
			{
				string @string = dbDataReader.GetString(0);
				string string2 = dbDataReader.GetString(1);
				int num = (int)dbDataReader.GetBytes(2, 0L, null, 0, 0);
				byte[] array = new byte[num];
				dbDataReader.GetBytes(2, 0L, array, 0, num);
				DecodeProfileData(@string, string2, array, settingsPropertyValueCollection);
			}
		}
		return settingsPropertyValueCollection;
	}

	/// <summary>Updates the SQL Server profile database with the specified property values.</summary>
	/// <param name="sc">The <see cref="T:System.Configuration.SettingsContext" /> that contains user profile information.</param>
	/// <param name="properties">A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> containing profile information and values for the properties to be updated.</param>
	public override void SetPropertyValues(SettingsContext sc, SettingsPropertyValueCollection properties)
	{
		string parameterValue = (string)sc["UserName"];
		bool flag = !(bool)sc["IsAuthenticated"];
		string allNames = string.Empty;
		string allValues = string.Empty;
		byte[] buf = null;
		EncodeProfileData(ref allNames, ref allValues, ref buf, properties, !flag);
		using DbConnection connection = CreateConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandType = CommandType.StoredProcedure;
		dbCommand.CommandText = "aspnet_Profile_SetProperties";
		AddParameter(dbCommand, "ApplicationName", ApplicationName);
		AddParameter(dbCommand, "PropertyNames", allNames);
		AddParameter(dbCommand, "PropertyValuesString", allValues);
		AddParameter(dbCommand, "PropertyValuesBinary", buf);
		AddParameter(dbCommand, "UserName", parameterValue);
		AddParameter(dbCommand, "IsUserAnonymous", flag);
		AddParameter(dbCommand, "CurrentTimeUtc", DateTime.UtcNow);
		AddParameter(dbCommand, null, ParameterDirection.ReturnValue, null);
		dbCommand.ExecuteNonQuery();
	}

	/// <summary>Initializes the SQL Server profile provider with the property values specified in the ASP.NET application's configuration file. This method is not intended to be used directly from your code.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance to initialize. </param>
	/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the names and values of configuration options for the profile provider. </param>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="connectionStringName" /> attribute is an empty string ("") or is not specified in the application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance.- or - The value of the connection string specified in the <see langword="connectionStringName" /> attribute value is empty or the specified <see langword="connectionStringName" /> value does not exist in the application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance.- or - The <see langword="applicationName" /> attribute value exceeds 256 characters.- or - The application configuration file for this <see cref="T:System.Web.Profile.SqlProfileProvider" /> instance contains an unrecognized attribute. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="config" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The current trust level is less than <see cref="F:System.Web.AspNetHostingPermissionLevel.Low" />.</exception>
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
		factory = ((connectionString == null || string.IsNullOrEmpty(connectionString.ProviderName)) ? SqlClientFactory.Instance : ProvidersHelper.GetDbProviderFactory(connectionString.ProviderName));
	}

	private DbConnection CreateConnection()
	{
		if (!schemaIsOk && !(schemaIsOk = AspNetDBSchemaChecker.CheckMembershipSchemaVersion(factory, connectionString.ConnectionString, "profile", "1")))
		{
			throw new ProviderException("Incorrect ASP.NET DB Schema Version.");
		}
		DbConnection dbConnection = factory.CreateConnection();
		dbConnection.ConnectionString = connectionString.ConnectionString;
		dbConnection.Open();
		return dbConnection;
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

	private void CheckParam(string pName, string p, int length)
	{
		if (p == null)
		{
			throw new ArgumentNullException(pName);
		}
		if (p.Length == 0 || p.Length > length || p.IndexOf(',') != -1)
		{
			throw new ArgumentException("invalid format for " + pName);
		}
	}

	private static int GetReturnValue(DbParameter returnValue)
	{
		object value = returnValue.Value;
		if (!(value is int))
		{
			return -1;
		}
		return (int)value;
	}

	private ProfileInfo ReadProfileInfo(DbDataReader reader)
	{
		ProfileInfo result = null;
		try
		{
			string @string = reader.GetString(0);
			bool boolean = reader.GetBoolean(1);
			DateTime dateTime = reader.GetDateTime(2);
			DateTime dateTime2 = reader.GetDateTime(3);
			int @int = reader.GetInt32(4);
			result = new ProfileInfo(@string, boolean, dateTime2, dateTime, @int);
		}
		catch
		{
		}
		return result;
	}

	private ProfileInfoCollection BuildProfileInfoCollection(DbDataReader reader, out int totalRecords)
	{
		ProfileInfoCollection profileInfoCollection = new ProfileInfoCollection();
		while (reader.Read())
		{
			ProfileInfo profileInfo = ReadProfileInfo(reader);
			if (profileInfo != null)
			{
				profileInfoCollection.Add(profileInfo);
			}
		}
		totalRecords = 0;
		if (reader.NextResult() && reader.Read())
		{
			totalRecords = reader.GetInt32(0);
		}
		return profileInfoCollection;
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

	private void DecodeProfileData(string allnames, string values, byte[] buf, SettingsPropertyValueCollection properties)
	{
		if (allnames == null || values == null || buf == null || properties == null)
		{
			return;
		}
		string[] array = allnames.Split(':');
		for (int i = 0; i < array.Length; i += 4)
		{
			string name = array[i];
			SettingsPropertyValue settingsPropertyValue = properties[name];
			if (settingsPropertyValue != null)
			{
				int num = int.Parse(array[i + 2], Helpers.InvariantCulture);
				int num2 = int.Parse(array[i + 3], Helpers.InvariantCulture);
				if (num2 == -1 && !settingsPropertyValue.Property.PropertyType.IsValueType)
				{
					settingsPropertyValue.PropertyValue = null;
					settingsPropertyValue.IsDirty = false;
					settingsPropertyValue.Deserialized = true;
				}
				else if (array[i + 1] == "S" && num >= 0 && num2 > 0 && values.Length >= num + num2)
				{
					settingsPropertyValue.SerializedValue = values.Substring(num, num2);
				}
				else if (array[i + 1] == "B" && num >= 0 && num2 > 0 && buf.Length >= num + num2)
				{
					byte[] array2 = new byte[num2];
					Buffer.BlockCopy(buf, num, array2, 0, num2);
					settingsPropertyValue.SerializedValue = array2;
				}
			}
		}
	}

	private void EncodeProfileData(ref string allNames, ref string allValues, ref byte[] buf, SettingsPropertyValueCollection properties, bool userIsAuthenticated)
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		MemoryStream memoryStream = new MemoryStream();
		try
		{
			foreach (SettingsPropertyValue property in properties)
			{
				if ((!userIsAuthenticated && !(bool)property.Property.Attributes["AllowAnonymous"]) || (!property.IsDirty && property.UsingDefaultValue))
				{
					continue;
				}
				int num = 0;
				int num2 = 0;
				string text = null;
				if (property.Deserialized && property.PropertyValue == null)
				{
					num = -1;
				}
				else
				{
					object serializedValue = property.SerializedValue;
					if (serializedValue == null)
					{
						num = -1;
					}
					else if (serializedValue is string)
					{
						text = (string)serializedValue;
						num = text.Length;
						num2 = stringBuilder2.Length;
					}
					else
					{
						byte[] array = (byte[])serializedValue;
						num2 = (int)memoryStream.Position;
						memoryStream.Write(array, 0, array.Length);
						memoryStream.Position = num2 + array.Length;
						num = array.Length;
					}
				}
				stringBuilder.Append(property.Name + ":" + ((text != null) ? "S" : "B") + ":" + num2.ToString(Helpers.InvariantCulture) + ":" + num.ToString(Helpers.InvariantCulture) + ":");
				if (text != null)
				{
					stringBuilder2.Append(text);
				}
			}
			buf = memoryStream.ToArray();
		}
		finally
		{
			memoryStream?.Close();
		}
		allNames = stringBuilder.ToString();
		allValues = stringBuilder2.ToString();
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.SqlProfileProvider" /> class.</summary>
	public SqlProfileProvider()
	{
	}
}
