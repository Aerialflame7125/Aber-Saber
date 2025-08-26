using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a display device or multiple display devices on a single system.</summary>
/// <filterpriority>2</filterpriority>
public class Screen
{
	private static Screen[] all_screens = new Screen[1]
	{
		new Screen(primary: true, "Mono MWF Primary Display", SystemInformation.VirtualScreen, SystemInformation.WorkingArea)
	};

	private bool primary;

	private Rectangle bounds;

	private Rectangle workarea;

	private string name;

	private int bits_per_pixel;

	/// <summary>Gets an array of all displays on the system.</summary>
	/// <returns>An array of type <see cref="T:System.Windows.Forms.Screen" />, containing all displays on the system.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Screen[] AllScreens => all_screens;

	/// <summary>Gets the primary display.</summary>
	/// <returns>The primary display.</returns>
	/// <filterpriority>1</filterpriority>
	public static Screen PrimaryScreen => all_screens[0];

	/// <summary>Gets the number of bits of memory, associated with one pixel of data.</summary>
	/// <returns>The number of bits of memory, associated with one pixel of data </returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Stub, always returns 32")]
	public int BitsPerPixel => bits_per_pixel;

	/// <summary>Gets the bounds of the display.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the bounds of the display.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets the device name associated with a display.</summary>
	/// <returns>The device name associated with a display.</returns>
	/// <filterpriority>1</filterpriority>
	public string DeviceName => name;

	/// <summary>Gets a value indicating whether a particular display is the primary device.</summary>
	/// <returns>true if this display is primary; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool Primary => primary;

	/// <summary>Gets the working area of the display. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" />, representing the working area of the display.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle WorkingArea => workarea;

	private Screen()
	{
		primary = true;
		bounds = SystemInformation.WorkingArea;
	}

	private Screen(bool primary, string name, Rectangle bounds, Rectangle workarea)
	{
		this.primary = primary;
		this.name = name;
		this.bounds = bounds;
		this.workarea = workarea;
		bits_per_pixel = 32;
	}

	/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the specified control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified control. In multiple display environments where no display contains the control, the display closest to the specified control is returned.</returns>
	/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Screen FromControl(Control control)
	{
		return FromPoint(control.Location);
	}

	/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the object referred to by the specified handle.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the object. In multiple display environments where no display contains any portion of the specified window, the display closest to the object is returned.</returns>
	/// <param name="hwnd">The window handle for which to retrieve the <see cref="T:System.Windows.Forms.Screen" />. </param>
	/// <filterpriority>1</filterpriority>
	public static Screen FromHandle(IntPtr hwnd)
	{
		Control control = Control.FromHandle(hwnd);
		if (control != null)
		{
			return FromPoint(control.Location);
		}
		return PrimaryScreen;
	}

	/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the specified point.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the point. In multiple display environments where no display contains the point, the display closest to the specified point is returned.</returns>
	/// <param name="point">A <see cref="T:System.Drawing.Point" /> that specifies the location for which to retrieve a <see cref="T:System.Windows.Forms.Screen" />. </param>
	/// <filterpriority>1</filterpriority>
	public static Screen FromPoint(Point point)
	{
		for (int i = 0; i < all_screens.Length; i++)
		{
			if (all_screens[i].Bounds.Contains(point))
			{
				return all_screens[i];
			}
		}
		return PrimaryScreen;
	}

	/// <summary>Retrieves a <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest portion of the rectangle.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Screen" /> for the display that contains the largest region of the specified rectangle. In multiple display environments where no display contains the rectangle, the display closest to the rectangle is returned.</returns>
	/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display. </param>
	/// <filterpriority>1</filterpriority>
	public static Screen FromRectangle(Rectangle rect)
	{
		return FromPoint(new Point(rect.Left, rect.Top));
	}

	/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified control. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
	/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the display bounds. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetBounds(Control ctl)
	{
		return FromControl(ctl).Bounds;
	}

	/// <summary>Retrieves the bounds of the display that contains the specified point.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified point. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the display bounds. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetBounds(Point pt)
	{
		return FromPoint(pt).Bounds;
	}

	/// <summary>Retrieves the bounds of the display that contains the largest portion of the specified rectangle.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounds of the display that contains the specified rectangle. In multiple display environments where no monitor contains the specified rectangle, the monitor closest to the rectangle is returned.</returns>
	/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetBounds(Rectangle rect)
	{
		return FromRectangle(rect).Bounds;
	}

	/// <summary>Retrieves the working area for the display that contains the largest region of the specified control. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified control, the display closest to the control is returned.</returns>
	/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the working area. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetWorkingArea(Control ctl)
	{
		return FromControl(ctl).WorkingArea;
	}

	/// <summary>Retrieves the working area closest to the specified point. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified point, the display closest to the point is returned.</returns>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates for which to retrieve the working area. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetWorkingArea(Point pt)
	{
		return FromPoint(pt).WorkingArea;
	}

	/// <summary>Retrieves the working area for the display that contains the largest portion of the specified rectangle. The working area is the desktop area of the display, excluding taskbars, docked windows, and docked tool bars.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the working area. In multiple display environments where no display contains the specified rectangle, the display closest to the rectangle is returned.</returns>
	/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the working area. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static Rectangle GetWorkingArea(Rectangle rect)
	{
		return FromRectangle(rect).WorkingArea;
	}

	/// <summary>Gets or sets a value indicating whether the specified object is equal to this Screen.</summary>
	/// <returns>true if the specified object is equal to this <see cref="T:System.Windows.Forms.Screen" />; otherwise, false.</returns>
	/// <param name="obj">The object to compare to this <see cref="T:System.Windows.Forms.Screen" />. </param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object obj)
	{
		if (obj is Screen)
		{
			Screen screen = (Screen)obj;
			if (name.Equals(screen.name) && primary == screen.primary && bounds.Equals(screen.Bounds) && workarea.Equals(screen.workarea))
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Computes and retrieves a hash code for an object.</summary>
	/// <returns>A hash code for an object.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Retrieves a string representing this object.</summary>
	/// <returns>A string representation of the object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return string.Concat("Screen[Bounds={", Bounds, "} WorkingArea={", WorkingArea, "} Primary={", Primary, "} DeviceName=", DeviceName);
	}
}
