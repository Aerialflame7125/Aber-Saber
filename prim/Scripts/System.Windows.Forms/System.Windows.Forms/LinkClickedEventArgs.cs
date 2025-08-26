using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.RichTextBox.LinkClicked" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class LinkClickedEventArgs : EventArgs
{
	private string link_text;

	/// <summary>Gets the text of the link being clicked.</summary>
	/// <returns>The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public string LinkText => link_text;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkClickedEventArgs" /> class.</summary>
	/// <param name="linkText">The text of the link that is clicked in the <see cref="T:System.Windows.Forms.RichTextBox" /> control. </param>
	public LinkClickedEventArgs(string linkText)
	{
		link_text = linkText;
	}
}
