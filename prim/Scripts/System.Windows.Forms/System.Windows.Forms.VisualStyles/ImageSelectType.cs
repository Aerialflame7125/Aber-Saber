namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies when the visual style selects a different multiple-image file to draw an element.</summary>
public enum ImageSelectType
{
	/// <summary>The image file does not change.</summary>
	None,
	/// <summary>Image file changes are based on size settings.</summary>
	Size,
	/// <summary>Image file changes are based on dots per inch (DPI) settings.</summary>
	Dpi
}
