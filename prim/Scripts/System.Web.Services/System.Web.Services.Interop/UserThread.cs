using System.Runtime.InteropServices;

namespace System.Web.Services.Interop;

[StructLayout(LayoutKind.Sequential)]
internal class UserThread
{
	internal int pSidBuffer;

	internal int dwSidLen;

	internal int dwTid;

	internal UserThread()
	{
		pSidBuffer = 0;
		dwSidLen = 0;
		dwTid = 0;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is UserThread))
		{
			return false;
		}
		UserThread userThread = (UserThread)obj;
		if (userThread.dwTid == dwTid && userThread.pSidBuffer == pSidBuffer && userThread.dwSidLen == dwSidLen)
		{
			return true;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}
