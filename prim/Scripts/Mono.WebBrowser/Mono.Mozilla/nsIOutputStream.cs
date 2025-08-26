using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("0d0acd2a-61b4-11d4-9877-00c04fa0cf4a")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIOutputStream
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int close();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int flush();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int write([MarshalAs(UnmanagedType.LPStr)] string aBuf, uint aCount, out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int writeFrom([MarshalAs(UnmanagedType.Interface)] nsIInputStream aFromStream, uint aCount, out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int writeSegments(nsIReadSegmentFunDelegate aReader, IntPtr aClosure, uint aCount, out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int isNonBlocking(out bool ret);
}
