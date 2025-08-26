using System.Runtime.Serialization;

namespace System.Transactions;

/// <summary>Describes a clone of a transaction providing guarantee that the transaction cannot be committed until the application comes to rest regarding work on the transaction. This class cannot be inherited.</summary>
[Serializable]
[System.MonoTODO("Not supported yet")]
public sealed class DependentTransaction : Transaction, ISerializable
{
	private bool completed;

	internal bool Completed => completed;

	internal DependentTransaction(Transaction parent, DependentCloneOption option)
	{
	}

	/// <summary>Attempts to complete the dependent transaction.</summary>
	/// <exception cref="T:System.Transactions.TransactionException">Any attempt for additional work on the transaction after this method is called. These include invoking methods such as <see cref="Overload:System.Transactions.Transaction.EnlistVolatile" />, <see cref="Overload:System.Transactions.Transaction.EnlistDurable" />, <see cref="M:System.Transactions.Transaction.Clone" />, <see cref="M:System.Transactions.Transaction.DependentClone(System.Transactions.DependentCloneOption)" /> , or any serialization operations on the transaction.</exception>
	[System.MonoTODO]
	public void Complete()
	{
		throw new NotImplementedException();
	}

	void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
	{
		completed = info.GetBoolean("completed");
	}
}
