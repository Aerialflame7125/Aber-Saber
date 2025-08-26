using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for splitter events.</summary>
/// <filterpriority>2</filterpriority>
public class SplitterCancelEventArgs : CancelEventArgs
{
	private int mouse_cursor_x;

	private int mouse_cursor_y;

	private int split_x;

	private int split_y;

	/// <summary>Gets the X coordinate of the mouse pointer in client coordinates.</summary>
	/// <returns>An integer representing the X coordinate of the mouse pointer in client coordinates.</returns>
	/// <filterpriority>1</filterpriority>
	public int MouseCursorX => mouse_cursor_x;

	/// <summary>Gets the Y coordinate of the mouse pointer in client coordinates.</summary>
	/// <returns>An integer representing the Y coordinate of the mouse pointer in client coordinates.</returns>
	/// <filterpriority>1</filterpriority>
	public int MouseCursorY => mouse_cursor_y;

	/// <summary>Gets or sets the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
	/// <returns>An integer representing the X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int SplitX
	{
		get
		{
			return split_x;
		}
		set
		{
			split_x = value;
		}
	}

	/// <summary>Gets or sets the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates.</summary>
	/// <returns>An integer representing the Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int SplitY
	{
		get
		{
			return split_y;
		}
		set
		{
			split_y = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterCancelEventArgs" /> class with the specified coordinates of the mouse pointer and the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <param name="mouseCursorX">The X coordinate of the mouse pointer in client coordinates. </param>
	/// <param name="mouseCursorY">The Y coordinate of the mouse pointer in client coordinates. </param>
	/// <param name="splitX">The X coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates. </param>
	/// <param name="splitY">The Y coordinate of the upper left corner of the <see cref="T:System.Windows.Forms.SplitContainer" /> in client coordinates. </param>
	public SplitterCancelEventArgs(int mouseCursorX, int mouseCursorY, int splitX, int splitY)
	{
		mouse_cursor_x = mouseCursorX;
		mouse_cursor_y = mouseCursorY;
		split_x = splitX;
		split_y = splitY;
	}
}
