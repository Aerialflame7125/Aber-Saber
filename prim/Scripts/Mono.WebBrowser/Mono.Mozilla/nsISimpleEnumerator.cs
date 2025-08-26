using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("D1899240-F9D2-11D2-BDD6-000064657374")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsISimpleEnumerator
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int hasMoreElements(out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getNext(out IntPtr ret);
}
