using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.WebBrowser.Navigating" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class WebBrowserNavigatingEventArgs : CancelEventArgs
{
	private Uri url;

	private string target_frame_name;

	/// <summary>Gets the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</summary>
	/// <returns>A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Uri Url => url;

	/// <summary>Gets the name of the Web page frame in which the new document will be loaded.</summary>
	/// <returns>The name of the frame in which the new document will be loaded.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public string TargetFrameName => target_frame_name;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WebBrowserNavigatingEventArgs" /> class.</summary>
	/// <param name="url">A <see cref="T:System.Uri" /> representing the location of the document to which the <see cref="T:System.Windows.Forms.WebBrowser" /> control is navigating. </param>
	/// <param name="targetFrameName">The name of the Web page frame in which the new document will be loaded. </param>
	public WebBrowserNavigatingEventArgs(Uri url, string targetFrameName)
	{
		this.url = url;
		target_frame_name = targetFrameName;
	}
}
