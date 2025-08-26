using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the HTML <see langword="&lt;input type= radio&gt;" /> element on the server.</summary>
[DefaultEvent("ServerChange")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlInputRadioButton : HtmlInputControl, IPostBackDataHandler
{
	private static readonly object serverChangeEvent = new object();

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected; otherwise, <see langword="false" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public bool Checked
	{
		get
		{
			return base.Attributes["checked"] == "checked";
		}
		set
		{
			if (value)
			{
				base.Attributes["checked"] = "checked";
			}
			else
			{
				base.Attributes.Remove("checked");
			}
		}
	}

	/// <summary>Gets or sets the name of the group that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is associated with.</summary>
	/// <returns>The group of check box controls that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is a member of.</returns>
	public override string Name
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
			if (value == null)
			{
				base.Attributes.Remove("name");
			}
			else
			{
				base.Attributes["name"] = value;
			}
		}
	}

	/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
	/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</returns>
	public override string Value
	{
		get
		{
			string text = base.Attributes["value"];
			if (text == null || text.Length == 0)
			{
				text = ID;
				if (text != null && text.Length == 0)
				{
					text = null;
				}
			}
			return text;
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

	/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.HtmlControls.HtmlInputRadioButton.Checked" /> property of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control changes between posts to the server.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class.</summary>
	public HtmlInputRadioButton()
		: base("radio")
	{
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control as one that requires postback handling.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
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

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputRadioButton.ServerChange" /> event. This allows you to create a custom event handler when the event is raised.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[serverChangeEvent])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID, Value);
		writer.WriteAttribute("value", Value, fEncode: true);
		base.Attributes.Remove("value");
		base.RenderAttributes(writer);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		bool flag = postCollection[Name] == Value;
		if (Checked == flag)
		{
			return false;
		}
		ValidateEvent(UniqueID, Value);
		Checked = flag;
		return flag;
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		OnServerChange(EventArgs.Empty);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.RaisePostDataChangedEvent" /> method.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
