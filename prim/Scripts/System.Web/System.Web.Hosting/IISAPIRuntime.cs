using System.Runtime.InteropServices;

namespace System.Web.Hosting;

/// <summary>Defines the methods that are used to create <see cref="T:System.Web.HttpWorkerRequest" /> objects in the .NET Framework.</summary>
[ComImport]
[Guid("08A2C56F-7C16-41C1-A8BE-432917A1A2D1")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IISAPIRuntime
{
	/// <summary>Forces garbage collection.</summary>
	void DoGCCollect();

	/// <summary>Creates a new <see cref="T:System.Web.HttpWorkerRequest" /> object to process the current request.</summary>
	/// <param name="ecb">An ISAPI extension control block.</param>
	/// <param name="useProcessModel">
	///       <see langword="0" /> to create an out-of-process request; otherwise, an in-process request is created.</param>
	/// <returns>
	///     <see langword="0" /> if <see cref="T:System.Web.HttpWorkerRequest" /> was created successfully; otherwise, <see langword="1" />.</returns>
	[return: MarshalAs(UnmanagedType.I4)]
	int ProcessRequest([In] IntPtr ecb, [In][MarshalAs(UnmanagedType.I4)] int useProcessModel);

	/// <summary>Starts processing all items in the worker process pipeline.</summary>
	void StartProcessing();

	/// <summary>Stops processing the items in the worker process pipeline.</summary>
	void StopProcessing();
}
