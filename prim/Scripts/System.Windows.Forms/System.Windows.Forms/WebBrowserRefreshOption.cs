namespace System.Windows.Forms;

/// <summary>Specifies constants that define how the <see cref="T:System.Windows.Forms.WebBrowser" /> control can refresh its contents.</summary>
/// <filterpriority>2</filterpriority>
public enum WebBrowserRefreshOption
{
	/// <summary>A refresh that requests a copy of the current Web page that has been cached on the server.</summary>
	Normal,
	/// <summary>A refresh that requests an update only if the current Web page has expired.</summary>
	IfExpired,
	/// <summary>For internal use only; do not use.</summary>
	Continue,
	/// <summary>A refresh that requests the latest version of the current Web page.</summary>
	Completely
}
