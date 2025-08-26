using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" /> event, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" /> event, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event. </summary>
public class WebPartCancelEventArgs : CancelEventArgs
{
	private WebPart _webPart;

	/// <summary>Gets the Web Parts control involved in the cancelable event.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanging" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosing" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleting" /> event.</returns>
	public WebPart WebPart
	{
		get
		{
			return _webPart;
		}
		set
		{
			_webPart = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCancelEventArgs" /> class. </summary>
	/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
	public WebPartCancelEventArgs(WebPart webPart)
	{
		_webPart = webPart;
	}
}
