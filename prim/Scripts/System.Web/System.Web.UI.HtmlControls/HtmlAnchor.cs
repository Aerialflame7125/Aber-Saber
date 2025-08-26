using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;a&gt;" /> element on the server.</summary>
[DefaultEvent("ServerClick")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlAnchor : HtmlContainerControl, IPostBackEventHandler
{
	private static readonly object serverClickEvent = new object();

	/// <summary>Gets or sets the URL target of the link specified in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
	/// <returns>The URL target of the link.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Action")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[UrlProperty]
	public string HRef
	{
		get
		{
			string text = base.Attributes["href"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove("href");
			}
			else
			{
				base.Attributes["href"] = value;
			}
		}
	}

	/// <summary>Gets or sets the bookmark name defined in the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> server control.</summary>
	/// <returns>The bookmark name.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Navigation")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Name
	{
		get
		{
			string text = base.Attributes["name"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove("name");
			}
			else
			{
				base.Attributes["name"] = value;
			}
		}
	}

	/// <summary>Gets or sets the name of the browser window or frame that displays the contents of the Web page that is linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
	/// <returns>The browser window or frame that displays the contents of the Web page linked to when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Navigation")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Target
	{
		get
		{
			string text = base.Attributes["target"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove("target");
			}
			else
			{
				base.Attributes["target"] = value;
			}
		}
	}

	/// <summary>Gets or sets the ToolTip text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control.</summary>
	/// <returns>The text displayed when the mouse pointer is placed over the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" />.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	public string Title
	{
		get
		{
			string text = base.Attributes["title"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				base.Attributes.Remove("title");
			}
			else
			{
				base.Attributes["title"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked. </summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> is clicked; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
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

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> causes validation when it posts back to the server. The default is an empty string ("").</returns>
	[DefaultValue("")]
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

	/// <summary>Occurs when the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control is clicked.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerClick
	{
		add
		{
			base.Events.AddHandler(serverClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(serverClickEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> class.</summary>
	public HtmlAnchor()
		: base("a")
	{
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers client script for generating a postback.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlAnchor.ServerClick" /> event. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnServerClick(EventArgs e)
	{
		((EventHandler)base.Events[serverClickEvent])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlAnchor.HRef" /> contains a malformed URL.</exception>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		if ((EventHandler)base.Events[serverClickEvent] != null)
		{
			PostBackOptions postBackOptions = GetPostBackOptions();
			ClientScriptManager clientScript = Page.ClientScript;
			clientScript.RegisterForEventValidation(postBackOptions);
			base.Attributes["href"] = clientScript.GetPostBackEventReference(postBackOptions, registerForEventValidation: true);
		}
		else
		{
			string hRef = HRef;
			if (hRef != string.Empty)
			{
				HRef = ResolveClientUrl(hRef);
			}
		}
		base.RenderAttributes(writer);
		base.Attributes.Remove("href");
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlAnchor" /> control when it posts back to the server. </summary>
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

	private PostBackOptions GetPostBackOptions()
	{
		Page page = Page;
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ValidationGroup = null;
		postBackOptions.ActionUrl = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = true;
		postBackOptions.ClientSubmit = true;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.Page.RaisePostBackEvent(System.Web.UI.IPostBackEventHandler,System.String)" />.</summary>
	/// <param name="eventArgument">The event arguments.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}
}
