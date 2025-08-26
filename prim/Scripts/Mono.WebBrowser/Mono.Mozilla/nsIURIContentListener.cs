using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("94928AB3-8B63-11d3-989D-001083010E9B")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIURIContentListener
{
	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	bool onStartURIOpen([MarshalAs(UnmanagedType.Interface)] nsIURI aURI);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	bool doContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, bool aIsContentPreferred, [MarshalAs(UnmanagedType.Interface)] nsIRequest aRequest, [MarshalAs(UnmanagedType.Interface)] out nsIStreamListener aContentHandler);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	bool isPreferred([MarshalAs(UnmanagedType.LPStr)] string aContentType, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	bool canHandleContent([MarshalAs(UnmanagedType.LPStr)] string aContentType, bool aIsContentPreferred, [MarshalAs(UnmanagedType.LPStr)] ref string aDesiredContentType);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[return: MarshalAs(UnmanagedType.Interface)]
	IntPtr getLoadCookie();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setLoadCookie([MarshalAs(UnmanagedType.Interface)] IntPtr value);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[return: MarshalAs(UnmanagedType.Interface)]
	nsIURIContentListener getParentContentListener();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setParentContentListener([MarshalAs(UnmanagedType.Interface)] nsIURIContentListener value);
}
