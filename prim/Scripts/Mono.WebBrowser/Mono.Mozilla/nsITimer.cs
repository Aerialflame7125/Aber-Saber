using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("436a83fa-b396-11d9-bcfa-00112478d626")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsITimer
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int init([MarshalAs(UnmanagedType.Interface)] nsIObserver aObserver, uint aDelay, uint aType);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int initWithFuncCallback(nsITimerCallbackDelegate aCallback, IntPtr aClosure, uint aDelay, uint aType);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int initWithCallback([MarshalAs(UnmanagedType.Interface)] nsITimerCallback aCallback, uint aDelay, uint aType);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int cancel();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDelay(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setDelay(uint value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getType(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setType(uint value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getClosure(IntPtr ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCallback([MarshalAs(UnmanagedType.Interface)] out nsITimerCallback ret);
}
