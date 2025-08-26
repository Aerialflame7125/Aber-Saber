namespace System.Web.UI.WebControls.WebParts;

/// <summary>Specifies the available types of user interfaces (UIs) for displaying Help content for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
public enum WebPartHelpMode
{
	/// <summary>Opens a separate browser window, if the browser has this capability. A user must close the window before returning to the Web Parts page. </summary>
	Modal,
	/// <summary>Opens a separate browser window, if the browser has this capability. A user does not have to close the window before returning to the Web page. </summary>
	Modeless,
	/// <summary>Replaces the Web Parts page in the browser window.</summary>
	Navigate
}
