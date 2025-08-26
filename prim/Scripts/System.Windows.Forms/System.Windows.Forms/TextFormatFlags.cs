namespace System.Windows.Forms;

/// <summary>Specifies the display and layout information for text strings.</summary>
[Flags]
public enum TextFormatFlags
{
	/// <summary>Aligns the text on the left side of the clipping area.</summary>
	Left = 0,
	/// <summary>Aligns the text on the top of the bounding rectangle.</summary>
	Top = 0,
	/// <summary>Applies the default formatting, which is left-aligned.</summary>
	Default = 0,
	/// <summary>Adds padding to the bounding rectangle to accommodate overhanging glyphs. </summary>
	GlyphOverhangPadding = 0,
	/// <summary>Centers the text horizontally within the bounding rectangle.</summary>
	HorizontalCenter = 1,
	/// <summary>Aligns the text on the right side of the clipping area.</summary>
	Right = 2,
	/// <summary>Centers the text vertically, within the bounding rectangle.</summary>
	VerticalCenter = 4,
	/// <summary>Aligns the text on the bottom of the bounding rectangle. Applied only when the text is a single line.</summary>
	Bottom = 8,
	/// <summary>Breaks the text at the end of a word.</summary>
	WordBreak = 0x10,
	/// <summary>Displays the text in a single line.</summary>
	SingleLine = 0x20,
	/// <summary>Expands tab characters. The default number of characters per tab is eight. The <see cref="F:System.Windows.Forms.TextFormatFlags.WordEllipsis" />, <see cref="F:System.Windows.Forms.TextFormatFlags.PathEllipsis" />, and <see cref="F:System.Windows.Forms.TextFormatFlags.EndEllipsis" /> values cannot be used with <see cref="F:System.Windows.Forms.TextFormatFlags.ExpandTabs" />.</summary>
	ExpandTabs = 0x40,
	/// <summary>Allows the overhanging parts of glyphs and unwrapped text reaching outside the formatting rectangle to show.</summary>
	NoClipping = 0x100,
	/// <summary>Includes the font external leading in line height. Typically, external leading is not included in the height of a line of text.</summary>
	ExternalLeading = 0x200,
	/// <summary>Turns off processing of prefix characters. Typically, the ampersand (&amp;) mnemonic-prefix character is interpreted as a directive to underscore the character that follows, and the double-ampersand (&amp;&amp;) mnemonic-prefix characters as a directive to print a single ampersand. By specifying <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" />, this processing is turned off. For example, an input string of "A&amp;bc&amp;&amp;d" with <see cref="F:System.Windows.Forms.TextFormatFlags.NoPrefix" /> applied would result in output of "A&amp;bc&amp;&amp;d".</summary>
	NoPrefix = 0x800,
	/// <summary>Uses the system font to calculate text metrics.</summary>
	Internal = 0x1000,
	/// <summary>Specifies the text should be formatted for display on a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
	TextBoxControl = 0x2000,
	/// <summary>Removes the center of trimmed lines and replaces it with an ellipsis. </summary>
	PathEllipsis = 0x4000,
	/// <summary>Removes the end of trimmed lines, and replaces them with an ellipsis.</summary>
	EndEllipsis = 0x8000,
	/// <summary>Has no effect on the drawn text.</summary>
	ModifyString = 0x10000,
	/// <summary>Displays the text from right to left.</summary>
	RightToLeft = 0x20000,
	/// <summary>Trims the line to the nearest word and an ellipsis is placed at the end of a trimmed line.</summary>
	WordEllipsis = 0x40000,
	/// <summary>Applies to Windows 98, Windows Me, Windows 2000, or Windows XP only:</summary>
	NoFullWidthCharacterBreak = 0x80000,
	/// <summary>Applies to Windows 2000 and Windows XP only: </summary>
	HidePrefix = 0x100000,
	/// <summary>Applies to Windows 2000 or Windows XP only: </summary>
	PrefixOnly = 0x200000,
	/// <summary>Preserves the clipping specified by a <see cref="T:System.Drawing.Graphics" /> object. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
	PreserveGraphicsClipping = 0x1000000,
	/// <summary>Preserves the transformation specified by a <see cref="T:System.Drawing.Graphics" />. Applies only to methods receiving an <see cref="T:System.Drawing.IDeviceContext" /> that is a <see cref="T:System.Drawing.Graphics" />.</summary>
	PreserveGraphicsTranslateTransform = 0x2000000,
	/// <summary>Does not add padding to the bounding rectangle.</summary>
	NoPadding = 0x10000000,
	/// <summary>Adds padding to both sides of the bounding rectangle.</summary>
	LeftAndRightPadding = 0x20000000
}
