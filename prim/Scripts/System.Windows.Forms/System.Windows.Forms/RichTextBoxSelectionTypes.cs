namespace System.Windows.Forms;

/// <summary>Specifies the type of selection in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum RichTextBoxSelectionTypes
{
	/// <summary>No text is selected in the current selection.</summary>
	Empty = 0,
	/// <summary>The current selection contains only text.</summary>
	Text = 1,
	/// <summary>At least one Object Linking and Embedding (OLE) object is selected.</summary>
	Object = 2,
	/// <summary>More than one character is selected.</summary>
	MultiChar = 4,
	/// <summary>More than one Object Linking and Embedding (OLE) object is selected.</summary>
	MultiObject = 8
}
