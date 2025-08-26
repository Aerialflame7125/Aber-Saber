using System.Runtime.InteropServices;

namespace System.Web.Hosting;

/// <summary>Provides listener-channel notifications from protocol handlers to the worker process framework. Also enables protocol handlers to access additional parameters, such as the ID of the listener channel.</summary>
[ComImport]
[Guid("dc3b0a85-9da7-47e4-ba1b-e27da9db8a1e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IListenerChannelCallback
{
	/// <summary>Notifies the worker process framework that a listener channel has started.</summary>
	void ReportStarted();

	/// <summary>Notifies the worker process framework that a listener channel has stopped.</summary>
	/// <param name="hr">The HRESULT value that reports the status of a listener channel.</param>
	void ReportStopped(int hr);

	/// <summary>Notifies the worker process framework that a new message was received.</summary>
	void ReportMessageReceived();

	/// <summary>Gets the ID of a listener channel that has to be started.</summary>
	/// <returns>The ID of the listener channel.</returns>
	int GetId();

	/// <summary>Gets the size of the buffered data in the listener channel.</summary>
	/// <returns>The size, in bytes, of the data that is in the listener channel buffer.</returns>
	int GetBlobLength();

	/// <summary>Retrieves data that the protocol manager will pass to a listener channel when the protocol manager is starting that listener channel.</summary>
	/// <param name="buffer">The data that will be passed to the listener channel.</param>
	/// <param name="bufferSize">The amount of data, in bytes, that is in the buffer.</param>
	void GetBlob([In][Out][MarshalAs(UnmanagedType.LPArray)] byte[] buffer, ref int bufferSize);
}
