namespace System.Windows.Forms;

/// <summary>Allows a control to act like a button on a form.</summary>
/// <filterpriority>2</filterpriority>
public interface IButtonControl
{
	/// <summary>Gets or sets the value returned to the parent form when the button is clicked.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	DialogResult DialogResult { get; set; }

	/// <summary>Notifies a control that it is the default button so that its appearance and behavior is adjusted accordingly.</summary>
	/// <param name="value">true if the control should behave as a default button; otherwise false. </param>
	/// <filterpriority>1</filterpriority>
	void NotifyDefault(bool value);

	/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the control.</summary>
	/// <filterpriority>1</filterpriority>
	void PerformClick();
}
