namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.HtmlWindow.Error" /> event. </summary>
public sealed class HtmlElementErrorEventArgs : EventArgs
{
	private string description;

	private bool handled;

	private int line_number;

	private Uri url;

	/// <summary>Gets the descriptive string corresponding to the error.</summary>
	public string Description => description;

	/// <summary>Gets or sets whether this error has been handled by the application hosting the document.</summary>
	/// <returns>True if the event has been handled; otherwise, false. The default is false.</returns>
	public bool Handled
	{
		get
		{
			return handled;
		}
		set
		{
			handled = value;
		}
	}

	/// <summary>Gets the line of HTML script code on which the error occurred.</summary>
	/// <returns>An <see cref="T:System.Int32" /> designating the script line number.</returns>
	public int LineNumber => line_number;

	/// <summary>Gets the location of the document that generated the error.</summary>
	/// <returns>A <see cref="T:System.Uri" />.</returns>
	public Uri Url => url;

	internal HtmlElementErrorEventArgs(string description, int lineNumber, Uri url)
	{
		this.description = description;
		line_number = lineNumber;
		this.url = url;
	}
}
