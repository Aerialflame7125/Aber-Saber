using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("a6cf9074-15b3-11d2-932e-00805f8add32")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMDOMImplementation
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int hasFeature(HandleRef feature, HandleRef version, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createDocumentType(HandleRef qualifiedName, HandleRef publicId, HandleRef systemId, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDocumentType ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int createDocument(HandleRef namespaceURI, HandleRef qualifiedName, [MarshalAs(UnmanagedType.Interface)] nsIDOMDocumentType doctype, [MarshalAs(UnmanagedType.Interface)] out nsIDOMDocument ret);
}
