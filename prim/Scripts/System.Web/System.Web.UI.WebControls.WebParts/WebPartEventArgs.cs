namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, and <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> events. </summary>
public class WebPartEventArgs : EventArgs
{
	private WebPart _webPart;

	/// <summary>Gets the Web Parts control involved in the event.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> involved in a <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartAdded" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartDeleted" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartClosed" />, <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.WebPartMoved" />, or <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event.</returns>
	public WebPart WebPart => _webPart;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> class. </summary>
	/// <param name="webPart">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> (or server or user control) involved in the event. </param>
	public WebPartEventArgs(WebPart webPart)
	{
		_webPart = webPart;
	}
}
