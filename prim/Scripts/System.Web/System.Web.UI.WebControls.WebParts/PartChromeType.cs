namespace System.Web.UI.WebControls.WebParts;

/// <summary>Specifies the kind of border that surrounds a Web Parts control.</summary>
public enum PartChromeType
{
	/// <summary>A border setting inherited from the part control's containing zone.</summary>
	Default,
	/// <summary>A title bar and a border.</summary>
	TitleAndBorder,
	/// <summary>No border and no title bar.</summary>
	None,
	/// <summary>A title bar only, without a border.</summary>
	TitleOnly,
	/// <summary>A border only, without a title bar.</summary>
	BorderOnly
}
