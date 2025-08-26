namespace System.Web.UI.WebControls;

/// <summary>Specifies the font sizes defined by HTML 4.0.</summary>
public enum FontSize
{
	/// <summary>The font size is not set.</summary>
	NotSet,
	/// <summary>The font size is specified by a point value.</summary>
	AsUnit,
	/// <summary>The font size is one size smaller than the parent element.</summary>
	Smaller,
	/// <summary>The font size is one size larger than the parent element.</summary>
	Larger,
	/// <summary>The font size is two sizes smaller than the base font size.</summary>
	XXSmall,
	/// <summary>The font size is one size smaller than the base font size.</summary>
	XSmall,
	/// <summary>The base font size determined by the browser.</summary>
	Small,
	/// <summary>The font size is one size larger than the default font size.</summary>
	Medium,
	/// <summary>The font size is two sizes larger than the base font size.</summary>
	Large,
	/// <summary>The font size is three sizes larger than the base font size.</summary>
	XLarge,
	/// <summary>The font size is four sizes larger than the base font size.</summary>
	XXLarge
}
