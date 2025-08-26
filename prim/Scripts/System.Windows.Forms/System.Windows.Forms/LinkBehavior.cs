namespace System.Windows.Forms;

/// <summary>Specifies the behaviors of a link in a <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
/// <filterpriority>2</filterpriority>
public enum LinkBehavior
{
	/// <summary>The behavior of this setting depends on the options set using the Internet Options dialog box in Control Panel or Internet Explorer.</summary>
	SystemDefault,
	/// <summary>The link always displays with underlined text.</summary>
	AlwaysUnderline,
	/// <summary>The link displays underlined text only when the mouse is hovered over the link text.</summary>
	HoverUnderline,
	/// <summary>The link text is never underlined. The link can still be distinguished from other text by use of the <see cref="P:System.Windows.Forms.LinkLabel.LinkColor" /> property of the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
	NeverUnderline
}
