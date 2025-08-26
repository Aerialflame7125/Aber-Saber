using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("dd4e0a6a-210f-419a-ad85-40e8543b9465")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIWebBrowserPersist : nsICancelable
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int cancel(int aReason);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getPersistFlags(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setPersistFlags(uint value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCurrentState(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getResult(out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getProgressListener([MarshalAs(UnmanagedType.Interface)] out nsIWebProgressListener ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int saveURI([MarshalAs(UnmanagedType.Interface)] nsIURI aURI, IntPtr aCacheKey, [MarshalAs(UnmanagedType.Interface)] nsIURI aReferrer, [MarshalAs(UnmanagedType.Interface)] nsIInputStream aPostData, [MarshalAs(UnmanagedType.LPStr)] string aExtraHeaders, IntPtr aFile);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int saveChannel([MarshalAs(UnmanagedType.Interface)] nsIChannel aChannel, IntPtr aFile);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int saveDocument([MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDocument, IntPtr aFile, IntPtr aDataPath, [MarshalAs(UnmanagedType.LPStr)] string aOutputContentType, uint aEncodingFlags, uint aWrapColumn);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int cancelSave();
}
