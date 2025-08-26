using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("78650582-4e93-4b60-8e85-26ebd3eb14ca")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIProperties
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int get([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IntPtr result);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int set([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.Interface)] IntPtr value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int has([MarshalAs(UnmanagedType.LPStr)] string prop, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int undefine([MarshalAs(UnmanagedType.LPStr)] string prop);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getKeys(out uint count, [MarshalAs(UnmanagedType.LPStr)] out string[] keys);
}
