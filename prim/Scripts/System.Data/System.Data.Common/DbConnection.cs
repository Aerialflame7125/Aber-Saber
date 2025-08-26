using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Common;

/// <summary>Represents a connection to a database.</summary>
public abstract class DbConnection : Component, IDbConnection, IDisposable
{
	internal bool _suppressStateChangeForReconnection;

	/// <summary>Gets or sets the string used to open the connection.</summary>
	/// <returns>The connection string used to establish the initial connection. The exact contents of the connection string depend on the specific data source for this connection. The default value is an empty string.</returns>
	[DefaultValue("")]
	[SettingsBindable(true)]
	[RefreshProperties(RefreshProperties.All)]
	[RecommendedAsConfigurable(true)]
	public abstract string ConnectionString { get; set; }

	/// <summary>Gets the time to wait while establishing a connection before terminating the attempt and generating an error.</summary>
	/// <returns>The time (in seconds) to wait for a connection to open. The default value is determined by the specific type of connection that you are using.</returns>
	public virtual int ConnectionTimeout => 15;

	/// <summary>Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the connection is opened.</summary>
	/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
	public abstract string Database { get; }

	/// <summary>Gets the name of the database server to which to connect.</summary>
	/// <returns>The name of the database server to which to connect. The default value is an empty string.</returns>
	public abstract string DataSource { get; }

	/// <summary>Gets the <see cref="T:System.Data.Common.DbProviderFactory" /> for this <see cref="T:System.Data.Common.DbConnection" />.</summary>
	/// <returns>A set of methods for creating instances of a provider's implementation of the data source classes.</returns>
	protected virtual DbProviderFactory DbProviderFactory => null;

	/// <summary>Gets a string that represents the version of the server to which the object is connected.</summary>
	/// <returns>The version of the database. The format of the string returned depends on the specific type of connection you are using.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Data.Common.DbConnection.ServerVersion" /> was called while the returned Task was not completed and the connection was not opened after a call to <see cref="Overload:System.Data.Common.DbConnection.OpenAsync" />.</exception>
	[Browsable(false)]
	public abstract string ServerVersion { get; }

	/// <summary>Gets a string that describes the state of the connection.</summary>
	/// <returns>The state of the connection. The format of the string returned depends on the specific type of connection you are using.</returns>
	[Browsable(false)]
	public abstract ConnectionState State { get; }

	internal DbProviderFactory ProviderFactory => DbProviderFactory;

	/// <summary>Occurs when the state of the event changes.</summary>
	public virtual event StateChangeEventHandler StateChange;

	/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
	protected DbConnection()
	{
	}

	/// <summary>Starts a database transaction.</summary>
	/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
	/// <returns>An object representing the new transaction.</returns>
	protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

	/// <summary>Starts a database transaction.</summary>
	/// <returns>An object representing the new transaction.</returns>
	public DbTransaction BeginTransaction()
	{
		return BeginDbTransaction(IsolationLevel.Unspecified);
	}

	/// <summary>Starts a database transaction with the specified isolation level.</summary>
	/// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
	/// <returns>An object representing the new transaction.</returns>
	public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
	{
		return BeginDbTransaction(isolationLevel);
	}

	/// <summary>Begins a database transaction.</summary>
	/// <returns>An object that represents the new transaction.</returns>
	IDbTransaction IDbConnection.BeginTransaction()
	{
		return BeginDbTransaction(IsolationLevel.Unspecified);
	}

	/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
	/// <param name="isolationLevel">One of the <see cref="T:System.Data.IsolationLevel" /> values.</param>
	/// <returns>An object that represents the new transaction.</returns>
	IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
	{
		return BeginDbTransaction(isolationLevel);
	}

	/// <summary>Closes the connection to the database. This is the preferred method of closing any open connection.</summary>
	/// <exception cref="T:System.Data.Common.DbException">The connection-level error that occurred while opening the connection.</exception>
	public abstract void Close();

	/// <summary>Changes the current database for an open connection.</summary>
	/// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
	public abstract void ChangeDatabase(string databaseName);

	/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
	/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
	public DbCommand CreateCommand()
	{
		return CreateDbCommand();
	}

	/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the current connection.</summary>
	/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object that is associated with the connection.</returns>
	IDbCommand IDbConnection.CreateCommand()
	{
		return CreateDbCommand();
	}

	/// <summary>Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.</summary>
	/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
	protected abstract DbCommand CreateDbCommand();

	/// <summary>Enlists in the specified transaction.</summary>
	/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
	public virtual void EnlistTransaction(Transaction transaction)
	{
		throw ADP.NotSupported();
	}

	/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" />.</summary>
	/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
	public virtual DataTable GetSchema()
	{
		throw ADP.NotSupported();
	}

	/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.</summary>
	/// <param name="collectionName">Specifies the name of the schema to return.</param>
	/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="collectionName" /> is specified as null.</exception>
	public virtual DataTable GetSchema(string collectionName)
	{
		throw ADP.NotSupported();
	}

	/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
	/// <param name="collectionName">Specifies the name of the schema to return.</param>
	/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
	/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="collectionName" /> is specified as null.</exception>
	public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
	{
		throw ADP.NotSupported();
	}

	/// <summary>Raises the <see cref="E:System.Data.Common.DbConnection.StateChange" /> event.</summary>
	/// <param name="stateChange">A <see cref="T:System.Data.StateChangeEventArgs" /> that contains the event data.</param>
	protected virtual void OnStateChange(StateChangeEventArgs stateChange)
	{
		if (!_suppressStateChangeForReconnection)
		{
			this.StateChange?.Invoke(this, stateChange);
		}
	}

	/// <summary>Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.</summary>
	public abstract void Open();

	/// <summary>An asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />, which opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />. This method invokes the virtual method <see cref="M:System.Data.Common.DbConnection.OpenAsync(System.Threading.CancellationToken)" /> with CancellationToken.None.</summary>
	/// <returns>A task representing the asynchronous operation.</returns>
	public Task OpenAsync()
	{
		return OpenAsync(CancellationToken.None);
	}

	/// <summary>This is the asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />. Providers should override with an appropriate implementation. The cancellation token can optionally be honored.  
	///  The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbConnection.Open" /> call and returns a completed task. The default implementation will return a cancelled task if passed an already cancelled cancellationToken. Exceptions thrown by Open will be communicated via the returned Task Exception property.  
	///  Do not invoke other methods and properties of the <see langword="DbConnection" /> object until the returned Task is complete.</summary>
	/// <param name="cancellationToken">The cancellation instruction.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public virtual Task OpenAsync(CancellationToken cancellationToken)
	{
		if (cancellationToken.IsCancellationRequested)
		{
			return Task.FromCanceled(cancellationToken);
		}
		try
		{
			Open();
			return Task.CompletedTask;
		}
		catch (Exception exception)
		{
			return Task.FromException(exception);
		}
	}
}
