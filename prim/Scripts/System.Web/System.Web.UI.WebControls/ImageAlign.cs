namespace System.Web.UI.WebControls;

/// <summary>Specifies the alignment of an image in relation to the text of a Web page.</summary>
public enum ImageAlign
{
	/// <summary>The alignment is not set.</summary>
	NotSet,
	/// <summary>The image is aligned on the left edge of the Web page with text wrapping on the right.</summary>
	Left,
	/// <summary>The image is aligned on the right edge of the Web page with text wrapping on the left.</summary>
	Right,
	/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
	Baseline,
	/// <summary>The upper edge of the image is aligned with the upper edge of the highest element on the same line.</summary>
	Top,
	/// <summary>The middle of the image is aligned with the lower edge of the first line of text.</summary>
	Middle,
	/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
	Bottom,
	/// <summary>The lower edge of the image is aligned with the lower edge of the largest element on the same line.</summary>
	AbsBottom,
	/// <summary>The middle of the image is aligned with the middle of the largest element on the same line.</summary>
	AbsMiddle,
	/// <summary>The upper edge of the image is aligned with the upper edge of the highest text on the same line.</summary>
	TextTop
}
