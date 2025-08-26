using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= text&gt;" /> and <see langword="&lt;input type= password&gt;" /> elements on the server.</summary>
[DefaultEvent("ServerChange")]
[ValidationProperty("Value")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputText : HtmlInputControl, IPostBackDataHandler
{
	private static readonly object serverChangeEvent = new object();

	/// <summary>Gets or sets the maximum number of characters that can be entered in the text box.</summary>
	/// <returns>The maximum number of characters that can be entered in the text box.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public int MaxLength
	{
		get
		{
			string text = base.Attributes["maxlength"];
			if (text != null)
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("maxlength");
			}
			else
			{
				base.Attributes["maxlength"] = value.ToString();
			}
		}
	}

	/// <summary>Gets or sets the width of the text box.</summary>
	/// <returns>The width, in characters, of the text box.</returns>
	[DefaultValue(-1)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Size
	{
		get
		{
			string text = base.Attributes["size"];
			if (text != null)
			{
				return Convert.ToInt32(text);
			}
			return -1;
		}
		set
		{
			if (value == -1)
			{
				base.Attributes.Remove("size");
			}
			else
			{
				base.Attributes["size"] = value.ToString();
			}
		}
	}

	/// <summary>Gets or sets the contents of the text box.</summary>
	/// <returns>The text contained in the text box. The default is an empty string ("").</returns>
	public override string Value
	{
		get
		{
			string text = base.Attributes["value"];
			if (text != null)
			{
				return text;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("value");
			}
			else
			{
				base.Attributes["value"] = value;
			}
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputText.Value" /> property is changed on the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerChange
	{
		add
		{
			base.Events.AddHandler(serverChangeEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(serverChangeEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using default values.</summary>
	public HtmlInputText()
		: base("text")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using the specified input control type.</summary>
	/// <param name="type">The type of input control. </param>
	public HtmlInputText(string type)
		: base(type)
	{
	}

	protected internal override void Render(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID);
		base.Render(writer);
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

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputText.ServerChange" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[serverChangeEvent])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		if (string.Compare(base.Type, 0, "password", 0, 8, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			base.Attributes.Remove("value");
		}
		base.RenderAttributes(writer);
	}

	private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[postDataKey];
		if (Value != text)
		{
			Value = text;
			return true;
		}
		return false;
	}

	private void RaisePostDataChangedEventInternal()
	{
		OnServerChange(EventArgs.Empty);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control. </summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostDataInternal(postDataKey, postCollection);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		RaisePostDataChangedEventInternal();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
