using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class QueryContinueDragEventArgs : EventArgs
{
	internal int key_state;

	internal bool escape_pressed;

	internal DragAction drag_action;

	/// <summary>Gets or sets the status of a drag-and-drop operation.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DragAction" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	public DragAction Action
	{
		get
		{
			return drag_action;
		}
		set
		{
			drag_action = value;
		}
	}

	/// <summary>Gets whether the user pressed the ESC key.</summary>
	/// <returns>true if the ESC key was pressed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool EscapePressed => escape_pressed;

	/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys.</summary>
	/// <returns>The current state of the SHIFT, CTRL, and ALT keys.</returns>
	/// <filterpriority>1</filterpriority>
	public int KeyState => key_state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> class.</summary>
	/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
	/// <param name="escapePressed">true if the ESC key was pressed; otherwise, false. </param>
	/// <param name="action">A <see cref="T:System.Windows.Forms.DragAction" /> value. </param>
	public QueryContinueDragEventArgs(int keyState, bool escapePressed, DragAction action)
	{
		key_state = keyState;
		escape_pressed = escapePressed;
		drag_action = action;
	}
}
