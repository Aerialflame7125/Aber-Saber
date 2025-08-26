using System.Runtime.InteropServices;

namespace System.Web.Hosting;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("692D0723-C338-4D09-9057-C71F0F47DA87")]
internal interface ICustomRuntime
{
	void Start([In] IntPtr reserved0, [In] int reserved1);

	void ResolveModules([In] IntPtr pResolveModuleData, [In] int resolveModuleDataSize);

	void Stop([In] IntPtr reserved0, [In] int reserved1);
}
