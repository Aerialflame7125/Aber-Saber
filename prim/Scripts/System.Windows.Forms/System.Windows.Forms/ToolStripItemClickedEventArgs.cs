namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class ToolStripItemClickedEventArgs : EventArgs
{
	private ToolStripItem clicked_item;

	/// <summary>Gets the item that was clicked on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripItem ClickedItem => clicked_item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> class, specifying the <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked. </summary>
	/// <param name="clickedItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> that was clicked.</param>
	public ToolStripItemClickedEventArgs(ToolStripItem clickedItem)
	{
		clicked_item = clickedItem;
	}
}
