using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.AdRotator.AdCreated" /> event of the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class AdCreatedEventArgs : EventArgs
{
	private IDictionary properties;

	private string alt_text;

	private string img_url;

	private string nav_url;

	/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> object that contains all the advertisement properties for the currently displayed advertisement.</summary>
	/// <returns>A <see cref="T:System.Collections.IDictionary" /> that contains a list of advertisement properties for the currently displayed advertisement. The default value is <see cref="F:System.String.Empty" />.</returns>
	public IDictionary AdProperties => properties;

	/// <summary>Gets or sets the alternate text displayed in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control when the advertisement image is unavailable. Browsers that support the ToolTips feature display this text as a ToolTip for the advertisement.</summary>
	/// <returns>The text displayed in place of the advertisement image if the image is unavailable. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string AlternateText
	{
		get
		{
			return alt_text;
		}
		set
		{
			alt_text = value;
		}
	}

	/// <summary>Gets or sets the URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control.</summary>
	/// <returns>The URL of an image to display in the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string ImageUrl
	{
		get
		{
			return img_url;
		}
		set
		{
			img_url = value;
		}
	}

	/// <summary>Gets or sets the Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked.</summary>
	/// <returns>The Web page to display when the <see cref="T:System.Web.UI.WebControls.AdRotator" /> control is clicked. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string NavigateUrl
	{
		get
		{
			return nav_url;
		}
		set
		{
			nav_url = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AdCreatedEventArgs" /> class.</summary>
	/// <param name="adProperties">A <see cref="T:System.Collections.IDictionary" /> containing the advertisement properties from the XML file. </param>
	public AdCreatedEventArgs(IDictionary adProperties)
	{
		properties = adProperties;
		if (properties != null)
		{
			alt_text = (string)properties["AlternateText"];
			img_url = (string)properties["ImageUrl"];
			nav_url = (string)properties["NavigateUrl"];
		}
	}
}
