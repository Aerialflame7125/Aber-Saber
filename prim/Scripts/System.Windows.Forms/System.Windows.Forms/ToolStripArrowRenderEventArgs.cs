using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class ToolStripArrowRenderEventArgs : EventArgs
{
	private Color arrow_color;

	private Rectangle arrow_rectangle;

	private ArrowDirection arrow_direction;

	private Graphics graphics;

	private ToolStripItem tool_strip_item;

	/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the arrow.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ArrowColor
	{
		get
		{
			return arrow_color;
		}
		set
		{
			arrow_color = value;
		}
	}

	/// <summary>Gets or sets the bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle ArrowRectangle
	{
		get
		{
			return arrow_rectangle;
		}
		set
		{
			arrow_rectangle = value;
		}
	}

	/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ArrowDirection" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public ArrowDirection Direction
	{
		get
		{
			return arrow_direction;
		}
		set
		{
			arrow_direction = value;
		}
	}

	/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint. </returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripItem Item => tool_strip_item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> class. </summary>
	/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
	/// <param name="toolStripItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</param>
	/// <param name="arrowRectangle">The bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
	/// <param name="arrowColor">The color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
	/// <param name="arrowDirection">The direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</param>
	public ToolStripArrowRenderEventArgs(Graphics g, ToolStripItem toolStripItem, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
	{
		graphics = g;
		tool_strip_item = toolStripItem;
		arrow_rectangle = arrowRectangle;
		arrow_color = arrowColor;
		arrow_direction = arrowDirection;
	}
}
