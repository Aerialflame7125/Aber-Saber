namespace System.Windows.Forms;

/// <summary>Specifies the layout for an image contained in a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewImageCellLayout
{
	/// <summary>The layout specification has not been set.</summary>
	NotSet,
	/// <summary>The graphic is displayed centered using its native resolution.</summary>
	Normal,
	/// <summary>The graphic is stretched by the percentages required to fit the width and height of the containing cell.</summary>
	Stretch,
	/// <summary>The graphic is uniformly enlarged until it fills the width or height of the containing cell.</summary>
	Zoom
}
