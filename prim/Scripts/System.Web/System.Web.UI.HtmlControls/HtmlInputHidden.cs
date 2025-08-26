using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type=hidden&gt;" /> element on the server.</summary>
[DefaultEvent("ServerChange")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputHidden : HtmlInputControl, IPostBackDataHandler
{
	private static readonly object ServerChangeEvent;

	/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputControl.Value" /> property is changed on the server.</summary>
	[WebSysDescription("")]
	[WebCategory("Action")]
	public event EventHandler ServerChange
	{
		add
		{
			base.Events.AddHandler(ServerChangeEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ServerChangeEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> class.</summary>
	public HtmlInputHidden()
		: base("hidden")
	{
	}

	private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[postDataKey];
		if (text != null && text != Value)
		{
			ValidateEvent(postDataKey, string.Empty);
			Value = text;
			return true;
		}
		return false;
	}

	private void RaisePostDataChangedEventInternal()
	{
		OnServerChange(EventArgs.Empty);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostDataInternal(postDataKey, postCollection);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEventInternal();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(Name);
		base.RenderAttributes(writer);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
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

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputHidden.ServerChange" /> event. This method allows you to handle the event directly.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[ServerChange])?.Invoke(this, e);
	}

	static HtmlInputHidden()
	{
		ServerChange = new object();
	}
}
