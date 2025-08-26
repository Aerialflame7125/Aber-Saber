using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("6aff3102-320d-4986-9790-12316bb87cf9")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMRGBColor
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getRed([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getGreen([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getBlue([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);
}
