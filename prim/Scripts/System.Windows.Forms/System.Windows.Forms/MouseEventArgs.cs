using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.MouseUp" />, <see cref="E:System.Windows.Forms.Control.MouseDown" />, and <see cref="E:System.Windows.Forms.Control.MouseMove" /> events.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class MouseEventArgs : EventArgs
{
	private MouseButtons buttons;

	private int clicks;

	private int delta;

	private int x;

	private int y;

	/// <summary>Gets which mouse button was pressed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public MouseButtons Button => buttons;

	/// <summary>Gets the number of times the mouse button was pressed and released.</summary>
	/// <returns>An <see cref="T:System.Int32" /> containing the number of times the mouse button was pressed and released.</returns>
	/// <filterpriority>1</filterpriority>
	public int Clicks => clicks;

	/// <summary>Gets a signed count of the number of detents the mouse wheel has rotated. A detent is one notch of the mouse wheel.</summary>
	/// <returns>A signed count of the number of detents the mouse wheel has rotated.</returns>
	/// <filterpriority>1</filterpriority>
	public int Delta => delta;

	/// <summary>Gets the x-coordinate of the mouse during the generating mouse event.</summary>
	/// <returns>The x-coordinate of the mouse, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public int X => x;

	/// <summary>Gets the y-coordinate of the mouse during the generating mouse event.</summary>
	/// <returns>The y-coordinate of the mouse, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public int Y => y;

	/// <summary>Gets the location of the mouse during the generating mouse event.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> containing the x- and y- coordinate of the mouse, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public Point Location => new Point(x, y);

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MouseEventArgs" /> class.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
	/// <param name="clicks">The number of times a mouse button was pressed. </param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
	/// <param name="y"></param>
	/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
	public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
	{
		buttons = button;
		this.clicks = clicks;
		this.delta = delta;
		this.x = x;
		this.y = y;
	}
}
