using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Mozilla;

[ComImport]
[Guid("e72f94b2-5f85-11d4-9877-00c04fa0cf4a")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIErrorService
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int registerErrorStringBundle(short errorModule, [MarshalAs(UnmanagedType.LPStr)] string stringBundleURL);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int unregisterErrorStringBundle(short errorModule);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getErrorStringBundle(short errorModule, [MarshalAs(UnmanagedType.LPStr)] ref string ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int registerErrorStringBundleKey(int error, [MarshalAs(UnmanagedType.LPStr)] string stringBundleKey);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int unregisterErrorStringBundleKey(int error);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getErrorStringBundleKey(int error, StringBuilder ret);
}
