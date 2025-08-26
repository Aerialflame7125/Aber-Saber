using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Implements a Windows message.</summary>
/// <filterpriority>2</filterpriority>
public struct Message
{
	private int msg;

	private IntPtr hwnd;

	private IntPtr lParam;

	private IntPtr wParam;

	private IntPtr result;

	/// <summary>Gets or sets the window handle of the message.</summary>
	/// <returns>The window handle of the message.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public IntPtr HWnd
	{
		get
		{
			return hwnd;
		}
		set
		{
			hwnd = value;
		}
	}

	/// <summary>Specifies the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</summary>
	/// <returns>The <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public IntPtr LParam
	{
		get
		{
			return lParam;
		}
		set
		{
			lParam = value;
		}
	}

	/// <summary>Gets or sets the ID number for the message.</summary>
	/// <returns>The ID number for the message.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public int Msg
	{
		get
		{
			return msg;
		}
		set
		{
			msg = value;
		}
	}

	/// <summary>Specifies the value that is returned to Windows in response to handling the message.</summary>
	/// <returns>The return value of the message.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public IntPtr Result
	{
		get
		{
			return result;
		}
		set
		{
			result = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</summary>
	/// <returns>The <see cref="P:System.Windows.Forms.Message.WParam" /> field of the message.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public IntPtr WParam
	{
		get
		{
			return wParam;
		}
		set
		{
			wParam = value;
		}
	}

	/// <summary>Creates a new <see cref="T:System.Windows.Forms.Message" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Message" /> that represents the message that was created.</returns>
	/// <param name="hWnd">The window handle that the message is for. </param>
	/// <param name="msg">The message ID. </param>
	/// <param name="wparam">The message <paramref name="wparam" /> field. </param>
	/// <param name="lparam">The message <paramref name="lparam" /> field. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public static Message Create(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
	{
		Message message = default(Message);
		message.msg = msg;
		message.hwnd = hWnd;
		message.wParam = wparam;
		message.lParam = lparam;
		return message;
	}

	/// <summary>Determines whether the specified object is equal to the current object.</summary>
	/// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
	/// <param name="o">The object to compare with the current object.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public override bool Equals(object o)
	{
		if (!(o is Message))
		{
			return false;
		}
		return msg == ((Message)o).msg && hwnd == ((Message)o).hwnd && lParam == ((Message)o).lParam && wParam == ((Message)o).wParam && result == ((Message)o).result;
	}

	/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Gets the <see cref="P:System.Windows.Forms.Message.LParam" /> value and converts the value to an object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents an instance of the class specified by the <paramref name="cls" /> parameter, with the data from the <see cref="P:System.Windows.Forms.Message.LParam" /> field of the message.</returns>
	/// <param name="cls">The type to use to create an instance. This type must be declared as a structure type. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public object GetLParam(Type cls)
	{
		return Marshal.PtrToStructure(lParam, cls);
	}

	/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Message" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
	/// </PermissionSet>
	public override string ToString()
	{
		return $"msg=0x{msg:x} ({((Msg)msg).ToString()}) hwnd=0x{hwnd.ToInt32():x} wparam=0x{wParam.ToInt32():x} lparam=0x{lParam.ToInt32():x} result=0x{result.ToInt32():x}";
	}

	/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are equal. </summary>
	/// <returns>true if <paramref name="a" /> and <paramref name="b" /> represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, false. </returns>
	/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
	public static bool operator ==(Message a, Message b)
	{
		return a.hwnd == b.hwnd && a.lParam == b.lParam && a.msg == b.msg && a.result == b.result && a.wParam == b.wParam;
	}

	/// <summary>Determines whether two instances of <see cref="T:System.Windows.Forms.Message" /> are not equal. </summary>
	/// <returns>true if <paramref name="a" /> and <paramref name="b" /> do not represent the same <see cref="T:System.Windows.Forms.Message" />; otherwise, false. </returns>
	/// <param name="a">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="b" />.</param>
	/// <param name="b">A <see cref="T:System.Windows.Forms.Message" /> to compare to <paramref name="a" />.</param>
	public static bool operator !=(Message a, Message b)
	{
		return !(a == b);
	}
}
