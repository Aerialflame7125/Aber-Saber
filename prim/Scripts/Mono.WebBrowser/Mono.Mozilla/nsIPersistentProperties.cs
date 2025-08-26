using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("1A180F60-93B2-11d2-9B8B-00805F8A16D9")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIPersistentProperties : nsIProperties
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int get([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.LPStruct)] Guid iid, out IntPtr result);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int set([MarshalAs(UnmanagedType.LPStr)] string prop, [MarshalAs(UnmanagedType.Interface)] IntPtr value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int has([MarshalAs(UnmanagedType.LPStr)] string prop, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int undefine([MarshalAs(UnmanagedType.LPStr)] string prop);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getKeys(out uint count, [MarshalAs(UnmanagedType.LPStr)] out string[] keys);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int load([MarshalAs(UnmanagedType.Interface)] nsIInputStream input);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int save([MarshalAs(UnmanagedType.Interface)] nsIOutputStream output, HandleRef header);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int subclass([MarshalAs(UnmanagedType.Interface)] nsIPersistentProperties superclass);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int enumerate([MarshalAs(UnmanagedType.Interface)] out nsISimpleEnumerator ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getStringProperty(HandleRef key, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setStringProperty(HandleRef key, HandleRef value, HandleRef ret);
}
