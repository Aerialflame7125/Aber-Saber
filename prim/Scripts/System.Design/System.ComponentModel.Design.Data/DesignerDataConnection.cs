namespace System.ComponentModel.Design.Data;

/// <summary>Represents a connection to a data store in a design tool. This class cannot be inherited.</summary>
public sealed class DesignerDataConnection
{
	private string name;

	private string provider_name;

	private string connection_string;

	private bool is_configured;

	/// <summary>Gets the name of the data connection.</summary>
	/// <returns>The name of the data connection.</returns>
	[System.MonoTODO]
	public string Name => name;

	/// <summary>Gets the name of the provider used to access the underlying data store.</summary>
	/// <returns>The name of the provider used to access the underlying data store.</returns>
	[System.MonoTODO]
	public string ProviderName => provider_name;

	/// <summary>Gets the application connection string defined for the connection.</summary>
	/// <returns>The application connection string defined for the connection.</returns>
	[System.MonoTODO]
	public string ConnectionString => connection_string;

	/// <summary>Gets a value indicating whether the connection information is in the application's configuration file.</summary>
	/// <returns>
	///   <see langword="true" /> if the connection is defined in the application's configuration file; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool IsConfigured => is_configured;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> class with the specified name, data provider, and connection string.</summary>
	/// <param name="name">The name associated with this connection.</param>
	/// <param name="providerName">The name of the provider object used to access the underlying data store</param>
	/// <param name="connectionString">The string that specifies how to connect to the data store.</param>
	[System.MonoTODO]
	public DesignerDataConnection(string name, string providerName, string connectionString)
		: this(name, providerName, connectionString, isConfigured: false)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" /> class with the specified name, data provider, and connection string, and indicates whether the connection was loaded from a configuration file.</summary>
	/// <param name="name">The name associated with this connection.</param>
	/// <param name="providerName">The name of the provider object used to access the underlying data store.</param>
	/// <param name="connectionString">The string that specifies how to connect to the data store.</param>
	/// <param name="isConfigured">
	///   <see langword="true" /> to indicate the connection was created from information stored in the application's configuration file; otherwise, <see langword="false" />.</param>
	[System.MonoTODO]
	public DesignerDataConnection(string name, string providerName, string connectionString, bool isConfigured)
	{
		throw new NotImplementedException();
	}
}
