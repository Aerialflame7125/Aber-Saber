using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("27386cf1-f27e-4d2d-9bf4-c4621d50d299")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIAccessibilityService : nsIAccessibleRetrieval
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getAttachedAccessibleFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getRelevantContentNodeFor([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIDOMNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getAccessibleInWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aDOMWin, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getAccessibleInWeakShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getAccessibleInShell([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getCachedAccessNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessNode ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getCachedAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getStringRole(uint aRole, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getStringStates(uint aStates, uint aExtraStates, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDOMStringList ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getStringEventType(uint aEventType, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	new int getStringRelationType(uint aRelationType, HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createOuterDocAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createRootAccessible(IntPtr aShell, IntPtr aDocument, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTML4ButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHyperTextAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLBRAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLAccessibleByMarkup(IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aWeakShell, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode aDOMNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLLIAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] IntPtr aBulletFrame, HandleRef aBulletText, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLCheckboxAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLComboboxAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLGenericAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLGroupboxAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLHRAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLImageAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLLabelAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLListboxAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLObjectFrameAccessible(IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLRadioButtonAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLSelectOptionAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, [MarshalAs(UnmanagedType.Interface)] nsIAccessible aAccParent, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aPresShell, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLTableAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLTableCellAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLTableHeadAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aDOMNode, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLTextAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLTextFieldAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createHTMLCaptionAccessible([MarshalAs(UnmanagedType.Interface)] IntPtr aFrame, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getAccessible([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode, IntPtr aPresShell, [MarshalAs(UnmanagedType.Interface)] nsIWeakReference aWeakShell, out IntPtr frameHint, out bool aIsHidden, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int addNativeRootAccessible(IntPtr aAtkAccessible, [MarshalAs(UnmanagedType.Interface)] out nsIAccessible ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int removeNativeRootAccessible([MarshalAs(UnmanagedType.Interface)] nsIAccessible aRootAccessible);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int invalidateSubtreeFor(IntPtr aPresShell, IntPtr aChangedContent, uint aEvent);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int processDocLoadEvent([MarshalAs(UnmanagedType.Interface)] nsITimer aTimer, IntPtr aClosure, uint aEventType);
}
