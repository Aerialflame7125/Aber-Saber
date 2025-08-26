using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms.Design;

internal class Native
{
	public enum Msg
	{
		WM_CREATE = 1,
		WM_SETFOCUS = 7,
		WM_PAINT = 15,
		WM_CANCELMODE = 31,
		WM_SETCURSOR = 32,
		WM_CONTEXTMENU = 123,
		WM_NCHITTEST = 132,
		WM_GETOBJECT = 61,
		WM_MOUSEFIRST = 512,
		WM_MOUSEMOVE = 512,
		WM_LBUTTONDOWN = 513,
		WM_LBUTTONUP = 514,
		WM_LBUTTONDBLCLK = 515,
		WM_RBUTTONDOWN = 516,
		WM_RBUTTONUP = 517,
		WM_RBUTTONDBLCLK = 518,
		WM_MBUTTONDOWN = 519,
		WM_MBUTTONUP = 520,
		WM_MBUTTONDBLCLK = 521,
		WM_MOUSEWHEEL = 522,
		WM_MOUSELAST = 522,
		WM_NCMOUSEHOVER = 672,
		WM_MOUSEHOVER = 673,
		WM_NCMOUSELEAVE = 674,
		WM_MOUSELEAVE = 675,
		WM_NCMOUSEMOVE = 160,
		WM_NCLBUTTONDOWN = 161,
		WM_NCLBUTTONUP = 162,
		WM_NCLBUTTONDBLCLK = 163,
		WM_NCRBUTTONDOWN = 164,
		WM_NCRBUTTONUP = 165,
		WM_NCRBUTTONDBLCLK = 166,
		WM_NCMBUTTONDOWN = 167,
		WM_NCMBUTTONUP = 168,
		WM_NCMBUTTONDBLCLK = 169,
		WM_KEYFIRST = 256,
		WM_KEYDOWN = 256,
		WM_KEYUP = 257,
		WM_CHAR = 258,
		WM_DEADCHAR = 259,
		WM_SYSKEYDOWN = 260,
		WM_SYSKEYUP = 261,
		WM_SYS1CHAR = 262,
		WM_SYSDEADCHAR = 263,
		WM_KEYLAST = 264,
		WM_HSCROLL = 276,
		WM_VSCROLL = 277,
		WM_IME_SETCONTEXT = 641,
		WM_IME_NOTIFY = 642,
		WM_IME_CONTROL = 643,
		WM_IME_COMPOSITIONFULL = 644,
		WM_IME_SELECT = 645,
		WM_IME_CHAR = 646,
		WM_IME_REQUEST = 648,
		WM_IME_KEYDOWN = 656,
		WM_IME_KEYUP = 657,
		WM_MOUSE_ENTER = 1025
	}

	private static Type _xplatuiType;

	static Native()
	{
		Assembly assembly = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
		if (assembly == null)
		{
			throw new InvalidOperationException("Can't load System.Windows.Forms assembly.");
		}
		_xplatuiType = assembly.GetType("System.Windows.Forms.XplatUI");
		if (_xplatuiType == null)
		{
			throw new InvalidOperationException("Can't find the System.Windows.Forms.XplatUI type.");
		}
	}

	private static object InvokeMethod(string methodName, object[] args)
	{
		return InvokeMethod(methodName, args, null);
	}

	private static object InvokeMethod(string methodName, object[] args, Type[] types)
	{
		MethodInfo methodInfo = null;
		methodInfo = ((types == null) ? _xplatuiType.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod) : _xplatuiType.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, types, null));
		if (methodInfo == null)
		{
			throw new InvalidOperationException(methodName + " not found!");
		}
		return methodInfo.Invoke(null, args);
	}

	public static void DefWndProc(ref Message m)
	{
		object[] array = new object[1] { m };
		m.Result = (IntPtr)InvokeMethod("DefWndProc", array);
		m = (Message)array[0];
	}

	public static IntPtr SendMessage(IntPtr hwnd, Msg message, IntPtr wParam, IntPtr lParam)
	{
		Type type = Assembly.Load("System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089").GetType("System.Windows.Forms.Message&");
		object[] array = new object[1] { Message.Create(hwnd, (int)message, wParam, lParam) };
		InvokeMethod("SendMessage", array, new Type[1] { type });
		return ((Message)array[0]).Result;
	}

	public static Point PointToClient(Control control, Point point)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		object[] array = new object[3] { control.Handle, point.X, point.Y };
		InvokeMethod("ScreenToClient", array);
		return new Point((int)array[1], (int)array[2]);
	}

	public static IntPtr SetParent(IntPtr childHandle, IntPtr parentHandle)
	{
		return (IntPtr)InvokeMethod("SetParent", new object[2] { childHandle, parentHandle });
	}

	public static int HiWord(int dword)
	{
		return (dword >> 16) & 0xFFFF;
	}

	public static int LoWord(int dword)
	{
		return dword & 0xFFFF;
	}

	public static IntPtr LParam(int hiword, int loword)
	{
		return (IntPtr)((loword << 16) | (hiword & 0xFFFF));
	}
}
