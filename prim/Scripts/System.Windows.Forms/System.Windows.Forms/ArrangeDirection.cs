using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies the direction in which the system arranges minimized windows.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
[Flags]
public enum ArrangeDirection
{
	/// <summary>Arranged horizontally, from left to right. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomRight" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
	Left = 0,
	/// <summary>Arranged horizontally, from right to left. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopLeft" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
	Right = 0,
	/// <summary>Arranged vertically, from top to bottom. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.TopRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
	Down = 4,
	/// <summary>Arranged vertically, from bottom to top. Valid with the <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomLeft" /> and <see cref="F:System.Windows.Forms.ArrangeStartingPosition.BottomRight" /><see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> enumeration values.</summary>
	Up = 4
}
