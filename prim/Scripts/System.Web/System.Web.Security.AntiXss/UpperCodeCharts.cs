namespace System.Web.Security.AntiXss;

/// <summary>Specifies values for the upper region of the UTF-8 Unicode code charts, from UA8E0 to UFFFD.</summary>
[Flags]
public enum UpperCodeCharts
{
	/// <summary>None of the UTF-8 Unicode code charts from the upper region are marked as safe.</summary>
	None = 0,
	/// <summary>The Devanagari Extended code chart.</summary>
	DevanagariExtended = 1,
	/// <summary>The Kayah Li code chart.</summary>
	KayahLi = 2,
	/// <summary>The Rejang code chart.</summary>
	Rejang = 4,
	/// <summary>The Hangul Jamo Extended-A code chart.</summary>
	HangulJamoExtendedA = 8,
	/// <summary>The Javanese code chart.</summary>
	Javanese = 0x10,
	/// <summary>The Cham code chart.</summary>
	Cham = 0x20,
	/// <summary>The Myanmar Extended-A code chart.</summary>
	MyanmarExtendedA = 0x40,
	/// <summary>The Tai Viet code chart.</summary>
	TaiViet = 0x80,
	/// <summary>The Meetei Mayek code chart.</summary>
	MeeteiMayek = 0x100,
	/// <summary>The Hangul Syllables code chart.</summary>
	HangulSyllables = 0x200,
	/// <summary>The Hangul Jamo Extended-B code chart.</summary>
	HangulJamoExtendedB = 0x400,
	/// <summary>The CJK Compatibility Ideographs code chart.</summary>
	CjkCompatibilityIdeographs = 0x800,
	/// <summary>The Alphabetic Presentation Forms code chart.</summary>
	AlphabeticPresentationForms = 0x1000,
	/// <summary>The Arabic Presentation Forms-A code chart.</summary>
	ArabicPresentationFormsA = 0x2000,
	/// <summary>The Variation Selectors code chart.</summary>
	VariationSelectors = 0x4000,
	/// <summary>The Vertical Forms code chart.</summary>
	VerticalForms = 0x8000,
	/// <summary>The Combining Half Marks code chart.</summary>
	CombiningHalfMarks = 0x10000,
	/// <summary>The CJK Compatibility Forms code chart.</summary>
	CjkCompatibilityForms = 0x20000,
	/// <summary>The Small Form Variants code chart.</summary>
	SmallFormVariants = 0x40000,
	/// <summary>The Arabic Presentation Forms-B code chart.</summary>
	ArabicPresentationFormsB = 0x80000,
	/// <summary>The Halfwidth and Fullwidth Forms code chart</summary>
	HalfWidthAndFullWidthForms = 0x100000,
	/// <summary>The Specials code chart.</summary>
	Specials = 0x200000
}
