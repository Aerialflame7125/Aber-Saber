using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the events that render the background of objects derived from <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> class. </summary>
/// <filterpriority>2</filterpriority>
public class ToolStripItemRenderEventArgs : EventArgs
{
	private Graphics graphics;

	private ToolStripItem item;

	/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripItem Item => item;

	/// <summary>Gets the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStrip ToolStrip => item.Owner;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to draw the item.</param>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to be drawn.</param>
	public ToolStripItemRenderEventArgs(Graphics g, ToolStripItem item)
	{
		graphics = g;
		this.item = item;
	}
}
