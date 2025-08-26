using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data;
using System.Data.Common;
using System.IO;
using System.IO.Compression;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.SessionState;

internal sealed class SessionSQLServerHandler : SessionStateStoreProviderBase
{
	private static readonly string defaultDbFactoryTypeName = "Mono.Data.Sqlite.SqliteFactory, Mono.Data.Sqlite, Version=2.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	private SessionStateSection sessionConfig;

	private string connectionString;

	private Type providerFactoryType;

	private DbProviderFactory providerFactory;

	private int sqlCommandTimeout;

	private DbProviderFactory ProviderFactory
	{
		get
		{
			if (providerFactory == null)
			{
				try
				{
					providerFactory = Activator.CreateInstance(providerFactoryType) as DbProviderFactory;
				}
				catch (Exception innerException)
				{
					throw new ProviderException("Failure to create database factory instance.", innerException);
				}
			}
			return providerFactory;
		}
	}

	public string ApplicationName { get; private set; }

	public override void Initialize(string name, NameValueCollection config)
	{
		if (config == null)
		{
			throw new ArgumentNullException("config");
		}
		if (string.IsNullOrEmpty(name))
		{
			name = "SessionSQLServerHandler";
		}
		if (string.IsNullOrEmpty(config["description"]))
		{
			config.Remove("description");
			config.Add("description", "Mono SQL Session Store Provider");
		}
		ApplicationName = HostingEnvironment.ApplicationVirtualPath;
		base.Initialize(name, config);
		sessionConfig = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
		connectionString = sessionConfig.SqlConnectionString;
		string text;
		if (string.IsNullOrEmpty(connectionString) || string.Compare(connectionString, SessionStateSection.DefaultSqlConnectionString, StringComparison.Ordinal) == 0)
		{
			connectionString = "Data Source=|DataDirectory|/ASPState.sqlite;Version=3";
			text = defaultDbFactoryTypeName;
		}
		else
		{
			string[] array = connectionString.Split(';');
			List<string> list = new List<string>();
			text = null;
			bool allowCustomSqlDatabase = sessionConfig.AllowCustomSqlDatabase;
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (text2.Trim().Length == 0)
				{
					continue;
				}
				if (text2.StartsWith("DbProviderName", StringComparison.OrdinalIgnoreCase))
				{
					int num = text2.IndexOf('=');
					if (num < 0)
					{
						throw new ProviderException("Invalid format for the 'DbProviderName' connection string parameter. Expected 'DbProviderName = value'.");
					}
					text = text2.Substring(num + 1);
					continue;
				}
				if (!allowCustomSqlDatabase)
				{
					string text3 = text2.Trim();
					if (text3.StartsWith("database", StringComparison.OrdinalIgnoreCase) || text3.StartsWith("initial catalog", StringComparison.OrdinalIgnoreCase))
					{
						throw new ProviderException("Specifying a custom database is not allowed. Set the allowCustomSqlDatabase attribute of the <system.web/sessionState> section to 'true' in order to use a custom database name.");
					}
				}
				list.Add(text2);
			}
			connectionString = string.Join(";", list.ToArray());
			if (string.IsNullOrEmpty(text))
			{
				text = defaultDbFactoryTypeName;
			}
		}
		Exception innerException = null;
		try
		{
			providerFactoryType = Type.GetType(text, throwOnError: true);
		}
		catch (Exception ex)
		{
			innerException = ex;
			providerFactoryType = null;
		}
		if (providerFactoryType == null)
		{
			throw new ProviderException("Unable to find database provider factory type.", innerException);
		}
		sqlCommandTimeout = (int)sessionConfig.SqlCommandTimeout.TotalSeconds;
	}

	public override void Dispose()
	{
	}

	public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
	{
		return false;
	}

	public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
	{
		DbCommand dbCommand = null;
		string value = Serialize((SessionStateItemCollection)item.Items);
		DbProviderFactory factory = ProviderFactory;
		string applicationName = ApplicationName;
		DbConnection dbConnection = CreateConnection(factory);
		DateTime now = DateTime.Now;
		DbCommand dbCommand2;
		if (newItem)
		{
			dbCommand = CreateCommand(factory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires < @Expires");
			DbParameterCollection parameters = dbCommand.Parameters;
			parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
			parameters.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
			parameters.Add(CreateParameter(factory, "@Expires", now));
			dbCommand2 = CreateCommand(factory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
			DbParameterCollection parameters2 = dbCommand2.Parameters;
			parameters2.Add(CreateParameter(factory, "@SessionId", id, 80));
			parameters2.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
			parameters2.Add(CreateParameter(factory, "@Created", now));
			parameters2.Add(CreateParameter(factory, "@Expires", now.AddMinutes(item.Timeout)));
			parameters2.Add(CreateParameter(factory, "@LockDate", now));
			parameters2.Add(CreateParameter(factory, "@LockId", 0));
			parameters2.Add(CreateParameter(factory, "@Timeout", item.Timeout));
			parameters2.Add(CreateParameter(factory, "@Locked", value: false));
			parameters2.Add(CreateParameter(factory, "@SessionItems", value));
			parameters2.Add(CreateParameter(factory, "@Flags", 0));
		}
		else
		{
			dbCommand2 = CreateCommand(factory, dbConnection, "UPDATE Sessions SET Expires = @Expires, SessionItems = @SessionItems, Locked = @Locked WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
			DbParameterCollection parameters3 = dbCommand2.Parameters;
			parameters3.Add(CreateParameter(factory, "@Expires", now.AddMinutes(item.Timeout)));
			parameters3.Add(CreateParameter(factory, "@SessionItems", value));
			parameters3.Add(CreateParameter(factory, "@Locked", value: false));
			parameters3.Add(CreateParameter(factory, "@SessionId", id, 80));
			parameters3.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
			parameters3.Add(CreateParameter(factory, "@Lockid", (int)lockId));
		}
		try
		{
			dbConnection.Open();
			dbCommand?.ExecuteNonQuery();
			dbCommand2.ExecuteNonQuery();
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Failure storing session item in database.", innerException);
		}
		finally
		{
			dbConnection.Close();
		}
	}

	public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
	{
		return GetSessionStoreItem(lockRecord: false, context, id, out locked, out lockAge, out lockId, out actionFlags);
	}

	public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
	{
		return GetSessionStoreItem(lockRecord: true, context, id, out locked, out lockAge, out lockId, out actionFlags);
	}

	private SessionStateStoreData GetSessionStoreItem(bool lockRecord, HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
	{
		SessionStateStoreData result = null;
		lockAge = TimeSpan.Zero;
		lockId = null;
		locked = false;
		actionFlags = SessionStateActions.None;
		DbProviderFactory factory = ProviderFactory;
		DbConnection dbConnection = CreateConnection(factory);
		string applicationName = ApplicationName;
		DbDataReader dbDataReader = null;
		string serializedItems = string.Empty;
		bool flag = false;
		bool flag2 = false;
		int timeout = 0;
		DateTime now = DateTime.Now;
		try
		{
			dbConnection.Open();
			if (lockRecord)
			{
				DbCommand dbCommand = CreateCommand(factory, dbConnection, "UPDATE Sessions SET Locked = @Locked, LockDate = @LockDate WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND Expires > @Expires");
				DbParameterCollection parameters = dbCommand.Parameters;
				parameters.Add(CreateParameter(factory, "@Locked", value: true));
				parameters.Add(CreateParameter(factory, "@LockDate", now));
				parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
				parameters.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
				parameters.Add(CreateParameter(factory, "@Expires", now));
				if (dbCommand.ExecuteNonQuery() == 0)
				{
					locked = true;
				}
				else
				{
					locked = false;
				}
			}
			DbCommand dbCommand2 = CreateCommand(factory, dbConnection, "SELECT Expires, SessionItems, LockId, LockDate, Flags, Timeout FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
			DbParameterCollection parameters2 = dbCommand2.Parameters;
			parameters2.Add(CreateParameter(factory, "@SessionId", id, 80));
			parameters2.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
			dbDataReader = dbCommand2.ExecuteReader(CommandBehavior.SingleRow);
			while (dbDataReader.Read())
			{
				if (dbDataReader.GetDateTime(dbDataReader.GetOrdinal("Expires")) < now)
				{
					locked = false;
					flag2 = true;
				}
				else
				{
					flag = true;
				}
				serializedItems = dbDataReader.GetString(dbDataReader.GetOrdinal("SessionItems"));
				lockId = dbDataReader.GetInt32(dbDataReader.GetOrdinal("LockId"));
				lockAge = now.Subtract(dbDataReader.GetDateTime(dbDataReader.GetOrdinal("LockDate")));
				actionFlags = (SessionStateActions)dbDataReader.GetInt32(dbDataReader.GetOrdinal("Flags"));
				timeout = dbDataReader.GetInt32(dbDataReader.GetOrdinal("Timeout"));
			}
			dbDataReader.Close();
			if (flag2)
			{
				DbCommand dbCommand3 = CreateCommand(factory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
				DbParameterCollection parameters3 = dbCommand3.Parameters;
				parameters3.Add(CreateParameter(factory, "@SessionId", id, 80));
				parameters3.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
				dbCommand3.ExecuteNonQuery();
			}
			if (!flag)
			{
				locked = false;
			}
			if (flag && !locked)
			{
				lockId = (int)lockId + 1;
				DbCommand dbCommand4 = CreateCommand(factory, dbConnection, "UPDATE Sessions SET LockId = @LockId, Flags = 0 WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
				DbParameterCollection parameters4 = dbCommand4.Parameters;
				parameters4.Add(CreateParameter(factory, "@LockId", (int)lockId));
				parameters4.Add(CreateParameter(factory, "@SessionId", id, 80));
				parameters4.Add(CreateParameter(factory, "@ApplicationName", applicationName, 255));
				dbCommand4.ExecuteNonQuery();
				result = ((actionFlags != SessionStateActions.InitializeItem) ? Deserialize(context, serializedItems, timeout) : CreateNewStoreData(context, (int)sessionConfig.Timeout.TotalMinutes));
			}
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Unable to retrieve session item from database.", innerException);
		}
		finally
		{
			dbDataReader?.Close();
			dbConnection.Close();
		}
		return result;
	}

	private string Serialize(SessionStateItemCollection items)
	{
		GZipStream gZipStream = null;
		MemoryStream memoryStream = null;
		BinaryWriter binaryWriter = null;
		try
		{
			memoryStream = new MemoryStream();
			Stream output = ((!sessionConfig.CompressionEnabled) ? ((Stream)memoryStream) : ((Stream)(gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))));
			binaryWriter = new BinaryWriter(output);
			items?.Serialize(binaryWriter);
			gZipStream?.Close();
			binaryWriter.Close();
			return Convert.ToBase64String(memoryStream.ToArray());
		}
		finally
		{
			binaryWriter?.Dispose();
			gZipStream?.Dispose();
			memoryStream?.Dispose();
		}
	}

	private SessionStateStoreData Deserialize(HttpContext context, string serializedItems, int timeout)
	{
		MemoryStream memoryStream = null;
		BinaryReader binaryReader = null;
		GZipStream gZipStream = null;
		try
		{
			memoryStream = new MemoryStream(Convert.FromBase64String(serializedItems));
			SessionStateItemCollection sessionItems = new SessionStateItemCollection();
			if (memoryStream.Length > 0)
			{
				Stream input = ((!sessionConfig.CompressionEnabled) ? ((Stream)memoryStream) : ((Stream)(gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))));
				binaryReader = new BinaryReader(input);
				sessionItems = SessionStateItemCollection.Deserialize(binaryReader);
				gZipStream?.Close();
				binaryReader.Close();
			}
			return new SessionStateStoreData(sessionItems, SessionStateUtility.GetSessionStaticObjects(context), timeout);
		}
		finally
		{
			binaryReader?.Dispose();
			gZipStream?.Dispose();
			memoryStream?.Dispose();
		}
	}

	public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
	{
		DbProviderFactory factory = ProviderFactory;
		DbConnection dbConnection = CreateConnection(factory);
		DbCommand dbCommand = CreateCommand(factory, dbConnection, "UPDATE Sessions SET Locked = 0, Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
		DbParameterCollection parameters = dbCommand.Parameters;
		parameters.Add(CreateParameter(factory, "@Expires", DateTime.Now.AddMinutes(sessionConfig.Timeout.TotalMinutes)));
		parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
		parameters.Add(CreateParameter(factory, "@ApplicationName", ApplicationName, 255));
		parameters.Add(CreateParameter(factory, "@LockId", (int)lockId));
		try
		{
			dbConnection.Open();
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Error releasing item in database.", innerException);
		}
		finally
		{
			dbConnection.Close();
		}
	}

	public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
	{
		DbProviderFactory factory = ProviderFactory;
		DbConnection dbConnection = CreateConnection(factory);
		DbCommand dbCommand = CreateCommand(factory, dbConnection, "DELETE FROM Sessions WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName AND LockId = @LockId");
		DbParameterCollection parameters = dbCommand.Parameters;
		parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
		parameters.Add(CreateParameter(factory, "@ApplicationName", ApplicationName, 255));
		parameters.Add(CreateParameter(factory, "@LockId", (int)lockId));
		try
		{
			dbConnection.Open();
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Error removing item from database.", innerException);
		}
		finally
		{
			dbConnection.Close();
		}
	}

	public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
	{
		DbProviderFactory factory = ProviderFactory;
		DbConnection dbConnection = CreateConnection(factory);
		DbCommand dbCommand = CreateCommand(factory, dbConnection, "INSERT INTO Sessions (SessionId, ApplicationName, Created, Expires, LockDate, LockId, Timeout, Locked, SessionItems, Flags) Values (@SessionId, @ApplicationName, @Created, @Expires, @LockDate, @LockId , @Timeout, @Locked, @SessionItems, @Flags)");
		DateTime now = DateTime.Now;
		DbParameterCollection parameters = dbCommand.Parameters;
		parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
		parameters.Add(CreateParameter(factory, "@ApplicationName", ApplicationName, 255));
		parameters.Add(CreateParameter(factory, "@Created", now));
		parameters.Add(CreateParameter(factory, "@Expires", now.AddMinutes(timeout)));
		parameters.Add(CreateParameter(factory, "@LockDate", now));
		parameters.Add(CreateParameter(factory, "@LockId", 0));
		parameters.Add(CreateParameter(factory, "@Timeout", timeout));
		parameters.Add(CreateParameter(factory, "@Locked", value: false));
		parameters.Add(CreateParameter(factory, "@SessionItems", string.Empty));
		parameters.Add(CreateParameter(factory, "@Flags", 1));
		try
		{
			dbConnection.Open();
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Error creating uninitialized session item in the database.", innerException);
		}
		finally
		{
			dbConnection.Close();
		}
	}

	public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
	{
		return new SessionStateStoreData(new SessionStateItemCollection(), SessionStateUtility.GetSessionStaticObjects(context), timeout);
	}

	public override void ResetItemTimeout(HttpContext context, string id)
	{
		DbProviderFactory factory = ProviderFactory;
		DbConnection dbConnection = CreateConnection(factory);
		DbCommand dbCommand = CreateCommand(factory, dbConnection, "UPDATE Sessions SET Expires = @Expires WHERE SessionId = @SessionId AND ApplicationName = @ApplicationName");
		DbParameterCollection parameters = dbCommand.Parameters;
		parameters.Add(CreateParameter(factory, "@Expires", DateTime.Now.AddMinutes(sessionConfig.Timeout.TotalMinutes)));
		parameters.Add(CreateParameter(factory, "@SessionId", id, 80));
		parameters.Add(CreateParameter(factory, "@ApplicationName", ApplicationName, 255));
		try
		{
			dbConnection.Open();
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception innerException)
		{
			throw new ProviderException("Error resetting session item timeout in the database.", innerException);
		}
		finally
		{
			dbConnection.Close();
		}
	}

	public override void InitializeRequest(HttpContext context)
	{
	}

	public override void EndRequest(HttpContext context)
	{
	}

	private DbConnection CreateConnection(DbProviderFactory factory)
	{
		DbConnection dbConnection = factory.CreateConnection();
		dbConnection.ConnectionString = connectionString;
		return dbConnection;
	}

	private DbCommand CreateCommand(DbProviderFactory factory, DbConnection conn, string commandText)
	{
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandTimeout = sqlCommandTimeout;
		dbCommand.Connection = conn;
		dbCommand.CommandText = commandText;
		return dbCommand;
	}

	private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value)
	{
		return CreateParameter(factory, name, value, -1);
	}

	private DbParameter CreateParameter<ValueType>(DbProviderFactory factory, string name, ValueType value, int size)
	{
		DbParameter dbParameter = factory.CreateParameter();
		dbParameter.ParameterName = name;
		Type typeFromHandle = typeof(ValueType);
		if (typeFromHandle == typeof(string))
		{
			dbParameter.DbType = DbType.String;
		}
		else if (typeFromHandle == typeof(int))
		{
			dbParameter.DbType = DbType.Int32;
		}
		else if (typeFromHandle == typeof(bool))
		{
			dbParameter.DbType = DbType.Boolean;
		}
		else if (typeFromHandle == typeof(DateTime))
		{
			dbParameter.DbType = DbType.DateTime;
		}
		if (size > -1)
		{
			dbParameter.Size = size;
		}
		dbParameter.Value = value;
		return dbParameter;
	}
}
