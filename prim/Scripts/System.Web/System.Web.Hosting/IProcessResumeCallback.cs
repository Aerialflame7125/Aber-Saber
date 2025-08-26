using System.Runtime.InteropServices;

namespace System.Web.Hosting;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("BB1AEEC0-E4EC-47BA-8724-D26AC4F16604")]
internal interface IProcessResumeCallback
{
	void Resume();
}
