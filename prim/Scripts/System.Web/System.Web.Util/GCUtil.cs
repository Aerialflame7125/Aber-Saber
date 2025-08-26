using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util;

[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
internal static class GCUtil
{
	public static IntPtr RootObject(object obj)
	{
		if (obj == null)
		{
			return IntPtr.Zero;
		}
		return (IntPtr)GCHandle.Alloc(obj);
	}

	public static object UnrootObject(IntPtr pointer)
	{
		if (pointer != IntPtr.Zero)
		{
			GCHandle gCHandle = (GCHandle)pointer;
			if (gCHandle.IsAllocated)
			{
				object target = gCHandle.Target;
				gCHandle.Free();
				return target;
			}
		}
		return null;
	}
}
