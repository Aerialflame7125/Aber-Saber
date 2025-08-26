namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
public class PreviewKeyDownEventArgs : EventArgs
{
	private Keys key_data;

	private bool is_input_key;

	/// <summary>Gets a value indicating whether the ALT key was pressed.</summary>
	/// <returns>true if the ALT key was pressed; otherwise, false.</returns>
	public bool Alt => (key_data & Keys.Alt) != 0;

	/// <summary>Gets a value indicating whether the CTRL key was pressed.</summary>
	/// <returns>true if the CTRL key was pressed; otherwise, false.</returns>
	public bool Control => (key_data & Keys.Control) != 0;

	/// <summary>Gets or sets a value indicating whether a key is a regular input key.</summary>
	/// <returns>true if the key is a regular input key; otherwise, false.</returns>
	public bool IsInputKey
	{
		get
		{
			return is_input_key;
		}
		set
		{
			is_input_key = value;
		}
	}

	/// <summary>Gets the keyboard code for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
	public Keys KeyCode => key_data & Keys.KeyCode;

	/// <summary>Gets the key data for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
	public Keys KeyData => key_data;

	/// <summary>Gets the keyboard value for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the keyboard value.</returns>
	public int KeyValue => Convert.ToInt32(key_data);

	/// <summary>Gets the modifier flags for a <see cref="E:System.Windows.Forms.Control.KeyDown" /> or <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Keys" /> values.</returns>
	public Keys Modifiers => key_data & Keys.Modifiers;

	/// <summary>Gets a value indicating whether the SHIFT key was pressed.</summary>
	/// <returns>true if the SHIFT key was pressed; otherwise, false.</returns>
	public bool Shift => (key_data & Keys.Shift) != 0;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> class with the specified key. </summary>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values.</param>
	public PreviewKeyDownEventArgs(Keys keyData)
	{
		key_data = keyData;
	}
}
