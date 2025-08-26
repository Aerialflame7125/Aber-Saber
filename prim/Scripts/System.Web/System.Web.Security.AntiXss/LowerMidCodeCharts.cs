namespace System.Web.Security.AntiXss;

/// <summary>Specifies values for the lower-middle region of the UTF-8 Unicode code charts, from U1000 to U1EFF.</summary>
[Flags]
public enum LowerMidCodeCharts : long
{
	/// <summary>None of the UTF-8 Unicode code charts from the lower-middle region are marked as safe.</summary>
	None = 0L,
	/// <summary>The Myanmar code chart.</summary>
	Myanmar = 1L,
	/// <summary>The Georgian code chart.</summary>
	Georgian = 2L,
	/// <summary>The Hangul Jamo code chart</summary>
	HangulJamo = 4L,
	/// <summary>The Ethiopic code chart.</summary>
	Ethiopic = 8L,
	/// <summary>The Ethiopic Supplement code chart.</summary>
	EthiopicSupplement = 0x10L,
	/// <summary>The Cherokee code chart.</summary>
	Cherokee = 0x20L,
	/// <summary>The Unified Canadian Aboriginal Syllabics code chart.</summary>
	UnifiedCanadianAboriginalSyllabics = 0x40L,
	/// <summary>The Ogham code chart.</summary>
	Ogham = 0x80L,
	/// <summary>The Runic code chart.</summary>
	Runic = 0x100L,
	/// <summary>The Tagalog code chart.</summary>
	Tagalog = 0x200L,
	/// <summary>The Hanunoo code chart.</summary>
	Hanunoo = 0x400L,
	/// <summary>The Buhid code chart</summary>
	Buhid = 0x800L,
	/// <summary>The Tagbanwa code chart.</summary>
	Tagbanwa = 0x1000L,
	/// <summary>The Khmer code chart.</summary>
	Khmer = 0x2000L,
	/// <summary>The Mongolian code chart.</summary>
	Mongolian = 0x4000L,
	/// <summary>The Unified Canadian Aboriginal Syllabics Extended code chart.</summary>
	UnifiedCanadianAboriginalSyllabicsExtended = 0x8000L,
	/// <summary>The Limbu code chart.</summary>
	Limbu = 0x10000L,
	/// <summary>The Tai Le code chart.</summary>
	TaiLe = 0x20000L,
	/// <summary>The New Tai Lue code chart.</summary>
	NewTaiLue = 0x40000L,
	/// <summary>The Khmer Symbols code chart.</summary>
	KhmerSymbols = 0x80000L,
	/// <summary>The Buginese code chart</summary>
	Buginese = 0x100000L,
	/// <summary>The Tai Tham code chart.</summary>
	TaiTham = 0x200000L,
	/// <summary>The Balinese code chart.</summary>
	Balinese = 0x400000L,
	/// <summary>The Sudanese code chart.</summary>
	Sudanese = 0x800000L,
	/// <summary>The Lepcha code chart.</summary>
	Lepcha = 0x1000000L,
	/// <summary>The Ol Chiki code chart.</summary>
	OlChiki = 0x2000000L,
	/// <summary>The Vedic Extensions code chart.</summary>
	VedicExtensions = 0x4000000L,
	/// <summary>The Phonetic Extensions code chart.</summary>
	PhoneticExtensions = 0x8000000L,
	/// <summary>The Phonetic Extensions Supplement code chart.</summary>
	PhoneticExtensionsSupplement = 0x10000000L,
	/// <summary>The Combining Diacritical Marks Supplement code chart.</summary>
	CombiningDiacriticalMarksSupplement = 0x20000000L,
	/// <summary>The Latin Extended Additional code chart.</summary>
	LatinExtendedAdditional = 0x40000000L
}
