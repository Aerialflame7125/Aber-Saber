using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Reflection;

namespace System.Data.Common;

/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
public static class DbProviderFactories
{
	private const string AssemblyQualifiedName = "AssemblyQualifiedName";

	private const string Instance = "Instance";

	private const string InvariantName = "InvariantName";

	private const string Name = "Name";

	private const string Description = "Description";

	private static ConnectionState _initState;

	private static DataTable _providerTable;

	private static object _lockobj = new object();

	/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
	/// <param name="providerInvariantName">Invariant name of a provider.</param>
	/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
	public static DbProviderFactory GetFactory(string providerInvariantName)
	{
		ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
		DataTable providerTable = GetProviderTable();
		if (providerTable != null)
		{
			DataRow dataRow = providerTable.Rows.Find(providerInvariantName);
			if (dataRow != null)
			{
				return GetFactory(dataRow);
			}
		}
		throw ADP.ConfigProviderNotFound();
	}

	/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
	/// <param name="providerRow">
	///   <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
	/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
	public static DbProviderFactory GetFactory(DataRow providerRow)
	{
		ADP.CheckArgumentNull(providerRow, "providerRow");
		DataColumn dataColumn = providerRow.Table.Columns["AssemblyQualifiedName"];
		if (dataColumn != null)
		{
			string text = providerRow[dataColumn] as string;
			if (!ADP.IsEmpty(text))
			{
				Type type = Type.GetType(text);
				if (null != type)
				{
					FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
					if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
					{
						object value = field.GetValue(null);
						if (value != null)
						{
							return (DbProviderFactory)value;
						}
					}
					throw ADP.ConfigProviderInvalid();
				}
				throw ADP.ConfigProviderNotInstalled();
			}
		}
		throw ADP.ConfigProviderMissing();
	}

	/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
	/// <param name="connection">The connection used.</param>
	/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified connection.</returns>
	public static DbProviderFactory GetFactory(DbConnection connection)
	{
		ADP.CheckArgumentNull(connection, "connection");
		return connection.ProviderFactory;
	}

	/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains information about all installed providers that implement <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
	/// <returns>A <see cref="T:System.Data.DataTable" /> containing <see cref="T:System.Data.DataRow" /> objects that contain the following data:  
	///   Column ordinal  
	///
	///   Column name  
	///
	///   Description  
	///
	///   0  
	///
	///   **Name**  
	///
	///   Human-readable name for the data provider.  
	///
	///   1  
	///
	///   **Description**  
	///
	///   Human-readable description of the data provider.  
	///
	///   2  
	///
	///   **InvariantName**  
	///
	///   Name that can be used programmatically to refer to the data provider.  
	///
	///   3  
	///
	///   **AssemblyQualifiedName**  
	///
	///   Fully qualified name of the factory class, which contains enough information to instantiate the object.</returns>
	public static DataTable GetFactoryClasses()
	{
		DataTable providerTable = GetProviderTable();
		if (providerTable != null)
		{
			return providerTable.Copy();
		}
		return DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
	}

	private static DataTable IncludeFrameworkFactoryClasses(DataTable configDataTable)
	{
		DataTable dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
		string factoryAssemblyQualifiedName = typeof(SqlClientFactory).AssemblyQualifiedName.ToString().Replace("System.Data.SqlClient.SqlClientFactory, System.Data,", "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,");
		DbProviderFactoryConfigSection[] array = new DbProviderFactoryConfigSection[4]
		{
			new DbProviderFactoryConfigSection(typeof(OdbcFactory), "Odbc Data Provider", ".Net Framework Data Provider for Odbc"),
			new DbProviderFactoryConfigSection(typeof(OleDbFactory), "OleDb Data Provider", ".Net Framework Data Provider for OleDb"),
			new DbProviderFactoryConfigSection("OracleClient Data Provider", "System.Data.OracleClient", ".Net Framework Data Provider for Oracle", factoryAssemblyQualifiedName),
			new DbProviderFactoryConfigSection(typeof(SqlClientFactory), "SqlClient Data Provider", ".Net Framework Data Provider for SqlServer")
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].IsNull())
			{
				continue;
			}
			bool flag = false;
			if (i == 2)
			{
				Type type = Type.GetType(array[i].AssemblyQualifiedName);
				if (type != null)
				{
					FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
					if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
					{
						object value = field.GetValue(null);
						if (value != null)
						{
							flag = true;
						}
					}
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Name"] = array[i].Name;
				dataRow["InvariantName"] = array[i].InvariantName;
				dataRow["Description"] = array[i].Description;
				dataRow["AssemblyQualifiedName"] = array[i].AssemblyQualifiedName;
				dataTable.Rows.Add(dataRow);
			}
		}
		int num = 0;
		while (configDataTable != null && num < configDataTable.Rows.Count)
		{
			try
			{
				bool flag2 = false;
				if (configDataTable.Rows[num]["AssemblyQualifiedName"].ToString().ToLowerInvariant().Contains("System.Data.OracleClient".ToString().ToLowerInvariant()))
				{
					Type type2 = Type.GetType(configDataTable.Rows[num]["AssemblyQualifiedName"].ToString());
					if (type2 != null)
					{
						FieldInfo field2 = type2.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if (null != field2 && field2.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object value2 = field2.GetValue(null);
							if (value2 != null)
							{
								flag2 = true;
							}
						}
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					dataTable.Rows.Add(configDataTable.Rows[num].ItemArray);
				}
			}
			catch (ConstraintException)
			{
			}
			num++;
		}
		return dataTable;
	}

	private static DataTable GetProviderTable()
	{
		Initialize();
		return _providerTable;
	}

	private static void Initialize()
	{
		if (ConnectionState.Open == _initState)
		{
			return;
		}
		lock (_lockobj)
		{
			switch (_initState)
			{
			case ConnectionState.Closed:
				_initState = ConnectionState.Connecting;
				try
				{
					_providerTable = ((System.Configuration.PrivilegedConfigurationManager.GetSection("system.data") is DataSet dataSet) ? IncludeFrameworkFactoryClasses(dataSet.Tables["DbProviderFactories"]) : IncludeFrameworkFactoryClasses(null));
					break;
				}
				finally
				{
					_initState = ConnectionState.Open;
				}
			}
		}
	}
}
