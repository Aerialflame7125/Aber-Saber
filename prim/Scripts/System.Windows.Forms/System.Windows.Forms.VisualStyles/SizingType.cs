namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies how elements with a bitmap background will adjust to fill a bounds.</summary>
public enum SizingType
{
	/// <summary>The element cannot be resized.</summary>
	FixedSize,
	/// <summary>The background image stretches to fill the bounds.</summary>
	Stretch,
	/// <summary>The background image repeats the pattern to fill the bounds.</summary>
	Tile
}
