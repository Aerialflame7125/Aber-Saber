using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= checkbox&gt;" /> element on the server.</summary>
[DefaultEvent("ServerChange")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputCheckBox : HtmlInputControl, IPostBackDataHandler
{
	private static readonly object EventServerChange = new object();

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control is selected; otherwise, <see langword="false" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
	public bool Checked
	{
		get
		{
			if (base.Attributes["checked"] == null)
			{
				return false;
			}
			return true;
		}
		set
		{
			if (!value)
			{
				base.Attributes.Remove("checked");
			}
			else
			{
				base.Attributes["checked"] = "checked";
			}
		}
	}

	/// <summary>Occurs when the Web page is submitted to the server and the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control changes state from the previous post.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerChange
	{
		add
		{
			base.Events.AddHandler(EventServerChange, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventServerChange, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> class.</summary>
	public HtmlInputCheckBox()
		: base("checkbox")
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> instance.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that contains the output stream to render on the client.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID);
		base.RenderAttributes(writer);
	}

	/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers the control as one that requires postback handling.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
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

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputCheckBox.ServerChange" /> event. This method allows you to handle the event directly.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event information. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[EventServerChange])?.Invoke(this, e);
	}

	private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[postDataKey];
		bool flag = text != null && text.Length > 0;
		if (Checked != flag)
		{
			Checked = flag;
			return true;
		}
		return false;
	}

	private void RaisePostDataChangedEventInternal()
	{
		OnServerChange(EventArgs.Empty);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control. </summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback event; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostDataInternal(postDataKey, postCollection);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		RaisePostDataChangedEventInternal();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
