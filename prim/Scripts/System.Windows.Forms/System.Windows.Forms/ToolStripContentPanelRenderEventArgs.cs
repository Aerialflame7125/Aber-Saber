using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event. </summary>
public class ToolStripContentPanelRenderEventArgs : EventArgs
{
	private Graphics graphics;

	private bool handled;

	private ToolStripContentPanel tool_strip_content_panel;

	/// <summary>Gets the object to use for drawing.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> to use for drawing.</returns>
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

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> affected by the click.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
	public ToolStripContentPanel ToolStripContentPanel => tool_strip_content_panel;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> class. </summary>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> representing the GDI+ drawing surface.</param>
	/// <param name="contentPanel">The <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> to render.</param>
	public ToolStripContentPanelRenderEventArgs(Graphics g, ToolStripContentPanel contentPanel)
	{
		graphics = g;
		tool_strip_content_panel = contentPanel;
		handled = false;
	}
}
