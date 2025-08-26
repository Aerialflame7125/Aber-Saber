using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DrawTreeNodeEventArgs : EventArgs
{
	private Rectangle bounds;

	private bool draw_default;

	private Graphics graphics;

	private TreeNode node;

	private TreeNodeStates state;

	/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle Bounds => bounds;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TreeNode" /> should be drawn by the operating system rather than being owner drawn.</summary>
	/// <returns>true if the node should be drawn by the operating system; false if the node will be drawn in the event handler. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool DrawDefault
	{
		get
		{
			return draw_default;
		}
		set
		{
			draw_default = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNode Node => node;

	/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNodeStates State => state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> class.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
	/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
	public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state)
	{
		this.bounds = bounds;
		draw_default = false;
		this.graphics = graphics;
		this.node = node;
		this.state = state;
	}
}
