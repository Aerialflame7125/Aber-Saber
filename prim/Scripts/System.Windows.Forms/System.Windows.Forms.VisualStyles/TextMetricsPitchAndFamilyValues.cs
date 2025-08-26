namespace System.Windows.Forms.VisualStyles;

/// <summary>Specifies information about the pitch, technology, and family of the font specified by a visual style for a particular element. </summary>
[Flags]
public enum TextMetricsPitchAndFamilyValues
{
	/// <summary>If this value is set, the font is a variable pitch font. Otherwise, the font is a fixed-pitch font. Note that the behavior of this value is opposite of what the name implies.</summary>
	FixedPitch = 1,
	/// <summary>The font is a vector font.</summary>
	Vector = 2,
	/// <summary>The font is a TrueType font.</summary>
	TrueType = 4,
	/// <summary>The font is a device font.</summary>
	Device = 8
}
