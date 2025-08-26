namespace System.Windows.Forms;

/// <summary>Specifies the types of punctuation tables that can be used with the <see cref="T:System.Windows.Forms.RichTextBox" /> control's word-wrapping and word-breaking features.</summary>
/// <filterpriority>2</filterpriority>
public enum RichTextBoxWordPunctuations
{
	/// <summary>Use pre-defined Level 1 punctuation table as default.</summary>
	Level1 = 128,
	/// <summary>Use pre-defined Level 2 punctuation table as default.</summary>
	Level2 = 256,
	/// <summary>Use a custom defined punctuation table.</summary>
	Custom = 512,
	/// <summary>Used as a mask.</summary>
	All = 896
}
