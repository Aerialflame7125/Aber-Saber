using System;
using System.Windows.Forms;

namespace Ookii.Dialogs.Interop;

internal class WindowHandleWrapper : IWin32Window
{
	private IntPtr _handle;

	public IntPtr Handle => _handle;

	public WindowHandleWrapper(IntPtr handle)
	{
		_handle = handle;
	}
}
