using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= password&gt;" /> element on the server.</summary>
[DefaultEvent("ServerChange")]
[ValidationProperty("Value")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputPassword : HtmlInputText, IPostBackDataHandler
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> class using default values.</summary>
	public HtmlInputPassword()
		: base("password")
	{
	}

	/// <summary>Renders the attributes of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputPassword" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		base.Attributes.Remove("value");
		base.RenderAttributes(writer);
	}

	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[postDataKey];
		if (base.Attributes["value"] != text)
		{
			base.Attributes["value"] = text;
			return true;
		}
		return false;
	}

	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		OnServerChange(EventArgs.Empty);
	}
}
