namespace System.Windows.Forms;

/// <summary>Specifies the mode for the automatic completion feature used in the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
/// <filterpriority>2</filterpriority>
public enum AutoCompleteMode
{
	/// <summary>Disables the automatic completion feature for the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
	None,
	/// <summary>Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.</summary>
	Suggest,
	/// <summary>Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.</summary>
	Append,
	/// <summary>Applies both Suggest and Append options.</summary>
	SuggestAppend
}
