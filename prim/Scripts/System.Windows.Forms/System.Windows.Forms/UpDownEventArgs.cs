namespace System.Windows.Forms;

/// <summary>Provides data for controls that derive from the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class UpDownEventArgs : EventArgs
{
	private int button_id;

	/// <summary>Gets a value that represents which button the user clicked.</summary>
	/// <returns>A value that represents which button the user clicked.</returns>
	/// <filterpriority>1</filterpriority>
	public int ButtonID => button_id;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownEventArgs" /> class</summary>
	/// <param name="buttonPushed">The button that was clicked on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</param>
	public UpDownEventArgs(int buttonPushed)
	{
		button_id = buttonPushed;
	}
}
