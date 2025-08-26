using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("d94ac0a0-bb18-46b8-844e-84159064b0bd")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsICancelable
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int cancel(int aReason);
}
