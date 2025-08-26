using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("decb9cc7-c08f-4ea5-be91-a8fc637ce2d2")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIPrefService
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int readUserPrefs([MarshalAs(UnmanagedType.Interface)] nsIFile aFile);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int resetPrefs();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int resetUserPrefs();

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int savePrefFile([MarshalAs(UnmanagedType.Interface)] nsIFile aFile);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getBranch([MarshalAs(UnmanagedType.LPStr)] string aPrefRoot, [MarshalAs(UnmanagedType.Interface)] out nsIPrefBranch ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getDefaultBranch([MarshalAs(UnmanagedType.LPStr)] string aPrefRoot, [MarshalAs(UnmanagedType.Interface)] out nsIPrefBranch ret);
}
