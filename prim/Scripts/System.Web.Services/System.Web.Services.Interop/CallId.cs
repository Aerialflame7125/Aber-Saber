using System.Runtime.InteropServices;

namespace System.Web.Services.Interop;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
internal struct CallId
{
	public string szMachine;

	public int dwPid;

	public IntPtr userThread;

	public long addStackPointer;

	public string szEntryPoint;

	public string szDestinationMachine;

	public CallId(string machine, int pid, IntPtr userThread, long stackPtr, string entryPoint, string destMachine)
	{
		szMachine = machine;
		dwPid = pid;
		this.userThread = userThread;
		addStackPointer = stackPtr;
		szEntryPoint = entryPoint;
		szDestinationMachine = destMachine;
	}
}
