using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the events defined on <see cref="T:System.Windows.Forms.HtmlDocument" /> and <see cref="T:System.Windows.Forms.HtmlElement" />.</summary>
/// <filterpriority>2</filterpriority>
public sealed class HtmlElementEventArgs : EventArgs
{
	private bool alt_key_pressed;

	private bool bubble_event;

	private Point client_mouse_position;

	private bool ctrl_key_pressed;

	private string event_type;

	private HtmlElement from_element;

	private int key_pressed_code;

	private MouseButtons mouse_buttons_pressed;

	private Point mouse_position;

	private Point offset_mouse_position;

	private bool return_value;

	private bool shift_key_pressed;

	private HtmlElement to_element;

	/// <summary>Indicates whether the ALT key was pressed when this event occurred.</summary>
	/// <returns>true is the ALT key was pressed; otherwise, false.</returns>
	public bool AltKeyPressed => alt_key_pressed;

	/// <summary>Gets or sets a value indicating whether the current event bubbles up through the element hierarchy of the HTML Document Object Model.</summary>
	/// <returns>true if the event bubbles; false if it does not. </returns>
	/// <filterpriority>1</filterpriority>
	public bool BubbleEvent
	{
		get
		{
			return bubble_event;
		}
		set
		{
			bubble_event = value;
		}
	}

	/// <summary>Gets or sets the position of the mouse cursor in the document's client area. </summary>
	/// <returns>The current position of the mouse cursor. </returns>
	/// <filterpriority>1</filterpriority>
	public Point ClientMousePosition => client_mouse_position;

	/// <summary>Indicates whether the CTRL key was pressed when this event occurred.</summary>
	/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
	public bool CtrlKeyPressed => ctrl_key_pressed;

	/// <summary>Gets the name of the event that was raised.</summary>
	/// <returns>The name of the event. </returns>
	/// <filterpriority>1</filterpriority>
	public string EventType => event_type;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> the mouse pointer is moving away from.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public HtmlElement FromElement => from_element;

	/// <summary>Gets the ASCII value of the keyboard character typed in a <see cref="E:System.Windows.Forms.HtmlElement.KeyPress" />, <see cref="E:System.Windows.Forms.HtmlElement.KeyDown" />, or <see cref="E:System.Windows.Forms.HtmlElement.KeyUp" /> event.</summary>
	/// <returns>The ASCII value of the composed keyboard entry.</returns>
	/// <filterpriority>1</filterpriority>
	public int KeyPressedCode => key_pressed_code;

	/// <summary>Gets the mouse button that was clicked during a <see cref="E:System.Windows.Forms.HtmlElement.MouseDown" /> or <see cref="E:System.Windows.Forms.HtmlElement.MouseUp" /> event.</summary>
	/// <filterpriority>1</filterpriority>
	public MouseButtons MouseButtonsPressed => mouse_buttons_pressed;

	/// <summary>Gets or sets the position of the mouse cursor relative to a relatively positioned parent element.</summary>
	/// <returns>The position of the mouse cursor relative to the upper-left corner of the parent of the element that raised the event, if the parent element is relatively positioned. </returns>
	/// <filterpriority>1</filterpriority>
	public Point MousePosition => mouse_position;

	/// <summary>Gets or sets the position of the mouse cursor relative to the element that raises the event.</summary>
	/// <returns>The mouse position relative to the element that raises the event.</returns>
	/// <filterpriority>1</filterpriority>
	public Point OffsetMousePosition => offset_mouse_position;

	/// <summary>Gets or sets the return value of the handled event. </summary>
	/// <returns>true if the event has been handled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool ReturnValue
	{
		get
		{
			return return_value;
		}
		set
		{
			return_value = value;
		}
	}

	/// <summary>Indicates whether the SHIFT key was pressed when this event occurred.</summary>
	/// <returns>true if the SHIFT key was pressed; otherwise, false.</returns>
	public bool ShiftKeyPressed => shift_key_pressed;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> toward which the user is moving the mouse pointer.</summary>
	/// <returns>The element toward which the mouse pointer is moving. </returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public HtmlElement ToElement => to_element;

	internal HtmlElementEventArgs()
	{
		alt_key_pressed = false;
		bubble_event = false;
		client_mouse_position = Point.Empty;
		ctrl_key_pressed = false;
		event_type = null;
		from_element = null;
		key_pressed_code = 0;
		mouse_buttons_pressed = MouseButtons.None;
		mouse_position = Point.Empty;
		offset_mouse_position = Point.Empty;
		return_value = false;
		shift_key_pressed = false;
		to_element = null;
	}
}
