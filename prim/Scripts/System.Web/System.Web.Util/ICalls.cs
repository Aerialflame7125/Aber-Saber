using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Web.Util;

internal class ICalls
{
	private ICalls()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern string GetMachineConfigPath();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern string GetMachineInstallDirectory();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool GetUnmanagedResourcesPtr(Assembly assembly, out IntPtr ptr, out int length);
}
