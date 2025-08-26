using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides an interface to expose Win32 HWND handles.</summary>
/// <filterpriority>2</filterpriority>
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
[ComVisible(true)]
public interface IWin32Window
{
	/// <summary>Gets the handle to the window represented by the implementer.</summary>
	/// <returns>A handle to the window represented by the implementer.</returns>
	/// <filterpriority>1</filterpriority>
	IntPtr Handle { get; }
}
