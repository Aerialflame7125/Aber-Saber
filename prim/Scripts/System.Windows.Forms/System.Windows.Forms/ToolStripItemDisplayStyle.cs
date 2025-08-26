namespace System.Windows.Forms;

/// <summary>Specifies what to render (image or text) for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
/// <filterpriority>2</filterpriority>
public enum ToolStripItemDisplayStyle
{
	/// <summary>Specifies that neither image nor text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	None,
	/// <summary>Specifies that only text is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	Text,
	/// <summary>Specifies that only an image is to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	Image,
	/// <summary>Specifies that both an image and text are to be rendered for this <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	ImageAndText
}
