namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies which edges of a visual style element to draw.</summary>
[Flags]
public enum Edges
{
	/// <summary>The left edge of the element.</summary>
	Left = 1,
	/// <summary>The top edge of the element.</summary>
	Top = 2,
	/// <summary>The right edge of the element.</summary>
	Right = 4,
	/// <summary>The bottom edge of the element.</summary>
	Bottom = 8,
	/// <summary>A diagonal border.</summary>
	Diagonal = 0x10
}
