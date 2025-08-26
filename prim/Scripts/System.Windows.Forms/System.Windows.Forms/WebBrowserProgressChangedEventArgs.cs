namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.ProgressChanged" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class WebBrowserProgressChangedEventArgs : EventArgs
{
	private long current_progress;

	private long maximum_progress;

	/// <summary>Gets the number of bytes that have been downloaded.</summary>
	/// <returns>The number of bytes that have been loaded or -1 to indicate that the download has completed.</returns>
	/// <filterpriority>1</filterpriority>
	public long CurrentProgress => current_progress;

	/// <summary>Gets the total number of bytes in the document being loaded.</summary>
	/// <returns>The total number of bytes to be loaded.</returns>
	/// <filterpriority>1</filterpriority>
	public long MaximumProgress => maximum_progress;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserProgressChangedEventArgs" /> class.</summary>
	/// <param name="currentProgress">The number of bytes that are loaded already. </param>
	/// <param name="maximumProgress">The total number of bytes to be loaded. </param>
	public WebBrowserProgressChangedEventArgs(long currentProgress, long maximumProgress)
	{
		current_progress = currentProgress;
		maximum_progress = maximumProgress;
	}
}
