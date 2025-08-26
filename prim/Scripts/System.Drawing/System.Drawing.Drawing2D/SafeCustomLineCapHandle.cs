using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Drawing2D;

[SecurityCritical]
internal class SafeCustomLineCapHandle : SafeHandle
{
	public override bool IsInvalid => handle == IntPtr.Zero;

	internal SafeCustomLineCapHandle(IntPtr h)
		: base(IntPtr.Zero, ownsHandle: true)
	{
		SetHandle(h);
	}

	[SecurityCritical]
	protected override bool ReleaseHandle()
	{
		int num = 0;
		if (!IsInvalid)
		{
			try
			{
				num = GDIPlus.GdipDeleteCustomLineCap(new HandleRef(this, handle));
			}
			catch (Exception ex)
			{
				if (ClientUtils.IsSecurityOrCriticalException(ex))
				{
					throw;
				}
			}
			finally
			{
				handle = IntPtr.Zero;
			}
		}
		return num == 0;
	}

	public static implicit operator IntPtr(SafeCustomLineCapHandle handle)
	{
		return handle?.handle ?? IntPtr.Zero;
	}

	public static explicit operator SafeCustomLineCapHandle(IntPtr handle)
	{
		return new SafeCustomLineCapHandle(handle);
	}
}
