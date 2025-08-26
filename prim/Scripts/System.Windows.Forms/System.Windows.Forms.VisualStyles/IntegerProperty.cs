namespace System.Windows.Forms.VisualStyles;

/// <summary>Identifies the integer properties of a visual style element.</summary>
public enum IntegerProperty
{
	/// <summary>The number of state images in multiple-image file.</summary>
	ImageCount = 2401,
	/// <summary>The alpha value for an icon, between 0 and 255.</summary>
	AlphaLevel,
	/// <summary>The size of the border line for elements with a filled-border background.</summary>
	BorderSize,
	/// <summary>A percentage value that represents the width of a rounded corner, from 0 to 100.</summary>
	RoundCornerWidth,
	/// <summary>A percentage value that represents the height of a rounded corner, from 0 to 100.</summary>
	RoundCornerHeight,
	/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor1" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
	GradientRatio1,
	/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor2" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
	GradientRatio2,
	/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor3" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
	GradientRatio3,
	/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor4" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
	GradientRatio4,
	/// <summary>The amount of <see cref="F:System.Windows.Forms.VisualStyles.ColorProperty.GradientColor5" />  to use in a color gradient. The sum of the five GradientRatio properties must equal 255.</summary>
	GradientRatio5,
	/// <summary>The size of progress bar elements.</summary>
	ProgressChunkSize,
	/// <summary>The size of spaces between progress bar elements.</summary>
	ProgressSpaceSize,
	/// <summary>The amount of saturation for an image, between 0 and 255.</summary>
	Saturation,
	/// <summary>The size of the border around text characters.</summary>
	TextBorderSize,
	/// <summary>The minimum alpha value of a solid pixel, between 0 and 255.</summary>
	AlphaThreshold,
	/// <summary>The width of an element.</summary>
	Width,
	/// <summary>The height of an element. </summary>
	Height,
	/// <summary>The index into the font for font-based glyphs.</summary>
	GlyphIndex,
	/// <summary>A percentage value indicating how far a fixed-size element will stretch when the target exceeds the source. </summary>
	TrueSizeStretchMark,
	/// <summary>The minimum dots per inch (DPI) that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile1" /> was designed for.</summary>
	MinDpi1,
	/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile2" /> was designed for.</summary>
	MinDpi2,
	/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile3" /> was designed for.</summary>
	MinDpi3,
	/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile4" /> was designed for.</summary>
	MinDpi4,
	/// <summary>The minimum DPI that <see cref="F:System.Windows.Forms.VisualStyles.FilenameProperty.ImageFile5" /> was designed for.</summary>
	MinDpi5
}
