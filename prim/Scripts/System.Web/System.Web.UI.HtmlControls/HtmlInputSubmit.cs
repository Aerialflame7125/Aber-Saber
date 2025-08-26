using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= submit&gt;" /> element on the server.</summary>
[DefaultEvent("ServerClick")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputSubmit : HtmlInputButton, IPostBackEventHandler
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using default values.</summary>
	public HtmlInputSubmit()
		: base("submit")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputSubmit" /> class using the specified type.</summary>
	/// <param name="type">The input button type. </param>
	public HtmlInputSubmit(string type)
		: base(type)
	{
	}

	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		base.RaisePostBackEvent(eventArgument);
	}
}
