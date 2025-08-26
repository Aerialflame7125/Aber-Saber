using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Implements the interfaces of an ActiveX site for use as a base class by the <see cref="T:System.Windows.Forms.WebBrowser.WebBrowserSite" /> class.</summary>
[System.MonoTODO("Needs Implementation")]
[ComVisible(true)]
public class WebBrowserSiteBase : IDisposable
{
	internal WebBrowserSiteBase()
	{
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" />. </summary>
	public void Dispose()
	{
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.WebBrowserSiteBase" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected virtual void Dispose(bool disposing)
	{
	}
}
