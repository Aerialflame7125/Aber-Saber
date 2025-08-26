namespace System.Windows.Forms.Design.Behavior;

/// <summary>Specifies the orientation and relative location of a snapline.</summary>
public enum SnapLineType
{
	/// <summary>A horizontal snapline typically aligned to the top edge of a control.</summary>
	Top,
	/// <summary>A horizontal snapline typically aligned to the bottom edge of a control.</summary>
	Bottom,
	/// <summary>A vertical snapline typically aligned to the left edge of a control.</summary>
	Left,
	/// <summary>A vertical snapline typically aligned to the right edge of a control.</summary>
	Right,
	/// <summary>A horizontal snapline typically not associated with an edge of a control.</summary>
	Horizontal,
	/// <summary>A vertical snapline typically not associated with an edge of a control.</summary>
	Vertical,
	/// <summary>A horizontal snapline typically associated with a primary internal feature of a control; for example, the base of the text string in a <see cref="T:System.Windows.Forms.Label" /> control.</summary>
	Baseline
}
