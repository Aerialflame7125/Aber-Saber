using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;button&gt;" /> tag on the server.</summary>
[DefaultEvent("ServerClick")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlButton : HtmlContainerControl, IPostBackEventHandler
{
	private static readonly object ServerClickEvent;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool CausesValidation
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

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control causes validation when it posts back to the server. The default value is an empty string ("") indicating that this property is not set.</returns>
	[DefaultValue("")]
	public virtual string ValidationGroup
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

	/// <summary>Occurs when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control on the client Web page.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerClick
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> class.</summary>
	public HtmlButton()
		: base("button")
	{
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The event arguments.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control when it posts back to the server. </summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnServerClick(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlButton.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnServerClick(EventArgs e)
	{
		((EventHandler)base.Events[ServerClick])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlButton" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page page = Page;
		if (page != null && (object)base.Events[ServerClick] != null)
		{
			PostBackOptions postBackOptions = GetPostBackOptions();
			base.Attributes["onclick"] += page.ClientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
			writer.WriteAttribute("language", "javascript");
		}
		base.RenderAttributes(writer);
	}

	private PostBackOptions GetPostBackOptions()
	{
		Page page = Page;
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ValidationGroup = null;
		postBackOptions.ActionUrl = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = true;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	static HtmlButton()
	{
		ServerClick = new object();
	}
}
