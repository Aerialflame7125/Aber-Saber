using System.Configuration.Provider;
using System.Data;
using System.Data.Common;

namespace System.Web.Security;

internal static class AspNetDBSchemaChecker
{
	private static DbConnection CreateConnection(DbProviderFactory factory, string connStr)
	{
		DbConnection dbConnection = factory.CreateConnection();
		dbConnection.ConnectionString = connStr;
		dbConnection.Open();
		return dbConnection;
	}

	public static bool CheckMembershipSchemaVersion(DbProviderFactory factory, string connStr, string feature, string compatibleVersion)
	{
		using DbConnection connection = CreateConnection(factory, connStr);
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.Connection = connection;
		dbCommand.CommandText = "aspnet_CheckSchemaVersion";
		dbCommand.CommandType = CommandType.StoredProcedure;
		AddParameter(factory, dbCommand, "@Feature", ParameterDirection.Input, feature);
		AddParameter(factory, dbCommand, "@CompatibleSchemaVersion", ParameterDirection.Input, compatibleVersion);
		DbParameter dbParameter = AddParameter(factory, dbCommand, "@ReturnVal", ParameterDirection.ReturnValue, null);
		try
		{
			dbCommand.ExecuteNonQuery();
		}
		catch (Exception)
		{
			throw new ProviderException("ASP.NET Membership schema not installed.");
		}
		if ((int)(dbParameter.Value ?? ((object)(-1))) == 0)
		{
			return true;
		}
		return false;
	}

	private static DbParameter AddParameter(DbProviderFactory factory, DbCommand command, string parameterName, ParameterDirection direction, object parameterValue)
	{
		DbParameter dbParameter = command.CreateParameter();
		dbParameter.ParameterName = parameterName;
		dbParameter.Value = parameterValue;
		dbParameter.Direction = direction;
		command.Parameters.Add(dbParameter);
		return dbParameter;
	}
}
