namespace System.Web.Security.AntiXss;

/// <summary>Specifies values for the lower region of the UTF-8 Unicode code charts, from U0000 to U0FFF.</summary>
[Flags]
public enum LowerCodeCharts : long
{
	/// <summary>None of the UTF-8 Unicode code charts from the lower region are marked as safe.</summary>
	None = 0L,
	/// <summary>The Basic Latin code chart.</summary>
	BasicLatin = 1L,
	/// <summary>The C1 Controls and Latin-1 Supplement code chart.</summary>
	C1ControlsAndLatin1Supplement = 2L,
	/// <summary>The Latin Extended-A code chart.</summary>
	LatinExtendedA = 4L,
	/// <summary>The Latin Extended-B code chart.</summary>
	LatinExtendedB = 8L,
	/// <summary>The IPA Extensions code chart.</summary>
	IpaExtensions = 0x10L,
	/// <summary>The Spacing Modifier Letters code chart.</summary>
	SpacingModifierLetters = 0x20L,
	/// <summary>The Combining Diacritical Marks code chart.</summary>
	CombiningDiacriticalMarks = 0x40L,
	/// <summary>The Greek and Coptic code chart.</summary>
	GreekAndCoptic = 0x80L,
	/// <summary>The Cyrillic code chart.</summary>
	Cyrillic = 0x100L,
	/// <summary>The Cyrillic Supplement code chart.</summary>
	CyrillicSupplement = 0x200L,
	/// <summary>The Armenian code chart.</summary>
	Armenian = 0x400L,
	/// <summary>The Hebrew code chart.</summary>
	Hebrew = 0x800L,
	/// <summary>The Arabic code chart.</summary>
	Arabic = 0x1000L,
	/// <summary>The Syriac code chart.</summary>
	Syriac = 0x2000L,
	/// <summary>The Arabic Supplement code chart.</summary>
	ArabicSupplement = 0x4000L,
	/// <summary>The Thaana code chart.</summary>
	Thaana = 0x8000L,
	/// <summary>The N'ko code chart.</summary>
	Nko = 0x10000L,
	/// <summary>The Samaritan code chart.</summary>
	Samaritan = 0x20000L,
	/// <summary>The Devanagari code chart.</summary>
	Devanagari = 0x40000L,
	/// <summary>The Bengali code chart.</summary>
	Bengali = 0x80000L,
	/// <summary>The Gurmukhi code chart.</summary>
	Gurmukhi = 0x100000L,
	/// <summary>The Gujarati code chart.</summary>
	Gujarati = 0x200000L,
	/// <summary>The Oriya code chart.</summary>
	Oriya = 0x400000L,
	/// <summary>The Tamil code chart.</summary>
	Tamil = 0x800000L,
	/// <summary>The Telugu code chart.</summary>
	Telugu = 0x1000000L,
	/// <summary>The Kannada code chart.</summary>
	Kannada = 0x2000000L,
	/// <summary>The Malayalam code chart.</summary>
	Malayalam = 0x4000000L,
	/// <summary>The Sinhala code chart.</summary>
	Sinhala = 0x8000000L,
	/// <summary>The Thai code chart.</summary>
	Thai = 0x10000000L,
	/// <summary>The Lao code chart.</summary>
	Lao = 0x20000000L,
	/// <summary>The Tibetan code table.</summary>
	Tibetan = 0x40000000L,
	/// <summary>The code charts that are marked as safe on initialization.</summary>
	Default = 0x7FL
}
