using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("f85c5a20-258d-11db-a98b-0800200c9a66")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDocumentEncoder
{
	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void init([MarshalAs(UnmanagedType.Interface)] nsIDOMDocument aDocument, HandleRef aMimeType, uint aFlags);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setSelection([MarshalAs(UnmanagedType.Interface)] nsISelection aSelection);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setRange([MarshalAs(UnmanagedType.Interface)] nsIDOMRange aRange);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aNode);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setContainerNode([MarshalAs(UnmanagedType.Interface)] nsIDOMNode aContainer);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setCharset(HandleRef aCharset);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setWrapColumn(uint aWrapColumn);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getMimeType(HandleRef ret);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void encodeToStream([MarshalAs(UnmanagedType.Interface)] nsIOutputStream aStream);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int encodeToString(HandleRef ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int encodeToStringWithContext(HandleRef aContextString, HandleRef aInfoString, HandleRef ret);

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void setNodeFixup([MarshalAs(UnmanagedType.Interface)] nsIDocumentEncoderNodeFixup aFixup);
}
