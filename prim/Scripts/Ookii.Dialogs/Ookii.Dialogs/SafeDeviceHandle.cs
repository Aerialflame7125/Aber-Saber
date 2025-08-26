using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs;

[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
internal class SafeDeviceHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	internal SafeDeviceHandle()
		: base(ownsHandle: true)
	{
	}

	[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
	internal SafeDeviceHandle(IntPtr existingHandle, bool ownsHandle)
		: base(ownsHandle)
	{
		SetHandle(existingHandle);
	}

	protected override bool ReleaseHandle()
	{
		return NativeMethods.DeleteDC(handle);
	}
}
