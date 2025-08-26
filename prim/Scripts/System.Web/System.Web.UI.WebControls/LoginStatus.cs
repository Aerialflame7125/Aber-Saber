using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Detects the user's authentication state and toggles the state of a link to log in to or log out of a Web site.</summary>
[Bindable(false)]
[DefaultEvent("LoggingOut")]
[Designer("System.Web.UI.Design.WebControls.LoginStatusDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class LoginStatus : CompositeControl
{
	private static readonly object loggedOutEvent = new object();

	private static readonly object loggingOutEvent = new object();

	private LinkButton logoutLinkButton;

	private ImageButton logoutImageButton;

	private LinkButton loginLinkButton;

	private ImageButton loginImageButton;

	/// <summary>Gets or sets the URL of the image used for the login link.</summary>
	/// <returns>A string containing the URL of the image used for the login link. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string LoginImageUrl
	{
		get
		{
			object obj = ViewState["LoginImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LoginImageUrl");
			}
			else
			{
				ViewState["LoginImageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text used for the login link.</summary>
	/// <returns>A string displayed as the login link. The default is "Login".</returns>
	[Localizable(true)]
	public virtual string LoginText
	{
		get
		{
			object obj = ViewState["LoginText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Login");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LoginText");
			}
			else
			{
				ViewState["LoginText"] = value;
			}
		}
	}

	/// <summary>Gets or sets a value that determines the action taken when a user logs out of a Web site with the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LogoutAction" /> values. The default is <see cref="F:System.Web.UI.WebControls.LogoutAction.Refresh" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The property is set to an invalid <see cref="T:System.Web.UI.WebControls.LogoutAction" /> value. </exception>
	[DefaultValue(LogoutAction.Refresh)]
	[Themeable(false)]
	public virtual LogoutAction LogoutAction
	{
		get
		{
			object obj = ViewState["LogoutAction"];
			if (obj != null)
			{
				return (LogoutAction)obj;
			}
			return LogoutAction.Refresh;
		}
		set
		{
			if (value < LogoutAction.Refresh || value > LogoutAction.RedirectToLoginPage)
			{
				throw new ArgumentOutOfRangeException("LogoutAction");
			}
			ViewState["LogoutAction"] = (int)value;
		}
	}

	/// <summary>Gets or sets the URL of the image used for the logout button.</summary>
	/// <returns>A string containing the URL of the image used for the logout link. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string LogoutImageUrl
	{
		get
		{
			object obj = ViewState["LogoutImageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LogoutImageUrl");
			}
			else
			{
				ViewState["LogoutImageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the URL of the logout page.</summary>
	/// <returns>A string containing the URL of the logout page. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Themeable(false)]
	[UrlProperty]
	public virtual string LogoutPageUrl
	{
		get
		{
			object obj = ViewState["LogoutPageUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LogoutPageUrl");
			}
			else
			{
				ViewState["LogoutPageUrl"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text used for the logout link.</summary>
	/// <returns>A string displayed as the logout link. The default is "Logout".</returns>
	[Localizable(true)]
	public virtual string LogoutText
	{
		get
		{
			object obj = ViewState["LogoutText"];
			if (obj != null)
			{
				return (string)obj;
			}
			return Locale.GetText("Logout");
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("LogoutText");
			}
			else
			{
				ViewState["LogoutText"] = value;
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
	/// <returns>Always returns <see cref="F:System.Web.UI.HtmlTextWriterTag.A" />.</returns>
	protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.A;

	/// <summary>Raised after the user clicks the logout link and the logout process is complete.</summary>
	public event EventHandler LoggedOut
	{
		add
		{
			base.Events.AddHandler(loggedOutEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(loggedOutEvent, value);
		}
	}

	/// <summary>Raised when the user clicks the logout button.</summary>
	public event LoginCancelEventHandler LoggingOut
	{
		add
		{
			base.Events.AddHandler(loggingOutEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(loggingOutEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> class. </summary>
	public LoginStatus()
	{
	}

	/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		logoutLinkButton = new LinkButton();
		logoutLinkButton.CausesValidation = false;
		logoutLinkButton.Command += LogoutClick;
		logoutImageButton = new ImageButton();
		logoutImageButton.CausesValidation = false;
		logoutImageButton.Command += LogoutClick;
		loginLinkButton = new LinkButton();
		loginLinkButton.CausesValidation = false;
		loginLinkButton.Command += LoginClick;
		loginImageButton = new ImageButton();
		loginImageButton.CausesValidation = false;
		loginImageButton.Command += LoginClick;
		Controls.Add(logoutLinkButton);
		Controls.Add(logoutImageButton);
		Controls.Add(loginLinkButton);
		Controls.Add(loginImageButton);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggedOut" /> event after the user clicks the logout link and logout processing is complete.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnLoggedOut(EventArgs e)
	{
		((EventHandler)base.Events[loggedOutEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginStatus.LoggingOut" /> event when a user clicks the logout link on the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> that contains event data.</param>
	protected virtual void OnLoggingOut(LoginCancelEventArgs e)
	{
		((LoginCancelEventHandler)base.Events[loggingOutEvent])?.Invoke(this, e);
	}

	/// <summary>Determines whether a user is logged in, and gets the URL of the login page.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (writer != null)
		{
			RenderContents(writer);
		}
	}

	/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (writer != null)
		{
			EnsureChildControls();
			bool flag = false;
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
				flag = Page.Request.IsAuthenticated;
			}
			bool flag2 = LogoutImageUrl.Length > 0;
			logoutLinkButton.Visible = flag && !flag2;
			logoutImageButton.Visible = flag && flag2;
			bool flag3 = LoginImageUrl.Length > 0;
			loginLinkButton.Visible = !flag && !flag3;
			loginImageButton.Visible = !flag && flag3;
			if (logoutLinkButton.Visible)
			{
				logoutLinkButton.Text = LogoutText;
				logoutLinkButton.CssClass = CssClass;
				logoutLinkButton.Render(writer);
			}
			else if (logoutImageButton.Visible)
			{
				logoutImageButton.AlternateText = LogoutText;
				logoutImageButton.CssClass = CssClass;
				logoutImageButton.ImageUrl = LogoutImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, logoutImageButton.UniqueID);
				logoutImageButton.Render(writer);
			}
			else if (loginLinkButton.Visible)
			{
				loginLinkButton.Text = LoginText;
				loginLinkButton.CssClass = CssClass;
				loginLinkButton.Render(writer);
			}
			else if (loginImageButton.Visible)
			{
				loginImageButton.AlternateText = LoginText;
				loginImageButton.CssClass = CssClass;
				loginImageButton.ImageUrl = LoginImageUrl;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, loginImageButton.UniqueID);
				loginImageButton.Render(writer);
			}
		}
	}

	/// <summary>Overrides the base <see cref="M:System.Web.UI.Control.SetDesignModeState(System.Collections.IDictionary)" /> method. </summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the state of the <see cref="T:System.Web.UI.WebControls.LoginStatus" /> control.</param>
	[MonoTODO("for design-time usage - no more details available")]
	protected override void SetDesignModeState(IDictionary data)
	{
		base.SetDesignModeState(data);
	}

	private void LogoutClick(object sender, CommandEventArgs e)
	{
		LoginCancelEventArgs loginCancelEventArgs = new LoginCancelEventArgs(cancel: false);
		OnLoggingOut(loginCancelEventArgs);
		if (loginCancelEventArgs.Cancel)
		{
			return;
		}
		FormsAuthentication.SignOut();
		OnLoggedOut(e);
		switch (LogoutAction)
		{
		case LogoutAction.Refresh:
			HttpContext.Current.Response.Redirect(Page.Request.Url.AbsoluteUri);
			break;
		case LogoutAction.RedirectToLoginPage:
			FormsAuthentication.RedirectToLoginPage();
			break;
		case LogoutAction.Redirect:
		{
			string text = LogoutPageUrl;
			if (text.Length == 0)
			{
				text = Page.Request.Url.AbsoluteUri;
			}
			HttpContext.Current.Response.Redirect(text);
			break;
		}
		}
	}

	private void LoginClick(object sender, CommandEventArgs e)
	{
		FormsAuthentication.RedirectToLoginPage();
	}
}
