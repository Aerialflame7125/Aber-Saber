namespace System.Windows.Forms;

/// <summary>Defines constants that indicate the alignment of content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewContentAlignment
{
	/// <summary>The alignment is not set.</summary>
	NotSet = 0,
	/// <summary>The content is aligned vertically at the top and horizontally at the left of a cell.</summary>
	TopLeft = 1,
	/// <summary>The content is aligned vertically at the top and horizontally at the center of a cell.</summary>
	TopCenter = 2,
	/// <summary>The content is aligned vertically at the top and horizontally at the right of a cell.</summary>
	TopRight = 4,
	/// <summary>The content is aligned vertically at the middle and horizontally at the left of a cell.</summary>
	MiddleLeft = 0x10,
	/// <summary>The content is aligned at the vertical and horizontal center of a cell.</summary>
	MiddleCenter = 0x20,
	/// <summary>The content is aligned vertically at the middle and horizontally at the right of a cell.</summary>
	MiddleRight = 0x40,
	/// <summary>The content is aligned vertically at the bottom and horizontally at the left of a cell.</summary>
	BottomLeft = 0x100,
	/// <summary>The content is aligned vertically at the bottom and horizontally at the center of a cell.</summary>
	BottomCenter = 0x200,
	/// <summary>The content is aligned vertically at the bottom and horizontally at the right of a cell.</summary>
	BottomRight = 0x400
}
