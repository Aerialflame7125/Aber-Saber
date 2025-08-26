using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Allows programmatic access to the <see langword="&lt;textarea&gt;" /> HTML element on the server.</summary>
[DefaultEvent("ServerChange")]
[ValidationProperty("Value")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlTextArea : HtmlContainerControl, IPostBackDataHandler
{
	private static readonly object serverChangeEvent = new object();

	/// <summary>Gets or sets the width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
	/// <returns>The width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is <see langword="-1" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Cols
	{
		get
		{
			string text = base.Attributes["cols"];
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
				base.Attributes.Remove("cols");
			}
			else
			{
				base.Attributes["cols"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
	/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string Name
	{
		get
		{
			return UniqueID;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
	/// <returns>The height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is <see langword="-1" />, which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public int Rows
	{
		get
		{
			string text = base.Attributes["rows"];
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
				base.Attributes.Remove("rows");
			}
			else
			{
				base.Attributes["rows"] = value.ToString(Helpers.InvariantCulture);
			}
		}
	}

	/// <summary>Gets or sets the text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
	/// <returns>The text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public string Value
	{
		get
		{
			return InnerText;
		}
		set
		{
			InnerText = value;
		}
	}

	/// <summary>Occurs when the content of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control changes between posts to the server.</summary>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> class.</summary>
	public HtmlTextArea()
		: base("textarea")
	{
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that an object was parsed and adds the object to the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's <see cref="T:System.Web.UI.ControlCollection" /> object. </summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
	/// <exception cref="T:System.Web.HttpException">The object specified by the <paramref name="obj" /> parameter can only be of the type <see cref="T:System.Web.UI.LiteralControl" /> or <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (!(obj is LiteralControl) && !(obj is DataBoundLiteralControl))
		{
			throw new HttpException(Locale.GetText("Wrong type."));
		}
		base.AddParsedSubObject(obj);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
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

	/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlTextArea.ServerChange" /> event of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. This allows you to provide a custom handler for the event.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnServerChange(EventArgs e)
	{
		((EventHandler)base.Events[serverChangeEvent])?.Invoke(this, e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		Page?.ClientScript.RegisterForEventValidation(UniqueID);
		if (base.Attributes["name"] == null)
		{
			writer.WriteAttribute("name", Name);
		}
		base.RenderAttributes(writer);
	}

	/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of the postback; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return DefaultLoadPostData(postDataKey, postCollection);
	}

	/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlTextArea.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that the state of the control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		ValidateEvent(UniqueID, string.Empty);
		OnServerChange(EventArgs.Empty);
	}

	internal bool DefaultLoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[postDataKey];
		if (text != null && text != Value)
		{
			Value = text;
			return true;
		}
		return false;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of postback; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
