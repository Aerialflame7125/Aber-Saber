using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies the sides of a rectangle to apply a three-dimensional border to.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
[ComVisible(true)]
public enum Border3DSide
{
	/// <summary>A three-dimensional border on the left edge of the rectangle.</summary>
	Left = 1,
	/// <summary>A three-dimensional border on the top edge of the rectangle.</summary>
	Top = 2,
	/// <summary>A three-dimensional border on the right side of the rectangle.</summary>
	Right = 4,
	/// <summary>A three-dimensional border on the bottom side of the rectangle.</summary>
	Bottom = 8,
	/// <summary>The interior of the rectangle is filled with the color defined for three-dimensional controls instead of the background color for the form.</summary>
	Middle = 0x800,
	/// <summary>A three-dimensional border on all four sides of the rectangle. The middle of the rectangle is filled with the color defined for three-dimensional controls.</summary>
	All = 0x80F
}
