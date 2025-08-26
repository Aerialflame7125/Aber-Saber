namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies when the visual style selects a different glyph font size.</summary>
public enum GlyphFontSizingType
{
	/// <summary>Glyph font sizes do not change.</summary>
	None,
	/// <summary>Glyph font size changes are based on font size settings.</summary>
	Size,
	/// <summary>Glyph font size changes are based on dots per inch (DPI) settings.</summary>
	Dpi
}
