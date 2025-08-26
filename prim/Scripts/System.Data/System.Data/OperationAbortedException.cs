namespace System.Data;

/// <summary>This exception is thrown when an ongoing operation is aborted by the user.</summary>
[Serializable]
public sealed class OperationAbortedException : SystemException
{
	private OperationAbortedException(string message, Exception innerException)
		: base(message, innerException)
	{
		base.HResult = -2146232010;
	}

	internal static OperationAbortedException Aborted(Exception inner)
	{
		if (inner == null)
		{
			return new OperationAbortedException(global::SR.GetString("Operation aborted."), null);
		}
		return new OperationAbortedException(global::SR.GetString("Operation aborted due to an exception (see InnerException for details)."), inner);
	}
}
