using System.Runtime.InteropServices;

namespace System;

internal static class Platform
{
	private static bool checkedOS;

	private static bool isMacOS;

	private static bool isFreeBSD;

	public static bool IsMacOS
	{
		get
		{
			if (!checkedOS)
			{
				CheckOS();
			}
			return isMacOS;
		}
	}

	public static bool IsFreeBSD
	{
		get
		{
			if (!checkedOS)
			{
				CheckOS();
			}
			return isFreeBSD;
		}
	}

	[DllImport("libc")]
	private static extern int uname(IntPtr buf);

	private static void CheckOS()
	{
		if (Environment.OSVersion.Platform != PlatformID.Unix)
		{
			checkedOS = true;
			return;
		}
		IntPtr intPtr = Marshal.AllocHGlobal(8192);
		if (uname(intPtr) == 0)
		{
			string text = Marshal.PtrToStringAnsi(intPtr);
			if (!(text == "Darwin"))
			{
				if (text == "FreeBSD")
				{
					isFreeBSD = true;
				}
			}
			else
			{
				isMacOS = true;
			}
		}
		Marshal.FreeHGlobal(intPtr);
		checkedOS = true;
	}
}
