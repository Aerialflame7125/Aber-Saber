using System.EnterpriseServices;
using System.Security.Permissions;

namespace System.Web.Util;

/// <summary>Provides a way to wrap a callback method within a transaction boundary.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Transactions
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Util.Transactions" /> class.</summary>
	public Transactions()
	{
	}

	/// <summary>Wraps a specified transaction support around a callback method.</summary>
	/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
	/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
	public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode)
	{
		bool transactionAborted = false;
		InvokeTransacted(callback, mode, ref transactionAborted);
	}

	/// <summary>Wraps a specified transaction support around a callback method and indicates whether the transaction aborted.</summary>
	/// <param name="callback">The <see cref="T:System.Web.Util.TransactedCallback" /> to be run under the specified transaction support.</param>
	/// <param name="mode">The <see cref="T:System.EnterpriseServices.TransactionOption" /> that specifies the transaction support for the delegate.</param>
	/// <param name="transactionAborted">The reference parameter that returns <see langword="true" /> if the transaction was aborted during the callback method; otherwise, <see langword="false" />. </param>
	/// <exception cref="T:System.PlatformNotSupportedException">The operating system is not Windows NT or later.</exception>
	/// <exception cref="T:System.Web.HttpException">The transacted code cannot be executed.</exception>
	[MonoTODO("Not implemented, not supported by Mono")]
	public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode, ref bool transactionAborted)
	{
		throw new PlatformNotSupportedException("Not supported on mono");
	}
}
