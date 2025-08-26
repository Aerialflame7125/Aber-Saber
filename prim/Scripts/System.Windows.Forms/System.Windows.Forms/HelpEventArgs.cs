using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class HelpEventArgs : EventArgs
{
	private Point mouse_position;

	private bool event_handled;

	/// <summary>Gets or sets a value indicating whether the help event was handled.</summary>
	/// <returns>true if the event is handled; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool Handled
	{
		get
		{
			return event_handled;
		}
		set
		{
			event_handled = value;
		}
	}

	/// <summary>Gets the screen coordinates of the mouse pointer.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> representing the screen coordinates of the mouse pointer.</returns>
	/// <filterpriority>1</filterpriority>
	public Point MousePos => mouse_position;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.HelpEventArgs" /> class.</summary>
	/// <param name="mousePos">The coordinates of the mouse pointer. </param>
	public HelpEventArgs(Point mousePos)
	{
		mouse_position = mousePos;
		event_handled = false;
	}
}
