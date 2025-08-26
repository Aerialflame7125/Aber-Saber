namespace System.Windows.Forms;

/// <summary>Specifies whether the tabs in a tab control are owner-drawn (drawn by the parent window), or drawn by the operating system.</summary>
/// <filterpriority>2</filterpriority>
public enum TabDrawMode
{
	/// <summary>The tabs are drawn by the operating system, and are of the same size.</summary>
	Normal,
	/// <summary>The tabs are drawn by the parent window, and are of the same size.</summary>
	OwnerDrawFixed
}
