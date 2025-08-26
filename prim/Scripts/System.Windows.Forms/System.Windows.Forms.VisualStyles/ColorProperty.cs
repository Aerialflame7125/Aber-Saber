namespace System.Windows.Forms.VisualStyles;

/// <summary>Identifies the color properties of a visual style element.</summary>
public enum ColorProperty
{
	/// <summary>The border color of an element with a filled-border background.</summary>
	BorderColor = 3801,
	/// <summary>The fill color of an element with a filled-border background.</summary>
	FillColor,
	/// <summary>The text color.</summary>
	TextColor,
	/// <summary>The light color for edges.</summary>
	EdgeLightColor,
	/// <summary>The highlight color for edges.</summary>
	EdgeHighlightColor,
	/// <summary>The shadow color for edges.</summary>
	EdgeShadowColor,
	/// <summary>The dark shadow color for edges.</summary>
	EdgeDarkShadowColor,
	/// <summary>The fill color for edges.</summary>
	EdgeFillColor,
	/// <summary>The color of pixels that are treated as transparent.</summary>
	TransparentColor,
	/// <summary>The first color in a gradient.</summary>
	GradientColor1,
	/// <summary>The second color in a gradient.</summary>
	GradientColor2,
	/// <summary>The third color in a gradient.</summary>
	GradientColor3,
	/// <summary>The fourth color in a gradient.</summary>
	GradientColor4,
	/// <summary>The fifth color in a gradient.</summary>
	GradientColor5,
	/// <summary>The color of the shadow.</summary>
	ShadowColor,
	/// <summary>The glow color.</summary>
	GlowColor,
	/// <summary>The color of the text border.</summary>
	TextBorderColor,
	/// <summary>The color of the text shadow.</summary>
	TextShadowColor,
	/// <summary>The color that a font-based glyph is drawn with.</summary>
	GlyphTextColor,
	/// <summary>The color of pixels in a glyph that are treated as transparent.</summary>
	GlyphTransparentColor,
	/// <summary>A recommended companion color for the fill color of the visual style.</summary>
	FillColorHint,
	/// <summary>A recommended companion color for the border color of the visual style.</summary>
	BorderColorHint,
	/// <summary>A recommended companion color for the accent color of the visual style.</summary>
	AccentColorHint
}
