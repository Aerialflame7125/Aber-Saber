namespace System.Windows.Forms;

/// <summary>Provides the functionality for a control to act as a parent for other controls.</summary>
/// <filterpriority>2</filterpriority>
public interface IContainerControl
{
	/// <summary>Gets or sets the control that is active on the container control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that is currently active on the container control.</returns>
	/// <filterpriority>1</filterpriority>
	Control ActiveControl { get; set; }

	/// <summary>Activates a specified control.</summary>
	/// <returns>true if the control is successfully activated; otherwise, false.</returns>
	/// <param name="active">The <see cref="T:System.Windows.Forms.Control" /> being activated. </param>
	/// <filterpriority>1</filterpriority>
	bool ActivateControl(Control active);
}
