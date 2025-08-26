namespace System.Transactions;

/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
public sealed class TransactionScope : IDisposable
{
	private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

	private Transaction transaction;

	private Transaction oldTransaction;

	private TransactionScope parentScope;

	private TimeSpan timeout;

	private int nested;

	private bool disposed;

	private bool completed;

	private bool isRoot;

	private bool asyncFlowEnabled;

	internal bool IsComplete => completed;

	internal TimeSpan Timeout => timeout;

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class.</summary>
	public TransactionScope()
		: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified asynchronous flow option.</summary>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(TransactionScopeAsyncFlowOption asyncFlowOption)
		: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout, asyncFlowOption)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
	/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
	public TransactionScope(Transaction transactionToUse)
		: this(transactionToUse, TransactionManager.DefaultTimeout)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
	/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
	/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
	public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout)
		: this(transactionToUse, scopeTimeout, EnterpriseServicesInteropOption.None)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
	/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
	/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
	/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
	[System.MonoTODO("EnterpriseServicesInteropOption not supported.")]
	public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, EnterpriseServicesInteropOption interopOption)
	{
		Initialize(TransactionScopeOption.Required, transactionToUse, defaultOptions, interopOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	public TransactionScope(TransactionScopeOption scopeOption)
		: this(scopeOption, TransactionManager.DefaultTimeout)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and requirements.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
	public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout)
		: this(scopeOption, scopeTimeout, TransactionScopeAsyncFlowOption.Suppress)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(TransactionScopeOption option, TransactionScopeAsyncFlowOption asyncFlow)
		: this(option, TransactionManager.DefaultTimeout, asyncFlow)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, requirements, and asynchronous flow option.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(TransactionScopeOption scopeOption, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
	{
		Initialize(scopeOption, null, defaultOptions, EnterpriseServicesInteropOption.None, scopeTimeout, asyncFlow);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
	public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions)
		: this(scopeOption, transactionOptions, EnterpriseServicesInteropOption.None)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified scope and COM+ interoperability requirements, and transaction options.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
	/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
	[System.MonoTODO("EnterpriseServicesInteropOption not supported")]
	public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, EnterpriseServicesInteropOption interopOption)
	{
		Initialize(scopeOption, null, transactionOptions, interopOption, TransactionManager.DefaultTimeout, TransactionScopeAsyncFlowOption.Suppress);
	}

	/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
	///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
	/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(Transaction transactionToUse, TransactionScopeAsyncFlowOption asyncFlowOption)
	{
		throw new NotImplementedException();
	}

	/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
	///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction.</summary>
	/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
	/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(Transaction transactionToUse, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlowOption)
	{
		throw new NotImplementedException();
	}

	/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
	///  Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements and asynchronous flow option.</summary>
	/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
	/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
	/// <param name="asyncFlowOption">An instance of the <see cref="T:System.Transactions.TransactionScopeAsyncFlowOption" /> enumeration that describes whether the ambient transaction associated with the transaction scope will flow across thread continuations when using Task or async/await .NET async programming patterns.</param>
	public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions transactionOptions, TransactionScopeAsyncFlowOption asyncFlowOption)
	{
		throw new NotImplementedException();
	}

	private void Initialize(TransactionScopeOption scopeOption, Transaction tx, TransactionOptions options, EnterpriseServicesInteropOption interop, TimeSpan scopeTimeout, TransactionScopeAsyncFlowOption asyncFlow)
	{
		completed = false;
		isRoot = false;
		nested = 0;
		asyncFlowEnabled = asyncFlow == TransactionScopeAsyncFlowOption.Enabled;
		if (scopeTimeout < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("scopeTimeout");
		}
		timeout = scopeTimeout;
		oldTransaction = Transaction.CurrentInternal;
		Transaction.CurrentInternal = (transaction = InitTransaction(tx, scopeOption));
		if (transaction != null)
		{
			transaction.InitScope(this);
		}
		if (parentScope != null)
		{
			parentScope.nested++;
		}
	}

	private Transaction InitTransaction(Transaction tx, TransactionScopeOption scopeOption)
	{
		if (tx != null)
		{
			return tx;
		}
		switch (scopeOption)
		{
		case TransactionScopeOption.Suppress:
			if (Transaction.CurrentInternal != null)
			{
				parentScope = Transaction.CurrentInternal.Scope;
			}
			return null;
		case TransactionScopeOption.Required:
			if (Transaction.CurrentInternal == null)
			{
				isRoot = true;
				return new Transaction();
			}
			parentScope = Transaction.CurrentInternal.Scope;
			return Transaction.CurrentInternal;
		default:
			if (Transaction.CurrentInternal != null)
			{
				parentScope = Transaction.CurrentInternal.Scope;
			}
			isRoot = true;
			return new Transaction();
		}
	}

	/// <summary>Indicates that all operations within the scope are completed successfully.</summary>
	/// <exception cref="T:System.InvalidOperationException">This method has already been called once.</exception>
	public void Complete()
	{
		if (completed)
		{
			throw new InvalidOperationException("The current TransactionScope is already complete. You should dispose the TransactionScope.");
		}
		completed = true;
	}

	/// <summary>Ends the transaction scope.</summary>
	public void Dispose()
	{
		if (disposed)
		{
			return;
		}
		disposed = true;
		if (parentScope != null)
		{
			parentScope.nested--;
		}
		if (nested > 0)
		{
			transaction.Rollback();
			throw new InvalidOperationException("TransactionScope nested incorrectly");
		}
		if (Transaction.CurrentInternal != transaction && !asyncFlowEnabled)
		{
			if (transaction != null)
			{
				transaction.Rollback();
			}
			if (Transaction.CurrentInternal != null)
			{
				Transaction.CurrentInternal.Rollback();
			}
			throw new InvalidOperationException("Transaction.Current has changed inside of the TransactionScope");
		}
		if (asyncFlowEnabled)
		{
			if (oldTransaction != null)
			{
				oldTransaction.Scope = parentScope;
			}
			Transaction currentInternal = Transaction.CurrentInternal;
			if (!(transaction == null) || !(currentInternal == null))
			{
				currentInternal.Scope = parentScope;
				Transaction.CurrentInternal = oldTransaction;
				transaction.Scope = null;
				if (!IsComplete)
				{
					transaction.Rollback();
					currentInternal.Rollback();
				}
				else if (isRoot)
				{
					currentInternal.CommitInternal();
					transaction.CommitInternal();
				}
			}
			return;
		}
		if (Transaction.CurrentInternal == oldTransaction && oldTransaction != null)
		{
			oldTransaction.Scope = parentScope;
		}
		Transaction.CurrentInternal = oldTransaction;
		if (!(transaction == null))
		{
			transaction.Scope = null;
			if (!IsComplete)
			{
				transaction.Rollback();
			}
			else if (isRoot)
			{
				transaction.CommitInternal();
			}
		}
	}
}
