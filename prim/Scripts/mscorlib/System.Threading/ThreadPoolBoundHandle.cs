using System.Runtime.InteropServices;

namespace System.Threading;

/// <summary>Represents an I/O handle that is bound to the system thread pool and enables low-level components to receive notifications for asynchronous I/O operations.</summary>
public sealed class ThreadPoolBoundHandle : IDisposable
{
	/// <summary>Gets the bound operating system handle.</summary>
	/// <returns>An object that holds the bound operating system handle.</returns>
	public SafeHandle Handle
	{
		get
		{
			throw new PlatformNotSupportedException();
		}
	}

	internal ThreadPoolBoundHandle()
	{
	}

	/// <summary>Returns an unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying a delegate that is invoked when the asynchronous I/O operation is complete, a user-provided object that supplies context, and managed objects that serve as buffers.</summary>
	/// <param name="callback">A delegate that represents the callback method to invoke when the asynchronous I/O operation completes.</param>
	/// <param name="state">A user-provided object that distinguishes this <see cref="T:System.Threading.NativeOverlapped" /> instance from other <see cref="T:System.Threading.NativeOverlapped" /> instances.</param>
	/// <param name="pinData">An object or array of objects that represent the input or output buffer for the operation, or <see langword="null" />. Each object represents a buffer, such an array of bytes.</param>
	/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="callback" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> object was disposed.</exception>
	[CLSCompliant(false)]
	public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>Returns an unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure using the callback state and buffers associated with the specified <see cref="T:System.Threading.PreAllocatedOverlapped" /> object.</summary>
	/// <param name="preAllocated">An object from which to create the <see cref="T:System.Threading.NativeOverlapped" /> pointer.</param>
	/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="preAllocated" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="preAllocated" /> is currently in use for another I/O operation.</exception>
	/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> was disposed.  
	///  -or-  
	///  This method was called after <paramref name="preAllocated" /> was disposed.</exception>
	[CLSCompliant(false)]
	public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>Returns a <see cref="T:System.Threading.ThreadPoolBoundHandle" /> for the specified handle, which is bound to the system thread pool.</summary>
	/// <param name="handle">An object that holds the operating system handle. The handle must have been opened for overlapped I/O in unmanaged code.</param>
	/// <returns>A <see cref="T:System.Threading.ThreadPoolBoundHandle" /> for <paramref name="handle" />, which is bound to the system thread pool.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="handle" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="handle" /> has been disposed.  
	/// -or-  
	/// <paramref name="handle" /> does not refer to a valid I/O handle.  
	/// -or-  
	/// <paramref name="handle" /> refers to a handle that has not been opened for overlapped I/O.  
	/// -or-  
	/// <paramref name="handle" /> refers to a handle that has already been bound.</exception>
	public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>Releases all unmanaged resources used by the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> instance.</summary>
	public void Dispose()
	{
	}

	/// <summary>Frees the memory associated with a <see cref="T:System.Threading.NativeOverlapped" /> structure allocated by the <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" /> method.</summary>
	/// <param name="overlapped">An unmanaged pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure structure to be freed.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="overlapped" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">This method was called after the <see cref="T:System.Threading.ThreadPoolBoundHandle" /> object was disposed.</exception>
	[CLSCompliant(false)]
	public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>Returns the user-provided object that was specified when the <see cref="T:System.Threading.NativeOverlapped" /> instance was allocated by calling the <see cref="M:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped(System.Threading.IOCompletionCallback,System.Object,System.Object)" /> method.</summary>
	/// <param name="overlapped">An unmanaged pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure from which to return the associated user-provided object.</param>
	/// <returns>A user-provided object that distinguishes this <see cref="T:System.Threading.NativeOverlapped" /> instance from other <see cref="T:System.Threading.NativeOverlapped" /> instances, or <see langword="null" /> if one was not specified when the intstance was allocated by calling the <see cref="Overload:System.Threading.ThreadPoolBoundHandle.AllocateNativeOverlapped" /> method.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="overlapped" /> is <see langword="null" />.</exception>
	[CLSCompliant(false)]
	public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
	{
		throw new PlatformNotSupportedException();
	}
}
