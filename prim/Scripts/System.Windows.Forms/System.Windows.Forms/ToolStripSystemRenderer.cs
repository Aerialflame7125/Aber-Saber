using System.Windows.Forms.Theming;

namespace System.Windows.Forms;

/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, using system colors and a flat visual style.</summary>
/// <filterpriority>2</filterpriority>
public class ToolStripSystemRenderer : ToolStripRenderer
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /> class. </summary>
	public ToolStripSystemRenderer()
	{
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderButtonBackground(e);
		base.OnRenderButtonBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderDropDownButtonBackground(e);
		base.OnRenderDropDownButtonBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderGrip(e);
		base.OnRenderGrip(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
	{
		base.OnRenderImageMargin(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
	{
		base.OnRenderItemBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
	protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
	{
		base.OnRenderLabelBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
	protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderMenuItemBackground(e);
		base.OnRenderMenuItemBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderOverflowButtonBackground(e);
		base.OnRenderOverflowButtonBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderSeparator(e);
		base.OnRenderSeparator(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
	protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderSplitButtonBackground(e);
		base.OnRenderSplitButtonBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data. </param>
	protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderToolStripBackground(e);
		base.OnRenderToolStripBackground(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
	protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
	{
		ThemeElements.CurrentTheme.ToolStripPainter.OnRenderToolStripBorder(e);
		base.OnRenderToolStripBorder(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
	protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
	{
		base.OnRenderToolStripStatusLabelBackground(e);
	}
}
