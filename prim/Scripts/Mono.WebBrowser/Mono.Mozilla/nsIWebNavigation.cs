using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("F5D9E7B0-D930-11d3-B057-00A024FFC08C")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIWebNavigation
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCanGoBack(out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCanGoForward(out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int goBack();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int goForward();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int gotoIndex(int index);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int loadURI([MarshalAs(UnmanagedType.LPWStr)] string aURI, uint aLoadFlags, [MarshalAs(UnmanagedType.Interface)] nsIURI aReferrer, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aPostData, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aHeaders);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int reload(uint aReloadFlags);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int stop(uint aStopFlags);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCurrentURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getReferringURI([MarshalAs(UnmanagedType.Interface)] out nsIURI ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getSessionHistory([MarshalAs(UnmanagedType.Interface)] out nsISHistory ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setSessionHistory([MarshalAs(UnmanagedType.Interface)] nsISHistory value);
}
