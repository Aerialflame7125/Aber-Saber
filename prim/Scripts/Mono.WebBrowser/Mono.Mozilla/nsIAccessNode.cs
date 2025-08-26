using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("71a3b4e7-e83d-45cf-a20e-9ce292bcf19f")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIAccessNode
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDOMNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getNumChildren(out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getChildNodeAt(int childNum, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getParentNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getFirstChildNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getLastChildNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getPreviousSiblingNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getNextSiblingNode([MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessibleDocument([MarshalAs(UnmanagedType.Interface)] out nsIAccessibleDocument ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getInnerHTML(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollTo(uint aScrollType);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int scrollToPoint(uint aCoordinateType, int aX, int aY);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getOwnerWindow(IntPtr ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getUniqueID(IntPtr ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getComputedStyleValue(HandleRef pseudoElt, HandleRef propertyName, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getComputedStyleCSSValue(HandleRef pseudoElt, HandleRef propertyName, [MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSPrimitiveValue ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getLanguage(HandleRef ret);
}
