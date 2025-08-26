using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs;

[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
internal class ActivationContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	public ActivationContextSafeHandle()
		: base(ownsHandle: true)
	{
	}

	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	protected override bool ReleaseHandle()
	{
		NativeMethods.ReleaseActCtx(handle);
		return true;
	}
}
