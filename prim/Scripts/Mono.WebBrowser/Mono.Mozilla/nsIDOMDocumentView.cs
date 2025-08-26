using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("1ACDB2BA-1DD2-11B2-95BC-9542495D2569")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIDOMDocumentView
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDefaultView([MarshalAs(UnmanagedType.Interface)] out nsIDOMAbstractView ret);
}
