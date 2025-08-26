using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Mozilla;

[ComImport]
[Guid("56c35506-f14b-11d3-99d3-ddbfac2ccf65")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface nsIPrefBranch
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getRoot(ref IntPtr ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getPrefType([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getBoolPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setBoolPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, int aValue);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getCharPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStr)] ref string ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setCharPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStr)] string aValue);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getIntPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out int ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setIntPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName, int aValue);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getComplexValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStruct)] Guid aType, out IntPtr aValue);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int setComplexValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, [MarshalAs(UnmanagedType.LPStruct)] Guid aType, IntPtr aValue);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int clearUserPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int lockPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int prefHasUserValue([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int prefIsLocked([MarshalAs(UnmanagedType.LPStr)] string aPrefName, out bool ret);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int unlockPref([MarshalAs(UnmanagedType.LPStr)] string aPrefName);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int deleteBranch([MarshalAs(UnmanagedType.LPStr)] string aStartingAt);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int getChildList([MarshalAs(UnmanagedType.LPStr)] string aStartingAt, out uint aCount, [MarshalAs(UnmanagedType.LPStr)] out string[] aChildArray);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int resetBranch([MarshalAs(UnmanagedType.LPStr)] string aStartingAt);
}
