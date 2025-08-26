namespace System.Web.UI;

/// <summary>Defines the property and event a control implements to act as a check box. </summary>
public interface ICheckBoxControl
{
	/// <summary>Gets or sets the value of an <see cref="T:System.Web.UI.ICheckBoxControl" /> control that indicates whether the control is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the check box is selected; otherwise, <see langword="false" />.</returns>
	bool Checked { get; set; }

	/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.ICheckBoxControl.Checked" /> property changes between posts to the server.</summary>
	event EventHandler CheckedChanged;
}
