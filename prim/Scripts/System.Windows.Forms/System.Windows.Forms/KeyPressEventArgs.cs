using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class KeyPressEventArgs : EventArgs
{
	private char key_char;

	private bool event_handled;

	/// <summary>Gets or sets a value indicating whether the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event was handled.</summary>
	/// <returns>true if the event is handled; otherwise, false.</returns>
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

	/// <summary>Gets or sets the character corresponding to the key pressed.</summary>
	/// <returns>The ASCII character that is composed. For example, if the user presses SHIFT + K, this property returns an uppercase K.</returns>
	/// <filterpriority>1</filterpriority>
	public char KeyChar
	{
		get
		{
			return key_char;
		}
		set
		{
			key_char = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> class.</summary>
	/// <param name="keyChar">The ASCII character corresponding to the key the user pressed. </param>
	public KeyPressEventArgs(char keyChar)
	{
		key_char = keyChar;
		event_handled = false;
	}
}
