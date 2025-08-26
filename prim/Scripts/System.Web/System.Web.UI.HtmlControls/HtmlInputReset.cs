using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type=reset&gt;" /> element on the server.</summary>
[DefaultEvent("")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputReset : HtmlInputButton
{
	private static readonly object ServerClickEvent;

	/// <summary>Gets or sets a value that indicates whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked. </summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool CausesValidation
	{
		get
		{
			return ViewState.GetBool("CausesValidation", def: true);
		}
		set
		{
			ViewState["CausesValidation"] = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. </summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control causes validation when it posts back to the server. The default value is an empty string (""), which indicates that this property is not set.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string ValidationGroup
	{
		get
		{
			return ViewState.GetString("ValidationGroup", "");
		}
		set
		{
			ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Occurs when an <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> control is clicked on the Web page. </summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler ServerClick
	{
		add
		{
			base.Events.AddHandler(ServerClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ServerClickEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using default values.</summary>
	public HtmlInputReset()
		: base("reset")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputReset" /> class using the specified input type.</summary>
	/// <param name="type">The input type.</param>
	public HtmlInputReset(string type)
		: base(type)
	{
	}

	static HtmlInputReset()
	{
		ServerClick = new object();
	}
}
