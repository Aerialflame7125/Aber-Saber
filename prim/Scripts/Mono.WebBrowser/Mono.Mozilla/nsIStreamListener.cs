using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("1a637020-1482-11d3-9333-00104ba0fd40")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIStreamListener : nsIRequestObserver
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int onStartRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int onStopRequest([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, int aStatusCode);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int onDataAvailable([MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, IntPtr aContext, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aInputStream, uint aOffset, uint aCount);
}
