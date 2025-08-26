namespace System.Windows.Forms.VisualStyles;

/// <summary>Identifies the Boolean properties of a visual style element.</summary>
public enum BooleanProperty
{
	/// <summary>The image has transparent areas.</summary>
	Transparent = 2201,
	/// <summary>The width of nonclient captions varies with the extent of the text.</summary>
	AutoSize,
	/// <summary>Only the border of an image is drawn.</summary>
	BorderOnly,
	/// <summary>The control will handle composite drawing.</summary>
	Composited,
	/// <summary>The background of a fixed-size element is a filled rectangle.</summary>
	BackgroundFill,
	/// <summary>The glyph has transparent areas.</summary>
	GlyphTransparent,
	/// <summary>Only the glyph should be drawn, not the background.</summary>
	GlyphOnly,
	/// <summary>The sizing handle will always be displayed.</summary>
	AlwaysShowSizingBar,
	/// <summary>The image is mirrored in right-to-left display modes.</summary>
	MirrorImage,
	/// <summary>The height and width must be sized equally.</summary>
	UniformSizing,
	/// <summary>The scaling factor must be an integer for fixed-size elements.</summary>
	IntegralSizing,
	/// <summary>The source image will scale larger when needed.</summary>
	SourceGrow,
	/// <summary>The source image will scale smaller when needed.</summary>
	SourceShrink
}
