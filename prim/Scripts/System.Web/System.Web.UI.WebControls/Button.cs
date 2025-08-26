using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays a push button control on the Web page.</summary>
[DefaultEvent("Click")]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultProperty("Text")]
[Designer("System.Web.UI.Design.WebControls.ButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<{0}:Button runat=\"server\" Text=\"Button\"></{0}:Button>")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Button : WebControl, IPostBackEventHandler, IButtonControl
{
	private static readonly object ClickEvent;

	private static readonly object CommandEvent;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[DefaultValue(true)]
	[Themeable(false)]
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

	/// <summary>Gets or sets an optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />.</summary>
	/// <returns>An optional parameter passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event along with the associated <see cref="P:System.Web.UI.WebControls.Button.CommandName" />. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Bindable(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public string CommandArgument
	{
		get
		{
			return ViewState.GetString("CommandArgument", string.Empty);
		}
		set
		{
			ViewState["CommandArgument"] = value;
		}
	}

	/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.Button" /> control that is passed to the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event.</summary>
	/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	[Themeable(false)]
	public string CommandName
	{
		get
		{
			return ViewState.GetString("CommandName", string.Empty);
		}
		set
		{
			ViewState["CommandName"] = value;
		}
	}

	/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</summary>
	/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.Button" /> control's <see cref="E:System.Web.UI.WebControls.Button.Click" /> event is raised.</returns>
	[Themeable(false)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string OnClientClick
	{
		get
		{
			return ViewState.GetString("OnClientClick", string.Empty);
		}
		set
		{
			ViewState["OnClientClick"] = value;
		}
	}

	/// <summary>Gets or sets the text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
	/// <returns>The text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Bindable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[Localizable(true)]
	public string Text
	{
		get
		{
			return ViewState.GetString("Text", string.Empty);
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Button" /> control uses the client browser's submit mechanism or the ASP.NET postback mechanism.</summary>
	/// <returns>
	///     <see langword="true" /> if the control uses the client browser's submit mechanism; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool UseSubmitBehavior
	{
		get
		{
			return ViewState.GetBool("UseSubmitBehavior", def: true);
		}
		set
		{
			ViewState["UseSubmitBehavior"] = value;
		}
	}

	/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
	/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[UrlProperty("*.aspx")]
	public virtual string PostBackUrl
	{
		get
		{
			return ViewState.GetString("PostBackUrl", string.Empty);
		}
		set
		{
			ViewState["PostBackUrl"] = value;
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.Button" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string ValidationGroup
	{
		get
		{
			return ViewState.GetString("ValidationGroup", string.Empty);
		}
		set
		{
			ViewState["ValidationGroup"] = value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.Button" /> control is clicked.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event CommandEventHandler Command
	{
		add
		{
			base.Events.AddHandler(CommandEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CommandEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Button" /> class.</summary>
	public Button()
		: base(HtmlTextWriterTag.Input)
	{
	}

	/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.Button" /> control to the output stream for rendering on the client.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.VerifyRenderingInServerForm(this);
		writer.AddAttribute(HtmlTextWriterAttribute.Type, UseSubmitBehavior ? "submit" : "button", fEncode: false);
		writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		writer.AddAttribute(HtmlTextWriterAttribute.Value, Text);
		string onClientClick = OnClientClick;
		onClientClick = ClientScriptManager.EnsureEndsWithSemicolon(onClientClick);
		if (base.HasAttributes && base.Attributes["onclick"] != null)
		{
			onClientClick = ClientScriptManager.EnsureEndsWithSemicolon(onClientClick + base.Attributes["onclick"]);
			base.Attributes.Remove("onclick");
		}
		if (page != null)
		{
			onClientClick += GetClientScriptEventReference();
		}
		if (onClientClick.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClick);
		}
		base.AddAttributesToRender(writer);
	}

	internal virtual string GetClientScriptEventReference()
	{
		PostBackOptions postBackOptions = GetPostBackOptions();
		Page page = Page;
		if (page != null)
		{
			return page.ClientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
		}
		return string.Empty;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</summary>
	/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.Button" /> control's postback behavior.</returns>
	protected virtual PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ActionUrl = ((PostBackUrl.Length > 0) ? Page.ResolveClientUrl(PostBackUrl) : null);
		postBackOptions.ValidationGroup = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = !UseSubmitBehavior;
		Page page = Page;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Click" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
	/// <param name="e">The event data. </param>
	protected virtual void OnClick(EventArgs e)
	{
		if (base.Events != null)
		{
			((EventHandler)base.Events[Click])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Button.Command" /> event of the <see cref="T:System.Web.UI.WebControls.Button" /> control.</summary>
	/// <param name="e">The event data. </param>
	protected virtual void OnCommand(CommandEventArgs e)
	{
		if (base.Events != null)
		{
			((CommandEventHandler)base.Events[Command])?.Invoke(this, e);
		}
		RaiseBubbleEvent(this, e);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.Button" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (CausesValidation)
		{
			Page?.Validate(ValidationGroup);
		}
		OnClick(EventArgs.Empty);
		OnCommand(new CommandEventArgs(CommandName, CommandArgument));
	}

	/// <summary>Determines whether the button has been clicked prior to rendering on the client.</summary>
	/// <param name="e">The event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the contents of the control to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
	}

	static Button()
	{
		Click = new object();
		Command = new object();
	}
}
