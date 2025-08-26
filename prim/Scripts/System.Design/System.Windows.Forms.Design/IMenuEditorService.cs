namespace System.Windows.Forms.Design;

/// <summary>Provides access to the menu editing service.</summary>
public interface IMenuEditorService
{
	/// <summary>Gets the current menu.</summary>
	/// <returns>The current <see cref="T:System.Windows.Forms.Menu" />.</returns>
	Menu GetMenu();

	/// <summary>Indicates whether the current menu is active.</summary>
	/// <returns>
	///   <see langword="true" /> if the current menu is currently active; otherwise, <see langword="false" />.</returns>
	bool IsActive();

	/// <summary>Allows the editor service to intercept Win32 messages.</summary>
	/// <param name="m">The <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	/// <returns>
	///   <see langword="true" /> if the message is for the control; otherwise, <see langword="false" />.</returns>
	bool MessageFilter(ref Message m);

	/// <summary>Sets the specified menu visible on the form.</summary>
	/// <param name="menu">The <see cref="T:System.Windows.Forms.Menu" /> to render.</param>
	void SetMenu(Menu menu);

	/// <summary>Sets the selected menu item of the current menu.</summary>
	/// <param name="item">A <see cref="T:System.Windows.Forms.MenuItem" /> to set as the currently selected menu item.</param>
	void SetSelection(MenuItem item);
}
