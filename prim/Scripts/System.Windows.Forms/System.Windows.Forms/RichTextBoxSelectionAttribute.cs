namespace System.Windows.Forms;

/// <summary>Specifies whether any characters in the current selection have the style or attribute.</summary>
/// <filterpriority>1</filterpriority>
public enum RichTextBoxSelectionAttribute
{
	/// <summary>No characters.</summary>
	None = 0,
	/// <summary>All characters.</summary>
	All = 1,
	/// <summary>Some but not all characters.</summary>
	Mixed = -1
}
