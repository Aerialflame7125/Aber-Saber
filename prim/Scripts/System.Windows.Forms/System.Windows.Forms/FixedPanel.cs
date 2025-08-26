namespace System.Windows.Forms;

/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />, or neither panel is fixed.</summary>
/// <filterpriority>2</filterpriority>
public enum FixedPanel
{
	/// <summary>Specifies that neither <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />, <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects both panels.</summary>
	None,
	/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
	Panel1,
	/// <summary>Specifies that <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is fixed. A <see cref="E:System.Windows.Forms.Control.Resize" /> event affects only <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
	Panel2
}
