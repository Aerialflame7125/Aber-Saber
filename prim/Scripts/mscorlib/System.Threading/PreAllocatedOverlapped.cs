namespace System.Threading;

/// <summary>Represents pre-allocated state for native overlapped I/O operations.</summary>
public sealed class PreAllocatedOverlapped : IDisposable
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Threading.PreAllocatedOverlapped" /> class and specifies a delegate to invoke when each asynchronous I/O operation is complete, a user-provided object that provides context, and managed objects that serve as buffers.</summary>
	/// <param name="callback">A delegate that represents the callback method to invoke when each asynchronous I/O operation completes.</param>
	/// <param name="state">A user-supplied object that distinguishes the <see cref="T:System.Threading.NativeOverlapped" /> instance produced from this object from other <see cref="T:System.Threading.NativeOverlapped" /> instances. Its value can be <see langword="null" />.</param>
	/// <param name="pinData">An object or array of objects that represent the input or output buffer for the operations. Each object represents a buffer, such as an array of bytes. Its value can be <see langword="null" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="callback" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> was disposed.</exception>
	[CLSCompliant(false)]
	public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>Frees the resources associated with this <see cref="T:System.Threading.PreAllocatedOverlapped" /> instance.</summary>
	public void Dispose()
	{
	}
}
