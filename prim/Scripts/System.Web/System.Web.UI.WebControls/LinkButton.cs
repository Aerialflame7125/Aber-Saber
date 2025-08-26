using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays a hyperlink-style button control on a Web page.</summary>
[ControlBuilder(typeof(LinkButtonControlBuilder))]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultEvent("Click")]
[DefaultProperty("Text")]
[Designer("System.Web.UI.Design.WebControls.LinkButtonDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ParseChildren(false)]
[SupportsEventValidation]
[ToolboxData("<{0}:LinkButton runat=\"server\">LinkButton</{0}:LinkButton>")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class LinkButton : WebControl, IPostBackEventHandler, IButtonControl
{
	private static readonly object ClickEvent;

	private static readonly object CommandEvent;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
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

	/// <summary>Gets or sets an optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property.</summary>
	/// <returns>An optional argument passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the associated <see cref="P:System.Web.UI.WebControls.LinkButton.CommandName" /> property. The default value is <see cref="F:System.String.Empty" />.</returns>
	[Bindable(true)]
	[DefaultValue("")]
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

	/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. This value is passed to the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event handler along with the <see cref="P:System.Web.UI.WebControls.LinkButton.CommandArgument" /> property.</summary>
	/// <returns>The command name of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets the client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised</summary>
	/// <returns>The client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is raised.</returns>
	[DefaultValue("")]
	[Themeable(false)]
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

	/// <summary>Gets or sets the text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
	/// <returns>The text caption displayed on the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control. The default value is an empty string ("").</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return ViewState.GetString("Text", string.Empty);
		}
		set
		{
			ViewState["Text"] = value;
			if (HasControls())
			{
				Controls.Clear();
			}
		}
	}

	/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
	/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[UrlProperty("*.aspx")]
	[DefaultValue("")]
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

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
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

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
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

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control is clicked.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> class.</summary>
	public LinkButton()
		: base(HtmlTextWriterTag.A)
	{
	}

	/// <summary>Adds the attributes of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control to the output stream for rendering on the client.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.VerifyRenderingInServerForm(this);
		base.AddAttributesToRender(writer);
		bool isEnabled = base.IsEnabled;
		string onClientClick = OnClientClick;
		onClientClick = ClientScriptManager.EnsureEndsWithSemicolon(onClientClick);
		if (base.HasAttributes && base.Attributes["onclick"] != null)
		{
			onClientClick = ClientScriptManager.EnsureEndsWithSemicolon(onClientClick + base.Attributes["onclick"]);
			base.Attributes.Remove("onclick");
		}
		if (onClientClick.Length > 0)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClientClick);
		}
		if (isEnabled && page != null)
		{
			PostBackOptions postBackOptions = GetPostBackOptions();
			string postBackEventReference = page.ClientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
		}
		AddDisplayStyleAttribute(writer);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control when it posts back to the server.</summary>
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

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />. </summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string ea)
	{
		RaisePostBackEvent(ea);
	}

	/// <summary>Notifies the control that an element, either XML or HTML, was parsed, and adds the element to the control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	protected override void AddParsedSubObject(object obj)
	{
		if (HasControls())
		{
			base.AddParsedSubObject(obj);
		}
		else if (!(obj is LiteralControl literalControl))
		{
			string text = Text;
			if (text.Length != 0)
			{
				Text = null;
				Controls.Add(new LiteralControl(text));
			}
			base.AddParsedSubObject(obj);
		}
		else
		{
			Text = literalControl.Text;
		}
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</summary>
	/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control's postback behavior.</returns>
	protected virtual PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		Page page = Page;
		postBackOptions.ActionUrl = ((PostBackUrl.Length <= 0) ? null : ((page != null) ? page.ResolveClientUrl(PostBackUrl) : PostBackUrl));
		postBackOptions.ValidationGroup = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.ClientSubmit = true;
		postBackOptions.RequiresJavaScriptProtocol = true;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	/// <summary>Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method.</summary>
	/// <param name="savedState">An object that represents the control state to restore.</param>
	protected override void LoadViewState(object savedState)
	{
		base.LoadViewState(savedState);
		if (ViewState["Text"] != null)
		{
			Text = (string)ViewState["Text"];
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	[MonoTODO("Why override?")]
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the contents of the control to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (HasControls() || HasRenderMethodDelegate())
		{
			base.RenderContents(writer);
		}
		else
		{
			writer.Write(Text);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(EventArgs e)
	{
		((EventHandler)base.Events[Click])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LinkButton.Command" /> event of the <see cref="T:System.Web.UI.WebControls.LinkButton" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
	protected virtual void OnCommand(CommandEventArgs e)
	{
		((CommandEventHandler)base.Events[Command])?.Invoke(this, e);
		RaiseBubbleEvent(this, e);
	}

	static LinkButton()
	{
		Click = new object();
		Command = new object();
	}
}
