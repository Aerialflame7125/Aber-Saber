using System.ComponentModel.Design;
using Microsoft.Win32;

namespace System.Windows.Forms.ComponentModel.Com2Interop;

/// <summary>Allows Visual Studio to communicate internally with the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
public interface IComPropertyBrowser
{
	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is currently setting one of the properties of its selected object; otherwise, false.</returns>
	bool InPropertySet { get; }

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.PropertyGrid" /> control is browsing a COM object and the user renames the object.</summary>
	event ComponentRenameEventHandler ComComponentNameChanged;

	/// <summary>Closes any open drop-down controls on the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	void DropDownDone();

	/// <summary>Commits all pending changes to the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.PropertyGrid" /> successfully commits changes; otherwise, false.</returns>
	bool EnsurePendingChangesCommitted();

	/// <summary>Activates the <see cref="T:System.Windows.Forms.PropertyGrid" /> control when the user chooses Properties for a control in Design view.</summary>
	void HandleF4();

	/// <summary>Loads user states from the registry into the <see cref="T:System.Windows.Forms.PropertyGrid" /> control.</summary>
	/// <param name="key">The registry key that contains the user states.</param>
	void LoadState(RegistryKey key);

	/// <summary>Saves user states from the <see cref="T:System.Windows.Forms.PropertyGrid" /> control to the registry.</summary>
	/// <param name="key">The registry key that contains the user states.</param>
	void SaveState(RegistryKey key);
}
