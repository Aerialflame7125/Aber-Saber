using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("69E5DF00-7B8B-11d3-AF61-00A024FFC08C")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIWebBrowser
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int addWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int removeWebBrowserListener([MarshalAs(UnmanagedType.Interface)] nsIWeakReference aListener, [MarshalAs(UnmanagedType.LPStruct)] Guid aIID);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getContainerWindow([MarshalAs(UnmanagedType.Interface)] out nsIWebBrowserChrome ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setContainerWindow([MarshalAs(UnmanagedType.Interface)] nsIWebBrowserChrome value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getParentURIContentListener([MarshalAs(UnmanagedType.Interface)] out nsIURIContentListener ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setParentURIContentListener([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getContentDOMWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);
}
