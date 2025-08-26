using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("9c18bb4e-1dd1-11b2-bf91-9cc82c275823")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDocCharset
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCharset(ref IntPtr ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setCharset([MarshalAs(UnmanagedType.LPStr)] string value);
}
