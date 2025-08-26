namespace System.Web.UI.WebControls;

/// <summary>Specifies the bullet styles you can apply to list items in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. </summary>
public enum BulletStyle
{
	/// <summary>The bullet style is not set. The browser that renders the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control will determine the bullet style to display.</summary>
	NotSet,
	/// <summary>The bullet style is a number (1, 2, 3, ...).</summary>
	Numbered,
	/// <summary>The bullet style is a lowercase letter (a, b, c, ...).</summary>
	LowerAlpha,
	/// <summary>The bullet style is an uppercase letter (A, B, C, ...).</summary>
	UpperAlpha,
	/// <summary>The bullet style is a lowercase Roman numeral (i, ii, iii, ...).</summary>
	LowerRoman,
	/// <summary>The bullet style is an uppercase Roman numeral (I, II, III, ...).</summary>
	UpperRoman,
	/// <summary>The bullet style is a filled circle shape.</summary>
	Disc,
	/// <summary>The bullet style is an empty circle shape.</summary>
	Circle,
	/// <summary>The bullet style is a filled square shape.</summary>
	Square,
	/// <summary>The bullet style is a custom image.</summary>
	CustomImage
}
