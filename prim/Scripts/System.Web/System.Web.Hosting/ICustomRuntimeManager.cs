using System.Runtime.InteropServices;

namespace System.Web.Hosting;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("A0BBBDFF-5AF5-42E3-9753-34441F764A6B")]
internal interface ICustomRuntimeManager
{
	[return: MarshalAs(UnmanagedType.Interface)]
	ICustomRuntimeRegistrationToken Register([In][MarshalAs(UnmanagedType.Interface)] ICustomRuntime customRuntime);
}
