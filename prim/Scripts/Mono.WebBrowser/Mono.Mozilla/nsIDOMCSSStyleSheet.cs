using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("a6cf90c2-15b3-11d2-932e-00805f8add32")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMCSSStyleSheet : nsIDOMStyleSheet
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getType(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getDisabled(out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int setDisabled(bool value);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getOwnerNode([MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getParentStyleSheet([MarshalAs(UnmanagedType.Interface)] out nsIDOMStyleSheet ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getHref(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getTitle(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getMedia([MarshalAs(UnmanagedType.Interface)] out nsIDOMMediaList ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getOwnerRule([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRule ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCssRules([MarshalAs(UnmanagedType.Interface)] out nsIDOMCSSRuleList ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int insertRule(HandleRef rule, uint index, out uint ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int deleteRule(uint index);
}
