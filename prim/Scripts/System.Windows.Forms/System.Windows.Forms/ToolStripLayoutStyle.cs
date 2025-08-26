namespace System.Windows.Forms;

/// <summary>Specifies the possible alignments with which the items of a <see cref="T:System.Windows.Forms.ToolStrip" /> can be displayed.</summary>
/// <filterpriority>2</filterpriority>
public enum ToolStripLayoutStyle
{
	/// <summary>Specifies that items are laid out automatically.</summary>
	StackWithOverflow,
	/// <summary>Specifies that items are laid out horizontally and overflow as necessary.</summary>
	HorizontalStackWithOverflow,
	/// <summary>Specifies that items are laid out vertically, are centered within the control, and overflow as necessary.</summary>
	VerticalStackWithOverflow,
	/// <summary>Specifies that items flow horizontally or vertically as necessary.</summary>
	Flow,
	/// <summary>Specifies that items are laid out flush left.</summary>
	Table
}
