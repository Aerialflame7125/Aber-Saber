using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Manages the list of documents and Web sites the user has visited within the current session.</summary>
/// <filterpriority>2</filterpriority>
public sealed class HtmlHistory : IDisposable
{
	private bool disposed;

	private IWebBrowser webHost;

	private IHistory history;

	/// <summary>Gets the size of the history stack.</summary>
	/// <returns>The current number of entries in the Uniform Resource Locator (URL) history. </returns>
	/// <filterpriority>1</filterpriority>
	public int Length => webHost.Navigation.HistoryCount;

	/// <summary>Gets the unmanaged interface wrapped by this class. </summary>
	/// <returns>An <see cref="T:System.Object" /> that can be cast into an IOmHistory interface pointer.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Not supported, will throw NotSupportedException")]
	public object DomHistory
	{
		get
		{
			throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
		}
	}

	internal HtmlHistory(IWebBrowser webHost, IHistory history)
	{
		this.webHost = webHost;
		this.history = history;
	}

	private void Dispose(bool disposing)
	{
		if (!disposed)
		{
			disposed = true;
		}
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.HtmlHistory" />. </summary>
	/// <filterpriority>1</filterpriority>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Navigates backward in the navigation stack by the specified number of entries.</summary>
	/// <param name="numberBack"></param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer. </exception>
	/// <filterpriority>1</filterpriority>
	public void Back(int numberBack)
	{
		history.Back(numberBack);
	}

	/// <summary>Navigates forward in the navigation stack by the specified number of entries. </summary>
	/// <param name="numberForward"></param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">Argument is not a positive 32-bit integer. </exception>
	/// <filterpriority>1</filterpriority>
	public void Forward(int numberForward)
	{
		history.Forward(numberForward);
	}

	/// <summary>Navigates to the specified relative position in the browser's history. </summary>
	/// <param name="relativePosition"></param>
	/// <filterpriority>1</filterpriority>
	public void Go(int relativePosition)
	{
		history.GoToIndex(relativePosition);
	}

	/// <summary>Navigates to the specified Uniform Resource Locator (URL). </summary>
	/// <param name="urlString"></param>
	/// <filterpriority>1</filterpriority>
	public void Go(string urlString)
	{
		history.GoToUrl(urlString);
	}

	/// <summary>Navigates to the specified Uniform Resource Locator (URL). </summary>
	/// <param name="url">The URL as a <see cref="T:System.Uri" /> object.</param>
	public void Go(Uri url)
	{
		history.GoToUrl(url.ToString());
	}
}
