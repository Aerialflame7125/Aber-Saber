using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>A control that displays an image and responds to mouse clicks on the image.</summary>
[DefaultEvent("Click")]
[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ImageButton : Image, IPostBackDataHandler, IPostBackEventHandler, IButtonControl
{
	private static readonly object ClickEvent;

	private static readonly object CommandEvent;

	private int pos_x;

	private int pos_y;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
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

	/// <summary>Gets or sets an optional argument that provides additional information about the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</summary>
	/// <returns>An optional argument that supplements the <see cref="P:System.Web.UI.WebControls.ImageButton.CommandName" /> property.</returns>
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

	/// <summary>Gets or sets the command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
	/// <returns>The command name associated with the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ImageButton" /> can be clicked to perform a post back to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	[DefaultValue(true)]
	[Bindable(true)]
	public new virtual bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the control generates an alternate-text attribute for an empty string value. </summary>
	/// <returns>
	///     <see langword="false" />, indicating that the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control does not generate an alternate-text attribute when the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property is empty.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set this property.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Themeable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool GenerateEmptyAlternateText
	{
		get
		{
			return false;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</summary>
	/// <returns>The client-side script that executes when an <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event is raised.</returns>
	[DefaultValue("")]
	[Themeable(false)]
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

	/// <summary>Gets or sets the URL of the page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked.</summary>
	/// <returns>The URL of the Web page to post to from the current page when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control is clicked. The default value is an empty string (""), which causes the page to post back to itself.</returns>
	[Themeable(false)]
	[UrlProperty("*.aspx")]
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control causes validation when it posts back to the server. The default value is an empty string ("").</returns>
	[Themeable(false)]
	[DefaultValue("")]
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

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
	/// <returns>An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration value.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Input;

	/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</summary>
	/// <returns>The value of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property.</returns>
	protected virtual string Text
	{
		get
		{
			return AlternateText;
		}
		set
		{
			AlternateText = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IButtonControl.Text" />.</summary>
	/// <returns>The text caption that is displayed for the button.</returns>
	string IButtonControl.Text
	{
		get
		{
			return Text;
		}
		set
		{
			Text = value;
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event ImageClickEventHandler Click
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

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ImageButton" /> is clicked.</summary>
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

	/// <summary>For a description of this member, see the <see cref="E:System.Web.UI.WebControls.IButtonControl.Click" /> event.</summary>
	event EventHandler IButtonControl.Click
	{
		add
		{
			base.Events.AddHandler(Click, value);
		}
		remove
		{
			base.Events.RemoveHandler(Click, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> class.</summary>
	public ImageButton()
	{
	}

	/// <summary>Adds the attributes of an <see cref="T:System.Web.UI.WebControls.ImageButton" /> to the output stream for rendering on the client.</summary>
	/// <param name="writer">The output stream to render on the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.VerifyRenderingInServerForm(this);
		writer.AddAttribute(HtmlTextWriterAttribute.Type, "image", fEncode: false);
		writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		base.AddAttributesToRender(writer);
		string onClientClick = OnClientClick;
		onClientClick = (string.IsNullOrEmpty(onClientClick) ? string.Empty : ClientScriptManager.EnsureEndsWithSemicolon(onClientClick));
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
	}

	internal virtual string GetClientScriptEventReference()
	{
		PostBackOptions postBackOptions = GetPostBackOptions();
		Page page = Page;
		if (postBackOptions.PerformValidation || !string.IsNullOrEmpty(postBackOptions.ActionUrl))
		{
			if (page == null)
			{
				return string.Empty;
			}
			return page.ClientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
		}
		page?.ClientScript.RegisterForEventValidation(postBackOptions);
		return string.Empty;
	}

	/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</summary>
	/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control's postback behavior.</returns>
	protected virtual PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		Page page = Page;
		postBackOptions.ActionUrl = ((PostBackUrl.Length <= 0) ? null : page?.ResolveClientUrl(PostBackUrl));
		postBackOptions.Argument = string.Empty;
		postBackOptions.ClientSubmit = true;
		postBackOptions.RequiresJavaScriptProtocol = true;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		else
		{
			postBackOptions.ValidationGroup = null;
		}
		return postBackOptions;
	}

	/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control.</summary>
	/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.</param>
	/// <returns>Returns <see langword="false" /> for all cases.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		string text = UniqueID;
		string text2 = postCollection[text + ".x"];
		string text3 = postCollection[text + ".y"];
		if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
		{
			pos_x = int.Parse(text2);
			pos_y = int.Parse(text3);
			Page.RegisterRequiresRaiseEvent(this);
			return true;
		}
		text2 = postCollection[text];
		if (!string.IsNullOrEmpty(text2))
		{
			pos_x = int.Parse(text2);
			pos_y = 0;
			Page.RegisterRequiresRaiseEvent(this);
			return true;
		}
		return false;
	}

	/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.ImageButton" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, string.Empty);
		if (CausesValidation)
		{
			Page?.Validate(ValidationGroup);
		}
		OnClick(new ImageClickEventArgs(pos_x, pos_y));
		OnCommand(new CommandEventArgs(CommandName, CommandArgument));
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> collection that contains value information indexed by control identifiers. </param>
	/// <returns>
	///     <see langword="true" /> if the server control's state changes as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
	/// <param name="eventArgument">The argument for the event</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Click" /> event directly.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(ImageClickEventArgs e)
	{
		if (base.Events != null)
		{
			((ImageClickEventHandler)base.Events[Click])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event and allows you to handle the <see cref="E:System.Web.UI.WebControls.ImageButton.Command" /> event directly.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
	protected virtual void OnCommand(CommandEventArgs e)
	{
		if (base.Events != null)
		{
			((CommandEventHandler)base.Events[Command])?.Invoke(this, e);
		}
		RaiseBubbleEvent(this, e);
	}

	/// <summary>Determines whether the image has been clicked prior to rendering on the client.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		Page page = Page;
		if (page != null && base.IsEnabled)
		{
			page.RegisterRequiresPostBack(this);
		}
	}

	static ImageButton()
	{
		Click = new object();
		Command = new object();
	}
}
