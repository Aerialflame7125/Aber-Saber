namespace System.Web.Security.AntiXss;

/// <summary>Specifies values for the upper-middle region of the UTF-8 Unicode code charts, from U2DE0 to UA8DF.</summary>
[Flags]
public enum UpperMidCodeCharts : long
{
	/// <summary>None of the UTF-8 Unicode code charts from the upper-middle region are marked as safe.</summary>
	None = 0L,
	/// <summary>The Cyrillic Extended-A code chart.</summary>
	CyrillicExtendedA = 1L,
	/// <summary>The Supplemental Punctuation code chart.</summary>
	SupplementalPunctuation = 2L,
	/// <summary>The CJK Radicals Supplement code chart.</summary>
	CjkRadicalsSupplement = 4L,
	/// <summary>The Kangxi Radicals code chart.</summary>
	KangxiRadicals = 8L,
	/// <summary>The Ideographic Description Characters code chart.</summary>
	IdeographicDescriptionCharacters = 0x10L,
	/// <summary>The CJK Symbols and Punctuation code chart.</summary>
	CjkSymbolsAndPunctuation = 0x20L,
	/// <summary>The Hiragana code chart.</summary>
	Hiragana = 0x40L,
	/// <summary>The Katakana code chart.</summary>
	Katakana = 0x80L,
	/// <summary>The Bopomofo code chart.</summary>
	Bopomofo = 0x100L,
	/// <summary>The Hangul Compatibility Jamo code chart.</summary>
	HangulCompatibilityJamo = 0x200L,
	/// <summary>The Kanbun code chart.</summary>
	Kanbun = 0x400L,
	/// <summary>The Bopomofo Extended code chart.</summary>
	BopomofoExtended = 0x800L,
	/// <summary>The CJK Strokes code chart.</summary>
	CjkStrokes = 0x1000L,
	/// <summary>The Katakana Phonetic Extensions code chart.</summary>
	KatakanaPhoneticExtensions = 0x2000L,
	/// <summary>The Enclosed CJK Letters and Months code chart.</summary>
	EnclosedCjkLettersAndMonths = 0x4000L,
	/// <summary>The CJK Compatibility code chart.</summary>
	CjkCompatibility = 0x8000L,
	/// <summary>The CJK Unified Ideographs Extension-A code chart.</summary>
	CjkUnifiedIdeographsExtensionA = 0x10000L,
	/// <summary>The Yijing Hexagram Symbols code chart.</summary>
	YijingHexagramSymbols = 0x20000L,
	/// <summary>The CJK Unified Ideographs code chart.</summary>
	CjkUnifiedIdeographs = 0x40000L,
	/// <summary>The Yi Syllables code chart.</summary>
	YiSyllables = 0x80000L,
	/// <summary>The Yi Radicals code chart.</summary>
	YiRadicals = 0x100000L,
	/// <summary>The Lisu code chart.</summary>
	Lisu = 0x200000L,
	/// <summary>The Vai code chart.</summary>
	Vai = 0x400000L,
	/// <summary>The Cyrillic Extended-B code chart.</summary>
	CyrillicExtendedB = 0x800000L,
	/// <summary>The Bamum code chart.</summary>
	Bamum = 0x1000000L,
	/// <summary>The Modifier Tone Letters code chart.</summary>
	ModifierToneLetters = 0x2000000L,
	/// <summary>The Latin Extended-D code chart.</summary>
	LatinExtendedD = 0x4000000L,
	/// <summary>The Syloti Nagri code chart.</summary>
	SylotiNagri = 0x8000000L,
	/// <summary>The Common Indic Number Forms code chart.</summary>
	CommonIndicNumberForms = 0x10000000L,
	/// <summary>The Phags-Pa code chart.</summary>
	Phagspa = 0x20000000L,
	/// <summary>The Saurashtra code chart.</summary>
	Saurashtra = 0x40000000L
}
