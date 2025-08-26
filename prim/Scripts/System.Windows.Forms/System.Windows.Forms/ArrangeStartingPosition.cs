namespace System.Windows.Forms;

/// <summary>Specifies the starting position that the system uses to arrange minimized windows.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum ArrangeStartingPosition
{
	/// <summary>Starts at the lower-left corner of the screen, which is the default position.</summary>
	BottomLeft = 0,
	/// <summary>Starts at the lower-right corner of the screen.</summary>
	BottomRight = 1,
	/// <summary>Starts at the upper-left corner of the screen.</summary>
	TopLeft = 2,
	/// <summary>Starts at the upper-right corner of the screen.</summary>
	TopRight = 3,
	/// <summary>Hides minimized windows by moving them off the visible area of the screen.</summary>
	Hide = 8
}
