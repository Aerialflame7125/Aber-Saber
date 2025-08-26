using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI.HtmlControls;

/// <summary>Provides programmatic access to the HTML <see langword="&lt;form&gt;" /> element on the server.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HtmlForm : HtmlContainerControl
{
	private bool inited;

	private string _defaultfocus;

	private string _defaultbutton;

	private bool submitdisabledcontrols;

	private bool? isUplevel;

	/// <summary>Gets or sets the action attribute of the HTML form.</summary>
	/// <returns>The action attribute of the HTML form. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string Action
	{
		get
		{
			string text = base.Attributes["action"];
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				base.Attributes["action"] = null;
			}
			else
			{
				base.Attributes["action"] = value;
			}
		}
	}

	/// <summary>Gets or sets the child control of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control that causes postback when the ENTER key is pressed.</summary>
	/// <returns>The <see cref="P:System.Web.UI.Control.ID" /> of the button control to display as the default button when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.InvalidOperationException">The control referenced as the default button is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
	[DefaultValue("")]
	public string DefaultButton
	{
		get
		{
			return _defaultbutton ?? string.Empty;
		}
		set
		{
			_defaultbutton = value;
		}
	}

	/// <summary>Gets or sets the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is loaded.</summary>
	/// <returns>The <see cref="P:System.Web.UI.Control.ClientID" /> of the control on the form to display as the control with input focus when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is loaded. The default value is an empty string ("").</returns>
	[DefaultValue("")]
	public string DefaultFocus
	{
		get
		{
			return _defaultfocus ?? string.Empty;
		}
		set
		{
			_defaultfocus = value;
		}
	}

	/// <summary>Gets or sets the encoding type a browser uses when posting the form's data to the server.</summary>
	/// <returns>A string that contains the encoding type. The default value is an empty string (""), indicating that the browser's default content type is used.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Enctype
	{
		get
		{
			string text = base.Attributes["enctype"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("enctype");
			}
			else
			{
				base.Attributes["enctype"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value that indicates how a browser posts form data to the server for processing.</summary>
	/// <returns>A string that indicates how a browser posts form data to the server. The default value is <see langword="POST" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Method
	{
		get
		{
			string text = base.Attributes["method"];
			if (text == null || text.Length == 0)
			{
				return "post";
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("method");
			}
			else
			{
				base.Attributes["method"] = value;
			}
		}
	}

	/// <summary>Gets the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <returns>A string that contains the identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" />.</returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

	/// <summary>Gets or sets a Boolean value indicating whether to force controls disabled on the client to submit their values, allowing them to preserve their values after the page posts back to the server. </summary>
	/// <returns>
	///     <see langword="true" /> if controls disabled on the client are forced to submit their values; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[DefaultValue(false)]
	public virtual bool SubmitDisabledControls
	{
		get
		{
			return submitdisabledcontrols;
		}
		set
		{
			submitdisabledcontrols = value;
		}
	}

	/// <summary>Gets or sets the frame or window in which to render the results of information that is posted to the server.</summary>
	/// <returns>The browser window or frame that displays the results of the information posted to the server. The default is an empty string (""), which refreshes the window or frame with focus. </returns>
	[DefaultValue("")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Target
	{
		get
		{
			string text = base.Attributes["target"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			if (value == null)
			{
				base.Attributes.Remove("target");
			}
			else
			{
				base.Attributes["target"] = value;
			}
		}
	}

	/// <summary>Gets the unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <returns>The unique programmatic identifier assigned to the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</returns>
	public override string UniqueID
	{
		get
		{
			if (NamingContainer == Page)
			{
				return ID;
			}
			return "aspnetForm";
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> class.</summary>
	public HtmlForm()
		: base("form")
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> collection for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's child server controls.</returns>
	[MonoTODO("why override?")]
	protected override ControlCollection CreateControlCollection()
	{
		return base.CreateControlCollection();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected internal override void OnInit(EventArgs e)
	{
		inited = true;
		Page page = Page;
		if (page != null)
		{
			page.RegisterViewStateHandler();
			page.RegisterForm(this);
		}
		base.OnInit(e);
	}

	internal bool DetermineRenderUplevel()
	{
		if (isUplevel.HasValue)
		{
			return isUplevel.Value;
		}
		isUplevel = UplevelHelper.IsUplevel(HttpCapabilitiesBase.GetUserAgentForDetection(HttpContext.Current.Request));
		return isUplevel.Value;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content. </param>
	/// <exception cref="T:System.InvalidOperationException">The control ID set in the <see cref="P:System.Web.UI.HtmlControls.HtmlForm.DefaultButton" /> property is not of the type <see cref="T:System.Web.UI.WebControls.IButtonControl" />.</exception>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		string text = base.Attributes["action"];
		Page page = Page;
		HttpRequest httpRequest = page?.RequestInternal;
		string text4;
		if (string.IsNullOrEmpty(text))
		{
			string text2 = httpRequest?.ClientFilePath;
			string text3 = httpRequest?.CurrentExecutionFilePath;
			if (text2 == null)
			{
				text4 = Action;
			}
			else if (text2 == text3)
			{
				text4 = UrlUtils.GetFile(text2);
			}
			else
			{
				bool flag = WebConfigurationManager.GetSection("system.web/sessionState") is SessionStateSection sessionStateSection && sessionStateSection.Cookieless == HttpCookieMode.UseUri;
				string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
				int length = appDomainAppVirtualPath.Length;
				if (length > 1)
				{
					if (flag)
					{
						if (StrUtils.StartsWith(text2, appDomainAppVirtualPath, ignore_case: true))
						{
							text2 = text2.Substring(length + 1);
						}
					}
					else if (StrUtils.StartsWith(text3, appDomainAppVirtualPath, ignore_case: true))
					{
						text3 = text3.Substring(length + 1);
					}
				}
				if (flag)
				{
					Uri toUri = new Uri("http://host" + text3);
					text4 = new Uri("http://host" + text2).MakeRelative(toUri);
				}
				else
				{
					text4 = text3;
				}
			}
		}
		else
		{
			text4 = text;
		}
		if (httpRequest != null)
		{
			text4 += httpRequest.QueryStringRaw;
		}
		if (httpRequest != null && !(WebConfigurationManager.GetSection("system.web/xhtmlConformance") is XhtmlConformanceSection { Mode: XhtmlConformanceMode.Strict }) && base.RenderingCompatibilityLessThan40)
		{
			writer.WriteAttribute("name", Name);
		}
		writer.WriteAttribute("method", Method);
		if (string.IsNullOrEmpty(text))
		{
			writer.WriteAttribute("action", text4, fEncode: true);
		}
		if (ID == null)
		{
			_ = ClientID;
		}
		string value = page?.GetSubmitStatements();
		if (!string.IsNullOrEmpty(value))
		{
			base.Attributes.Remove("onsubmit");
			writer.WriteAttribute("onsubmit", value);
		}
		string enctype = Enctype;
		if (!string.IsNullOrEmpty(enctype))
		{
			writer.WriteAttribute("enctype", enctype);
		}
		string target = Target;
		if (!string.IsNullOrEmpty(target))
		{
			writer.WriteAttribute("target", target);
		}
		string defaultButton = DefaultButton;
		if (!string.IsNullOrEmpty(defaultButton))
		{
			Control control = FindControl(defaultButton);
			if (control == null || !(control is IButtonControl))
			{
				throw new InvalidOperationException($"The DefaultButton of '{ID}' must be the ID of a control of type IButtonControl.");
			}
			if (page != null && DetermineRenderUplevel())
			{
				writer.WriteAttribute("onkeypress", "javascript:return " + page.WebFormScriptReference + ".WebForm_FireDefaultButton(event, '" + control.ClientID + "')");
			}
		}
		base.Attributes.Remove("method");
		base.Attributes.Remove("enctype");
		base.Attributes.Remove("target");
		base.RenderAttributes(writer);
	}

	/// <summary>Renders the child controls of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	/// <exception cref="T:System.Web.HttpException">The Web page has more than one server-side <see langword="&lt;form&gt;" /> tag.</exception>
	protected internal override void RenderChildren(HtmlTextWriter writer)
	{
		Page page = Page;
		if (!inited && page != null)
		{
			page.RegisterViewStateHandler();
			page.RegisterForm(this);
		}
		page?.OnFormRender(writer, ClientID);
		base.RenderChildren(writer);
		page?.OnFormPostRender(writer, ClientID);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the form control content.</param>
	[MonoTODO("why override?")]
	public override void RenderControl(HtmlTextWriter writer)
	{
		base.RenderControl(writer);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is not rendered without a reference to the <see cref="T:System.Web.UI.Page" /> instance.</exception>
	protected internal override void Render(HtmlTextWriter output)
	{
		base.Render(output);
	}
}
