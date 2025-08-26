namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.DocumentCompleted" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class WebBrowserDocumentCompletedEventArgs : EventArgs
{
	private Uri url;

	/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control has navigated.</summary>
	/// <returns>A <see cref="T:System.Uri" /> representing the location of the document that was loaded.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Uri Url => url;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserDocumentCompletedEventArgs" /> class.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document that was loaded. </param>
	public WebBrowserDocumentCompletedEventArgs(Uri url)
	{
		this.url = url;
	}
}
