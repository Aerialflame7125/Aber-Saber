namespace System.Windows.Forms;

/// <summary>Specifies the layout of multiple document interface (MDI) child windows in an MDI parent window.</summary>
/// <filterpriority>2</filterpriority>
public enum MdiLayout
{
	/// <summary>All MDI child windows are cascaded within the client region of the MDI parent form.</summary>
	Cascade,
	/// <summary>All MDI child windows are tiled horizontally within the client region of the MDI parent form.</summary>
	TileHorizontal,
	/// <summary>All MDI child windows are tiled vertically within the client region of the MDI parent form.</summary>
	TileVertical,
	/// <summary>All MDI child icons are arranged within the client region of the MDI parent form.</summary>
	ArrangeIcons
}
