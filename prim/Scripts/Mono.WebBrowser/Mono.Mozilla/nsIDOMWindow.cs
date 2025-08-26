using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("a6cf906b-15b3-11d2-932e-00805f8add32")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMWindow
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDocument([MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getParent([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getTop([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindow ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getScrollbars([MarshalAs(UnmanagedType.Interface)] out nsIDOMBarProp ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getFrames([MarshalAs(UnmanagedType.Interface)] out nsIDOMWindowCollection ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getName(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setName(HandleRef value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getTextZoom(out float ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setTextZoom(float value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getScrollX(out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getScrollY(out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollTo(int xScroll, int yScroll);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollBy(int xScrollDif, int yScrollDif);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getSelection([MarshalAs(UnmanagedType.Interface)] out nsISelection ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollByLines(int numLines);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollByPages(int numPages);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int sizeToContent();
}
