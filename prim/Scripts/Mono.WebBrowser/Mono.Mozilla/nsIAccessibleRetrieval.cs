using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("244e4c67-a1d3-44f2-9cab-cdaa31b68046")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIAccessibleRetrieval
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAttachedAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getRelevantContentNodeFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessibleInWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aDOMWin, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessibleInWeakShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessibleInShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCachedAccessNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCachedAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getStringRole(uint aRole, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getStringStates(uint aStates, uint aExtraStates, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDOMStringList ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getStringEventType(uint aEventType, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getStringRelationType(uint aRelationType, HandleRef ret);
}
