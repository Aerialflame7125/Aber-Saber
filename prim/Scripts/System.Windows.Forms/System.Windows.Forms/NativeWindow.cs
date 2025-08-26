using System.Collections;

namespace System.Windows.Forms;

/// <summary>Provides a low-level encapsulation of a window handle and a window procedure.</summary>
/// <filterpriority>2</filterpriority>
public class NativeWindow : MarshalByRefObject, IWin32Window
{
	private IntPtr window_handle = IntPtr.Zero;

	private static Hashtable window_collection = new Hashtable();

	[ThreadStatic]
	private static NativeWindow WindowCreating;

	/// <summary>Gets the handle for this window. </summary>
	/// <returns>If successful, an <see cref="T:System.IntPtr" /> representing the handle to the associated native Win32 window; otherwise, 0 if no handle is associated with the window.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public IntPtr Handle => window_handle;

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.NativeWindow" /> class.</summary>
	public NativeWindow()
	{
		window_handle = IntPtr.Zero;
	}

	/// <summary>Retrieves the window associated with the specified handle. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.NativeWindow" /> associated with the specified handle. This method returns null when the handle does not have an associated window.</returns>
	/// <param name="handle">A handle to a window. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public static NativeWindow FromHandle(IntPtr handle)
	{
		return FindFirstInTable(handle);
	}

	internal void InvalidateHandle()
	{
		RemoveFromTable(this);
		window_handle = IntPtr.Zero;
	}

	/// <summary>Assigns a handle to this window. </summary>
	/// <param name="handle">The handle to assign to this window. </param>
	/// <exception cref="T:System.Exception">This window already has a handle. </exception>
	/// <exception cref="T:System.ComponentModel.Win32Exception">The windows procedure for the associated native window could not be retrieved.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AssignHandle(IntPtr handle)
	{
		RemoveFromTable(this);
		window_handle = handle;
		AddToTable(this);
		OnHandleChange();
	}

	private static void AddToTable(NativeWindow window)
	{
		IntPtr handle = window.Handle;
		if (handle == IntPtr.Zero)
		{
			return;
		}
		lock (window_collection)
		{
			object obj = window_collection[handle];
			if (obj == null)
			{
				window_collection.Add(handle, window);
			}
			else if (obj is NativeWindow nativeWindow)
			{
				if (nativeWindow != window)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(nativeWindow);
					arrayList.Add(window);
					window_collection[handle] = arrayList;
				}
			}
			else
			{
				ArrayList arrayList2 = (ArrayList)window_collection[handle];
				if (!arrayList2.Contains(window))
				{
					arrayList2.Add(window);
				}
			}
		}
	}

	private static void RemoveFromTable(NativeWindow window)
	{
		IntPtr handle = window.Handle;
		if (handle == IntPtr.Zero)
		{
			return;
		}
		lock (window_collection)
		{
			object obj = window_collection[handle];
			if (obj == null)
			{
				return;
			}
			if (obj is NativeWindow)
			{
				window_collection.Remove(handle);
				return;
			}
			ArrayList arrayList = (ArrayList)window_collection[handle];
			arrayList.Remove(window);
			if (arrayList.Count == 0)
			{
				window_collection.Remove(handle);
			}
			else if (arrayList.Count == 1)
			{
				window_collection[handle] = arrayList[0];
			}
		}
	}

	private static NativeWindow FindFirstInTable(IntPtr handle)
	{
		if (handle == IntPtr.Zero)
		{
			return null;
		}
		NativeWindow nativeWindow = null;
		lock (window_collection)
		{
			object obj = window_collection[handle];
			if (obj != null)
			{
				nativeWindow = obj as NativeWindow;
				if (nativeWindow == null)
				{
					ArrayList arrayList = (ArrayList)obj;
					if (arrayList.Count > 0)
					{
						nativeWindow = (NativeWindow)arrayList[0];
					}
				}
			}
		}
		return nativeWindow;
	}

	/// <summary>Creates a window and its handle with the specified creation parameters. </summary>
	/// <param name="cp">A <see cref="T:System.Windows.Forms.CreateParams" /> that specifies the creation parameters for this window. </param>
	/// <exception cref="T:System.OutOfMemoryException">The operating system ran out of resources when trying to create the native window.</exception>
	/// <exception cref="T:System.ComponentModel.Win32Exception">The native Win32 API could not create the specified window. </exception>
	/// <exception cref="T:System.InvalidOperationException">The handle of the current native window is already assigned; in explanation, the <see cref="P:System.Windows.Forms.NativeWindow.Handle" /> property is not equal to <see cref="F:System.IntPtr.Zero" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void CreateHandle(CreateParams cp)
	{
		if (cp != null)
		{
			WindowCreating = this;
			window_handle = XplatUI.CreateWindow(cp);
			WindowCreating = null;
			if (window_handle != IntPtr.Zero)
			{
				AddToTable(this);
			}
		}
	}

	/// <summary>Invokes the default window procedure associated with this window. </summary>
	/// <param name="m">The message that is currently being processed. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public void DefWndProc(ref Message m)
	{
		m.Result = XplatUI.DefWndProc(ref m);
	}

	/// <summary>Destroys the window and its handle. </summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual void DestroyHandle()
	{
		if (window_handle != IntPtr.Zero)
		{
			XplatUI.DestroyWindow(window_handle);
		}
	}

	/// <summary>Releases the handle associated with this window. </summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public virtual void ReleaseHandle()
	{
		RemoveFromTable(this);
		window_handle = IntPtr.Zero;
		OnHandleChange();
	}

	/// <summary>Releases the resources associated with this window. </summary>
	~NativeWindow()
	{
	}

	/// <summary>Specifies a notification method that is called when the handle for a window is changed. </summary>
	protected virtual void OnHandleChange()
	{
	}

	/// <summary>When overridden in a derived class, manages an unhandled thread exception. </summary>
	/// <param name="e">An <see cref="T:System.Exception" /> that specifies the unhandled thread exception. </param>
	protected virtual void OnThreadException(Exception e)
	{
		Application.OnThreadException(e);
	}

	/// <summary>Invokes the default window procedure associated with this window. </summary>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that is associated with the current Windows message. </param>
	protected virtual void WndProc(ref Message m)
	{
		DefWndProc(ref m);
	}

	internal static IntPtr WndProc(IntPtr hWnd, Msg msg, IntPtr wParam, IntPtr lParam)
	{
		IntPtr result = IntPtr.Zero;
		Message msg2 = default(Message);
		msg2.HWnd = hWnd;
		msg2.Msg = (int)msg;
		msg2.WParam = wParam;
		msg2.LParam = lParam;
		msg2.Result = IntPtr.Zero;
		NativeWindow nativeWindow = null;
		try
		{
			object obj = null;
			lock (window_collection)
			{
				obj = window_collection[hWnd];
			}
			nativeWindow = obj as NativeWindow;
			if (obj == null)
			{
				nativeWindow = EnsureCreated(nativeWindow, hWnd);
			}
			if (nativeWindow != null)
			{
				nativeWindow.WndProc(ref msg2);
				result = msg2.Result;
			}
			else if (obj is ArrayList)
			{
				ArrayList arrayList = (ArrayList)obj;
				lock (arrayList)
				{
					if (arrayList.Count > 0)
					{
						nativeWindow = EnsureCreated((NativeWindow)arrayList[0], hWnd);
						nativeWindow.WndProc(ref msg2);
						result = msg2.Result;
						for (int i = 1; i < arrayList.Count; i++)
						{
							((NativeWindow)arrayList[i]).WndProc(ref msg2);
						}
					}
				}
			}
			else
			{
				result = XplatUI.DefWndProc(ref msg2);
			}
		}
		catch (Exception e)
		{
			nativeWindow?.OnThreadException(e);
		}
		return result;
	}

	private static NativeWindow EnsureCreated(NativeWindow window, IntPtr hWnd)
	{
		if (window == null && WindowCreating != null)
		{
			window = WindowCreating;
			WindowCreating = null;
			if (window.Handle == IntPtr.Zero)
			{
				window.AssignHandle(hWnd);
			}
		}
		return window;
	}
}
