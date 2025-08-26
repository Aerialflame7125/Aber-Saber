namespace System.Windows.Forms;

/// <summary>Specifies the types of input and output streams used to load and save data in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
/// <filterpriority>2</filterpriority>
public enum RichTextBoxStreamType
{
	/// <summary>A Rich Text Format (RTF) stream.</summary>
	RichText,
	/// <summary>A plain text stream that includes spaces in places of Object Linking and Embedding (OLE) objects.</summary>
	PlainText,
	/// <summary>A Rich Text Format (RTF) stream with spaces in place of OLE objects. This value is only valid for use with the <see cref="M:System.Windows.Forms.RichTextBox.SaveFile(System.String)" /> method of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	RichNoOleObjs,
	/// <summary>A plain text stream with a textual representation of OLE objects. This value is only valid for use with the <see cref="M:System.Windows.Forms.RichTextBox.SaveFile(System.String)" /> method of the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	TextTextOleObjs,
	/// <summary>A text stream that contains spaces in place of Object Linking and Embedding (OLE) objects. The text is encoded in Unicode.</summary>
	UnicodePlainText
}
