using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("bddeda3f-9020-4d12-8c70-984ee9f7935e")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIIOService
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getProtocolHandler([MarshalAs(UnmanagedType.LPStr)] string aScheme, [MarshalAs(UnmanagedType.Interface)] out nsIProtocolHandler ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getProtocolFlags([MarshalAs(UnmanagedType.LPStr)] string aScheme, out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newURI(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newFileURI([MarshalAs(UnmanagedType.Interface)] nsIFile aFile, [MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newChannelFromURI([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int newChannel(HandleRef aSpec, [MarshalAs(UnmanagedType.LPStr)] string aOriginCharset, [MarshalAs(UnmanagedType.Interface)] nsIURI aBaseURI, [MarshalAs(UnmanagedType.Interface)] out nsIChannel ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getOffline(out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setOffline(bool value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int allowPort(int aPort, [MarshalAs(UnmanagedType.LPStr)] string aScheme, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int extractScheme(HandleRef urlString, HandleRef ret);
}
