using System.Runtime.InteropServices;

namespace System.Web.Hosting;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("3A8E9CED-D3C9-4C4B-8956-6F15E2F559D9")]
internal interface ICustomRuntimeRegistrationToken
{
	void Unregister();
}
