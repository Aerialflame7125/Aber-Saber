using System.Runtime.InteropServices;

namespace System.Web.Services.Interop;

[ComImport]
[Guid("26E7F0F1-B49C-48cb-B43E-78DCD577E1D9")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface INotifySource2
{
	void SetNotifyFilter([In] NotifyFilter in_NotifyFilter, [In] UserThread in_pUserThreadFilter);
}
