namespace System.Windows.Forms;

/// <summary>Specifies the bounds of the control to use when defining a control's size and position.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum BoundsSpecified
{
	/// <summary>No bounds are specified.</summary>
	None = 0,
	/// <summary>The left edge of the control is defined.</summary>
	X = 1,
	/// <summary>The top edge of the control is defined.</summary>
	Y = 2,
	/// <summary>Both X and Y coordinates of the control are defined.</summary>
	Location = 3,
	/// <summary>The width of the control is defined.</summary>
	Width = 4,
	/// <summary>The height of the control is defined.</summary>
	Height = 8,
	/// <summary>Both <see cref="P:System.Windows.Forms.Control.Width" /> and <see cref="P:System.Windows.Forms.Control.Height" /> property values of the control are defined.</summary>
	Size = 0xC,
	/// <summary>Both <see cref="P:System.Windows.Forms.Control.Location" /> and <see cref="P:System.Windows.Forms.Control.Size" /> property values are defined.</summary>
	All = 0xF
}
