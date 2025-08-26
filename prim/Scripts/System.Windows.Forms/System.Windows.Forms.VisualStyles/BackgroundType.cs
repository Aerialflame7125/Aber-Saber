namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies the source of a visual style element's background.</summary>
public enum BackgroundType
{
	/// <summary>The background of the element is a bitmap. If this value is set, then the property corresponding to the <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile" /> value will contain the name of a valid image file.</summary>
	ImageFile,
	/// <summary>The background of the element is a rectangle filled with a color or pattern. </summary>
	BorderFill,
	/// <summary>The element has no background.</summary>
	None
}
