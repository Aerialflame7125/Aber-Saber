using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies constants that define which mouse button was pressed.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
[ComVisible(true)]
public enum MouseButtons
{
	/// <summary>No mouse button was pressed.</summary>
	None = 0,
	/// <summary>The left mouse button was pressed.</summary>
	Left = 0x100000,
	/// <summary>The right mouse button was pressed.</summary>
	Right = 0x200000,
	/// <summary>The middle mouse button was pressed.</summary>
	Middle = 0x400000,
	/// <summary>The first XButton was pressed.</summary>
	XButton1 = 0x800000,
	/// <summary>The second XButton was pressed.</summary>
	XButton2 = 0x1000000
}
