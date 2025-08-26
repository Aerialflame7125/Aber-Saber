using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= image&gt;" /> element on the server.</summary>
[DefaultEvent("ServerClick")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputImage : HtmlInputControl, IPostBackDataHandler, IPostBackEventHandler
{
	private static readonly object ServerClickEvent;

	private int clicked_x;

	private int clicked_y;

	/// <summary>Gets or sets a value indicating whether validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> if validation is performed when the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control is clicked; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
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

	/// <summary>Gets or sets the alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</summary>
	/// <returns>The alignment of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control in relation to other elements on the Web page.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string Align
	{
		get
		{
			return GetAtt("align");
		}
		set
		{
			SetAtt("align", value);
		}
	}

	/// <summary>Gets or sets the alternative text that the browser displays if the image is unavailable or has not been downloaded.</summary>
	/// <returns>The alternative text for the specified image. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string Alt
	{
		get
		{
			return GetAtt("alt");
		}
		set
		{
			SetAtt("alt", value);
		}
	}

	/// <summary>Gets or sets the location of the image file.</summary>
	/// <returns>The location of the image file. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebCategory("Appearance")]
	[UrlProperty]
	public string Src
	{
		get
		{
			return GetAtt("src");
		}
		set
		{
			SetAtt("src", value);
		}
	}

	/// <summary>Gets or sets the border width for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
	/// <returns>The border width, in pixels, for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</returns>
	[DefaultValue("-1")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Border
	{
		get
		{
			string text = base.Attributes["border"];
			if (text == null)
			{
				return -1;
			}
			return int.Parse(text, Helpers.InvariantCulture);
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("border");
			}
			else
			{
				base.Attributes["border"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server.</summary>
	/// <returns>The group of controls for which the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control causes validation when it posts back to the server. The default value is an empty string (""), indicating that this property is not set. </returns>
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

	/// <summary>Occurs on the server when the user clicks an <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event ImageClickEventHandler ServerClick
	{
		add
		{
			base.Events.AddHandler(ServerClickEvent, value);
		}
		remove
		{
			base.Events.AddHandler(ServerClickEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> class.</summary>
	public HtmlInputImage()
		: base("image")
	{
	}

	private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[UniqueID + ".x"];
		string text2 = postCollection[UniqueID + ".y"];
		if (text != null && text.Length != 0 && text2 != null && text2.Length != 0)
		{
			clicked_x = int.Parse(text, Helpers.InvariantCulture);
			clicked_y = int.Parse(text2, Helpers.InvariantCulture);
			Page.RegisterRequiresRaiseEvent(this);
			return true;
		}
		return false;
	}

	private void RaisePostBackEventInternal(string eventArgument)
	{
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnServerClick(new ImageClickEventArgs(clicked_x, clicked_y));
	}

	private void RaisePostDataChangedEventInternal()
	{
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostDataInternal(postDataKey, postCollection);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control when it posts back to the server.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEventInternal(eventArgument);
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		RaisePostDataChangedEventInternal();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputImage.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Enables the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control to raise events on postback.</summary>
	/// <param name="eventArgument">The argument for the event.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && !base.Disabled)
		{
			page.RegisterRequiresPostBack(this);
			page.RegisterEnabledControl(this);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputImage.ServerClick" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.ImageClickEventArgs" /> that contains event data. </param>
	protected virtual void OnServerClick(ImageClickEventArgs e)
	{
		if (base.Events[ServerClick] is ImageClickEventHandler imageClickEventHandler)
		{
			imageClickEventHandler(this, e);
		}
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.HtmlControls.HtmlInputImage.Src" /> property contains a malformed URL.</exception>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.ClientScript.RegisterForEventValidation(UniqueID);
		if (CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup))
		{
			ClientScriptManager clientScript = page.ClientScript;
			base.Attributes["onclick"] += clientScript.GetClientValidationEvent(ValidationGroup);
		}
		PreProcessRelativeReference(writer, "src");
		base.RenderAttributes(writer);
	}

	private void SetAtt(string name, string value)
	{
		if (value == null || value.Length == 0)
		{
			base.Attributes.Remove(name);
		}
		else
		{
			base.Attributes[name] = value;
		}
	}

	private string GetAtt(string name)
	{
		string text = base.Attributes[name];
		if (text == null)
		{
			return string.Empty;
		}
		return text;
	}

	static HtmlInputImage()
	{
		ServerClick = new object();
	}
}
