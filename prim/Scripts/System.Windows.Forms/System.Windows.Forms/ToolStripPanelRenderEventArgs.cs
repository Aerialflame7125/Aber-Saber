using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripPanel" /> drawing.</summary>
public class ToolStripPanelRenderEventArgs : EventArgs
{
	private Graphics graphics;

	private bool handled;

	private ToolStripPanel tool_strip_panel;

	/// <summary>Gets or sets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
	public Graphics Graphics => graphics;

	/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
	/// <returns>true if the event was handled; otherwise, false. </returns>
	public bool Handled
	{
		get
		{
			return handled;
		}
		set
		{
			handled = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</returns>
	public ToolStripPanel ToolStripPanel => tool_strip_panel;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripPanel" /> that uses the specified graphics for drawing. </summary>
	/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
	/// <param name="toolStripPanel">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to draw.</param>
	public ToolStripPanelRenderEventArgs(Graphics g, ToolStripPanel toolStripPanel)
	{
		graphics = g;
		tool_strip_panel = toolStripPanel;
		handled = false;
	}
}
