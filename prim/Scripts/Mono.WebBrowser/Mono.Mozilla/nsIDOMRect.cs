using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("71735f62-ac5c-4236-9a1f-5ffb280d531c")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMRect
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getTop([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getRight([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getBottom([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getLeft([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);
}
