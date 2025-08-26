using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("15fd6940-8ea7-11d3-93ad-00104ba0fd40")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIProtocolHandler
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getScheme(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDefaultPort(out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getProtocolFlags(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newURI(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newChannel([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int allowPort(int port, [MarshalAs(UnmanagedType.LPStr)] string scheme, out bool ret);
}
