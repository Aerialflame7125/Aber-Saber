using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.DragDrop" />, <see cref="E:System.Windows.Forms.Control.DragEnter" />, or <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class DragEventArgs : EventArgs
{
	internal int x;

	internal int y;

	internal int keystate;

	internal DragDropEffects allowed_effect;

	internal DragDropEffects current_effect;

	internal IDataObject data_object;

	/// <summary>Gets which drag-and-drop operations are allowed by the originator (or source) of the drag event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public DragDropEffects AllowedEffect => allowed_effect;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.IDataObject" /> that contains the data associated with this event.</summary>
	/// <returns>The data associated with this event.</returns>
	/// <filterpriority>1</filterpriority>
	public IDataObject Data => data_object;

	/// <summary>Gets or sets the target drop effect in a drag-and-drop operation.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public DragDropEffects Effect
	{
		get
		{
			return current_effect;
		}
		set
		{
			current_effect = value;
		}
	}

	/// <summary>Gets the current state of the SHIFT, CTRL, and ALT keys, as well as the state of the mouse buttons.</summary>
	/// <returns>The current state of the SHIFT, CTRL, and ALT keys and of the mouse buttons.</returns>
	/// <filterpriority>1</filterpriority>
	public int KeyState => keystate;

	/// <summary>Gets the x-coordinate of the mouse pointer, in screen coordinates.</summary>
	/// <returns>The x-coordinate of the mouse pointer in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public int X => x;

	/// <summary>Gets the y-coordinate of the mouse pointer, in screen coordinates.</summary>
	/// <returns>The y-coordinate of the mouse pointer in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	public int Y => y;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DragEventArgs" /> class.</summary>
	/// <param name="data">The data associated with this event. </param>
	/// <param name="keyState">The current state of the SHIFT, CTRL, and ALT keys. </param>
	/// <param name="x">The x-coordinate of the mouse cursor in pixels. </param>
	/// <param name="y">The y-coordinate of the mouse cursor in pixels. </param>
	/// <param name="allowedEffect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
	/// <param name="effect">One of the <see cref="T:System.Windows.Forms.DragDropEffects" /> values. </param>
	public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect)
	{
		this.x = x;
		this.y = y;
		keystate = keyState;
		allowed_effect = allowedEffect;
		current_effect = effect;
		data_object = data;
	}
}
