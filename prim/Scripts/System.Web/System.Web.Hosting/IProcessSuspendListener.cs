using System.Runtime.InteropServices;

namespace System.Web.Hosting;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("406E6C4C-1C5D-4357-9DFE-EF4BE00D654B")]
internal interface IProcessSuspendListener
{
	[return: MarshalAs(UnmanagedType.Interface)]
	IProcessResumeCallback Suspend();
}
