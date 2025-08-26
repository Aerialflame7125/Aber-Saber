namespace System.Web.Security.AntiXss;

/// <summary>Specifies values for the middle region of the UTF-8 Unicode code charts, from U1F00 to U2DDF.</summary>
[Flags]
public enum MidCodeCharts : long
{
	/// <summary>None of the UTF-8 Unicode code charts from the middle region are marked as safe.</summary>
	None = 0L,
	/// <summary>The Greek Extended code chart.</summary>
	GreekExtended = 1L,
	/// <summary>The General Punctuation code chart.</summary>
	GeneralPunctuation = 2L,
	/// <summary>The Superscripts and Subscripts code chart.</summary>
	SuperscriptsAndSubscripts = 4L,
	/// <summary>The Currency Symbols code chart.</summary>
	CurrencySymbols = 8L,
	/// <summary>The Combining Diacritical Marks for Symbols code chart.</summary>
	CombiningDiacriticalMarksForSymbols = 0x10L,
	/// <summary>The Letterlike Symbols code chart.</summary>
	LetterlikeSymbols = 0x20L,
	/// <summary>The Number Forms code chart.</summary>
	NumberForms = 0x40L,
	/// <summary>The Arrows code chart.</summary>
	Arrows = 0x80L,
	/// <summary>The Mathematical Operators code chart.</summary>
	MathematicalOperators = 0x100L,
	/// <summary>The Miscellaneous Technical code chart.</summary>
	MiscellaneousTechnical = 0x200L,
	/// <summary>The Control Pictures code chart.</summary>
	ControlPictures = 0x400L,
	/// <summary>The Optical Character Recognition code chart.</summary>
	OpticalCharacterRecognition = 0x800L,
	/// <summary>The Enclosed Alphanumerics code chart.</summary>
	EnclosedAlphanumerics = 0x1000L,
	/// <summary>The Box Drawing code chart.</summary>
	BoxDrawing = 0x2000L,
	/// <summary>The Block Elements code chart.</summary>
	BlockElements = 0x4000L,
	/// <summary>The Geometric Shapes code chart.</summary>
	GeometricShapes = 0x8000L,
	/// <summary>The Miscellaneous Symbols code chart.</summary>
	MiscellaneousSymbols = 0x10000L,
	/// <summary>The Dingbats code chart.</summary>
	Dingbats = 0x20000L,
	/// <summary>The Miscellaneous Mathematical Symbols-A code chart.</summary>
	MiscellaneousMathematicalSymbolsA = 0x40000L,
	/// <summary>The Supplemental Arrows-A code chart.</summary>
	SupplementalArrowsA = 0x80000L,
	/// <summary>The Braille Patterns code chart.</summary>
	BraillePatterns = 0x100000L,
	/// <summary>The Supplemental Arrows-B code chart.</summary>
	SupplementalArrowsB = 0x200000L,
	/// <summary>The Miscellaneous Mathematical Symbols-B code chart.</summary>
	MiscellaneousMathematicalSymbolsB = 0x400000L,
	/// <summary>The Supplemental Mathematical Operators code chart.</summary>
	SupplementalMathematicalOperators = 0x800000L,
	/// <summary>The Miscellaneous Symbols and Arrows code chart.</summary>
	MiscellaneousSymbolsAndArrows = 0x1000000L,
	/// <summary>The Glagolitic code chart.</summary>
	Glagolitic = 0x2000000L,
	/// <summary>The Latin Extended-C code chart.</summary>
	LatinExtendedC = 0x4000000L,
	/// <summary>The Coptic code chart.</summary>
	Coptic = 0x8000000L,
	/// <summary>The Georgian Supplement code chart.</summary>
	GeorgianSupplement = 0x10000000L,
	/// <summary>The Tifinagh code chart.</summary>
	Tifinagh = 0x20000000L,
	/// <summary>The Ethiopic Extended code chart.</summary>
	EthiopicExtended = 0x4000L
}
