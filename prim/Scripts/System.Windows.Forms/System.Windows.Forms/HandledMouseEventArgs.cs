namespace System.Windows.Forms;

/// <summary>Allows a custom control to prevent the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event from being sent to its parent container.</summary>
/// <filterpriority>2</filterpriority>
public class HandledMouseEventArgs : MouseEventArgs
{
	private bool handled;

	/// <summary>Gets or sets whether this event should be forwarded to the control's parent container.</summary>
	/// <returns>true if the mouse event should go to the parent control; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, and the change of mouse pointer position.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
	/// <param name="clicks">The number of times a mouse button was pressed. </param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
	public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		: base(button, clicks, x, y, delta)
	{
		handled = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> class with the specified mouse button, number of mouse button clicks, horizontal and vertical screen coordinates, the change of mouse pointer position, and the value indicating whether the event is handled.</summary>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values indicating which mouse button was pressed. </param>
	/// <param name="clicks">The number of times a mouse button was pressed. </param>
	/// <param name="x">The x-coordinate of a mouse click, in pixels. </param>
	/// <param name="y">The y-coordinate of a mouse click, in pixels. </param>
	/// <param name="delta">A signed count of the number of detents the wheel has rotated. </param>
	/// <param name="defaultHandledValue">true if the event is handled; otherwise, false. </param>
	public HandledMouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta, bool defaultHandledValue)
		: base(button, clicks, x, y, delta)
	{
		handled = defaultHandledValue;
	}
}
