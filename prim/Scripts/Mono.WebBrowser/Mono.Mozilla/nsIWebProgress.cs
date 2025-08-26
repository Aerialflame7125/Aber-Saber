using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("570F39D0-EFD0-11d3-B093-00A024FFC08C")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIWebProgress
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int addProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener aListener, uint aNotifyMask);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int removeProgressListener([MarshalAs(UnmanagedType.Interface)] nsIWebProgressListener aListener);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDOMWindow([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getIsLoadingDocument(out bool ret);
}
